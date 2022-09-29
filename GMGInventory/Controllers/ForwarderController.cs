using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Authorization;
using GMGInventory.Application.Contracts;
using Microsoft.AspNetCore.Mvc.Rendering;
using GMGInventory.Application.Repositories;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class ForwarderController : Controller
    {
        private readonly IForwarderRepository forwarderRepository;
        private readonly ICustomerRepository customerRepository;

        public ForwarderController(IForwarderRepository forwarderRepository
            , ICustomerRepository customerRepository)
        {
            this.forwarderRepository = forwarderRepository;
            this.customerRepository = customerRepository;
        }

        public async Task<IActionResult> GetOptions(int? IncludeId)
        {
            return Json(await forwarderRepository.GetOptions(IncludeId));
        }

        public async Task<IActionResult> Index()
        {
            return View(await forwarderRepository.GetAllForwarderVMAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            var forwarderVM = await forwarderRepository.GetForwarderViewVMAsync(id);
            if (forwarderVM == null)
            {
                return NotFound();
            }
            return View(forwarderVM);
        }

        public async Task<IActionResult> Create()
        {
            var forwarderVM = new ForwarderVM
            {
                Customers = new SelectList(await customerRepository.GetAllActiveCustomerAsync(-1), "Id", "Name"),
               
            };
            return View(forwarderVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ForwarderVM forwarderVM)
        {
            if (ModelState.IsValid)
            {
                if (await forwarderRepository.IsForwarderCodeExist(forwarderVM.Name))
                {
                    ModelState.AddModelError(string.Empty, "Forwarder Name already in use.");
                    TempData["ErrorMessage"] = "Forwarder Name already in use";
                }
                else
                {

                    await forwarderRepository.AddAsync(forwarderVM);
                    TempData["SuccessMessage"] = "Forwarder successfully added";
                    return RedirectToAction(nameof(Index));
                }
            }
            forwarderVM.Customers = new SelectList(await customerRepository.GetAllActiveCustomerAsync(forwarderVM.CustomerId), "Id", "Name", forwarderVM.CustomerId);
           
            return View(forwarderVM);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var forwarderVM = await forwarderRepository.GetForwarderVMAsync(id);
            if (forwarderVM == null)
            {
                return NotFound();
            }
            forwarderVM.Customers = new SelectList(await customerRepository.GetAllActiveCustomerAsync(-1), "Id", "Name");
            return View(forwarderVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ForwarderVM forwarderVM)
        {
            if (id != forwarderVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await forwarderRepository.UpdateAsync(id, forwarderVM);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await forwarderRepository.Exists(forwarderVM.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["SuccessMessage"] = "Forwarder successfully updated";
                return RedirectToAction(nameof(Index));
            }

            forwarderVM.Customers = new SelectList(await customerRepository.GetAllActiveCustomerAsync(-1), "Id", "Name");
            return View(forwarderVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (await forwarderRepository.IsForwarderIdExistAsync(id) == false)
            {
                return NotFound();
            }
            if (await forwarderRepository.IsForwarderInUse(id))
     
            {
                await forwarderRepository.DeleteAsync(id);
                TempData["SuccessMessage"] = "Forwarder successfully deleted";
            }
            return RedirectToAction(nameof(Index));
        }

        
    }
}
