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