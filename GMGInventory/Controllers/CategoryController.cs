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
        private readonly IGenericRepository<Category> categoryRepository;
        private readonly IMapper mapper;

        public CategoryController(IGenericRepository<Category> categoryRepository
            , IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await categoryRepository.GetAllAsync();
            var categoriesVM = mapper.Map<List<GenericAuditVM>>(categories);
            foreach(GenericAuditVM cvm in categoriesVM)
            {
                cvm.UserCreated = await categoryRepository.GetUserName(cvm.UserCreated);
                cvm.UserModified = await categoryRepository.GetUserName(cvm.UserModified);
            }
            return View(categoriesVM);
        }

        public async Task<IActionResult> Details(int? id)
        {
            var category = await categoryRepository.GetAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            var categoryVM = mapper.Map<GenericAuditVM>(category);
            categoryVM.UserCreated = await categoryRepository.GetUserName(categoryVM.UserCreated);
            categoryVM.UserModified = await categoryRepository.GetUserName(categoryVM.UserModified);
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
                var category = mapper.Map<Category>(categoryVM);
                await categoryRepository.AddAsync(category);
                return RedirectToAction(nameof(Index));
            }
            return View(categoryVM);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var category = await categoryRepository.GetAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            var categoryVM = mapper.Map<CategoryVM>(category);
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
                    var category = await categoryRepository.GetAsync(id);
                    mapper.Map(categoryVM, category);
                    await categoryRepository.UpdateAsync(category);
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
            return View(categoryVM);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var category = categoryRepository.GetAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            var categoryVM = mapper.Map<CategoryVM>(category);
            return View(categoryVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await categoryRepository.GetAllAsync() == null)
            {
                return Problem("Entity set 'GMGInventoryContext.Category'  is null.");
            }
            var category = await categoryRepository.GetAsync(id);
            if (category != null)
            {
                await categoryRepository.DeleteAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
