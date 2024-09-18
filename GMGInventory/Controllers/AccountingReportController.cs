using ClosedXML.Excel;
using GMGInventory.Application.Contracts;
using GMGInventory.Application.Repositories;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GMGInventory.Controllers
{
    public class AccountingReportController(IAccessRepository accessRepository
            , IReportAccountingRepository reportAccountingRepository
            , UserManager<User> userManager) : BaseController(accessRepository, userManager)
    {
        private readonly ControllerHelper chelper = new();

        #region Credit Memo

        public async Task<IActionResult> CreditMemo()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.CreditMemoListingReport, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.CreditMemoListingReport)).Convert();

            return View();
        }

        public async Task<IActionResult> PrintCreditMemo(ListFilterCustomerModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.CreditMemoListingReport, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;

            return View(await reportAccountingRepository.PrintCreditMemo(filter));
        }

        [HttpPost]
        public async Task<IActionResult> GetCreditMemoDT(ListFilterCustomerModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.CreditMemoListingReport, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

             DataTablesRequestForm dtForm = new(Request.Form);
            return Json(await reportAccountingRepository.GetCreditMemoDT(dtForm, filter));
        }

        public async Task<IActionResult> DownloadCreditMemo(ListFilterCustomerModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.CreditMemoListingReport, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            DataTable dt = new("Credit Memo "
                             + filter.DateFrom.GetValueOrDefault().ToString("MMddyy")
                             + "-"
                             + filter.DateTo.GetValueOrDefault().ToString("MMddyy"));
            dt.Columns.AddRange([
                new DataColumn(chelper.DisplayName<CreditDebitMemoRVM>("MemoDate")),
                new DataColumn(chelper.DisplayName<CreditDebitMemoRVM>("MemoNumber")),
                new DataColumn(chelper.DisplayName<CreditDebitMemoRVM>("Customer")),
                new DataColumn(chelper.DisplayName<CreditDebitMemoRVM>("Amount"))
            ]);
            var credits = await reportAccountingRepository.GetCreditMemo(filter);
            foreach (var credit in credits)
            {
                dt.Rows.Add(credit.MemoDate,
                            credit.MemoNumber,
                            credit.Customer,
                            credit.Amount);
            }

            using XLWorkbook wb = new();
            wb.Worksheets.Add(dt);
            using MemoryStream stream = new();
            wb.SaveAs(stream);
            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                DateTime.Now.ToString("yyyyMMdd") + " Credit Memo Listing Report.xlsx");
        }

        #endregion Credit Memo

        #region Debit Memo

        public async Task<IActionResult> DebitMemo()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.DebitMemoListingReport, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.DebitMemoListingReport)).Convert();

            return View();
        }

        public async Task<IActionResult> PrintDebitMemo(ListFilterCustomerModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.DebitMemoListingReport, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;

            return View(await reportAccountingRepository.PrintDebitMemo(filter));
        }

        [HttpPost]
        public async Task<IActionResult> GetDebitMemoDT(ListFilterCustomerModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.DebitMemoListingReport, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm dtForm = new(Request.Form);
            return Json(await reportAccountingRepository.GetDebitMemoDT(dtForm, filter));
        }

        public async Task<IActionResult> DownloadDebitMemo(ListFilterCustomerModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.DebitMemoListingReport, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            DataTable dt = new("Debit Memo "
                             + filter.DateFrom.GetValueOrDefault().ToString("MMddyy")
                             + "-"
                             + filter.DateTo.GetValueOrDefault().ToString("MMddyy"));
            dt.Columns.AddRange([
                new DataColumn(chelper.DisplayName<CreditDebitMemoRVM>("MemoDate")),
                new DataColumn(chelper.DisplayName<CreditDebitMemoRVM>("MemoNumber")),
                new DataColumn(chelper.DisplayName<CreditDebitMemoRVM>("Customer")),
                new DataColumn(chelper.DisplayName<CreditDebitMemoRVM>("Amount"))
            ]);
            var debits = await reportAccountingRepository.GetDebitMemo(filter);
            foreach (var debit in debits)
            {
                dt.Rows.Add(debit.MemoDate,
                            debit.MemoNumber,
                            debit.Customer,
                            debit.Amount);
            }

            using XLWorkbook wb = new();
            wb.Worksheets.Add(dt);
            using MemoryStream stream = new();
            wb.SaveAs(stream);
            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                DateTime.Now.ToString("yyyyMMdd") + " Debit Memo Listing Report.xlsx");
        }

        #endregion Debit Memo
    }
}