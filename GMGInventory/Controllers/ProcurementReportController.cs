using ClosedXML.Excel;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GMGInventory.Controllers
{
    public class ProcurementReportController(IAccessRepository accessRepository
                            , IReportProcurementRepository reportProcurementRepository) : BaseController(accessRepository)
    {
        private readonly ControllerHelper chelper = new();

        #region Purchase Order
        public async Task<IActionResult> PurchaseOrder()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseOrder, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.PurchaseOrderReport)).Convert();

            return View();
        }

        public async Task<IActionResult> PrintPurchaseOrder(ListFilterPurchaseOrderModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseOrderReport, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;

            return View(await reportProcurementRepository.PrintPurchaseOrder(filter));
        }

        [HttpPost]
        public async Task<IActionResult> GetPurchaseOrderDT(ListFilterPurchaseOrderModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseOrderReport, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
            return Json(await reportProcurementRepository.GetPurchaseOrderDT(dtForm, filter));
        }

        public async Task<IActionResult> DownloadPurchaseOrder(ListFilterPurchaseOrderModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseOrderReport, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            DataTable dt = new DataTable("Purchase Order");
            dt.Columns.AddRange(new DataColumn[9] {
                new DataColumn(chelper.DisplayName<PurchaseOrderRVM>("OrderDate")),
                new DataColumn(chelper.DisplayName<PurchaseOrderRVM>("DocumentNumber")),
                new DataColumn(chelper.DisplayName<PurchaseOrderRVM>("Supplier")),
                new DataColumn(chelper.DisplayName<PurchaseOrderRVM>("Item")),
                new DataColumn(chelper.DisplayName<PurchaseOrderRVM>("Quantity")),
                new DataColumn(chelper.DisplayName<PurchaseOrderRVM>("Unit")),
                new DataColumn(chelper.DisplayName<PurchaseOrderRVM>("UnitCost")),
                new DataColumn(chelper.DisplayName<PurchaseOrderRVM>("UnservedQuantity")),
                new DataColumn(chelper.DisplayName<PurchaseOrderRVM>("UnservedAmount"))

            });
            var pos = await reportProcurementRepository.GetPurchaseOrder(filter);
            if (filter.GroupBy != null)
            {
                foreach (var grp in pos.GroupBy(x => filter.GroupBy == "item" ? x.Item :
                                                     filter.GroupBy == "supplier" ? x.Supplier : x.OrderDate))
                {
                    dt.Rows.Add(grp.Key, "", "", "", "", "", "", "", "");

                    foreach (var po in pos.Where(x => filter.GroupBy == "item" ? x.Item == grp.FirstOrDefault()!.Item :
                                       filter.GroupBy == "supplier" ? x.Supplier == grp.FirstOrDefault()!.Supplier :
                                       x.OrderDate == grp.FirstOrDefault()!.OrderDate))
                    {

                        dt.Rows.Add(po.OrderDate,
                                    po.DocumentNumber,
                                    po.Supplier,
                                    po.Item,
                                    po.Quantity,
                                    po.Unit,
                                    po.UnitCost,
                                    po.UnservedQuantity,
                                    po.UnservedAmount);
                    }

                }
            }
            else
            {
                foreach (var po in pos)
                {
                    dt.Rows.Add(po.OrderDate,
                                po.DocumentNumber,
                                po.Supplier,
                                po.Item,
                                po.Quantity,
                                po.Unit,
                                po.UnitCost,
                                po.UnservedQuantity,
                                po.UnservedAmount);
                }
            }


            using (XLWorkbook wb = new())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        filter.DateFrom.GetValueOrDefault().ToString("MMddyy") + "-"
                        + filter.DateTo.GetValueOrDefault().ToString("MMddyy") + " Purchase Order.xlsx");
                }
            }
        }
        #endregion

        #region Purchase Return
        public async Task<IActionResult> PurchaseReturn()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseReturn, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.PurchaseReturnReport)).Convert();

            return View();
        }

        public async Task<IActionResult> PrintPurchaseReturn(ListFilterReturnReceivingModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseReturnReport, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;

            return View(await reportProcurementRepository.PrintPurchaseReturn(filter));
        }

        [HttpPost]
        public async Task<IActionResult> GetPurchaseReturnDT(ListFilterReturnReceivingModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseReturnReport, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
            return Json(await reportProcurementRepository.GetPurchaseReturnDT(dtForm, filter));
        }

        public async Task<IActionResult> DownloadPurchaseReturn(ListFilterReturnReceivingModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseReturnReport, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            DataTable dt = new DataTable("Purchase Return");
            dt.Columns.AddRange(new DataColumn[11] {
                new DataColumn(chelper.DisplayName<PurchaseReturnRVM>("ReturnDate")),
                new DataColumn(chelper.DisplayName<PurchaseReturnRVM>("DocumentNumber")),
                new DataColumn(chelper.DisplayName<PurchaseReturnRVM>("Supplier")),
                new DataColumn(chelper.DisplayName<PurchaseReturnRVM>("Item")),
                new DataColumn(chelper.DisplayName<PurchaseReturnRVM>("ItemDescription")),
                new DataColumn(chelper.DisplayName<PurchaseReturnRVM>("Quantity")),
                new DataColumn(chelper.DisplayName<PurchaseReturnRVM>("Unit")),
                new DataColumn(chelper.DisplayName<PurchaseReturnRVM>("UnitCost")),
                new DataColumn(chelper.DisplayName<PurchaseReturnRVM>("Total Amt")),
                new DataColumn(chelper.DisplayName<PurchaseReturnRVM>("Return Qty")),
                new DataColumn(chelper.DisplayName<PurchaseReturnRVM>("Amount"))
            });
            var preturns = await reportProcurementRepository.GetPurchaseReturn(filter);

            if (filter.GroupBy != null)
            {
                foreach (var grp in preturns.GroupBy(x => filter.GroupBy == "item" ? x.Item :
                                                     filter.GroupBy == "supplier" ? x.Supplier : x.ReturnDate))
                {
                    dt.Rows.Add(grp.Key, "", "", "", "", "", "", "", "");

                    foreach (var preturn in preturns.Where(x => filter.GroupBy == "item" ? x.Item == grp.FirstOrDefault()!.Item :
                                       filter.GroupBy == "supplier" ? x.Supplier == grp.FirstOrDefault()!.Supplier :
                                       x.ReturnDate == grp.FirstOrDefault()!.ReturnDate))
                    {
                        dt.Rows.Add(preturn.ReturnDate,
                                    preturn.DocumentNumber,
                                    preturn.Supplier,
                                    preturn.Item,
                                    preturn.ItemDescription,
                                    preturn.Quantity,
                                    preturn.Unit,
                                    preturn.UnitCost,
                                    preturn.TotalAmount,
                                    preturn.ReturnQuantity,
                                    preturn.ReturnTotalAmount);
                    }
                }
            }

            else
            {

                foreach (var preturn in preturns)
                {
                    dt.Rows.Add(preturn.ReturnDate,
                                preturn.DocumentNumber,
                                preturn.Supplier,
                                preturn.Item,
                                preturn.ItemDescription,
                                preturn.Quantity,
                                preturn.Unit,
                                preturn.UnitCost,
                                preturn.TotalAmount,
                                preturn.ReturnQuantity,
                                preturn.ReturnTotalAmount);
                }
            }

            using (XLWorkbook wb = new())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        filter.DateFrom.GetValueOrDefault().ToString("MMddyy") + "-"
                        + filter.DateTo.GetValueOrDefault().ToString("MMddyy") + " Purchase Return.xlsx");
                }
            }
        }
        #endregion

        #region Purchase Receiving
        public async Task<IActionResult> PurchaseReceiving()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseReceivingReport, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.PurchaseReceivingReport)).Convert();

            return View();
        }

        public async Task<IActionResult> PrintPurchaseReceiving(ListFilterReturnReceivingModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseReceivingReport, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;

            return View(await reportProcurementRepository.PrintPurchaseReceiving(filter));
        }

        [HttpPost]
        public async Task<IActionResult> GetPurchaseReceivingDT(ListFilterReturnReceivingModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseReceivingReport, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
            return Json(await reportProcurementRepository.GetPurchaseReceivingDT(dtForm, filter));
        }

        public async Task<IActionResult> DownloadPurchaseReceiving(ListFilterReturnReceivingModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseReceivingReport, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            DataTable dt = new DataTable("Purchase Receiving");
            dt.Columns.AddRange(new DataColumn[11] {
                new DataColumn(chelper.DisplayName<PurchaseReceivingRVM>("ReceivedDate")),
                new DataColumn(chelper.DisplayName<PurchaseReceivingRVM>("DocumentNumber")),
                new DataColumn(chelper.DisplayName<PurchaseReceivingRVM>("Supplier")),
                new DataColumn(chelper.DisplayName<PurchaseReceivingRVM>("Item")),
                new DataColumn(chelper.DisplayName<PurchaseReceivingRVM>("ItemDescription")),
                new DataColumn(chelper.DisplayName<PurchaseReceivingRVM>("Quantity")),
                new DataColumn(chelper.DisplayName<PurchaseReceivingRVM>("Unit")),
                new DataColumn(chelper.DisplayName<PurchaseReceivingRVM>("UnitCost")),
                new DataColumn(chelper.DisplayName<PurchaseReceivingRVM>("TotalAmount")),
                new DataColumn(chelper.DisplayName<PurchaseReceivingRVM>("Unserved Qty")),
                new DataColumn(chelper.DisplayName<PurchaseReceivingRVM>("Amount"))

            });
            var preceives = await reportProcurementRepository.GetPurchaseReceiving(filter);

            if (filter.GroupBy != null)
            {
                foreach (var grp in preceives.GroupBy(x => filter.GroupBy == "item" ? x.Item :
                                                     filter.GroupBy == "supplier" ? x.Supplier : x.ReturnDate))
                {
                    dt.Rows.Add(grp.Key, "", "", "", "", "", "", "", "");

                    foreach (var preceive in preceives.Where(x => filter.GroupBy == "item" ? x.Item == grp.FirstOrDefault()!.Item :
                                       filter.GroupBy == "supplier" ? x.Supplier == grp.FirstOrDefault()!.Supplier :
                                       x.ReturnDate == grp.FirstOrDefault()!.ReturnDate))

                    {
                        dt.Rows.Add(preceive.ReturnDate,
                                    preceive.DocumentNumber,
                                    preceive.Supplier,
                                    preceive.Item,
                                    preceive.ItemDescription,
                                    preceive.Quantity,
                                    preceive.Unit,
                                    preceive.UnitCost,
                                    preceive.TotalAmount,
                                    preceive.UnservedQuantity,
                                    preceive.UnservedAmount);
                    }
                }
            }
            else
            {
                foreach (var preceive in preceives)
                {
                    dt.Rows.Add(preceive.ReturnDate,
                                   preceive.DocumentNumber,
                                   preceive.Supplier,
                                   preceive.Item,
                                   preceive.ItemDescription,
                                   preceive.Quantity,
                                   preceive.Unit,
                                   preceive.UnitCost,
                                   preceive.TotalAmount,
                                   preceive.UnservedQuantity,
                                   preceive.UnservedAmount);
                }
            }
            using (XLWorkbook wb = new())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        filter.DateFrom.GetValueOrDefault().ToString("MMddyy") + "-"
                        + filter.DateTo.GetValueOrDefault().ToString("MMddyy") + " Purchase Receiving.xlsx");
                }
            }
        }
        #endregion
    }
}
