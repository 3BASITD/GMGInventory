using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using GMGInventory.Data.Migrations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

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
            var regions = filter.Region2 == "ML" ? new[] { "MM", "LUZ" }
                        : filter.Region2 == "VM" ? new[] { "VIS", "MIN" }
                        : null;
            var salesOrder = from s in context.SalesOrderLineItems
                             join user in userManager.Users on s.SalesOrder.UserCreated equals user.Id
                             where (s.SalesOrder.Status.Name.Trim().ToLower() == "packing draft"
                                || s.SalesOrder.Status.Name.Trim().ToLower() == "packing submitted"
                                || s.SalesOrder.Status.Name.Trim().ToLower() == "fully packed")
                             && (filter.Brand == null || s.Item.Brand.Id == filter.Brand)
                             && (filter.Region2 == null || regions == null || regions.Contains(s.SalesOrder.Region.Code))
                             && filter.StartDate.Date <= s.SalesOrder.SalesOrderDate.Date
                             && s.SalesOrder.SalesOrderDate.Date <= filter.EndDate.Date
                             let packed = context.PackLineItems
                                                 .Where(p => p.Pack.SalesOrderId == s.SalesOrderId
                                                          && s.ItemId == p.ItemId)
                                                 .Sum(p => (int?)p.Quantity) ?? 0
                             let returned = context.SalesReturnLineItems
                                                   .Where(r => r.SalesReturn.SalesOrderId == s.SalesOrderId
                                                            && s.ItemId == r.ItemId)
                                                   .Sum(r => (int?)r.Quantity) ?? 0
                             let inventory = context.ItemInventories
                                                    .Where(i => i.ItemId == s.ItemId
                                                             && i.Warehouse.IsMain == true)
                                                    .Sum(i => (int?)i.Quantity) ?? 0
                             where s.Quantity - (packed - returned) > 0
                             select new UnservedItemVM
                             {
                                 Agent = s.SalesOrder.Agent.Code,
                                 AgentId = s.SalesOrder.Agent.Id,
                                 CreatedBy = user.FirstName + " " + user.LastName,
                                 Customer = s.SalesOrder.Customer.Name,
                                 CustomerId = s.SalesOrder.Customer.Id,
                                 Discount1D = s.Discount1,
                                 Discount2D = s.Discount2,
                                 Id = s.SalesOrder.Id,
                                 Item = s.Item.Code,
                                 ItemId = s.Item.Id,
                                 OldSKU = s.Item.OldCode,
                                 ItemName = s.Item.Name,
                                 OrderDateD = s.SalesOrder.SalesOrderDate,
                                 PackedQuantityD = packed,
                                 QuantityD = s.Quantity,
                                 QuantityOnHandD = inventory,
                                 SalesOrderNumber = s.SalesOrder.SalesOrderNumber,
                                 SalesOrderId = s.SalesOrder.Id,
                                 UnitPriceD = s.SellingPrice,
                                 UnservedQuantityD = s.Quantity - (packed - returned)
                             };
            return await salesOrder.ToListAsync();
        }
        //public async Task<IList<UnservedItemVM>> GetListAsync(UnservedItemFilter filter)
        //{
        //    var regions = filter.Region2 == "ML" ? new[] { "MM", "LUZ" }
        //                : filter.Region2 == "VM" ? new[] { "VIS", "MIN" }
        //                : null;
        //    var regionSelected = await context.Regions.Where(s => s.Id == filter.Region).Select(s => s.Name).FirstOrDefaultAsync();
        //    var salesOrder = from l in context.SalesOrderLineItems
        //                     join packed in context.PackLineItems on new { l.SalesOrderId, l.ItemId } equals new { packed.Pack.SalesOrderId, packed.ItemId }
        //                     join returned in context.SalesReturnLineItems on new { SalesOrderId = (int?)l.SalesOrderId, l.ItemId }
        //                                                               equals new { returned.SalesReturn.SalesOrderId, returned.ItemId } into greturned
        //                     from returned in greturned.DefaultIfEmpty()
        //                     join user in userManager.Users on l.SalesOrder.UserCreated equals user.Id
        //                     let inventory = context.ItemInventories
        //                                            .Where(s => s.ItemId == l.ItemId
        //                                                     && s.Warehouse.IsMain == true)
        //                                            .Sum(s => (int?)s.Quantity) ?? 0
        //                     //let packedQuantity = context.PackLineItems
        //                     //                            .Where(s => s.Pack.SalesOrderId == l.SalesOrderId
        //                     //                                     && s.ItemId == l.ItemId
        //                     //                                     && s.Pack.Status.Code.Trim().ToLower() == "posted")
        //                     //                            .Sum(s => (int?)s.Quantity) ?? 0
        //                     //let returnedQuantity = context.SalesReturnLineItems
        //                     //                              .Where(s => s.InvoiceLineItem != null
        //                     //                                       && s.InvoiceLineItem.Invoice.SalesOrderId == l.SalesOrderId
        //                     //                                       && s.ItemId == l.ItemId)
        //                     //                              .Sum(s => (int?)s.Quantity) ?? 0
        //                     where (l.SalesOrder.Status.Name == "PACKING DRAFT" || l.SalesOrder.Status.Name == "PACKING SUBMITTED" || l.SalesOrder.Status.Name == "FULLY PACKED")
        //                     //&& l.Quantity - (packed.Quantity - returnedQuantity) > 0
        //                     && (filter.Region2 == null || regions == null || regions.Contains(l.SalesOrder.Customer.Area.Region.Code))
        //                     && filter.StartDate <= l.SalesOrder.DateCreated.Date
        //                     && l.SalesOrder.DateCreated.Date <= filter.EndDate
        //                     select new UnservedItemVM
        //                     {
        //                         Agent = l.SalesOrder.Agent.Code,
        //                         AgentId = l.SalesOrder.Agent.Id,
        //                         CreatedBy = user.FirstName + " " + user.LastName,
        //                         Customer = l.SalesOrder.Customer.Name,
        //                         CustomerId = l.SalesOrder.Customer.Id,
        //                         Discount1D = l.Discount1,
        //                         Discount2D = l.Discount2,
        //                         Id = l.SalesOrder.Id,
        //                         Item = l.Item.Code,
        //                         ItemId = l.Item.Id,
        //                         OldSKU = l.Item.OldCode,
        //                         ItemName = l.Item.Name,
        //                         OrderDateD = l.SalesOrder.DateCreated,
        //                         PackedQuantityD = packed.Quantity,
        //                         QuantityD = l.Quantity,
        //                         QuantityOnHandD = inventory,
        //                         SalesOrderNumber = l.SalesOrder.SalesOrderNumber,
        //                         SalesOrderId = l.SalesOrder.Id,
        //                         UnitPriceD = l.SellingPrice,
        //                         UnservedQuantityD = l.Quantity - (packed.Quantity - (returned == null ? 0 : returned.Quantity))
        //                     };
        //    return await salesOrder.ToListAsync();
        //}


        public async Task<ForDataTables<UnservedItemVM>> GetListDataTablesAsync(DataTablesRequestForm form, UnservedItemFilter filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            int totalQuantity1 = 0, totalQuantity2 = 0, totalQuantity3 = 0;
            decimal totalAmount = 0;

            var data = (await GetListAsync(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(s => new[]
                {
                    s.Agent,
                    s.CreatedBy,
                    s.Customer,
                    s.Discount1D.ToString(),
                    s.Discount2D.ToString(),
                    s.Item,
                    s.ItemName,
                    s.OldSKU,
                    s.OrderDate,
                    s.PackedQuantityD.ToString(),
                    s.QuantityD.ToString(),
                    s.QuantityOnHandD.ToString(),
                    s.SalesOrderNumber,
                    s.TotalAmountD.ToString(),
                    s.UnitPriceD.ToString(),
                    s.UnservedQuantityD.ToString()
                }.Any(a => a != null && a.Trim().ToLower().Contains((form.SearchValue + "").Trim().ToLower())));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                bool isAsc = form.SortColumnDirection == "asc";
                data = form.SortColumn switch
                {
                    "Discount1" => isAsc ? data.OrderBy(s => s.Discount1D) : data.OrderByDescending(s => s.Discount1D),
                    "Discount2" => isAsc ? data.OrderBy(s => s.Discount2D) : data.OrderByDescending(s => s.Discount2D),
                    "OrderDate" => isAsc ? data.OrderBy(s => s.OrderDateD) : data.OrderByDescending(s => s.OrderDateD),
                    "PackedQuantity" => isAsc ? data.OrderBy(s => s.PackedQuantityD) : data.OrderByDescending(s => s.PackedQuantityD),
                    "TotalAmount" => isAsc ? data.OrderBy(s => s.TotalAmountD) : data.OrderByDescending(s => s.TotalAmountD),
                    "Quantity" => isAsc ? data.OrderBy(s => s.QuantityD) : data.OrderByDescending(s => s.QuantityD),
                    "QuantityOnHand" => isAsc ? data.OrderBy(s => s.QuantityOnHandD) : data.OrderByDescending(s => s.QuantityOnHandD),
                    "UnitPrice" => isAsc ? data.OrderBy(s => s.UnitPriceD) : data.OrderByDescending(s => s.UnitPriceD),
                    "UnservedQuantity" => isAsc ? data.OrderBy(s => s.UnservedQuantityD) : data.OrderByDescending(s => s.UnservedQuantityD),
                    _ => data.OrderBy(form.SortColumn + " " + form.SortColumnDirection)
                };
            }
            //get total quantity of data in table
            totalQuantity1 = data.Sum(s => s.QuantityD);
            totalQuantity2 = data.Sum(s => s.PackedQuantityD);
            totalQuantity3 = data.Sum(s => s.UnservedQuantityD);
            totalAmount = data.Sum(s => s.TotalAmountD);
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<UnservedItemVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList,
                TotalQuantity = totalQuantity1,
                TotalQuantity2 = totalQuantity2,
                TotalQuantity3 = totalQuantity3,
                TotalAmount = totalAmount
            };
        }
    }
}