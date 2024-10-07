using ClosedXML.Excel;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GMGInventory.Controllers
{
    public class InventoryReportController(IAccessRepository accessRepository
                                         , IReportInventoryRepository reportInventoryRepository) : BaseController(accessRepository)
    {
        private readonly ControllerHelper chelper = new();

        #region Items Below Maintaining Quantity

        public async Task<IActionResult> BelowMaintainingItem()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.BelowMaintainingItemsReport, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.BelowMaintainingItemsReport)).Convert();

            return View();
        }

        public async Task<IActionResult> PrintBelowMaintaining(ListFilterInventoryModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.BelowMaintainingItemsReport, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;

            return View(await reportInventoryRepository.PrintBelowMaintaining(filter));
        }

        [HttpPost]
        public async Task<IActionResult> GetBelowMaintainingDT(ListFilterInventoryModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.BelowMaintainingItemsReport, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm dtForm = new(Request.Form);
            return Json(await reportInventoryRepository.GetBelowMaintainingDT(dtForm, filter));
        }

        public async Task<IActionResult> DownloadBelowMaintaining(ListFilterInventoryModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.BelowMaintainingItemsReport, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            DataTable dt = new("Below Maintaining Quantity");
            dt.Columns.AddRange([
                new DataColumn(chelper.DisplayName<BelowMaintainingRVM>("Brand")),
                new DataColumn(chelper.DisplayName<BelowMaintainingRVM>("ItemCode")),
                new DataColumn(chelper.DisplayName<BelowMaintainingRVM>("ItemName")),
                new DataColumn(chelper.DisplayName<BelowMaintainingRVM>("PartNumber")),
                new DataColumn(chelper.DisplayName<BelowMaintainingRVM>("Unit")),
                new DataColumn(chelper.DisplayName<BelowMaintainingRVM>("MaintainingQuantity")),
                new DataColumn(chelper.DisplayName<BelowMaintainingRVM>("QuantityOnHand"))
            ]);
            var items = await reportInventoryRepository.GetBelowMaintaining(filter);
            foreach (var item in items)
            {
                dt.Rows.Add(item.Brand,
                            item.ItemCode,
                            item.ItemName,
                            item.PartNumber,
                            item.Unit,
                            item.MaintainingQuantity,
                            item.QuantityOnHand);
            }

            using XLWorkbook wb = new();
            wb.Worksheets.Add(dt);
            using MemoryStream stream = new();
            wb.SaveAs(stream);
            return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        DateTime.Now.ToString("yyyyMMdd") + " Items Below Maintaining Quantity.xlsx");
        }

        #endregion Items Below Maintaining Quantity

        #region Receiving Listing

        public async Task<IActionResult> ReceivingListing()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ReceivingListingReport, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.ReceivingListingReport)).Convert();

            return View();
        }

        public async Task<IActionResult> PrintReceiving(ListFilterInventoryModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ReceivingListingReport, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;

            return View(await reportInventoryRepository.PrintReceiving(filter));
        }

        [HttpPost]
        public async Task<IActionResult> GetReceivingDT(ListFilterInventoryModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ReceivingListingReport, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm dtForm = new(Request.Form);
            return Json(await reportInventoryRepository.GetReceivingDT(dtForm, filter));
        }

        public async Task<IActionResult> DownloadReceiving(ListFilterInventoryModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ReceivingListingReport, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            DataTable dt = new("Receiving Listing "
                             + filter.DateFrom.GetValueOrDefault().ToString("MMddyy")
                             + "-"
                             + filter.DateTo.GetValueOrDefault().ToString("MMddyy"));
            dt.Columns.AddRange([
                new DataColumn(chelper.DisplayName<ReceivingRVM>("Brand")),
                new DataColumn(chelper.DisplayName<ReceivingRVM>("ItemType")),
                new DataColumn(chelper.DisplayName<ReceivingRVM>("ItemCode")),
                new DataColumn(chelper.DisplayName<ReceivingRVM>("ItemName")),
                new DataColumn(chelper.DisplayName<ReceivingRVM>("PartNumber")),
                new DataColumn(chelper.DisplayName<ReceivingRVM>("Unit")),
                new DataColumn(chelper.DisplayName<ReceivingRVM>("DocumentNumber")),
                new DataColumn(chelper.DisplayName<ReceivingRVM>("Date")),
                new DataColumn(chelper.DisplayName<ReceivingRVM>("Quantity"))
            ]);
            var receivings = await reportInventoryRepository.GetReceiving(filter);
            foreach (var receiving in receivings)
            {
                dt.Rows.Add(receiving.Brand,
                            receiving.ItemType,
                            receiving.ItemCode,
                            receiving.ItemName,
                            receiving.PartNumber,
                            receiving.Unit,
                            receiving.DocumentNumber,
                            receiving.Date,
                            receiving.Quantity);
            }

            using XLWorkbook wb = new();
            wb.Worksheets.Add(dt);
            using MemoryStream stream = new();
            wb.SaveAs(stream);
            return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        DateTime.Now.ToString("yyyyMMdd") + " Receiving Listing.xlsx");
        }

        #endregion Receiving Listing

        #region Receiving Listing by Supplier

        public async Task<IActionResult> ReceivingListingSupplier()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ReceivingListingBySupplierReport, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.ReceivingListingBySupplierReport)).Convert();

            return View();
        }

        public async Task<IActionResult> PrintReceivingSupplier(ListFilterInventoryModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ReceivingListingBySupplierReport, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;

            return View(await reportInventoryRepository.PrintReceivingSupplier(filter));
        }

        [HttpPost]
        public async Task<IActionResult> GetReceivingSupplierDT(ListFilterInventoryModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ReceivingListingBySupplierReport, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm dtForm = new(Request.Form);
            return Json(await reportInventoryRepository.GetReceivingSupplierDT(dtForm, filter));
        }

        public async Task<IActionResult> DownloadReceivingSupplier(ListFilterInventoryModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ReceivingListingBySupplierReport, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTable dt = new("RL by Supplier "
                             + filter.DateFrom.GetValueOrDefault().ToString("MMddyy")
                             + "-"
                             + filter.DateTo.GetValueOrDefault().ToString("MMddyy"));
            dt.Columns.AddRange([
                new DataColumn(chelper.DisplayName<ReceivingSupplierRVM>("Supplier")),
                new DataColumn(chelper.DisplayName<ReceivingSupplierRVM>("Date")),
                new DataColumn(chelper.DisplayName<ReceivingSupplierRVM>("DocumentNumber")),
                new DataColumn(chelper.DisplayName<ReceivingSupplierRVM>("Item")),
                new DataColumn(chelper.DisplayName<ReceivingSupplierRVM>("Quantity")),
                new DataColumn(chelper.DisplayName<ReceivingSupplierRVM>("Unit")),
                new DataColumn(chelper.DisplayName<ReceivingSupplierRVM>("UnitCost")),
                new DataColumn(chelper.DisplayName<ReceivingSupplierRVM>("Amount"))
            ]);
            var receivings = await reportInventoryRepository.GetReceivingSupplier(filter);
            foreach (var receiving in receivings)
            {
                dt.Rows.Add(receiving.Supplier,
                            receiving.Date,
                            receiving.DocumentNumber,
                            receiving.Item,
                            receiving.Quantity,
                            receiving.Unit,
                            receiving.UnitCost,
                            receiving.Amount);
            }

            using XLWorkbook wb = new();
            wb.Worksheets.Add(dt);
            using MemoryStream stream = new();
            wb.SaveAs(stream);
            return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        DateTime.Now.ToString("yyyyMMdd") + " Receiving Listing by Supplier.xlsx");
        }

        #endregion Receiving Listing by Supplier

        #region Receiving Summary

        public async Task<IActionResult> ReceivingSummary()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ReceivingSummaryReport, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.ReceivingSummaryReport)).Convert();

            return View();
        }

        public async Task<IActionResult> PrintReceivingSummary(ListFilterInventoryModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ReceivingSummaryReport, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;

            return View(await reportInventoryRepository.PrintReceivingSummary(filter));
        }

        [HttpPost]
        public async Task<IActionResult> GetReceivingSummaryDT(ListFilterInventoryModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ReceivingSummaryReport, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm dtForm = new(Request.Form);
            return Json(await reportInventoryRepository.GetReceivingSummaryDT(dtForm, filter));
        }

        public async Task<IActionResult> DownloadReceivingSummary(ListFilterInventoryModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ReceivingSummaryReport, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            DataTable dt = new("Receiving Summary "
                             + filter.DateFrom.GetValueOrDefault().ToString("MMddyy")
                             + "-"
                             + filter.DateTo.GetValueOrDefault().ToString("MMddyy"));
            dt.Columns.AddRange([
                new DataColumn(chelper.DisplayName<ReceivingSummaryRVM>("Brand")),
                new DataColumn(chelper.DisplayName<ReceivingSummaryRVM>("ItemType")),
                new DataColumn(chelper.DisplayName<ReceivingSummaryRVM>("ItemCode")),
                new DataColumn(chelper.DisplayName<ReceivingSummaryRVM>("ItemName")),
                new DataColumn(chelper.DisplayName<ReceivingSummaryRVM>("PartNumber")),
                new DataColumn(chelper.DisplayName<ReceivingSummaryRVM>("Unit")),
                new DataColumn(chelper.DisplayName<ReceivingSummaryRVM>("Quantity"))
            ]);
            var receivings = await reportInventoryRepository.GetReceivingSummary(filter);
            foreach (var receiving in receivings)
            {
                dt.Rows.Add(receiving.Brand,
                            receiving.ItemType,
                            receiving.ItemCode,
                            receiving.ItemName,
                            receiving.PartNumber,
                            receiving.Unit,
                            receiving.Quantity);
            }

            using XLWorkbook wb = new();
            wb.Worksheets.Add(dt);
            using MemoryStream stream = new();
            wb.SaveAs(stream);
            return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        DateTime.Now.ToString("yyyyMMdd") + " Receiving Summary.xlsx");
        }

        #endregion Receiving Summary

        #region Stock Card Report

        public async Task<IActionResult> StockCard()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.StockCardReport, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.StockCardReport)).Convert();

            return View();
        }

        public async Task<IActionResult> PrintStockCard(ListFilterInventoryModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.StockCardReport, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;

            return View(await reportInventoryRepository.PrintStockCard(filter));
        }

        [HttpPost]
        public async Task<IActionResult> GetStockCardDT(ListFilterInventoryModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.StockCardReport, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm dtForm = new(Request.Form);
            return Json(await reportInventoryRepository.GetStockCardDT(dtForm, filter));
        }

        public async Task<IActionResult> DownloadStockCard(ListFilterInventoryModel filter)
        {
            DataTable dt = new("Stock Card "
                             + filter.DateFrom.GetValueOrDefault().ToString("MMddyy")
                             + "-"
                             + filter.DateTo.GetValueOrDefault().ToString("MMddyy"));
            dt.Columns.AddRange([
                new DataColumn(chelper.DisplayName<StockCardRVM>("Item")),
                new DataColumn(chelper.DisplayName<StockCardRVM>("TransactionType")),
                new DataColumn(chelper.DisplayName<StockCardRVM>("DetailsRemarks")),
                new DataColumn(chelper.DisplayName<StockCardRVM>("Date")),
                new DataColumn(chelper.DisplayName<StockCardRVM>("In")),
                new DataColumn(chelper.DisplayName<StockCardRVM>("Out"))
            ]);
            var stocks = await reportInventoryRepository.GetStockCard(filter);
            foreach (var stock in stocks)
            {
                dt.Rows.Add(stock.Item,
                            stock.TransactionType,
                            stock.DetailsRemarks,
                            stock.Date,
                            stock.In,
                            stock.Out);
            }

            using XLWorkbook wb = new();
            wb.Worksheets.Add(dt);
            using MemoryStream stream = new();
            wb.SaveAs(stream);
            return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        DateTime.Now.ToString("yyyyMMdd") + " Stock Card.xlsx");
        }

        #endregion Stock Card Report

        #region Stock Status

        public async Task<IActionResult> StockStatus()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.StockStatusReport, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.StockStatusReport)).Convert();

            return View();
        }

        public async Task<IActionResult> PrintStockStatus(ListFilterInventoryModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.StockStatusReport, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;

            return View(await reportInventoryRepository.PrintStockStatus(filter));
        }

        [HttpPost]
        public async Task<IActionResult> GetStockStatusDT(ListFilterInventoryModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.StockStatusReport, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm dtForm = new(Request.Form);
            return Json(await reportInventoryRepository.GetStockStatusDT(dtForm, filter));
        }

        public async Task<IActionResult> DownloadStockStatus(ListFilterInventoryModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.StockStatusReport, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            DataTable dt = new("Stock Status");
            dt.Columns.AddRange([
                new DataColumn(chelper.DisplayName<StockStatusRVM>("Brand")),
                new DataColumn(chelper.DisplayName<StockStatusRVM>("ItemType")),
                new DataColumn(chelper.DisplayName<StockStatusRVM>("ItemCode")),
                new DataColumn(chelper.DisplayName<StockStatusRVM>("ItemName")),
                new DataColumn(chelper.DisplayName<StockStatusRVM>("PartNumber")),
                new DataColumn(chelper.DisplayName<StockStatusRVM>("Unit")),
                new DataColumn(chelper.DisplayName<StockStatusRVM>("QuantityOnHand")),
                new DataColumn(chelper.DisplayName<StockStatusRVM>("UnitCost")),
                new DataColumn(chelper.DisplayName<StockStatusRVM>("TotalCost"))
            ]);
            var stocks = await reportInventoryRepository.GetStockStatus(filter);
            foreach (var stock in stocks)
            {
                dt.Rows.Add(stock.Brand,
                            stock.ItemType,
                            stock.ItemCode,
                            stock.ItemName,
                            stock.PartNumber,
                            stock.Unit,
                            stock.QuantityOnHand,
                            stock.UnitCost,
                            stock.TotalCost);
            }

            using XLWorkbook wb = new();
            wb.Worksheets.Add(dt);
            using MemoryStream stream = new();
            wb.SaveAs(stream);
            return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        DateTime.Now.ToString("yyyyMMdd") + " Stock Status.xlsx");
        }

        #endregion Stock Status

        #region Supplier Purchase History

        public async Task<IActionResult> SupplierPurchase()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SupplierPurchaseHistoryReport, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.SupplierPurchaseHistoryReport)).Convert();

            return View();
        }

        public async Task<IActionResult> PrintSupplierPurchase(ListFilterInventoryModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SupplierPurchaseHistoryReport, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;

            return View(await reportInventoryRepository.PrintSupplierPurchase(filter));
        }

        [HttpPost]
        public async Task<IActionResult> GetSupplierPurchaseDT(ListFilterInventoryModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SupplierPurchaseHistoryReport, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm dtForm = new(Request.Form);
            return Json(await reportInventoryRepository.GetSupplierPurchaseDT(dtForm, filter));
        }

        public async Task<IActionResult> DownloadSupplierPurchase(ListFilterInventoryModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SupplierPurchaseHistoryReport, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            DataTable dt = new("Supplier Purchase "
                             + filter.DateFrom.GetValueOrDefault().ToString("MMddyy")
                             + "-"
                             + filter.DateTo.GetValueOrDefault().ToString("MMddyy"));
            dt.Columns.AddRange([
                new DataColumn(chelper.DisplayName<SupplierPurchaseRVM>("PurchaseDate")),
                new DataColumn(chelper.DisplayName<SupplierPurchaseRVM>("Supplier")),
                new DataColumn(chelper.DisplayName<SupplierPurchaseRVM>("Brand")),
                new DataColumn(chelper.DisplayName<SupplierPurchaseRVM>("ItemCode")),
                new DataColumn(chelper.DisplayName<SupplierPurchaseRVM>("ItemName")),
                new DataColumn(chelper.DisplayName<SupplierPurchaseRVM>("Unit")),
                new DataColumn(chelper.DisplayName<SupplierPurchaseRVM>("Quantity")),
                new DataColumn(chelper.DisplayName<SupplierPurchaseRVM>("Currency")),
                new DataColumn(chelper.DisplayName<SupplierPurchaseRVM>("UnitPrice")),
                new DataColumn(chelper.DisplayName<SupplierPurchaseRVM>("TotalPrice"))
            ]);
            var purchases = await reportInventoryRepository.GetSupplierPurchase(filter);
            foreach (var purchase in purchases)
            {
                dt.Rows.Add(purchase.PurchaseDate,
                            purchase.Supplier,
                            purchase.Brand,
                            purchase.ItemCode,
                            purchase.ItemName,
                            purchase.Unit,
                            purchase.Quantity,
                            purchase.Currency,
                            purchase.UnitPrice,
                            purchase.TotalPrice);
            }

            using XLWorkbook wb = new();
            wb.Worksheets.Add(dt);
            using MemoryStream stream = new();
            wb.SaveAs(stream);
            return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        DateTime.Now.ToString("yyyyMMdd") + " Supplier Purchase History.xlsx");
        }

        #endregion Supplier Purchase History
    }
}