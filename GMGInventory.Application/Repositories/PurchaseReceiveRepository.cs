using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace GMGInventory.Application.Repositories
{
    public class PurchaseReceiveReporitory : IPurchaseReceiveRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;

        public PurchaseReceiveReporitory(ApplicationDbContext context
                                       , IHttpContextAccessor httpContextAccessor
                                       , UserManager<User> userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        private async Task<IList<PurchaseReceiveVM>> GetListAsync()
        {
            var requestVM = (from request in context.PurchaseReceivings
                                                .Include(s => s.Supplier)
                                                .Include(s => s.Term)
                                                .Include(s => s.PurchaseOrder)
                                                .Include(s => s.DeliveryAddress)
                                                .Include(s => s.Status)
                           select new PurchaseReceiveVM
                           {
                               Id = request.Id,
                               DocumentNumber = request.DocumentNumber,
                               Supplier = request.Supplier.Code,
                               Term = request.Term.Code,
                               PONumber = request.PurchaseOrder.DocumentNumber,
                               DeliveryAddress = request.DeliveryAddress.Code,
                               Status = request.Status.Code,
                               ReceiveDateD = request.ReceiveDate
                           });
            return await requestVM.ToListAsync();
        }

        public async Task<ForDataTables<PurchaseReceiveVM>> GetListDTAsync(DataTablesRequestForm form)
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
                    s.Supplier,
                    s.Term,
                    s.PONumber,
                    s.DeliveryAddress,
                    s.Status,
                    s.ReceiveDate
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
                            case "ReceiveDate": data = data.OrderBy(s => s.ReceiveDateD); break;
                            default: break;
                        }
                    }
                    else if (form.SortColumnDirection == "desc")
                    {
                        switch (form.SortColumn)
                        {
                            case "ReceiveDate": data = data.OrderByDescending(s => s.ReceiveDateD); break;
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
            return new ForDataTables<PurchaseReceiveVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }
    }
}