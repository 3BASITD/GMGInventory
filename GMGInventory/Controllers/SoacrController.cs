using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GMGInventory.Controllers
{
    //[Authorize(Roles = Roles.InventoryManager + ","
    //                 + Roles.SalesDirector + ","
    //                 + Roles.SystemAdmin + ","
    //                 + Roles.Telemarketer)]

    [Authorize]
    public class SoacrController : Controller
    {
        private readonly ISoacrRepository soaRepository;
        private readonly IInvoiceRepository invoiceRepository;

        public SoacrController(ISoacrRepository soaRepository, IInvoiceRepository invoiceRepository)
        {
            this.soaRepository = soaRepository;
            this.invoiceRepository = invoiceRepository;
        }
        public async Task<ActionResult> Index()
        { 
            return View();
        }


        [HttpPost]
        public async Task<JsonResult> CreateJson(SoacrVM soaVM)
        {
            ResponseModel responseModel = new ResponseModel();
            if (await soaRepository.IsSOAIdExist(soaVM.Id))
            {
                responseModel.Status = "Error";
                responseModel.Message = "SoA Id already used.";
            }
            else
            {
                await soaRepository.AddAsync(soaVM);
                responseModel.Status = "Success";
                responseModel.Message = "SOA successfully added";
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



        public ActionResult EditInvoice(int id)
        {
            var inv = soaRepository.GetSoaInvoiceDtlVM(id); 
            {
                return NotFound();
            }
            return Json(inv);
        }
        public async Task<ActionResult> ContractReceipt()
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
        public async Task<JsonResult> GetContractReceiptDetailsJson(int id)
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await soaRepository.GetContractReceiptDetailsDTAsync(form, id));
        }

    }
}