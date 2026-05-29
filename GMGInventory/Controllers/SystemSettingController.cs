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
    public class SystemSettingController(IAccessRepository accessRepository
                                       , ISystemSettingRepository settingRepository) : BaseController(accessRepository)
    {

        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SystemSettings, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.SystemSettings)).Convert();

            return View(await settingRepository.GetSystemSettings());
        }

        public async Task<IActionResult> GetSystemSettings()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SystemSettings, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return Json(await settingRepository.GetSystemSettings());
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSettings(SystemSettingsVM ssettings)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SystemSettings, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            return Json(await settingRepository.UpdateSettings(ssettings));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSetting(string Name, string Value)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SystemSettings, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            return Json(await settingRepository.UpdateSetting(Name, Value));
        }

        [HttpPost]
        public async Task<IActionResult> SaveImageAsync(PageImageFormVM form)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SystemSettings, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            var response = await settingRepository.SaveImageAsync(form);
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveImageAsync(string Name)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SystemSettings, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            var response = await settingRepository.RemoveImageAsync(Name);
            return Json(response);
        }
    }
}