using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace GMGInventory.Application.Repositories
{
    public class WarehouseDashboardRepository : IWarehouseDashboardRepository
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;

        public WarehouseDashboardRepository(ApplicationDbContext context
                                          , UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<IList<OrderFulfillmentVM>> GetOrderFulfillments(DateTime dt, bool IncludeDone)
        {
            var packed = from pli in context.PackLineItems
                         where !string.IsNullOrWhiteSpace(pli.Pack.SalesOrder.ApprovedBy)
                         && pli.Pack.SalesOrder.SalesOrderDate.Date == dt.Date
                         group pli by new { pli.Pack.SalesOrderId, pli.ItemId } into g
                         select new
                         {
                             g.Key.SalesOrderId,
                             g.Key.ItemId,
                             PackedQuantity = g.Sum(x => x.Quantity)
                         };

            var packedQuantity = from soli in context.SalesOrderLineItems
                                 where !string.IsNullOrWhiteSpace(soli.SalesOrder.ApprovedBy)
                                 && soli.SalesOrder.SalesOrderDate.Date == dt.Date
                                 let pquantity = packed.Where(p => p.SalesOrderId == soli.SalesOrderId
                                                                && p.ItemId == soli.ItemId)
                                                    .Select(p => (decimal?)p.PackedQuantity)
                                                    .FirstOrDefault()
                                 select new
                                 {
                                     soli.SalesOrderId,
                                     Packed = pquantity == null ? 0
                                            : soli.Quantity < pquantity ? soli.Quantity
                                            : pquantity,
                                     Ordered = soli.Quantity
                                 };
            
            var fulfillments = from order in context.SalesOrders
                               join pack in packedQuantity
                                   on order.Id equals pack.SalesOrderId into gpack
                               from pack in gpack.DefaultIfEmpty()
                               where !string.IsNullOrWhiteSpace(order.ApprovedBy)
                                   && order.SalesOrderDate.Date == dt.Date
                               orderby order.DateModified descending
                               let orderQty = order.SalesOrderLineItems.Sum(s => s.Quantity)
                               let packedQty = pack == null ? 0 : pack.Packed
                               select new OrderFulfillmentVM
                               {
                                   SalesOrderId = order.Id,
                                   SalesOrderNumber = order.SalesOrderNumber,
                                   CustomerId = order.Customer.Id,
                                   Customer = order.Customer.Name,
                                   PickingStatus = "",
                                   PackingStatus = packedQty == 0 ? "NOT STARTED"
                                                 : packedQty < orderQty ? "ONGOING"
                                                 : "DONE"
                               };
            return await fulfillments.ToListAsync();
        }

        public async Task<IncomingOrderVM> GetIncomingOrders()
        {
            DateTime dtNow = DateTime.Today;
            var incoming = from order in context.SalesOrders
                           where order.Status.Code == "DRAFT"
                           && order.SalesOrderDate.Date == dtNow
                           orderby order.DateModified descending
                           select new WarehouseDashboardVM
                           {
                               SalesOrderId = order.Id,
                               SalesOrderNumber = order.SalesOrderNumber,
                               CustomerId = order.Customer.Id,
                               Customer = order.Customer.Name
                           };
            var backlog = from order in context.SalesOrders
                          where order.Status.Code == "DRAFT"
                          && order.SalesOrderDate.Date < dtNow
                          select order;
            return new IncomingOrderVM
            {
                Orders = await incoming.ToListAsync(),
                TodayCount = await incoming.CountAsync(),
                BacklogCount = await backlog.CountAsync()
            };
        }
    }
}