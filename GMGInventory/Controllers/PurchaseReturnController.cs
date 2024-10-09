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
   
    public class PurchaseReturnController(IAccessRepository accessRepository
                                        , IPurchaseReturnRepository purchaseReturnRepository) : BaseController(accessRepository)
    {

        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseReturn, AccessTypes.View);
            if (redirectResult != null) return redirectResult;
            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.PurchaseReturn)).Convert();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetListJson(int purchaseOrderId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseReturn, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return Json(await purchaseReturnRepository.GetListAsync(purchaseOrderId));
        }

        [HttpPost]
        public async Task<IActionResult> SaveJson(PurchaseReturnFormVM formVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseReturn, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            ResponseModel responseModel = new ResponseModel();
            await purchaseReturnRepository.AddAsync(formVM);
            responseModel.Status = "Success";
            responseModel.Message = "Purchase return successfully posted";
            return Json(responseModel);
        }
    }
}