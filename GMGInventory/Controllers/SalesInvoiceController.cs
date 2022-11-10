using GMGInventory.Application.Contracts;
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
    }
}