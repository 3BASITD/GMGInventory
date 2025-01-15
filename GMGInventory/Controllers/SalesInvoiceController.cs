using GMGInventory.Application.Contracts;
using GMGInventory.Application.Repositories;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class SalesInvoiceController(IAccessRepository accessRepository
                                      , IInvoiceRepository invoiceRepository) : BaseController(accessRepository)
    {

        public async Task<IActionResult> GetInvoiceTypeOptions(int? IncludeId)
        {
            return Json(await invoiceRepository.GetInvoiceTypeOptions(IncludeId));
        }

        public async Task<IActionResult> GetInvoiceByCustomer(string search, int page, int customer)
        {
            return Json(await invoiceRepository.GetInvoiceByCustomer(search, page, customer));
        }

        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesInvoice, AccessTypes.View);
            if (redirectResult != null) return redirectResult;


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetListJson(ListFilterCommonModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesInvoice, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await invoiceRepository.GetListDTAsync(form, filter));
        }

        public async Task<IActionResult> Details(int? Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesInvoice, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.SalesInvoice)).Convert();

            var order = await invoiceRepository.GetDetailsAsync(Id);
            if (order == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> GetItemsJson(int invoiceId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesInvoice, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return Json(await invoiceRepository.GetItemsAsync(invoiceId));
        }

        [HttpPost]
        public async Task<JsonResult> GetItemsDTJson(int invoiceId)
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await invoiceRepository.GetItemsDTAsync(form, invoiceId));
        }

        public async Task<IActionResult> PrintReceipt(int Id, string? Company)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesInvoice, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;


            var invoice = await invoiceRepository.GetDetailsWithItemsAsync(Id);
            if (invoice == null) return NotFound();

            TempData["CompanyTemplate"] = Company;
            return View(invoice);
        }

        public async Task<IActionResult> Create(int? Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesInvoice, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.SalesInvoice)).Convert();

            var order = await invoiceRepository.GetDetailsByPackAsync(Id);
            if (order == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> SaveJson(InvoiceFormVM form)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesInvoice, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

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
        public async Task<IActionResult> UpdateJson(InvoiceFormVM form)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesInvoice, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

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
                    var submitMessage = "Sales Invoice successfully " + (form.SavingAs == "post" ? "posted" : form.SavingAs == "delete" ? "cancelled" : form.SavingAs == "update" ? "updated" : "saved");
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