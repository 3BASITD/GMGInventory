using DocumentFormat.OpenXml.Spreadsheet;
using GMGInventory.Application.Contracts;
using GMGInventory.Application.Repositories;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class PurchaseRequestController(IAccessRepository accessRepository
                                         , IPurchaseRequestRepository requestRepository) : BaseController(accessRepository)
    {

        public async Task<IActionResult> GetTypeOptions(int? SelectedId)
        {
            return Json(await requestRepository.GetTypeOptions(SelectedId));
        }

        public async Task<IActionResult> GetPurposeOptions(int TypeId, int? SelectedId)
        {
            return Json(await requestRepository.GetPurposeOptions(TypeId, SelectedId));
        }

        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseRequest, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.PurchaseRequest)).Convert();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetListDTJson()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseRequest, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            var form = new DataTablesRequestForm(Request.Form);
            return Json(await requestRepository.GetListDTAsync(form));
        }

        public async Task<IActionResult> Details(int Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseRequest, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.PurchaseRequest)).Convert();

            var request = await requestRepository.GetDetails(Id);
            if (request == null)
                return RedirectToAction(nameof(Index));

            return View(request);
        }

        public async Task<IActionResult> DetailsJson(int Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseRequest, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            var request = await requestRepository.GetDetails(Id);
            if (request == null)
                return NotFound();

            return Json(request);
        }

        public async Task<IActionResult> GetPurchaseRequestType(int PurchaseOrderId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseRequest, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            var requestType = await requestRepository.GetPurchaseRequestType(PurchaseOrderId);
            if (requestType == null)
                return NotFound();

            return Json(requestType);
        }
        public async Task<IActionResult> PrintDetails(int Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseRequest, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;

            ViewData["Items"] = await requestRepository.GetItemListAsync(Id);

            return View(await requestRepository.GetDetails(Id));
        }

        public async Task<IActionResult> Create(int? Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseRequest, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            var request = await requestRepository.GetDetails(Id ?? 0);
            if (Id != null && (request == null || request.DisapprovedBy == null || request.Status != "Disapproved"))
            {
                return RedirectToAction("Details", new { Id });
            }
            TempData["PurchaseRequestId"] = Id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetItemListJson(int purchaseRequestId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseRequest, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return Json(await requestRepository.GetItemListAsync(purchaseRequestId));
        }

        [HttpPost]
        public async Task<IActionResult> GetItemListDTJson(int purchaseRequestId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseRequest, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            var form = new DataTablesRequestForm(Request.Form);
            return Json(await requestRepository.GetItemListDTAsync(form, purchaseRequestId));
        }

        [HttpPost]
        public async Task<IActionResult> AddJson(PurchaseRequestSaveVM form, List<PurchaseRequestLineItemVM> items)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseRequest, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            var response = await requestRepository.AddAsync(form, items);
            if (response.Status == "Success") TempData["SuccessMessage"] = response.Message;
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> SaveItemsJson(List<PurchaseRequestLineItemVM> items, int Id, string status)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseRequest, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            var response = await requestRepository.SaveItemsAsync(items, Id, status);
            if (response.Status == "Success") TempData["SuccessMessage"] = response.Message;

            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveJson(int Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseRequest, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            var response = await requestRepository.ApproveAsync(Id);
            if (response.Status == "Success") TempData["SuccessMessage"] = response.Message;
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> DisapproveJson(int Id, string? reason)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseRequest, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            var response = await requestRepository.DisapproveAsync(Id, reason);
            if (response.Status == "Success") TempData["SuccessMessage"] = response.Message;
            return Json(response);
        }
    }
}