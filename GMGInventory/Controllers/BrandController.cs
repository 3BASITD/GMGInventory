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
            var brandVM = await brandRepository.GetBrandVMAsync(id);
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
                }
                else
                {
                    var brand = mapper.Map<Brand>(brandVM);
                    brand.IsActive = true;
                    await brandRepository.AddAsync(brand);
                    return RedirectToAction(nameof(Index));
                }
            }
            brandVM.ItemTypes = new SelectList(await itemTypeRepository.GetAllActiveItemTypeAsync(brandVM.ItemTypeId), "Id", "Name", brandVM.ItemTypeId);
            return View(brandVM);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var brand = await brandRepository.GetAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            var brandVM = mapper.Map<BrandVM>(brand);
            brandVM.ItemTypes = new SelectList(await itemTypeRepository.GetAllActiveItemTypeAsync(-1), "Id", "Name");
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
                    var brand = await brandRepository.GetAsync(id);
                    mapper.Map(brandVM, brand);
                    await brandRepository.UpdateAsync(brand);
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
                return RedirectToAction(nameof(Index));
            }

            brandVM.ItemTypes = new SelectList(await itemTypeRepository.GetAllActiveItemTypeAsync(-1), "Id", "Name");
            return View(brandVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var brand = await brandRepository.GetAsync(id);
            if (brand== null)
            {
                return NotFound();
            }
            if (await brandRepository.IsBrandInUse(id))
            {
                brand.IsActive = false;
                await brandRepository.UpdateAsync(brand);
            }
            else
            {
                await brandRepository.DeleteAsync(id);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reactivate(int id)
        {
            var brand = await brandRepository.GetAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            brand.IsActive = true;
            await brandRepository.UpdateAsync(brand);
            return RedirectToAction(nameof(Index));
        }
    }
}
