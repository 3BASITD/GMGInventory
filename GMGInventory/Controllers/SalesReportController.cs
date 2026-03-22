using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class SalesReportController(IAccessRepository accessRepository
                            , IReportSalesRepository reportSalesRepository) : BaseController(accessRepository)
    {

        private readonly ControllerHelper chelper = new();

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
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.AgentSalesPerItemReport, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;

            return View(await reportSalesRepository.PrintAgentSalesPerItem(filter));
        }

        [HttpPost]
        public async Task<IActionResult> GetAgentSalesPerItemDT(ListFilterSalesModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.AgentSalesPerItemReport, AccessTypes.View);
            if (redirectResult != null) return redirectResult;


            DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
            return Json(await reportSalesRepository.GetAgentSalesPerItemDT(dtForm, filter));
        }

        public async Task<IActionResult> DownloadAgentSalesPerItem(ListFilterSalesModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.AgentSalesPerItemReport, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

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
        #endregion Agent Sales per Item

        #region Brand Sales
        public async Task<IActionResult> BrandSales()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.BrandSalesReport, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return View();
        }

        public async Task<IActionResult> PrintBrandSales(ListFilterSalesModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.BrandSalesReport, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;

            return View(await reportSalesRepository.PrintBrandSales(filter));
        }

        [HttpPost]
        public async Task<IActionResult> GetBrandSalesDT(ListFilterSalesModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.BrandSalesReport, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
            return Json(await reportSalesRepository.GetBrandSalesDT(dtForm, filter));
        }

        public async Task<IActionResult> DownloadBrandSales(ListFilterSalesModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.BrandSalesReport, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

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
        #endregion Brand Sales

        #region Best Selling Item
        public async Task<IActionResult> BestSellingItem()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.BestSellingItemsReport, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return View();
        }

        public async Task<IActionResult> PrintBestSellingItem(ListFilterSalesModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.BestSellingItemsReport, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;


            return View(await reportSalesRepository.PrintBestSellingItem(filter));
        }

        [HttpPost]
        public async Task<IActionResult> GetBestSellingItemDT(ListFilterSalesModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.BestSellingItemsReport, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
            return Json(await reportSalesRepository.GetBestSellingItemDT(dtForm, filter));
        }

        public async Task<IActionResult> DownloadBestSellingItem(ListFilterSalesModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.BestSellingItemsReport, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

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
        #endregion Best Selling Item

        #region Commission
        public async Task<IActionResult> Commission()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.CommissionReport, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return View();
        }

        public async Task<IActionResult> PrintCommission(ListFilterSalesModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.CommissionReport, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;

            return View(await reportSalesRepository.PrintCommission(filter));
        }

        [HttpPost]
        public async Task<IActionResult> GetCommissionDT(ListFilterSalesModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.CommissionReport, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
            return Json(await reportSalesRepository.GetCommissionDT(dtForm, filter));
        }

        public async Task<IActionResult> DownloadCommission(ListFilterSalesModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.CommissionReport, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            DataTable dt = new("Commission");
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
                            sale.CommissionAmount2);
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
        #endregion Commission

        #region Customer Sales Ranking
        public async Task<IActionResult> CustomerSalesRanking()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.CustomerSalesRankingReport, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return View();
        }

        public async Task<IActionResult> PrintCustomerSalesRanking(ListFilterSalesModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.CustomerSalesRankingReport, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;

            return View(await reportSalesRepository.PrintCustomerSalesRanking(filter));
        }

        [HttpPost]
        public async Task<IActionResult> GetCustomerSalesRankingDT(ListFilterSalesModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.CustomerSalesRankingReport, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
            return Json(await reportSalesRepository.GetCustomerSalesRankingDT(dtForm, filter));
        }

        public async Task<IActionResult> DownloadCustomerSalesRanking(ListFilterSalesModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.CustomerSalesRankingReport, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

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
        #endregion Customer Sales Ranking

        #region Customer Sales
        public async Task<IActionResult> CustomerSales()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.CustomerSalesReport, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return View();
        }

        public async Task<IActionResult> CustomerSalesCompare()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.CustomerSalesReport, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return View();
        }

        public async Task<IActionResult> PrintCustomerSales(ListFilterSalesModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.CustomerSalesReport, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;

            return View(await reportSalesRepository.PrintCustomerSales(filter));
        }

        public async Task<IActionResult> PrintCustomerSalesCompare(ListFilterSalesModel filter1, ListFilterSalesModel filter2)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.CustomerSalesReport, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;

            var sales = new CustomerSalesComparePVM
            {
                CustomerSales1 = await reportSalesRepository.PrintCustomerSales(filter1),
                CustomerSales2 = await reportSalesRepository.PrintCustomerSales(filter2)
            };
            return View(sales);
        }

        [HttpPost]
        public async Task<IActionResult> GetCustomerSalesDT(ListFilterSalesModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.CustomerSalesReport, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
            return Json(await reportSalesRepository.GetCustomerSalesDT(dtForm, filter));
        }

        public async Task<IActionResult> DownloadCustomerSales(ListFilterSalesModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.CustomerSalesReport, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

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

        public async Task<IActionResult> DownloadCustomerSalesCompare(ListFilterSalesModel filter1, ListFilterSalesModel filter2)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.CustomerSalesReport, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            DataTable dt1 = new DataTable("Sales Report #1");
            dt1.Columns.AddRange(new DataColumn[3] {
                new DataColumn("Customer"),
                new DataColumn(chelper.DisplayName<CustomerSalesRVM>("Brand")),
                new DataColumn(chelper.DisplayName<CustomerSalesRVM>("Amount"))
            });
            var debits1 = await reportSalesRepository.GetCustomerSales(filter1);
            foreach (var debit in debits1)
            {
                dt1.Rows.Add(debit.CustomerCode,
                            debit.Brand,
                            debit.Amount);
            }

            DataTable dt2 = new DataTable("Sales Report #2");
            dt2.Columns.AddRange(new DataColumn[3] {
                new DataColumn("Customer"),
                new DataColumn(chelper.DisplayName<CustomerSalesRVM>("Brand")),
                new DataColumn(chelper.DisplayName<CustomerSalesRVM>("Amount"))
            });
            var debits2 = await reportSalesRepository.GetCustomerSales(filter2);
            foreach (var debit in debits2)
            {
                dt2.Rows.Add(debit.CustomerCode,
                            debit.Brand,
                            debit.Amount);
            }

            using (XLWorkbook wb = new())
            {
                wb.Worksheets.Add(dt1);
                wb.Worksheets.Add(dt2);
                using (MemoryStream stream = new())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        DateTime.Now.ToString("yyyyMMdd") + " Sales Report by Customer (Compare).xlsx");
                }
            }
        }
        #endregion Customer Sales

        #region Daily Sales
        public async Task<IActionResult> DailySales()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.DailySalesReport, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return View();
        }

        public async Task<IActionResult> PrintDailySales(ListFilterSalesModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.DailySalesReport, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;

            return View(await reportSalesRepository.PrintDailySales(filter));
        }

        [HttpPost]
        public async Task<IActionResult> GetDailySalesDT(ListFilterSalesModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.DailySalesReport, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
            return Json(await reportSalesRepository.GetDailySalesDT(dtForm, filter));
        }

        public async Task<IActionResult> DownloadDailySales(ListFilterSalesModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.DailySalesReport, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            DataTable dt = new DataTable("Daily Sales");
            dt.Columns.AddRange(new DataColumn[8] {
                new DataColumn(chelper.DisplayName<DailySalesRVM>("InvoiceDate")),
                new DataColumn(chelper.DisplayName<DailySalesRVM>("InvoiceNumber")),
                new DataColumn(chelper.DisplayName<DailySalesRVM>("BIRInvoiceNumber")),
                new DataColumn(chelper.DisplayName<DailySalesRVM>("CustomerCode")),
                new DataColumn(chelper.DisplayName<DailySalesRVM>("CustomerName")),
                new DataColumn(chelper.DisplayName<DailySalesRVM>("Agent")),
                new DataColumn(chelper.DisplayName<DailySalesRVM>("Term")),
                //new DataColumn(chelper.DisplayName<DailySalesRVM>("CommissionRate")),
                new DataColumn(chelper.DisplayName<DailySalesRVM>("Amount"))
            });
            var sales = await reportSalesRepository.FilteredDailySales(filter);
            foreach (var sale in sales)
            {
                dt.Rows.Add(sale.InvoiceDate,
                            sale.InvoiceNumber,
                            sale.BIRInvoiceNumber,
                            sale.CustomerCode,
                            sale.CustomerName,
                            sale.Agent,
                            sale.Term,
                            //sale.CommissionRate,
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
        #endregion Daily Sales

        #region Sales Invoice
        public async Task<IActionResult> SalesInvoice()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesInvoiceListingReport, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return View();
        }

        public async Task<IActionResult> PrintSalesInvoice(ListFilterSalesModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesInvoiceListingReport, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;

            return View(await reportSalesRepository.PrintSalesInvoice(filter));
        }

        [HttpPost]
        public async Task<IActionResult> GetSalesInvoiceDT(ListFilterSalesModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesInvoiceListingReport, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
            return Json(await reportSalesRepository.GetSalesInvoiceDT(dtForm, filter));
        }

        public async Task<IActionResult> DownloadSalesInvoice(ListFilterSalesModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesInvoiceListingReport, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

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
        #endregion Sales Invoice

        #region Sales by Agent-Area
        public async Task<IActionResult> SalesAgentArea()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesByAgentAreaReport, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return View();
        }

        public async Task<IActionResult> PrintSalesAgentArea(ListFilterSalesModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesByAgentAreaReport, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;

            return View(await reportSalesRepository.PrintSalesAgentArea(filter));
        }

        public async Task<IActionResult> GetSalesAgentArea(ListFilterSalesModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesByAgentAreaReport, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return Json(await reportSalesRepository.GetSalesAgentArea(filter));
        }

        [HttpPost]
        public async Task<IActionResult> GetSalesAgentAreaDT(ListFilterSalesModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesByAgentAreaReport, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
            return Json(await reportSalesRepository.GetSalesAgentAreaDT(dtForm, filter));
        }

        public async Task<IActionResult> DownloadSalesAgentArea(ListFilterSalesModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesByAgentAreaReport, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

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
        #endregion Sales by Agent-Area

        #region Sales Report by Basis
        public async Task<IActionResult> SalesReportBasis()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesReportByBasisReport, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return View();
        }

        public async Task<IActionResult> PrintSalesReportBasis(ListFilterSalesModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesReportByBasisReport, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;

            return View(await reportSalesRepository.PrintSalesReportBasis(filter));
        }

        [HttpPost]
        public async Task<IActionResult> GetSalesReportBasisDT(ListFilterSalesModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesReportByBasisReport, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
            return Json(await reportSalesRepository.GetSalesReportBasisDT(dtForm, filter));
        }

        public async Task<IActionResult> DownloadSalesReportBasis(ListFilterSalesModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesReportByBasisReport, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

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
        #endregion Sales Report by Basis

        #region Sales Return
        public async Task<IActionResult> SalesReturn()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesReturnListingReport, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return View();
        }

        public async Task<IActionResult> PrintSalesReturn(ListFilterSalesModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesReturnListingReport, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;

            return View(await reportSalesRepository.PrintSalesReturn(filter));
        }

        [HttpPost]
        public async Task<IActionResult> GetSalesReturnDT(ListFilterSalesModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesReturnListingReport, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
            return Json(await reportSalesRepository.GetSalesReturnDT(dtForm, filter));
        }

        public async Task<IActionResult> DownloadSalesReturn(ListFilterSalesModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesReturnListingReport, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            DataTable dt = new DataTable("Sales Return Listing");
            dt.Columns.AddRange(new DataColumn[19] {
                new DataColumn(chelper.DisplayName<SalesReturnRVM>("SRNumber")),
                new DataColumn(chelper.DisplayName<SalesReturnRVM>("ReturnDate")),
                new DataColumn(chelper.DisplayName<SalesReturnRVM>("ReturnReason")),
                new DataColumn(chelper.DisplayName<SalesReturnRVM>("SONumber")),
                new DataColumn(chelper.DisplayName<SalesReturnRVM>("OrderDate")),
                new DataColumn(chelper.DisplayName<SalesReturnRVM>("Customer")),
                new DataColumn(chelper.DisplayName<SalesReturnRVM>("Agent")),
                new DataColumn(chelper.DisplayName<SalesReturnRVM>("SINumber")),
                new DataColumn(chelper.DisplayName<SalesReturnRVM>("ControlNumber")),
                new DataColumn(chelper.DisplayName<SalesReturnRVM>("DeliveryDate")),
                new DataColumn(chelper.DisplayName<SalesReturnRVM>("PickupDate")),
                new DataColumn(chelper.DisplayName<SalesReturnRVM>("ItemCode")),
                new DataColumn(chelper.DisplayName<SalesReturnRVM>("ItemName")),
                new DataColumn(chelper.DisplayName<SalesReturnRVM>("Brand")),
                new DataColumn(chelper.DisplayName<SalesReturnRVM>("ItemType")),
                new DataColumn(chelper.DisplayName<SalesReturnRVM>("Unit")),
                new DataColumn(chelper.DisplayName<SalesReturnRVM>("QuantityReturned")),
                new DataColumn(chelper.DisplayName<SalesReturnRVM>("PriceReturned")),
                new DataColumn(chelper.DisplayName<SalesReturnRVM>("Amount"))
            });
            var sales = await reportSalesRepository.GetSalesReturn(filter);
            foreach (var sale in sales)
            {
                dt.Rows.Add(sale.SRNumber,
                            sale.ReturnDate,
                            sale.ReturnReason,
                            sale.SONumber,
                            sale.OrderDate,
                            sale.Customer,
                            sale.Agent,
                            sale.SINumber,
                            sale.ControlNumber,
                            sale.DeliveryDate,
                            sale.PickupDate,
                            sale.ItemCode,
                            sale.ItemName,
                            sale.Brand,
                            sale.ItemType,
                            sale.Unit,
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
        #endregion Sales Return

        #region Unserved Items
        public async Task<IActionResult> UnservedItem()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.UnservedItemReport, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return View();
        }

        public async Task<IActionResult> PrintUnservedItem(ListFilterSalesModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.UnservedItemReport, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;

            return View(await reportSalesRepository.PrintUnservedItem(filter));
        }

        [HttpPost]
        public async Task<IActionResult> GetUnservedItemDT(ListFilterSalesModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.UnservedItemReport, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
            return Json(await reportSalesRepository.GetUnservedItemDT(dtForm, filter));
        }

        public async Task<IActionResult> DownloadUnservedItem(ListFilterSalesModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.UnservedItemReport, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            DataTable dt = new("Unserved Item "
                             + filter.DateFrom.GetValueOrDefault().ToString("MMddyy")
                             + "-"
                             + filter.DateTo.GetValueOrDefault().ToString("MMddyy"));
            dt.Columns.AddRange([
                new DataColumn(chelper.DisplayName<UnservedItemRVM>("Brand")),
                new DataColumn(chelper.DisplayName<UnservedItemRVM>("ItemCode")),
                new DataColumn(chelper.DisplayName<UnservedItemRVM>("ItemName")),
                new DataColumn(chelper.DisplayName<UnservedItemRVM>("Pending")),
                new DataColumn(chelper.DisplayName<UnservedItemRVM>("Current")),
                new DataColumn(chelper.DisplayName<UnservedItemRVM>("ReplenishDate"))
            ]);
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

            using XLWorkbook wb = new();
            wb.Worksheets.Add(dt);
            using MemoryStream stream = new();
            wb.SaveAs(stream);
            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                DateTime.Now.ToString("yyyyMMdd") + " Unserved Items.xlsx");
        }
        #endregion Unserved Items



        #region  Outbound
        public async Task<IActionResult> Outbound()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.DailyOutboundReport, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.DailyOutboundReport)).Convert();

           

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetOutboundDT(ListFilterSalesModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.DailyOutboundReport, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
            return Json(await reportSalesRepository.GetOutboundDT(dtForm, filter));
        }

        public async Task<IActionResult> DownloadOutbound(ListFilterSalesModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.DailyOutboundReport, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            DataTable dt = new("Outbound "
                             + filter.DateFrom.GetValueOrDefault().ToString("MMddyy")
                             + "-"
                             + filter.DateTo.GetValueOrDefault().ToString("MMddyy"));
            dt.Columns.AddRange([
                new DataColumn(chelper.DisplayName<OutboundRVM>("OldSkuCode")),
                new DataColumn(chelper.DisplayName<OutboundRVM>("ItemCode")),
                new DataColumn(chelper.DisplayName<OutboundRVM>("ItemName")),
                new DataColumn(chelper.DisplayName<OutboundRVM>("Brand")),
                new DataColumn(chelper.DisplayName<OutboundRVM>("Quantity")),
                new DataColumn(chelper.DisplayName<OutboundRVM>("SalesOrderNumber")),
                new DataColumn(chelper.DisplayName<OutboundRVM>("Region")),
                new DataColumn(chelper.DisplayName<OutboundRVM>("Area"))
            ]);
            var items = await reportSalesRepository.GetOutbound(filter);
            foreach (var item in items)
            {
                dt.Rows.Add(item.OldSkuCode,
                            item.ItemCode,
                            item.ItemName,
                            item.Brand,
                            item.Quantity,
                            item.SalesOrderNumber,
                            item.Region,
                            item.Area);
            }

            using XLWorkbook wb = new();
            wb.Worksheets.Add(dt);
            using MemoryStream stream = new();
            wb.SaveAs(stream);
            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                DateTime.Now.ToString("yyyyMMdd") + " Outbound.xlsx");
        }

        public async Task<IActionResult> PrintOutbound(ListFilterSalesModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.DailyOutboundReport, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;

            return View(await reportSalesRepository.PrintOutbound(filter));
        }
        #endregion Outbound
    }

}