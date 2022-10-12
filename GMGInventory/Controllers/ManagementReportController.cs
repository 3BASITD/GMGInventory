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

namespace GMGInventory.Controllers
{
    [Authorize]
    public class ManagementReportController : Controller
    {
        private readonly IReportManagementRepository reportManagementRepository;
        private readonly ControllerHelper chelper;

        public ManagementReportController(IReportManagementRepository reportManagementRepository)
        {
            this.reportManagementRepository = reportManagementRepository;
            chelper = new();
        }

        #region Check Register by Date
        public IActionResult CheckRegisterDate()
        {
            return View();
        }

        public async Task<JsonResult> GetCheckRegisterByDate(ListFilterCustomerModel filter)
        {
            filter.Customer = null;
            return Json(await reportManagementRepository.GetCheckRegisterByDate(filter));
        }

        [HttpPost]
        public async Task<JsonResult> GetCheckRegisterByDateDT(ListFilterCustomerModel filter)
        {
            filter.Customer = null;
            DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
            return Json(await reportManagementRepository.GetCheckRegisterDT(dtForm, filter));
        }

        public async Task<IActionResult> DownloadCheckRegisterByDate(ListFilterCustomerModel filter)
        {
            filter.Customer = null;
            DataTable dt = new DataTable("Check Register (" 
                                       + filter.DateFrom.GetValueOrDefault().ToString("MMddyy") 
                                       + "-" 
                                       + filter.DateTo.GetValueOrDefault().ToString("MMddyy")
                                       + ")");
            dt.Columns.AddRange(new DataColumn[5] {
                new DataColumn(chelper.DisplayName<ReportManagementCheckRegisterRVM>("CheckDate")),
                new DataColumn(chelper.DisplayName<ReportManagementCheckRegisterRVM>("Customer")),
                new DataColumn(chelper.DisplayName<ReportManagementCheckRegisterRVM>("BankDetails")),
                new DataColumn(chelper.DisplayName<ReportManagementCheckRegisterRVM>("CheckNumber")),
                new DataColumn(chelper.DisplayName<ReportManagementCheckRegisterRVM>("CheckAmount"))
            });
            var checks = await reportManagementRepository.GetCheckRegisterByDate(filter);
            foreach (var check in checks)
            {
                dt.Rows.Add(check.CheckDate,
                            check.Customer,
                            check.BankDetails,
                            check.CheckNumber,
                            check.CheckAmount);
            }

            using (XLWorkbook wb = new())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        DateTime.Now.ToString("yyyyMMdd") + " Check Register by Date.xlsx");
                }
            }
        }
        #endregion

        #region Check Register by Customer
        public IActionResult CheckRegisterCustomer()
        {
            return View();
        }

        public async Task<JsonResult> GetCheckRegisterByCustomer(ListFilterCustomerModel filter)
        {
            return Json(await reportManagementRepository.GetCheckRegisterByCustomer(filter));
        }

        [HttpPost]
        public async Task<JsonResult> GetCheckRegisterByCustomerDT(ListFilterCustomerModel filter)
        {
            DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
            return Json(await reportManagementRepository.GetCheckRegisterDT(dtForm, filter));
        }

        public async Task<IActionResult> DownloadCheckRegisterByCustomer(ListFilterCustomerModel filter)
        {
            DataTable dt = new DataTable("Check Register ("
                                       + filter.DateFrom.GetValueOrDefault().ToString("MMddyy")
                                       + "-"
                                       + filter.DateTo.GetValueOrDefault().ToString("MMddyy")
                                       + ")");
            dt.Columns.AddRange(new DataColumn[5] {
                new DataColumn(chelper.DisplayName<ReportManagementCheckRegisterRVM>("Customer")),
                new DataColumn(chelper.DisplayName<ReportManagementCheckRegisterRVM>("CheckDate")),
                new DataColumn(chelper.DisplayName<ReportManagementCheckRegisterRVM>("BankDetails")),
                new DataColumn(chelper.DisplayName<ReportManagementCheckRegisterRVM>("CheckNumber")),
                new DataColumn(chelper.DisplayName<ReportManagementCheckRegisterRVM>("CheckAmount"))
            });
            var checks = await reportManagementRepository.GetCheckRegisterByCustomer(filter);
            foreach (var check in checks)
            {
                dt.Rows.Add(check.Customer,
                            check.CheckDate,
                            check.BankDetails,
                            check.CheckNumber,
                            check.CheckAmount);
            }

            using (XLWorkbook wb = new())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        DateTime.Now.ToString("yyyyMMdd") + " Check Register by Customer.xlsx");
                }
            }
        }
        #endregion

        #region Check Register Summary
        public IActionResult CheckRegisterSummary()
        {
            return View();
        }

        public async Task<JsonResult> GetCheckRegisterSummary(ListFilterCustomerModel filter)
        {
            return Json(await reportManagementRepository.GetCheckRegisterSummary(filter));
        }

        [HttpPost]
        public async Task<JsonResult> GetCheckRegisterSummaryDT(ListFilterCustomerModel filter)
        {
            DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
            return Json(await reportManagementRepository.GetCheckRegisterSummaryDT(dtForm, filter));
        }

        public async Task<IActionResult> DownloadCheckRegisterSummary(ListFilterCustomerModel filter)
        {
            DataTable dt = new DataTable("Check Register ("
                                       + filter.DateFrom.GetValueOrDefault().ToString("MMddyy")
                                       + "-"
                                       + filter.DateTo.GetValueOrDefault().ToString("MMddyy")
                                       + ")");
            dt.Columns.AddRange(new DataColumn[3] {
                new DataColumn(chelper.DisplayName<ReportManagementRegisterSummaryRVM>("CheckDate")),
                new DataColumn(chelper.DisplayName<ReportManagementRegisterSummaryRVM>("NumberOfChecks")),
                new DataColumn(chelper.DisplayName<ReportManagementRegisterSummaryRVM>("CheckAmount"))
            });
            var checks = await reportManagementRepository.GetCheckRegisterSummary(filter);
            foreach (var check in checks)
            {
                dt.Rows.Add(check.CheckDate,
                            check.NumberOfChecks,
                            check.CheckAmount);
            }

            using (XLWorkbook wb = new())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        DateTime.Now.ToString("yyyyMMdd") + " Check Register Summary.xlsx");
                }
            }
        }
        #endregion
    }
}
