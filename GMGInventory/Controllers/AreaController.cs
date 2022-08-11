using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GMGInventory.Data;
using GMGInventory.Common.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using GMGInventory.Application.Contracts;
using Microsoft.AspNetCore.Mvc.Rendering;
using GMGInventory.Common.Constants;

namespace GMGInventory.Controllers
{
    //[Authorize(Roles = Roles.InventoryManager + "," + Roles.SalesDirector)]
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
            var areaVM = await areaRepository.GetAreaViewVMAsync(id);
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
                    TempData["ErrorMessage"] = "Area code already in use";
                }
                else
                {
                    await areaRepository.AddAsync(areaVM);
                    TempData["SuccessMessage"] = "Area successfully added";
                    return RedirectToAction(nameof(Index));
                }
            }
            areaVM.Regions = new SelectList(await regionRepository.GetAllActiveRegionAsync(areaVM.RegionId), "Id", "Name", areaVM.RegionId);
            return View(areaVM);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var areaVM = await areaRepository.GetAreaVMAsync(id);
            if (areaVM == null)
            {
                return NotFound();
            }
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
                    await areaRepository.UpdateAsync(id, areaVM);
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
                TempData["SuccessMessage"] = "Area successfully updated";
                return RedirectToAction(nameof(Index));
            }

            areaVM.Regions = new SelectList(await regionRepository.GetAllActiveRegionAsync(-1), "Id", "Name");
            return View(areaVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (await areaRepository.IsAreaIdExistAsync(id) == false)
            {
                return NotFound();
            }
            if (await areaRepository.IsAreaInUse(id))
            {
                await areaRepository.DeactivateAreaAsync(id);
                TempData["WarningMessage"] = "Area has been deactivated";
            }
            else
            {
                await areaRepository.DeleteAsync(id);
                TempData["SuccessMessage"] = "Area successfully deleted";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reactivate(int id)
        {
            if (await areaRepository.IsAreaIdExistAsync(id) == false)
            {
                return NotFound();
            }
            await areaRepository.ReactivateAreaAsync(id);
            TempData["SuccessMessage"] = "Area reactivated successfully";
            return RedirectToAction(nameof(Index));
        }


    }
}
