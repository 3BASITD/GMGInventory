using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    public class LocationCodeController(IAccessRepository accessRepository
                            , ILocationCodeRepository locationCodeRepository) : BaseController(accessRepository)
    {
        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.LocationCodes, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.LocationCodes)).Convert();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetListJson()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.LocationCodes, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            var form = new DataTablesRequestForm(Request.Form);
            return Json(await locationCodeRepository.GetAllForDataTablesAsync(form));
        }

        [HttpPost]
        public async Task<IActionResult> CreateJson(LocationCodeVM locationCodeVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.LocationCodes, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            var responseModel = new ResponseModel();
            if (await locationCodeRepository.IsLocationCodeExist(locationCodeVM.LocationCode))
            {
                responseModel.Status = "Error";
                responseModel.Message = "Location code already in use";
            }
            else
            {
                await locationCodeRepository.AddAsync(locationCodeVM);
                responseModel.Status = "Success";
                responseModel.Message = "Location successfully added";
            }
            return Json(responseModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditJson(LocationCodeVM locationCodeVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.LocationCodes, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            var responseModel = new ResponseModel();
            try
            {
                await locationCodeRepository.UpdateAsync(locationCodeVM);
                responseModel.Status = "Success";
                responseModel.Message = "Location code successfully updated";
            }
            catch (Exception ex)
            {
                if (!await locationCodeRepository.Exists(locationCodeVM.Id))
                {
                    responseModel.Status = "Error";
                    responseModel.Message = "Location code cannot be found";
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
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.LocationCodes, AccessTypes.Delete);
            if (redirectResult != null) return redirectResult;

            var reason = await locationCodeRepository.GetAsync(id);
            if (reason == null)
            {
                return NotFound();
            }
            //if (await locationCodeRepository.IsLocationCodeInUse(id))
            //{
            //    reason.IsActive = false;
            //    await locationCodeRepository.UpdateAsync(reason);
            //    TempData["WarningMessage"] = "Location code has been deactivated";
            //}
            //else
            //{
                await locationCodeRepository.DeleteAsync(id);
                TempData["SuccessMessage"] = "Location code successfully deleted";
            //}

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reactivate(int id)
        {
            var redirectResultAdd = await CheckAccessAndRedirectAsync(PageNames.LocationCodes, AccessTypes.Add);
            var redirectResultEdit = await CheckAccessAndRedirectAsync(PageNames.LocationCodes, AccessTypes.Edit);
            if (redirectResultAdd != null && redirectResultEdit != null) return redirectResultAdd;

            var reason = await locationCodeRepository.GetAsync(id);
            if (reason == null)
            {
                return NotFound();
            }
            reason.IsActive = true;
            await locationCodeRepository.UpdateAsync(reason);
            TempData["SuccessMessage"] = "Location code successfully reactivated";
            return RedirectToAction(nameof(Index));
        }
    }
}
