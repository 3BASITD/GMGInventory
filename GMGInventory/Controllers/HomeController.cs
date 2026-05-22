using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class HomeController(IAccessRepository accessRepository
                              , IHomeImageRepository homeImageRepository
                              , ISystemSettingRepository settingRepository) : BaseController(accessRepository)
    {
        public async Task<IActionResult> Index()
        {
            var images = await homeImageRepository.GetListAsync();
            var activeImages = images.Where(s => s.DisplayOrder > 0)
                                     .OrderBy(s => s.DisplayOrder)
                                     .AsEnumerable();
            return View(activeImages);
        }

        public IActionResult Progress()
        {
            return View();
        }

        public IActionResult PrintSample()
        {
            return View();
        }

        public IActionResult PrintTest()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error(int statuscode)
        {
            var errorList = await settingRepository.GetSystemSettings();
            Response.StatusCode = statuscode;
            return statuscode switch
            {
                400 => View("~/Views/Shared/E400.cshtml", errorList),
                403 => View("~/Views/Shared/E403.cshtml", errorList),
                404 => View("~/Views/Shared/E404.cshtml", errorList),
                500 => View("~/Views/Shared/E500.cshtml", errorList),
                502 => View("~/Views/Shared/E502.cshtml", errorList),
                503 => View("~/Views/Shared/E503.cshtml", errorList),
                _ => View("~/Views/Shared/Error.cshtml", errorList)
            };
        }
    }

    public class HomeTaskRow
    {
        public string MD { get; set; }
        public string ST { get; set; }
    }
}