using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace GMGInventory.Application.Repositories
{
    public class ReportAccountingRepository : IReportAccountingRepository
    {
        private readonly ApplicationDbContext context;

        public ReportAccountingRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        #region Credit Memo

        public async Task<IList<CreditDebitMemoRVM>> GetCreditMemo(ListFilterCustomerModel filter)
        {
            var credits = from soa in context.SOACRLineItems
                          where filter.DateFrom <= soa.DateCreated.Date
                          && soa.DateCreated.Date <= filter.DateTo
                          && (filter.Customer == null || soa.Invoice.SalesOrder.Customer.Id == filter.Customer)
                          orderby soa.DateCreated descending
                          select new CreditDebitMemoRVM
                          {
                              AmountD = soa.CreditAmount,
                              Customer = soa.Invoice.SalesOrder.Customer.Name,
                              MemoDateD = soa.DateCreated,
                              MemoNumber = soa.CreditReferenceNumber
                          };
            return await credits.ToListAsync();
        }

        public async Task<ForDataTables<CreditDebitMemoRVM>> GetCreditMemoDT(DataTablesRequestForm form, ListFilterCustomerModel filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            decimal totalAmount = 0;

            var data = (await GetCreditMemo(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.MemoDate.ToLower().Contains(form.SearchValue.ToLower())
                                    || (x.MemoNumber + "").ToLower().Contains(form.SearchValue.ToLower())
                                    || x.Customer.ToLower().Contains(form.SearchValue.ToLower())
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
            //get total amount of data in table
            totalAmount = data.Sum(s => s.AmountD);
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<CreditDebitMemoRVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList,
                TotalAmount = totalAmount
            };
        }

        public async Task<CreditDebitMemoPVM> PrintCreditMemo(ListFilterCustomerModel filter)
        {
            var items = await GetCreditMemo(filter);
            var customer = await context.Customers.Where(s => s.Id == filter.Customer).FirstOrDefaultAsync();
            return new CreditDebitMemoPVM
            {
                Memos = items,
                DateFromD = filter.DateFrom.GetValueOrDefault(),
                DateToD = filter.DateTo.GetValueOrDefault(),
                Customer = filter.Customer == null || customer == null ? "All" : customer.Name,
                TotalAmountD = items.Sum(s => s.AmountD)
            };
        }

        #endregion Credit Memo

        #region Debit Memo

        public async Task<IList<CreditDebitMemoRVM>> GetDebitMemo(ListFilterCustomerModel filter)
        {
            var debits = from soa in context.SOACRLineItems
                         where filter.DateFrom <= soa.DateCreated.Date
                         && soa.DateCreated.Date <= filter.DateTo
                         && (filter.Customer == null || soa.Invoice.SalesOrder.Customer.Id == filter.Customer)
                         orderby soa.DateCreated descending
                         select new CreditDebitMemoRVM
                         {
                             AmountD = soa.DebitAmount,
                             Customer = soa.Invoice.SalesOrder.Customer.Name,
                             MemoDateD = soa.DateCreated,
                             MemoNumber = soa.DebitReferenceNumber
                         };
            return await debits.ToListAsync();
        }

        public async Task<ForDataTables<CreditDebitMemoRVM>> GetDebitMemoDT(DataTablesRequestForm form, ListFilterCustomerModel filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            decimal totalAmount = 0;

            var data = (await GetDebitMemo(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.MemoDate.ToLower().Contains(form.SearchValue.ToLower())
                                    || (x.MemoNumber + "").ToLower().Contains(form.SearchValue.ToLower())
                                    || x.Customer.ToLower().Contains(form.SearchValue.ToLower())
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
            //get total amount of data in table
            totalAmount = data.Sum(s => s.AmountD);
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<CreditDebitMemoRVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList,
                TotalAmount = totalAmount
            };
        }

        public async Task<CreditDebitMemoPVM> PrintDebitMemo(ListFilterCustomerModel filter)
        {
            var items = await GetDebitMemo(filter);
            var customer = await context.Customers.Where(s => s.Id == filter.Customer).FirstOrDefaultAsync();
            return new CreditDebitMemoPVM
            {
                Memos = items,
                DateFromD = filter.DateFrom.GetValueOrDefault(),
                DateToD = filter.DateTo.GetValueOrDefault(),
                Customer = filter.Customer == null || customer == null ? "All" : customer.Name,
                TotalAmountD = items.Sum(s => s.AmountD)
            };
        }

        #endregion Debit Memo
    }
}