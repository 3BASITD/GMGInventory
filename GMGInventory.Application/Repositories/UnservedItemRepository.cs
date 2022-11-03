using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Application.Repositories
{
    public class UnservedItemRepository : IUnservedItemRepository
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;

        public UnservedItemRepository(ApplicationDbContext context,
            UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<IList<UnservedItemVM>> GetListAsync()
        {
            var salesOrder = from s in context.SalesOrders
                                              .Include(s => s.Customer)
                                              .Include(s => s.Agent)
                             join l in context.SalesOrderLineItems
                                              .Include(s => s.Item) on s.Id equals l.SalesOrderId
                             join u in userManager.Users on s.UserCreated equals u.Id
                             where l.Quantity - l.PackedQuantity > 0
                             select new UnservedItemVM
                             {
                                 Agent = s.Agent.Code,
                                 CreatedBy = u.Nickname,
                                 Customer = s.Customer.Name,
                                 Discount1D = l.Discount1,
                                 Discount2D = l.Discount2,
                                 Id = s.Id,
                                 Item = l.Item.Code,
                                 OrderDateD = s.DateCreated,
                                 PackedQuantity = l.PackedQuantity,
                                 Quantity = l.Quantity,
                                 SalesOrderNumber = s.SalesOrderNumber,
                                 UnitPriceD = l.SellingPrice
                             };
            return await salesOrder.ToListAsync();
        }

        public async Task<ForDataTables<UnservedItemVM>> GetListDataTablesAsync(DataTablesRequestForm form)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetListAsync()).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Agent.ToLower().Contains(form.SearchValue)
                                    || x.CreatedBy.ToLower().Contains(form.SearchValue)
                                    || x.Customer.ToLower().Contains(form.SearchValue)
                                    || x.Discount1D.ToString().Contains(form.SearchValue)
                                    || x.Discount2D.ToString().Contains(form.SearchValue)
                                    || x.Item.ToLower().Contains(form.SearchValue)
                                    || x.OrderDate.ToLower().Contains(form.SearchValue)
                                    || x.PackedQuantity.ToString().Contains(form.SearchValue)
                                    || x.Quantity.ToString().Contains(form.SearchValue)
                                    || x.SalesOrderNumber.ToLower().Contains(form.SearchValue)
                                    || x.UnitPriceD.ToString().Contains(form.SearchValue)
                                    || x.UnservedQuantity.ToString().Contains(form.SearchValue));
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
                        case "Agent": data = data.OrderBy(s => s.Agent); break;
                        case "CreatedBy": data = data.OrderBy(s => s.CreatedBy); break;
                        case "Customer": data = data.OrderBy(s => s.Customer); break;
                        case "Discount1": data = data.OrderBy(s => s.Discount1D); break;
                        case "Discount2": data = data.OrderBy(s => s.Discount2D); break;
                        case "Item": data = data.OrderBy(s => s.Item); break;
                        case "OrderDate": data = data.OrderBy(s => s.OrderDateD); break;
                        case "PackedQuantity": data = data.OrderBy(s => s.PackedQuantity); break;
                        case "Quantity": data = data.OrderBy(s => s.Quantity); break;
                        case "SalesOrderNumber": data = data.OrderBy(s => s.SalesOrderNumber); break;
                        case "UnitPrice": data = data.OrderBy(s => s.UnitPriceD); break;
                        case "UnservedQuantity": data = data.OrderBy(s => s.UnservedQuantity); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Agent": data = data.OrderByDescending(s => s.Agent); break;
                        case "CreatedBy": data = data.OrderByDescending(s => s.CreatedBy); break;
                        case "Customer": data = data.OrderByDescending(s => s.Customer); break;
                        case "Discount1": data = data.OrderByDescending(s => s.Discount1D); break;
                        case "Discount2": data = data.OrderByDescending(s => s.Discount2D); break;
                        case "Item": data = data.OrderByDescending(s => s.Item); break;
                        case "OrderDate": data = data.OrderByDescending(s => s.OrderDateD); break;
                        case "PackedQuantity": data = data.OrderByDescending(s => s.PackedQuantity); break;
                        case "Quantity": data = data.OrderByDescending(s => s.Quantity); break;
                        case "SalesOrderNumber": data = data.OrderByDescending(s => s.SalesOrderNumber); break;
                        case "UnitPrice": data = data.OrderByDescending(s => s.UnitPriceD); break;
                        case "UnservedQuantity": data = data.OrderByDescending(s => s.UnservedQuantity); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<UnservedItemVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList
            };
        }
    }
}