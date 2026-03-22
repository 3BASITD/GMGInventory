using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using GMGInventory.Data.Migrations;
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

        public async Task<IList<UnservedItemVM>> GetListAsync(UnservedItemFilter filter)
        {
            var regionSelected = await context.Regions.Where(s => s.Id == filter.Region).Select(s => s.Name).FirstOrDefaultAsync();
            var salesOrder = from l in context.SalesOrderLineItems
                             join user in userManager.Users on l.SalesOrder.UserCreated equals user.Id
                             let i = (from ii in context.ItemInventories
                                      where ii.ItemId == l.ItemId
                                      && ii.Warehouse.IsMain == true
                                      select (int?)ii.Quantity).Sum() ?? 0
                             let packedQuantity = context.PackLineItems
                                                         .Where(s => s.Pack.SalesOrderId == l.SalesOrderId
                                                                  && s.ItemId == l.ItemId
                                                                  && s.Pack.Status.Code.Trim().ToLower() == "posted")
                                                         .Sum(s => (int?)s.Quantity) ?? 0
                             let returnedQuantity = context.SalesReturnLineItems
                                                           .Where(s => s.InvoiceLineItem != null
                                                                    && s.InvoiceLineItem.Invoice.SalesOrderId == l.SalesOrderId
                                                                    && s.ItemId == l.ItemId)
                                                           .Sum(s => (int?)s.Quantity) ?? 0
                             where l.Quantity - (packedQuantity - returnedQuantity) > 0
                             && (filter.Region == null || l.SalesOrder.Customer.Area.Region.Id == filter.Region
                             || (filter.Region != null && regionSelected != null && regionSelected.ToLower() == "luzon" && l.SalesOrder.Customer.Area.Region.Name.ToLower() == "metro manila"))
                             && filter.StartDate <= l.SalesOrder.DateCreated.Date
                             && l.SalesOrder.DateCreated.Date <= filter.EndDate
                             select new UnservedItemVM
                             {
                                 Agent = l.SalesOrder.Agent.Code,
                                 CreatedBy = user.FirstName + " " + user.LastName,
                                 Customer = l.SalesOrder.Customer.Name,
                                 Discount1D = l.Discount1,
                                 Discount2D = l.Discount2,
                                 Id = l.SalesOrder.Id,
                                 Item = l.Item.Code,
                                 OldSKU = l.Item.OldCode,
                                 ItemName = l.Item.Name,
                                 OrderDateD = l.SalesOrder.DateCreated,
                                 PackedQuantityD = packedQuantity,
                                 QuantityD = l.Quantity,
                                 QuantityOnHandD = i,
                                 SalesOrderNumber = l.SalesOrder.SalesOrderNumber,
                                 UnitPriceD = l.SellingPrice,
                                 UnservedQuantityD = l.Quantity - (packedQuantity - returnedQuantity) > 0
                                                   ? l.Quantity - (packedQuantity - returnedQuantity)
                                                   : 0
                             };
            return await salesOrder.ToListAsync();
        }

        public async Task<ForDataTables<UnservedItemVM>> GetListDataTablesAsync(DataTablesRequestForm form, UnservedItemFilter filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetListAsync(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Agent.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.CreatedBy.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.Customer.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.Discount1D.ToString().Contains(form.SearchValue)
                                    || x.Discount2D.ToString().Contains(form.SearchValue)
                                    || x.Item.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.ItemName.ToLower().Contains(form.SearchValue.ToLower())
                                    || (x.OldSKU + "").ToLower().Contains(form.SearchValue.ToLower())
                                    || x.OrderDate.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.PackedQuantityD.ToString().Contains(form.SearchValue)
                                    || x.QuantityD.ToString().Contains(form.SearchValue)
                                    || x.QuantityOnHandD.ToString().Contains(form.SearchValue)
                                    || x.SalesOrderNumber.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.UnitPriceD.ToString().Contains(form.SearchValue)
                                    || x.UnservedQuantityD.ToString().Contains(form.SearchValue));
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
                        case "ItemName": data = data.OrderBy(s => s.ItemName); break;
                        case "OldSKU": data = data.OrderBy(s => s.OldSKU); break;
                        case "OrderDate": data = data.OrderBy(s => s.OrderDateD); break;
                        case "PackedQuantity": data = data.OrderBy(s => s.PackedQuantityD); break;
                        case "Quantity": data = data.OrderBy(s => s.QuantityD); break;
                        case "QuantityOnHand": data = data.OrderBy(s => s.QuantityOnHandD); break;
                        case "SalesOrderNumber": data = data.OrderBy(s => s.SalesOrderNumber); break;
                        case "UnitPrice": data = data.OrderBy(s => s.UnitPriceD); break;
                        case "UnservedQuantity": data = data.OrderBy(s => s.UnservedQuantityD); break;
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
                        case "ItemName": data = data.OrderByDescending(s => s.ItemName); break;
                        case "OldSKU": data = data.OrderByDescending(s => s.OldSKU); break;
                        case "OrderDate": data = data.OrderByDescending(s => s.OrderDateD); break;
                        case "PackedQuantity": data = data.OrderByDescending(s => s.PackedQuantityD); break;
                        case "Quantity": data = data.OrderByDescending(s => s.QuantityD); break;
                        case "QuantityOnHand": data = data.OrderByDescending(s => s.QuantityOnHandD); break;
                        case "SalesOrderNumber": data = data.OrderByDescending(s => s.SalesOrderNumber); break;
                        case "UnitPrice": data = data.OrderByDescending(s => s.UnitPriceD); break;
                        case "UnservedQuantity": data = data.OrderByDescending(s => s.UnservedQuantityD); break;
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