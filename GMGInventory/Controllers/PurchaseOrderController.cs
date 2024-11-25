using DocumentFormat.OpenXml.Office2010.Excel;
using GMGInventory.Application.Contracts;
using GMGInventory.Application.Repositories;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{

    [Authorize]
    public class PurchaseOrderController(IAccessRepository accessRepository
                                       , IPurchaseOrderNewRepository orderRepository) : BaseController(accessRepository)
    {
        public async Task<IActionResult> GetTypeOptions(int? SelectedId)
        {
            return Json(await orderRepository.GetTypeOptions(SelectedId));
        }

        public async Task<IActionResult> GetDeliveryModeOptions(int? SelectedId)
        {
            return Json(await orderRepository.GetDeliveryModeOptions(SelectedId));
        }

        public async Task<IActionResult> GetPurchaseRequestOptions(int OrderTypeId, int? SelectedId)
        {
            return Json(await orderRepository.GetPurchaseRequestOptions(OrderTypeId, SelectedId));
        }

        public async Task<IActionResult> GetPurchaseRequestItemOptions(int RequestId, int? SelectedId)
        {
            return Json(await orderRepository.GetPurchaseRequestItemOptions(RequestId, SelectedId));
        }

        public async Task<IActionResult> GetNewDocumentNumber(int? TypeId)
        {
            return Json(await orderRepository.GetNewDocumentNumber(TypeId));
        }

        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseOrder, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.PurchaseOrder)).Convert();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetListDTJson()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseOrder, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            var form = new DataTablesRequestForm(Request.Form);
            return Json(await orderRepository.GetListDTAsync(form));
        }

        public async Task<IActionResult> Details(int Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseOrder, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.PurchaseOrder)).Convert();

            var request = await orderRepository.GetDetails(Id);
            if (request == null)
                return RedirectToAction(nameof(Index));

            return View(request);
        }

        public async Task<IActionResult> DetailsJson(int Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseOrder, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            var request = await orderRepository.GetDetails(Id);
            if (request == null)
                return NotFound();

            return Json(request);
        }

        public async Task<IActionResult> PrintDetails(int Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseOrder, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;

            ViewData["Items"] = await orderRepository.GetItemListAsync(Id);

            return View(await orderRepository.GetDetails(Id));
        }

        public async Task<IActionResult> Create(int? Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseOrder, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            var request = await orderRepository.GetDetails(Id ?? 0);
            if (Id != null && (request == null || request.DisapprovedBy == null || request.Status != "Disapproved"))
            {
                return RedirectToAction("Details", new { Id });
            }
            TempData["PurchaseOrderId"] = Id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetItemListJson(int purchaseOrderId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseOrder, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return Json(await orderRepository.GetItemListAsync(purchaseOrderId));
        }

        [HttpPost]
        public async Task<IActionResult> GetItemListDTJson(int purchaseOrderId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseOrder, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            var form = new DataTablesRequestForm(Request.Form);
            return Json(await orderRepository.GetItemListDTAsync(form, purchaseOrderId));
        }

        [HttpPost]
        public async Task<IActionResult> AddJson(PurchaseOrderSaveVM form, List<PurchaseOrderLineItemVM> items)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseOrder, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            var response = await orderRepository.AddAsync(form, items);
            if (response.Status == "Success") TempData["SuccessMessage"] = response.Message;
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> SaveItemsJson(List<PurchaseOrderLineItemVM> items, int Id, string status)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseOrder, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            var response = await orderRepository.SaveItemsAsync(items, Id, status);
            if (response.Status == "Success") TempData["SuccessMessage"] = response.Message;

            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveJson(int Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseOrder, AccessTypes.ApproveDisapprove);
            if (redirectResult != null) return redirectResult;

            var response = await orderRepository.ApproveAsync(Id);
            if (response.Status == "Success") TempData["SuccessMessage"] = response.Message;
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> DisapproveJson(int Id, string? reason)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseOrder, AccessTypes.ApproveDisapprove);
            if (redirectResult != null) return redirectResult;

            var response = await orderRepository.DisapproveAsync(Id, reason);
            if (response.Status == "Success") TempData["SuccessMessage"] = response.Message;
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteJson(int Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseOrder, AccessTypes.Delete);
            if (redirectResult != null) return redirectResult;

            var response = await orderRepository.DeleteAsync(Id);
            if (response.Status == "Success") TempData["SuccessMessage"] = response.Message;
            return Json(response);
        }
    }
}