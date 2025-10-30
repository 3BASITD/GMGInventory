using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using GMGInventory.Data.Migrations;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Application.Repositories
{
    public class PackRepository : IPackRepository
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;
        private readonly string currentUserId;

        public PackRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
             , UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
        }

        public async Task<IList<JsonOptionsModel>> GetPackTypeOptions(int? IncludeId)
        {
            return await context.PackTypes
                .Select(s => new JsonOptionsModel
                {
                    Id = s.Id.ToString(),
                    Text = s.Name,
                    Selected = IncludeId != null && IncludeId == s.Id
                })
                .OrderBy(s => s.Text)
                .ToListAsync();
        }

        public async Task<Select2Model<JsonOptionsModel>> GetOptions(string search, int page)
        {
            var orders = from order in context.Packs
                         let quantityOrdered = context.SalesOrderLineItems
                                                      .Where(s => s.SalesOrderId == order.SalesOrderId)
                                                      .Sum(s => (int?)s.Quantity) ?? 0
                         let quantityInvoiced = context.InvoiceLineItems
                                                       .Where(s => s.Invoice.SalesOrderId == order.SalesOrderId)
                                                       .Sum(s => (int?)s.Quantity) ?? 0
                         where order.Status.Code.ToLower() != "posted"
                         && quantityOrdered != quantityInvoiced
                         && (string.IsNullOrWhiteSpace(search)
                         || (order.Id.ToString().ToLower().Contains(search.ToLower().Trim())))
                         orderby order.Id ascending
                         select new JsonOptionsModel
                         {
                             Id = order.Id.ToString(),
                             Text = order.Id.ToString()
                         };
            var ordersFiltered = await orders.Skip((page - 1) * 50).Take(50).ToListAsync();
            return new Select2Model<JsonOptionsModel>
            {
                items = ordersFiltered,
                total_count = orders.Count()
            };
        }

        public async Task<PackDetailsVM?> GetDetailsAsync(int? Id)
        {
            var packs = from pack in context.Packs
                        join user in userManager.Users on pack.UserCreated equals user.Id
                        where pack.Id == Id
                        select new PackDetailsVM
                        {
                            Agent = pack.SalesOrder.Agent.FirstName + " " + pack.SalesOrder.Agent.LastName,
                            CustomerCode = pack.SalesOrder.Customer.Code,
                            CustomerName = pack.SalesOrder.Customer.Name,
                            Forwarder = pack.SalesOrder
                                            .SalesOrderDeliveryMonitorings
                                            .Select(s => s.Forwarder.Name)
                                            .FirstOrDefault(),
                            Id = pack.Id,
                            PackBy = user.FirstName + " " + user.LastName,
                            PackDateD = pack.DateCreated,
                            SalesOrderDateD = pack.SalesOrder.DateCreated,
                            SalesOrderId = pack.SalesOrder.Id,
                            SONumber = pack.SalesOrder.SalesOrderNumber,
                            Status = pack.Status.Code
                        };
            return await packs.FirstOrDefaultAsync();
        }


        public async Task<PackDetailsPVM> PrintDetailsAsync(int? Id)
        {
            return new PackDetailsPVM
            {
                Contents = await GetItemsAsync(Id ?? 0),
                Details = await GetDetailsAsync(Id) ?? new PackDetailsVM()
            };
        }

        public async Task<PackDetailsVM?> GetDetailsBySOAsync(int? Id)
        {
            var orderVM = (from order in context.SalesOrders
                           where order.Id == Id
                           select new PackDetailsVM
                           {
                               CustomerCode = order.Customer.Code,
                               CustomerName = order.Customer.Name,
                               Forwarder = order.SalesOrderDeliveryMonitorings
                                                .Select(s => s.Forwarder.Name)
                                                .FirstOrDefault(),
                               Id = order.Id,
                               SalesOrderDateD = order.DateCreated,
                               SONumber = order.SalesOrderNumber
                           });
            return await orderVM.FirstOrDefaultAsync();
        }

        public async Task<IList<PackVM>> GetListAsync(ListFilterCommonModel filter)
        {
            var packs = from pck in context.Packs
                        join user in userManager.Users on pck.UserCreated equals user.Id
                        where filter.DateFrom <= pck.DateCreated.Date
                        && pck.DateCreated.Date <= filter.DateTo
                        && (filter.StatusCode == null
                        || (filter.StatusCode.ToLower() == "draft" && pck.Status.Name.ToLower() == "packing draft" && pck.UserCreated == currentUserId)
                        || (filter.StatusCode.ToLower() == "invoicing" && pck.Status.Name.ToLower() == "packing submitted")
                        || (filter.StatusCode.ToLower() == "invoiced" && pck.Status.Name.ToLower() == "invoice submitted"))
                        select new PackVM
                        {
                            Customer = pck.SalesOrder.Customer.Name,
                            Id = pck.Id,
                            PackedBy = SystemFormat.Name(user.FirstName, user.LastName, user.Nickname),
                            PackedDateD = pck.DateCreated,
                            PackNumber = pck.Id.ToString(),
                            SONumber = pck.SalesOrder.SalesOrderNumber,
                            Status = pck.Status.Code
                        };

            return await packs.ToListAsync();
        }

        public async Task<ForDataTables<PackVM>> GetListDTAsync(DataTablesRequestForm form, ListFilterCommonModel filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetListAsync(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Customer.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.Id.ToString().Contains(form.SearchValue)
                                    || x.PackedBy.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.PackedDateD.ToString().Contains(form.SearchValue)
                                    || x.PackNumber.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.SONumber.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.Status.ToLower().Contains(form.SearchValue.ToLower()));
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
                        case "Id": data = data.OrderBy(s => s.Id); break;
                        case "PackedBy": data = data.OrderBy(s => s.PackedBy); break;
                        case "PackedDate": data = data.OrderBy(s => s.PackedDateD); break;
                        case "PackNumber": data = data.OrderBy(s => s.PackNumber); break;
                        case "SONumber": data = data.OrderBy(s => s.SONumber); break;
                        case "Status": data = data.OrderBy(s => s.Status); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Customer": data = data.OrderByDescending(s => s.Customer); break;
                        case "Id": data = data.OrderByDescending(s => s.Id); break;
                        case "PackedBy": data = data.OrderByDescending(s => s.PackedBy); break;
                        case "PackedDate": data = data.OrderByDescending(s => s.PackedDateD); break;
                        case "PackNumber": data = data.OrderByDescending(s => s.PackNumber); break;
                        case "SONumber": data = data.OrderByDescending(s => s.SONumber); break;
                        case "Status": data = data.OrderByDescending(s => s.Status); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<PackVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList
            };
        }

        public async Task<IList<PackItemVM>> GetItemsAsync(int packId)
        {
            var itemVM = from pack in context.PackLineItems
                                              .Include(s => s.Item)
                         join order in context.SalesOrderLineItems on pack.Pack.SalesOrder.Id equals order.SalesOrder.Id
                         where pack.PackId == packId
                         && pack.Item.Id == order.Item.Id
                         orderby pack.BoxNumber ascending, pack.PackTypeId ascending
                         select new PackItemVM
                         {
                             BoxNumber = pack.BoxNumber,
                             Id = pack.Id,
                             ItemId = pack.Item.Id,
                             ItemCode = pack.Item.Code,
                             ItemName = pack.Item.Name,
                             PackType = pack.PackType.Id,
                             PackTypeName = pack.PackType.Code,
                             Quantity = pack.Quantity,
                             SalesOrderLineItemId = order.Id,
                             Unit = pack.Item.Unit.Code
                         };
            return await itemVM.ToListAsync();
        }

        public async Task<IList<PackItemVM>> GetItemsWithPriceAsync(int packId)
        {
            var itemVM = from pack in context.PackLineItems
                         join order in context.SalesOrderLineItems on pack.Pack.SalesOrder.Id equals order.SalesOrder.Id
                         let price = context.ItemPrices
                                            .Where(s => s.ItemId == pack.Item.Id && s.DateCreated <= pack.DateCreated)
                                            .OrderByDescending(s => s.DateCreated)
                                            .First()
                         let packedQuantity = context.PackLineItems
                                                     .Where(s => s.PackId == packId && s.ItemId == order.ItemId)
                                                     .Sum(s => (int?)s.Quantity) ?? 0
                         let invoicedQuantity = context.InvoiceLineItems
                                                       .Where(s => s.Invoice.PackId == packId && s.ItemId == order.ItemId)
                                                       .Sum(s => (int?)s.Quantity) ?? 0
                         where pack.PackId == packId
                         && pack.Item.Id == order.Item.Id
                         && invoicedQuantity < packedQuantity

                         group new { pack, order, price } by new { pack.ItemId, order.Discount1, order.Discount2, order.SellingPrice } into grp
                         select new PackItemVM
                         {
                             Discount1D = grp.First().order.Discount1,
                             Discount2D = grp.First().order.Discount2,
                             ItemCode = grp.First().pack.Item.Code,
                             ItemId = grp.First().order.ItemId,
                             ItemName = grp.First().pack.Item.Name,
                             Quantity = grp.Sum(s => s.pack.Quantity),
                             SellingPriceD = grp.First().order.SellingPrice,
                             SellingPriceType = grp.First().price.RetailSellingPrice > grp.First().price.NetSellingPrice ? "Gross" : "Net"
                         };
            return await itemVM.ToListAsync();
        }

        public async Task<ForDataTables<PackItemVM>> GetItemsDTAsync(DataTablesRequestForm form, int packId)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetItemsAsync(packId)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.BoxNumber.ToString().Contains(form.SearchValue)
                                    || x.ItemCode.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.ItemName.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.PackTypeName.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.Quantity.ToString().Contains(form.SearchValue));
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
                        case "BoxNumber": data = data.OrderBy(s => s.BoxNumber); break;
                        case "ItemCode": data = data.OrderBy(s => s.ItemCode); break;
                        case "ItemName": data = data.OrderBy(s => s.ItemName); break;
                        case "PackType": data = data.OrderBy(s => s.PackTypeName); break;
                        case "PackTypeName": data = data.OrderBy(s => s.PackTypeName); break;
                        case "Quantity": data = data.OrderBy(s => s.Quantity); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "BoxNumber": data = data.OrderByDescending(s => s.BoxNumber); break;
                        case "ItemCode": data = data.OrderByDescending(s => s.ItemCode); break;
                        case "ItemName": data = data.OrderByDescending(s => s.ItemName); break;
                        case "PackType": data = data.OrderByDescending(s => s.PackTypeName); break;
                        case "PackTypeName": data = data.OrderByDescending(s => s.PackTypeName); break;
                        case "Quantity": data = data.OrderByDescending(s => s.Quantity); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<PackItemVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public string? CheckItem(PackFormVM form)
        {
            var invalids = from ite in form.Items
                           join itm in context.Items on ite.Item equals itm.Id
                           let ii = (from inv in context.ItemInventories
                                     where inv.Warehouse.IsMain == true
                                     && inv.ItemId == ite.Item
                                     select inv.Quantity).FirstOrDefault()
                           where ii < ite.Quantity
                           select itm.Name + ": Ordered " + ite.Quantity + ", Available " + ii + " (Short by " + (ite.Quantity - ii) + ")<br />";
            return invalids.Any() ? string.Join(", ", invalids) : null;
        }

        public async Task<int> SaveAsync(PackFormVM form)
        {
            var orderVM = await context.SalesOrders.Where(s => s.Id == form.SalesOrderId).FirstOrDefaultAsync();
            if (orderVM == null)
            {
                return -1;
            }

            Pack pack = new()
            {
                SalesOrderId = orderVM.Id,
                StatusId = await context.Statuses
                                        .Where(s => (form.SavingAs.ToLower() == "post" && s.Name.ToLower() == "packing submitted")
                                                 || (form.SavingAs.ToLower() == "save" && s.Name.ToLower() == "packing draft"))
                                        .Select(s => s.Id)
                                        .FirstAsync()
            };

            await context.Packs.AddAsync(pack);
            await context.SaveChangesAsync(currentUserId);

            foreach (var item in form.Items)
            {
                PackLineItem packItem = new()
                {
                    BoxNumber = item.BoxNumber,
                    ItemId = item.Item,
                    PackId = pack.Id,
                    PackTypeId = item.PackType,
                    Quantity = item.Quantity
                };
                await context.PackLineItems.AddAsync(packItem);
                await context.SaveChangesAsync(currentUserId);

                if (form.SavingAs.ToLower() == "post")
                {
                    var warehouseId = await context.Warehouses
                                                   .Where(s => s.IsMain == true)
                                                   .Select(s => s.Id)
                                                   .FirstAsync();
                    var itemInventory = await context.ItemInventories
                                                     .Where(s => s.ItemId == item.Item && s.WarehouseId == warehouseId)
                                                     .FirstOrDefaultAsync();

                    if (itemInventory != null)
                    {
                        var oldInventoryQuantity = itemInventory.Quantity;
                        if (itemInventory.Quantity >= item.Quantity)
                        {
                            itemInventory.Quantity -= item.Quantity;
                        }
                        else if (itemInventory.Quantity < 0)
                        {
                            itemInventory.Quantity += item.Quantity;
                        }
                        else
                        {
                            itemInventory.Quantity = 0;
                        }
                        context.ItemInventories.Update(itemInventory);
                        await context.SaveChangesAsync(currentUserId);

                        var countHistory = new PhysicalCountHistory
                        {
                            ItemId = item.Item,
                            QuantityFrom = oldInventoryQuantity,
                            QuantityTo = itemInventory.Quantity,
                            Remarks = "Pack Posted",
                            WarehouseId = warehouseId
                        };
                        context.PhysicalCountHistories.Add(countHistory);
                        await context.SaveChangesAsync(currentUserId);
                    }
                    else
                    {
                        itemInventory = new ItemInventory
                        {
                            ItemId = item.Item,
                            Quantity = item.Quantity,
                            WarehouseId = warehouseId
                        };
                        await context.ItemInventories.AddAsync(itemInventory);
                        await context.SaveChangesAsync(currentUserId);

                        var countHistory = new PhysicalCountHistory
                        {
                            ItemId = item.Item,
                            QuantityFrom = 0,
                            QuantityTo = item.Quantity,
                            Remarks = "Pack Posted",
                            WarehouseId = warehouseId
                        };
                        context.PhysicalCountHistories.Add(countHistory);
                        await context.SaveChangesAsync(currentUserId);
                    }
                }
            }

            var quantityOrdered = await context.SalesOrderLineItems.Where(s => s.SalesOrderId == form.SalesOrderId).SumAsync(s => s.Quantity);
            var quantityPacked = await context.PackLineItems.Where(s => s.Pack.SalesOrderId == form.SalesOrderId).SumAsync(s => (int?)s.Quantity) ?? 0;
            quantityPacked += form.Items.Sum(s => s.Quantity);

            if (quantityOrdered <= quantityPacked && form.SavingAs == "post")
            {
                orderVM.StatusId = await context.Statuses
                                                .Where(s => s.Name.ToLower() == "fully packed")
                                                .Select(s => s.Id)
                                                .FirstAsync();
            }
            context.SalesOrders.Update(orderVM);
            await context.SaveChangesAsync(currentUserId);

            return pack.Id;
        }

        public async Task<int> UpdateAsync(PackFormVM form)
        {
            var orderVM = await context.SalesOrders.Where(s => s.Id == form.SalesOrderId).FirstOrDefaultAsync();
            var packVM = await context.Packs.Where(s => s.Id == form.Id).FirstOrDefaultAsync();
            if (orderVM == null || packVM == null)
            {
                return -1;
            }

            //delete pack items and adjust references
            var packItems = await context.PackLineItems.Where(s => s.PackId == form.Id).ToListAsync();
            context.PackLineItems.RemoveRange(packItems);
            await context.SaveChangesAsync(currentUserId);
            //end of delete pack items

            if (form.SavingAs != "delete")
            {
                var quantityOrdered = await context.SalesOrderLineItems.Where(s => s.SalesOrderId == form.SalesOrderId).SumAsync(s => s.Quantity);
                var quantityPacked = await context.PackLineItems.Where(s => s.Pack.SalesOrderId == form.SalesOrderId).SumAsync(s => (int?)s.Quantity) ?? 0;
                quantityPacked += form.Items.Sum(s => s.Quantity);

                if (quantityOrdered <= quantityPacked && form.SavingAs == "post")
                {
                    orderVM.StatusId = await context.Statuses
                                                    .Where(s => s.Name.ToLower() == "fully packed")
                                                    .Select(s => s.Id)
                                                    .FirstAsync();
                }
                await context.SaveChangesAsync(currentUserId);

                packVM.StatusId = await context.Statuses
                                               .Where(s => (form.SavingAs.ToLower() == "post" && s.Name.ToLower() == "packing submitted")
                                                        || (form.SavingAs.ToLower() == "save" && s.Name.ToLower() == "packing draft"))
                                               .Select(s => s.Id)
                                               .FirstAsync();

                context.Packs.Update(packVM);
                await context.SaveChangesAsync(currentUserId);

                foreach (var item in form.Items)
                {
                    PackLineItem packItem = new()
                    {
                        BoxNumber = item.BoxNumber,
                        ItemId = item.Item,
                        PackId = packVM.Id,
                        PackTypeId = item.PackType,
                        Quantity = item.Quantity
                    };
                    await context.PackLineItems.AddAsync(packItem);
                    //await context.SaveChangesAsync(currentUserId);

                    if (form.SavingAs.ToLower() == "post")
                    {
                        var warehouseId = await context.Warehouses
                                                       .Where(s => s.IsMain == true)
                                                       .Select(s => s.Id)
                                                       .FirstAsync();
                        var itemInventory = await context.ItemInventories
                                                         .Where(s => s.ItemId == item.Item && s.WarehouseId == warehouseId)
                                                         .FirstOrDefaultAsync();

                        if (itemInventory != null)
                        {
                            var oldInventoryQuantity = itemInventory.Quantity;
                            if (itemInventory.Quantity >= item.Quantity)
                            {
                                itemInventory.Quantity -= item.Quantity;
                            }
                            else if (itemInventory.Quantity < 0)
                            {
                                itemInventory.Quantity += item.Quantity;
                            }
                            else
                            {
                                itemInventory.Quantity = 0;
                            }
                            context.ItemInventories.Update(itemInventory);
                            //await context.SaveChangesAsync(currentUserId);

                            var countHistory = new PhysicalCountHistory
                            {
                                ItemId = item.Item,
                                QuantityFrom = oldInventoryQuantity,
                                QuantityTo = itemInventory.Quantity,
                                Remarks = "Pack Posted",
                                WarehouseId = warehouseId
                            };
                            context.PhysicalCountHistories.Add(countHistory);
                            //await context.SaveChangesAsync(currentUserId);
                        }
                        else
                        {

                            var countHistory = new PhysicalCountHistory
                            {
                                ItemId = item.Item,
                                QuantityFrom = 0,
                                QuantityTo = item.Quantity,
                                Remarks = "Pack Posted",
                                WarehouseId = warehouseId
                            };
                            context.PhysicalCountHistories.Add(countHistory);
                            //await context.SaveChangesAsync(currentUserId);
                        }
                    }
                }

                await context.SaveChangesAsync(currentUserId);
                return packVM.Id;
            }
            else
            {
                context.Packs.Remove(packVM);
                await context.SaveChangesAsync(currentUserId);
                return -2;
            }
        }

        public async Task<ResponseModelData<int>> AddAPI(PackFormAPIVM form)
        {
            var orderVM = await context.SalesOrders.Where(s => s.Id == form.SalesOrderId).FirstOrDefaultAsync();
            if (orderVM == null)
                return new ResponseModelData<int>
                {
                    Status = "Error",
                    Message = "Sales Order cannot be found",
                    Data = -1
                };


            var checkItems = CheckItem(new PackFormVM { Items = form.Items });
            if (checkItems != null)
                return new ResponseModelData<int>
                {
                    Status = "Error",
                    Message = checkItems,
                    Data = -1
                };

            Pack pack = new()
            {
                SalesOrderId = orderVM.Id,
                StatusId = await context.Statuses
                                        .Where(s => s.Name.ToLower() == "packing draft")
                                        .Select(s => s.Id)
                                        .FirstAsync()
            };

            await context.Packs.AddAsync(pack);
            await context.SaveChangesAsync(currentUserId);

            foreach (var item in form.Items)
            {
                PackLineItem packItem = new()
                {
                    BoxNumber = item.BoxNumber,
                    ItemId = item.Item,
                    PackId = pack.Id,
                    PackTypeId = item.PackType,
                    Quantity = item.Quantity
                };
                await context.PackLineItems.AddAsync(packItem);
                await context.SaveChangesAsync(currentUserId);
            }

            var quantityOrdered = await context.SalesOrderLineItems.Where(s => s.SalesOrderId == form.SalesOrderId).SumAsync(s => s.Quantity);
            var quantityPacked = await context.PackLineItems.Where(s => s.Pack.SalesOrderId == form.SalesOrderId).SumAsync(s => (int?)s.Quantity) ?? 0;
            quantityPacked += form.Items.Sum(s => s.Quantity);


            return new ResponseModelData<int>
            {
                Status = "Success",
                Message = "Pack successfully saved as draft",
                Data = pack.Id
            };
        }
    }
}