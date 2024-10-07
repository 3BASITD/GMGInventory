using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    [Authorize(Roles = Roles.InventoryManager + ","
                     + Roles.SalesDirector + ","
                     + Roles.SystemAdmin + ","
                     + Roles.Telemarketer)]
   
    public class PurchaseReturnController(IAccessRepository accessRepository
                             , IPurchaseReturnRepository purchaseReturnRepository
                             , UserManager<User> userManager) : BaseController(accessRepository, userManager)
    {

        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseReturn, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

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