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
            var paymentVM = (from cp in context.CustomerPayments
                                     .Include(s => s.Customer)

                             where cp.Id == id
                             select new PaymentVM
                             {
                                 Id = cp.Id,
                                 CRNumber = cp.CRNumber,
                                 SOANo = cp.SOACRId.ToString(),
                                 CustomerName = cp.Customer.Name,
                                 TotalInvoiceAmount = cp.TotalInvoiceAmount.ToString(SystemFormat.StringDecimalComma),
                                 AmountPaid = cp.TotalAmountPaid.ToString(SystemFormat.StringDecimalComma),
                                 OverPayment = cp.OverPayment.ToString(SystemFormat.StringDecimalComma),
                                 TransactionDate = cp.DateCreated.ToString(SystemFormat.StringDateTime),
                                 CreatedBy = cp.UserCreated,
                                 Remarks = cp.Remarks

                             }).FirstOrDefault();
            return paymentVM;
        }


        private IQueryable<PaymentVM> GetList()
        {

            return 
                   from cp in context.CustomerPayments
                                     .Include (s => s.Customer)
                   select new PaymentVM
                   {
                       Id = cp.Id,
                       CRNumber = cp.CRNumber,
                       SOANo = cp.SOACRId.ToString(),
                       CustomerName = cp.Customer.Name,
                       TotalInvoiceAmount = cp.TotalInvoiceAmount.ToString(SystemFormat.StringDecimalComma),
                       AmountPaid = cp.TotalAmountPaid.ToString(SystemFormat.StringDecimalComma),
                       OverPayment = cp.OverPayment.ToString(SystemFormat.StringDecimalComma),
                       TransactionDate = cp.DateCreated.ToString(SystemFormat.StringDateTime),
                       CreatedBy = cp.UserCreated,
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

        private async Task<IList<PaymentVM>> GetPaymentChecksAsync(int id)
        {
            var customerVM = from cp in context.CustomerPayments
                                               .Include(s => s.Customer)
                             join p in context.Payments
                                              on cp.Customer.Id equals p.CustomerId
                             join pd in context.PaymentDetails
                                              on p.Id equals pd.PaymentId
                             where cp.Id == id
                             select new PaymentVM
                             {
                                 CheckNo = pd.CheckNumber.ToString(),
                                 CheckAmount = pd.CheckAmount.ToString(),
                                 CheckDate = pd.CheckDate.ToString(),
                                 BankName = pd.BankName.ToString(),
                                 BankBranch = pd.BankBranch.ToString()

                             };
            return await customerVM.ToListAsync();
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
            var customerVM = from cp in context.CustomerPayments
                                               .Include(s => s.Customer)
                             join p in context.Payments
                                              on cp.Customer.Id equals p.CustomerId
                             join pd in context.PaymentDetails
                                              on p.Id equals pd.PaymentId
                             where cp.Id == id
                             select new PaymentVM
                             {
                                 CheckNo = pd.CheckNumber.ToString(),
                                 CheckAmount = pd.CheckAmount.ToString(),
                                 CheckDate = pd.CheckDate.ToString(),
                                 BankName = pd.BankName.ToString(),
                                 BankBranch = pd.BankBranch.ToString()

                             };
            return await customerVM.ToListAsync();
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


    }

}