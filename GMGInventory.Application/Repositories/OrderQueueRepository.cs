using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Spreadsheet;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using GMGInventory.Data.Migrations;
using GMGInventory.Data.OrderAppDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Security.Cryptography;

namespace GMGInventory.Application.Repositories
{
    public class OrderQueueRepository : IOrderQueueRepository
    {
        private readonly ApplicationDbContext context;
        private readonly AppOrderDbContext app_context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly string currentUser;

        public OrderQueueRepository(ApplicationDbContext context
                                  , AppOrderDbContext app_context
                                  , IHttpContextAccessor httpContextAccessor
                                  , UserManager<User> userManager)
        {
            this.context = context;
            this.app_context = app_context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.currentUser = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
        }

        private async Task<IList<OrderQueueVM>> GetOrders(string? Status, DateTime? DateFrom, DateTime? DateTo)
        {
            if (DateFrom == null) DateFrom = DateTime.Today.AddMonths(-1);
            if (DateTo == null) DateTo = DateTime.Today;
            if (DateFrom > DateTo) (DateFrom, DateTo) = (DateTo, DateFrom);

            var orders = from order in app_context.Orders
                         where !EF.Functions.Like(order.OrderNo, "draft-%")
                         && DateFrom <= order.CreatedDate
                         && order.CreatedDate <= DateTo
                         group order by order.OrderNo into gorder
                         select new OrderQueueVM
                         {
                             OrderNumber = gorder.Key,
                             AgentId = gorder.Select(s => s.AgentId).FirstOrDefault(),
                             Agent = gorder.Select(s => s.Agent).FirstOrDefault(),
                             CustomerId = gorder.Select(s => s.CustomerId).FirstOrDefault(),
                             Customer = gorder.Select(s => s.Customer).FirstOrDefault(),
                             ExtendedPriceD = gorder.Sum(s => s.ExtendedPrice) ?? 0,
                             DateCreatedD = gorder.Select(s => s.CreatedDate).FirstOrDefault(),
                             ItemCount = gorder.Count()
                         };

            var ordersList = await orders.ToListAsync();

            var salesSources = await (
                from source in context.SalesOrderItemSources
                group source by source.SourceOrderNumber into gsource
                select new
                {
                    SourceOrderNumber = gsource.Key,
                    CountAdded = gsource.Where(s => s.Status.ToLower() == "added").Count(),
                    CountIgnored = gsource.Where(s => s.Status.ToLower() == "ignored").Count()
                }
            ).ToListAsync();

            var sourceLookup = salesSources.GroupBy(x => x.SourceOrderNumber)
                                           .ToDictionary(g => g.Key, g => g.First());

            for (int i = ordersList.Count - 1; i >= 0; i--)
            {
                var order = ordersList[i];
                if (sourceLookup.TryGetValue(order.OrderNumber, out var ord))
                {
                    order.AddedCount = Math.Min(ord.CountAdded, order.ItemCount);
                    order.IgnoredCount = Math.Min(ord.CountIgnored, order.ItemCount);
                    order.Status = order.ItemCount == ord.CountIgnored ? "Fully Ignored" :
                                   order.ItemCount == ord.CountAdded ? "Fully Added" :
                                   (ord.CountAdded > 0 && ord.CountIgnored > 0) ? "Partial Added & Partial Ignored" :
                                   ord.CountAdded > 0 ? "Partial Added" :
                                   ord.CountIgnored > 0 ? "Partial Ignored" :
                                   "Pending";
                }
                else
                {
                    order.AddedCount = 0;
                    order.IgnoredCount = 0;
                    order.Status = "Pending";
                }
            }

            return ordersList.Where(s => Status == null
                                      || (Status.ToLower().Contains("pending") && s.Status.ToLower().Contains("pending"))
                                      || (Status.ToLower().Contains("partialadded") && s.Status.ToLower().Contains("partial added"))
                                      || (Status.ToLower().Contains("fulladded") && s.Status.ToLower().Contains("fully added"))
                                      || (Status.ToLower().Contains("partialignored") && s.Status.ToLower().Contains("partial ignored"))
                                      || (Status.ToLower().Contains("fullignored") && s.Status.ToLower().Contains("fully ignored")))
                             .ToList();
        }

        public async Task<ForDataTables<OrderQueueVM>> GetOrdersDT(DataTablesRequestForm form, string? Status, DateTime? DateFrom, DateTime? DateTo)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetOrders(Status, DateFrom, DateTo)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(s => new[]
                {
                    s.OrderNumber,
                    s.Agent,
                    s.Customer,
                    s.ExtendedPrice,
                    s.Status,
                    s.DateCreated
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
                    "Status" => isAsc ? data.OrderBy(s => (decimal)s.AddedCount / (decimal)s.ItemCount).ThenBy(s => s.ItemCount) :
                                data.OrderByDescending(s => (decimal)s.AddedCount / (decimal)s.ItemCount).ThenByDescending(s => s.ItemCount),
                    "ExtendedPrice" => isAsc ? data.OrderBy(s => s.ExtendedPriceD) : data.OrderByDescending(s => s.ExtendedPriceD),
                    "DateCreated" => isAsc ? data.OrderBy(s => s.DateCreatedD) : data.OrderByDescending(s => s.DateCreatedD),
                    _ => data.OrderBy(form.SortColumn + " " + form.SortColumnDirection)
                };
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();

            foreach (var list in pageList)
            {
                list.IsValidAgent = list.AgentId > 0 && context.Agents.Any(s => s.Id == list.AgentId);
                list.IsValidCustomer = list.CustomerId > 0 && context.Customers.Any(s => s.Id == list.CustomerId);
                var soConverts = context.SalesOrderItemSources
                                        .Where(s => s.SourceOrderNumber == list.OrderNumber
                                                 && (s.Status == null || s.Status.ToLower() == "added")
                                                 && s.SalesOrder != null)
                                        .Select(s => s.SalesOrder.SalesOrderNumber)
                                        .ToList();
                list.ConvertTimes = soConverts.Count;
            }

            return new ForDataTables<OrderQueueVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public async Task<OrderQueueDetailsVM?> GetDetails(string OrderNumber)
        {
            var orders = await app_context.Orders
                                          .Where(s => s.OrderNo != null && s.OrderNo.ToLower() == OrderNumber.ToLower())
                                          .Select(s => new OrderQueueDetailsVM
                                          {
                                              OrderNumber = s.OrderNo,
                                              AgentId = s.AgentId,
                                              Agent = s.Agent,
                                              CustomerId = s.CustomerId,
                                              Customer = s.Customer,
                                              Instruction = s.Instruction
                                          })
                                          .ToListAsync();

            if (orders.Count() == 0) return null;

            var order = orders.First();

            var sources = await (from source in context.SalesOrderItemSources
                                 where source.SourceOrderNumber == order.OrderNumber
                                 select source).ToListAsync();

            var countAdded = sources.Where(s => s.Status.Trim().ToLower() == "added").Count();
            var countIgnored = sources.Where(s => s.Status.Trim().ToLower() == "ignored").Count();

            order.Status = orders.Count() == countIgnored ? "Fully Ignored" :
                           orders.Count() == countAdded ? "Fully Added" :
                           (countAdded > 0 && countIgnored > 0) ? "Partial Added & Partial Ignored" :
                           countAdded > 0 ? "Partial Added" :
                           countIgnored > 0 ? "Partial Ignored" :
                           "Pending";

            order.IsValidAgent = order.AgentId > 0 && context.Agents.Any(s => s.Id == order.AgentId);
            order.IsValidCustomer = order.CustomerId > 0 && context.Customers.Any(s => s.Id == order.CustomerId);
            var soConverts = context.SalesOrderItemSources
                                    .Where(s => s.SourceOrderNumber == order.OrderNumber)
                                    .OrderBy(s => s.DateCreated)
                                    .Select(s => s.SalesOrder.SalesOrderNumber)
                                    .ToList();
            order.ConvertTimes = soConverts.Count;

            order.Items = app_context.Orders
                                     .Where(s => s.OrderNo!.ToLower() == OrderNumber.ToLower())
                                     .Select(s => new OrderQueueItemVM
                                     {
                                         Id = s.Id,
                                         ItemId = s.ItemId,
                                         Brand = s.Brand,
                                         Model = s.Model,
                                         OldCode = s.OldCode,
                                         Code = s.Code,
                                         ItemName = s.ItemName,
                                         Size = s.Size,
                                         PartNumber = s.PartNumber,
                                         Unit = s.Unit,
                                         UnitPriceD = s.UnitPrice,
                                         QuantityD = s.Quantity,
                                         Discount1D = s.Discount1,
                                         Discount2D = s.Discount2,
                                         Discount3D = s.Discount3,
                                         Discount4D = s.Discount4,
                                         Discount5D = s.Discount5,
                                         ExtendedPriceD = s.ExtendedPrice
                                     })
                                     .ToList();

            foreach (var i in order.Items)
            {
                int.TryParse(i.ItemId, out int itemId);
                var itm = context.Items
                                 .Include(s => s.ItemClass)
                                 .Where(s => itemId > 0
                                          && s.Id == itemId)
                                 .FirstOrDefault();

                i.IsValidItem = itm != null;
                i.ItemClass = itm == null ? "Invalid Item" :
                              itm.ItemClass != null ? itm.ItemClass.Name :
                              null;

                if (itm != null)
                {
                    var dest = await (from destination in context.SalesOrderItemSources
                                      where destination.SourceOrderNumber == OrderNumber
                                      && destination.ItemId == itm.Id
                                      select new
                                      {
                                          destination.Status,
                                          destination.SalesOrder
                                      }).FirstOrDefaultAsync();

                    i.SalesOrderNumber = dest == null ? null :
                                         dest.Status.ToLower() == "added" ? dest.SalesOrder.SalesOrderNumber :
                                         dest.Status.ToLower() == "ignored" ? "Ignored" :
                                         null;
                    i.SalesOrderId = dest == null ? null :
                                     dest.Status.ToLower() == "added" ? dest.SalesOrder.Id :
                                     null;
                }
            }

            return order;
        }

        public async Task<ResponseModel> AddOrder(string OrderNumber, List<int> ItemIds)
        {
            //try
            //{
                var invalids = new List<string>();
                var orders = app_context.Orders
                                        .Where(s => ItemIds != null
                                                 && ItemIds.Contains(s.Id)
                                                 && s.OrderNo != null
                                                 && s.OrderNo.ToLower() == OrderNumber.ToLower())
                                        .OrderBy(s => s.Id)
                                        .ToList();

                if (orders.Count == 0)
                    return new ResponseModel
                    {
                        Status = "Error",
                        Message = "Invalid Order Number. Record cannot be found"
                    };

                var agent_first = orders.First().AgentId;
                var customer_first = orders.First().CustomerId;

                var agent = context.Agents
                                   .Where(s => agent_first > 0
                                            && s.Id == agent_first)
                                   .FirstOrDefault();

                var customer = context.Customers
                                      .Where(s => customer_first > 0
                                               && s.Id == customer_first)
                                      .FirstOrDefault();

                var temp = new List<string>();

                if (agent == null) temp.Add($"<span class='text-muted'>Invalid Agent:</span> Unknown PK_{agent_first}");
                if (customer == null) temp.Add($"<span class='text-muted'>Invalid Customer:</span> Unknown PK_{customer_first}");

                if (temp.Count > 0)
                {
                    temp = [.. temp.Distinct().OrderBy(s => s)];
                    return new ResponseModel
                    {
                        Status = "Error",
                        Message = "Cannot proceed with the following problems: <ul><li>" + string.Join("</li><li>", temp) + "</li>"
                    };
                }
                else
                {
                    var latestOrder = context.SalesOrders
                                         .Where(s => s.SalesOrderNumber.StartsWith("SO")
                                                  && !s.SalesOrderNumber.Contains("-D"))
                                         .OrderByDescending(s => s.SalesOrderNumber)
                                         .Select(s => s.SalesOrderNumber)
                                         .FirstOrDefault();

                    if (latestOrder != null && latestOrder.Contains('.'))
                    {
                        var splitLatestOrder = latestOrder.Split(".");
                        latestOrder = splitLatestOrder[0];
                    }

                    if (latestOrder != null && latestOrder.Contains('-'))
                    {
                        var splitLatestOrder = latestOrder.Split("-");
                        latestOrder = splitLatestOrder[0];
                    }

                    var latestOrderNumber = latestOrder == null ? 0 : int.Parse(latestOrder.Replace("SO", ""));
                    var soNumber = string.Format("{0:SO00000000}", latestOrderNumber + 1);

                    int statusId = context.Statuses
                                          .Where(s => s.Name.ToLower() == "sales order draft")
                                          .Select(s => s.Id)
                                          .First();

                    var itemClasses = await (from item in context.Items
                                             select new
                                             {
                                                 item.ItemClass,
                                                 ItemId = item.Id
                                             }).ToListAsync();

                           var itemsToAdd = (from item in itemClasses
                                             join order in orders on item.ItemId equals int.Parse(order.ItemId!)
                                            group item by item.ItemClass into gitem
                                            orderby gitem.Key == null ? "0" : (1 + "_" + gitem.Key!.Name)
                                             select new
                                            {
                                                ItemClass = gitem.Key,
                                                ItemIds = gitem.Select(s => s.ItemId)
                                            }).ToList();

                    var soNumberAdded = new List<string>();

                    foreach (var itemToAdd in itemsToAdd)
                    {
                        var soNumberForAdd = soNumber + (itemsToAdd.Count() == 1 ? "" : string.Format("-{0:00}", itemsToAdd.IndexOf(itemToAdd) + 1));
                        var salesOrder = new SalesOrder
                        {
                            AgentId = agent == null ? 0 : agent.Id,
                            CustomerId = customer == null ? 0 : customer.Id,
                            RequiredDeliveryDate = DateTime.Today,
                            SalesOrderDate = DateTime.Today,
                            SalesOrderNumber = soNumberForAdd,
                            StatusId = statusId,
                            OverallDiscount = 0,
                            WithLP = false,
                            WithREYCONonDefective = false,
                            SpecialInstruction = orders.First().Instruction
                        };
                        soNumberAdded.Add(soNumberForAdd);

                        await context.SalesOrders.AddAsync(salesOrder);
                        await context.SaveChangesAsync(currentUser);

                        var lineNumber = 1;

                        foreach (var itemId in itemToAdd.ItemIds)
                        {
                            var itm = orders.Where(s => s.ItemId == itemId.ToString()).First();

                            var soitem = new SalesOrderLineItem
                            {
                                SalesOrderId = salesOrder.Id,
                                LineNumber = lineNumber,
                                ItemId = itemId,
                                Quantity = itm.Quantity == null ? 0 : itm.Quantity.Value,
                                SellingPrice = itm.UnitPrice == null ? 0 : itm.UnitPrice.Value,
                                Discount1 = itm.Discount1 == null ? 0 : itm.Discount1.Value,
                                Discount2 = itm.Discount2 == null ? 0 : itm.Discount2.Value,
                                Discount3 = itm.Discount3 == null ? 0 : itm.Discount3.Value,
                                Discount4 = itm.Discount4 == null ? 0 : itm.Discount4.Value,
                                Discount5 = itm.Discount5 == null ? 0 : itm.Discount5.Value
                            };
                            await context.SalesOrderLineItems.AddAsync(soitem);

                            var source = new SalesOrderItemSource
                            {
                                SourceOrderNumber = OrderNumber,
                                SalesOrderId = salesOrder.Id,
                                ItemId = itemId,
                                Status = "Added"
                            };
                            await context.SalesOrderItemSources.AddAsync(source);

                            await context.SaveChangesAsync(currentUser);

                            lineNumber++;
                        }
                    }
                    return new ResponseModel
                    {
                        Status = "Success",
                        Message = $"Successfully added to sales order with SONumber: {string.Join(",", soNumberAdded)}"
                    };
                }
            //}
            //catch (Exception ex)
            //{
            //    return new ResponseModel { Status = "Error", Message = SystemFormat.GetInnermostExceptionMessage(ex) };
            //}
        }

        public async Task<ResponseModel> IgnoreOrder(string OrderNumber, List<int> ItemIds)
        {
            var invalids = new List<string>();
            var orders = app_context.Orders
                                    .Where(s => ItemIds != null
                                             && ItemIds.Contains(s.Id)
                                             && s.OrderNo != null
                                             && s.OrderNo.ToLower() == OrderNumber.ToLower())
                                    .OrderBy(s => s.Id)
                                    .ToList();

            if (orders.Count == 0)
                return new ResponseModel
                {
                    Status = "Error",
                    Message = "Invalid Order Number. Record cannot be found"
                };


            var temp = new List<string>();
            var source = new List<SalesOrderItemSource>();
            foreach (var order in orders)
            {
                var isInvalid = false;

                int.TryParse(order.ItemId, out int itemId);
                var itm = context.Items
                                 .Where(s => itemId > 0
                                          && s.Id == itemId)
                                 .FirstOrDefault();
                if (itm == null)
                {
                    temp.Add($"<span class='text-muted'>Invalid Item:</span> Unknown PK_{order.ItemId}");
                    isInvalid = true;
                }

                if (isInvalid == false)
                {
                    source.Add(new SalesOrderItemSource
                    {
                        SourceOrderNumber = OrderNumber,
                        SalesOrderId = null,
                        ItemId = itm.Id,
                        Status = "Ignored"
                    });
                }

            }

            if (temp.Count > 0)
            {
                temp = [.. temp.Distinct().OrderBy(s => s)];
                return new ResponseModel
                {
                    Status = "Error",
                    Message = "Cannot proceed with the following problems: <ul><li>" + string.Join("</li><li>", temp) + "</li>"
                };
            }
            else
            {
                await context.SalesOrderItemSources.AddRangeAsync(source);
                await context.SaveChangesAsync(currentUser);

                return new ResponseModel
                {
                    Status = "Success",
                    Message = $"Successfully ignored selected items"
                };
            }
        }

        public async Task<ResponseModel> MarkIgnoreOrder(string OrderNumber)
        {
            var source = new SalesOrderItemSource
            {
                SourceOrderNumber = OrderNumber,
                SalesOrderId = null,
                Status = "Ignored"
            };
            await context.SalesOrderItemSources.AddAsync(source);
            await context.SaveChangesAsync(currentUser);

            return new ResponseModel
            {
                Status = "Success",
                Message = $"Successfully marked ignore Order: {OrderNumber}"
            };
        }
    }
}