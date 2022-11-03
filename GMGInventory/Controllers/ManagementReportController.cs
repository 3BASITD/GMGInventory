using ClosedXML.Excel;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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

        public async Task<IActionResult> PrintCheckRegisterByDate(ListFilterCustomerModel filter)
        {
            filter.Customer = null;
            return View(await reportManagementRepository.PrintCheckRegisterByDate(filter));
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
            DataTable dt = new DataTable("Check Register "
                                       + filter.DateFrom.GetValueOrDefault().ToString("MMddyy")
                                       + "-"
                                       + filter.DateTo.GetValueOrDefault().ToString("MMddyy"));
            dt.Columns.AddRange(new DataColumn[5] {
                new DataColumn(chelper.DisplayName<CheckRegisterRVM>("CheckDate")),
                new DataColumn(chelper.DisplayName<CheckRegisterRVM>("Customer")),
                new DataColumn(chelper.DisplayName<CheckRegisterRVM>("BankDetails")),
                new DataColumn(chelper.DisplayName<CheckRegisterRVM>("CheckNumber")),
                new DataColumn(chelper.DisplayName<CheckRegisterRVM>("CheckAmount"))
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

        #endregion Check Register by Date

        #region Check Register by Customer

        public IActionResult CheckRegisterCustomer()
        {
            return View();
        }

        public async Task<IActionResult> PrintCheckRegisterByCustomer(ListFilterCustomerModel filter)
        {
            return View(await reportManagementRepository.PrintCheckRegisterByCustomer(filter));
        }

        [HttpPost]
        public async Task<JsonResult> GetCheckRegisterByCustomerDT(ListFilterCustomerModel filter)
        {
            DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
            return Json(await reportManagementRepository.GetCheckRegisterDT(dtForm, filter));
        }

        public async Task<IActionResult> DownloadCheckRegisterByCustomer(ListFilterCustomerModel filter)
        {
            DataTable dt = new DataTable("Check Register "
                                       + filter.DateFrom.GetValueOrDefault().ToString("MMddyy")
                                       + "-"
                                       + filter.DateTo.GetValueOrDefault().ToString("MMddyy"));
            dt.Columns.AddRange(new DataColumn[5] {
                new DataColumn(chelper.DisplayName<CheckRegisterRVM>("Customer")),
                new DataColumn(chelper.DisplayName<CheckRegisterRVM>("CheckDate")),
                new DataColumn(chelper.DisplayName<CheckRegisterRVM>("BankDetails")),
                new DataColumn(chelper.DisplayName<CheckRegisterRVM>("CheckNumber")),
                new DataColumn(chelper.DisplayName<CheckRegisterRVM>("CheckAmount"))
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

        #endregion Check Register by Customer

        #region Check Register Summary

        public IActionResult CheckRegisterSummary()
        {
            return View();
        }

        public async Task<IActionResult> PrintCheckRegisterSummary(ListFilterCustomerModel filter)
        {
            return View(await reportManagementRepository.PrintCheckRegisterSummary(filter));
        }

        [HttpPost]
        public async Task<JsonResult> GetCheckRegisterSummaryDT(ListFilterCustomerModel filter)
        {
            DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
            return Json(await reportManagementRepository.GetCheckRegisterSummaryDT(dtForm, filter));
        }

        public async Task<IActionResult> DownloadCheckRegisterSummary(ListFilterCustomerModel filter)
        {
            DataTable dt = new DataTable("Check Register "
                                       + filter.DateFrom.GetValueOrDefault().ToString("MMddyy")
                                       + "-"
                                       + filter.DateTo.GetValueOrDefault().ToString("MMddyy"));
            dt.Columns.AddRange(new DataColumn[3] {
                new DataColumn(chelper.DisplayName<RegisterSummaryRVM>("CheckDate")),
                new DataColumn(chelper.DisplayName<RegisterSummaryRVM>("NumberOfChecks")),
                new DataColumn(chelper.DisplayName<RegisterSummaryRVM>("CheckAmount"))
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

        #endregion Check Register Summary
    }
}