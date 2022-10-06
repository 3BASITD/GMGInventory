using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Security.AccessControl;

namespace GMGInventory.Application.Repositories
{
    public class ReportAccountingRepository : IReportAccountingRepository
    {
        private readonly ApplicationDbContext context;

        public ReportAccountingRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IList<CreditDebitMemoVM>> GetCreditMemo(ListFilterCustomerModel filter)
        {
            var credits = from credit in context.CreditDebitMemos
                                                .Include(s => s.CreditDebitMemoType)
                          join invoice in context.Invoices
                                                 .Include(s => s.SalesOrder)
                                                 .ThenInclude(s => s.Customer) on credit.InvoiceId equals invoice.Id
                          where credit.CreditDebitMemoType.Name == "Credit"
                          && filter.DateFrom <= credit.DateCreated
                          && credit.DateCreated <= filter.DateTo
                          && (filter.Customer == null || invoice.SalesOrder.Customer.Id == filter.Customer)
                          orderby credit.DateCreated descending
                          select new CreditDebitMemoVM
                          {
                              AmountD = credit.Amount,
                              Customer = invoice.SalesOrder.Customer.Name,
                              MemoDateD = credit.DateCreated,
                              MemoNumber = credit.MemoNumber
                          };
            return await credits.ToListAsync();
        }

        public async Task<ForDataTables<CreditDebitMemoVM>> GetCreditMemoDT(DataTablesRequestForm form, ListFilterCustomerModel filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;

            var data = (await GetCreditMemo(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.MemoDate.ToLower().Contains(form.SearchValue)
                                    || x.MemoNumber.ToLower().Contains(form.SearchValue)
                                    || x.Customer.ToLower().Contains(form.SearchValue)
                                    || x.AmountD.ToString().Contains(form.SearchValue));
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
                        case "MemoDate": data = data.OrderBy(s => s.MemoDate); break;
                        case "MemoNumber": data = data.OrderBy(s => s.MemoNumber); break;
                        case "Customer": data = data.OrderBy(s => s.Customer); break;
                        case "Amount": data = data.OrderBy(s => s.AmountD); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "MemoDate": data = data.OrderByDescending(s => s.MemoDate); break;
                        case "MemoNumber": data = data.OrderByDescending(s => s.MemoNumber); break;
                        case "Customer": data = data.OrderByDescending(s => s.Customer); break;
                        case "Amount": data = data.OrderByDescending(s => s.AmountD); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<CreditDebitMemoVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList
            };
        }

        public async Task<IList<CreditDebitMemoVM>> GetDebitMemo(ListFilterCustomerModel filter)
        {
            var debits = from debit in context.CreditDebitMemos
                                              .Include(s => s.CreditDebitMemoType)
                         join invoice in context.Invoices
                                                .Include(s => s.SalesOrder)
                                                .ThenInclude(s => s.Customer) on debit.InvoiceId equals invoice.Id
                         where debit.CreditDebitMemoType.Name == "Debit"
                         && filter.DateFrom <= debit.DateCreated
                         && debit.DateCreated <= filter.DateTo
                         && (filter.Customer == null || invoice.SalesOrder.Customer.Id == filter.Customer)
                         orderby debit.DateCreated descending
                         select new CreditDebitMemoVM
                         {
                             AmountD = debit.Amount,
                             Customer = invoice.SalesOrder.Customer.Name,
                             MemoDateD = debit.DateCreated,
                             MemoNumber = debit.MemoNumber
                         };
            return await debits.ToListAsync();
        }

        public async Task<ForDataTables<CreditDebitMemoVM>> GetDebitMemoDT(DataTablesRequestForm form, ListFilterCustomerModel filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;

            var data = (await GetDebitMemo(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.MemoDate.ToLower().Contains(form.SearchValue)
                                    || x.MemoNumber.ToLower().Contains(form.SearchValue)
                                    || x.Customer.ToLower().Contains(form.SearchValue)
                                    || x.AmountD.ToString().Contains(form.SearchValue));
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
                        case "MemoDate": data = data.OrderBy(s => s.MemoDate); break;
                        case "MemoNumber": data = data.OrderBy(s => s.MemoNumber); break;
                        case "Customer": data = data.OrderBy(s => s.Customer); break;
                        case "Amount": data = data.OrderBy(s => s.AmountD); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "MemoDate": data = data.OrderByDescending(s => s.MemoDate); break;
                        case "MemoNumber": data = data.OrderByDescending(s => s.MemoNumber); break;
                        case "Customer": data = data.OrderByDescending(s => s.Customer); break;
                        case "Amount": data = data.OrderByDescending(s => s.AmountD); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<CreditDebitMemoVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList
            };
        }
    }
}