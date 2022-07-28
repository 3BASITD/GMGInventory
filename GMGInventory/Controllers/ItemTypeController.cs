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
    public class ItemTypeController : Controller
    {
        private readonly IItemTypeRepository itemTypeRepository;
        private readonly ICategoryRepository categoryRepository;

        public ItemTypeController(IItemTypeRepository itemTypeRepository
            , ICategoryRepository categoryRepository)
        {
            this.itemTypeRepository = itemTypeRepository;
            this.categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await itemTypeRepository.GetAllItemTypeVMAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            var itemTypeVM = await itemTypeRepository.GetItemTypeVMAsync(id);
            if (itemTypeVM == null)
            {
                return NotFound();
            }
            return View(itemTypeVM);
        }

        public async Task<IActionResult> Create()
        {
            var cat = await categoryRepository.GetAllActiveCategoryAsync(-1);
            var itemTypeVM = new ItemTypeVM
            {
                //Categories = new SelectList(await categoryRepository.GetAllActiveCategoryAsync(-1), "Id", "Name")
                Categories = cat.Select(s => new SelectListItem{ 
                    Value = s.Id.ToString(), 
                    Text = s.Code 
                }).ToList()
            };
            return View(itemTypeVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemTypeVM itemTypeVM)
        {
            if (ModelState.IsValid)
            {
                if (await itemTypeRepository.IsItemTypeCodeExist(itemTypeVM.Code))
                {
                    ModelState.AddModelError(string.Empty, "Item type code already in use.");
                    TempData["ErrorMessage"] = "Item type code already in use";
                }
                else
                {
                    await itemTypeRepository.AddAsync(itemTypeVM);
                    TempData["SuccessMessage"] = "Item type successfully added";
                    return RedirectToAction(nameof(Index));
                }
            }
            //itemTypeVM.Categories = new SelectList(await categoryRepository.GetAllActiveCategoryAsync(itemTypeVM.CategoryId), "Id", "Name", itemTypeVM.CategoryId);

            var cat = await categoryRepository.GetAllActiveCategoryAsync(itemTypeVM.CategoryId);
            itemTypeVM.Categories = cat.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Code,
                Selected = s.Id == itemTypeVM.CategoryId
            }).ToList();
            return View(itemTypeVM);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var itemTypeVM = await itemTypeRepository.GetItemTypeVMAsync(id);
            if (itemTypeVM == null)
            {
                return NotFound();
            }

            //itemTypeVM.Categories = new SelectList(await categoryRepository.GetAllActiveCategoryAsync(-1), "Id", "Name");
            var cat = await categoryRepository.GetAllActiveCategoryAsync(itemTypeVM.CategoryId);
            itemTypeVM.Categories = cat.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Code
            }).ToList();
            return View(itemTypeVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ItemTypeVM itemTypeVM)
        {
            if (id != itemTypeVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await itemTypeRepository.UpdateAsync(id, itemTypeVM);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await itemTypeRepository.Exists(itemTypeVM.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["SuccessMessage"] = "Item type successfully updated";
                return RedirectToAction(nameof(Index));
            }

            //itemTypeVM.Categories = new SelectList(await categoryRepository.GetAllActiveCategoryAsync(-1), "Id", "Name");

            var cat = await categoryRepository.GetAllActiveCategoryAsync(itemTypeVM.CategoryId);
            itemTypeVM.Categories = cat.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Code,
                Selected = s.Id == itemTypeVM.CategoryId
            }).ToList();
            return View(itemTypeVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (await itemTypeRepository.IsItemTypeIdExistAsync(id) == false)
            {
                return NotFound();
            }
            if (await itemTypeRepository.IsItemTypeInUse(id))
            {
                await itemTypeRepository.DeactivateItemTypeAsync(id);
                TempData["WarningMessage"] = "Item type has been deactivated";
            }
            else
            {
                await itemTypeRepository.DeleteAsync(id);
                TempData["SuccessMessage"] = "Item type successfully deleted";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reactivate(int id)
        {
            if (await itemTypeRepository.IsItemTypeIdExistAsync(id) == false)
            {
                return NotFound();
            }
            await itemTypeRepository.ReactivateItemTypeAsync(id);
            TempData["SuccessMessage"] = "Item type reactivated successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
