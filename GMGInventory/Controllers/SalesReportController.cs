using Microsoft.AspNetCore.Mvc;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Authorization;
using GMGInventory.Application.Contracts;
using System.Data;
using ClosedXML.Excel;
using GMGInventory.Helpers;
using GMGInventory.Data;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class SalesReportController : Controller
    {
        private readonly IReportSalesRepository reportSalesRepository;
        private readonly ControllerHelper chelper;

        public SalesReportController(IReportSalesRepository reportSalesRepository)
        {
            this.reportSalesRepository = reportSalesRepository;
            chelper = new();
        }

        //"AgentSalesPerItem", "BrandSalesReport", "BestSellingItem", "BestSellingItemInvoice",
        //"CommissionReport", "CustomerSalesAnalysisReport", "CustomerSalesRanking", "DailySalesReport",
        //"DailySalesMarginReport", "SalesInvoiceListingReport", "SalesAgentArea", "SalesMarginItem",
        //"SalesReportBasis", "SalesReturnListingReport", "UnservedItem"


        #region Agent Sales per Item
        public IActionResult AgentSalesPerItem()
        {
            return View();
        }

        public async Task<IActionResult> PrintAgentSalesPerItem(ListFilterSalesModel filter)
        {
            return View(await reportSalesRepository.PrintAgentSalesPerItem(filter));
        }

        [HttpPost]
        public async Task<JsonResult> GetAgentSalesPerItemDT(ListFilterSalesModel filter)
        {
            DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
            return Json(await reportSalesRepository.GetAgentSalesPerItemDT(dtForm, filter));
        }

        public async Task<IActionResult> DownloadAgentSalesPerItem(ListFilterSalesModel filter)
        {
            DataTable dt = new DataTable("Agent Sales per Item");
            dt.Columns.AddRange(new DataColumn[8] {
                new DataColumn(chelper.DisplayName<AgentSalesPerItemRVM>("Agent")),
                new DataColumn(chelper.DisplayName<AgentSalesPerItemRVM>("Brand")),
                new DataColumn(chelper.DisplayName<AgentSalesPerItemRVM>("ItemType")),
                new DataColumn(chelper.DisplayName<AgentSalesPerItemRVM>("ItemCode")),
                new DataColumn(chelper.DisplayName<AgentSalesPerItemRVM>("ItemName")),
                new DataColumn(chelper.DisplayName<AgentSalesPerItemRVM>("Quantity")),
                new DataColumn(chelper.DisplayName<AgentSalesPerItemRVM>("Price")),
                new DataColumn(chelper.DisplayName<AgentSalesPerItemRVM>("Amount"))
            });
            var sales = await reportSalesRepository.GetAgentSalesPerItem(filter);
            foreach (var sale in sales)
            {
                dt.Rows.Add(sale.Agent,
                            sale.Brand,
                            sale.ItemType,
                            sale.ItemCode,
                            sale.ItemName,
                            sale.Quantity,
                            sale.Price,
                            sale.Amount);
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
                        + filter.DateTo.GetValueOrDefault().ToString("MMddyy") + " Agent Sales per Item.xlsx");
                }
            }
        }
        #endregion

        #region Brand Sales
        public IActionResult BrandSales()
        {
            return View();
        }

        public async Task<IActionResult> PrintBrandSales(ListFilterSalesModel filter)
        {
            return View(await reportSalesRepository.PrintBrandSales(filter));
        }

        [HttpPost]
        public async Task<JsonResult> GetBrandSalesDT(ListFilterSalesModel filter)
        {
            DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
            return Json(await reportSalesRepository.GetBrandSalesDT(dtForm, filter));
        }

        public async Task<IActionResult> DownloadBrandSales(ListFilterSalesModel filter)
        {
            DataTable dt = new DataTable("Brand Sales");
            dt.Columns.AddRange(new DataColumn[4] {
                new DataColumn(chelper.DisplayName<BrandSalesRVM>("Brand")),
                new DataColumn(chelper.DisplayName<BrandSalesRVM>("AgentCode")),
                new DataColumn(chelper.DisplayName<BrandSalesRVM>("AgentName")),
                new DataColumn(chelper.DisplayName<BrandSalesRVM>("Amount"))
            });
            var sales = await reportSalesRepository.GetBrandSales(filter);
            foreach (var sale in sales)
            {
                dt.Rows.Add(sale.Brand,
                            sale.AgentCode,
                            sale.AgentName,
                            sale.Amount);
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
                        + filter.DateTo.GetValueOrDefault().ToString("MMddyy") + " Brand Sales.xlsx");
                }
            }
        }
        #endregion

        #region Best Selling Item
        public IActionResult BestSellingItem()
        {
            return View();
        }

        public async Task<IActionResult> PrintBestSellingItem(ListFilterSalesModel filter)
        {
            return View(await reportSalesRepository.PrintBestSellingItem(filter));
        }

        [HttpPost]
        public async Task<JsonResult> GetBestSellingItemDT(ListFilterSalesModel filter)
        {
            DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
            return Json(await reportSalesRepository.GetBestSellingItemDT(dtForm, filter));
        }

        public async Task<IActionResult> DownloadBestSellingItem(ListFilterSalesModel filter)
        {
            DataTable dt = new DataTable("Best Selling Item (" + filter.Basis + ")");
            dt.Columns.AddRange(new DataColumn[7] {
                new DataColumn(chelper.DisplayName<BestSellingItemRVM>("Brand")),
                new DataColumn(chelper.DisplayName<BestSellingItemRVM>("ItemCode")),
                new DataColumn(chelper.DisplayName<BestSellingItemRVM>("ItemName")),
                new DataColumn(chelper.DisplayName<BestSellingItemRVM>("PartNumber")),
                new DataColumn(chelper.DisplayName<BestSellingItemRVM>("Unit")),
                new DataColumn(filter.Basis == "Quantity" ? chelper.DisplayName<BestSellingItemRVM>("Quantity") : chelper.DisplayName<BestSellingItemRVM>("UnitPrice")),
                new DataColumn(filter.Basis == "Quantity" ? chelper.DisplayName<BestSellingItemRVM>("CurrentQuantity") : chelper.DisplayName<BestSellingItemRVM>("Amount"))
            });
            var sales = await reportSalesRepository.GetBestSellingItem(filter);
            foreach (var sale in sales)
            {
                dt.Rows.Add(sale.Brand,
                            sale.ItemCode,
                            sale.ItemName,
                            sale.PartNumber,
                            sale.Unit,
                            filter.Basis == "Quantity" ? sale.Quantity : sale.UnitPrice,
                            filter.Basis == "Quantity" ? sale.CurrentQuantity : sale.Amount);
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
                        + filter.DateTo.GetValueOrDefault().ToString("MMddyy") + " Best Selling Item (" + filter.Basis + ").xlsx");
                }
            }
        }
        #endregion

        #region Commission
        public IActionResult Commission()
        {
            return View();
        }

        public async Task<IActionResult> PrintCommission(ListFilterSalesModel filter)
        {
            return View(await reportSalesRepository.PrintCommission(filter));
        }

        [HttpPost]
        public async Task<JsonResult> GetCommissionDT(ListFilterSalesModel filter)
        {
            DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
            return Json(await reportSalesRepository.GetCommissionDT(dtForm, filter));
        }

        public async Task<IActionResult> DownloadCommission(ListFilterSalesModel filter)
        {
            DataTable dt = new ("Commission");
            dt.Columns.AddRange(new DataColumn[7] {
                new DataColumn(chelper.DisplayName<CommissionRVM>("InvoiceDate")),
                new DataColumn(chelper.DisplayName<CommissionRVM>("InvoiceNumber")),
                new DataColumn(chelper.DisplayName<CommissionRVM>("Customer")),
                new DataColumn(chelper.DisplayName<CommissionRVM>("Agent")),
                new DataColumn(chelper.DisplayName<CommissionRVM>("CommissionRate")),
                new DataColumn(chelper.DisplayName<CommissionRVM>("InvoiceAmount")),
                new DataColumn(chelper.DisplayName<CommissionRVM>("CommissionAmount"))
            });
            var sales = await reportSalesRepository.GetCommission(filter);
            foreach (var sale in sales)
            {
                dt.Rows.Add(sale.InvoiceDate,
                            sale.InvoiceNumber,
                            sale.Customer,
                            sale.Agent,
                            sale.CommissionRate,
                            sale.InvoiceAmount,
                            sale.CommissionAmount);
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
                        + filter.DateTo.GetValueOrDefault().ToString("MMddyy") + " Commission.xlsx");
                }
            }
        }
        #endregion

        #region Customer Sales Ranking
        public IActionResult CustomerSalesRanking()
        {
            return View();
        }

        public async Task<IActionResult> PrintCustomerSalesRanking(ListFilterSalesModel filter)
        {
            return View(await reportSalesRepository.PrintCustomerSalesRanking(filter));
        }

        [HttpPost]
        public async Task<JsonResult> GetCustomerSalesRankingDT(ListFilterSalesModel filter)
        {
            DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
            return Json(await reportSalesRepository.GetCustomerSalesRankingDT(dtForm, filter));
        }

        public async Task<IActionResult> DownloadCustomerSalesRanking(ListFilterSalesModel filter)
        {
            DataTable dt = new("Customer Sales Ranking");
            dt.Columns.AddRange(new DataColumn[4] {
                new DataColumn(chelper.DisplayName<CustomerSalesRankingRVM>("Rank")),
                new DataColumn(chelper.DisplayName<CustomerSalesRankingRVM>("CustomerCode")),
                new DataColumn(chelper.DisplayName<CustomerSalesRankingRVM>("CustomerName")),
                new DataColumn(chelper.DisplayName<CustomerSalesRankingRVM>("Amount"))
            });
            var sales = await reportSalesRepository.GetCustomerSalesRanking(filter);
            foreach (var sale in sales)
            {
                dt.Rows.Add(sale.Rank,
                            sale.CustomerCode,
                            sale.CustomerName,
                            sale.Amount);
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
                        + filter.DateTo.GetValueOrDefault().ToString("MMddyy") + " Customer Sales Ranking.xlsx");
                }
            }
        }
        #endregion

        #region Customer Sales
        public IActionResult CustomerSales()
        {
            return View();
        }

        public async Task<IActionResult> PrintCustomerSales(ListFilterSalesModel filter)
        {
            return View(await reportSalesRepository.PrintCustomerSales(filter));
        }

        [HttpPost]
        public async Task<JsonResult> GetCustomerSalesDT(ListFilterSalesModel filter)
        {
            DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
            return Json(await reportSalesRepository.GetCustomerSalesDT(dtForm, filter));
        }

        public async Task<IActionResult> DownloadCustomerSales(ListFilterSalesModel filter)
        {
            DataTable dt = new DataTable("Sales Report "
                                       + filter.DateFrom.GetValueOrDefault().ToString("MMddyy")
                                       + "-"
                                       + filter.DateTo.GetValueOrDefault().ToString("MMddyy"));
            CreditDebitMemoRVM debitVM = new CreditDebitMemoRVM();
            dt.Columns.AddRange(new DataColumn[4] {
                new DataColumn(chelper.DisplayName<CustomerSalesRVM>("CustomerCode")),
                new DataColumn(chelper.DisplayName<CustomerSalesRVM>("CustomerName")),
                new DataColumn(chelper.DisplayName<CustomerSalesRVM>("Brand")),
                new DataColumn(chelper.DisplayName<CustomerSalesRVM>("Amount"))
            });
            var debits = await reportSalesRepository.GetCustomerSales(filter);
            foreach (var debit in debits)
            {
                dt.Rows.Add(debit.CustomerCode,
                            debit.CustomerName,
                            debit.Brand,
                            debit.Amount);
            }

            using (XLWorkbook wb = new())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        DateTime.Now.ToString("yyyyMMdd") + " Sales Report by Customer.xlsx");
                }
            }
        }
        #endregion

        #region Daily Sales
        public IActionResult DailySales()
        {
            return View();
        }

        public async Task<IActionResult> PrintDailySales(ListFilterSalesModel filter)
        {
            return View(await reportSalesRepository.PrintDailySales(filter));
        }

        [HttpPost]
        public async Task<JsonResult> GetDailySalesDT(ListFilterSalesModel filter)
        {
            DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
            return Json(await reportSalesRepository.GetDailySalesDT(dtForm, filter));
        }

        public async Task<IActionResult> DownloadDailySales(ListFilterSalesModel filter)
        {
            DataTable dt = new DataTable("Daily Sales");
            dt.Columns.AddRange(new DataColumn[8] {
                new DataColumn(chelper.DisplayName<DailySalesRVM>("InvoiceDate")),
                new DataColumn(chelper.DisplayName<DailySalesRVM>("InvoiceNumber")),
                new DataColumn(chelper.DisplayName<DailySalesRVM>("CustomerCode")),
                new DataColumn(chelper.DisplayName<DailySalesRVM>("CustomerName")),
                new DataColumn(chelper.DisplayName<DailySalesRVM>("Agent")),
                new DataColumn(chelper.DisplayName<DailySalesRVM>("Term")),
                new DataColumn(chelper.DisplayName<DailySalesRVM>("CommissionRate")),
                new DataColumn(chelper.DisplayName<DailySalesRVM>("Amount"))
            });
            var sales = await reportSalesRepository.GetDailySales(filter);
            foreach (var sale in sales)
            {
                dt.Rows.Add(sale.InvoiceDate,
                            sale.InvoiceNumber,
                            sale.CustomerCode,
                            sale.CustomerName,
                            sale.Agent,
                            sale.Term,
                            sale.CommissionRate,
                            sale.Amount);
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
                        + filter.DateTo.GetValueOrDefault().ToString("MMddyy") + " Daily Sales.xlsx");
                }
            }
        }
        #endregion

        #region Sales Invoice
        public IActionResult SalesInvoice()
        {
            return View();
        }

        public async Task<IActionResult> PrintSalesInvoice(ListFilterSalesModel filter)
        {
            return View(await reportSalesRepository.PrintSalesInvoice(filter));
        }

        [HttpPost]
        public async Task<JsonResult> GetSalesInvoiceDT(ListFilterSalesModel filter)
        {
            DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
            return Json(await reportSalesRepository.GetSalesInvoiceDT(dtForm, filter));
        }

        public async Task<IActionResult> DownloadSalesInvoice(ListFilterSalesModel filter)
        {
            DataTable dt = new DataTable("Sales Invoice Listing");
            dt.Columns.AddRange(new DataColumn[8] {
                new DataColumn(chelper.DisplayName<SalesInvoiceRVM>("InvoiceNumber")),
                new DataColumn(chelper.DisplayName<SalesInvoiceRVM>("ControlNumber")),
                new DataColumn(chelper.DisplayName<SalesInvoiceRVM>("InvoiceDate")),
                new DataColumn(chelper.DisplayName<SalesInvoiceRVM>("CustomerCode")),
                new DataColumn(chelper.DisplayName<SalesInvoiceRVM>("CustomerName")),
                new DataColumn(chelper.DisplayName<SalesInvoiceRVM>("Agent")),
                new DataColumn(chelper.DisplayName<SalesInvoiceRVM>("Status")),
                new DataColumn(chelper.DisplayName<SalesInvoiceRVM>("Amount"))
            });
            var sales = await reportSalesRepository.GetSalesInvoice(filter);
            foreach (var sale in sales)
            {
                dt.Rows.Add(sale.InvoiceNumber,
                            sale.ControlNumber,
                            sale.InvoiceDate,
                            sale.CustomerCode,
                            sale.CustomerName,
                            sale.Agent,
                            sale.Status,
                            sale.Amount);
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
                        + filter.DateTo.GetValueOrDefault().ToString("MMddyy") + " Sales Invoice Listing.xlsx");
                }
            }
        }
        #endregion

        #region Sales by Agent-Area
        public IActionResult SalesAgentArea()
        {
            return View();
        }

        public async Task<IActionResult> PrintSalesAgentArea(ListFilterSalesModel filter)
        {
            return View(await reportSalesRepository.PrintSalesAgentArea(filter));
        }

        [HttpPost]
        public async Task<JsonResult> GetSalesAgentAreaDT(ListFilterSalesModel filter)
        {
            DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
            return Json(await reportSalesRepository.GetSalesAgentAreaDT(dtForm, filter));
        }

        public async Task<IActionResult> DownloadSalesAgentArea(ListFilterSalesModel filter)
        {
            DataTable dt = new DataTable("Sales by Agent-Area");
            dt.Columns.AddRange(new DataColumn[5] {
                new DataColumn(chelper.DisplayName<SalesAgentAreaRVM>("AgentCode")),
                new DataColumn(chelper.DisplayName<SalesAgentAreaRVM>("AgentName")),
                new DataColumn(chelper.DisplayName<SalesAgentAreaRVM>("Area")),
                new DataColumn(chelper.DisplayName<SalesAgentAreaRVM>("Sales")),
                new DataColumn(chelper.DisplayName<SalesAgentAreaRVM>("PercentSales"))
            });
            var sales = await reportSalesRepository.GetSalesAgentArea(filter);
            foreach (var sale in sales)
            {
                dt.Rows.Add(sale.AgentCode,
                            sale.AgentName,
                            sale.Area,
                            sale.Sales,
                            sale.PercentSales);
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
                        + filter.DateTo.GetValueOrDefault().ToString("MMddyy") + " Sales by Agent-Area.xlsx");
                }
            }
        }
        #endregion



        #region Sales Report by Basis
        public IActionResult SalesReportBasis()
        {
            return View();
        }

        public async Task<IActionResult> PrintSalesReportBasis(ListFilterSalesModel filter)
        {
            return View(await reportSalesRepository.PrintSalesReportBasis(filter));
        }

        [HttpPost]
        public async Task<JsonResult> GetSalesReportBasisDT(ListFilterSalesModel filter)
        {
            DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
            return Json(await reportSalesRepository.GetSalesReportBasisDT(dtForm, filter));
        }

        public async Task<IActionResult> DownloadSalesReportBasis(ListFilterSalesModel filter)
        {
            DataTable dt = new DataTable("Sales Report by Basis");
            dt.Columns.AddRange(new DataColumn[3] {
                new DataColumn(filter.Basis + " " + chelper.DisplayName<SalesReportBasisRVM>("Code")),
                new DataColumn(filter.Basis + " " + chelper.DisplayName<SalesReportBasisRVM>("Name")),
                new DataColumn(chelper.DisplayName<SalesReportBasisRVM>("Amount"))
            });
            var sales = await reportSalesRepository.GetSalesReportBasis(filter);
            foreach (var sale in sales)
            {
                dt.Rows.Add(sale.Code,
                            sale.Name,
                            sale.Amount);
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
                        + filter.DateTo.GetValueOrDefault().ToString("MMddyy") + " Sales Report by Basis.xlsx");
                }
            }
        }
        #endregion

        #region Sales Return
        public IActionResult SalesReturn()
        {
            return View();
        }

        public async Task<IActionResult> PrintSalesReturn(ListFilterSalesModel filter)
        {
            return View(await reportSalesRepository.PrintSalesReturn(filter));
        }

        [HttpPost]
        public async Task<JsonResult> GetSalesReturnDT(ListFilterSalesModel filter)
        {
            DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
            return Json(await reportSalesRepository.GetSalesReturnDT(dtForm, filter));
        }

        public async Task<IActionResult> DownloadSalesReturn(ListFilterSalesModel filter)
        {
            DataTable dt = new DataTable("Sales Return Listing");
            dt.Columns.AddRange(new DataColumn[14] {
                new DataColumn(chelper.DisplayName<SalesReturnRVM>("Brand")),
                new DataColumn(chelper.DisplayName<SalesReturnRVM>("ItemType")),
                new DataColumn(chelper.DisplayName<SalesReturnRVM>("ItemCode")),
                new DataColumn(chelper.DisplayName<SalesReturnRVM>("ItemName")),
                new DataColumn(chelper.DisplayName<SalesReturnRVM>("Unit")),
                new DataColumn(chelper.DisplayName<SalesReturnRVM>("SRNumber")),
                new DataColumn(chelper.DisplayName<SalesReturnRVM>("ReturnDate")),
                new DataColumn(chelper.DisplayName<SalesReturnRVM>("Customer")),
                new DataColumn(chelper.DisplayName<SalesReturnRVM>("Agent")),
                new DataColumn(chelper.DisplayName<SalesReturnRVM>("SINumber")),
                new DataColumn(chelper.DisplayName<SalesReturnRVM>("ControlNumber")),
                new DataColumn(chelper.DisplayName<SalesReturnRVM>("QuantityReturned")),
                new DataColumn(chelper.DisplayName<SalesReturnRVM>("PriceReturned")),
                new DataColumn(chelper.DisplayName<SalesReturnRVM>("Amount"))
            });
            var sales = await reportSalesRepository.GetSalesReturn(filter);
            foreach (var sale in sales)
            {
                dt.Rows.Add(sale.Brand,
                            sale.ItemType,
                            sale.ItemCode,
                            sale.ItemName,
                            sale.Unit,
                            sale.SRNumber,
                            sale.ReturnDate,
                            sale.Customer,
                            sale.Agent,
                            sale.SINumber,
                            sale.ControlNumber,
                            sale.QuantityReturned,
                            sale.PriceReturned,
                            sale.Amount);
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
                        + filter.DateTo.GetValueOrDefault().ToString("MMddyy") + " Sales Return Listing.xlsx");
                }
            }
        }
        #endregion

        #region Unserved Items
        public IActionResult UnservedItem()
        {
            return View();
        }

        public async Task<IActionResult> PrintUnservedItem(ListFilterSalesModel filter)
        {
            return View(await reportSalesRepository.PrintUnservedItem(filter));
        }

        [HttpPost]
        public async Task<JsonResult> GetUnservedItemDT(ListFilterSalesModel filter)
        {
            DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
            return Json(await reportSalesRepository.GetUnservedItemDT(dtForm, filter));
        }

        public async Task<IActionResult> DownloadUnservedItem(ListFilterSalesModel filter)
        {
            DataTable dt = new DataTable("Unserved Item "
                                       + filter.DateFrom.GetValueOrDefault().ToString("MMddyy")
                                       + "-"
                                       + filter.DateTo.GetValueOrDefault().ToString("MMddyy"));
            dt.Columns.AddRange(new DataColumn[6] {
                new DataColumn(chelper.DisplayName<UnservedItemRVM>("Brand")),
                new DataColumn(chelper.DisplayName<UnservedItemRVM>("ItemCode")),
                new DataColumn(chelper.DisplayName<UnservedItemRVM>("ItemName")),
                new DataColumn(chelper.DisplayName<UnservedItemRVM>("Pending")),
                new DataColumn(chelper.DisplayName<UnservedItemRVM>("Current")),
                new DataColumn(chelper.DisplayName<UnservedItemRVM>("ReplenishDate"))
            });
            var items = await reportSalesRepository.GetUnservedItem(filter);
            foreach (var item in items)
            {
                dt.Rows.Add(item.Brand,
                            item.ItemCode,
                            item.ItemName,
                            item.Pending,
                            item.Current,
                            item.ReplenishDate);
            }

            using (XLWorkbook wb = new())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        DateTime.Now.ToString("yyyyMMdd") + " Unserved Items.xlsx");
                }
            }
        }
        #endregion
    }
}
