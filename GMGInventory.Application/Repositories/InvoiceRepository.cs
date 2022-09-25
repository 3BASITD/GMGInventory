using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Application.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;

        public InvoiceRepository(ApplicationDbContext context
             , UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        private async Task<IList<InvoiceVM>> GetListAsync(ListFilterCommonModel filter)
        {
            var invoices = from inv in context.Invoices
                                           .Include(s => s.InvoiceType)
                                           .Include(s => s.Status)
                                           .Include(s => s.SalesOrder)
                                               .ThenInclude(s => s.Customer)
                                           .Where(s => (filter.StatusId == null || s.StatusId == filter.StatusId)
                                                     && filter.DateFrom <= s.DateCreated
                                                     && s.DateCreated <= filter.DateTo)
                           join usr in userManager.Users on inv.UserCreated equals usr.Id
                        select new InvoiceVM
                        {
                            Customer = inv.SalesOrder.Customer.Code,
                            Id = inv.Id,
                            InvoiceDateD = inv.DateCreated,
                            InvoiceNumber = inv.InvoiceNumber,
                            InvoiceType = inv.InvoiceType.Code,
                            PackNumber = inv.PackId + "",
                            SONumber = inv.SalesOrder.SalesOrderNumber,
                            Status = inv.Status.Code,
                            TotalAmountD = inv.Amount
                        };

            return await invoices.ToListAsync();
        }

        public async Task<ForDataTables<InvoiceVM>> GetListDataTablesAsync(DataTablesRequestForm form, ListFilterCommonModel filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetListAsync(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Customer.ToLower().Contains(form.SearchValue)
                                    || x.Id.ToString().Contains(form.SearchValue)
                                    || x.InvoiceDate.ToLower().Contains(form.SearchValue)
                                    || x.InvoiceNumber.ToLower().Contains(form.SearchValue)
                                    || x.InvoiceType.ToLower().Contains(form.SearchValue)
                                    || x.PackNumber.ToLower().Contains(form.SearchValue)
                                    || x.SONumber.ToLower().Contains(form.SearchValue)
                                    || x.Status.ToLower().Contains(form.SearchValue)
                                    || x.TotalAmountD.ToString().Contains(form.SearchValue));
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
                        case "Customer": data = data.OrderBy(s => s.Customer); break;
                        case "Id": data = data.OrderBy(s => s.Id); break;
                        case "InvoiceDate": data = data.OrderBy(s => s.InvoiceDateD); break;
                        case "InvoiceNumber": data = data.OrderBy(s => s.InvoiceNumber); break;
                        case "InvoiceType": data = data.OrderBy(s => s.InvoiceType); break;
                        case "PackNumber": data = data.OrderBy(s => s.PackNumber); break;
                        case "SONumber": data = data.OrderBy(s => s.SONumber); break;
                        case "Status": data = data.OrderBy(s => s.Status); break;
                        case "TotalAmount": data = data.OrderBy(s => s.TotalAmountD); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Customer": data = data.OrderByDescending(s => s.Customer); break;
                        case "Id": data = data.OrderByDescending(s => s.Id); break;
                        case "InvoiceDate": data = data.OrderByDescending(s => s.InvoiceDateD); break;
                        case "InvoiceNumber": data = data.OrderByDescending(s => s.InvoiceNumber); break;
                        case "InvoiceType": data = data.OrderByDescending(s => s.InvoiceType); break;
                        case "PackNumber": data = data.OrderByDescending(s => s.PackNumber); break;
                        case "SONumber": data = data.OrderByDescending(s => s.SONumber); break;
                        case "Status": data = data.OrderByDescending(s => s.Status); break;
                        case "TotalAmount": data = data.OrderByDescending(s => s.TotalAmountD); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<InvoiceVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList
            };
        }
    }
}