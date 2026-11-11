using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using GMGInventory.Data.Migrations;
using GMGInventory.Data.OrderAppDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace GMGInventory.Application.Repositories
{
    public class ReportAgentRepository : IAgentReport
    {
        private readonly ApplicationDbContext context;
        private readonly AppOrderDbContext app_context;
        private readonly string currentUser;

        public ReportAgentRepository(ApplicationDbContext context
                                    , AppOrderDbContext app_context
                                    , IHttpContextAccessor httpContextAccessor
                                    , UserManager<User> userManager)
        {
            this.context = context;
            this.app_context = app_context;
            this.currentUser = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
        }

        public async Task<IList<AgentItirenaryVM>> GetAgentItineraryDT(ListFilterCustomerModel filter)
        {
            var agentId = filter.Agent?.ToString();

            // 1. Customer names from the Customers context (materialized, only needed if filtering by customer)
            List<string> customerNames = null;
            if (filter.Customer != null)
            {
                customerNames = await context.Customers
                    .Where(c => c.Id == filter.Customer)
                    .Select(c => c.Name)
                    .ToListAsync();
            }

            // 2. Visits from app_context only
            var query = app_context.CustomerVisit
                .Where(cv => filter.DateFrom <= cv.CreatedDate.Value.Date
                          && cv.CreatedDate.Value.Date <= filter.DateTo);

            if (agentId != null)
                query = query.Where(cv => cv.AgentID == agentId);

            if (customerNames != null)
                query = query.Where(cv => customerNames.Contains(cv.CustomerName));

            return await query
                .Select(cv => new AgentItirenaryVM
                {
                    VisitDate = cv.VisitDate,
                    CustomerCode = cv.CustomerCode,
                    CustomerName = cv.CustomerName,
                    AgentId = cv.AgentID,
                    AgentName = cv.AgentName,
                    SalesAmount = cv.SalesAmount,
                    Lat = cv.Lat,
                    Long = cv.Long,
                    PurposeOfVisit = cv.PurposeOfVisit
                })
                .ToListAsync();
        }

        public async Task<ForDataTables<AgentItirenaryVM>> GetAgentDT(DataTablesRequestForm form, ListFilterCustomerModel filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;

            var data = (await GetAgentItineraryDT(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.VisitDate.ToString().Contains(form.SearchValue.ToLower())
                                    || (x.CustomerCode + "").ToLower().Contains(form.SearchValue.ToLower())
                                    || x.CustomerName.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.SalesAmount.ToString().Contains(form.SearchValue));
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
                        case "VisitDate": data = data.OrderBy(s => s.VisitDate); break;
                        case "CustomerCode": data = data.OrderBy(s => s.CustomerCode); break;
                        case "CustomerName": data = data.OrderBy(s => s.CustomerName); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "VisitDate": data = data.OrderByDescending(s => s.VisitDate); break;
                        case "CustomerCode": data = data.OrderByDescending(s => s.CustomerCode); break;
                        case "CustomerName": data = data.OrderByDescending(s => s.CustomerName); break;
                       
                        default: break;
                    }
                }
            }
            //get total amount of data in table
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<AgentItirenaryVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList
            };
        }

        public async Task<AgentWeeklyVM> PrintAgentWeeklyReport(ListFilterCustomerModel filter)
        {
            var items = await GetAgentItineraryDT(filter);
            var customer = await context.Customers.Where(s => s.Id == filter.Customer).FirstOrDefaultAsync();
            return new AgentWeeklyVM
            {
                Rows = items,
                DateFrom = filter.DateFrom.GetValueOrDefault(),
                DateTo = filter.DateTo.GetValueOrDefault(),
                Customer = customer?.Name ?? "All",
                AgentName = items.FirstOrDefault()?.AgentName ?? "",
                SalesForWeek = items.Sum(r => r.SalesAmount ?? 0),
                CollectionForWeek = items.Sum(r => r.Collection ?? 0)
            };
        }

        public async Task<AgentMonthlyVM> PrintAgentMonthlyReport(ListFilterCustomerModel filter)
        {
            var items = await GetAgentItineraryDT(filter);

            return new AgentMonthlyVM
            {
                Rows = items,
                DateFrom = filter.DateFrom.GetValueOrDefault(),
                DateTo = filter.DateTo.GetValueOrDefault(),
                AgentName = items.FirstOrDefault()?.AgentName ?? ""
            };
        }

    }
}