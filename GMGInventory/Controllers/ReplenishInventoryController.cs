using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class ReplenishInventoryController(IAccessRepository accessRepository
                                       , IPurchaseOrderRepository purchaseOrderRepository) : BaseController(accessRepository)
    {

        public async Task<IActionResult> GetOptions(string search, int page)
        {
            return Json(await purchaseOrderRepository.GetOptions(search, page));
        }

        public async Task<IActionResult> GetCurrencyOptions(int? IncludeId)
        {
            return Json(await purchaseOrderRepository.GetCurrencyOptions(IncludeId));
        }

        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ReplenishInventory, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.ReplenishInventory)).Convert();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetListDTJson()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ReplenishInventory, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await purchaseOrderRepository.GetListDTAsync(form));
        }

        public async Task<IActionResult> Details(int? Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ReplenishInventory, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.ReplenishInventory)).Convert();

            var order = await purchaseOrderRepository.GetDetailsAsync(Id);
            if (order == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var dtNow = DateTime.Now;
            var newChecker = dtNow.AddMinutes(-1) <= order.OrderDateD && order.OrderDateD <= dtNow;
            if (newChecker)
            {
                TempData["SuccessMessage"] = order.Status + " successfully saved";
            }

            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> GetItemsJson(int purchaseOrderId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ReplenishInventory, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return Json(await purchaseOrderRepository.GetItemsAsync(purchaseOrderId));
        }

        [HttpPost]
        public async Task<IActionResult> GetItemsDTJson(int purchaseOrderId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ReplenishInventory, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await purchaseOrderRepository.GetItemsDTAsync(form, purchaseOrderId));
        }

        public async Task<IActionResult> Create()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ReplenishInventory, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.PurchaseOrder)).Convert();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveJson(PurchaseOrderFormVM form, string type)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ReplenishInventory, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            ResponseModel<int> responseModel = new ResponseModel<int>();
            if (type == "Draft")
            {
                var draft = await purchaseOrderRepository.SaveDraftAsync(form);
                responseModel.Status = "Success";
                responseModel.Message = "Draft successfully saved";
                responseModel.NewRow = draft;
            }
            else
            {
                var replenish = await purchaseOrderRepository.SaveReplenishAsync(form);
                responseModel.Status = "Success";
                responseModel.Message = "Replenish successfully saved";
                responseModel.NewRow = replenish;
            }
            return Json(responseModel);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateJson(PurchaseOrderFormVM form)
        {
            ResponseModel<int> responseModel = new ResponseModel<int>();
                var replenish = await purchaseOrderRepository.UpdateItemsAsync(form);
                responseModel.Status = "Success";
                responseModel.Message = replenish + " Item(s) updated successfully saved";
                responseModel.NewRow = replenish;
            return Json(responseModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDraftJson(int Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ReplenishInventory, AccessTypes.Delete);
            if (redirectResult != null) return redirectResult;

            ResponseModel responseModel = new ResponseModel();
            if (await purchaseOrderRepository.DeleteDraftAsync(Id))
            {
                responseModel.Status = "Success";
                responseModel.Message = "Draft successfully removed";
            }
            else
            {
                responseModel.Status = "Error";
                responseModel.Message = "Cannot remove draft. Please try again later";
            }
            return Json(responseModel);
        }

        //[HttpPost]
        //public async Task<JsonResult> CreateJson(PurchaseOrderFormVM formVM)
        //{
        //    ResponseModel responseModel = new ResponseModel();
        //    await purchaseOrderRepository.AddAsync(formVM);
        //    responseModel.Status = "Success";
        //    responseModel.Message = "Transfer successfully added";
        //    return Json(responseModel);
        //}
    }
}