using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GMGInventory.Data;
using GMGInventory.Common.Models;
using GMGInventory.Common.Constants;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using GMGInventory.Application.Contracts;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GMGInventory.Controllers
{
    //[Authorize(Roles = Roles.InventoryManager + "," + Roles.SalesDirector)]
    [Authorize]
    public class AgentController : Controller
    {
        private readonly IAgentRepository agentRepository;
        private readonly IAreaRepository areaRepository;
        private readonly IMapper mapper;

        public AgentController(IAgentRepository agentRepository
            , IAreaRepository areaRepository)
        {
            this.agentRepository = agentRepository;
            this.areaRepository = areaRepository;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await agentRepository.GetAllAgentVMAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            var agentVM = await agentRepository.GetAgentViewVMAsync(id);
            if (agentVM == null)
            {
                return NotFound();
            }
            return View(agentVM);
        }

        public async Task<IActionResult> Create()
        {
            var agentVM = new AgentVM
            {
                Areas = new SelectList(await areaRepository.GetAllActiveAreaAsync(-1), "Id", "Name")
            };
            return View(agentVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AgentVM agentVM)
        {
            if (ModelState.IsValid)
            {
                if (await agentRepository.IsAgentCodeExist(agentVM.Code))
                {
                    ModelState.AddModelError(string.Empty, "Agent code already in use.");
                    TempData["ErrorMessage"] = "Agent code already in use";
                }
                else
                {
                    await agentRepository.AddAsync(agentVM);
                    TempData["SuccessMessage"] = "Agent successfully added";
                    return RedirectToAction(nameof(Index));
                }
            }
            agentVM.Areas = new SelectList(await areaRepository.GetAllActiveAreaAsync(agentVM.AreaId), "Id", "Name", agentVM.AreaId);
            return View(agentVM);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var agentVM = await agentRepository.GetAgentVMAsync(id);
            if (agentVM == null)
            {
                return NotFound();
            }
            agentVM.Areas = new SelectList(await areaRepository.GetAllActiveAreaAsync(-1), "Id", "Name");
            return View(agentVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AgentVM agentVM)
        {
            if (id != agentVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await agentRepository.UpdateAsync(id, agentVM);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await agentRepository.Exists(agentVM.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["SuccessMessage"] = "Agent successfully updated";
                return RedirectToAction(nameof(Index));
            }

            agentVM.Areas= new SelectList(await areaRepository.GetAllActiveAreaAsync(-1), "Id", "Name");
            return View(agentVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (await agentRepository.IsAgentIdExistAsync(id) == false)
            {
                return NotFound();
            }
            if (await agentRepository.IsAgentInUse(id))
            {
                await agentRepository.DeactivateAgentAsync(id);
                TempData["WarningMessage"] = "Agent has been deactivated";
            }
            else
            {
                await agentRepository.DeleteAsync(id);
                TempData["SuccessMessage"] = "Agent successfully deleted";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reactivate(int id)
        {
            if (await agentRepository.IsAgentIdExistAsync(id) == false)
            {
                return NotFound();
            }
            await agentRepository.ReactivateAgentAsync(id);
            TempData["SuccessMessage"] = "Agent reactivated successfully";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> GetOptions(int? IncludeId)
        {

            return Json(await agentRepository.GetOptions(IncludeId));
        }
    }
}
