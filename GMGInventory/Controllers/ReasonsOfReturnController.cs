using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    public class ReasonsOfReturnController(IAccessRepository accessRepository
                         , IReasonOfReturnRepository reasonOfReturnRepository) : BaseController(accessRepository)
    {
       
        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ReasonsOfReturn, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.ReasonsOfReturn)).Convert();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetListJson()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ReasonsOfReturn, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            var form = new DataTablesRequestForm(Request.Form);
            return Json(await reasonOfReturnRepository.GetAllForDataTablesAsync(form));
        }

        [HttpPost]
        public async Task<IActionResult> CreateJson(ReasonOfReturnVM reasonOfReturnVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ReasonsOfReturn, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            var responseModel = new ResponseModel();
            if (await reasonOfReturnRepository.IsReasonOfReturnCodeExist(reasonOfReturnVM.ReasonOfReturnCode))
            {
                responseModel.Status = "Error";
                responseModel.Message = "Reason of return code already in use";
            }
            else
            {
                await reasonOfReturnRepository.AddAsync(reasonOfReturnVM);
                responseModel.Status = "Success";
                responseModel.Message = "Reason of return successfully added";
            }
            return Json(responseModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditJson(ReasonOfReturnVM reasonOfReturnVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ReasonsOfReturn, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            var responseModel = new ResponseModel();
            try
            {
                await reasonOfReturnRepository.UpdateAsync(reasonOfReturnVM);
                responseModel.Status = "Success";
                responseModel.Message = "Reason of return successfully updated";
            }
            catch (Exception ex)
            {
                if (!await reasonOfReturnRepository.Exists(reasonOfReturnVM.Id))
                {
                    responseModel.Status = "Error";
                    responseModel.Message = "Reason of return cannot be found";
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
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ReasonsOfReturn, AccessTypes.Delete);
            if (redirectResult != null) return redirectResult;

            var reason = await reasonOfReturnRepository.GetAsync(id);
            if (reason == null)
            {
                return NotFound();
            }
            //if (await reasonOfReturnRepository.IsReasonOfReturnInUse(id))
            //{
            //    reason.IsActive = false;
            //    await reasonOfReturnRepository.UpdateAsync(reason);
            //    TempData["WarningMessage"] = "Reason of return has been deactivated";
            //}
            //else
            //{
                await reasonOfReturnRepository.DeleteAsync(id);
                TempData["SuccessMessage"] = "Reason of return successfully deleted";
           // }
          
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reactivate(int id)
        {
            var redirectResultAdd = await CheckAccessAndRedirectAsync(PageNames.ReasonsOfReturn, AccessTypes.Add);
            var redirectResultEdit = await CheckAccessAndRedirectAsync(PageNames.ReasonsOfReturn, AccessTypes.Edit);
            if (redirectResultAdd != null && redirectResultEdit != null) return redirectResultAdd;

            var reason = await reasonOfReturnRepository.GetAsync(id);
            if (reason == null)
            {
                return NotFound();
            }
            reason.IsActive = true;
            await reasonOfReturnRepository.UpdateAsync(reason);
            TempData["SuccessMessage"] = "Reason of return successfully reactivated";
            return RedirectToAction(nameof(Index));
        }
    }
}
