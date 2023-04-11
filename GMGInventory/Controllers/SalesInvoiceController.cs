using GMGInventory.Application.Contracts;
using GMGInventory.Application.Repositories;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
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
            var orderId = await invoiceRepository.SaveAsync(form);
            if (orderId == -1)
            {
                responseModel.Status = "Error";
                responseModel.Message = "Sales Order missing. Failed to submit.";
            }
            else
            {
                responseModel.Status = "Success";
                responseModel.Message = "Pack successfully submitted";
            }
            responseModel.NewRow = orderId;
            return Json(responseModel);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateJson(InvoiceFormUpdateVM form)
        {
            ResponseModel<int> responseModel = new ResponseModel<int>();
            var orderId = await invoiceRepository.UpdateAsync(form);
            if (orderId == -1)
            {
                responseModel.Status = "Error";
                responseModel.Message = "Sales Invoice missing. Failed to submit.";
            }
            else
            {
                responseModel.Status = "Success";
                responseModel.Message = "Sales Invoice successfully saved.";
            }
            responseModel.NewRow = orderId;
            return Json(responseModel);
        }
    }
}