using GMGInventory.Application.Contracts;
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

        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Warehouse, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.Warehouse)).Convert();

            var warehouses = await warehouseRepository.GetAllAsync();
            return View(warehouses);
        }

        public async Task<IActionResult> Details(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Warehouse, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.Warehouse)).Convert();

            var warehouse = await warehouseRepository.GetAsync(id);
            if (warehouse == null)
            {
                return NotFound();
            }
            var itemFilter = new ItemFilterVM() { WarehouseId = warehouse.Id };
            return View(warehouse);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WarehouseFormVM warehouseVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Warehouse, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            if (ModelState.IsValid)
            {
                if (await warehouseRepository.IsCodeExists(warehouseVM.Code))
                {
                    TempData["ErrorMessage"] = "Code already in use";
                }
                else
                {
                    await warehouseRepository.AddAsync(warehouseVM);
                    TempData["SuccessMessage"] = "Warehouse successfully created";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(warehouseVM);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Warehouse, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            var warehouse = await warehouseRepository.GetToEditAsync(id);
            if (warehouse == null)
            {
                return NotFound();
            }

            return View(warehouse);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(WarehouseFormVM warehouseVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Warehouse, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            if (ModelState.IsValid)
            {
                await warehouseRepository.UpdateAsync(warehouseVM);
                TempData["SuccessMessage"] = "Warehouse successfully updated";
                return RedirectToAction(nameof(Index));
            }
            return View(warehouseVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Warehouse, AccessTypes.Delete);
            if (redirectResult != null) return redirectResult;

            if (await warehouseRepository.IsIdExists(id) != true)
            {
                return NotFound();
            }
            if (await warehouseRepository.IsInUse(id))
            {
                await warehouseRepository.DeactivateAsync(id);
                TempData["WarningMessage"] = "Warehouse has been deactivated";
            }
            else
            {
                await warehouseRepository.DeleteAsync(id);
                TempData["SuccessMessage"] = "Warehouse successfully deleted";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reactivate(int id)
        {
            var redirectResultAdd = await CheckAccessAndRedirectAsync(PageNames.Warehouse, AccessTypes.Add);
            var redirectResultEdit = await CheckAccessAndRedirectAsync(PageNames.Warehouse, AccessTypes.Edit);
            if (redirectResultAdd != null && redirectResultEdit != null) return redirectResultAdd;

            if (await warehouseRepository.IsIdExists(id) != true)
            {
                return NotFound();
            }
            await warehouseRepository.ReactivateAsync(id);
            TempData["SuccessMessage"] = "Warehouse successfully reactivated";
            return RedirectToAction(nameof(Index));
        }
    }
}