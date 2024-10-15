using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Buffers;
using System.Linq.Dynamic.Core;

namespace GMGInventory.Application.Repositories
{
    public class PurchaseReturnNewReporitory : IPurchaseReturnNewRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;

        public PurchaseReturnNewReporitory(ApplicationDbContext context
                                        , IHttpContextAccessor httpContextAccessor
                                        , UserManager<User> userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        private async Task<IList<PurchaseReturnNewVM>> GetListAsync()
        {
            var returnVM = from retrn in context.PurchaseReturnNews
                                                .Include(s => s.Supplier)
                                                .Include(s => s.PurchaseOrder)
                                                .Include(s => s.Warehouse)
                                                .Include(s => s.Status)
                           select new PurchaseReturnNewVM
                           {
                               Id = retrn.Id,
                               DocumentNumber = retrn.DocumentNumber,
                               Supplier = retrn.Supplier.Code,
                               PONumber = retrn.PurchaseOrder.DocumentNumber,
                               Warehouse = retrn.Warehouse.Code,
                               DRNumber = retrn.DeliveryReceiptNumber,
                               SINumber = retrn.SalesInvoiceNumber,
                               Status = retrn.Status.Code,
                               ReceiveDateD = retrn.ReceiveDate
                           };
            return await returnVM.ToListAsync();
        }

        public async Task<ForDataTables<PurchaseReturnNewVM>> GetListDTAsync(DataTablesRequestForm form)
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
                    s.PONumber,
                    s.Warehouse,
                    s.DRNumber,
                    s.SINumber,
                    s.Status,
                    s.ReceiveDate,
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
                }
                else
                {
                    data = data.OrderBy(form.SortColumn + " " + form.SortColumnDirection);
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<PurchaseReturnNewVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }
    }
}