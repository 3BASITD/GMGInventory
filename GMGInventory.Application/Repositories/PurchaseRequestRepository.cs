using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace GMGInventory.Application.Repositories
{
    public class PurchaseRequestReporitory : IPurchaseRequestRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;

        public PurchaseRequestReporitory(ApplicationDbContext context
                                       , IHttpContextAccessor httpContextAccessor
                                       , UserManager<User> userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        private async Task<IList<PurchaseRequestVM>> GetListAsync()
        {
            var requestVM = (from request in context.PurchaseRequests
                                                .Include(s => s.Type)
                                                .Include(s => s.Purpose)
                                                .Include(s => s.Area)
                                                .Include(s => s.Status)
                           select new PurchaseRequestVM
                           {
                               Id = request.Id,
                               DocumentNumber = request.DocumentNumber,
                               Type = request.Type.Code,
                               Purpose = request.Purpose.Code,
                               Area = request.Area.Code,
                               Status = request.Status.Code,
                               RequestDateD = request.RequestDate
                           });
            return await requestVM.ToListAsync();
        }

        public async Task<ForDataTables<PurchaseRequestVM>> GetListDTAsync(DataTablesRequestForm form)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetListAsync()).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(s => new[]
                {
                    s.DocumentNumber,
                    s.Type,
                    s.Purpose,
                    s.Area,
                    s.Status,
                    s.RequestDate
                }.Any(a => a != null && a.Contains(form.SearchValue, StringComparison.CurrentCultureIgnoreCase)));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                if (form.SortColumn.Contains("date", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (form.SortColumnDirection == "asc")
                    {
                        switch (form.SortColumn)
                        {
                            case "RequestDate": data = data.OrderBy(s => s.RequestDateD); break;
                            default: break;
                        }
                    }
                    else if (form.SortColumnDirection == "desc")
                    {
                        switch (form.SortColumn)
                        {
                            case "RequestDate": data = data.OrderByDescending(s => s.RequestDateD); break;
                            default: break;
                        }
                    }
                } else
                {
                    data = data.OrderBy(form.SortColumn + " " + form.SortColumnDirection);
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<PurchaseRequestVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }
    }
}