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
    public class PurchaseReturnNewController(IAccessRepository accessRepository
                                           , IPurchaseReturnNewRepository returnRepository) : BaseController(accessRepository)
    {
        private readonly ControllerHelper chelper = new();

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
        public async Task<IActionResult> GetListDTJson(PurchaseReturnFilter filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseReturnNew, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            var form = new DataTablesRequestForm(Request.Form);
            return Json(await returnRepository.GetListDTAsync(form, filter));
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

        public async Task<IActionResult> PrintDetails(int Id, string? Company)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseReturnNew, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;

            var details = await returnRepository.GetDetails(Id);

            var items = await returnRepository.GetItemListAsync(Id);

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
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseReturnNew, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            var request = await returnRepository.GetDetails(Id ?? 0);
            if (Id != null && (request == null || request.DisapprovedBy == null || request.Status.ToLower() != "disapproved"))
            {
                return RedirectToAction("Details", new { Id });
            }
            TempData["PurchaseReturnId"] = Id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetItemListJson(int purchaseReturnId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseReturnNew, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return Json(await returnRepository.GetWithRRItemsListAsync(purchaseReturnId));
        }

        [HttpPost]
        public async Task<IActionResult> GetItemsFromRRListJson(int purchaseReceiveId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseOrder, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return Json(await returnRepository.GetItemsFromRRListAsync(purchaseReceiveId));
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

        public async Task<IActionResult> DownloadFile(PurchaseReturnFilter filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseReturn, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            DataTable dt = new("Purchase Return");
            dt.Columns.AddRange([
                new DataColumn(chelper.DisplayName<PurchaseReturnVM>("DocumentNumber")),
                new DataColumn(chelper.DisplayName<PurchaseReturnVM>("PONumber")),
                new DataColumn(chelper.DisplayName<PurchaseReturnVM>("RRNumber")),
                new DataColumn(chelper.DisplayName<PurchaseReturnVM>("Supplier")),
                new DataColumn(chelper.DisplayName<PurchaseReturnVM>("Warehouse")),
                new DataColumn(chelper.DisplayName<PurchaseReturnVM>("DRNumber")),
                new DataColumn(chelper.DisplayName<PurchaseReturnVM>("SINumber")),
                new DataColumn(chelper.DisplayName<PurchaseReturnVM>("Status")),
                new DataColumn(chelper.DisplayName<PurchaseReturnVM>("ReturnDate"))
            ]);
            var returns = await returnRepository.GetListAsync(filter);
            foreach (var rturn in returns)
            {
                dt.Rows.Add(rturn.DocumentNumber,
                            rturn.PONumber,
                            rturn.RRNumber,
                            rturn.Supplier,
                            rturn.Warehouse,
                            rturn.DRNumber,
                            rturn.SINumber,
                            rturn.Status,
                            rturn.ReturnDate);
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