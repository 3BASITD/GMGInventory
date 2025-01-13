using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Humanizer;
using Microsoft.AspNetCore.Authentication;
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
                         where order.Status.Code != "Draft"
                         && (string.IsNullOrWhiteSpace(search)
                         || (order.PONumber.ToLower().Trim().Contains(search.ToLower().Trim())))
                         orderby order.PONumber ascending
                         select new JsonPurchaseOrderOptionsModel
                         {
                             Id = order.Id.ToString(),
                             OrderBy = SystemFormat.Name(user.FirstName, user.LastName, user.Nickname),
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

        public async Task<Select2Model<JsonSalesOrderOptionsModel>> GetSalesOrderOptions(string search, int page)
        {
            var orders = from order in context.SalesOrders
                         let quantityOrdered = context.SalesOrderLineItems
                                                      .Where(s => s.SalesOrderId == order.Id)
                                                      .Sum(s => (int?)s.Quantity) ?? 0
                         let quantityPacked = context.PackLineItems
                                                     .Where(s => s.Pack.SalesOrderId == order.Id)
                                                     .Sum(s => (int?)s.Quantity) ?? 0
                         where order.Status.Code.ToLower() != "submitted"
                         && order.Status.Code.ToLower() != "disapproved"
                         && order.Status.Code.ToLower() != "denied"
                         && (string.IsNullOrWhiteSpace(search)
                         || (order.SalesOrderNumber.ToLower().Trim().Contains(search.ToLower().Trim())))
                         orderby order.SalesOrderNumber ascending
                         select new JsonSalesOrderOptionsModel
                         {
                             Id = order.Id.ToString(),
                             Text = order.SalesOrderNumber,
                             CustomerCode = order.Customer.Code,
                             CustomerName = order.Customer.Name,
                             SalesOrderDateD = order.SalesOrderDate,
                             ForwarderName = order.Forwarder.Name
                         };
            var ordersFiltered = await orders.Skip((page - 1) * 50).Take(50).ToListAsync();
            return new Select2Model<JsonSalesOrderOptionsModel>
            {
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
                                .OrderBy(s => s.Text)
                                .ToListAsync();
        }

        public async Task<SalesOrderDetailsVM?> GetDetailsAsync(int? Id)
        {
            var currentUser = await userManager.GetUserAsync(httpContextAccessor?.HttpContext?.User);
            var currentUserId = currentUser.Id;
            //var currentUserRoles = await userManager.GetRolesAsync(currentUser);
            //var isAdminOrManager = currentUserRoles.Contains(Roles.SystemAdmin) || currentUserRoles.Contains(Roles.SalesDirector);

            var orderVM = (from order in context.SalesOrders
                                                .Include(s => s.Agent)
                                                .Include(s => s.Customer).ThenInclude(s => s.CreditTerm)
                                                .Include(s => s.Forwarder)
                                                .Include(s => s.PriceType)
                                                .Include(s => s.PurchaseOrder)
                           join userc in userManager.Users on order.UserCreated equals userc.Id
                           join userm in userManager.Users on order.UserCreated equals userm.Id into um
                           from userm in um.DefaultIfEmpty()
                           from approve in userManager.Users.Where(s => s.Id == order.ApprovedBy).DefaultIfEmpty()
                           from disapprove in userManager.Users.Where(s => s.Id == order.DisapprovedBy).DefaultIfEmpty()
                           where order.Id == Id
                           //&& (currentUserId == order.UserCreated || isAdminOrManager)
                           select new SalesOrderDetailsVM
                           {
                               AgentCode = order.Agent.Code,
                               AgentId = order.Agent.Id,
                               AgentName = order.Agent.FirstName + (order.Agent.MiddleName == null ? "" : " " + order.Agent.MiddleName) + " " + order.Agent.LastName,
                               ApprovedBy = SystemFormat.Name(approve.FirstName, approve.LastName, approve.Nickname),
                               ApprovedDateD = order.ApprovedDate,
                               CreditTerms = order.Customer.CreditTerm.Name,
                               CustomerAddress = order.Customer.Address,
                               CustomerCode = order.Customer.Code,
                               CustomerId = order.Customer.Id,
                               CustomerName = order.Customer.Name,
                               CustomerContactNumber = order.Customer.ContactNumber + "",
                               CustomerContactPerson = order.Customer.ContactPerson + "",
                               DisapprovedBy = SystemFormat.Name(disapprove.FirstName, disapprove.LastName, disapprove.Nickname),
                               DisapprovedDateD = order.DisapprovedDate,
                               DisapprovedReason = order.DisapprovedReason + "",
                               Forwarder = order.Forwarder.Name,
                               ForwarderId = order.Forwarder.Id,
                               GrossNetPrice = order.PriceType.Name,
                               GrossNetPriceId = order.PriceType.Id,
                               Id = order.Id,
                               OrderDateD = order.SalesOrderDate,
                               OutstandingBalanceD = order.Customer.OutstandingBalance,
                               POId = order.PurchaseOrder == null ? null : order.PurchaseOrder.Id,
                               PONumber = order.PurchaseOrder == null ? "-" : order.PurchaseOrder.DocumentNumber,
                               Remarks = order.Remarks + "",
                               ResubmitId = order.ResubmittedSalesOrderId,
                               ShippingAddress = "",
                               SONumber = order.SalesOrderNumber,
                               Status = order.Status.Code,
                               AccountStatus = order.Customer.CustomerAccountStatus.Name,
                               Region = order.Customer.Area.Region.Name,
                               UserCreated = SystemFormat.Name(userc.FirstName, userc.LastName, userc.Nickname),
                               DateCreatedD = order.DateCreated,
                               UserModified = userm == null ? null : SystemFormat.Name(userm.FirstName, userm.LastName, userm.Nickname),
                               DateModifiedD = order.DateModified == null ? null : order.DateModified.Value
                           });

            return await orderVM.FirstOrDefaultAsync();
        }

        public async Task<SalesOrderPrintVM?> PrintDetailsAsync(int? Id)
        {
            var details = await GetDetailsAsync(Id);
            if (details == null)
            {
                return null;
            }
            else
            {
                return new SalesOrderPrintVM
                {
                    Details = details,
                    Items = await GetItemsAsync(Id ?? 0)
                };
            }
        }

        public async Task<IList<SalesOrderVM>> GetListAsync(SalesOrderFilterVM filter)
        {
            var currentUser = await userManager.GetUserAsync(httpContextAccessor?.HttpContext?.User);
            var currentUserId = currentUser.Id;
            //var currentUserRoles = await userManager.GetRolesAsync(currentUser);
            //var isAdminOrManager = currentUserRoles.Contains(Roles.SystemAdmin) || currentUserRoles.Contains(Roles.SalesDirector);

            var users = userManager.Users;

            var orderVM = (from order in context.SalesOrders
                                                .Include(s => s.Customer)
                                                .Include(s => s.Status)
                           join user in users on order.UserCreated equals user.Id
                           join approvedby in users on order.ApprovedBy equals approvedby.Id into ab
                           from approvedby in ab.DefaultIfEmpty()
                           join disapprovedby in users on order.DisapprovedBy equals disapprovedby.Id into db
                           from disapprovedby in db.DefaultIfEmpty()
                           where /*(order.UserCreated == currentUserId || isAdminOrManager)
                           &&*/ (filter.AgentId == null || order.AgentId == filter.AgentId)
                           && (filter.CustomerId == null || order.CustomerId == filter.CustomerId)
                           && (filter.RegionId == null || order.Customer.Area.Region.Id == filter.RegionId)
                           && filter.DateFrom <= order.DateCreated.Date
                           && order.DateCreated.Date <= filter.DateTo
                           && (filter.StatusCode == null || filter.StatusCode.Trim() == ""
                           || (filter.StatusCode.ToLower() == "submitted" && order.UserCreated == currentUserId)
                           || (filter.StatusCode.ToLower() == "approveddisapproved" && (order.Status.Name.ToLower() == "sales order approved" || order.Status.Name.ToLower() == "sales order disapproved"))
                           || (filter.StatusCode.ToLower() == "approval" && order.Status.Name.ToLower() == "sales order submitted")
                           || (filter.StatusCode.ToLower() == "approved" && order.ApprovedBy == currentUserId))
                           select new SalesOrderVM
                           {
                               Customer = order.Customer.Name,
                               Id = order.Id,
                               OrderDateD = order.DateCreated,
                               SONumber = order.SalesOrderNumber,
                               Status = order.Status.Code,
                               TotalAmountD = order.SalesOrderLineItems.Sum(s => s.Quantity * (((100 - s.Discount1 - s.Discount2) * s.SellingPrice) / 100)),
                               ApprovedDisapprovedBy = approvedby != null ? SystemFormat.Name(approvedby.FirstName, approvedby.LastName, approvedby.Nickname) : disapprovedby != null ? SystemFormat.Name(disapprovedby.FirstName, disapprovedby.LastName, disapprovedby.Nickname) : "-",
                               ApprovedDisapprovedDateD = order.ApprovedDate ?? order.DisapprovedDate
                           });
            return await orderVM.ToListAsync();
        }

        public async Task<ForDataTables<SalesOrderVM>> GetListDTAsync(DataTablesRequestForm form, SalesOrderFilterVM filter)
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
                                    || x.OrderDate.ToLower().Contains(form.SearchValue)
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
                        case "OrderDate": data = data.OrderBy(s => s.OrderDateD); break;
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
                        case "OrderDate": data = data.OrderByDescending(s => s.OrderDateD); break;
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

        public async Task<IList<SalesOrderItemVM>> GetItemsAsync(int salesOrderId, bool forPack = false)
        {
            var itemVM = await (from order in context.SalesOrderLineItems
                                                     .Include(s => s.Item)
                                                     .ThenInclude(s => s.Unit)
                                let price = context.ItemPrices
                                                   .Where(s => s.ItemId == order.ItemId
                                                            && s.DateCreated <= order.SalesOrder.ApprovedDate)
                                                   .OrderByDescending(s => s.DateCreated)
                                                   .FirstOrDefault()
                                let inventory = context.ItemInventories
                                                       .Where(s => s.ItemId == order.ItemId && s.Warehouse.Code.ToLower().Contains("main"))
                                                       .Sum(s => (int?)s.Quantity) ?? 0
                                let packedQuantity = context.PackLineItems
                                                            .Where(s => s.Pack.SalesOrderId == order.SalesOrderId
                                                                     && s.ItemId == order.ItemId)
                                                            .Sum(s => (int?)s.Quantity) ?? 0
                                let invoicedQuantity = context.InvoiceLineItems
                                                              .Where(s => s.Invoice.SalesOrderId == order.SalesOrderId
                                                                       && s.ItemId == order.ItemId)
                                                              .Sum(s => (int?)s.Quantity) ?? 0
                                let returnedQuantity = context.SalesReturnLineItems
                                                              .Where(s => s.InvoiceLineItem.Invoice.SalesOrderId == order.SalesOrderId
                                                                       && s.ItemId == order.ItemId)
                                                              .Sum(s => (int?)s.Quantity) ?? 0
                                where order.SalesOrderId == salesOrderId
                                //&& ((order.Quantity != order.PackedQuantity && forPack == true) || forPack == false)
                                && ((order.Quantity != invoicedQuantity && forPack == true) || forPack == false)
                                select new SalesOrderItemVM
                                {
                                    Discount1D = order.Discount1,
                                    Discount2D = order.Discount2,
                                    Id = order.Id,
                                    InventoryCountD = inventory,
                                    InvoicedQuantityD = invoicedQuantity,
                                    ItemCode = order.Item.Code,
                                    ItemId = order.Item.Id,
                                    ItemName = order.Item.Name,
                                    ItemPartNumber = order.Item.PartNumber + "",
                                    ItemSize = order.Item.Size,
                                    LineNumber = order.LineNumber,
                                    NetPriceD = price == null ? 0 : price.NetSellingPrice,
                                    QuantityD = order.Quantity,
                                    PackedQuantityD = packedQuantity,
                                    RemainingQuantityD = (packedQuantity - returnedQuantity) - invoicedQuantity,
                                    RetailPriceD = price == null ? 0 : price.RetailSellingPrice,
                                    SellingPriceD = order.SellingPrice,
                                    ServedQuantityD = packedQuantity - returnedQuantity,
                                    Unit = order.Item.Unit.Code,
                                    UnservedQuantityD = order.Quantity - (packedQuantity - returnedQuantity)
                                }).ToListAsync();

            int newLineNumber = 1;
            foreach (var item in itemVM.OrderBy(s => s.LineNumber))
            {
                item.LineNumber = newLineNumber++;
            }

            return itemVM;
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
                                    || x.GrossPriceD.ToString().Contains(form.SearchValue)
                                    || x.ServedQuantityD.ToString().Contains(form.SearchValue)
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
                        case "ServedQuantity": data = data.OrderBy(s => s.ServedQuantityD); break;
                        case "RetailPrice": data = data.OrderBy(s => s.RetailPriceD); break;
                        case "Unit": data = data.OrderBy(s => s.Unit); break;
                        case "UnservedQuantity": data = data.OrderBy(s => s.UnservedQuantityD); break;
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
                        case "ServedQuantity": data = data.OrderByDescending(s => s.ServedQuantityD); break;
                        case "RetailPrice": data = data.OrderByDescending(s => s.RetailPriceD); break;
                        case "Unit": data = data.OrderByDescending(s => s.Unit); break;
                        case "UnservedQuantity": data = data.OrderByDescending(s => s.UnservedQuantityD); break;
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

            var previous = await context.SalesOrders.Where(s => s.Id == form.ResubmitId).FirstOrDefaultAsync();
            string soNumber = "";
            if (previous == null)
            {
                var latestOrder = context.SalesOrders
                                         .Where(s => s.SalesOrderNumber.StartsWith("SO"))
                                         .OrderByDescending(s => s.SalesOrderNumber)
                                         .Select(s => s.SalesOrderNumber)
                                         .FirstOrDefault();
                var latestOrderNumber = latestOrder == null ? 1 : int.Parse(latestOrder.Replace("SO", ""));
                soNumber = string.Format("{0:SO00000000}", latestOrderNumber + 1);
            }
            else
            {
                soNumber = previous.SalesOrderNumber;
            }

            int statusId = context.Statuses
                                  .Where(s => (form.Status.ToLower() == "submit" && s.Name.ToLower() == "sales order submitted")
                                           || (form.Status.ToLower() == "draft" && s.Name.ToLower() == "sales order draft"))
                                  .Select(s => s.Id)
                                  .First();
            if (orderVM == null)
            {
                orderVM = new SalesOrder
                {
                    AgentId = form.Agent,
                    CustomerId = form.Customer,
                    RequiredDeliveryDate = form.DeliveryDate,
                    ForwarderId = form.Forwarder,
                    PriceTypeId = 1,//form.GrossNet,
                    PurchaseOrderId = form.PurchaseOrder,
                    Remarks = form.Remarks,
                    SalesOrderDate = form.OrderDate,
                    SalesOrderNumber = soNumber,
                    StatusId = statusId,
                    //TotalAmount = form.Items.Sum(s => s.GrossPrice)
                };
                await context.SalesOrders.AddAsync(orderVM);
                await context.SaveChangesAsync(currentUserId);

                if (previous != null)
                {
                    //previous.ResubmittedSalesOrderId = orderVM.Id;
                    //previous.SalesOrderNumber = "*" + previous.SalesOrderNumber;

                    var soLineItems = await context.SalesOrderLineItems.Where(s => s.SalesOrderId == previous.Id).ToListAsync();
                    if (soLineItems != null)
                    {
                        context.SalesOrderLineItems.RemoveRange(soLineItems);
                        await context.SaveChangesAsync(currentUserId);
                    }


                    var soDelivery = await context.SalesOrderDeliveryMonitorings.Where(s => s.SalesOrderId == previous.Id).ToListAsync();
                    if (soDelivery != null)
                    {
                        context.SalesOrderDeliveryMonitorings.RemoveRange(soDelivery);
                        await context.SaveChangesAsync(currentUserId);
                    }

                    context.SalesOrders.Remove(previous);
                    await context.SaveChangesAsync(currentUserId);
                }

                SalesOrderDeliveryMonitoring salesOrderDelivery = new()
                {
                    DateDeliver = null,
                    DatePickup = null,
                    ForwarderId = orderVM.ForwarderId,
                    Remarks = orderVM.Remarks + "",
                    SalesOrderId = orderVM.Id,
                    Waybill = ""
                };

                await context.SalesOrderDeliveryMonitorings.AddAsync(salesOrderDelivery);
                await context.SaveChangesAsync(currentUserId);
            }
            else
            {
                orderVM.StatusId = statusId;
                //orderVM.TotalAmount = form.Items.Sum(s => s.GrossPrice);
                context.SalesOrders.Update(orderVM);
                await context.SaveChangesAsync(currentUserId);
            }

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
                                   IsServed = false,
                                   ItemId = s.Item,
                                   Quantity = s.Quantity,
                                   SalesOrderId = salesOrderId,
                                   SellingPrice = s.UnitPrice
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
                dbitem.SellingPrice = di.UnitPrice;

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
                int statusId = context.Statuses.Where(s => s.Name.ToLower() == "sales order approved").Select(s => s.Id).First();
                orderVM.ApprovedBy = currentUserId;
                orderVM.ApprovedDate = DateTime.Now;
                orderVM.DisapprovedBy = null;
                orderVM.DisapprovedDate = null;
                orderVM.StatusId = statusId;

                context.SalesOrders.Update(orderVM);
                await context.SaveChangesAsync(currentUserId);

                List<SalesOrderLineItem> salesOrderLineItems = context.SalesOrderLineItems.Where(s => s.SalesOrderId == orderVM.Id).ToList();
                foreach (var salesOrderLineItem in salesOrderLineItems)
                {
                    var itemInventory = await context.ItemInventories
                                                     .Where(s => s.ItemId == salesOrderLineItem.ItemId && s.Warehouse.Code.ToLower().Contains("main"))
                                                     .FirstOrDefaultAsync();

                    if (itemInventory != null)
                    {
                        var oldInventoryQuantity = itemInventory.Quantity;
                        if (itemInventory.Quantity >= salesOrderLineItem.Quantity)
                        {
                            itemInventory.Quantity -= salesOrderLineItem.Quantity;
                        }
                        else if (itemInventory.Quantity < 0)
                        {
                            itemInventory.Quantity += salesOrderLineItem.Quantity;
                        }
                        else
                        {
                            itemInventory.Quantity = 0;
                        }
                        context.ItemInventories.Update(itemInventory);
                        await context.SaveChangesAsync(currentUserId);

                        var countHistory = new PhysicalCountHistory
                        {
                            ItemId = salesOrderLineItem.ItemId,
                            QuantityFrom = oldInventoryQuantity,
                            QuantityTo = itemInventory.Quantity,
                            Remarks = "SO Served",
                            WarehouseId = itemInventory.WarehouseId
                        };
                        context.PhysicalCountHistories.Add(countHistory);
                        await context.SaveChangesAsync(currentUserId);
                    }
                    context.SalesOrderLineItems.Update(salesOrderLineItem);
                    await context.SaveChangesAsync(currentUserId);
                }

                return true;
            }
        }

        public async Task<bool> DisapproveAsync(int salesOrderId, string reason)
        {
            var orderVM = context.SalesOrders.Where(s => s.Id == salesOrderId).FirstOrDefault();
            if (orderVM == null)
            {
                return false;
            }
            else
            {
                string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
                int statusId = await context.Statuses.Where(s => s.Name.ToLower() == "sales order disapproved").Select(s => s.Id).FirstAsync();
                orderVM.ApprovedBy = null;
                orderVM.ApprovedDate = null;
                orderVM.DisapprovedBy = currentUserId;
                orderVM.DisapprovedDate = DateTime.Now;
                orderVM.DisapprovedReason = reason;
                orderVM.StatusId = statusId;

                context.SalesOrders.Update(orderVM);
                await context.SaveChangesAsync(currentUserId);

                var delivery = await context.SalesOrderDeliveryMonitorings.Where(s => s.SalesOrderId == salesOrderId).FirstOrDefaultAsync();
                if (delivery != null)
                {
                    context.SalesOrderDeliveryMonitorings.Remove(delivery);
                    await context.SaveChangesAsync(currentUserId);
                }

                return true;
            }
        }

        public async Task<bool> UpdateServedSOItem(int salesOrderLineItemId, int quantity)
        {
            var soItem = await context.SalesOrderLineItems
                                      .Where(s => s.Id == salesOrderLineItemId)
                                      .FirstOrDefaultAsync();
            if (soItem == null)
            {
                return false;
            }
            else
            {
                string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
                var inventory = await context.ItemInventories
                                             .Where(s => s.ItemId == soItem.ItemId && s.Warehouse.Code.ToLower().Contains("main"))
                                             .FirstOrDefaultAsync();
                if (inventory == null)
                {
                    return false;
                }
                else if (inventory.Quantity >= quantity)
                {
                    inventory.Quantity -= quantity;
                }
                else
                {
                    inventory.Quantity = 0;
                }
                context.SalesOrderLineItems.Update(soItem);
                context.ItemInventories.Update(inventory);
                await context.SaveChangesAsync(currentUserId);
                return true;
            }
        }
    }
}