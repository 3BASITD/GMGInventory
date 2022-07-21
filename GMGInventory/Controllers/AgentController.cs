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
    public class AgentController : Controller 
    {
        private readonly IAgentRepository agentRepository;
        private readonly IMapper mapper;

        public AgentController(IAgentRepository agentRepository
            , IMapper mapper)
        {
            this.agentRepository = agentRepository;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await agentRepository.GetAllAgentVMAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            var agentVM = await agentRepository.GetAgentVMAsync(id);
            if (agentVM == null)
            {
                return NotFound();
            }
            return View(agentVM);
        }

        public IActionResult Create()
        {
            return View();
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
                }
                else
                {
                    var agent = mapper.Map<Agent>(agentVM);
                    agent.IsActive = true;
                    await agentRepository.AddAsync(agent);
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(agentVM);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var agent = await agentRepository.GetAsync(id);
            if (agent == null)
            {
                return NotFound();
            }
            var agentVM = mapper.Map<AgentVM>(agent);
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
                    var agent = await agentRepository.GetAsync(id);
                    mapper.Map(agentVM, agent);
                    await agentRepository.UpdateAsync(agent);
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
                return RedirectToAction(nameof(Index));
            }
            return View(agentVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var agent = await agentRepository.GetAsync(id);
            if (agent == null)
            {
                return NotFound();
            }
            if (await agentRepository.IsAgentInUse(id))
            {
                agent.IsActive = false;
                await agentRepository.UpdateAsync(agent);
            }
            else
            {
                await agentRepository.DeleteAsync(id);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reactivate(int id)
        {
            var agent= await agentRepository.GetAsync(id);
            if (agent == null)
            {
                return NotFound();
            }
            agent.IsActive = true;
            await agentRepository.UpdateAsync(agent);
            return RedirectToAction(nameof(Index));
        }
    }
}
