using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    [Authorize(Roles = Roles.Accounting + ","
                     + Roles.InventoryManager + ","
                     + Roles.InvoicingClerk + ","
                     + Roles.SalesDirector + ","
                     + Roles.SystemAdmin + ","
                     + Roles.Telemarketer)]
    public class SalesReturnController : Controller
    {
        private readonly ISalesReturnRepository salesReturnRepository;

        public SalesReturnController(ISalesReturnRepository salesReturnRepository)
        {
            this.salesReturnRepository = salesReturnRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetListJson()
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await salesReturnRepository.GetListDTAsync(form));
        }

        public async Task<ActionResult> Details(int? Id)
        {
            var rturn = await salesReturnRepository.GetDetailsAsync(Id);
            if (rturn == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(rturn);
        }

        [HttpPost]
        public async Task<JsonResult> GetItemsJson(int salesReturnId)
        {
            return Json(await salesReturnRepository.GetItemsAsync(salesReturnId));
        }

        public ActionResult Create()
        {
            return View();
        }
    }
}