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
    public class SupplierController : Controller
    {
        private readonly ISupplierRepository supplierRepository;
        private readonly IMapper mapper;

        public SupplierController(ISupplierRepository supplierRepository
            , IMapper mapper)
        {
            this.supplierRepository = supplierRepository;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await supplierRepository.GetAllSupplierVMAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            var supplierVM = await supplierRepository.GetSupplierVMAsync(id);
            if (supplierVM == null)
            {
                return NotFound();
            }
            return View(supplierVM);
        }

        public IActionResult Create()
        {
            return View();
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
                }
                else
                {
                    var supplier = mapper.Map<Supplier>(supplierVM);
                    supplier.IsActive = true;
                    await supplierRepository.AddAsync(supplier);
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(supplierVM);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var supplier = await supplierRepository.GetAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }
            var supplierVM = mapper.Map<SupplierVM>(supplier);
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
                    var supplier = await supplierRepository.GetAsync(id);
                    mapper.Map(supplierVM, supplier);
                    await supplierRepository.UpdateAsync(supplier);
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
                return RedirectToAction(nameof(Index));
            }
            return View(supplierVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var supplier = await supplierRepository.GetAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }
            if (await supplierRepository.IsSupplierInUse(id))
            {
                supplier.IsActive = false;
                await supplierRepository.UpdateAsync(supplier);
            }
            else
            {
                await supplierRepository.DeleteAsync(id);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reactivate(int id)
        {
            var supplier = await supplierRepository.GetAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }
            supplier.IsActive = true;
            await supplierRepository.UpdateAsync(supplier);
            return RedirectToAction(nameof(Index));
        }
    }
}
