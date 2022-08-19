using AutoMapper;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace GMGInventory.Application.Repositories
{
    public class SoacrRepository : ISoacrRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly string currentUser;

        public SoacrRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
            , UserManager<User> userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.currentUser = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
        }


        public SoacrVM? GetSoacrVM(int? id)
        {
            var soaVM = (from c in context.Customers
                         join soa in context.SOACRs
                                          on c.Id equals soa.CustomerId into custSoa
                         from custSoaGrouping in custSoa.DefaultIfEmpty()
                         join st in context.SOACRTypes
                                          on custSoaGrouping.SOACRTypeId equals st.Id
                         join sl in context.SOACRLineItems
                                     on custSoaGrouping.Id equals sl.SOACRId into custSoaSL
                         from custSoaGroupingSL in custSoaSL.DefaultIfEmpty()
                         join i in context.Invoices
                                          on custSoaGroupingSL.InvoiceId equals i.Id
                         where custSoaGrouping.Id == id
                         select new SoacrVM
                         {

                             Id = custSoaGrouping.Id,
                             CRNumber = custSoaGrouping.Id.ToString(),
                             CustomerName = custSoaGrouping.Customer.Name,
                             Type = st.Code,
                             SOAFrom = custSoaGrouping.SOAFrom.ToString(),
                             SOATo = custSoaGrouping.SOATo.ToString(),
                             TotalAmount = custSoaGrouping.TotalAmount.ToString(),
                             TBR = custSoaGroupingSL.TBRAmount.ToString(),
                             Credit = custSoaGroupingSL.CreditAmount.ToString(),
                             Debit = custSoaGroupingSL.DebitAmount.ToString(),
                             PaidAmount = custSoaGroupingSL.PaidAmount.ToString(),
                             FreightCharge = custSoaGroupingSL.FreightCost.ToString(),
                             Balance = custSoaGroupingSL.Balance,
                             OverPayment = custSoaGrouping.OverPayment.ToString(),
                             InvoiceNumber = i.InvoiceNumber.ToString(),
                             InvoiceAmount = i.Amount.ToString()

                         }).FirstOrDefault();
            return soaVM;
        }



        //private IQueryable<SoacrVM> GetListDetails()
        //{

        //    return from c in context.Customers
        //           join soa in context.SOACRs
        //                            on c.Id equals soa.CustomerId
        //           join st in context.SOACRTypes
        //                            on soa.SOACRTypeId equals st.Id
        //           join sl in context.SOACRLineItems
        //                            on soa.Id equals sl.SOACRId
        //           select new SoacrVM
        //           {

        //               Id = soa.Id,
        //               CRNumber = soa.Id.ToString(),
        //               CustomerName = c.Name,
        //               Type = st.Code,
        //               SOAFrom = soa.SOAFrom.ToString(),
        //               SOATo = soa.SOATo.ToString(),
        //               TotalAmount = soa.TotalAmount.ToString(),
        //               TBR = sl.TBRAmount.ToString(),
        //               Credit = sl.CreditAmount.ToString(),
        //               Debit = sl.DebitAmount.ToString(),
        //               PaidAmount = sl.PaidAmount.ToString(),
        //               FreightCharge = sl.FreightCost.ToString(),
        //               Balance = sl.Balance.ToString(),
        //               OverPayment = soa.OverPayment.ToString()

        //           };
        //}

        //public ForDataTables<PaymentVM> GetListDataTablesDetails(DataTablesRequestForm form)
        //{
        //    int totalRecord = 0;
        //    int filterRecord = 0;
        //    //var data = context.AuditLogs.Include(s=>s.User).AsQueryable();
        //    var data = GetListDetails();
        //    //get total count of data in table
        //    totalRecord = data.Count();
        //    // search data when search value found
        //    if (!string.IsNullOrWhiteSpace(form.SearchValue))
        //    {

        //        data = data.Where(x => x.CRNumber.ToLower().Contains(form.SearchValue)
        //                            || x.SOANo.ToLower().Contains(form.SearchValue)
        //                            || x.CustomerName.ToLower().Contains(form.SearchValue)
        //                            || x.TotalInvoiceAmount.ToLower().Contains(form.SearchValue)
        //                            || x.AmountPaid.ToLower().Contains(form.SearchValue)
        //                            || x.OverPayment.ToLower().Contains(form.SearchValue)
        //                            || x.TransactionDate.ToLower().Contains(form.SearchValue)
        //                            || x.CreatedBy.ToLower().Contains(form.SearchValue)
        //                            || x.Remarks.ToLower().Contains(form.SearchValue)
        //                            || x.SalesOrderNo.ToLower().Contains(form.SearchValue)
        //                            || x.CheckNo.ToLower().Contains(form.SearchValue)
        //                            || x.Tax.ToLower().Contains(form.SearchValue)
        //                            || x.Discount.ToLower().Contains(form.SearchValue)
        //                            || x.CashAmount.ToLower().Contains(form.SearchValue)
        //                            || x.TKT.ToLower().Contains(form.SearchValue)
        //                            || x.CashAdvanced.ToLower().Contains(form.SearchValue)
        //                            || x.Offset.ToLower().Contains(form.SearchValue)
        //                            || x.BackLoad.ToLower().Contains(form.SearchValue)
        //                            || x.BankName.ToLower().Contains(form.SearchValue)
        //                            || x.BankBranch.ToLower().Contains(form.SearchValue)
        //                            || x.CheckAmount.ToLower().Contains(form.SearchValue)
        //                            || x.CheckDate.ToLower().Contains(form.SearchValue)
        //                            || x.InvoiceDate.ToLower().Contains(form.SearchValue)
        //                            || x.InvoiceNumber.ToLower().Contains(form.SearchValue)
        //                            || x.InvoiceType.ToLower().Contains(form.SearchValue)
        //                            || x.InvoiceAmount.ToLower().Contains(form.SearchValue)
        //                            || x.BalanceAmount.ToLower().Contains(form.SearchValue)
        //                            || x.PackNo.ToLower().Contains(form.SearchValue));
        //    }
        //    // get total count of records after search
        //    filterRecord = data.Count();
        //    //sort data
        //    if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
        //    {
        //        if (form.SortColumnDirection == "asc")
        //        {
        //            switch (form.SortColumn)
        //            {
        //                case "CRNumber": data = data.OrderBy(s => s.CRNumber); break;
        //                case "SOANo": data = data.OrderBy(s => s.SOANo); break;
        //                case "CustomerName": data = data.OrderBy(s => s.CustomerName); break;
        //                case "TotalInvoiceAmount": data = data.OrderBy(s => s.TotalInvoiceAmount); break;
        //                case "AmountPaid": data = data.OrderBy(s => s.AmountPaid); break;
        //                case "OverPayment": data = data.OrderBy(s => s.OverPayment); break;
        //                case "TransactionDate": data = data.OrderBy(s => s.TransactionDate); break;
        //                case "CreatedBy": data = data.OrderBy(s => s.CreatedBy); break;
        //                case "Remarks": data = data.OrderBy(s => s.Remarks); break;
        //                case "SalesOrderNo": data = data.OrderBy(s => s.SalesOrderNo); break;
        //                case "CheckNo": data = data.OrderBy(s => s.CheckNo); break;
        //                case "Tax": data = data.OrderBy(s => s.Tax); break;
        //                case "Discount": data = data.OrderBy(s => s.Discount); break;
        //                case "CashAmount": data = data.OrderBy(s => s.CashAmount); break;
        //                case "TKT": data = data.OrderBy(s => s.TKT); break;
        //                case "CashAdvanced": data = data.OrderBy(s => s.CashAdvanced); break;
        //                case "Offset": data = data.OrderBy(s => s.Offset); break;
        //                case "BackLoad": data = data.OrderBy(s => s.BackLoad); break;
        //                case "BankName": data = data.OrderBy(s => s.BankName); break;
        //                case "BankBranch": data = data.OrderBy(s => s.BankBranch); break;
        //                case "CheckAmount": data = data.OrderBy(s => s.CheckAmount); break;
        //                case "CheckDate": data = data.OrderBy(s => s.CheckDate); break;
        //                case "InvoiceDate": data = data.OrderBy(s => s.InvoiceDate); break;
        //                case "InvoiceNumber": data = data.OrderBy(s => s.InvoiceNumber); break;
        //                case "InvoiceType": data = data.OrderBy(s => s.InvoiceType); break;
        //                case "InvoiceAmount": data = data.OrderBy(s => s.InvoiceAmount); break;
        //                case "BalanceAmount": data = data.OrderBy(s => s.BalanceAmount); break;
        //                case "PackNo": data = data.OrderBy(s => s.PackNo); break;


        //                default: break;
        //            }
        //        }
        //        else if (form.SortColumnDirection == "desc")
        //        {
        //            switch (form.SortColumn)
        //            {
        //                case "CRNumber": data = data.OrderByDescending(s => s.CRNumber); break;
        //                case "SOANo": data = data.OrderByDescending(s => s.SOANo); break;
        //                case "CustomerName": data = data.OrderByDescending(s => s.CustomerName); break;
        //                case "TotalInvoiceAmount": data = data.OrderByDescending(s => s.TotalInvoiceAmount); break;
        //                case "AmountPaid": data = data.OrderByDescending(s => s.AmountPaid); break;
        //                case "OverPayment": data = data.OrderByDescending(s => s.OverPayment); break;
        //                case "TransactionDate": data = data.OrderByDescending(s => s.TransactionDate); break;
        //                case "CreatedBy": data = data.OrderByDescending(s => s.CreatedBy); break;
        //                case "Remarks": data = data.OrderByDescending(s => s.Remarks); break;
        //                case "SalesOrderNo": data = data.OrderByDescending(s => s.SalesOrderNo); break;
        //                case "CheckNo": data = data.OrderByDescending(s => s.CheckNo); break;
        //                case "Tax": data = data.OrderByDescending(s => s.Tax); break;
        //                case "Discount": data = data.OrderByDescending(s => s.Discount); break;
        //                case "CashAmount": data = data.OrderByDescending(s => s.CashAmount); break;
        //                case "TKT": data = data.OrderByDescending(s => s.TKT); break;
        //                case "CashAdvanced": data = data.OrderByDescending(s => s.CashAdvanced); break;
        //                case "Offset": data = data.OrderByDescending(s => s.Offset); break;
        //                case "BackLoad": data = data.OrderByDescending(s => s.BackLoad); break;
        //                case "BankName": data = data.OrderByDescending(s => s.BankName); break;
        //                case "BankBranch": data = data.OrderByDescending(s => s.BankBranch); break;
        //                case "CheckAmount": data = data.OrderByDescending(s => s.CheckAmount); break;
        //                case "CheckDate": data = data.OrderByDescending(s => s.CheckDate); break;
        //                case "InvoiceDate": data = data.OrderByDescending(s => s.InvoiceDate); break;
        //                case "InvoiceNumber": data = data.OrderByDescending(s => s.InvoiceNumber); break;
        //                case "InvoiceType": data = data.OrderByDescending(s => s.InvoiceType); break;
        //                case "InvoiceAmount": data = data.OrderByDescending(s => s.InvoiceAmount); break;
        //                case "BalanceAmount": data = data.OrderByDescending(s => s.BalanceAmount); break;
        //                case "PackNo": data = data.OrderByDescending(s => s.PackNo); break;
        //                default: break;
        //            }
        //        }
        //    }
        //    //pagination
        //    var pageList = data.Skip(form.Skip).Take(form.PageSize);
        //    var dataList = pageList.ToList();
        //    return new ForDataTables<PaymentVM>
        //    {
        //        Draw = form.Draw,
        //        RecordsTotal = totalRecord,
        //        RecordsFiltered = filterRecord,
        //        Data = dataList
        //    };
        //}

        private IQueryable<SoacrVM> GetList()
        {

            return from c in context.Customers
                   join soa in context.SOACRs
                                    on c.Id equals soa.CustomerId into custSoa
                   from custSoaGrouping in custSoa.DefaultIfEmpty()
               
                   join st in context.SOACRTypes
                                    on custSoaGrouping.SOACRTypeId equals st.Id
                   join sl in context.SOACRLineItems
                                    on custSoaGrouping.Id equals sl.SOACRId into custSoaSL
                   from custSoaGroupingSL in custSoaSL.DefaultIfEmpty()
                   join i in context.Invoices
                                    on custSoaGroupingSL.InvoiceId equals i.Id
                   
                   

                   select new SoacrVM
                   {

                       Id = custSoaGrouping.Id,
                       CRNumber = custSoaGrouping.Id.ToString(),
                       CustomerName = custSoaGrouping.Customer.Name,
                       Type = st.Code,
                       SOAFrom = custSoaGrouping.SOAFrom.ToString(),
                       SOATo = custSoaGrouping.SOATo.ToString(),
                       TotalAmount = custSoaGrouping.TotalAmount.ToString(),
                       TBR = custSoaGroupingSL.TBRAmount.ToString(),
                       Credit = custSoaGroupingSL.CreditAmount.ToString(),
                       Debit = custSoaGroupingSL.DebitAmount.ToString(),
                       PaidAmount = custSoaGroupingSL.PaidAmount.ToString(),
                       FreightCharge = custSoaGroupingSL.FreightCost.ToString(),
                       Balance = custSoaGroupingSL.Balance,
                       OverPayment = custSoaGrouping.OverPayment.ToString(),
                       InvoiceNumber = i.InvoiceNumber.ToString(),
                       InvoiceAmount = i.Amount.ToString()

                   };

        }

        public ForDataTables<SoacrVM> GetListDataTables(DataTablesRequestForm form)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            //var data = context.AuditLogs.Include(s=>s.User).AsQueryable();
            var data = GetList();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.CustomerName.ToLower().Contains(form.SearchValue)
                                    || x.CRNumber.ToLower().Contains(form.SearchValue)
                                    || x.Type.ToLower().Contains(form.SearchValue)
                                    || x.SOAFrom.ToLower().Contains(form.SearchValue)
                                    || x.SOATo.ToLower().Contains(form.SearchValue)
                                    || x.TotalAmount.ToLower().Contains(form.SearchValue)
                                    || x.TBR.ToLower().Contains(form.SearchValue)
                                    || x.Credit.ToLower().Contains(form.SearchValue)
                                    || x.Debit.ToLower().Contains(form.SearchValue)
                                    || x.PaidAmount.ToLower().Contains(form.SearchValue)
                                    || x.FreightCharge.ToLower().Contains(form.SearchValue)
                                    || x.OverPayment.ToLower().Contains(form.SearchValue));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                if (form.SortColumnDirection == "asc")
                {
                    switch (form.SortColumn)
                    {
                        case "CRNumber": data = data.OrderBy(s => s.CRNumber); break;
                        case "CustomerName": data = data.OrderBy(s => s.CustomerName); break;
                        case "Type": data = data.OrderBy(s => s.Type); break;
                        case "SOAFrom": data = data.OrderBy(s => s.SOAFrom); break;
                        case "SOATo": data = data.OrderBy(s => s.SOATo); break;
                        case "TotalAmount": data = data.OrderBy(s => s.TotalAmount); break;
                        case "TBR": data = data.OrderBy(s => s.TBR); break;
                        case "Credit": data = data.OrderBy(s => s.Credit); break;
                        case "Debit": data = data.OrderBy(s => s.Debit); break;
                        case "PaidAmount": data = data.OrderBy(s => s.PaidAmount); break;
                        case "FreightCharge": data = data.OrderBy(s => s.FreightCharge); break;
                        case "Balance": data = data.OrderBy(s => s.Balance); break;
                        case "OverPayment": data = data.OrderBy(s => s.OverPayment); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "CRNumber": data = data.OrderByDescending(s => s.CRNumber); break;
                        case "CustomerName": data = data.OrderByDescending(s => s.CustomerName); break;
                        case "Type": data = data.OrderByDescending(s => s.Type); break;
                        case "SOAFrom": data = data.OrderByDescending(s => s.SOAFrom); break;
                        case "SOATo": data = data.OrderByDescending(s => s.SOATo); break;
                        case "TotalAmount": data = data.OrderByDescending(s => s.TotalAmount); break;
                        case "TBR": data = data.OrderByDescending(s => s.TBR); break;
                        case "Credit": data = data.OrderByDescending(s => s.Credit); break;
                        case "Debit": data = data.OrderByDescending(s => s.Debit); break;
                        case "PaidAmount": data = data.OrderByDescending(s => s.PaidAmount); break;
                        case "FreightCharge": data = data.OrderByDescending(s => s.FreightCharge); break;
                        case "Balance": data = data.OrderByDescending(s => s.Balance); break;
                        case "OverPayment": data = data.OrderByDescending(s => s.OverPayment); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<SoacrVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList
            };
        }
    }
}