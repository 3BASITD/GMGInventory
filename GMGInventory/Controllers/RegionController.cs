using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class RegionController(IAccessRepository accessRepository
                         , IRegionRepository regionRepository
                         , UserManager<User> userManager) : BaseController(accessRepository, userManager)
    {

        public async Task<IActionResult> GetOptions(int? IncludeId)
        {
            return Json(await regionRepository.GetOptions(IncludeId));
        }

        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Regions, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.Regions)).Convert();

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> GetListJson()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Regions, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            var form = new DataTablesRequestForm(Request.Form);
            return Json(await regionRepository.GetAllForDataTablesAsync(form));
        }

        [HttpPost]
        public async Task<IActionResult> CreateJson(RegionVM regionVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Regions, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            var responseModel = new ResponseModel();
            if (await regionRepository.IsRegionCodeExist(regionVM.Code))
            {
                responseModel.Status = "Error";
                responseModel.Message = "Region already in use";
            }
            else
            {
                await regionRepository.AddAsync(regionVM);
                responseModel.Status = "Success";
                responseModel.Message = "Region successfully added";
            }
            return Json(responseModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditJson(RegionVM regionVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Regions, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            var responseModel = new ResponseModel();
            try
            {
                await regionRepository.UpdateAsync(regionVM);
                responseModel.Status = "Success";
                responseModel.Message = "Region successfully updated";
            }
            catch (Exception ex)
            {
                if (!await regionRepository.Exists(regionVM.Id))
                {
                    responseModel.Status = "Error";
                    responseModel.Message = "Region cannot be found";
                }
                else
                {
                    responseModel.Status = "Error";
                    responseModel.Message = ex.Message;
                }
            }
            return Json(responseModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Regions, AccessTypes.Delete);
            if (redirectResult != null) return redirectResult;

            var reason = await regionRepository.GetAsync(id);
            if (reason == null)
            {
                return NotFound();
            }
            if (await regionRepository.IsRegionInUse(id))
            {
                reason.IsActive = false;
                await regionRepository.UpdateAsync(reason);
                TempData["WarningMessage"] = "Reason of return has been deactivated";
            }
            else
            {
                await regionRepository.DeleteAsync(id);
                TempData["SuccessMessage"] = "Reason of return successfully deleted";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reactivate(int id)
        {
            var redirectResultAdd = await CheckAccessAndRedirectAsync(PageNames.Regions, AccessTypes.Add);
            var redirectResultEdit = await CheckAccessAndRedirectAsync(PageNames.Regions, AccessTypes.Edit);
            if (redirectResultAdd != null && redirectResultEdit != null) return redirectResultAdd;

            var reason = await regionRepository.GetAsync(id);
            if (reason == null)
            {
                return NotFound();
            }
            reason.IsActive = true;
            await regionRepository.UpdateAsync(reason);
            TempData["SuccessMessage"] = "Location code successfully reactivated";
            return RedirectToAction(nameof(Index));
        }
    }
}