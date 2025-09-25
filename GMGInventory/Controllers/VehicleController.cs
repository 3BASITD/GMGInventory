using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{

    [Authorize]
    public class VehicleController(IAccessRepository accessRepository
                                 , IVehicleRepository vehicleRepository) : BaseController(accessRepository)
    {
        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Vehicle, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.Vehicle)).Convert();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetVehiclesDT()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Vehicle, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            var form = new DataTablesRequestForm(Request.Form);
            return Json(await vehicleRepository.GetVehiclesDT(form));
        }

        [HttpPost]
        public async Task<IActionResult> AddVehicle(VehicleVM vehicle)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Vehicle, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            return Json(await vehicleRepository.AddVehicle(vehicle));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateVehicle(VehicleVM vehicle)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Vehicle, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            return Json(await vehicleRepository.UpdateVehicle(vehicle));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteOrDeactivateVehicle(int? Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Vehicle, AccessTypes.Delete);
            if (redirectResult != null) return redirectResult;

            return Json(await vehicleRepository.DeleteOrDeactivateVehicle(Id));
        }

        [HttpPost]
        public async Task<IActionResult> ReactivateVehicle(int? Id)
        {
            var redirectResultAdd = await CheckAccessAndRedirectAsync(PageNames.Vehicle, AccessTypes.Add);
            var redirectResultEdit = await CheckAccessAndRedirectAsync(PageNames.Vehicle, AccessTypes.Edit);
            if (redirectResultAdd != null && redirectResultEdit != null) return redirectResultAdd;

            return Json(await vehicleRepository.ReactivateVehicle(Id));
        }
    }
}