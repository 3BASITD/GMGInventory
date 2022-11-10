using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    [Authorize(Roles = Roles.InvoicingClerk + ","
                     + Roles.SystemAdmin + ","
                     + Roles.Telemarketer)]
    public class PackController : Controller
    {
        private readonly IPackRepository packRepository;

        public PackController(IPackRepository packRepository)
        {
            this.packRepository = packRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetListJson(ListFilterCommonModel filter)
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await packRepository.GetListDTAsync(form, filter));
        }

        public async Task<ActionResult> Details(int? Id)
        {
            var order = await packRepository.GetDetailsAsync(Id);
            if (order == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        [HttpPost]
        public async Task<JsonResult> GetItemsJson(int packId)
        {
            return Json(await packRepository.GetItemsAsync(packId));
        }

        [HttpPost]
        public async Task<JsonResult> GetItemsDTJson(int packId)
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await packRepository.GetItemsDTAsync(form, packId));
        }
    }
}