using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    [Authorize(Roles = Roles.InventoryManager + "," + Roles.SalesDirector + "," + Roles.SystemAdmin + "," + Roles.Telemarketer)]
    public class PurchaseReturnController : Controller
    {
        private readonly IPurchaseReturnRepository purchaseReturnRepository;

        public PurchaseReturnController(IPurchaseReturnRepository purchaseReturnRepository)
        {
            this.purchaseReturnRepository = purchaseReturnRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetListJson(int purchaseOrderId)
        {
            return Json(await purchaseReturnRepository.GetListAsync(purchaseOrderId));
        }

        [HttpPost]
        public async Task<JsonResult> SaveJson(PurchaseReturnFormVM formVM)
        {
            ResponseModel responseModel = new ResponseModel();
            await purchaseReturnRepository.AddAsync(formVM);
            responseModel.Status = "Success";
            responseModel.Message = "Purchase return successfully posted";
            return Json(responseModel);
        }
    }
}