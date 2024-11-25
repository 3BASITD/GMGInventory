using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class PurchaseReturnNewController(IAccessRepository accessRepository
                                           , IPurchaseReturnNewRepository returnRepository) : BaseController(accessRepository)
    {
        public async Task<IActionResult> GetPurchaseReceivingOptions(int? SelectedId)
        {
            return Json(await returnRepository.GetPurchaseReceivingOptions(SelectedId));
        }

        public async Task<IActionResult> GetPurchaseReceiveItemOptions(int ReceiveId, int? SelectedId)
        {
            return Json(await returnRepository.GetPurchaseReceiveItemOptions(ReceiveId, SelectedId));
        }

        public async Task<IActionResult> GetPurchaseReceiveItemList(int ReceiveId)
        {
            return Json(await returnRepository.GetPurchaseReceiveItemList(ReceiveId));
        }

        public async Task<IActionResult> GetNewDocumentNumber()
        {
            return Json(await returnRepository.GetNewDocumentNumber());
        }

        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseReturnNew, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.PurchaseReturnNew)).Convert();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetListDTJson()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseReturnNew, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            var form = new DataTablesRequestForm(Request.Form);
            return Json(await returnRepository.GetListDTAsync(form));
        }

        public async Task<IActionResult> Details(int Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseReturnNew, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.PurchaseReturnNew)).Convert();

            var request = await returnRepository.GetDetails(Id);
            if (request == null)
                return RedirectToAction(nameof(Index));

            return View(request);
        }

        public async Task<IActionResult> DetailsJson(int Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseReturnNew, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            var request = await returnRepository.GetDetails(Id);
            if (request == null)
                return NotFound();

            return Json(request);
        }

        public async Task<IActionResult> PrintDetails(int Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseReturnNew, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;

            ViewData["Items"] = await returnRepository.GetItemListAsync(Id);

            return View(await returnRepository.GetDetails(Id));
        }

        public async Task<IActionResult> Create(int? Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseReturnNew, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            var request = await returnRepository.GetDetails(Id ?? 0);
            if (Id != null && (request == null || request.DisapprovedBy == null || request.Status != "Disapproved"))
            {
                return RedirectToAction("Details", new { Id });
            }
            TempData["PurchaseReturnNewId"] = Id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetItemListJson(int purchaseReturnId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseReturnNew, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return Json(await returnRepository.GetItemListAsync(purchaseReturnId));
        }

        [HttpPost]
        public async Task<IActionResult> GetItemListDTJson(int purchaseReturnId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseReturnNew, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            var form = new DataTablesRequestForm(Request.Form);
            return Json(await returnRepository.GetItemListDTAsync(form, purchaseReturnId));
        }

        [HttpPost]
        public async Task<IActionResult> AddJson(PurchaseReturnNewSaveVM form, List<PurchaseReturnNewLineItemVM> items)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseReturnNew, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            var response = await returnRepository.AddAsync(form, items);
            if (response.Status == "Success") TempData["SuccessMessage"] = response.Message;
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> SaveItemsJson(List<PurchaseReturnNewLineItemVM> items, int Id, string status)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseReturnNew, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            var response = await returnRepository.SaveItemsAsync(items, Id, status);
            if (response.Status == "Success") TempData["SuccessMessage"] = response.Message;

            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveJson(int Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseReturnNew, AccessTypes.ApproveDisapprove);
            if (redirectResult != null) return redirectResult;

            var response = await returnRepository.ApproveAsync(Id);
            if (response.Status == "Success") TempData["SuccessMessage"] = response.Message;
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> DisapproveJson(int Id, string? reason)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseReturnNew, AccessTypes.ApproveDisapprove);
            if (redirectResult != null) return redirectResult;

            var response = await returnRepository.DisapproveAsync(Id, reason);
            if (response.Status == "Success") TempData["SuccessMessage"] = response.Message;
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteJson(int Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseReturnNew, AccessTypes.Delete);
            if (redirectResult != null) return redirectResult;

            var response = await returnRepository.DeleteAsync(Id);
            if (response.Status == "Success") TempData["SuccessMessage"] = response.Message;
            return Json(response);
        }
    }
}