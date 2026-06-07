using GMGInventory.Application.Contracts;
using GMGInventory.Application.Repositories;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class StockController(IAccessRepository accessRepository
                               , IStockRepository stockRepository
                               , ISystemSettingRepository settingRepository
                               , IAPIBCT_Service apiBCTService) : BaseController(accessRepository)
    {
        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.BarcotechStock, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.BarcotechStock)).Convert();

            var stockAutoSynching = await settingRepository.GetValue("BarcotechAutoStock");
            ViewData["AutoSynching"] = stockAutoSynching == null ? false : stockAutoSynching == "true";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetStocksDT()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.BarcotechStock, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            var form = new DataTablesRequestForm(Request.Form);
            return Json(await stockRepository.GetStocksDT(form));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAutoSync(bool AutoEnabled)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.BarcotechStock, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            return Json(await settingRepository.UpdateSetting("BarcotechAutoStock", AutoEnabled ? "true" : "false"));
        }

        public async Task<IActionResult> SyncStock(int? ItemId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.BarcotechStock, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            var response = await apiBCTService.SyncStock(ItemId);
            return Json(response);
        }
    }
}