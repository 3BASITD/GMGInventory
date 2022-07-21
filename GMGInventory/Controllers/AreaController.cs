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
    public class AreaController : Controller
    {
        private readonly IAreaRepository areaRepository;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public AreaController(IAreaRepository areaRepository
            , IRegionRepository regionRepository
            , IMapper mapper)
        {
            this.areaRepository = areaRepository;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await areaRepository.GetAllAreaVMAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            var areaVM = await areaRepository.GetAreaVMAsync(id);
            if (areaVM == null)
            {
                return NotFound();
            }
            return View(areaVM);
        }

        public async Task<IActionResult> Create()
        {
            var areaVM = new AreaVM
            {
                Regions = new SelectList(await regionRepository.GetAllActiveRegionAsync(-1), "Id", "Name")
            };
            return View(areaVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AreaVM areaVM)
        {
            if (ModelState.IsValid)
            {
                if (await areaRepository.IsAreaCodeExist(areaVM.Code))
                {
                    ModelState.AddModelError(string.Empty, "Area code already in use.");
                }
                else
                {
                    var area = mapper.Map<Area>(areaVM);
                    area.IsActive = true;
                    await areaRepository.AddAsync(area);
                    return RedirectToAction(nameof(Index));
                }
            }
            areaVM.Regions = new SelectList(await regionRepository.GetAllActiveRegionAsync(areaVM.RegionId), "Id", "Name", areaVM.RegionId);
            return View(areaVM);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var area = await areaRepository.GetAsync(id);
            if (area == null)
            {
                return NotFound();
            }
            var areaVM = mapper.Map<AreaVM>(area);
            areaVM.Regions = new SelectList(await regionRepository.GetAllActiveRegionAsync(-1), "Id", "Name");
            return View(areaVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AreaVM areaVM)
        {
            if (id != areaVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var area = await areaRepository.GetAsync(id);
                    mapper.Map(areaVM, area);
                    await areaRepository.UpdateAsync(area);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await areaRepository.Exists(areaVM.Id))
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

            areaVM.Regions = new SelectList(await regionRepository.GetAllActiveRegionAsync(-1), "Id", "Name");
            return View(areaVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var area = await areaRepository.GetAsync(id);
            if (area == null)
            {
                return NotFound();
            }
            if (await areaRepository.IsAreaInUse(id))
            {
                area.IsActive = false;
                await areaRepository.UpdateAsync(area);
            }
            else
            {
                await areaRepository.DeleteAsync(id);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reactivate(int id)
        {
            var area = await areaRepository.GetAsync(id);
            if (area == null)
            {
                return NotFound();
            }
            area.IsActive = true;
            await areaRepository.UpdateAsync(area);
            return RedirectToAction(nameof(Index));
        }
    }
}
