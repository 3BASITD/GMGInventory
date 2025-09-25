using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{

    [Authorize]
    public class DriverController(IAccessRepository accessRepository
                                , IDriverRepository driverRepository) : BaseController(accessRepository)
    {
        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Driver, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.Driver)).Convert();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetDriversDT()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Driver, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            var form = new DataTablesRequestForm(Request.Form);
            return Json(await driverRepository.GetDriversDT(form));
        }

        [HttpPost]
        public async Task<IActionResult> AddDriver(DriverVM driver)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Driver, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            return Json(await driverRepository.AddDriver(driver));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDriver(DriverVM driver)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Driver, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            return Json(await driverRepository.UpdateDriver(driver));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteOrDeactivateDriver(int? Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Driver, AccessTypes.Delete);
            if (redirectResult != null) return redirectResult;

            return Json(await driverRepository.DeleteOrDeactivateDriver(Id));
        }

        [HttpPost]
        public async Task<IActionResult> ReactivateDriver(int? Id)
        {
            var redirectResultAdd = await CheckAccessAndRedirectAsync(PageNames.Driver, AccessTypes.Add);
            var redirectResultEdit = await CheckAccessAndRedirectAsync(PageNames.Driver, AccessTypes.Edit);
            if (redirectResultAdd != null && redirectResultEdit != null) return redirectResultAdd;

            return Json(await driverRepository.ReactivateDriver(Id));
        }
    }
}