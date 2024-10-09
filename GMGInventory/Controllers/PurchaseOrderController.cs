using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class PurchaseOrderController(IAccessRepository accessRepository, IPurchaseOrderRepository purchaseOrderRepository)  : BaseController(accessRepository)
    {
        public async Task<IActionResult> GetOptions(string search, int page)
        {
            return Json(await purchaseOrderRepository.GetOptions(search, page));
        }

        public async Task<IActionResult> GetCurrencyOptions(int? IncludeId)
        {
            return Json(await purchaseOrderRepository.GetCurrencyOptions(IncludeId));
        }

        [HttpPost]
        public async Task<IActionResult> GetItemsJson(int purchaseOrderId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseOrder, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return Json(await purchaseOrderRepository.GetItemsAsync(purchaseOrderId));
        }

        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.SalesDirector + ","
                         + Roles.SystemAdmin + ","
                         + Roles.Telemarketer)]
        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseOrder, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.PurchaseOrder)).Convert();

            return View();
        }

        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.SalesDirector + ","
                         + Roles.SystemAdmin + ","
                         + Roles.Telemarketer)]
        [HttpPost]
        public async Task<IActionResult> GetListDTJson()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseOrder, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await purchaseOrderRepository.GetListDTAsync(form));
        }

        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.SalesDirector + ","
                         + Roles.SystemAdmin + ","
                         + Roles.Telemarketer)]
        public async Task<IActionResult> Details(int? Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseOrder, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.PurchaseOrder)).Convert();

            var order = await purchaseOrderRepository.GetDetailsAsync(Id);
            if (order == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var dtNow = DateTime.Now;
            var newChecker = dtNow.AddMinutes(-1) <= order.OrderDateD && order.OrderDateD <= dtNow;
            if (newChecker)
            {
                TempData["SuccessMessage"] = order.Status + " successfully saved";
            }

            return View(order);
        }

      
    }
}