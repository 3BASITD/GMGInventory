using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.Excel;
using GMGInventory.Application.Contracts;
using GMGInventory.Application.Repositories;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GMGInventory.Controllers
{

    [Authorize]
    public class PurchaseOrderController(IAccessRepository accessRepository
                                       , IPurchaseOrderNewRepository orderRepository) : BaseController(accessRepository)
    {
        private readonly ControllerHelper chelper = new();

        public async Task<IActionResult> GetOptions(string search, int page)
        {
            return Json(await orderRepository.GetOptions(search, page));
        }

        public async Task<IActionResult> GetTypeOptions(int? SelectedId)
        {
            return Json(await orderRepository.GetTypeOptions(SelectedId));
        }

        public async Task<IActionResult> GetDeliveryModeOptions(int? SelectedId)
        {
            return Json(await orderRepository.GetDeliveryModeOptions(SelectedId));
        }

        public async Task<IActionResult> GetPurchaseRequestOptions(int RequestTypeId, int? RequestPurposeId, int? RegionId, int? SelectedId)
        {
            return Json(await orderRepository.GetPurchaseRequestOptions(RequestTypeId, RequestPurposeId, RegionId, SelectedId));
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
        public async Task<IActionResult> GetListDTJson(PurchaseOrderFilter filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseOrder, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            var form = new DataTablesRequestForm(Request.Form);
            return Json(await orderRepository.GetListDTAsync(form, filter));
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

        public async Task<IActionResult> PrintDetails(int Id, string? Company)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseOrder, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;

            var details = await orderRepository.GetDetails(Id);

            var items = await orderRepository.GetItemListAsync(Id);

            if (details == null || Company == null)
            {
                return NotFound();
            }

            TempData["CompanyLogo"] = Company;
            ViewData["Items"] = items;
            return View(details);
        }

        public async Task<IActionResult> Create(int? Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseOrder, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            var request = await orderRepository.GetDetails(Id ?? 0);
            if (Id != null && (request == null || request.DisapprovedBy == null || request.Status.ToLower() != "disapproved"))
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
        public async Task<IActionResult> UpdateCostJson(POItemCostVM cost)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseOrder, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            var response = await orderRepository.UpdateCostAsync(cost);
            if (response.Status == "Success") TempData["SuccessMessage"] = response.Message;
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> GetCostUpdateListJson(int OrderItemId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseOrder, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            var response = await orderRepository.GetCostUpdateListAsync(OrderItemId);
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

        public async Task<IActionResult> DownloadFile(PurchaseOrderFilter filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseOrder, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            DataTable dt = new("Purchase Order");
            dt.Columns.AddRange([
                new DataColumn(chelper.DisplayName<PurchaseOrderVM>("DocumentNumber")),
                new DataColumn(chelper.DisplayName<PurchaseOrderVM>("Type")),
                new DataColumn(chelper.DisplayName<PurchaseOrderVM>("Supplier")),
                new DataColumn(chelper.DisplayName<PurchaseOrderVM>("Term")),
                new DataColumn(chelper.DisplayName<PurchaseOrderVM>("DeliveryMode")),
                new DataColumn(chelper.DisplayName<PurchaseOrderVM>("Status")),
                new DataColumn(chelper.DisplayName<PurchaseOrderVM>("OrderDate"))
            ]);
            var orders = await orderRepository.GetListAsync(filter);
            foreach (var order in orders)
            {
                dt.Rows.Add(order.DocumentNumber,
                            order.Type,
                            order.Supplier,
                            order.Term,
                            order.DeliveryMode,
                            order.Status,
                            order.OrderDate);
            }

            using XLWorkbook wb = new();
            wb.Worksheets.Add(dt);
            using MemoryStream stream = new();
            wb.SaveAs(stream);
            return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        DateTime.Now.ToString("yyyyMMdd") + " Purchase Order.xlsx");
        }
    }
}