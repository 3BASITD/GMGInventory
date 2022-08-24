using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GMGInventory.Data;
using GMGInventory.Common.Models;
using GMGInventory.Common.Constants;
using Microsoft.AspNetCore.Authorization;
using GMGInventory.Application.Contracts;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GMGInventory.Controllers
{
    [Authorize(Roles = Roles.InventoryManager + "," + Roles.SalesDirector + "," + Roles.SystemAdmin)]
    public class SupplierController : Controller
    {
        private readonly ISupplierRepository supplierRepository;
        private readonly IAreaRepository areaRepository;
        private readonly ICurrencyRepository currencyRepository;
        private readonly IRegionRepository regionRepository;

        public SupplierController(ISupplierRepository supplierRepository
            , IAreaRepository areaRepository
            , ICurrencyRepository currencyRepository
            , IRegionRepository regionRepository)
        {
            this.supplierRepository = supplierRepository;
            this.areaRepository = areaRepository;
            this.currencyRepository = currencyRepository;
            this.regionRepository = regionRepository;
        }

        public async Task<IActionResult> GetOptions(int? IncludeId)
        {
            return Json(await supplierRepository.GetOptions(IncludeId));
        }

        public async Task<IActionResult> Index()
        {
            return View(await supplierRepository.GetAllSupplierVMAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            var supplierVM = await supplierRepository.GetSupplierViewVMAsync(id);
            if (supplierVM == null)
            {
                return NotFound();
            }
            return View(supplierVM);
        }

        public async Task<IActionResult> Create()
        {
            var supplierVM = new SupplierVM
            {
                Areas = new SelectList(await areaRepository.GetAllActiveAreaAsync(-1), "Id", "Name"),
                Currencies = new SelectList(await currencyRepository.GetAllActiveCurrencyAsync(-1), "Id", "Name"),
                Regions = new SelectList(await regionRepository.GetAllActiveRegionAsync(-1), "Id", "Name")
            };
            return View(supplierVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SupplierVM supplierVM)
        {
            if (ModelState.IsValid)
            {
                if (await supplierRepository.IsSupplierCodeExist(supplierVM.Code))
                {
                    ModelState.AddModelError(string.Empty, "Supplier code already in use.");
                    TempData["ErrorMessage"] = "Supplier code already in use";
                }
                else
                {
                    await supplierRepository.AddAsync(supplierVM);
                    TempData["SuccessMessage"] = "Supplier successfully added";
                    return RedirectToAction(nameof(Index));
                }
            }
            supplierVM.Areas = new SelectList(await areaRepository.GetAllActiveAreaAsync(supplierVM.AreaId), "Id", "Name", supplierVM.AreaId);
            supplierVM.Currencies = new SelectList(await currencyRepository.GetAllActiveCurrencyAsync(supplierVM.CurrencyId), "Id", "Name", supplierVM.CurrencyId);
            supplierVM.Regions = new SelectList(await regionRepository.GetAllActiveRegionAsync(supplierVM.RegionId), "Id", "Name", supplierVM.RegionId);
            return View(supplierVM);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var supplierVM = await supplierRepository.GetSupplierVMAsync(id);
            if (supplierVM == null)
            {
                return NotFound();
            }
            supplierVM.Areas = new SelectList(await areaRepository.GetAllActiveAreaAsync(-1), "Id", "Name");
           
            supplierVM.Currencies = new SelectList(await currencyRepository.GetAllActiveCurrencyAsync(-1), "Id", "Name");
            supplierVM.Regions = new SelectList(await regionRepository.GetAllActiveRegionAsync(-1), "Id", "Name");

            return View(supplierVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SupplierVM supplierVM)
        {
            if (id != supplierVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await supplierRepository.UpdateAsync(id, supplierVM);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await supplierRepository.Exists(supplierVM.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["SuccessMessage"] = "Supplier successfully updated";
                return RedirectToAction(nameof(Index));
            }

            supplierVM.Areas = new SelectList(await areaRepository.GetAllActiveAreaAsync(-1), "Id", "Name");
            supplierVM.Currencies = new SelectList(await currencyRepository.GetAllActiveCurrencyAsync(-1), "Id", "Name");
            supplierVM.Regions = new SelectList(await regionRepository.GetAllActiveRegionAsync(-1), "Id", "Name");
            return View(supplierVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (await supplierRepository.IsSupplierIdExistAsync(id) == false)
            {
                return NotFound();
            }
            if (await supplierRepository.IsSupplierInUse(id))
            {
                await supplierRepository.DeactivateSupplierAsync(id);
                TempData["WarningMessage"] = "Supplier has been deactivated";
            }
            else
            {
                await supplierRepository.DeleteAsync(id);
                TempData["SuccessMessage"] = "Supplier successfully deleted";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reactivate(int id)
        {
            if (await supplierRepository.IsSupplierIdExistAsync(id) == false)
            {
                return NotFound();
            }
            await supplierRepository.ReactivateSupplierAsync(id);
            TempData["SuccessMessage"] = "Supplier reactivated successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
