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
    public class AccountingReportController : Controller
    {
        private readonly IReportAccountingRepository reportAccountingRepository;
        private readonly ControllerHelper chelper;

        public AccountingReportController(IReportAccountingRepository reportAccountingRepository)
        {
            this.reportAccountingRepository = reportAccountingRepository;
            chelper = new();
        }

        #region Credit Memo
        public IActionResult CreditMemo()
        {
            return View();
        }

        public async Task<IActionResult> PrintCreditMemo(ListFilterCustomerModel filter)
        {
            return View(await reportAccountingRepository.PrintCreditMemo(filter));
        }

        [HttpPost]
        public async Task<JsonResult> GetCreditMemoDT(ListFilterCustomerModel filter)
        {
            DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
            return Json(await reportAccountingRepository.GetCreditMemoDT(dtForm, filter));
        }

        public async Task<IActionResult> DownloadCreditMemo(ListFilterCustomerModel filter)
        {
            DataTable dt = new DataTable("Credit Memo " 
                                       + filter.DateFrom.GetValueOrDefault().ToString("MMddyy") 
                                       + "-" 
                                       + filter.DateTo.GetValueOrDefault().ToString("MMddyy"));
            dt.Columns.AddRange(new DataColumn[4] {
                new DataColumn(chelper.DisplayName<CreditDebitMemoRVM>("MemoDate")),
                new DataColumn(chelper.DisplayName<CreditDebitMemoRVM>("MemoNumber")),
                new DataColumn(chelper.DisplayName<CreditDebitMemoRVM>("Customer")),
                new DataColumn(chelper.DisplayName<CreditDebitMemoRVM>("Amount"))
            });
            var credits = await reportAccountingRepository.GetCreditMemo(filter);
            foreach (var credit in credits)
            {
                dt.Rows.Add(credit.MemoDate,
                            credit.MemoNumber,
                            credit.Customer,
                            credit.Amount);
            }

            using (XLWorkbook wb = new())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        DateTime.Now.ToString("yyyyMMdd") + " Credit Memo Listing Report.xlsx");
                }
            }
        }
        #endregion

        #region Debit Memo
        public IActionResult DebitMemo()
        {
            return View();
        }

        public async Task<IActionResult> PrintDebitMemo(ListFilterCustomerModel filter)
        {
            return View(await reportAccountingRepository.PrintDebitMemo(filter));
        }

        [HttpPost]
        public async Task<JsonResult> GetDebitMemoDT(ListFilterCustomerModel filter)
        {
            DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
            return Json(await reportAccountingRepository.GetDebitMemoDT(dtForm, filter));
        }

        public async Task<IActionResult> DownloadDebitMemo(ListFilterCustomerModel filter)
        {
            DataTable dt = new DataTable("Debit Memo "
                                       + filter.DateFrom.GetValueOrDefault().ToString("MMddyy")
                                       + "-"
                                       + filter.DateTo.GetValueOrDefault().ToString("MMddyy"));
            CreditDebitMemoRVM debitVM = new CreditDebitMemoRVM();
            dt.Columns.AddRange(new DataColumn[4] {
                new DataColumn(chelper.DisplayName<CreditDebitMemoRVM>("MemoDate")),
                new DataColumn(chelper.DisplayName<CreditDebitMemoRVM>("MemoNumber")),
                new DataColumn(chelper.DisplayName<CreditDebitMemoRVM>("Customer")),
                new DataColumn(chelper.DisplayName<CreditDebitMemoRVM>("Amount"))
            });
            var debits = await reportAccountingRepository.GetDebitMemo(filter);
            foreach (var debit in debits)
            {
                dt.Rows.Add(debit.MemoDate,
                            debit.MemoNumber,
                            debit.Customer,
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
                        DateTime.Now.ToString("yyyyMMdd") + " Debit Memo Listing Report.xlsx");
                }
            }
        }
        #endregion
    }
}