using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class WarehouseController : Controller
    {
        private readonly IWarehouseRepository warehouseRepository;
        private readonly IItemRepository itemRepository;

        public WarehouseController(IWarehouseRepository warehouseRepository,
            IItemRepository itemRepository)
        {
            this.warehouseRepository = warehouseRepository;
            this.itemRepository = itemRepository;
        }

        public async Task<IActionResult> GetOptions(int? IncludeId)
        {
            return Json(await warehouseRepository.GetOptions(IncludeId));
        }

        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.SalesDirector + ","
                         + Roles.SystemAdmin + ","
                         + Roles.Telemarketer)]
        public async Task<ActionResult> Index()
        {
            var warehouses = await warehouseRepository.GetAllAsync();
            return View(warehouses);
        }

        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.SalesDirector + ","
                         + Roles.SystemAdmin + ","
                         + Roles.Telemarketer)]
        public async Task<ActionResult> Details(int id)
        {
            var warehouse = await warehouseRepository.GetAsync(id);
            if (warehouse == null)
            {
                return NotFound();
            }
            var itemFilter = new ItemFilterVM() { WarehouseId = warehouse.Id };
            return View(warehouse);
        }

        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.SalesDirector + ","
                         + Roles.SystemAdmin + ","
                         + Roles.Telemarketer)]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.SalesDirector + ","
                         + Roles.SystemAdmin + ","
                         + Roles.Telemarketer)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(WarehouseFormVM warehouseVM)
        {
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

        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.SalesDirector + ","
                         + Roles.SystemAdmin + ","
                         + Roles.Telemarketer)]
        public async Task<ActionResult> Edit(int id)
        {
            var warehouse = await warehouseRepository.GetToEditAsync(id);
            if (warehouse == null)
            {
                return NotFound();
            }

            return View(warehouse);
        }

        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.SalesDirector + ","
                         + Roles.SystemAdmin + ","
                         + Roles.Telemarketer)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(WarehouseFormVM warehouseVM)
        {
            if (ModelState.IsValid)
            {
                await warehouseRepository.UpdateAsync(warehouseVM);
                TempData["SuccessMessage"] = "Warehouse successfully updated";
                return RedirectToAction(nameof(Index));
            }
            return View(warehouseVM);
        }

        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.SalesDirector + ","
                         + Roles.SystemAdmin + ","
                         + Roles.Telemarketer)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
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

        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.SalesDirector + ","
                         + Roles.SystemAdmin + ","
                         + Roles.Telemarketer)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reactivate(int id)
        {
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