using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Globalization;

namespace GMGInventory.Application.Repositories
{
    public class ReportManagementRepository : IReportManagementRepository
    {
        private readonly ApplicationDbContext context;

        public ReportManagementRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        private async Task<IList<CheckRegisterRVM>> GetCheckRegister(ListFilterCustomerModel filter)
        {
            var checks = from check in context.SOACRChecks
                                              .Include(s => s.CustomerPayment).ThenInclude(s => s.Customer)
                         where filter.DateFrom <= check.CheckDate
                         && check.CheckDate <= filter.DateTo
                         && (filter.Customer == null || check.CustomerPayment.Customer.Id == filter.Customer)
                         select new CheckRegisterRVM
                         {
                             BankDetails = check.BankName + " - " + check.BankBranch,
                             CheckAmountD = check.CheckAmount,
                             CheckDateD = check.CheckDate,
                             CheckNumber = check.CheckNumber,
                             Customer = check.CustomerPayment.Customer.Name
                         };
            return await checks.ToListAsync();
        }

        #region Check Register by Date / Customer
        public async Task<IList<CheckRegisterRVM>> GetCheckRegisterByDate(ListFilterCustomerModel filter)
        {
            return (await GetCheckRegister(filter)).OrderBy(s => s.CheckDateD).ThenBy(s => s.Customer).ToList();
        }

        public async Task<IList<CheckRegisterRVM>> GetCheckRegisterByCustomer(ListFilterCustomerModel filter)
        {
            return (await GetCheckRegister(filter)).OrderBy(s => s.Customer).ThenBy(s => s.CheckDateD).ToList();
        }

        public async Task<ForDataTables<CheckRegisterRVM>> GetCheckRegisterDT(DataTablesRequestForm form, ListFilterCustomerModel filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            decimal totalAmount = 0;

            var data = (await GetCheckRegister(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            //get total amount of data in table
            totalAmount = data.Sum(s => s.CheckAmountD);
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.BankDetails.ToLower().Contains(form.SearchValue)
                                    || x.CheckAmount.ToString().Contains(form.SearchValue)
                                    || x.CheckDate.ToLower().Contains(form.SearchValue)
                                    || x.CheckNumber.ToLower().Contains(form.SearchValue)
                                    || x.Customer.ToLower().Contains(form.SearchValue));
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
                        case "BankDetails": data = data.OrderBy(s => s.BankDetails); break;
                        case "CheckAmount": data = data.OrderBy(s => s.CheckAmountD); break;
                        case "CheckDate": data = data.OrderBy(s => s.CheckDateD); break;
                        case "CheckNumber": data = data.OrderBy(s => s.CheckNumber); break;
                        case "Customer": data = data.OrderBy(s => s.Customer); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "BankDetails": data = data.OrderByDescending(s => s.BankDetails); break;
                        case "CheckAmount": data = data.OrderByDescending(s => s.CheckAmountD); break;
                        case "CheckDate": data = data.OrderByDescending(s => s.CheckDateD); break;
                        case "CheckNumber": data = data.OrderByDescending(s => s.CheckNumber); break;
                        case "Customer": data = data.OrderByDescending(s => s.Customer); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<CheckRegisterRVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList,
                TotalAmount = totalAmount
            };
        }

        public async Task<CheckRegisterPVM> PrintCheckRegisterByDate(ListFilterCustomerModel filter)
        {
            var items = await GetCheckRegisterByDate(filter);
            var customer = await context.Customers.Where(s => s.Id == filter.Customer).FirstOrDefaultAsync();
            return new CheckRegisterPVM
            {
                Registers = items,
                DateFromD = filter.DateFrom.GetValueOrDefault(),
                DateToD = filter.DateTo.GetValueOrDefault(),
                TotalAmountD = items.Sum(s => s.CheckAmountD)
            };
        }

        public async Task<CheckRegisterPVM> PrintCheckRegisterByCustomer(ListFilterCustomerModel filter)
        {
            var items = await GetCheckRegisterByCustomer(filter);
            var customer = await context.Customers.Where(s => s.Id == filter.Customer).FirstOrDefaultAsync();
            return new CheckRegisterPVM
            {
                Registers = items,
                DateFromD = filter.DateFrom.GetValueOrDefault(),
                DateToD = filter.DateTo.GetValueOrDefault(),
                Customer = filter.Customer == null || customer == null ? "All" : customer.Name,
                TotalAmountD = items.Sum(s => s.CheckAmountD)
            };
        }
        #endregion

        #region Check Register Summary
        public async Task<IList<RegisterSummaryRVM>> GetCheckRegisterSummary(ListFilterCommonModel filter)
        {
            var checks = from check in context.SOACRChecks
                         group check by check.CheckDate.Date into d
                         where filter.DateFrom <= d.Key
                         && d.Key <= filter.DateTo
                         orderby d.Key ascending
                         select new RegisterSummaryRVM
                         {
                             CheckAmountD = d.Sum(s => s.CheckAmount),
                             CheckDateD = d.Key,
                             NumberOfChecksD = d.Count()
                         };
            return await checks.ToListAsync();
        }

        public async Task<ForDataTables<RegisterSummaryRVM>> GetCheckRegisterSummaryDT(DataTablesRequestForm form, ListFilterCommonModel filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            decimal totalAmount = 0;
            int totalQuantity = 0;

            var data = (await GetCheckRegisterSummary(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            //get total amount of data in table
            totalAmount = data.Sum(s => s.CheckAmountD);
            //get total quantity of data in table
            totalQuantity = data.Sum(s => s.NumberOfChecksD);
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.CheckAmountD.ToString().Contains(form.SearchValue)
                                    || x.CheckDate.ToLower().Contains(form.SearchValue)
                                    || x.NumberOfChecksD.ToString().Contains(form.SearchValue));
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
                        case "CheckAmount": data = data.OrderBy(s => s.CheckAmountD); break;
                        case "CheckDate": data = data.OrderBy(s => s.CheckDateD); break;
                        case "NumberOfChecks": data = data.OrderBy(s => s.NumberOfChecksD); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "CheckAmount": data = data.OrderByDescending(s => s.CheckAmountD); break;
                        case "CheckDate": data = data.OrderByDescending(s => s.CheckDateD); break;
                        case "NumberOfChecks": data = data.OrderByDescending(s => s.NumberOfChecksD); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<RegisterSummaryRVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList,
                TotalAmount = totalAmount,
                TotalQuantity = totalQuantity
            };
        }
        public async Task<RegisterSummaryPVM> PrintCheckRegisterSummary(ListFilterCommonModel filter)
        {
            var items = await GetCheckRegisterSummary(filter);
            return new RegisterSummaryPVM
            {
                Registers = items,
                DateFromD = filter.DateFrom.GetValueOrDefault(),
                DateToD = filter.DateTo.GetValueOrDefault(),
                TotalChecksD = items.Sum(s => s.NumberOfChecksD),
                TotalAmountD = items.Sum(s => s.CheckAmountD)
            };
        }
        #endregion
    }
}