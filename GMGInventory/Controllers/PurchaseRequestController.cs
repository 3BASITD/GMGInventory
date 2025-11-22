using ClosedXML.Excel;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class PurchaseRequestController(IAccessRepository accessRepository
                                         , IPurchaseRequestRepository requestRepository) : BaseController(accessRepository)
    {
        private readonly ControllerHelper chelper = new();

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
        public async Task<IActionResult> GetListDTJson(PurchaseRequestFilter filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseRequest, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            var form = new DataTablesRequestForm(Request.Form);
            return Json(await requestRepository.GetListDTAsync(form, filter));
        }

        public async Task<IActionResult> Details(int Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseRequest, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.PurchaseRequest)).Convert();
            ViewData["UserAccessAutomotive"] = new ViewsAccessData(await GetAccessAsync(PageNames.PurchaseRequestAutomotive)).Convert();

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

        public async Task<IActionResult> PrintDetails(int Id, string? Company)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseRequest, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;

            var details = await requestRepository.GetDetails(Id);
            var items = await requestRepository.GetItemListAsync(Id);

            if (details == null || Company == null)
            {
                return NotFound();
            }

            TempData["CompanyLogo"] = Company;
            ViewData["Items"] = items.OrderBy(s => s.LineNumber).ToList();
            return View(details);
        }

        public async Task<IActionResult> Create(int? Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseRequest, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            var request = await requestRepository.GetDetails(Id ?? 0);
            if (Id != null && (request == null || request.DisapprovedBy == null || request.Status.ToLower() != "disapproved"))
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
        public async Task<IActionResult> GetSalesOrderItems(int SalesOrderId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseRequest, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return Json(await requestRepository.GetSalesOrderItems(SalesOrderId));
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
        public async Task<IActionResult> ApproveJson(int Id, int? approverNumber = null)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseRequest, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            var response = await requestRepository.ApproveAsync(Id, approverNumber);
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

        public async Task<IActionResult> DeleteJson(int Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseRequest, AccessTypes.Delete);
            if (redirectResult != null) return redirectResult;

            var response = await requestRepository.DeleteAsync(Id);
            if (response.Status == "Success") TempData["SuccessMessage"] = response.Message;
            return Json(response);
        }

        public async Task<IActionResult> DownloadFile(PurchaseRequestFilter filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseRequest, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            DataTable dt = new("Purchase Request");
            dt.Columns.AddRange([
                new DataColumn(chelper.DisplayName<PurchaseRequestVM>("DocumentNumber")),
                new DataColumn(chelper.DisplayName<PurchaseRequestVM>("Type")),
                new DataColumn(chelper.DisplayName<PurchaseRequestVM>("Purpose")),
                new DataColumn(chelper.DisplayName<PurchaseRequestVM>("Region")),
                new DataColumn(chelper.DisplayName<PurchaseRequestVM>("Status")),
                new DataColumn(chelper.DisplayName<PurchaseRequestVM>("RequestDate")),
                new DataColumn(chelper.DisplayName<PurchaseRequestVM>("Remarks"))
            ]);
            var requests = await requestRepository.GetListAsync(filter);
            foreach (var request in requests)
            {
                dt.Rows.Add(request.DocumentNumber,
                            request.Type,
                            request.Purpose,
                            request.Region,
                            request.Status,
                            request.RequestDate,
                            request.Remarks);
            }

            using XLWorkbook wb = new();
            wb.Worksheets.Add(dt);
            using MemoryStream stream = new();
            wb.SaveAs(stream);
            return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        DateTime.Now.ToString("yyyyMMdd") + " Purchase Request.xlsx");
        }
    }
}