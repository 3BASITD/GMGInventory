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

        public async Task<IList<ReportManagementCheckRegister>> GetCheckRegisterByDate(ListFilterCommonModel filter)
        {
            //var checks = from check in context.CustomerPayments

            return await context.Users.Select(s => new ReportManagementCheckRegister
            {
                
            }).ToListAsync(); // This is temporary
        }

        public async Task<IList<ReportManagementCheckRegister>> GetCheckRegisterByCustomer(ListFilterCustomerModel filter)
        {
            return await context.Users.Select(s => new ReportManagementCheckRegister
            {

            }).ToListAsync(); // This is temporary
        }

        public async Task<IList<ReportManagementRegisterSummary>> GetCheckRegisterSummary(ListFilterCommonModel filter)
        {
            return await context.Users.Select(s => new ReportManagementRegisterSummary
            {

            }).ToListAsync(); // This is temporary
        }

        public async Task<ForDataTables<ReportManagementRegisterSummary>> GetCheckRegisterSummaryDT(DataTablesRequestForm form, ListFilterCommonModel filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;

            var data = (await GetCheckRegisterSummary(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.CheckDate.ToLower().Contains(form.SearchValue)
                                    || x.NumberOfChecks.ToString().Contains(form.SearchValue)
                                    || x.CheckAmountD.ToString().Contains(form.SearchValue));
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
                        case "CheckDate": data = data.OrderBy(s => s.CheckDateD); break;
                        case "NumberOfChecks": data = data.OrderBy(s => s.NumberOfChecks); break;
                        case "CheckAmount": data = data.OrderBy(s => s.CheckAmountD); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "CheckDate": data = data.OrderByDescending(s => s.CheckDateD); break;
                        case "NumberOfChecks": data = data.OrderByDescending(s => s.NumberOfChecks); break;
                        case "CheckAmount": data = data.OrderByDescending(s => s.CheckAmountD); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<ReportManagementRegisterSummary>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList
            };
        }
    }
}