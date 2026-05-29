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
    public class DeliveryMonitoringController(IAccessRepository accessRepository
                                            , IDeliveryMonitoringRepository deliveryMonitoringRepository) : BaseController(accessRepository)
    {
        public async Task<IActionResult> GetStatusOptions(int? IncludeId)
        {
            return Json(await deliveryMonitoringRepository.GetStatusOptions(IncludeId));
        }

        public async Task<IActionResult> GetStatusOptions2(string search, int page)
        {
            return Json(await deliveryMonitoringRepository.GetStatusOptions2(search, page));
        }

        public async Task<IActionResult> GetMethodOptions(int? IncludeId)
        {
            return Json(await deliveryMonitoringRepository.GetMethodOptions(IncludeId));
        }

        public async Task<IActionResult> GetMethodOptions2(string search, int page)
        {
            return Json(await deliveryMonitoringRepository.GetMethodOptions2(search, page));
        }

        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.DeliveryMonitoring, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.DeliveryMonitoring)).Convert();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetListDT(DeliveryMonitoringFilterVM filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.DeliveryMonitoring, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            var form = new DataTablesRequestForm(Request.Form);
            return Json(await deliveryMonitoringRepository.GetListDTAsync(form, filter));
        }

        [HttpPost]
        public async Task<IActionResult> SaveDeliveryAsync(DeliveryMonitoringFormVM form)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.DeliveryMonitoring, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            return Json(await deliveryMonitoringRepository.SaveDeliveryAsync(form));
        }
    }
}