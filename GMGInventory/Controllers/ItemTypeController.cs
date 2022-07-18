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
        private readonly IMapper mapper;

        public ItemTypeController(IItemTypeRepository itemTypeRepository
            , ICategoryRepository categoryRepository
            , IMapper mapper)
        {
            this.itemTypeRepository = itemTypeRepository;
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
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
            var itemTypeVM = new ItemTypeVM
            {
                Categories = new SelectList(await categoryRepository.GetAllActiveCategoryAsync(-1), "Id", "Name")
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
                }
                else
                {
                    var itemType = mapper.Map<ItemType>(itemTypeVM);
                    itemType.IsActive = true;
                    await itemTypeRepository.AddAsync(itemType);
                    return RedirectToAction(nameof(Index));
                }
            }
            itemTypeVM.Categories = new SelectList(await categoryRepository.GetAllActiveCategoryAsync(itemTypeVM.CategoryId), "Id", "Name", itemTypeVM.CategoryId);
            return View(itemTypeVM);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var itemType = await itemTypeRepository.GetAsync(id);
            if (itemType == null)
            {
                return NotFound();
            }
            var itemTypeVM = mapper.Map<ItemTypeVM>(itemType);
            itemTypeVM.Categories = new SelectList(await categoryRepository.GetAllActiveCategoryAsync(-1), "Id", "Name");
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
                    var itemType = await itemTypeRepository.GetAsync(id);
                    mapper.Map(itemTypeVM, itemType);
                    await itemTypeRepository.UpdateAsync(itemType);
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
                return RedirectToAction(nameof(Index));
            }

            itemTypeVM.Categories = new SelectList(await categoryRepository.GetAllActiveCategoryAsync(-1), "Id", "Name");
            return View(itemTypeVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var itemType = await itemTypeRepository.GetAsync(id);
            if (itemType == null)
            {
                return NotFound();
            }
            if (await itemTypeRepository.IsItemTypeInUse(id))
            {
                itemType.IsActive = false;
                await itemTypeRepository.UpdateAsync(itemType);
            }
            else
            {
                await itemTypeRepository.DeleteAsync(id);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reactivate(int id)
        {
            var itemType = await itemTypeRepository.GetAsync(id);
            if (itemType == null)
            {
                return NotFound();
            }
            itemType.IsActive = true;
            await itemTypeRepository.UpdateAsync(itemType);
            return RedirectToAction(nameof(Index));
        }
    }
}
