using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GMGInventory.Data;
using GMGInventory.Common.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using GMGInventory.Application.Contracts;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await categoryRepository.GetAllGenericVMAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            var categoryVM = await categoryRepository.GetGenericVMAsync(id);
            if (categoryVM == null)
            {
                return NotFound();
            }
            return View(categoryVM);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryVM categoryVM)
        {
            if (ModelState.IsValid)
            {
                if (await categoryRepository.IsCategoryCodeExist(categoryVM.Code))
                {
                    //ModelState.AddModelError(string.Empty, "Category code already in use.");
                    TempData["ErrorMessage"] = "Category code already in use";
                }
                else
                {
                    await categoryRepository.AddAsync(categoryVM);
                    TempData["SuccessMessage"] = "Category successfully added";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(categoryVM);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var categoryVM = await categoryRepository.GetCategoryVMAsync(id);
            if (categoryVM == null)
            {
                return NotFound();
            }
            return View(categoryVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryVM categoryVM)
        {
            if (id != categoryVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await categoryRepository.UpdateAsync(id, categoryVM);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await categoryRepository.Exists(categoryVM.Id))
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
            TempData["SuccessMessage"] = "Category successfully updated";
            return View(categoryVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await categoryRepository.GetAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            if (await categoryRepository.IsCategoryInUse(id))
            {
                category.IsActive = false;
                await categoryRepository.UpdateAsync(category);
                TempData["WarningMessage"] = "Category has been deactivated";
            }
            else
            {
                await categoryRepository.DeleteAsync(id);
                TempData["SuccessMessage"] = "Category successfully deleted";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reactivate(int id)
        {
            var category = await categoryRepository.GetAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            category.IsActive = true;
            await categoryRepository.UpdateAsync(category);
            TempData["SuccessMessage"] = "Category successfully reactivated";
            return RedirectToAction(nameof(Index));
        }
    }
}
