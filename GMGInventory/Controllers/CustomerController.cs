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
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IAreaRepository areaRepository;
        private readonly IRegionRepository regionRepository;
        private readonly ICreditTermRepository credittermRepository;
        private readonly ICustomerTypeRepository customertypeRepository;
        private readonly IAgentRepository agentRepository;
        private readonly IMapper mapper;

        public CustomerController(ICustomerRepository customerRepository
            , IAreaRepository areaRepository
            , IRegionRepository regionRepository
            , ICreditTermRepository credittermRepository
            , ICustomerTypeRepository customertypeRepository
            , IAgentRepository agentRepository)
        {
            this.customerRepository = customerRepository;
            this.areaRepository = areaRepository;
            this.regionRepository = regionRepository;
            this.credittermRepository = credittermRepository;
            this.customertypeRepository = customertypeRepository;
            this.agentRepository = agentRepository;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await customerRepository.GetAllCustomerVMAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            var customerVM = await customerRepository.GetCustomerViewVMAsync(id);
            if (customerVM == null)
            {
                return NotFound();
            }
            return View(customerVM);
        }

        public async Task<IActionResult> Create()

        {
            


            var customerVM = new CustomerVM
            {
                Areas = new SelectList(await areaRepository.GetAllActiveAreaAsync(-1), "Id", "Name"),
                Regions = new SelectList(await regionRepository.GetAllActiveRegionAsync(-1), "Id", "Name"),
                CreditTerms = new SelectList(await credittermRepository.GetAllActiveCreditTermAsync(-1), "Id", "Name"),
                CustomerTypes = new SelectList(await customertypeRepository.GetAllActiveCustomerTypeAsync(-1), "Id", "Name"),
             
                Agents = new SelectList(await agentRepository.GetAllActiveAgentAsync(-1), "Id", "FullName")


            };
            return View(customerVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerVM customerVM)
        {
            if (ModelState.IsValid)
            {
                if (await customerRepository.IsCustomerCodeExist(customerVM.Code))
                {
                    ModelState.AddModelError(string.Empty, "Customer code already in use.");
                    TempData["ErrorMessage"] = "Customer code already in use";
                }
                else
                {
                    await customerRepository.AddAsync(customerVM);
                    TempData["SuccessMessage"] = "Customer successfully added";
                    return RedirectToAction(nameof(Index));
                }
            }
            customerVM.Areas = new SelectList(await areaRepository.GetAllActiveAreaAsync(customerVM.AreaId), "Id", "Name", customerVM.AreaId);
            customerVM.Regions = new SelectList(await regionRepository.GetAllActiveRegionAsync(customerVM.RegionId), "Id", "Name", customerVM.RegionId);
            customerVM.CustomerTypes = new SelectList(await customertypeRepository.GetAllActiveCustomerTypeAsync(customerVM.CustomerTypeId), "Id", "Name", customerVM.CustomerTypeId);
            customerVM.CreditTerms = new SelectList(await credittermRepository.GetAllActiveCreditTermAsync(customerVM.CreditTermId), "Id", "Name", customerVM.CreditTermId);
            customerVM.Agents = new SelectList(await agentRepository.GetAllActiveAgentAsync(customerVM.AgentId), "Id", "FullName", customerVM.AgentId);

            return View(customerVM);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var customerVM = await customerRepository.GetCustomerVMAsync(id);
            if (customerVM == null)
            {
                return NotFound();
            }
            customerVM.Areas = new SelectList(await areaRepository.GetAllActiveAreaAsync(customerVM.AreaId), "Id", "Name", customerVM.AreaId);
            customerVM.Regions = new SelectList(await regionRepository.GetAllActiveRegionAsync(customerVM.RegionId), "Id", "Name", customerVM.RegionId);
            customerVM.CustomerTypes = new SelectList(await customertypeRepository.GetAllActiveCustomerTypeAsync(customerVM.CustomerTypeId), "Id", "Name", customerVM.CustomerTypeId);
            customerVM.CreditTerms = new SelectList(await credittermRepository.GetAllActiveCreditTermAsync(customerVM.CreditTermId), "Id", "Name", customerVM.CreditTermId);
            customerVM.Agents = new SelectList(await agentRepository.GetAllActiveAgentAsync(customerVM.AgentId), "Id", "FullName", customerVM.AgentId);
            return View(customerVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CustomerVM customerVM)
        {
            if (id != customerVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await customerRepository.UpdateAsync(id, customerVM);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await customerRepository.Exists(customerVM.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["SuccessMessage"] = "Customer successfully updated";
                return RedirectToAction(nameof(Index));
            }

            customerVM.Areas = new SelectList(await areaRepository.GetAllActiveAreaAsync(customerVM.AreaId), "Id", "Name", customerVM.AreaId);
            customerVM.Regions = new SelectList(await regionRepository.GetAllActiveRegionAsync(customerVM.RegionId), "Id", "Name", customerVM.RegionId);
            customerVM.CustomerTypes = new SelectList(await customertypeRepository.GetAllActiveCustomerTypeAsync(customerVM.CustomerTypeId), "Id", "Name", customerVM.CustomerTypeId);
            customerVM.CreditTerms = new SelectList(await credittermRepository.GetAllActiveCreditTermAsync(customerVM.CreditTermId), "Id", "Name", customerVM.CreditTermId);
            customerVM.Agents = new SelectList(await agentRepository.GetAllActiveAgentAsync(customerVM.AgentId), "Id", "FullName", customerVM.AgentId);
            return View(customerVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (await customerRepository.IsCustomerIdExistAsync(id) == false)
            {
                return NotFound();
            }
            if (await customerRepository.IsCustomerInUse(id))
            {
                await customerRepository.DeactivateCustomerAsync(id);
                TempData["WarningMessage"] = "Customer has been deactivated";
            }
            else
            {
                await customerRepository.DeleteAsync(id);
                TempData["SuccessMessage"] = "Customer successfully deleted";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reactivate(int id)
        {
            if (await customerRepository.IsCustomerIdExistAsync(id) == false)
            {
                return NotFound();
            }
            await customerRepository.ReactivateCustomerAsync(id);
            TempData["SuccessMessage"] = "Customer reactivated successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
