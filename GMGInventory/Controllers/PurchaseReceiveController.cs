using ClosedXML.Excel;
using GMGInventory.Application.Contracts;
using GMGInventory.Application.Services;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Data;

namespace GMGInventory.Controllers
{
    [Authorize] 
    public class PurchaseReceiveController(IAccessRepository accessRepository
                                         , IPurchaseReceiveRepository receiveRepository
                                         , IAPIHM_Service apiHMService
                                         , IOptions<APIHMSettings> apiHMOptions) : BaseController(accessRepository)
    {
        private readonly ControllerHelper chelper = new();

        public async Task<IActionResult> GetPurchaseOrderOptions(int? SelectedId)
        {
            return Json(await receiveRepository.GetPurchaseOrderOptions(SelectedId));
        }
        public async Task<IActionResult> GetPurchaseOrderOptions2(string search, int page)
        {
            return Json(await receiveRepository.GetPurchaseOrderOptions2(search, page));
        }

        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseReceiving, AccessTypes.View);
            var redirectResultBasic = await CheckAccessAndRedirectAsync(PageNames.PurchaseReceivingBasic, AccessTypes.View);
            if (redirectResult != null && redirectResultBasic != null) return redirectResultBasic;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.PurchaseReceiving)).Convert();
            ViewData["HashMicroUserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.HashMicroBill)).Convert();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetListDTJson(PurchaseReceiveFilter filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseReceiving, AccessTypes.View);
            var redirectResultBasic = await CheckAccessAndRedirectAsync(PageNames.PurchaseReceivingBasic, AccessTypes.View);
            if (redirectResult != null && redirectResultBasic != null) return redirectResultBasic;

            var form = new DataTablesRequestForm(Request.Form);
            return Json(await receiveRepository.GetListDTAsync(form, filter));
        }

        public async Task<IActionResult> Details(int Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseReceiving, AccessTypes.View);
            var redirectResultBasic = await CheckAccessAndRedirectAsync(PageNames.PurchaseReceivingBasic, AccessTypes.View);
            if (redirectResult != null && redirectResultBasic != null) return redirectResultBasic;


            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.PurchaseReceiving)).Convert();
            ViewData["UserAccessBasic"] = new ViewsAccessData(await GetAccessAsync(PageNames.PurchaseReceivingBasic)).Convert();
            ViewData["HashMicroUserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.HashMicroBill)).Convert();

            var receive = await receiveRepository.GetDetails(Id);
            if (receive == null)
                return RedirectToAction(nameof(Index));

            return View(receive);
        }

        public async Task<IActionResult> DetailsJson(int Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseReceiving, AccessTypes.View);
            var redirectResultBasic = await CheckAccessAndRedirectAsync(PageNames.PurchaseReceivingBasic, AccessTypes.View);
            if (redirectResult != null && redirectResultBasic != null) return redirectResultBasic;

            var receive = await receiveRepository.GetDetails(Id);
            if (receive == null)
                return NotFound();

            return Json(receive);
        }

        [HttpPost]
        public async Task<IActionResult> GetItemListJson(int purchaseReceiveId)
        {
            return Json(await receiveRepository.GetWithPOItemsListAsync(purchaseReceiveId));
        }

        [HttpPost]
        public async Task<IActionResult> GetItemsFromPOListJson(int purchaseOrderId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseOrder, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return Json(await receiveRepository.GetItemsFromPOListAsync(purchaseOrderId));
        }


        //[HttpPost]
        //public async Task<IActionResult> GetItemListForReturnJson(int purchaseReceiveId)
        //{
        //    return Json(await receiveRepository.GetItemListAsync(purchaseReceiveId));
        //}

        [HttpPost]
        public async Task<IActionResult> SaveItemsJson(List<PurchaseReceiveLineItemVM> items, int Id, string status)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseReceiving, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            var response = await receiveRepository.SaveItemsAsync(items, Id, status);
            if (response.Status == "Success") TempData["SuccessMessage"] = response.Message;

            return Json(response);
        }

        public async Task<IActionResult> PrintDetails(int Id, string? Company)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseReceiving, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccessBasic"] = new ViewsAccessData(await GetAccessAsync(PageNames.PurchaseReceivingBasic)).Convert();

            var details = await receiveRepository.GetDetails(Id);

            var items = await receiveRepository.GetItemListAsync(Id);

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
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseReceiving, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            var request = await receiveRepository.GetDetails(Id ?? 0);
            if (Id != null && (request == null || request.DisapprovedBy == null || request.Status != "DISAPPROVED"))
            {
                return RedirectToAction("Details", new { Id });
            }
            TempData["PurchaseReceivingId"] = Id;
            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.PurchaseReceiving)).Convert();
            ViewData["UserAccessBasic"] = new ViewsAccessData(await GetAccessAsync(PageNames.PurchaseReceivingBasic)).Convert();

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

        public async Task<IActionResult> DownloadFile(PurchaseReceiveFilter filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseReceiving, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            DataTable dt = new("Purchase Receive");
            dt.Columns.AddRange([
                new DataColumn(chelper.DisplayName<PurchaseReceiveVM>("DocumentNumber")),
                new DataColumn(chelper.DisplayName<PurchaseReceiveVM>("Supplier")),
                new DataColumn(chelper.DisplayName<PurchaseReceiveVM>("Term")),
                new DataColumn(chelper.DisplayName<PurchaseReceiveVM>("PONumber")),
                new DataColumn(chelper.DisplayName<PurchaseReceiveVM>("Warehouse")),
                new DataColumn(chelper.DisplayName<PurchaseReceiveVM>("Status")),
                new DataColumn(chelper.DisplayName<PurchaseReceiveVM>("ReceiveDate")),
                new DataColumn(chelper.DisplayName<PurchaseReceiveVM>("Remarks"))
            ]);
            var receives = await receiveRepository.GetListAsync(filter);
            foreach (var receive in receives)
            {
                dt.Rows.Add(receive.DocumentNumber,
                            receive.Supplier,
                            receive.Term,
                            receive.PONumber,
                            receive.Warehouse,
                            receive.Status,
                            receive.ReceiveDate,
                            receive.Remarks);
            }

            using XLWorkbook wb = new();
            wb.Worksheets.Add(dt);
            using MemoryStream stream = new();
            wb.SaveAs(stream);
            return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        DateTime.Now.ToString("yyyyMMdd") + " Purchase Receive.xlsx");
        }

        public async Task<IActionResult> MoveToHashMicro(int? Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.HashMicroBill, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            return Json(await apiHMService.MovePurchaseOrder(Id, apiHMOptions.Value.Host, apiHMOptions.Value.User, apiHMOptions.Value.Password));
        }

        public async Task<IActionResult> MoveAllToHashMicro(DateTime DateFrom, DateTime DateTo)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.HashMicroBill, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            return Json(await apiHMService.MoveAllPostedPurchaseOrder(DateFrom, DateTo, apiHMOptions.Value.Host, apiHMOptions.Value.User, apiHMOptions.Value.Password));
        }
    }
}