using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    [Authorize(Roles = Roles.Accounting + "," + Roles.SystemAdmin)]
    public class SoacrController : Controller
    {
        private readonly ISoacrRepository soaRepository;
        private readonly IInvoiceRepository invoiceRepository;
        private readonly ISoacrLineItemRepository soacrLineItemRepository;

        public SoacrController(ISoacrRepository soaRepository, IInvoiceRepository invoiceRepository, ISoacrLineItemRepository soacrLineItemRepository)
        {
            this.soaRepository = soaRepository;
            this.invoiceRepository = invoiceRepository;
            this.soacrLineItemRepository = soacrLineItemRepository;
        }

        public ActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> AddInvoice(int id, int custId)
        {
            var soaInv = await soaRepository.GetContractInvoicesCustomer2(id, custId);
            if (soaInv == null)
            {
                return NotFound();
            }
            return View(soaInv);


        }

        public async Task<IActionResult> AddCounterReceipt(int? id)
        {
            ResponseModel responseModel = new ResponseModel();
            var soaInv = await soaRepository.GetCounterReceiptInvoices(id);
            if (soaInv == null)
            {
                responseModel.Status = "Error";
                responseModel.Message = "Choose Invoices.";

            }
            return View(soaInv);


        }



        [HttpPost]
        public async Task<JsonResult> CreateJson(SoacrVM soaVM)
        {
            ResponseModel<int> responseModel = new ResponseModel<int>();
            if (await soaRepository.IsSOAIdExist(soaVM.Id))
            {
                responseModel.Status = "Error";
                responseModel.Message = "SoA Id already used.";
            }
            else
            {
                var soaId = await soaRepository.AddAsync(soaVM);
                await soaRepository.AddAsync(soaVM);
                responseModel.Status = "Success";
                responseModel.Message = "SOA successfully added";
                responseModel.NewRow = soaId;
            }
            return Json(responseModel);
        }

        [HttpPost]
        public async Task<JsonResult> CreatePaymentJson(PaymentVM paymentVM)
        {
            ResponseModel responseModel = new ResponseModel();
            if (await soaRepository.IsCustomerPaymentIdExist(paymentVM.Id))
            {
                responseModel.Status = "Error";
                responseModel.Message = "Payment Id already used.";
            }
            else
            {
                await soaRepository.AddCustomerPaymentAsync(paymentVM);
                responseModel.Status = "Success";
                responseModel.Message = "Payment successfully added";
            }
            return Json(responseModel);
        }

        [HttpPost]
        public async Task<JsonResult> EditInvoiceJson(SoacrLineItemVM soaVM)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                await soaRepository.UpdateAsync(soaVM);
                responseModel.Status = "Success";
                responseModel.Message = "Invoice successfully updated";
            }
            catch (Exception ex)
            {
                if (!await soaRepository.IsSOALineItemIdExist(soaVM.Id))
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
        public async Task<JsonResult> GetList2Json(SoaFilterVM filter)
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await soaRepository.GetAllForDataTables2Async(form, filter));
        }

        [HttpPost]
        public async Task<JsonResult> GetListJson(SoaFilterVM filter)
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await soaRepository.GetAllForDataTablesAsync(form, filter));
        }

        public ActionResult Details(int id)
        {
            var soa = soaRepository.GetSoacrVM(id);
            if (soa == null)
            {
                return NotFound();
            }
            return View(soa);
        }

        public ActionResult ContractReceiptDetails(int id)
        {
            var soa = soaRepository.GetInvoiceVM(id);
            if (soa == null)
            {
                return NotFound();
            }
            return View(soa);
        }

        [HttpPost]
        public async Task<JsonResult> GetSoaDetailsJson(int id)
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await soaRepository.GetSoaDetailsDTAsync(form, id));
        }

        [HttpPost]
        public async Task<JsonResult> GetSOAInvoice(int id)
        {
            //return Json(await soaRepository.GetSOAInvoice(/*id*/));
            return Json(await soaRepository.GetSoaDetailsAsync(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, int soacrId)
        {
            if (await soacrLineItemRepository.IsSoaLineItemIdExistAsync(id) == false)
            {
                return NotFound();
            }
            else
            {
                await soacrLineItemRepository.DeleteAsync(id);
                TempData["SuccessMessage"] = "Invoice successfully deleted";
            }
            return RedirectToAction(nameof(Details), new { id = soacrId });
        }

        public async Task<IActionResult> PrintSoaDetails(int id)
        {
            return View(await soaRepository.PrintSoaDetails(id));
        }

        public async Task<IActionResult> PrintCRDetails(int id)
        {
            return View(await soaRepository.PrintCRDetails(id));
        }

        public async Task<IActionResult> PrintInvoice(SoaFilterVM filter)
        {
            return View(await soaRepository.PrintInvoice(filter));
        }

        public ActionResult EditInvoice(int id)
        {
            var inv = soaRepository.GetSoaInvoiceDtlVM(id);
            if(inv == null) {
                return NotFound();
            }
            return Json(inv);
        }

        public ActionResult ContractReceipt()
        {
            return View();
        }

        public ActionResult Invoice()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetContractReceiptJson(SoaFilterVM filter)
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await soaRepository.GetContractReceiptDTAsync(form, filter));
        }

        [HttpPost]
        public async Task<JsonResult> GetContractReceipt2Json(SoaFilterVM filter)
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await soaRepository.GetContractReceipt2DTAsync(form, filter));
        }

        [HttpPost]
        public async Task<JsonResult> GetCustomerInvoicesJson(int id, int soaId)
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await soaRepository.GetContractReceiptCustomerDTAsync(form, id, soaId));
        }


        [HttpPost]
        public async Task<JsonResult> GetContractReceiptDetailsJson(int id)
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await soaRepository.GetContractReceiptDetailsDTAsync(form, id));
        }

        [HttpPost]
        public async Task<JsonResult> AddInvoices(int soaId, int[] invoiceIds)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.Status = "Success";
            responseModel.Message = "Invoice Successfully Added";

            await soaRepository.AddInvoicesAsync(soaId, invoiceIds);
            return Json(responseModel);

        }


        [HttpPost]
        public async Task<JsonResult> CreateJsonCR(SoacrVM soaVM, int[] invoiceIds)
        {
            ResponseModel<int> responseModel = new ResponseModel<int>();
            if (await soaRepository.IsSOAIdExist(soaVM.Id))
            {
                responseModel.Status = "Error";
                responseModel.Message = "Counter Receipt Id already used.";
            }
            else
            {
                var soaId = await soaRepository.AddCrAsync(soaVM, invoiceIds);
                await soaRepository.AddCrAsync(soaVM, invoiceIds);
                responseModel.Status = "Success";
                responseModel.Message = "Counter Receipt Successfully added";
                responseModel.NewRow = soaId;
            }
            return Json(responseModel);
        }

       



    }
}