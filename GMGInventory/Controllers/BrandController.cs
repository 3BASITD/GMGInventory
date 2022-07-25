using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GMGInventory.Data;
using GMGInventory.Common.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using GMGInventory.Application.Contracts;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class BrandController : Controller
    {
        private readonly IBrandRepository brandRepository;
        private readonly IItemTypeRepository itemTypeRepository;
        private readonly IMapper mapper;

        public BrandController(IBrandRepository brandRepository
            , IItemTypeRepository itemTypeRepository
            , IMapper mapper)
        {
            this.itemTypeRepository = itemTypeRepository;
            this.brandRepository = brandRepository;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await brandRepository.GetAllBrandVMAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            var brandVM = await brandRepository.GetBrandViewVMAsync(id);
            if (brandVM == null)
            {
                return NotFound();
            }
            return View(brandVM);
        }

        public async Task<IActionResult> Create()
        {
            var brandVM = new BrandVM
            {
                ItemTypes = new SelectList(await itemTypeRepository.GetAllActiveItemTypeAsync(-1), "Id", "Name")
            };
            return View(brandVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BrandVM brandVM)
        {
            if (ModelState.IsValid)
            {
                if (await brandRepository.IsBrandCodeExist(brandVM.Code))
                {
                    ModelState.AddModelError(string.Empty, "Brand code already in use.");
                    TempData["ErrorMessage"] = "Brand code already in use";
                }
                else
                {
                    await brandRepository.AddAsync(brandVM);
                    TempData["SuccessMessage"] = "Brand successfully added";
                    return RedirectToAction(nameof(Index));
                }
            }
            brandVM.ItemTypes = new SelectList(await brandRepository.GetAllActiveBrandAsync(brandVM.ItemTypeId), "Id", "Name", brandVM.ItemTypeId);
            return View(brandVM);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var brandVM = await brandRepository.GetBrandVMAsync(id);
            if (brandVM == null)
            {
                return NotFound();
            }
            brandVM.ItemTypes = new SelectList(await brandRepository.GetAllActiveBrandAsync(-1), "Id", "Name");
            return View(brandVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BrandVM brandVM)
        {
            if (id != brandVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await brandRepository.UpdateAsync(id, brandVM);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await brandRepository.Exists(brandVM.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["SuccessMessage"] = "Brand successfully updated";
                return RedirectToAction(nameof(Index));
            }

            brandVM.ItemTypes = new SelectList(await itemTypeRepository.GetAllActiveItemTypeAsync(-1), "Id", "Name");
            return View(brandVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (await brandRepository.IsBrandIdExistAsync(id) == false)
            {
                return NotFound();
            }
            if (await brandRepository.IsBrandInUse(id))
            {
                await brandRepository.DeactivateBrandAsync(id);
                TempData["WarningMessage"] = "Brand has been deactivated";
            }
            else
            {
                await brandRepository.DeleteAsync(id);
                TempData["SuccessMessage"] = "Brand successfully deleted";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reactivate(int id)
        {
            if (await brandRepository.IsBrandIdExistAsync(id) == false)
            {
                return NotFound();
            }
            await brandRepository.ReactivateBrandAsync(id);
            TempData["SuccessMessage"] = "Brand reactivated successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
