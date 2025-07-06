using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using GMGInventory.Data.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static Humanizer.On;

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
            var paymentVM = (from cp in context.CustomerPayments
                                 //let soalit = context.SOACRLineItems.Where(s => s.SOACRId == cp.SOACRId).ToList()
                                 //let tbr = context.SOACRLineItemTBRs.Where(s => soalit.Select(a => a.Id).Contains(s.SOACRLineItemId)).Sum(s => s.Amount)
                             let sliamt = (from sli in context.SOACRLineItems
                                           where sli.SOACRId == cp.SOACRId
                                           select (decimal?)(sli.DebitAmount + sli.FreightCost - sli.CreditAmount)).Sum() ?? 0
                             let tbramt = (from tbr in context.SOACRLineItemTBRs
                                           join sli in context.SOACRLineItems on tbr.SOACRLineItemId equals sli.Id
                                           where sli.SOACRId == cp.SOACRId
                                           select (decimal?)tbr.Amount).Sum() ?? 0
                             let invamt = (from ili in context.InvoiceLineItems
                                           join inv in context.Invoices on ili.InvoiceId equals inv.Id
                                           join sli in context.SOACRLineItems on inv.Id equals sli.InvoiceId
                                           where sli.SOACRId == cp.SOACRId
                                           select (decimal?)ili.Quantity * ili.UnitPrice).Sum() ?? 0
                             let chkamt = (from chk in context.SOACRChecks
                                           where chk.CustomerPaymentId == cp.Id
                                           select (decimal?)chk.CheckAmount).Sum() ?? 0
                             let payamt = cp.BackLoad + cp.CashAdvance + cp.Cash  + cp.OffSet + cp.OverPayment + cp.Tax + cp.TKTSolicitation + chkamt //+ cp.Discount
                             let invamttotal = invamt - sliamt - tbramt - payamt 
                             where cp.Id == id
                             select new PaymentVM
                             {
                                 Id = cp.Id,
                                 CRNumber = cp.CRNumber,
                                 SOANo = cp.SOACRId,
                                 CustomerName = cp.Customer.Name,
                                 OverPaymentD =  cp.OverPayment, //0 > invamttotal ? invamttotal * -1 : 0,
                                 TaxD = cp.Tax,
                                 DiscountD = cp.Discount,
                                 TKTD = cp.TKTSolicitation,
                                 OffsetD = cp.OffSet,
                                 BackLoadD = cp.BackLoad,
                                 CashAmountD = cp.Cash,
                                 CashAdvancedD = cp.CashAdvance,
                                 Remarks = cp.Remarks,
                                 AmountPaidD = payamt,
                                 TotalInvoiceAmountD = invamttotal
                             }).FirstOrDefault();
            return paymentVM;
        }

        private async Task<List<PaymentVM>> GetList()
        {
            var customerPayment = (from cp in context.CustomerPayments
                                   let sliamt = (from sli in context.SOACRLineItems
                                                 where sli.SOACRId == cp.SOACRId
                                                 select (decimal?)(sli.DebitAmount + sli.FreightCost - sli.CreditAmount)).Sum() ?? 0
                                   let tbramt = (from tbr in context.SOACRLineItemTBRs
                                                 join sli in context.SOACRLineItems on tbr.SOACRLineItemId equals sli.Id
                                                 where sli.SOACRId == cp.SOACRId
                                                 select (decimal?)tbr.Amount).Sum() ?? 0
                                   let invamt = (from ili in context.InvoiceLineItems
                                                 join inv in context.Invoices on ili.InvoiceId equals inv.Id
                                                 join sli in context.SOACRLineItems on inv.Id equals sli.InvoiceId
                                                 where sli.SOACRId == cp.SOACRId
                                                 select (decimal?)ili.Quantity * ili.UnitPrice).Sum() ?? 0
                                   //let invamt = (from paydetails in context.CustomerPaymentDetails
                                   //              where paydetails.CustomerPayment.Id == cp.Id
                                   //              select (decimal?)paydetails.Invoice.InvoiceLineItems.Sum(a => a.Quantity * a.UnitPrice)).FirstOrDefault() ?? 0

                                   let chkamt = (from chk in context.SOACRChecks
                                                 where chk.CustomerPaymentId == cp.Id
                                                 select (decimal?)chk.CheckAmount).Sum() ?? 0
                                   let payamt = cp.BackLoad + cp.CashAdvance + cp.Cash  + cp.OffSet + cp.OverPayment + cp.Tax + cp.TKTSolicitation + chkamt // + cp.Discount
                                   let invamttotal = invamt - tbramt 
                                   let overpaytotal = invamt - tbramt + sliamt - payamt 

                                   join user in userManager.Users on cp.UserCreated equals user.Id
                                   select new PaymentVM
                                   {
                                       Id = cp.Id,
                                       CRNumber = cp.CRNumber,
                                       SOANo = cp.SOACRId,
                                       CustomerName = cp.Customer.Name,
                                       TotalInvoiceAmountD = invamttotal,
                                       AmountPaidD = payamt,
                                       OverPaymentD =  0 > overpaytotal + cp.Discount ? overpaytotal * -1 + cp.Discount: 0,
                                       TransactionDateD = cp.DateCreated,
                                       CreatedBy = user.FirstName + " " + user.LastName,
                                       Remarks = cp.Remarks
                                   });
            return await customerPayment.ToListAsync();
        }

        public async Task<ForDataTables<PaymentVM>> GetListDataTables(DataTablesRequestForm form)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            //var data = context.AuditLogs.Include(s=>s.User).AsQueryable();
            var data = (await GetList()).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.CRNumber.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.SOANo.ToString().Contains(form.SearchValue)
                                    || x.CustomerName.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.TotalInvoiceAmount.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.AmountPaid.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.OverPayment.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.TransactionDate.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.CreatedBy.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.Remarks.ToLower().Contains(form.SearchValue.ToLower()));
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
                        case "TotalInvoiceAmount": data = data.OrderBy(s => s.TotalInvoiceAmountD); break;
                        case "AmountPaid": data = data.OrderBy(s => s.AmountPaidD); break;
                        case "OverPayment": data = data.OrderBy(s => s.OverPaymentD); break;
                        case "TransactionDate": data = data.OrderBy(s => s.TransactionDateD); break;
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
                        case "TotalInvoiceAmount": data = data.OrderByDescending(s => s.TotalInvoiceAmountD); break;
                        case "AmountPaid": data = data.OrderByDescending(s => s.AmountPaidD); break;
                        case "OverPayment": data = data.OrderByDescending(s => s.OverPaymentD); break;
                        case "TransactionDate": data = data.OrderByDescending(s => s.TransactionDateD); break;
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

        private async Task<IList<PaymentVM>> GetPaymentChecksAsync(int id)
        {
            //var customerVM = from cp in context.CustomerPayments
            //                 join sc in context.SOACRChecks
            //                         on cp.Id equals sc.CustomerPaymentId
            //                 where cp.Id == id
            //                 select new PaymentVM
            //                 {
            //                     CheckNo = sc.CheckNumber,
            //                     CheckAmountD = sc.CheckAmount,
            //                     CheckDateD = sc.CheckDate,
            //                     BankName = sc.BankName,
            //                     BankBranch = sc.BankBranch
            //                 };
            //return await customerVM.ToListAsync();

            var checkVM = from check in context.SOACRChecks
                          where check.CustomerPaymentId == id
                          select new PaymentVM
                          {
                              CheckNo = check.CheckNumber,
                              CheckAmountD = check.CheckAmount,
                              CheckDateD = check.CheckDate,
                              BankName = check.BankName,
                              BankBranch = check.BankBranch
                          };
            return await checkVM.ToListAsync();
        }

        public async Task<ForDataTables<PaymentVM>> GetPaymentChecksDTAsync(DataTablesRequestForm form, int id)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetPaymentChecksAsync(id)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.CheckNo.ToString().Contains(form.SearchValue)
                                    || x.CheckAmount.ToString().Contains(form.SearchValue)
                                    || x.CheckDate.ToString().Contains(form.SearchValue)
                                    || x.BankName.ToString().Contains(form.SearchValue)
                                    || x.BankBranch.ToString().Contains(form.SearchValue));
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
                        case "CheckNo": data = data.OrderBy(s => s.CheckNo); break;
                        case "CheckAmount": data = data.OrderBy(s => s.CheckAmount); break;
                        case "CheckDate": data = data.OrderBy(s => s.CheckDate); break;
                        case "BankName": data = data.OrderBy(s => s.BankName); break;
                        case "BankBranch": data = data.OrderBy(s => s.BankBranch); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "CheckNo": data = data.OrderByDescending(s => s.CheckNo); break;
                        case "CheckAmount": data = data.OrderByDescending(s => s.CheckAmount); break;
                        case "CheckDate": data = data.OrderByDescending(s => s.CheckDate); break;
                        case "BankName": data = data.OrderByDescending(s => s.BankName); break;
                        case "BankBranch": data = data.OrderByDescending(s => s.BankBranch); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<PaymentVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        private async Task<IList<PaymentVM>> GetPaymentInvoicesAsync(int id)
        {
            //var customerVM = from inv in context.Invoices
            //                 join soali in context.SOACRLineItems
            //                            on inv.Id equals soali.InvoiceId
            //                 join soacr in context.SOACRs
            //                            on soali.SOACRId equals soacr.Id into soacrs
            //                 from soacrsGroup in soacrs.DefaultIfEmpty()
            //                 join c in context.CustomerPayments
            //                            on soacrsGroup.Id equals c.SOACRId

            //                 where c.Id == id
            //                 select new PaymentVM
            //                 {
            //                     InvoiceNumber = inv.InvoiceNumber,
            //                     InvoiceDateD = inv.DateModified ?? inv.DateCreated,
            //                     InvoiceType = inv.InvoiceType.Name,
            //                     InvoiceAmountD = inv.Amount,
            //                     BalanceAmountD = inv.PaidAmount,
            //                     PackNo = inv.Pack.QuantityInvoiced,
            //                     SalesOrderNo = inv.SalesOrder.SalesOrderNumber
            //                 };
            //return await customerVM.ToListAsync();

            var payment = from pay in context.CustomerPaymentDetails
                          let paid = context.CustomerPaymentDetails
                                            .Where(s=>s.InvoiceId == pay.InvoiceId)
                                            .Sum(s=>(decimal?) s.PaidAmount) ?? 0
                          where pay.CustomerPayment.Id == id
                          select new PaymentVM
                          {
                              InvoiceNumber = pay.Invoice.InvoiceNumber,
                              InvoiceDateD = pay.Invoice.InvoiceDate,
                              InvoiceType = pay.Invoice.InvoiceType.Name,
                              InvoiceAmountD = pay.Invoice.InvoiceLineItems.Sum(a => a.Quantity * a.UnitPrice),
                              BalanceAmountD = Math.Max(0, pay.Invoice.InvoiceLineItems.Sum(a => a.Quantity * a.UnitPrice) - paid),
                              PackNo = pay.Invoice.Pack.Id,
                              SalesOrderNo = pay.Invoice.SalesOrder.SalesOrderNumber
                          };
            return await payment.ToListAsync();
        }

        public async Task<ForDataTables<PaymentVM>> GetPaymentInvoicesDTAsync(DataTablesRequestForm form, int id)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetPaymentInvoicesAsync(id)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.InvoiceNumber.ToString().Contains(form.SearchValue)
                                    || x.InvoiceDateD.ToString().Contains(form.SearchValue)
                                    || x.InvoiceType.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.InvoiceAmountD.ToString().Contains(form.SearchValue)
                                    || x.BalanceAmountD.ToString().Contains(form.SearchValue)
                                    || x.PackNo.ToString().Contains(form.SearchValue)
                                    || x.SalesOrderNo.ToString().Contains(form.SearchValue));
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
                        case "InvoiceNumber": data = data.OrderBy(s => s.InvoiceNumber); break;
                        case "InvoiceDate": data = data.OrderBy(s => s.InvoiceDate); break;
                        case "InvoiceType": data = data.OrderBy(s => s.InvoiceType); break;
                        case "InvoiceAmount": data = data.OrderBy(s => s.InvoiceAmount); break;
                        case "BalanceAmount": data = data.OrderBy(s => s.BalanceAmount); break;
                        case "PackNo": data = data.OrderBy(s => s.PackNo); break;
                        case "SalesOrderNo": data = data.OrderBy(s => s.SalesOrderNo); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "InvoiceNumber": data = data.OrderByDescending(s => s.InvoiceNumber); break;
                        case "InvoiceDate": data = data.OrderByDescending(s => s.InvoiceDate); break;
                        case "InvoiceType": data = data.OrderByDescending(s => s.InvoiceType); break;
                        case "InvoiceAmount": data = data.OrderByDescending(s => s.InvoiceAmount); break;
                        case "BalanceAmount": data = data.OrderByDescending(s => s.BalanceAmount); break;
                        case "PackNo": data = data.OrderByDescending(s => s.PackNo); break;
                        case "SalesOrderNo": data = data.OrderByDescending(s => s.SalesOrderNo); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<PaymentVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }
    }
}