using GMGInventory.Application.Contracts;
using GMGInventory.Application.Repositories;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    [Authorize(Roles = Roles.Accounting + ","
                     + Roles.InvoicingClerk + ","
                     + Roles.SystemAdmin + ","
                     + Roles.Telemarketer)]
    public class SalesInvoiceController : Controller
    {
        private readonly IInvoiceRepository invoiceRepository;

        public SalesInvoiceController(IInvoiceRepository invoiceRepository)
        {
            this.invoiceRepository = invoiceRepository;
        }

        public async Task<IActionResult> GetInvoiceTypeOptions(int? IncludeId)
        {
            return Json(await invoiceRepository.GetInvoiceTypeOptions(IncludeId));
        }

        public async Task<IActionResult> GetInvoiceByCustomer(string search, int page, int customer)
        {
            return Json(await invoiceRepository.GetInvoiceByCustomer(search, page, customer));
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetListJson(ListFilterCommonModel filter)
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await invoiceRepository.GetListDTAsync(form, filter));
        }

        public async Task<ActionResult> Details(int? Id)
        {
            var order = await invoiceRepository.GetDetailsAsync(Id);
            if (order == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        [HttpPost]
        public async Task<JsonResult> GetItemsJson(int invoiceId)
        {
            return Json(await invoiceRepository.GetItemsAsync(invoiceId));
        }

        [HttpPost]
        public async Task<JsonResult> GetItemsDTJson(int invoiceId)
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await invoiceRepository.GetItemsDTAsync(form, invoiceId));
        }

        public async Task<ActionResult> PrintReceipt(int Id)
        {
            var invoice = await invoiceRepository.GetDetailsWithItemsAsync(Id);
            return View(invoice);
        }

        public async Task<ActionResult> Create(int? Id)
        {
            var order = await invoiceRepository.GetDetailsByPackAsync(Id);
            if (order == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(order);
        }

        [HttpPost]
        public async Task<JsonResult> SaveJson(InvoiceFormVM form)
        {
            ResponseModel<int> responseModel = new ResponseModel<int>();
            if (await invoiceRepository.IsBIRInvoiceNumberInUser(form.Id, form.BIRInvoiceNumber))
            {
                responseModel.Status = "Error";
                responseModel.Message = "BIR Invoice Number already in use";
            }
            else
            {
                var orderId = await invoiceRepository.SaveAsync(form);
                if (orderId == -1)
                {
                    responseModel.Status = "Error";
                    responseModel.Message = "Pack missing. Failed to submit.";
                    TempData["ErrorMessage"] = "Pack missing. Failed to submit.";
                }
                else
                {
                    var submitMessage = "Pack successfully " + (form.SavingAs == "post" ? "posted" : "saved");
                    responseModel.Status = "Success";
                    responseModel.Message = submitMessage;
                    TempData["SuccessMessage"] = submitMessage;
                }
                responseModel.NewRow = orderId;
            }
            return Json(responseModel);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateJson(InvoiceFormVM form)
        {
            ResponseModel<int> responseModel = new ResponseModel<int>();
            if (await invoiceRepository.IsBIRInvoiceNumberInUser(form.Id, form.BIRInvoiceNumber))
            {
                responseModel.Status = "Error";
                responseModel.Message = "BIR Invoice Number already in use";
            }
            else if (form.SavingAs == "delete" && await invoiceRepository.IsInvoiceInUse(form.Id))
            {
                responseModel.Status = "Error";
                responseModel.Message = "Cannot proceed to cancel. Invoice already in use";
            }
            else
            {
                var responseId = await invoiceRepository.UpdateAsync(form);
                if (responseId == -1)
                {
                    responseModel.Status = "Error";
                    responseModel.Message = "Sales Invoice missing. Failed to submit.";
                }
                else
                {
                    var submitMessage = "Sales Invoice successfully " + (form.SavingAs == "post" ? "posted" : form.SavingAs == "delete" ? "cancelled" : "saved");
                    responseModel.Status = "Success";
                    responseModel.Message = submitMessage;
                    TempData["SuccessMessage"] = submitMessage;
                }
                responseModel.NewRow = responseId;
            }
            return Json(responseModel);
        }
    }
}