using DocumentFormat.OpenXml.Drawing.Charts;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using GMGInventory.Data.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

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
                         || (order.Id.ToString().ToLower().Contains(search.Trim().ToLower())))
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
                            Branch = pack.SalesOrder.Customer.Branch,
                            CustomerrId = pack.SalesOrder.Customer.Id,
                            CustomerCode = pack.SalesOrder.Customer.Code,
                            CustomerName = pack.SalesOrder.Customer.Name,
                            Forwarder = pack.SalesOrder.Forwarder != null ? pack.SalesOrder.Forwarder.Name : null,
                            Id = pack.Id,
                            PackBy = user.FirstName + " " + user.LastName,
                            PackDateD = pack.DateCreated,
                            PriorityD = pack.SalesOrder.Customer.Priority,
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
                        || (filter.StatusCode.Trim().ToLower() == "draft" && pck.Status.Name.ToLower() == "packing draft" && pck.UserCreated == currentUserId)
                        || (filter.StatusCode.Trim().ToLower() == "invoicing" && pck.Status.Name.ToLower() == "packing submitted")
                        || (filter.StatusCode.Trim().ToLower() == "invoiced" && pck.Status.Name.ToLower() == "invoice submitted"))
                        select new PackVM
                        {
                            CustomerCode = pck.SalesOrder.Customer.Code,
                            CustomerId = pck.SalesOrder.Customer.Id,
                            CustomerName = pck.SalesOrder.Customer.Name,
                            Id = pck.Id,
                            PackedBy = SystemFormat.Name(user.FirstName, user.LastName, user.Nickname),
                            PackedDateD = pck.DateCreated,
                            PackNumber = pck.Id.ToString(),
                            PriorityD = pck.SalesOrder.Customer.Priority,
                            SONumber = pck.SalesOrder.SalesOrderNumber,
                            SalesOrderId = pck.SalesOrder.Id,
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
                data = data.Where(s => new[]
                {
                    s.CustomerCode,
                    s.CustomerName,
                    s.Id.ToString(),
                    s.PackedBy,
                    s.PackedDate,
                    s.PackNumber,
                    s.Priority,
                    s.SONumber,
                    s.Status
                }.Any(a => a != null && a.ToLower().Contains(form.SearchValue.ToLower())));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                var direction = form.SortColumnDirection.ToLower();
                var sortExpr = $"{form.SortColumn} {direction}";
                IOrderedQueryable<PackVM> orderedData = null;

                if (form.SortColumn.ToLower().Contains("date") || form.SortColumn == "Priority")
                {
                    orderedData = direction == "asc" ? form.SortColumn switch
                    {
                        "PackedDate" => data.OrderBy(s => s.PackedDateD),
                        "Priority" => data.OrderBy(s => s.PriorityD),
                        _ => data.OrderBy(sortExpr)
                    }
                    : form.SortColumn switch
                    {
                        "PackedDate" => data.OrderByDescending(s => s.PackedDateD),
                        "Priority" => data.OrderByDescending(s => s.PriorityD),
                        _ => data.OrderBy(sortExpr)
                    };

                    data = orderedData.ThenBy($"Priority {direction}")
                                      .ThenBy($"Id {direction}");
                }
                else
                {
                    data = data.OrderBy($"{sortExpr}, Priority {direction}, Id {direction}");
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

        public async Task<IList<APIPack>> GetListAPI(DateTime? dt)
        {
            DateTime dtNow = DateTime.Now;
            DateTime dtYesterday = dtNow.AddDays(-1);
            var packs = from pack in context.Packs
                        join userc in userManager.Users on pack.UserCreated equals userc.Id
                        join userm in userManager.Users on pack.UserModified equals userm.Id into um
                        from userm in um.DefaultIfEmpty()
                        where ((dt != null && pack.PackDate.Date == dt.Value.Date)
                        || (dt == null && (pack.BarcotechRequestDate == null
                        || pack.BarcotechRequestDate > dtYesterday)))
                     && pack.Status.Code.Trim().ToLower() == "posted"
                        select new APIPack
                        {
                            Id = pack.Id,
                            SalesOrderNumber = pack.SalesOrder.SalesOrderNumber,
                            PackDate = pack.DateCreated,
                            UserCreated = userc.FirstName + " " + userc.LastName,
                            DateCreated = pack.DateCreated,
                            LastUserModified = userm == null ? null : userm.FirstName + " " + userm.LastName,
                            LastDateModified = pack.DateModified,
                            LineItems = from item in pack.PackLineItems
                                        select new APIPackLineItem
                                        {
                                            Id = item.Id,
                                            PackType = item.PackType.Code,
                                            BoxNumber = item.BoxNumber,
                                            ItemId = item.Item.Id,
                                            ItemCode = item.Item.Code,
                                            Quantity = item.Quantity
                                        }
                        };
            var resultPacks = await packs.ToListAsync();
            foreach (var pack in resultPacks)
            {
                var retrievedPack = context.Packs.Where(s => s.Id == pack.Id).First();
                if (retrievedPack.BarcotechRequestDate == null)
                {
                    retrievedPack.BarcotechRequestDate = dtNow;
                    context.SaveChanges();
                }
            }

            return resultPacks;
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
                             ItemName = pack.Item.Name + (order.ItemPromo == null ? "" : " (" + order.ItemPromo.Name + ")"),
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
                             ItemName = grp.First().pack.Item.Name + (grp.First().order.ItemPromo == null ? "" : " (" + grp.First().order.ItemPromo.Name + ")"),
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

            var invalids = new List<string>();

            foreach (var item in form.Items)
            {
                if (item.Quantity == 0)
                {
                    var tempId = item.ItemId?.ToString();
                    invalids.Add($"Nothing to pack: {(tempId ?? item.ItemCode ?? item.ItemName)}");
                    continue;
                }
                var sitem = context.Items.Where(s => s.Id == item.ItemId || s.Code == item.ItemCode || s.Name == item.ItemName).FirstOrDefault();
                if (sitem == null)
                {
                    var tempId = item.ItemId?.ToString();
                    invalids.Add($"Invalid Item. Record cannot be found: {(tempId ?? item.ItemCode ?? item.ItemName)}");
                    continue;
                }

                var stype = context.PackTypes.Where(s => s.Id == item.PackTypeId || s.Name == item.PackTypeName).FirstOrDefault();
                if (stype == null)
                {
                    var tempId = item.PackTypeId?.ToString();
                    invalids.Add($"Invalid Pack Type. Record cannot be found: {(tempId ?? item.PackTypeName)}");
                    continue;
                }
                var sorder = context.SalesOrders.Where(s => s.Id == form.SalesOrderId || s.SalesOrderNumber == form.SalesOrderNumber).FirstOrDefault();
                if (sorder == null)
                {
                    var tempId = form.SalesOrderId?.ToString();
                    invalids.Add($"Invalid Sales Order. Record cannot be found: {(tempId ?? form.SalesOrderNumber)}");
                    continue;
                }
                var soli = context.SalesOrderLineItems.Where(s => s.SalesOrderId == sorder.Id && s.ItemId == sitem.Id).FirstOrDefault();
                if (soli == null)
                {
                    invalids.Add($"Invalid Item. Record cannot be found: {sitem.Code} for {sorder.SalesOrderNumber}");
                    continue;
                }

                var sinv = context.ItemInventories.Where(s => s.Warehouse.IsMain == true && s.ItemId == sitem.Id).FirstOrDefault();
                if (sinv == null || sinv.Quantity < item.Quantity)
                {
                    var tempId = item.ItemId?.ToString();
                    invalids.Add($"Insufficient Quantity on Stock: {item.Quantity} of {(tempId ?? item.ItemCode ?? item.ItemName)}");
                    continue;
                }

                var pquantity = context.PackLineItems
                                       .Where(s => s.Pack.SalesOrderId == sorder.Id
                                                && s.ItemId == sitem.Id
                                                && s.Pack.Status.Name.ToLower() != "packing draft")
                                       .Select(s => (int?)s.Quantity)
                                       .Sum() ?? 0;
                if (item.Quantity > soli.Quantity - pquantity)
                {
                    invalids.Add($"Invalid Item Quantity: {(soli.Quantity - pquantity)} remaining to pack for {sitem.Code}");
                    continue;
                }
            }

            return invalids.Any() ? string.Join(", ", invalids) : null;
        }

        // new
        public async Task<int> SaveAsync(PackFormVM form)
        {
            var orderVM = await context.SalesOrders.Where(s => s.Id == form.SalesOrderId).FirstOrDefaultAsync();
            if (orderVM == null)
            {
                return -1;
            }
            var warehouseId = await context.Warehouses
                                           .Where(s => s.IsMain == true)
                                           .Select(s => s.Id)
                                           .FirstAsync();

            Pack pack = new()
            {
                SalesOrderId = orderVM.Id,
                WarehouseId = warehouseId,
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
                    ItemId = item.ItemId!.Value,
                    PackId = pack.Id,
                    PackTypeId = item.PackTypeId!.Value,
                    Quantity = item.Quantity
                };
                await context.PackLineItems.AddAsync(packItem);
                await context.SaveChangesAsync(currentUserId);

                if (form.SavingAs.ToLower() == "post")
                {
                    await AdjustingQuantity(item.ItemId!.Value, warehouseId, item.Quantity, "Pack Posted");
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

        // update
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

            var warehouseId = await context.Warehouses
                                           .Where(s => s.IsMain == true)
                                           .Select(s => s.Id)
                                           .FirstAsync();
            packVM.WarehouseId = warehouseId;
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
                    ItemId = item.ItemId!.Value,
                    PackId = packVM.Id,
                    PackTypeId = item.PackTypeId!.Value,
                    Quantity = item.Quantity
                };
                await context.PackLineItems.AddAsync(packItem);
                //await context.SaveChangesAsync(currentUserId);

                if (form.SavingAs.ToLower() == "post")
                {
                    await AdjustingQuantity(item.ItemId!.Value, warehouseId, item.Quantity, "Pack Posted");
                }
            }

            await context.SaveChangesAsync(currentUserId);
            return packVM.Id;
        }

        private async Task AdjustingQuantity(int ItemId, int WarehouseId, int Quantity, string Action)
        {

            var itemInventory = await context.ItemInventories
                                             .Where(s => s.ItemId == ItemId && s.WarehouseId == WarehouseId)
                                             .FirstOrDefaultAsync();

            if (itemInventory != null)
            {
                var oldInventoryQuantity = itemInventory.Quantity;
                if (itemInventory.Quantity >= Quantity)
                {
                    itemInventory.Quantity -= Quantity;
                }
                else if (itemInventory.Quantity < 0)
                {
                    itemInventory.Quantity += Quantity;
                }
                else
                {
                    itemInventory.Quantity = 0;
                }
                context.ItemInventories.Update(itemInventory);
                await context.SaveChangesAsync(currentUserId);

                var countHistory = new PhysicalCountHistory
                {
                    ItemId = ItemId,
                    QuantityFrom = oldInventoryQuantity,
                    QuantityTo = itemInventory.Quantity,
                    Remarks = Action,
                    WarehouseId = WarehouseId
                };
                context.PhysicalCountHistories.Add(countHistory);
                await context.SaveChangesAsync(currentUserId);
            }
            else
            {
                itemInventory = new ItemInventory
                {
                    ItemId = ItemId,
                    Quantity = Quantity,
                    WarehouseId = WarehouseId
                };
                await context.ItemInventories.AddAsync(itemInventory);
                await context.SaveChangesAsync(currentUserId);

                var countHistory = new PhysicalCountHistory
                {
                    ItemId = ItemId,
                    QuantityFrom = 0,
                    QuantityTo = Quantity,
                    Remarks = Action,
                    WarehouseId = WarehouseId
                };
                context.PhysicalCountHistories.Add(countHistory);
                await context.SaveChangesAsync(currentUserId);
            }
        }

        public async Task<ResponseModel> CancelAsync(int Id)
        {
            try
            {
                var pack = await context.Packs
                                        .Include(s => s.Status)
                                        .Include(s => s.Invoices)
                                        .Include(s => s.ReceptaclePrintHistories)
                                        .Where(s => s.Id == Id)
                                        .FirstOrDefaultAsync();

                if (pack == null)
                {
                    return new ResponseModel { Status = "Error", Message = "Invalid pack, Record cannot be found" };
                }
                else if (pack.Status.Name.Trim().ToLower() != "packing submitted")
                {
                    return new ResponseModel { Status = "Error", Message = "Only submitted can be cancelled." };
                }
                else if (pack.Invoices.Count > 0)
                {
                    return new ResponseModel { Status = "Error", Message = "Record already used in Invoice. Cancel cannot proceed" };
                }
                else if (pack.ReceptaclePrintHistories.Count > 0)
                {
                    return new ResponseModel { Status = "Error", Message = "Record already used in Receptacle Print History. Cancel cannot proceed" };
                }
                else
                {
                    var draftStatus = await context.Statuses.Where(s => s.Name.Trim().ToLower() == "packing draft").Select(s => s.Id).FirstAsync();

                    pack.StatusId = draftStatus;

                    context.Packs.Update(pack);
                    await context.SaveChangesAsync(currentUserId);

                    var warehouseId = pack.WarehouseId ?? await context.Warehouses
                                                                       .Where(s => s.IsMain == true)
                                                                       .Select(s => s.Id)
                                                                       .FirstAsync();

                    var packItems = await context.PackLineItems.Where(s => s.PackId == pack.Id).ToListAsync();

                    foreach (var item in packItems)
                    {
                        await AdjustingQuantity(item.ItemId, warehouseId, item.Quantity * -1, "Pack Cancelled");
                    }

                    return new ResponseModel { Status = "Success", Message = "Pack successfully cancelled" };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Status = "Error",
                    Message = SystemFormat.GetInnermostExceptionMessage(ex)
                };
            }
        }

        public async Task<ResponseModel> DeleteAsync(int Id)
        {
            try
            {
                var pack = await context.Packs
                                        .Include(s => s.Status)
                                        .Include(s => s.Invoices)
                                        .Include(s => s.ReceptaclePrintHistories)
                                        .Where(s => s.Id == Id)
                                        .FirstOrDefaultAsync();

                if (pack == null)
                {
                    return new ResponseModel { Status = "Error", Message = "Invalid pack, Record cannot be found" };
                }
                else if (pack.Status.Name.ToLower() != "packing draft")
                {
                    return new ResponseModel { Status = "Error", Message = "Only draft can be deleted." };
                }
                else if (pack.Invoices.Count > 0)
                {
                    return new ResponseModel { Status = "Error", Message = "Record already used in Invoice. Delete cannot proceed" };
                }
                else if (pack.ReceptaclePrintHistories.Count > 0)
                {
                    return new ResponseModel { Status = "Error", Message = "Record already used in Receptacle Print History. Delete cannot proceed" };
                }
                else
                {
                    var packItems = await context.PackLineItems
                                                 .Where(s => s.PackId == Id)
                                                 .ToListAsync();

                    context.PackLineItems.RemoveRange(packItems);

                    context.Packs.Remove(pack);
                    await context.SaveChangesAsync(currentUserId);

                    return new ResponseModel { Status = "Success", Message = "Pack successfully deleted" };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Status = "Error",
                    Message = SystemFormat.GetInnermostExceptionMessage(ex)
                };
            }
        }

        public async Task<ResponseModelData<int>> AddAPI(PackFormAPIVM form)
        {
            var orderVM = await context.SalesOrders
                                       .Where(s => s.Id == form.SalesOrderId || s.SalesOrderNumber == form.SalesOrderNumber)
                                       .Include(s => s.Status)
                                       .FirstOrDefaultAsync();
            if (orderVM == null)
                return new ResponseModelData<int>
                {
                    Status = "Error",
                    Message = "Invalid Sales Order. Record cannot be found",
                    Data = -1
                };
            if (orderVM.Status.Code.Trim().ToLower() == "draft" || orderVM.Status.Code.Trim().ToLower() == "disapproved")
                return new ResponseModelData<int>
                {
                    Status = "Error",
                    Message = $"Sales Order cannot be used with current status: {orderVM.Status.Code}",
                    Data = -1
                };

            var checkItems = CheckItem(new PackFormVM
            {
                SalesOrderId = form.SalesOrderId,
                SalesOrderNumber = form.SalesOrderNumber,
                Items = form.Items
            });
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
                var itemTemp = await context.Items.Where(s => s.Id == item.ItemId || s.Code == item.ItemCode || s.Name == item.ItemName).FirstAsync();
                var packTypeTemp = await context.PackTypes.Where(s => s.Id == item.PackTypeId || s.Name == item.PackTypeName).FirstAsync();
                PackLineItem packItem = new()
                {
                    BoxNumber = item.BoxNumber,
                    ItemId = itemTemp.Id,
                    PackId = pack.Id,
                    PackTypeId = packTypeTemp.Id,
                    Quantity = item.Quantity
                };
                await context.PackLineItems.AddAsync(packItem);
                await context.SaveChangesAsync(currentUserId);
            }

            return new ResponseModelData<int>
            {
                Status = "Success",
                Message = "Pack successfully saved as draft",
                Data = pack.Id
            };
        }
    }
}