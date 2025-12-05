using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using GMGInventory.Data.Migrations;
using GMGInventory.Data.OrderAppDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

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
                         where order.OrderNo!.ToLower().StartsWith("draft-") == false
                         && DateFrom <= order.CreatedDate
                         && order.CreatedDate <= DateTo
                         group order by order.OrderNo into gorder
                         select new OrderQueueVM
                         {
                             OrderNumber = gorder.Key,
                             Agent = gorder.Select(s => s.Agent).FirstOrDefault(),
                             Customer = gorder.Select(s => s.Customer).FirstOrDefault(),
                             ExtendedPriceD = gorder.Sum(s => s.ExtendedPrice) ?? 0,
                             DateCreatedD = gorder.Select(s => s.CreatedDate).FirstOrDefault(),
                             ItemCount = gorder.Count()
                         };

            var ordersList = await orders.ToListAsync();

            for (int i = ordersList.Count - 1; i >= 0; i--)
            {
                var order = ordersList[i];

                var status = from source in context.SalesOrderSources
                             join userc in userManager.Users on source.UserCreated equals userc.Id
                             where source.SourceOrderNumber == order.OrderNumber
                             select new
                             {
                                 source.Status,
                                 ItemCount = source.SalesOrderLineItems.Count()
                             };

                order.AddedCount = status.Any() ? status.First().ItemCount : 0;

                order.Status = status.Any() == false
                             ? "Pending"
                             : (status.First().Status == null || status.First().Status.ToLower().Contains("added"))
                             ? (order.ItemCount == order.AddedCount ? "Fully Added" : "Partial Added")
                             : status.First().Status;
            }

            return ordersList.Where(s => Status == null
                                      || (Status.ToLower().Contains("pending") && s.Status.ToLower().Contains("pending"))
                                      || (Status.ToLower().Contains("partial") && s.Status.ToLower().Contains("partial"))
                                      || (Status.ToLower().Contains("full") && s.Status.ToLower().Contains("full"))
                                      || (Status.ToLower().Contains("ignore") && s.Status.ToLower().Contains("ignore")))
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
                }.Any(a => a != null && a.ToLower().Contains(form.SearchValue.ToLower())));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                if (form.SortColumn.ToLower().Contains("date") || form.SortColumn.ToLower() == "ExtendedPrice")
                {
                    if (form.SortColumnDirection == "asc")
                    {
                        switch (form.SortColumn)
                        {
                            case "ExtendedPrice": data = data.OrderBy(s => s.ExtendedPriceD); break;
                            case "DateCreated": data = data.OrderBy(s => s.DateCreatedD); break;
                            default: break;
                        }
                    }
                    else if (form.SortColumnDirection == "desc")
                    {
                        switch (form.SortColumn)
                        {
                            case "ExtendedPrice": data = data.OrderByDescending(s => s.ExtendedPriceD); break;
                            case "DateCreated": data = data.OrderByDescending(s => s.DateCreatedD); break;
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

            foreach (var list in pageList)
            {
                list.IsValidAgent = list.Agent != null && context.Agents.Any(s => (s.FirstName + " " + s.LastName).ToLower() == list.Agent.ToLower());
                list.IsValidCustomer = list.Customer != null && context.Customers.Any(s => s.Name.ToLower() == list.Customer.ToLower());
                var soConverts = context.SalesOrderSources
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
            var order = app_context.Orders
                                   .Where(s => s.OrderNo != null && s.OrderNo.ToLower() == OrderNumber.ToLower())
                                   .Select(s => new OrderQueueDetailsVM
                                   {
                                       OrderNumber = s.OrderNo,
                                       Agent = s.Agent,
                                       Customer = s.Customer,
                                       Instruction = s.Instruction
                                   })
                                   .FirstOrDefault();

            if (order == null) return null;

            var status = from source in context.SalesOrderSources
                         join userc in userManager.Users on source.UserCreated equals userc.Id
                         where source.SourceOrderNumber == order.OrderNumber
                         orderby source.DateCreated ascending
                         select new
                         {
                             SONumber = source.SalesOrder == null ? null : source.SalesOrder.SalesOrderNumber,
                             source.Status,
                             CreatedBy = userc.FirstName + " " + userc.LastName,
                             DateCreated = source.DateCreated.ToString("MMMM d, yyyy hh:mm:ss tt")
                         };

            order.Status = status.Any()
                         ? string.Join("<br />", status.Select(s => (s.Status == null ? "Added" : s.Status)
                                                                  + (s.SONumber == null ? "" : " " + s.SONumber)
                                                                  + "<span class='fw-normal text-muted'>"
                                                                  + " by "
                                                                  + s.CreatedBy
                                                                  + " on "
                                                                  + s.DateCreated
                                                                  + "</span>"))
                         : "Pending";

            order.IsValidAgent = order.Agent != null && context.Agents.Any(s => (s.FirstName + " " + s.LastName).ToLower() == order.Agent.ToLower());
            order.IsValidCustomer = order.Customer != null && context.Customers.Any(s => s.Name.ToLower() == order.Customer.ToLower());
            var soConverts = context.SalesOrderSources
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
                var itm = context.Items.Where(s => i.Code != null && s.Code.ToLower() == i.Code.ToLower()).FirstOrDefault();
                if (itm != null)
                {
                    var dest = (from destination in context.SalesOrderSources
                                join destinationorder in context.SalesOrders on destination.SalesOrderId equals destinationorder.Id
                                join destinationitem in context.SalesOrderLineItems on destinationorder.Id equals destinationitem.SalesOrderId
                                where destination.SourceOrderNumber == OrderNumber
                                && destinationitem.ItemId == itm.Id
                                select destinationorder).FirstOrDefault();

                    i.SalesOrderNumber = dest == null ? null : dest.SalesOrderNumber;
                    i.SalesOrderId = dest == null ? null : dest.Id;

                    var brnd = context.Brands
                                      .Where(s => s.Name == i.Brand
                                               || (s.Name != null && i.Brand != null && s.Name.ToLower() == i.Brand.ToLower()))
                                      .FirstOrDefault();
                    i.IsValidBrand = brnd != null && itm.BrandId == brnd.Id;

                    i.IsValidModel = itm.ModelNumber == i.Model
                                  || (itm.ModelNumber != null && i.Model != null && itm.ModelNumber.ToLower() == i.Model.ToLower());
                    i.IsValidOldCode = itm.OldCode == i.OldCode
                                    || (itm.OldCode != null && i.OldCode != null && itm.OldCode.ToLower() == i.OldCode.ToLower());
                    i.IsValidItemName = itm.Name == i.ItemName
                                     || (i.ItemName != null && itm.Name.ToLower() == i.ItemName.ToLower());
                    i.IsValidSize = itm.Size == i.Size
                                 || (i.Size != null && itm.Size.ToLower() == i.Size.ToLower());
                    i.IsValidPartNumber = itm.PartNumber == i.PartNumber
                                       || (itm.PartNumber != null && i.PartNumber != null && itm.PartNumber.ToLower() == i.PartNumber.ToLower());
                    var unt = context.Units
                                     .Where(s => s.Name == i.Unit
                                              || (s.Name != null && i.Unit != null && s.Name.ToLower() == i.Unit.ToLower()))
                                     .FirstOrDefault();
                    i.IsValidUnit = unt != null && itm.UnitId == unt.Id;
                }
            }

            return order;
        }

        public async Task<ResponseModel> AddOrder(string OrderNumber, List<int> ItemIds)
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

            var agent_first = orders.First().Agent;
            var customer_first = orders.First().Customer;

            var agent = context.Agents
                               .Where(s => agent_first != null
                                        && (s.FirstName + " " + s.LastName).ToLower() == agent_first.ToLower())
                               .FirstOrDefault();

            var customer = context.Customers
                                  .Where(s => customer_first != null
                                           && s.Name.ToLower() == customer_first.ToLower())
                                  .FirstOrDefault();

            var temp = new List<string>();

            if (agent == null) temp.Add($"<span class='text-muted'>Invalid Agent:</span> {agent_first}. ");
            if (customer == null) temp.Add($"<span class='text-muted'>Invalid Customer:</span> {customer_first}. ");

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

            var latestOrderNumber = latestOrder == null ? 0 : int.Parse(latestOrder.Replace("SO", ""));
            var soNumber = string.Format("{0:SO00000000}", latestOrderNumber + 1);

            int statusId = context.Statuses
                                  .Where(s => s.Name.ToLower() == "sales order draft")
                                  .Select(s => s.Id)
                                  .First();

            var salesOrder = new SalesOrder
            {
                AgentId = agent == null ? 0 : agent.Id,
                CustomerId = customer == null ? 0 : customer.Id,
                RequiredDeliveryDate = DateTime.Today,
                SalesOrderDate = DateTime.Today,
                SalesOrderNumber = soNumber,
                StatusId = statusId,
                OverallDiscount = 0,
                WithLP = false,
                WithREYCONonDefective = false,
                SpecialInstruction = orders.First().Instruction
            };

            var salesOrderItems = new List<SalesOrderLineItem>();
            var lineNumber = 1;

            foreach (var order in orders)
            {
                var isInvalid = false;
                var itm = context.Items
                                 .Where(s => order.Code != null && s.Code.ToLower() == order.Code.ToLower())
                                 .FirstOrDefault();
                if (itm == null)
                {
                    temp.Add($"<span class='text-muted'>Invalid Item Code:</span> {order.Code}. ");
                    isInvalid = true;
                }

                var brnd = context.Brands
                                  .Where(s => s.Name == order.Brand
                                           || (s.Name != null && order.Brand != null && s.Name.ToLower() == order.Brand.ToLower()))
                                  .FirstOrDefault();
                if (brnd == null || itm == null || itm.BrandId != brnd.Id)
                {
                    temp.Add($"<span class='text-muted'>Invalid Item Brand:</span> {order.Brand}. ");
                    isInvalid = true;
                }

                if (itm!.ModelNumber != order.Model
                && (itm.ModelNumber == null || order.Model == null || itm.ModelNumber.ToLower() != order.Model.ToLower()))
                {
                    temp.Add($"<span class='text-muted'>Invalid Item Model Number:</span> {order.Model}. ");
                    isInvalid = true;
                }

                if (itm!.OldCode != order.OldCode
                && (itm.OldCode == null || order.OldCode == null || itm.OldCode.ToLower() != order.OldCode.ToLower()))
                {
                    temp.Add($"<span class='text-muted'>Invalid Item Old Code:</span> {order.OldCode}. ");
                    isInvalid = true;
                }

                if (itm!.Name != order.ItemName
                && (itm.Name == null || order.ItemName == null || itm.Name.ToLower() != order.ItemName.ToLower()))
                {
                    temp.Add($"<span class='text-muted'>Invalid Item Name:</span> {order.ItemName}. ");
                    isInvalid = true;
                }

                if (itm!.Size != order.Size
                && (itm.Size == null || order.Size == null || itm.Size.ToLower() != order.Size.ToLower()))
                {
                    temp.Add($"<span class='text-muted'>Invalid Item Size:</span> {order.Size}. ");
                    isInvalid = true;
                }

                if (itm!.PartNumber != order.PartNumber
                && (itm.PartNumber == null || order.PartNumber == null || itm.PartNumber.ToLower() != order.PartNumber.ToLower()))
                {
                    temp.Add($"<span class='text-muted'>Invalid Item Part Number:</span> {order.PartNumber}. ");
                    isInvalid = true;
                }

                var unt = context.Units
                                 .Where(s => s.Name == order.Unit
                                          || (s.Name != null && order.Unit != null && s.Name.ToLower() == order.Unit.ToLower()))
                                 .FirstOrDefault();
                if (unt == null || itm == null || itm != null && itm.UnitId != unt.Id)
                {
                    temp.Add($"<span class='text-muted'>Invalid Item Unit:</span> {order.Unit}. ");
                    isInvalid = true;
                }

                if (order.Quantity == null || order.Quantity <= 0)
                {
                    temp.Add($"<span class='text-muted'>Invalid Quantity:</span> {order.Quantity}. ");
                    isInvalid = true;
                }

                if (isInvalid == false)
                {
                    salesOrderItems.Add(new SalesOrderLineItem
                    {
                        LineNumber = lineNumber,
                        ItemId = itm.Id,
                        Quantity = order.Quantity == null ? 0 : order.Quantity.Value,
                        SellingPrice = order.UnitPrice == null ? 0 : order.UnitPrice.Value,
                        Discount1 = order.Discount1 == null ? 0 : order.Discount1.Value,
                        Discount2 = order.Discount2 == null ? 0 : order.Discount2.Value,
                        Discount3 = order.Discount3 == null ? 0 : order.Discount3.Value,
                        Discount4 = order.Discount4 == null ? 0 : order.Discount4.Value,
                        Discount5 = order.Discount5 == null ? 0 : order.Discount5.Value
                    });
                    lineNumber++;
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
                await context.SalesOrders.AddAsync(salesOrder);
                await context.SaveChangesAsync(currentUser);

                var source = new SalesOrderSourced
                {
                    SourceOrderNumber = OrderNumber,
                    SalesOrderId = salesOrder.Id,
                    Status = "Added"
                };
                await context.SalesOrderSources.AddAsync(source);
                await context.SaveChangesAsync(currentUser);

                foreach (var salesOrderItem in salesOrderItems)
                {
                    salesOrderItem.SalesOrderId = salesOrder.Id;
                    salesOrderItem.SalesOrderSourceId = source.Id;
                }

                await context.SalesOrderLineItems.AddRangeAsync(salesOrderItems);
                await context.SaveChangesAsync(currentUser);

                return new ResponseModel
                {
                    Status = "Success",
                    Message = $"Successfully added to sales order with SONumber: {salesOrder.SalesOrderNumber}"
                };
            }
        }

        public async Task<ResponseModel> MarkIgnoreOrder(string OrderNumber)
        {
            var source = new SalesOrderSourced
            {
                SourceOrderNumber = OrderNumber,
                SalesOrderId = null,
                Status = "Ignored"
            };
            await context.SalesOrderSources.AddAsync(source);
            await context.SaveChangesAsync(currentUser);

            return new ResponseModel
            {
                Status = "Success",
                Message = $"Successfully marked ignore Order: {OrderNumber}"
            };
        }
    }
}