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
    public class WarehouseController(IAccessRepository accessRepository
                                   , IWarehouseRepository warehouseRepository) : BaseController(accessRepository)
    {
        public async Task<IActionResult> GetOptions(int? IncludeId)
        {
            return Json(await warehouseRepository.GetOptions(IncludeId));
        }

        public async Task<IActionResult> GetOptions2(string search, int page)
        {
            return Json(await warehouseRepository.GetOptions2(search, page));
        }

        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Warehouse, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.Warehouse)).Convert();

            return View();
        }

        public async Task<IActionResult> Details(int Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Warehouse, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.Warehouse)).Convert();

            var warehouse = await warehouseRepository.GetWarehouse(Id);

            if (warehouse == null) return NotFound();

            return View(warehouse);
        }

        [HttpPost]
        public async Task<JsonResult> GetWarehousesDT()
        {
            var form = new DataTablesRequestForm(Request.Form);
            return Json(await warehouseRepository.GetWarehousesDT(form));
        }

        public async Task<IActionResult> AddWarehouse(WarehouseVM warehouse)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Warehouse, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            var responseModel = await warehouseRepository.AddAsync(warehouse);
            return Json(responseModel);
        }

        public async Task<IActionResult> UpdateWarehouse(WarehouseVM warehouse)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Warehouse, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            var responseModel = await warehouseRepository.UpdateAsync(warehouse);
            return Json(responseModel);
        }

        public async Task<IActionResult> DeleteDeactivateWarehouse(int Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Warehouse, AccessTypes.Delete);
            if (redirectResult != null) return redirectResult;
            var responseModel = await warehouseRepository.DeleteDeactivateAsync(Id);
            return Json(responseModel);
        }

        public async Task<IActionResult> ReactivateWarehouse(int Id)
        {
            var redirectResultAdd = await CheckAccessAndRedirectAsync(PageNames.Warehouse, AccessTypes.Add);
            var redirectResultEdit = await CheckAccessAndRedirectAsync(PageNames.Warehouse, AccessTypes.Edit);
            if (redirectResultAdd != null && redirectResultEdit != null) return redirectResultAdd;

            var responseModel = await warehouseRepository.ReactivateAsync(Id);

            return Json(responseModel);
        }

        public async Task<IActionResult> SetMainWarehouse(int Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Warehouse, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            var responseModel = await warehouseRepository.SetMainAsync(Id);
            return Json(responseModel);
        }
    }
}