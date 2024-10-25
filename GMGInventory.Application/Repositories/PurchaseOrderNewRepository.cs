using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace GMGInventory.Application.Repositories
{
    public class PurchaseOrderNewReporitory : IPurchaseOrderNewRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;

        public PurchaseOrderNewReporitory(ApplicationDbContext context
                                        , IHttpContextAccessor httpContextAccessor
                                        , UserManager<User> userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        private async Task<IList<PurchaseOrderNewVM>> GetListAsync()
        {
            var orderVM = (from order in context.PurchaseOrderNews
                                                .Include(s => s.Type)
                                                .Include(s => s.Supplier)
                                                .Include(s => s.Term)
                                                .Include(s => s.DeliveryMode)
                                                .Include(s => s.Status)
                           //join userc in userManager.Users on order.UserCreated equals userc.Id
                           //join userm in userManager.Users on order.UserModified equals userm.Id into m
                           //from um in m.DefaultIfEmpty()
                           select new PurchaseOrderNewVM
                           {
                               Id = order.Id,
                               DocumentNumber = order.DocumentNumber,
                               Type = order.Type.Code,
                               Supplier = order.Supplier.Code,
                               Term = order.Term.Code,
                               DeliveryMode = order.DeliveryMode.Code,
                               Status = order.Status.Code,
                               OrderDateD = order.OrderDate
                           });
            return await orderVM.ToListAsync();
        }

        public async Task<ForDataTables<PurchaseOrderNewVM>> GetListDTAsync(DataTablesRequestForm form)
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
                    s.Supplier,
                    s.Term,
                    s.DeliveryMode,
                    s.Status,
                    s.OrderDate
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
                            case "OrderDate": data = data.OrderBy(s => s.OrderDateD); break;
                            default: break;
                        }
                    }
                    else if (form.SortColumnDirection == "desc")
                    {
                        switch (form.SortColumn)
                        {
                            case "OrderDate": data = data.OrderByDescending(s => s.OrderDateD); break;
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
            return new ForDataTables<PurchaseOrderNewVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }
    }
}