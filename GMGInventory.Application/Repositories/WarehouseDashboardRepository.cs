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

        public async Task<OrderFulfillmentVM> GetOrderFulfillments(DateTime dt, bool IncludeDone)
        {
            var items = from so in context.SalesOrders
                        join soli in context.SalesOrderLineItems on so.Id equals soli.SalesOrderId
                        join pick in context.Picks on so.Id equals pick.SalesOrderId into gpick
                        from pick in gpick.DefaultIfEmpty()
                        join pack in context.Packs on so.Id equals pack.SalesOrderId into gpack
                        from pack in gpack.DefaultIfEmpty()
                        join pli in context.PackLineItems on pack.Id equals pli.PackId into gpli
                        from pli in gpli.DefaultIfEmpty()
                        where so.SalesOrderDate.Date == dt.Date
                        && so.Status.Code != "DRAFT"
                        && so.Status.Code != "SUBMITTED"
                        && so.Status.Code != "DISAPPROVED"
                        select new
                        {
                            so.Id,
                            so.SalesOrderNumber,
                            so.CustomerId,
                            Customer = so.Customer.Name,
                            soli.ItemId,
                            soQuantity = soli.Quantity,
                            pQuantity = pli == null ? 0 : pli.Quantity,
                            PickStatus = pick == null ? null : pick.Status,
                            so.SalesOrderDate,
                            so.DateCreated,
                            so.DateModified
                        };

            var fulfillments = from item in items
                               group item by new { item.Id, item.SalesOrderNumber, item.CustomerId, item.Customer, item.PickStatus, item.DateCreated, item.DateModified } into g
                               orderby g.Key.DateModified == null ? g.Key.DateCreated : g.Key.DateModified descending
                               select new WarehouseDashboardVM
                               {
                                   SalesOrderId = g.Key.Id,
                                   SalesOrderNumber = g.Key.SalesOrderNumber,
                                   CustomerId = g.Key.CustomerId,
                                   Customer = g.Key.Customer,
                                   PickingStatus = g.Key.PickStatus.ToUpper(),
                                   PackingStatus = g.Sum(s => s.pQuantity) == 0 ? "NOT STARTED"
                                                 : g.Sum(s => s.soQuantity - s.pQuantity <= 0 ? 0 : s.soQuantity - s.pQuantity) > 0 ? "ONGOING"
                                                 : "DONE"
                               };
            return new OrderFulfillmentVM
            {
                Orders = await fulfillments.Where(s => IncludeDone == true
                                                    //|| s.PickingStatus != ""
                                                    || s.PackingStatus != "DONE")
                                           .ToListAsync(),
                PickingNotStartedCount = await fulfillments.CountAsync(s=> s.PickingStatus == "NOT STARTED" || s.PickingStatus == "PENDING"),
                PickingOngoingCount = await fulfillments.CountAsync(s => s.PickingStatus == "ONGOING"),
                PickingDoneCount = await fulfillments.CountAsync(s => s.PickingStatus == "DONE" || s.PickingStatus == "POSTED"),
                PackingNotStartedCount = await fulfillments.CountAsync(s => s.PackingStatus == "NOT STARTED"),
                PackingOngoingCount = await fulfillments.CountAsync(s => s.PackingStatus == "ONGOING"),
                PackingDoneCount = await fulfillments.CountAsync(s => s.PackingStatus == "DONE"),
            };
        }

        public async Task<IncomingOrderVM> GetIncomingOrders()
        {
            DateTime dtNow = DateTime.Today;
            var incoming = from order in context.SalesOrders
                           where (order.Status.Code == "DRAFT"
                           || order.Status.Code == "SUBMITTED")
                           && order.SalesOrderDate.Date == dtNow
                           orderby order.DateModified == null ? order.DateCreated : order.DateModified descending
                           select new WarehouseDashboardVM
                           {
                               SalesOrderId = order.Id,
                               SalesOrderNumber = order.SalesOrderNumber,
                               CustomerId = order.Customer.Id,
                               Customer = order.Customer.Name
                           };
            var backlog = from order in context.SalesOrders
                          where (order.Status.Code == "DRAFT"
                          || order.Status.Code == "SUBMITTED")
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