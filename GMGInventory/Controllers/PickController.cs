using DocumentFormat.OpenXml.Bibliography;
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
    public class PickController(IAccessRepository accessRepository
                              , IPickRepository pickRepository
                              , ISystemSettingRepository settingRepository
                              , IAPIBCT_Service apiBCTService
                              , IOptions<APIBCTSettings> apiBCTOptions) : BaseController(accessRepository)
    {
        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.BarcotechPicking, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.BarcotechPicking)).Convert();

            var pickAutoSynching = await settingRepository.GetValue("PickAutoSynching");
            ViewData["AutoSynching"] = pickAutoSynching == null ? false : pickAutoSynching == "true";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetPickingsDT()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.BarcotechPicking, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            var form = new DataTablesRequestForm(Request.Form);
            return Json(await pickRepository.GetListDTAsync(form));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAutoSync(bool AutoEnabled)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.BarcotechPicking, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            return Json(await settingRepository.UpdateSetting("PickAutoSynching", AutoEnabled ? "true" : "false"));
        }

        public async Task<IActionResult> SyncPick()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.BarcotechPicking, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            var response = await apiBCTService.SyncPick(apiBCTOptions.Value.URLPicking);
            return Json(response);
        }
    }
}