using GMGInventory.Application.Contracts;
using GMGInventory.Application.Repositories;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
   [Authorize]
    public class WarehouseDashboardController(IAccessRepository accessRepository
                                            , IWarehouseDashboardRepository warehouseRepository) : BaseController(accessRepository)
    {

        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.WarehouseDashboard, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetIncomingOrders()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.WarehouseDashboard, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return Json(await warehouseRepository.GetIncomingOrders());
        }

        [HttpPost]
        public async Task<IActionResult> GetOrderFulfillments(DateTime Date, bool IncludeDone)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.WarehouseDashboard, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return Json(await warehouseRepository.GetOrderFulfillments(Date, IncludeDone));
        }
    }
}