using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class PurchaseReceiveController(IAccessRepository accessRepository
                                           , IPurchaseReceiveRepository receiveRepository) : BaseController(accessRepository)
    {
        public async Task<IActionResult> GetPurchaseOrderOptions(int? SelectedId)
        {
            return Json(await receiveRepository.GetPurchaseOrderOptions(SelectedId));
        }

        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseReceiving, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.PurchaseReceiving)).Convert();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetListDTJson()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseReceiving, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            var form = new DataTablesRequestForm(Request.Form);
            return Json(await receiveRepository.GetListDTAsync(form));
        }

        public async Task<IActionResult> Details(int Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseReceiving, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.PurchaseReceiving)).Convert();

            var receive = await receiveRepository.GetDetails(Id);
            if (receive == null)
                return RedirectToAction(nameof(Index));

            return View(receive);
        }

        public async Task<IActionResult> DetailsJson(int Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseReceiving, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            var receive = await receiveRepository.GetDetails(Id);
            if (receive == null)
                return NotFound();

            return Json(receive);
        }

        [HttpPost]
        public async Task<IActionResult> GetItemListJson(int purchaseReceiveId)
        {
            return Json(await receiveRepository.GetItemListAsync(purchaseReceiveId));
        }
   
        [HttpPost]
        public async Task<IActionResult> SaveItemsJson(List<PurchaseReceiveLineItemVM> items, int Id, string status)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseReceiving, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            var response = await receiveRepository.SaveItemsAsync(items, Id, status);
            if (response.Status == "Success") TempData["SuccessMessage"] = response.Message;

            return Json(response);
        }

        public async Task<IActionResult> PrintDetails(int Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseReceiving, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;

            ViewData["Items"] = await receiveRepository.GetItemListAsync(Id);

            return View(await receiveRepository.GetDetails(Id));
        }


        public async Task<IActionResult> Create(int? Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseReceiving, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            var request = await receiveRepository.GetDetails(Id ?? 0);
            if (Id != null && (request == null || request.DisapprovedBy == null || request.Status != "Disapproved"))
            {
                return RedirectToAction("Details", new { Id });
            }
            TempData["PurchaseReceivingId"] = Id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddJson(PurchaseReceiveSaveVM form, List<PurchaseReceiveLineItemVM> items)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseReceiving, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            var response = await receiveRepository.AddAsync(form, items);
            if (response.Status == "Success") TempData["SuccessMessage"] = response.Message;
            return Json(response);
        }
        [HttpPost]
        public async Task<IActionResult> ApproveJson(int Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseReceiving, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            var response = await receiveRepository.ApproveAsync(Id);
            if (response.Status == "Success") TempData["SuccessMessage"] = response.Message;
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> DisapproveJson(int Id, string? reason)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseReceiving, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            var response = await receiveRepository.DisapproveAsync(Id, reason);
            if (response.Status == "Success") TempData["SuccessMessage"] = response.Message;
            return Json(response);
        }

        public async Task<IActionResult> GetNewDocumentNumber()
        {
            return Json(await receiveRepository.GetNewDocumentNumber());
        }
        public async Task<IActionResult> GetPurchaseOrderItemOptions(int SelectedId)
        {
            return Json(await receiveRepository.GetPurchaseOrderItemOptions(SelectedId));
        }
    }
}