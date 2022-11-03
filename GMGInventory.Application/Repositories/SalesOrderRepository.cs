using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Application.Repositories
{
    public class SalesOrderRepository : ISalesOrderRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;

        public SalesOrderRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
            , UserManager<User> userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        public async Task<Select2Model<JsonPurchaseOrderOptionsModel>> GetOptions(string search, int page)
        {
            var orders = from order in context.PurchaseOrders
                                              .Include(s => s.Supplier)
                                              .Include(s => s.Status)
                         join user in userManager.Users on order.UserCreated equals user.Id
                         where string.IsNullOrWhiteSpace(search)
                         && order.Status.Code != "Draft"
                            || (order.PONumber.ToLower().Contains(search.ToLower()))
                         select new JsonPurchaseOrderOptionsModel
                         {
                             Id = order.Id.ToString(),
                             OrderBy = string.IsNullOrWhiteSpace(user.FirstName) || string.IsNullOrWhiteSpace(user.FirstName) ? user.Nickname : (user.FirstName + " " + user.LastName),
                             OrderDateD = order.ReplenishDate,
                             Supplier = order.Supplier.Name,
                             Text = order.PONumber
                         };
            var ordersFiltered = await orders.Skip((page - 1) * 50).Take(50).ToListAsync();
            return new Select2Model<JsonPurchaseOrderOptionsModel>
            {
                //incomplete_results = false,
                items = ordersFiltered,
                total_count = orders.Count()
            };
        }

        public async Task<IList<JsonOptionsModel>> GetPriceTypeOptions(int? IncludeId)
        {
            return await context.PriceTypes
                                .Select(s => new JsonOptionsModel
                                {
                                    Id = s.Id.ToString(),
                                    Text = s.Name,
                                    Selected = IncludeId == s.Id
                                })
                                .ToListAsync();
        }

        public async Task<IList<JsonOptionsModel>> GetDeliveryTypeOptions(int? IncludeId)
        {
            return await context.DeliveryTypes
                                .Select(s => new JsonOptionsModel
                                {
                                    Id = s.Id.ToString(),
                                    Text = s.Name,
                                    Selected = IncludeId == s.Id
                                })
                                .ToListAsync();
        }

        public async Task<SalesOrderDetailsVM?> GetDetailsAsync(int? Id)
        {
            var orderVM = (from order in context.SalesOrders
                                                .Include(s => s.Agent)
                                                .Include(s => s.Customer).ThenInclude(s => s.CreditTerm)
                                                .Include(s => s.DeliveryType)
                                                .Include(s => s.PriceType)
                                                .Include(s => s.PurchaseOrder)
                           join user in userManager.Users on order.UserCreated equals user.Id
                           from approve in userManager.Users.Where(s => s.Id == order.ApprovedBy).DefaultIfEmpty()
                           from disapprove in userManager.Users.Where(s => s.Id == order.DisapprovedBy).DefaultIfEmpty()
                           where order.Id == Id
                           select new SalesOrderDetailsVM
                           {
                               AgentCode = order.Agent.Code,
                               AgentName = order.Agent.FirstName + (order.Agent.MiddleName == null ? "" : " " + order.Agent.MiddleName) + " " + order.Agent.LastName,
                               ApprovedBy = approve.Nickname,
                               ApprovedDateD = order.ApprovedDate,
                               CreditTerms = order.Customer.CreditTerm.Name,
                               CustomerAddress = order.Customer.Address,
                               CustomerCode = order.Customer.Code,
                               CustomerName = order.Customer.Name,
                               DeclaredValueD = order.DeclaredValue,
                               DeliveryDateD = order.DeliveryDate,
                               DeliveryType = order.DeliveryType.Name,
                               DisapprovedBy = disapprove.Nickname,
                               DisapprovedDateD = order.DisapprovedDate,
                               GrossNetPrice = order.PriceType.Name,
                               Id = order.Id,
                               OrderDateD = order.DateCreated,
                               PONumber = order.PurchaseOrder == null ? "-" : order.PurchaseOrder.PONumber,
                               RequiredDateD = order.RequiredDate,
                               SalesOrderBy = user.Nickname,
                               ShippingAddress = "",
                               SONumber = order.SalesOrderNumber,
                               Status = order.Status.Code
                           });
            return await orderVM.FirstOrDefaultAsync();
        }

        private async Task<IList<SalesOrderVM>> GetListAsync()
        {
            var orderVM = (from order in context.SalesOrders
                                                .Include(s => s.Customer)
                                                .Include(s => s.Status)
                           join user in userManager.Users on order.UserCreated equals user.Id
                           select new SalesOrderVM
                           {
                               Customer = order.Customer.Name,
                               DeliveryDateD = order.DeliveryDate,
                               Id = order.Id,
                               OrderDateD = order.DateCreated,
                               RequiredDateD = order.RequiredDate,
                               SONumber = order.SalesOrderNumber,
                               Status = order.Status.Code,
                               TotalAmountD = order.TotalAmount
                           });
            return await orderVM.ToListAsync();
        }

        public async Task<ForDataTables<SalesOrderVM>> GetListDTAsync(DataTablesRequestForm form)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetListAsync()).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Customer.ToLower().Contains(form.SearchValue)
                                    || x.DeliveryDate.ToLower().Contains(form.SearchValue)
                                    || x.OrderDate.ToLower().Contains(form.SearchValue)
                                    || x.RequiredDate.ToLower().Contains(form.SearchValue)
                                    || x.SONumber.ToLower().Contains(form.SearchValue)
                                    || x.Status.ToLower().Contains(form.SearchValue)
                                    || x.TotalAmount.ToLower().Contains(form.SearchValue));
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
                        case "DeliveryDate": data = data.OrderBy(s => s.DeliveryDateD); break;
                        case "OrderDate": data = data.OrderBy(s => s.OrderDateD); break;
                        case "RequiredDate": data = data.OrderBy(s => s.RequiredDateD); break;
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
                        case "DeliveryDate": data = data.OrderByDescending(s => s.DeliveryDateD); break;
                        case "OrderDate": data = data.OrderByDescending(s => s.OrderDateD); break;
                        case "RequiredDate": data = data.OrderByDescending(s => s.RequiredDateD); break;
                        case "SONumber": data = data.OrderByDescending(s => s.SONumber); break;
                        case "Status": data = data.OrderByDescending(s => s.Status); break;
                        case "TotalAmount": data = data.OrderByDescending(s => s.TotalAmountD); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<SalesOrderVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public async Task<IList<SalesOrderItemVM>> GetItemsAsync(int salesOrderId)
        {
            var itemVM = from order in context.SalesOrderLineItems
                                              .Include(s => s.Item)
                                                .ThenInclude(s => s.Unit)
                         from price in context.ItemPrices
                                              .Where(s => s.ItemId == order.ItemId
                                                       && s.DateCreated <= DateTime.Now)
                                              .OrderByDescending(s => s.DateCreated)
                                              .Take(1)
                                              .DefaultIfEmpty()
                         let inventory = context.ItemInventories.Where(s => s.ItemId == order.ItemId).Sum(s => s.Quantity)
                         where order.SalesOrderId == salesOrderId
                         select new SalesOrderItemVM
                         {
                             Discount1D = order.Discount1,
                             Discount2D = order.Discount2,
                             Id = order.ItemId,
                             InventoryCount = inventory,
                             InvoicedQuantity = order.InvoicedQuantity,
                             ItemCode = order.Item.Code,
                             ItemName = order.Item.Name,
                             NetPriceD = price.NetSellingPrice,
                             Quantity = order.Quantity,
                             PackedQuantity = order.PackedQuantity,
                             RetailPriceD = price.RetailSellingPrice,
                             SellingPriceD = order.SellingPrice,
                             Unit = order.Item.Unit.Code,
                         };
            return await itemVM.ToListAsync();
        }

        public async Task<ForDataTables<SalesOrderItemVM>> GetItemsDTAsync(DataTablesRequestForm form, int salesOrderId)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetItemsAsync(salesOrderId)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Discount1.ToLower().Contains(form.SearchValue)
                                    || x.Discount2.ToLower().Contains(form.SearchValue)
                                    || x.InventoryCount.ToString().Contains(form.SearchValue)
                                    || x.InvoicedQuantity.ToString().Contains(form.SearchValue)
                                    || x.ItemCode.ToLower().Contains(form.SearchValue)
                                    || x.ItemName.ToLower().Contains(form.SearchValue)
                                    || x.NetPriceD.ToString().Contains(form.SearchValue)
                                    || x.PackedQuantity.ToString().Contains(form.SearchValue)
                                    || x.Quantity.ToString().Contains(form.SearchValue)
                                    || x.RetailPriceD.ToString().Contains(form.SearchValue)
                                    || x.SellingPriceD.ToString().Contains(form.SearchValue)
                                    || x.Unit.ToLower().Contains(form.SearchValue)
                                    || x.GrossPriceD.ToString().Contains(form.SearchValue));
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
                        case "Discount1": data = data.OrderBy(s => s.Discount1D); break;
                        case "Discount2": data = data.OrderBy(s => s.Discount2D); break;
                        case "InventoryCount": data = data.OrderBy(s => s.InventoryCount); break;
                        case "InvoicedQuantity": data = data.OrderBy(s => s.InvoicedQuantity); break;
                        case "ItemCode": data = data.OrderBy(s => s.ItemCode); break;
                        case "ItemName": data = data.OrderBy(s => s.ItemName); break;
                        case "Quantity": data = data.OrderBy(s => s.Quantity); break;
                        case "NetPrice": data = data.OrderBy(s => s.NetPriceD); break;
                        case "PackedQuantity": data = data.OrderBy(s => s.PackedQuantity); break;
                        case "SellingPrice": data = data.OrderBy(s => s.SellingPriceD); break;
                        case "RetailPrice": data = data.OrderBy(s => s.RetailPriceD); break;
                        case "Unit": data = data.OrderBy(s => s.Unit); break;
                        case "GrossPrice": data = data.OrderBy(s => s.GrossPriceD); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Discount1": data = data.OrderByDescending(s => s.Discount1D); break;
                        case "Discount2": data = data.OrderByDescending(s => s.Discount2D); break;
                        case "InventoryCount": data = data.OrderByDescending(s => s.InventoryCount); break;
                        case "InvoicedQuantity": data = data.OrderByDescending(s => s.InvoicedQuantity); break;
                        case "ItemCode": data = data.OrderByDescending(s => s.ItemCode); break;
                        case "ItemName": data = data.OrderByDescending(s => s.ItemName); break;
                        case "Quantity": data = data.OrderByDescending(s => s.Quantity); break;
                        case "NetPrice": data = data.OrderByDescending(s => s.NetPriceD); break;
                        case "PackedQuantity": data = data.OrderByDescending(s => s.PackedQuantity); break;
                        case "SellingPrice": data = data.OrderByDescending(s => s.SellingPriceD); break;
                        case "RetailPrice": data = data.OrderByDescending(s => s.RetailPriceD); break;
                        case "Unit": data = data.OrderByDescending(s => s.Unit); break;
                        case "GrossPrice": data = data.OrderByDescending(s => s.GrossPriceD); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<SalesOrderItemVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public async Task<int> SaveAsync(SalesOrderFormVM form)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var orderVM = await context.SalesOrders.Where(s => s.Id == form.Id).FirstOrDefaultAsync();
            //var previousOrders = context.SalesOrders.Count();
            //string soNumber = string.Format("{0:SO00000000}", previousOrders);
            //while (context.SalesOrders.Any(s => s.SalesOrderNumber == soNumber))
            //{
            //    previousOrders++;
            //    soNumber = string.Format("{0:SO00000000}", previousOrders);
            //}

            var latestOrder = context.SalesOrders.OrderByDescending(s => s.SalesOrderNumber).Select(s => s.SalesOrderNumber).FirstOrDefault();
            var latestOrderNumber = latestOrder == null ? 1 : int.Parse(latestOrder.Replace("SO", ""));
            string soNumber = string.Format("{0:SO00000000}", latestOrderNumber + 1);

            int statusId = context.Statuses.Where(s => s.Name == "Sales Order Submitted").Select(s => s.Id).First();
            if (orderVM == null)
            {
                orderVM = new SalesOrder
                {
                    AgentId = form.Agent,
                    CustomerId = form.Customer,
                    DeclaredValue = form.DeclaredValue,
                    DeliveryDate = form.DeliveryDate,
                    DeliveryTypeId = form.DeliveryType,
                    PriceTypeId = form.GrossNet,
                    PurchaseOrderId = form.PurchaseOrder,
                    QuantityInvoiced = 0,
                    QuantityOrdered = form.Items.Sum(s => s.Quantity),
                    QuantityPacked = 0,
                    RequiredDate = form.RequiredDate,
                    SalesOrderNumber = soNumber,
                    StatusId = statusId,
                    TotalAmount = form.Items.Sum(s => s.GrossPrice)
                };
                await context.SalesOrders.AddAsync(orderVM);
            }
            else
            {
                orderVM.StatusId = statusId;
                orderVM.TotalAmount = form.Items.Sum(s => s.GrossPrice);
                context.SalesOrders.Update(orderVM);
            }
            await context.SaveChangesAsync(currentUserId);

            await SaveItemsAsync(form.Items, orderVM.Id);

            return orderVM.Id;
        }

        public async Task SaveItemsAsync(IList<SalesOrderFormItemVM> Items, int salesOrderId)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var citemsVM = context.SalesOrderLineItems.Where(s => s.SalesOrderId == salesOrderId);

            // add item
            // check items not in db
            var items = citemsVM.Select(s => s.ItemId);
            // add excluding items already available
            var itemsVM = Items.Where(s => items.Contains(s.Item) == false)
                               .Select(s => new SalesOrderLineItem
                               {
                                   Discount1 = s.Discount1,
                                   Discount2 = s.Discount2,
                                   InvoicedQuantity = 0,
                                   IsServed = false,
                                   ItemId = s.Item,
                                   PackedQuantity = 0,
                                   Quantity = s.Quantity,
                                   SalesOrderId = salesOrderId,
                                   SellingPrice = s.SellingPrice
                               });
            context.SalesOrderLineItems.AddRange(itemsVM);

            // update item
            // get items available in db
            var ditems = Items.Where(s => items.Contains(s.Item));
            foreach (var di in ditems)
            {
                // update all available in db
                var dbitem = await citemsVM.Where(s => s.ItemId == di.Item).FirstAsync();
                dbitem.Discount1 = di.Discount1;
                dbitem.Discount2 = di.Discount2;
                dbitem.Quantity = di.Quantity;
                dbitem.SellingPrice = di.SellingPrice;

                context.SalesOrderLineItems.Update(dbitem);
            }

            // delete item
            // get items not in form
            var fitems = Items.Select(s => s.Item);
            var ritems = citemsVM.Where(s => fitems.Contains(s.ItemId) == false);
            // remove unavailable items
            context.SalesOrderLineItems.RemoveRange(ritems);

            await context.SaveChangesAsync(currentUserId);
        }

        public async Task<bool> ApproveAsync(int salesOrderId)
        {
            var orderVM = context.SalesOrders.Where(s => s.Id == salesOrderId).FirstOrDefault();
            if (orderVM == null)
            {
                return false;
            }
            else
            {
                string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
                int statusId = context.Statuses.Where(s => s.Name == "Sales Order Approved").Select(s => s.Id).First();
                orderVM.ApprovedBy = currentUserId;
                orderVM.ApprovedDate = DateTime.Now;
                orderVM.DisapprovedBy = null;
                orderVM.DisapprovedDate = null;
                orderVM.StatusId = statusId;

                context.SalesOrders.Update(orderVM);
                await context.SaveChangesAsync(currentUserId);

                return true;
            }
        }

        public async Task<bool> DisapproveAsync(int salesOrderId)
        {
            var orderVM = context.SalesOrders.Where(s => s.Id == salesOrderId).FirstOrDefault();
            if (orderVM == null)
            {
                return false;
            }
            else
            {
                string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
                int statusId = context.Statuses.Where(s => s.Name == "Sales Order Disapproved").Select(s => s.Id).First();
                orderVM.ApprovedBy = null;
                orderVM.ApprovedDate = null;
                orderVM.DisapprovedBy = currentUserId;
                orderVM.DisapprovedDate = DateTime.Now;
                orderVM.StatusId = statusId;

                context.SalesOrders.Update(orderVM);
                await context.SaveChangesAsync(currentUserId);

                return true;
            }
        }
    }
}