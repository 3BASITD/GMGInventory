using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GMGInventory.Data;
using GMGInventory.Common.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using GMGInventory.Application.Contracts;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using ClosedXML.Excel;
using GMGInventory.Helpers;
using GMGInventory.Application.Repositories;

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
        //"CommissionReport", "CustomerSalesAnalysisReport", "CustomerSalesReport", "DailySalesReport",
        //"DailySalesMarginReport", "SalesInvoiceListingReport", "SalesAgentArea", "SalesMarginItem",
        //"SalesReportBasis", "SalesReturnListingReport", "UnservedItem"


        #region Sales Report by Customer
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
                new DataColumn(chelper.DisplayName<SalesCustomerRVM>("CustomerCode")),
                new DataColumn(chelper.DisplayName<SalesCustomerRVM>("CustomerName")),
                new DataColumn(chelper.DisplayName<SalesCustomerRVM>("Brand")),
                new DataColumn(chelper.DisplayName<SalesCustomerRVM>("Amount"))
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
