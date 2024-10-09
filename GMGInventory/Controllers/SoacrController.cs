using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class SoacrController(IAccessRepository accessRepository
                               , ISoacrRepository soaRepository
                               , ISoacrLineItemRepository soacrLineItemRepository) : BaseController(accessRepository)
    {
        #region for SOACR/Index
        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SOACR, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.SOACR)).Convert();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetListJson()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SOACR, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await soaRepository.SOACRListDT(form));
        }

        public async Task<IActionResult> GenerateCR()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SOACR, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GenerateSOAJson(GenerateSOAVM soaVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SOACR, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ResponseModel<int> responseModel = new ResponseModel<int>();
            var soaId = await soaRepository.GenerateSOAAsync(soaVM);
            if (soaId == 0)
            {
                responseModel.Status = "Error";
                responseModel.Message = "No available invoice within selected combination";
            }
            else
            {
                responseModel.Status = "Success";
                responseModel.Message = "SOA successfully added";
                responseModel.NewRow = soaId;
            }
            return Json(responseModel);
        }
        #endregion

        #region for SOACR/Invoice
        public async Task<IActionResult> Invoice()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SOACR, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.SOACR)).Convert();

            return View();
		}

		[HttpPost]
		public async Task<IActionResult> GetInvoiceJson(ListFilterSalesModel filter)
		{
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SOACR, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
			return Json(await soaRepository.GetInvoiceListDTAsync(form, filter));
        }

        public async Task<IActionResult> PrintInvoice(ListFilterSalesModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SOACR, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;

            return View(await soaRepository.PrintInvoice(filter));
        }
        #endregion

        #region for SOACR/Generate CR
        public async Task<IActionResult> GetCustomerInvoiceToAddDT(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SOACR, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await soaRepository.GetCustomerInvoiceToAddDT(form, id));
        }

        [HttpPost]
        public async Task<IActionResult> GenerateCRJson(int CustomerId, int[] InvoiceIds)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SOACR, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ResponseModel<int> responseModel = new ResponseModel<int>();
            responseModel.Status = "Success";
            responseModel.Message = "Counter Receipt Successfully added";
            responseModel.NewRow = await soaRepository.GenerateCRAsync(CustomerId, InvoiceIds);

            TempData["SuccessMessage"] = responseModel.Message;

            return Json(responseModel);
        }
        #endregion

        #region for SOACR/Details
        public async Task<IActionResult> Details(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SOACR, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.SOACR)).Convert();

            var soa = await soaRepository.GetSOACRDetails(id);
            if (soa == null)
            {
                return NotFound();
            }
            return View(soa);
        }

        [HttpPost]
        public async Task<JsonResult> GetSOACRItemsJson(int id)
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await soaRepository.GetSOACRItemsDTAsync(form, id));
        }

        [HttpPost]
        public async Task<IActionResult> EditInvoiceJson(SOACRItemsForm soaVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SOACR, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            ResponseModel responseModel = new ResponseModel();
            try
            {
                await soaRepository.UpdateSOACRInvoiceAsync(soaVM);
                responseModel.Status = "Success";
                responseModel.Message = "Invoice successfully updated";
            }
            catch (Exception ex)
            {
                if (!await soaRepository.IsSOACRLineItemIdExist(soaVM.Id))
                {
                    responseModel.Status = "Error";
                    responseModel.Message = "Invoice cannot be found";
                }
                else
                {
                    responseModel.Status = "Error";
                    responseModel.Message = ex.Message;
                }
            }
            return Json(responseModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteInvoiceJson(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SOACR, AccessTypes.Delete);
            if (redirectResult != null) return redirectResult;

            ResponseModel responseModel = new ResponseModel();
            if (await soacrLineItemRepository.IsSoaLineItemIdExistAsync(id) == false)
            {
                responseModel.Status = "Error";
                responseModel.Message = "Invoice cannot be found";
            }
            else
            {
                await soacrLineItemRepository.DeleteAsync(id);
                responseModel.Status = "Success";
                responseModel.Message = "Invoice successfully deleted";
            }
            return Json(responseModel);
        }

        [HttpPost]
        public async Task<IActionResult> CheckCheckNumberJson(string CheckNumber)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SOACR, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ResponseModel responseModel = new ResponseModel();
            if (await soaRepository.IsCheckNumberInUse(CheckNumber))
            {
                responseModel.Status = "Error";
                responseModel.Message = "Check Number already in use";
            }
            else
            {
                responseModel.Status = "Success";
                responseModel.Message = "Check Number available";
            }
            return Json(responseModel);
        }

        [HttpPost]
        public async Task<IActionResult> CollectorReceiptNumberJson(string CollectorReceiptNumber)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SOACR, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ResponseModel responseModel = new ResponseModel();
            if (await soaRepository.IsCollectorReceiptNumberInUse(CollectorReceiptNumber))
            {
                responseModel.Status = "Error";
                responseModel.Message = "Collector Receipt Number already in use";
            }
            else
            {
                responseModel.Status = "Success";
                responseModel.Message = "Collector Receipt Number available";
            }
            return Json(responseModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddPaymentDetailsJson(SOACRPaymentDetails payment)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SOACR, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            ResponseModel<int> responseModel = new ResponseModel<int>();
            if (payment.CollectorReceiptNumber == null || payment.CollectorReceiptNumber.Length > 500)
            {
                responseModel.Status = "Warning";
                responseModel.Message = payment.CollectorReceiptNumber == null 
                                      ? "Collector Receipt Number cannot be empty"
                                      : "Collector Receipt Number cannot exceed 50 characters";
            }
            else if (payment.Remarks != null && payment.Remarks.Length > 500)
            {
                responseModel.Status = "Warning";
                responseModel.Message = "Remarks cannot exceed 500 characters";
            }
            else if (await soaRepository.IsCollectorReceiptNumberInUse(payment.CollectorReceiptNumber))
            {
                responseModel.Status = "Error";
                responseModel.Message = "Collector Receipt Number already in use";
            }
            else if (await soaRepository.AddPaymentDetails(payment))
            {
                responseModel.Status = "Success";
                responseModel.Message = "Payment added successfully";
            }
            else
            {
                responseModel.Status = "Error";
                responseModel.Message = "Adding payment failed";
            }
            return Json(responseModel);
        }

        public async Task<IActionResult> PrintCRDetails(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SOACR, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;

            return View(await soaRepository.PrintCRDetails(id));
        }

        public async Task<IActionResult> PrintSOADetails(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SOACR, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;

            return View(await soaRepository.PrintSOADetails(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddInvoices(int soaId, int[] invoiceIds)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SOACR, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            ResponseModel responseModel = new ResponseModel();
            responseModel.Status = "Success";
            responseModel.Message = "Invoice Successfully Added";

            await soaRepository.AddInvoicesAsync(soaId, invoiceIds);
            return Json(responseModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetInvoiceTBR(string search, int page, int CustomerId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SOACR, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return Json(await soaRepository.GetInvoicesTBRAsync(search, page, CustomerId));
        }

        [HttpPost]
        public async Task<IActionResult> AddInvoiceTBR(int soaLineItemId, int salesReturnId, decimal amount)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SOACR, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            await soaRepository.AddInvoiceTBRAsync(soaLineItemId, salesReturnId, amount);
            ResponseModel responseModel = new ResponseModel();
            responseModel.Status = "Success";
            responseModel.Message = "TBR Successfully Added";

            return Json(responseModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetInvoiceTBRHistory(int soaLineItemId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SOACR, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await soaRepository.GetInvoiceTBRHistoryDTAsync(form, soaLineItemId));
        }
        #endregion








        //public async Task<IActionResult> AddInvoice(int id, int custId)
        //{
        //    var soaInv = await soaRepository.GetContractInvoicesCustomer2(id, custId);
        //    if (soaInv == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(soaInv);


        //}





        //[HttpPost]
        //public async Task<JsonResult> CreatePaymentJson(PaymentVM paymentVM)
        //{
        //    ResponseModel responseModel = new ResponseModel();
        //    await soaRepository.AddCustomerPaymentAsync(paymentVM);
        //    responseModel.Status = "Success";
        //    responseModel.Message = "Payment successfully added";
        //    return Json(responseModel);
        //}

        //[HttpPost]
        //public async Task<JsonResult> GetPaymentDetailsJson(SoacrPaymentFilter filter)
        //{
        //    return Json(await soaRepository.GetPaymentDetails(filter));
        //}

        //[HttpPost]
        //public async Task<JsonResult> GetPaymentChecksJson(int customerPaymentId)
        //{
        //    DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
        //    return Json(await soaRepository.GetPaymentChecksDTAsync(form, customerPaymentId));
        //}

        //[HttpPost]
        //public async Task<JsonResult> AddCheckJson(SoacrPaymentCheck check)
        //{
        //    ResponseModel<int> responseModel = new ResponseModel<int>();
        //    if (check.CheckAmountD == 0)
        //    {
        //        responseModel.Status = "Error";
        //        responseModel.Message = "Check Amount cannot be 0";
        //    }
        //    else if (await soaRepository.IsCheckNumberInUse(check.CheckNumber))
        //    {
        //        responseModel.Status = "Error";
        //        responseModel.Message = "Check Number already in use";
        //    }
        //    else if (await soaRepository.AddPaymentCheck(check))
        //    {
        //        responseModel.Status = "Success";
        //        responseModel.Message = "Add Check successfully";
        //    }
        //    else
        //    {
        //        responseModel.Status = "Error";
        //        responseModel.Message = "Add Check failed";
        //    }
        //    return Json(responseModel);
        //}

        //[HttpPost]
        //public async Task<JsonResult> RemoveCheckJson(int? soaCheckId)
        //{
        //    ResponseModel<int> responseModel = new ResponseModel<int>();
        //    if (soaCheckId == null)
        //    {
        //        responseModel.Status = "Error";
        //        responseModel.Message = "Please try again later";
        //    }
        //    else if (await soaRepository.RemovePaymentCheck(soaCheckId.Value))
        //    {
        //        responseModel.Status = "Success";
        //        responseModel.Message = "Remove Check successfully";
        //    }
        //    else
        //    {
        //        responseModel.Status = "Error";
        //        responseModel.Message = "Remove Check failed";
        //    }
        //    return Json(responseModel);
        //}

        //[HttpPost]
        //public async Task<JsonResult> GetList2Json(SoaFilterVM filter)
        //{
        //    DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
        //    return Json(await soaRepository.GetAllForDataTables2Async(form, filter));
        //}

        //public ActionResult ContractReceiptDetails(int id)
        //{
        //    var soa = soaRepository.GetInvoiceVM(id);
        //    if (soa == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(soa);
        //}

        //[HttpPost]
        //public async Task<JsonResult> GetSOAInvoice(int id)
        //{
        //    //return Json(await soaRepository.GetSoaDetailsAsync(id));
        //    return Json(null);
        //}

        ////public ActionResult EditInvoice(int id)
        ////{
        ////    var inv = soaRepository.GetSoaInvoiceDtlVM(id);
        ////    if (inv == null)
        ////    {
        ////        return NotFound();
        ////    }
        ////    return Json(inv);
        ////}

        //public ActionResult ContractReceipt()
        //{
        //    return View();
        //}


        //[HttpPost]
        //public async Task<JsonResult> GetContractReceipt2Json(SoaFilterVM filter)
        //{
        //    DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
        //    return Json(await soaRepository.GetContractReceipt2DTAsync(form, filter));
        //}

        //[HttpPost]
        //public async Task<JsonResult> GetCustomerInvoicesJson(int id, int soaId)
        //{
        //    DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
        //    return Json(await soaRepository.GetContractReceiptCustomerDTAsync(form, id, soaId));
        //}


        //[HttpPost]
        //public async Task<JsonResult> GetContractReceiptDetailsJson(int id)
        //{
        //    DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
        //    return Json(await soaRepository.GetContractReceiptDetailsDTAsync(form, id));
        //}


        //[HttpPost]
        //public async Task<JsonResult> CreateJsonCR(SoacrVM soaVM, int[] invoiceIds)
        //{
        //    ResponseModel<int> responseModel = new ResponseModel<int>();
        //    if (await soaRepository.IsSOAIdExist(soaVM.Id))
        //    {
        //        responseModel.Status = "Error";
        //        responseModel.Message = "Counter Receipt Id already used.";
        //    }
        //    else
        //    {
        //        var soaId = await soaRepository.AddCrAsync(soaVM, invoiceIds);
        //        await soaRepository.AddCrAsync(soaVM, invoiceIds);
        //        responseModel.Status = "Success";
        //        responseModel.Message = "Counter Receipt Successfully added";
        //        responseModel.NewRow = soaId;
        //    }
        //    return Json(responseModel);
        //}
    }
}