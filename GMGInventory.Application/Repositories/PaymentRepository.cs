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
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly string currentUser;

        public PaymentRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
            , UserManager<User> userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.currentUser = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
        }


        public PaymentVM? GetPaymentVM(int? id)
        {
            var paymentVM = (from c in context.Customers
                                 join cp in context.CustomerPayments
                                                  on c.Id equals cp.CustomerId
                                 join p in context.Payments
                                                   .Include(s => s.Customer)
                                                  on c.Id equals p.CustomerId
                                 join pd in context.PaymentDetails
                                                  on p.Id equals pd.PaymentId
                                 join i in context.Invoices
                                                  on pd.InvoiceId equals i.Id
                                 join ip in context.InvoicePayments
                                                  on i.Id equals ip.InvoiceId
                                 join it in context.InvoiceTypes
                                                 on i.InvoiceTypeId equals it.Id

                                 join u in userManager.Users on c.UserCreated equals u.Id
                                 where cp.Id == id
                                 select new PaymentVM
                                 {
                                     Id = cp.Id,
                                     CRNumber = cp.CRNumber,
                                     SOANo = cp.SOACRId.ToString(),
                                     CustomerName = c.Name,
                                     TotalInvoiceAmount = cp.TotalInvoiceAmount.ToString($"{0:#,##0.00}"),
                                     AmountPaid = cp.TotalAmountPaid.ToString($"{0:#,##0.00}"),
                                     OverPayment = cp.OverPayment.ToString($"{0:#,##0.00}"),
                                     TransactionDate = cp.DateCreated.ToString(SystemFormat.StringDateTime),
                                     CreatedBy = u.Nickname,
                                     Remarks = cp.Remarks,
                                     SalesOrderNo = i.SalesOrderId.ToString(),
                                     CheckNo = ip.CheckNumber,
                                     Tax = cp.Tax.ToString(),
                                     Discount = cp.Discount.ToString($"{0:#,##0.00}"),
                                     CashAmount = cp.Cash.ToString($"{0:#,##0.00}"),
                                     TKT = cp.TKTSolicitation.ToString(),
                                     CashAdvanced = cp.CashAdvance.ToString($"{0:#,##0.00}"),
                                     Offset = cp.OffSet.ToString(),
                                     BackLoad = cp.BackLoad.ToString(),
                                     BankName = ip.BankName,
                                     BankBranch = ip.BankBranch,
                                     CheckAmount = ip.CheckAmount.ToString($"{0:#,##0.00}"),
                                     CheckDate = ip.CheckDate.ToString(SystemFormat.StringDateTime),
                                     InvoiceDate = i.DateCreated.ToString(SystemFormat.StringDateTime),
                                     InvoiceNumber = i.InvoiceNumber,
                                     InvoiceType = i.InvoiceTypeId.ToString(),
                                     InvoiceAmount = i.Amount.ToString($"{0:#,##0.00}"),
                                     BalanceAmount = i.PaidAmount.ToString($"{0:#,##0.00}"),
                                     PackNo = i.PackId.ToString()


                                 }).FirstOrDefault();
            return paymentVM;
        }

        private async Task<string> GetUserName(string? userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            return user != null ? user.Nickname : "-";
        }

        private IQueryable<PaymentVM> GetListDetails()
        {

           return from c in context.Customers
            join cp in context.CustomerPayments
                             on c.Id equals cp.CustomerId
            join p in context.Payments
                                .Include(s => s.Customer)
                             on c.Id equals p.CustomerId
            join pd in context.PaymentDetails
                             on p.Id equals pd.PaymentId
            join i in context.Invoices
                             on pd.InvoiceId equals i.Id
            join ip in context.InvoicePayments
                             on i.Id equals ip.InvoiceId
            join it in context.InvoiceTypes
                             on i.InvoiceTypeId equals it.Id

            join u in userManager.Users on c.UserCreated equals u.Id
            select new PaymentVM
            {
                Id = cp.Id,
                CRNumber = cp.CRNumber,
                SOANo = cp.SOACRId.ToString(),
                CustomerName = c.Name,
                TotalInvoiceAmount = cp.TotalInvoiceAmount.ToString($"{0:#,##0.00}"),
                AmountPaid = cp.TotalAmountPaid.ToString($"{0:#,##0.00}"),
                OverPayment = cp.OverPayment.ToString($"{0:#,##0.00}"),
                TransactionDate = cp.DateCreated.ToString(SystemFormat.StringDateTime),
                CreatedBy = u.Nickname,
                Remarks = cp.Remarks,
                SalesOrderNo = i.SalesOrderId.ToString(),
                CheckNo = ip.CheckNumber,
                Tax = cp.Tax.ToString(),
                Discount = cp.Discount.ToString($"{0:#,##0.00}"),
                CashAmount = cp.Cash.ToString($"{0:#,##0.00}"),
                TKT = cp.TKTSolicitation.ToString(),
                CashAdvanced = cp.CashAdvance.ToString($"{0:#,##0.00}"),
                Offset = cp.OffSet.ToString(),
                BackLoad = cp.BackLoad.ToString(),
                BankName = ip.BankName,
                BankBranch = ip.BankBranch,
                CheckAmount = ip.CheckAmount.ToString($"{0:#,##0.00}"),
                CheckDate = ip.CheckDate.ToString(SystemFormat.StringDateTime),
                InvoiceDate = i.DateCreated.ToString(SystemFormat.StringDateTime),
                InvoiceNumber = i.InvoiceNumber,
                InvoiceType = i.InvoiceTypeId.ToString(),
                InvoiceAmount = i.Amount.ToString($"{0:#,##0.00}"),
                BalanceAmount = i.PaidAmount.ToString($"{0:#,##0.00}"),
                PackNo = i.PackId.ToString()


            };
        }

        public ForDataTables<PaymentVM> GetListDataTablesDetails(DataTablesRequestForm form)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            //var data = context.AuditLogs.Include(s=>s.User).AsQueryable();
            var data = GetListDetails();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
             
                data = data.Where(x => x.CRNumber.ToLower().Contains(form.SearchValue)
                                    || x.SOANo.ToLower().Contains(form.SearchValue)
                                    || x.CustomerName.ToLower().Contains(form.SearchValue)
                                    || x.TotalInvoiceAmount.ToLower().Contains(form.SearchValue)
                                    || x.AmountPaid.ToLower().Contains(form.SearchValue)
                                    || x.OverPayment.ToLower().Contains(form.SearchValue)
                                    || x.TransactionDate.ToLower().Contains(form.SearchValue)
                                    || x.CreatedBy.ToLower().Contains(form.SearchValue)
                                    || x.Remarks.ToLower().Contains(form.SearchValue)
                                    || x.SalesOrderNo.ToLower().Contains(form.SearchValue)
                                    || x.CheckNo.ToLower().Contains(form.SearchValue)
                                    || x.Tax.ToLower().Contains(form.SearchValue)
                                    || x.Discount.ToLower().Contains(form.SearchValue)
                                    || x.CashAmount.ToLower().Contains(form.SearchValue)
                                    || x.TKT.ToLower().Contains(form.SearchValue)
                                    || x.CashAdvanced.ToLower().Contains(form.SearchValue)
                                    || x.Offset.ToLower().Contains(form.SearchValue)
                                    || x.BackLoad.ToLower().Contains(form.SearchValue)
                                    || x.BankName.ToLower().Contains(form.SearchValue)
                                    || x.BankBranch.ToLower().Contains(form.SearchValue)
                                    || x.CheckAmount.ToLower().Contains(form.SearchValue)
                                    || x.CheckDate.ToLower().Contains(form.SearchValue)
                                    || x.InvoiceDate.ToLower().Contains(form.SearchValue)
                                    || x.InvoiceNumber.ToLower().Contains(form.SearchValue)
                                    || x.InvoiceType.ToLower().Contains(form.SearchValue)
                                    || x.InvoiceAmount.ToLower().Contains(form.SearchValue)
                                    || x.BalanceAmount.ToLower().Contains(form.SearchValue)
                                    || x.PackNo.ToLower().Contains(form.SearchValue));
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
                        case "SOANo": data = data.OrderBy(s => s.SOANo); break;
                        case "CustomerName": data = data.OrderBy(s => s.CustomerName); break;
                        case "TotalInvoiceAmount": data = data.OrderBy(s => s.TotalInvoiceAmount); break;
                        case "AmountPaid": data = data.OrderBy(s => s.AmountPaid); break;
                        case "OverPayment": data = data.OrderBy(s => s.OverPayment); break;
                        case "TransactionDate": data = data.OrderBy(s => s.TransactionDate); break;
                        case "CreatedBy": data = data.OrderBy(s => s.CreatedBy); break;
                        case "Remarks": data = data.OrderBy(s => s.Remarks); break;
                        case "SalesOrderNo": data = data.OrderBy(s => s.SalesOrderNo); break;
                        case "CheckNo": data = data.OrderBy(s => s.CheckNo); break;
                        case "Tax": data = data.OrderBy(s => s.Tax); break;
                        case "Discount": data = data.OrderBy(s => s.Discount); break;
                        case "CashAmount": data = data.OrderBy(s => s.CashAmount); break;
                        case "TKT": data = data.OrderBy(s => s.TKT); break;
                        case "CashAdvanced": data = data.OrderBy(s => s.CashAdvanced); break;
                        case "Offset": data = data.OrderBy(s => s.Offset); break;
                        case "BackLoad": data = data.OrderBy(s => s.BackLoad); break;
                        case "BankName": data = data.OrderBy(s => s.BankName); break;
                        case "BankBranch": data = data.OrderBy(s => s.BankBranch); break;
                        case "CheckAmount": data = data.OrderBy(s => s.CheckAmount); break;
                        case "CheckDate": data = data.OrderBy(s => s.CheckDate); break;
                        case "InvoiceDate": data = data.OrderBy(s => s.InvoiceDate); break;
                        case "InvoiceNumber": data = data.OrderBy(s => s.InvoiceNumber); break;
                        case "InvoiceType": data = data.OrderBy(s => s.InvoiceType); break;
                        case "InvoiceAmount": data = data.OrderBy(s => s.InvoiceAmount); break;
                        case "BalanceAmount": data = data.OrderBy(s => s.BalanceAmount); break;
                        case "PackNo": data = data.OrderBy(s => s.PackNo); break;


                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "CRNumber": data = data.OrderByDescending(s => s.CRNumber); break;
                        case "SOANo": data = data.OrderByDescending(s => s.SOANo); break;
                        case "CustomerName": data = data.OrderByDescending(s => s.CustomerName); break;
                        case "TotalInvoiceAmount": data = data.OrderByDescending(s => s.TotalInvoiceAmount); break;
                        case "AmountPaid": data = data.OrderByDescending(s => s.AmountPaid); break;
                        case "OverPayment": data = data.OrderByDescending(s => s.OverPayment); break;
                        case "TransactionDate": data = data.OrderByDescending(s => s.TransactionDate); break;
                        case "CreatedBy": data = data.OrderByDescending(s => s.CreatedBy); break;
                        case "Remarks": data = data.OrderByDescending(s => s.Remarks); break;
                        case "SalesOrderNo": data = data.OrderByDescending(s => s.SalesOrderNo); break;
                        case "CheckNo": data = data.OrderByDescending(s => s.CheckNo); break;
                        case "Tax": data = data.OrderByDescending(s => s.Tax); break;
                        case "Discount": data = data.OrderByDescending(s => s.Discount); break;
                        case "CashAmount": data = data.OrderByDescending(s => s.CashAmount); break;
                        case "TKT": data = data.OrderByDescending(s => s.TKT); break;
                        case "CashAdvanced": data = data.OrderByDescending(s => s.CashAdvanced); break;
                        case "Offset": data = data.OrderByDescending(s => s.Offset); break;
                        case "BackLoad": data = data.OrderByDescending(s => s.BackLoad); break;
                        case "BankName": data = data.OrderByDescending(s => s.BankName); break;
                        case "BankBranch": data = data.OrderByDescending(s => s.BankBranch); break;
                        case "CheckAmount": data = data.OrderByDescending(s => s.CheckAmount); break;
                        case "CheckDate": data = data.OrderByDescending(s => s.CheckDate); break;
                        case "InvoiceDate": data = data.OrderByDescending(s => s.InvoiceDate); break;
                        case "InvoiceNumber": data = data.OrderByDescending(s => s.InvoiceNumber); break;
                        case "InvoiceType": data = data.OrderByDescending(s => s.InvoiceType); break;
                        case "InvoiceAmount": data = data.OrderByDescending(s => s.InvoiceAmount); break;
                        case "BalanceAmount": data = data.OrderByDescending(s => s.BalanceAmount); break;
                        case "PackNo": data = data.OrderByDescending(s => s.PackNo); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<PaymentVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList
            };
        }

        private IQueryable<PaymentVM> GetList()
        {
            
            return from c in context.Customers
                   join cp in context.CustomerPayments
                                 //   .Include(s => s.SOACR)
                                    on c.Id equals cp.CustomerId
                   //join p in context.Payments
                   //                 on c.Id equals p.CustomerId
                   //join pd in context.PaymentDetails
                   //                 on p.Id equals pd.PaymentId
                   //join i in context.Invoices
                   //                 on pd.InvoiceId equals i.Id
                   //join ip in context.InvoicePayments
                   //                 on i.Id equals ip.InvoiceId
                   
                   join u in userManager.Users on c.UserCreated equals u.Id
                   select new PaymentVM
                   {
                       Id = cp.Id,
                       CRNumber = cp.CRNumber,
                       SOANo = cp.SOACRId.ToString(),
                       CustomerName = c.Name,
                       TotalInvoiceAmount = cp.TotalInvoiceAmount.ToString($"{0:#,##0.00}"),
                       AmountPaid = cp.TotalAmountPaid.ToString($"{0:#,##0.00}"),
                       OverPayment = cp.OverPayment.ToString($"{0:#,##0.00}"),
                       TransactionDate = cp.DateCreated.ToString(SystemFormat.StringDateTime),
                       CreatedBy = u.Nickname,
                       Remarks = cp.Remarks


                   };

        }

        public ForDataTables<PaymentVM> GetListDataTables(DataTablesRequestForm form)
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
                data = data.Where(x => x.CRNumber.ToLower().Contains(form.SearchValue)
                                    || x.SOANo.ToLower().Contains(form.SearchValue)
                                    || x.CustomerName.ToLower().Contains(form.SearchValue)
                                    || x.TotalInvoiceAmount.ToLower().Contains(form.SearchValue)
                                    || x.AmountPaid.ToLower().Contains(form.SearchValue)
                                    || x.OverPayment.ToLower().Contains(form.SearchValue)
                                    || x.TransactionDate.ToLower().Contains(form.SearchValue)
                                    || x.CreatedBy.ToLower().Contains(form.SearchValue)
                                    || x.Remarks.ToLower().Contains(form.SearchValue));
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
                        case "SOANo": data = data.OrderBy(s => s.SOANo); break;
                        case "CustomerName": data = data.OrderBy(s => s.CustomerName); break;
                        case "TotalInvoiceAmount": data = data.OrderBy(s => s.TotalInvoiceAmount); break;
                        case "AmountPaid": data = data.OrderBy(s => s.AmountPaid); break;
                        case "OverPayment": data = data.OrderBy(s => s.OverPayment); break;
                        case "TransactionDate": data = data.OrderBy(s => s.TransactionDate); break;
                        case "CreatedBy": data = data.OrderBy(s => s.CreatedBy); break;
                        case "Remarks": data = data.OrderBy(s => s.Remarks); break;

                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "CRNumber": data = data.OrderByDescending(s => s.CRNumber); break;
                        case "SOANo": data = data.OrderByDescending(s => s.SOANo); break;
                        case "CustomerName": data = data.OrderByDescending(s => s.CustomerName); break;
                        case "TotalInvoiceAmount": data = data.OrderByDescending(s => s.TotalInvoiceAmount); break;
                        case "AmountPaid": data = data.OrderByDescending(s => s.AmountPaid); break;
                        case "OverPayment": data = data.OrderByDescending(s => s.OverPayment); break;
                        case "TransactionDate": data = data.OrderByDescending(s => s.TransactionDate); break;
                        case "CreatedBy": data = data.OrderByDescending(s => s.CreatedBy); break;
                        case "Remarks": data = data.OrderByDescending(s => s.Remarks); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<PaymentVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList
            };
        }
    }
}