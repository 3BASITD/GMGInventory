using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Application.Repositories
{
    public class PurchaseOrderReporitory : IPurchaseOrderRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;

        public PurchaseOrderReporitory(ApplicationDbContext context
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
                         && order.Status.Code.ToLower() != "draft"
                            || (order.PONumber.ToLower().Contains(search.ToLower()))
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

        public async Task<IList<JsonCurrencyOptionModel>> GetCurrencyOptions(int? IncludeId)
        {
            return await context.Currencies
                                .Select(s => new JsonCurrencyOptionModel
                                {
                                    Id = s.Id.ToString(),
                                    Text = (s.Code == s.Name ? "" : "(" + s.Code + ") ") + s.Name,
                                    Selected = IncludeId != null && IncludeId == s.Id,
                                    Rate = s.Rate
                                })
                                .ToListAsync();
        }

        public async Task<PurchaseOrderDetailsVM?> GetDetailsAsync(int? Id)
        {
            var orderVM = (from order in context.PurchaseOrders
                                                .Include(s => s.Currency)
                                                .Include(s => s.Destination)
                                                .Include(s => s.Status)
                                                .Include(s => s.Supplier)
                           join user in userManager.Users on order.UserCreated equals user.Id
                           where order.Id == Id
                           select new PurchaseOrderDetailsVM
                           {
                               Address1 = order.Supplier.AddressLine1,
                               Address2 = order.Supplier.AddressLine2,
                               Currency = order.Currency.Code,
                               CurrencyId = order.CurrencyId,
                               CurrencyRateD = order.CurrencyRate,
                               DateCreatedD = order.DateCreated,
                               DateModifiedD = order.DateModified,
                               Destination = order.Destination.Name,
                               Id = order.Id,
                               OrderBy = SystemFormat.Name(user.FirstName, user.LastName, user.Nickname),
                               OrderDateD = order.ReplenishDate,
                               PONumber = order.PONumber,
                               ReferenceNumber = order.ReferenceNumber + "",
                               Status = order.Status.Code,
                               Supplier = order.Supplier.Name,
                               TotalAmountD = order.TotalPurchaseAmount == null ? 0 : order.TotalPurchaseAmount.Value
                           });
            return await orderVM.FirstOrDefaultAsync();
        }

        private async Task<IList<PurchaseOrderVM>> GetListAsync()
        {
            var orderVM = (from order in context.PurchaseOrders
                                                .Include(s => s.Destination)
                                                .Include(s => s.Status)
                                                .Include(s => s.Supplier)
                           join user in userManager.Users on order.UserCreated equals user.Id
                           select new PurchaseOrderVM
                           {
                               Destination = order.Destination.Code,
                               Id = order.Id,
                               OrderBy = SystemFormat.Name(user.FirstName, user.LastName, user.Nickname),
                               OrderDateD = order.ReplenishDate,
                               PONumber = order.PONumber,
                               Status = order.Status.Code,
                               Supplier = order.Supplier.Code,
                               ReferenceNumber = order.ReferenceNumber + "",
                               TotalAmountD = order.TotalPurchaseAmount == null ? 0 : order.TotalPurchaseAmount.Value
                           });
            return await orderVM.ToListAsync();
        }

        public async Task<ForDataTables<PurchaseOrderVM>> GetListDTAsync(DataTablesRequestForm form)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetListAsync()).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Destination.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.OrderBy.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.OrderDate.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.PONumber.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.ReferenceNumber.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.Status.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.Supplier.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.TotalAmount.ToLower().Contains(form.SearchValue.ToLower()));
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
                        case "Destination": data = data.OrderBy(s => s.Destination); break;
                        case "OrderBy": data = data.OrderBy(s => s.OrderBy); break;
                        case "OrderDate": data = data.OrderBy(s => s.OrderDateD); break;
                        case "PONumber": data = data.OrderBy(s => s.PONumber); break;
                        case "ReferenceNumber": data = data.OrderBy(s => s.ReferenceNumber); break;
                        case "Status": data = data.OrderBy(s => s.Status); break;
                        case "Supplier": data = data.OrderBy(s => s.Supplier); break;
                        case "TotalAmount": data = data.OrderBy(s => s.TotalAmountD); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Destination": data = data.OrderByDescending(s => s.Destination); break;
                        case "OrderBy": data = data.OrderByDescending(s => s.OrderBy); break;
                        case "OrderDate": data = data.OrderByDescending(s => s.OrderDateD); break;
                        case "PONumber": data = data.OrderByDescending(s => s.PONumber); break;
                        case "ReferenceNumber": data = data.OrderByDescending(s => s.ReferenceNumber); break;
                        case "Status": data = data.OrderByDescending(s => s.Status); break;
                        case "Supplier": data = data.OrderByDescending(s => s.Supplier); break;
                        case "TotalAmount": data = data.OrderByDescending(s => s.TotalAmountD); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<PurchaseOrderVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public async Task<IList<PurchaseOrderItemVM>> GetItemsAsync(int purchaseOrderId)
        {
            var itemVM = from order in context.PurchaseOrderLineItems
                                              .Include(s => s.Item)
                                                .ThenInclude(s => s.Unit)
                             //join rturn in context.PurchaseReturnLineItems on order.Id equals rturn.PurchaseOrderLineItemId into ritem
                         where order.PurchaseOrderId == purchaseOrderId
                         select new PurchaseOrderItemVM
                         {
                             Discount1D = order.Discount1,
                             Discount2D = order.Discount2,
                             Discount3D = order.Discount3,
                             Id = order.ItemId,
                             ItemCode = order.Item.Code,
                             ItemName = order.Item.Name,
                             Quantity = order.Quantity - (context.PurchaseReturnLineItems.Where(s => s.PurchaseOrderLineItemId == order.Id).Sum(s => (int?)s.Quantity) ?? 0),
                             Unit = order.Item.Unit.Code,
                             UnitPriceD = order.UnitPrice
                         };
            return await itemVM.ToListAsync();
        }

        public async Task<ForDataTables<PurchaseOrderItemVM>> GetItemsDTAsync(DataTablesRequestForm form, int purchaseOrderId)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetItemsAsync(purchaseOrderId)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Discount1.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.Discount2.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.Discount3.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.ItemCode.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.ItemName.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.Quantity.ToString().Contains(form.SearchValue)
                                    || x.Unit.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.UnitPrice.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.NetPrice.ToLower().Contains(form.SearchValue.ToLower()));
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
                        case "Discount3": data = data.OrderBy(s => s.Discount3D); break;
                        case "ItemCode": data = data.OrderBy(s => s.ItemCode); break;
                        case "ItemName": data = data.OrderBy(s => s.ItemName); break;
                        case "Quantity": data = data.OrderBy(s => s.Quantity); break;
                        case "Unit": data = data.OrderBy(s => s.Unit); break;
                        case "UnitPrice": data = data.OrderBy(s => s.UnitPriceD); break;
                        case "NetPrice": data = data.OrderBy(s => s.NetPriceD); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Discount1": data = data.OrderByDescending(s => s.Discount1D); break;
                        case "Discount2": data = data.OrderByDescending(s => s.Discount2D); break;
                        case "Discount3": data = data.OrderByDescending(s => s.Discount3D); break;
                        case "ItemCode": data = data.OrderByDescending(s => s.ItemCode); break;
                        case "ItemName": data = data.OrderByDescending(s => s.ItemName); break;
                        case "Quantity": data = data.OrderByDescending(s => s.Quantity); break;
                        case "Unit": data = data.OrderByDescending(s => s.Unit); break;
                        case "UnitPrice": data = data.OrderByDescending(s => s.UnitPriceD); break;
                        case "NetPrice": data = data.OrderByDescending(s => s.NetPriceD); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<PurchaseOrderItemVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        private decimal ComputeDiscount(decimal Amount, decimal Discount)
        {
            return (Amount * (100 - Discount)) / 100m;
        }

        private async Task<int> SaveOrderAsync(PurchaseOrderFormVM form, int statusId)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var orderVM = await context.PurchaseOrders.Where(s => s.Id == form.Id).FirstOrDefaultAsync();
            var previousOrders = context.PurchaseOrders.Count();
            string poNumber = string.Format("{0:PO00000000}", previousOrders);
            while (context.PurchaseOrders.Any(s => s.PONumber == poNumber))
            {
                previousOrders++;
                poNumber = string.Format("{0:PO00000000}", previousOrders);
            }

            decimal totalPurchaseAmount = form.Items.Sum(s =>
            {
                decimal discountedPrice = s.UnitPrice
                    * (100 - s.Discount1) / 100m
                    * (100 - s.Discount2) / 100m
                    * (100 - s.Discount3) / 100m;

                return s.Quantity * discountedPrice;
            });

            if (orderVM == null)
            {
                orderVM = new PurchaseOrder
                {
                    CurrencyId = form.Currency,
                    CurrencyRate = form.CurrencyRate,
                    DestinationId = form.Destination,
                    PONumber = poNumber,
                    ReferenceNumber = form.ReferenceNumber,
                    ReplenishDate = form.ReplenishDate,
                    StatusId = statusId,
                    SupplierId = form.Supplier,
                    TotalPurchaseAmount = totalPurchaseAmount
                };
                await context.PurchaseOrders.AddAsync(orderVM);
            }
            else
            {
                orderVM.StatusId = statusId;
                orderVM.TotalPurchaseAmount = totalPurchaseAmount;
                orderVM.CurrencyId = form.Currency;
                orderVM.CurrencyRate = form.CurrencyRate;
                orderVM.ReferenceNumber = form.ReferenceNumber;
                orderVM.ReplenishDate = form.ReplenishDate;
                context.PurchaseOrders.Update(orderVM);
            }
            await context.SaveChangesAsync(currentUserId);

            await SaveReplenishItemsAsync(form.Items, orderVM.Id);

            return orderVM.Id;
        }

        public async Task<int> SaveDraftAsync(PurchaseOrderFormVM form)
        {
            int draftStatusId = await context.Statuses.Where(s => s.Name.ToLower() == "purchase order draft").Select(s => s.Id).FirstAsync();
            return await SaveOrderAsync(form, draftStatusId);
        }

        public async Task<int> SaveReplenishAsync(PurchaseOrderFormVM form)
        {
            int draftPostedId = await context.Statuses.Where(s => s.Name.ToLower() == "purchase order posted").Select(s => s.Id).FirstAsync();
            return await SaveOrderAsync(form, draftPostedId);
        }

        public async Task SaveReplenishItemsAsync(IList<PurchaseOrderFormItemVM> Items, int PurchaseOrderId)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var citemsVM = context.PurchaseOrderLineItems.Where(s => s.PurchaseOrderId == PurchaseOrderId);
            context.PurchaseOrderLineItems.RemoveRange(citemsVM);
            
            await context.SaveChangesAsync(currentUserId);

            var itemsVM = Items.Select(s => new PurchaseOrderLineItem
            {
                Discount1 = s.Discount1,
                Discount2 = s.Discount2,
                Discount3 = s.Discount3,
                ItemId = s.Item,
                PurchaseOrderId = PurchaseOrderId,
                Quantity = s.Quantity,
                UnitPrice = s.UnitPrice
            });
            context.PurchaseOrderLineItems.AddRange(itemsVM);

            await context.SaveChangesAsync(currentUserId);

            await ReplenishInventory(PurchaseOrderId);
        }

        public async Task<int> UpdateItemsAsync(PurchaseOrderFormVM form)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

            var purchaseOrder = await context.PurchaseOrders.Where(s => s.Id == form.Id).FirstAsync();
            purchaseOrder.CurrencyId = form.Currency;
            purchaseOrder.CurrencyRate = form.CurrencyRate;
            purchaseOrder.ReferenceNumber = form.ReferenceNumber;
            purchaseOrder.ReplenishDate = form.ReplenishDate;

            context.PurchaseOrders.Update(purchaseOrder);
            await context.SaveChangesAsync(currentUserId);

            int success = 0;
            var poItems = await context.PurchaseOrderLineItems.Where(s => s.PurchaseOrderId == form.Id).ToListAsync();
            foreach (var item in poItems)
            {
                var formItem = form.Items.Where(s => s.Item == item.ItemId).FirstOrDefault();
                if (formItem != null && (item.UnitPrice != formItem.UnitPrice
                                     || item.Discount1 != formItem.Discount1
                                     || item.Discount2 != formItem.Discount2
                                     || item.Discount3 != formItem.Discount3))
                {
                    item.UnitPrice = formItem.UnitPrice;
                    item.Discount1 = formItem.Discount1;
                    item.Discount2 = formItem.Discount2;
                    item.Discount3 = formItem.Discount3;

                    context.PurchaseOrderLineItems.Update(item);
                    await context.SaveChangesAsync(currentUserId);
                    success++;
                }
            }
            return success;
        }

        private async Task ReplenishInventory(int purchaseOrderId)
        {
            int postedId = await context.Statuses.Where(s => s.Name.ToLower() == "purchase order posted").Select(s => s.Id).FirstAsync();
            var purchaseOrder = await context.PurchaseOrders.Where(s => s.Id == purchaseOrderId && s.StatusId == postedId).FirstOrDefaultAsync();
            if (purchaseOrder != null)
            {
                string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
                foreach (var item in purchaseOrder.PurchaseOrderLineItems)
                {
                    var inventory = await context.ItemInventories
                                                 .Where(s => s.ItemId == item.ItemId && s.WarehouseId == purchaseOrder.DestinationId)
                                                 .FirstOrDefaultAsync();
                    if (inventory != null)
                    {
                        inventory.Quantity += item.Quantity;
                        context.ItemInventories.Update(inventory);
                    }
                    else
                    {
                        inventory = new ItemInventory()
                        {
                            ItemId = item.ItemId,
                            Quantity = item.Quantity,
                            WarehouseId = purchaseOrder.DestinationId
                        };
                        await context.ItemInventories.AddAsync(inventory);
                    }
                    await context.SaveChangesAsync(currentUserId);
                }
            }
        }

        public async Task<bool> DeleteDraftAsync(int purchaseOrderId)
        {
            var orderVM = context.PurchaseOrders.Where(s => s.Id == purchaseOrderId).FirstOrDefault();
            if (orderVM == null)
            {
                return false;
            }
            else
            {
                string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

                var orderItemsVM = context.PurchaseOrderLineItems.Where(s => s.PurchaseOrderId == orderVM.Id);
                context.PurchaseOrderLineItems.RemoveRange(orderItemsVM);

                context.PurchaseOrders.Remove(orderVM);
                await context.SaveChangesAsync(currentUserId);

                return true;
            }
        }
    }
}