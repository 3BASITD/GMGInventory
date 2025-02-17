using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using GMGInventory.Data.Migrations;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace GMGInventory.Application.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;

        public ItemRepository(ApplicationDbContext context
             , IHttpContextAccessor httpContextAccessor
             , UserManager<User> userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        public async Task<Select2Model<JsonItemOptionsModel>> GetOptions(string search, int page)
        {
            //var items = await context.Items.Include(s => s.Unit)
            //                    .Where(s => string.IsNullOrWhiteSpace(search)
            //                             || (s.Code.ToLower().Contains(search.ToLower())))
            //                    .Select(s => new JsonItemOptionsModel
            //                    {
            //                        Id = s.Id.ToString(),
            //                        Text = s.Code,
            //                        Name = s.Name,
            //                        Quantity = 0,
            //                        Unit = s.Unit.Code
            //                    })
            //                    .ToListAsync();
            var items = from item in context.Items
                                            .Include(s => s.Unit)
                        from price in context.ItemPrices
                                             .Where(s => s.ItemId == item.Id
                                                      && s.DateCreated <= DateTime.Now)
                                             .OrderByDescending(s => s.DateCreated)
                                             .Take(1)
                                             .DefaultIfEmpty()
                        let inventory = context.ItemInventories
                                               .Where(s => s.ItemId == item.Id && s.Warehouse.Code.ToLower().Contains("main"))
                                               .Sum(s => (int?)s.Quantity) ?? 0
                        where item.IsActive == true
                           && (string.IsNullOrWhiteSpace(search)
                           || item.Code.ToLower().Contains(search.ToLower())
                           || item.Name.ToLower().Contains(search.ToLower()))
                        orderby item.Code ascending
                        select new JsonItemOptionsModel
                        {
                            Id = item.Id.ToString(),
                            InventoryCount = inventory,
                            Text = item.Code,
                            Name = item.Name,
                            PartNumber = item.PartNumber + "",
                            NetPriceD = price.NetSellingPrice,
                            Quantity = 0,
                            RetailPriceD = price.RetailSellingPrice,
                            UnitId = item.Unit.Id,
                            Unit = item.Unit.Code
                        };
            var itemsFiltered = await items.Skip((page - 1) * 50).Take(50).ToListAsync();
            return new Select2Model<JsonItemOptionsModel>
            {
                //incomplete_results = false,
                items = itemsFiltered,
                total_count = items.Count()
            };
        }

        public async Task<Select2Model<JsonItemOptionsModel>> GetPOItemOptions(int id, string search)
        {
            var items = await context.PurchaseOrderLineItems.Include(s => s.Item)
                                .Where(s => s.PurchaseOrderId == id &&
                                            string.IsNullOrWhiteSpace(search)
                                         || (s.Item.Code.ToLower().Contains(search.ToLower())))
                                .Select(s => new JsonItemOptionsModel
                                {
                                    Id = s.Id.ToString(),
                                    Text = s.Item.Code,
                                    Name = s.Item.Name,
                                    Quantity = 0,
                                    Unit = s.Item.Unit.Code
                                })
                                .ToListAsync();

            return new Select2Model<JsonItemOptionsModel>
            {
                items = items,
                total_count = items.Count()
            };
        }

        public async Task<Select2Model<JsonItemOptionsModel>> GetUnservedOptions(string search, int page, int SalesOrderId)
        {
            var items = from soitem in context.SalesOrderLineItems
                        join item in context.Items on soitem.ItemId equals item.Id
                        from price in context.ItemPrices
                                             .Where(s => s.ItemId == item.Id
                                                      && s.DateCreated <= DateTime.Now)
                                             .OrderByDescending(s => s.DateCreated)
                                             .Take(1)
                                             .DefaultIfEmpty()
                        let ii = context.ItemInventories
                                        .Where(s=>s.ItemId == item.Id)
                                        .Select(s=>s.Quantity)
                                        .Sum()
                        where item.IsActive == true
                           && soitem.SalesOrderId == SalesOrderId
                           && (string.IsNullOrWhiteSpace(search)
                           || item.Code.ToLower().Contains(search.ToLower())
                           || item.Name.ToLower().Contains(search.ToLower()))
                           && (soitem.Quantity > 0 || soitem.Quantity > ii)
                        orderby item.Code ascending
                        select new JsonItemOptionsModel
                        {
                            Id = item.Id.ToString(),
                            InventoryCount = ii,
                            Text = item.Code,
                            Name = item.Name,
                            PartNumber = item.PartNumber + "",
                            NetPriceD = price.NetSellingPrice,
                            //Quantity = soitem.Quantity - (packedQuantity - returnedQuantity),
                            Quantity = soitem.Quantity - ii,
                            RetailPriceD = price.RetailSellingPrice,
                            UnitId = item.Unit.Id,
                            Unit = item.Unit.Code
                        };
            var itemsFiltered = await items.Skip((page - 1) * 50).Take(50).ToListAsync();
            return new Select2Model<JsonItemOptionsModel>
            {
                //incomplete_results = false,
                items = itemsFiltered,
                total_count = items.Count()
            };
        }

        public async Task<Select2Model<JsonOptionsModel>> GetModelNumberOptions(string search, int page)
        {
            var items = from item in context.Items
                        where item.IsActive == true
                           && (string.IsNullOrWhiteSpace(search)
                           || (item.ModelNumber != null && item.ModelNumber.ToLower().Contains(search.ToLower())))
                        orderby item.ModelNumber ascending
                        select new JsonOptionsModel
                        {
                            Id = item.ModelNumber + "!",
                            Text = item.ModelNumber + ""
                        };
            var itemsFiltered = await items.Distinct().Skip((page - 1) * 50).Take(50).ToListAsync();
            return new Select2Model<JsonOptionsModel>
            {
                items = itemsFiltered,
                total_count = items.Count()
            };
        }

        public async Task<Select2Model<JsonOptionsModel>> GetSizeOptions(string search, int page)
        {
            var items = from item in context.Items
                        where item.IsActive == true
                           && (string.IsNullOrWhiteSpace(search)
                           || item.Size.ToLower().Contains(search.ToLower()))
                        orderby item.Size ascending
                        select new JsonOptionsModel
                        {
                            Id = item.Size + "!",
                            Text = item.Size
                        };
            var itemsFiltered = await items.Distinct().Skip((page - 1) * 50).Take(50).ToListAsync();
            return new Select2Model<JsonOptionsModel>
            {
                items = itemsFiltered,
                total_count = items.Count()
            };
        }

        public async Task<int?> GetIdAsync(string? Code)
        {
            var item = await context.Items.Where(s => s.Code == Code).FirstOrDefaultAsync();
            return item?.Id;
        }

        public async Task<Select2Model<JsonItemOptionsModel>> GetInWarehouseOptions(string search, int warehouse, int page)
        {
            //var items = await context.ItemInventories.Include(s => s.Item).ThenInclude(s => s.Unit)
            //                    .Where(s => s.WarehouseId == warehouse
            //                             && (string.IsNullOrWhiteSpace(search)
            //                             || (s.Item.Code.ToLower().Contains(search.ToLower()))
            //                             || (s.Item.Name.ToLower().Contains(search.ToLower()))))
            //                    .Select(s => new JsonItemOptionsModel
            //                    {
            //                        Id = s.Item.Id.ToString(),
            //                        Text = s.Item.Code,
            //                        Name = s.Item.Name,
            //                        Quantity = s.Quantity,
            //                        Unit = s.Item.Unit.Code
            //                    })
            //                    .OrderBy(s => s.Text)
            //                    .ToListAsync();

            var whouse = context.Warehouses.Where(s => s.Id == warehouse).FirstOrDefault();
            var items = await (from item in context.ItemInventories
                               where item.WarehouseId == warehouse
                               && item.Item.IsActive == true
                               && (search == null
                               || item.Item.Code.ToLower().Contains(search.ToLower())
                               || item.Item.Name.ToLower().Contains(search.ToLower()))
                               && item.Quantity > 0
                               orderby item.Item.Code ascending
                               select new JsonItemOptionsModel
                               {
                                   Id = item.ItemId.ToString(),
                                   Text = item.Item.Code,
                                   Name = item.Item.Name,
                                   Quantity = item.Quantity,
                                   Unit = item.Item.Unit.Code
                               }).ToListAsync();

            var itemsFiltered = items.Skip((page - 1) * 50).Take(50).ToList();
            return new Select2Model<JsonItemOptionsModel>
            {
                //incomplete_results = false,
                items = itemsFiltered,
                total_count = items.Count()
            };
        }

        public async Task<IList<JsonOptionsModel>> GetUnitOptions(int? IncludeId)
        {
            return await context.Units
                                .Select(s => new JsonOptionsModel
                                {
                                    Id = s.Id.ToString(),
                                    Text = (s.Code == s.Name ? "" : "(" + s.Code + ") ") + s.Name,
                                    Selected = IncludeId != null && IncludeId == s.Id
                                })
                                .OrderBy(s => s.Text)
                                .ToListAsync();
        }

        public async Task<int?> GetUnitIdAsync(string? Code)
        {
            var unit = await context.Units.Where(s => s.Code.ToLower() == Code.ToLower()).FirstOrDefaultAsync();
            return unit?.Id;
        }

        public async Task<int?> GetUnitIdByNameAsync(string? Name)
        {
            var unit = await context.Units.Where(s => s.Name.ToLower() == Name.ToLower()).FirstOrDefaultAsync();
            return unit?.Id;
        }

        public async Task<IList<ItemCommonVM>> GetAllAsync(ItemFilterVM filter)
        {
            //var WarehouseInventory = from inv in context.ItemInventories
            //                         select new
            //                         {
            //                             ItemId = inv.ItemId,
            //                             Quantity = inv.Quantity,
            //                             Warehouse = inv.Warehouse.Name,
            //                             HasWarehouse = filter.WarehouseId == null || inv.WarehouseId == filter.WarehouseId
            //                         };
            //var itemsVM = await (from item in context.Items
            //                     from price in context.ItemPrices
            //                                          .Where(s => s.ItemId == item.Id
            //                                                   && s.DateCreated.Date <= DateTime.Now)
            //                                          .OrderByDescending(s => s.DateCreated)
            //                                          .Take(1)
            //                                          .DefaultIfEmpty()
            //                     join inventory in WarehouseInventory on item.Id equals inventory.ItemId
            //                     let quantityPacked = context.PackLineItems
            //                                                 .Where(s => s.ItemId == item.Id)
            //                                                 .Sum(s => (int?)s.Quantity) ?? 0
            //                     let quantityInvoiced = context.InvoiceLineItems
            //                                                   .Where(s => s.ItemId == item.Id)
            //                                                   .Sum(s => (int?)s.Quantity) ?? 0
            //                     let quantityReturned = context.SalesReturnLineItems
            //                                                   .Where(s => s.ItemId == item.Id)
            //                                                   .Sum(s => (int?)s.Quantity) ?? 0
            //                     where (!filter.ActiveOnly || (filter.ActiveOnly && item.IsActive))
            //                         && (filter.CategoryId == null || filter.CategoryId == item.CategoryId)
            //                         && (filter.ItemTypeId == null || filter.ItemTypeId == item.ItemTypeId)
            //                         && (filter.BrandId == null || filter.BrandId == item.BrandId)
            //                         && inventory.HasWarehouse == true
            //                     select new ItemCommonVM
            //                     {
            //                         Brand = item.Brand.Name,
            //                         BrandId = item.Brand.Id,
            //                         Category = item.Category.Name,
            //                         CategoryId = item.Category.Id,
            //                         Code = item.Code,
            //                         Id = item.Id,
            //                         ItemType = item.ItemType.Name,
            //                         ItemTypeId = item.ItemType.Id,
            //                         InterchangeNumber = item.InterchangeNumber + "",
            //                         LocationCode = item.LocationCode == null ? null : item.LocationCode.Code,
            //                         LocationCodeId = item.LocationCode == null ? null : item.LocationCode.Id,
            //                         ModelNumber = item.ModelNumber + "",
            //                         Name = item.Name,
            //                         NetPriceD = price == null ? 0 : price.NetSellingPrice,
            //                         PartNumber = item.PartNumber + "",
            //                         QuantityInventoryD = inventory.Quantity,
            //                         QuantityServedD = quantityPacked - quantityReturned,
            //                         QuantityInvoiceD = quantityInvoiced,
            //                         RetailPriceD = price == null ? 0 : price.RetailSellingPrice,
            //                         Size = item.Size,
            //                         Unit = item.Unit.Name,
            //                         UnitId = item.Unit.Id,
            //                         Warehouse = inventory.Warehouse,
            //                         IsActive = item.IsActive,
            //                         GiveAction = false
            //                     }).ToListAsync();
            // Code, Name,  PartNumber, InterchangeNumber, Warehouse, GrossPrice, NetPrice, QuantityOnHand

            // Code, Name, PartNumber, InterchangeNumber, Warehouse, ModelNumber, Category, ItemType, Brand, Size, Unit, Quantity

            var itemsVM = from item in context.Items
                          join inventory in context.ItemInventories on item.Id equals inventory.ItemId
                          let price = item.ItemPrices
                                          .Where(s => s.DateCreated.Date <= DateTime.Now)
                                          .OrderByDescending(s => s.DateCreated)
                                          .FirstOrDefault()
                          where (!filter.ActiveOnly || (filter.ActiveOnly && item.IsActive))
                              && (filter.CategoryId == null || filter.CategoryId == item.CategoryId)
                              && (filter.ItemTypeId == null || filter.ItemTypeId == item.ItemTypeId)
                              && (filter.BrandId == null || filter.BrandId == item.BrandId)
                          select new ItemCommonVM
                          {
                              Id = item.Id,
                              Code = item.Code,
                              Name = item.Name,
                              PartNumber = item.PartNumber,
                              InterchangeNumber = item.InterchangeNumber,
                              ModelNumber = item.ModelNumber,
                              Category = item.Category.Name,
                              ItemType = item.ItemType.Name,
                              Brand = item.Brand.Name,
                              Size = item.Size,
                              Unit = item.Unit.Name,
                              QuantityD = inventory.Quantity,
                              RetailPriceD = price == null ? 0 : price.RetailSellingPrice,
                              NetPriceD = price == null ? 0 : price.NetSellingPrice,
                              Warehouse = inventory.Warehouse.Name,
                              IsActive = item.IsActive
                          };
            return await itemsVM.ToListAsync();
        }

        public async Task<ForDataTables<ItemCommonVM>> GetAllForDataTablesAsync(DataTablesRequestForm form, ItemFilterVM filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetAllAsync(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(s => new[]
                {
                    s.Code,
                    s.Name,
                    s.PartNumber,
                    s.InterchangeNumber + "",
                    s.ModelNumber + "",
                    s.Category,
                    s.ItemType,
                    s.Brand,
                    s.Size,
                    s.Unit,
                    s.QuantityD.ToString(),
                    s.RetailPriceD.ToString(),
                    s.NetPriceD.ToString(),
                    s.Warehouse
                }.Any(a => a != null && a.ToLower().Contains(form.SearchValue)));
            }


            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                if (form.SortColumn == "Quantity" || form.SortColumn == "RetailPrice" || form.SortColumn == "NetPrice")
                {
                    if (form.SortColumnDirection == "asc")
                    {
                        switch (form.SortColumn)
                        {
                            case "Quantity": data = data.OrderBy(s => s.QuantityD); break;
                            case "RetailPrice": data = data.OrderBy(s => s.RetailPriceD); break;
                            case "NetPrice": data = data.OrderBy(s => s.NetPriceD); break;
                            default: break;
                        }
                    }
                    else if (form.SortColumnDirection == "desc")
                    {
                        switch (form.SortColumn)
                        {
                            case "Quantity": data = data.OrderByDescending(s => s.QuantityD); break;
                            case "RetailPrice": data = data.OrderByDescending(s => s.RetailPriceD); break;
                            case "NetPrice": data = data.OrderByDescending(s => s.NetPriceD); break;
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

            pageList = pageList.GroupBy(s => s.Id)
                               .SelectMany(s => s.Select((j, i) => new ItemCommonVM
                               {
                                   Id = j.Id,
                                   Code = j.Code,
                                   Name = j.Name,
                                   PartNumber = j.PartNumber,
                                   InterchangeNumber = j.InterchangeNumber,
                                   ModelNumber = j.ModelNumber,
                                   Category = j.Category,
                                   ItemType = j.ItemType,
                                   Brand = j.Brand,
                                   Size = j.Size,
                                   Unit = j.Unit,
                                   QuantityD = j.QuantityD,
                                   RetailPriceD = j.RetailPriceD,
                                   NetPriceD = j.NetPriceD,
                                   Warehouse = j.Warehouse,
                                   IsActive = j.IsActive,
                                   GiveAction = i == 0
                               }))
                               .ToList();

            return new ForDataTables<ItemCommonVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public ItemViewVM? GetItemViewVM(int? id)
        {
            var itemVM = (from item in context.Items
                          from price in context.ItemPrices.Where(s => s.ItemId == item.Id && s.DateCreated <= DateTime.Now).OrderByDescending(s => s.DateCreated).Take(1).DefaultIfEmpty()
                          join inventory in context.ItemInventories on item.Id equals inventory.ItemId
                          where item.Id == id
                          select new ItemViewVM
                          {
                              Brand = item.Brand.Name,
                              Category = item.Category.Name,
                              Code = item.Code,
                              Id = item.Id,
                              InterchangeNumber = item.InterchangeNumber + "",
                              ItemType = item.ItemType.Name,
                              IsActive = item.IsActive,
                              LocationCode = item.LocationCode == null ? null : item.LocationCode.Code,
                              ModelNumber = item.ModelNumber + "",
                              Name = item.Name,
                              NetPriceD = price == null ? 0 : price.NetSellingPrice,
                              PartNumber = item.PartNumber + "",
                              QuantityD = inventory.Quantity,
                              RetailPriceD = price == null ? 0 : price.RetailSellingPrice,
                              Size = item.Size,
                              UnitCode = item.Unit.Name,
                              Warehouse = inventory.Warehouse.Name
                          }).FirstOrDefault();
            return itemVM;
        }

        public async Task<IList<ItemFileVM>> GetAllFileAsync()
        {
            var itemsVM = from item in context.Items
                              //.Include(s => s.Category)
                              //.Include(s => s.ItemType)
                              //.Include(s => s.Brand)
                              //.Include(s => s.Unit)
                          let inventory = context.ItemInventories
                                                 .Where(s => s.ItemId == item.Id && s.Warehouse.Code.ToLower().Contains("main"))
                                                 .Sum(s => (int?)s.Quantity) ?? 0
                          select new ItemFileVM
                          {
                              Brand = item.Brand.Name,
                              Category = item.Category.Name,
                              Code = item.Code,
                              Id = item.Id,
                              ItemType = item.ItemType.Name,
                              InterchangeNumber = item.InterchangeNumber + "",
                              ModelNumber = item.ModelNumber + "",
                              Name = item.Name,
                              PartNumber = item.PartNumber + "",
                              Size = item.Size,
                              Unit = item.Unit.Code,
                              Quantity = inventory,
                              IsActive = item.IsActive
                          };
            return await itemsVM.ToListAsync();
        }

        public async Task<bool> IsCodeExistAsync(string code)
        {
            return await context.Items.AnyAsync(s => s.Code == code);
        }

        public async Task<bool> IsInUseAsync(int id)
        {
            return await context.InvoiceLineItems.AnyAsync(s => s.ItemId == id)
                //|| await context.ItemInventories.AnyAsync(s => s.ItemId == id)
                //|| await context.ItemPrices.AnyAsync(s => s.ItemId == id)
                || await context.PackLineItems.AnyAsync(s => s.ItemId == id)
                || await context.PackReturnLineItems.AnyAsync(s => s.ItemId == id)
                //|| await context.PhysicalCountHistories.AnyAsync(s => s.ItemId == id)
                || await context.PurchaseOrderLineItems.AnyAsync(s => s.ItemId == id)
                || await context.SalesOrderLineItems.AnyAsync(s => s.ItemId == id)
                || await context.SalesReturnLineItems.AnyAsync(s => s.ItemId == id)
                //|| await context.StockCards.AnyAsync(s => s.ItemId == id)
                || await context.TransferItemDetails.AnyAsync(s => s.ItemId == id);
        }

        public async Task<bool> IsIdExistAsync(int id)
        {
            return await context.Items.AnyAsync(s => s.Id == id);
        }

        public async Task DeactivateAsync(int id)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var itemType = await context.Items.FirstAsync(s => s.Id == id);
            itemType.IsActive = false;
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task ReactivateAsync(int id)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var itemType = await context.Items.FirstAsync(s => s.Id == id);
            itemType.IsActive = true;
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task AddAsync(ItemCommonVM itemVM)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var item = new Item
            {
                BrandId = itemVM.BrandId,
                CategoryId = itemVM.CategoryId,
                Code = itemVM.Code,
                InterchangeNumber = itemVM.InterchangeNumber + "",
                IsActive = true,
                ItemTypeId = itemVM.ItemTypeId,
                LocationCodeId = itemVM.LocationCodeId,
                ModelNumber = itemVM.ModelNumber + "",
                Name = itemVM.Name,
                PartNumber = itemVM.PartNumber + "",
                Size = itemVM.Size + "",
                UnitId = itemVM.UnitId
            };
            await context.Items.AddAsync(item);
            await context.SaveChangesAsync(currentUserId);

            var itemPrice = new ItemPrice
            {
                ItemId = item.Id,
                NetSellingPrice = itemVM.NetPriceD,
                RetailSellingPrice = itemVM.RetailPriceD
            };
            await context.ItemPrices.AddAsync(itemPrice);
            await context.SaveChangesAsync(currentUserId);
            var itemInventories = new ItemInventory
            {
                ItemId = item.Id,
                Quantity = itemVM.QuantityD,
                WarehouseId = itemVM.WarehouseId
            };
            await context.ItemInventories.AddAsync(itemInventories);
            await context.SaveChangesAsync(currentUserId);

            var itemConversion = new ItemConversion
            {
                ItemId = item.Id,
                UnitId = itemVM.UnitId,
                Quantity = 1,
                OrderNo = 1
            };
            await context.ItemConversions.AddAsync(itemConversion);
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task UpdateAsync(ItemCommonVM itemVM)
        {
            var item = context.Items.Where(s => s.Id == itemVM.Id).FirstOrDefault();
            if (item == null)
            {
                throw new DbUpdateConcurrencyException();
            }

            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            item.BrandId = itemVM.BrandId;
            item.CategoryId = itemVM.CategoryId;
            item.InterchangeNumber = itemVM.InterchangeNumber + "";
            item.ItemTypeId = itemVM.ItemTypeId;
            item.LocationCodeId = itemVM.LocationCodeId;
            item.ModelNumber = itemVM.ModelNumber + "";
            item.Name = itemVM.Name;
            item.PartNumber = itemVM.PartNumber + "";
            item.Size = itemVM.Size + "";
            item.UnitId = itemVM.UnitId;

            context.Items.Update(item);
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task DeleteAsync(int id)
        {
            var itemInventories = context.ItemInventories.Where(s => s.ItemId == id).ToList();
            if (itemInventories.Count > 0)
            {
                context.ItemInventories.RemoveRange(itemInventories);
            }

            var itemConversions = context.ItemConversions.Where(s => s.ItemId == id).ToList();
            if (itemConversions.Count > 0)
            {
                context.ItemConversions.RemoveRange(itemConversions);
            }

            var itemPrices = context.ItemPrices.Where(s => s.ItemId == id).ToList();
            if (itemPrices.Count > 0)
            {
                context.ItemPrices.RemoveRange(itemPrices);
            }

            var physicalCount = context.PhysicalCountHistories.Where(s => s.ItemId == id).ToList();
            if (physicalCount.Count > 0)
            {
                context.PhysicalCountHistories.RemoveRange(physicalCount);
            }

            var item = context.Items.Where(s => s.Id == id).First();
            context.Items.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateQuantityAsync(int id, int quantity, string remarks)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var inventory = context.ItemInventories.Where(s => s.Id == id).First();
            var oldQuantity = inventory.Quantity;
            inventory.Quantity = quantity;
            context.ItemInventories.Update(inventory);
            await context.SaveChangesAsync(currentUserId);

            var countHistory = new PhysicalCountHistory
            {
                ItemId = inventory.ItemId,
                QuantityFrom = oldQuantity,
                QuantityTo = quantity,
                Remarks = remarks,
                WarehouseId = inventory.WarehouseId
            };
            context.PhysicalCountHistories.Add(countHistory);
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task DeleteWarehouse(int id)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var inventory = await context.ItemInventories.Where(s => s.Id == id).FirstAsync();
            context.ItemInventories.Remove(inventory);
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task<bool> UpdateOrAddItemWarehouse(int itemId, int warehouseId, int quantity, string remarks)
        {
            var item = await context.Items.Where(s => s.Id == itemId).FirstOrDefaultAsync();
            var warehouse = await context.Warehouses.Where(s => s.Id == warehouseId).FirstOrDefaultAsync();

            if (item != null && warehouse != null)
            {
                string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
                var inventory = await context.ItemInventories.Where(s => s.ItemId == itemId && s.WarehouseId == warehouseId).FirstOrDefaultAsync();
                if (inventory == null)
                {
                    inventory = new ItemInventory
                    {
                        ItemId = itemId,
                        Quantity = quantity,
                        WarehouseId = warehouseId
                    };
                    await context.ItemInventories.AddAsync(inventory);
                    await context.SaveChangesAsync(currentUserId);
                }
                else
                {
                    var countHistory = new PhysicalCountHistory
                    {
                        ItemId = itemId,
                        QuantityFrom = inventory.Quantity,
                        QuantityTo = quantity,
                        Remarks = remarks,
                        WarehouseId = warehouseId
                    };
                    context.PhysicalCountHistories.Add(countHistory);
                    await context.SaveChangesAsync(currentUserId);

                    inventory.Quantity = quantity;
                    await context.SaveChangesAsync(currentUserId);
                }
                return true;
            }
            return false;
        }

        public async Task UpdateOrAddItemPrice(int itemId, decimal retailPrice, decimal netPrice)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var price = await context.ItemPrices.Where(s => s.ItemId == itemId).FirstOrDefaultAsync();

            if (price != null)
            {
                price.RetailSellingPrice = retailPrice;
                price.NetSellingPrice = netPrice;

                context.ItemPrices.Update(price);
            }
            else
            {
                price = new ItemPrice
                {
                    ItemId = itemId,
                    RetailSellingPrice = retailPrice,
                    NetSellingPrice = netPrice
                };

                await context.ItemPrices.AddAsync(price);
            }

            await context.SaveChangesAsync(currentUserId);
        }

        private async Task<IList<ItemDetailWarehouseVM>> GetItemWarehouseAsync(int id)
        {
            var inventoryVM = from inv in context.ItemInventories
                              where inv.ItemId == id
                              select new ItemDetailWarehouseVM
                              {
                                  Code = inv.Warehouse.Code,
                                  Id = inv.Id,
                                  Name = inv.Warehouse.Name,
                                  QuantityD = inv.Quantity,
                              };
            return await inventoryVM.ToListAsync();
        }

        public async Task<ForDataTables<ItemDetailWarehouseVM>> GetItemWarehouseDTAsync(DataTablesRequestForm form, int id)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetItemWarehouseAsync(id)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Code.ToString().Contains(form.SearchValue)
                                    || x.Name.ToString().Contains(form.SearchValue)
                                    || x.QuantityD.ToString().Contains(form.SearchValue));
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
                        case "Code": data = data.OrderBy(s => s.Code); break;
                        case "Name": data = data.OrderBy(s => s.Name); break;
                        case "Quantity": data = data.OrderBy(s => s.QuantityD); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Code": data = data.OrderByDescending(s => s.Code); break;
                        case "Name": data = data.OrderByDescending(s => s.Name); break;
                        case "Quantity": data = data.OrderByDescending(s => s.QuantityD); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<ItemDetailWarehouseVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        private async Task<IList<ItemDetailPurchaseVM>> GetItemPurchaseAsync(int id)
        {
            var purchasesVM = from purchase in context.PurchaseOrderNews
                                                     .Include(s => s.DeliveryAddress)
                                                     .Include(s => s.Status)
                                                     .Include(s => s.Supplier).ThenInclude(s => s.Currency)

                              join purchaseItem in context.PurchaseOrderNewLineItems on purchase.Id equals purchaseItem.PurchaseOrderId
                              join purchaseRequest in context.PurchaseRequestLineItems on purchaseItem.RequestItemId equals purchaseRequest.Id
                              join user in userManager.Users on purchase.UserCreated equals user.Id

                              where purchaseRequest.ItemId == id
                              select new ItemDetailPurchaseVM
                              {
                                  ConversionFactorD = purchase.Supplier.Currency.Rate,
                                  Currency = purchase.Supplier.Currency.Code,
                                  PurchaseNumber = purchase.DocumentNumber,
                                  QuantityD = purchaseItem.Quantity,
                                  ReplenishBy = SystemFormat.Name(user.FirstName, user.LastName, user.Nickname),
                                  ReplenishDateD = purchase.DateCreated,
                                  Status = purchase.Status.Code,
                                  Supplier = purchase.Supplier.Code,
                                  UnitCostD = purchaseItem.UnitCost, // tinanggal na sa db
                                  UnitPriceD = purchaseItem.UnitCost,
                                  Warehouse = purchase.DeliveryAddress.Code
                              };
            return await purchasesVM.ToListAsync();

            //var purchasesVM = from purchase in context.PurchaseOrders
            //                                          .Include(s => s.Currency)
            //                                          .Include(s => s.Destination)
            //                                          .Include(s => s.Status)
            //                                          .Include(s => s.Supplier)
            //                  join purchaseItem in context.PurchaseOrderLineItems on purchase.Id equals purchaseItem.PurchaseOrderId
            //                  join user in userManager.Users on purchase.UserCreated equals user.Id
            //                  where purchaseItem.Item.Id == id
            //                  select new ItemDetailPurchaseVM
            //                  {
            //                      ConversionFactorD = purchase.CurrencyRate,
            //                      Currency = purchase.Currency.Code,
            //                      PurchaseNumber = purchase.PONumber,
            //                      QuantityD = purchaseItem.Quantity,
            //                      ReplenishBy = SystemFormat.Name(user.FirstName, user.LastName, user.Nickname),
            //                      ReplenishDateD = purchase.DateCreated,
            //                      Status = purchase.Status.Code,
            //                      Supplier = purchase.Supplier.Code,
            //                      UnitCostD = 0, // tinanggal na sa db
            //                      UnitPriceD = purchaseItem.UnitPrice,
            //                      Warehouse = purchase.Destination.Code
            //                  };
            //return await purchasesVM.ToListAsync();
        }

        public async Task<ForDataTables<ItemDetailPurchaseVM>> GetItemPurchaseDTAsync(DataTablesRequestForm form, int id)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetItemPurchaseAsync(id)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.ConversionFactorD.ToString().Contains(form.SearchValue)
                                    || x.ConvertedUnitPriceD.ToString().Contains(form.SearchValue)
                                    || x.Currency.ToString().Contains(form.SearchValue)
                                    || x.PurchaseNumber.ToLower().Contains(form.SearchValue)
                                    || x.QuantityD.ToString().Contains(form.SearchValue)
                                    || x.ReplenishBy.ToLower().Contains(form.SearchValue)
                                    || x.ReplenishDate.ToLower().Contains(form.SearchValue)
                                    || x.Status.ToLower().Contains(form.SearchValue)
                                    || x.Supplier.ToLower().Contains(form.SearchValue)
                                    || x.UnitCostD.ToString().Contains(form.SearchValue)
                                    || x.UnitPriceD.ToString().Contains(form.SearchValue)
                                    || x.Warehouse.ToLower().Contains(form.SearchValue));
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
                        case "ConversionFactor": data = data.OrderBy(s => s.ConversionFactorD); break;
                        case "ConvertedUnitPrice": data = data.OrderBy(s => s.ConvertedUnitPriceD); break;
                        case "Currency": data = data.OrderBy(s => s.Currency); break;
                        case "PurchaseNumber": data = data.OrderBy(s => s.PurchaseNumber); break;
                        case "Quantity": data = data.OrderBy(s => s.QuantityD); break;
                        case "ReplenishBy": data = data.OrderBy(s => s.ReplenishBy); break;
                        case "ReplenishDate": data = data.OrderBy(s => s.ReplenishDateD); break;
                        case "Status": data = data.OrderBy(s => s.Status); break;
                        case "Supplier": data = data.OrderBy(s => s.Supplier); break;
                        case "UnitCost": data = data.OrderBy(s => s.UnitCostD); break;
                        case "UnitPrice": data = data.OrderBy(s => s.UnitPriceD); break;
                        case "Warehouse": data = data.OrderBy(s => s.Warehouse); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "ConversionFactor": data = data.OrderByDescending(s => s.ConversionFactorD); break;
                        case "ConvertedUnitPrice": data = data.OrderByDescending(s => s.ConvertedUnitPriceD); break;
                        case "Currency": data = data.OrderByDescending(s => s.Currency); break;
                        case "PurchaseNumber": data = data.OrderByDescending(s => s.PurchaseNumber); break;
                        case "Quantity": data = data.OrderByDescending(s => s.QuantityD); break;
                        case "ReplenishBy": data = data.OrderByDescending(s => s.ReplenishBy); break;
                        case "ReplenishDate": data = data.OrderByDescending(s => s.ReplenishDateD); break;
                        case "Status": data = data.OrderByDescending(s => s.Status); break;
                        case "Supplier": data = data.OrderByDescending(s => s.Supplier); break;
                        case "UnitCost": data = data.OrderByDescending(s => s.UnitCostD); break;
                        case "UnitPrice": data = data.OrderByDescending(s => s.UnitPriceD); break;
                        case "Warehouse": data = data.OrderByDescending(s => s.Warehouse); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<ItemDetailPurchaseVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        private async Task<IList<ItemDetailSalesVM>> GetItemSalesAsync(int id)
        {

            var salesVM = from sitem in context.SalesOrderLineItems

                          let quantityPacked = context.PackLineItems
                                                      .Where(s => s.Pack.SalesOrderId == sitem.SalesOrderId
                                                               && s.ItemId == sitem.ItemId)
                                                      .Sum(s => (int?)s.Quantity) ?? 0
                          let quantityReturned = context.SalesReturnLineItems
                                                      .Where(s => s.InvoiceLineItem.Invoice.SalesOrderId == sitem.Id
                                                               && s.ItemId == sitem.ItemId)
                                                      .Sum(s => (int?)s.Quantity) ?? 0
                          let inventory = context.ItemInventories
                                                 .Where(s => s.ItemId == sitem.ItemId)
                                                 .Sum(s => (int?)s.Quantity) ?? 0
                          join user in userManager.Users on sitem.SalesOrder.UserCreated equals user.Id
                          where sitem.Item.Id == id
                          select new ItemDetailSalesVM
                          {
                              Agent = sitem.SalesOrder.Agent.Code,
                              Customer = sitem.SalesOrder.Customer.Name,
                              Discount1D = sitem.Discount1,
                              Discount2D = sitem.Discount2,
                              OrderBy = SystemFormat.Name(user.FirstName, user.LastName, user.Nickname),
                              OrderDateD = sitem.SalesOrder.DateCreated,
                              PackedQuantityD = quantityPacked,
                              QuantityD = sitem.Quantity,
                              SalesNumber = sitem.SalesOrder.SalesOrderNumber,
                              UnitPriceD = sitem.SellingPrice,
                              UnservedQuantityD = sitem.Quantity - inventory > 0 ? sitem.Quantity - inventory : 0
                          };
            return await salesVM.ToListAsync();
        }

        public async Task<ForDataTables<ItemDetailSalesVM>> GetItemSalesDTAsync(DataTablesRequestForm form, int id)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetItemSalesAsync(id)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Agent.ToLower().Contains(form.SearchValue)
                                    || x.Customer.ToLower().Contains(form.SearchValue)
                                    || x.Discount1D.ToString().Contains(form.SearchValue)
                                    || x.Discount2D.ToString().Contains(form.SearchValue)
                                    || x.OrderBy.ToLower().Contains(form.SearchValue)
                                    || x.OrderDate.ToLower().Contains(form.SearchValue)
                                    || x.PackedQuantityD.ToString().Contains(form.SearchValue)
                                    || x.QuantityD.ToString().Contains(form.SearchValue)
                                    || x.UnitPriceD.ToString().Contains(form.SearchValue)
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
                        case "Agent": data = data.OrderBy(s => s.Agent); break;
                        case "Customer": data = data.OrderBy(s => s.Customer); break;
                        case "Discount1": data = data.OrderBy(s => s.Discount1D); break;
                        case "Discount2": data = data.OrderBy(s => s.Discount2D); break;
                        case "OrderBy": data = data.OrderBy(s => s.OrderBy); break;
                        case "OrderDate": data = data.OrderBy(s => s.OrderDateD); break;
                        case "PackedQuantity": data = data.OrderBy(s => s.PackedQuantityD); break;
                        case "Quantity": data = data.OrderBy(s => s.QuantityD); break;
                        case "UnitPrice": data = data.OrderBy(s => s.UnitPriceD); break;
                        case "UnservedQuantity": data = data.OrderBy(s => s.UnservedQuantityD); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Agent": data = data.OrderByDescending(s => s.Agent); break;
                        case "Customer": data = data.OrderByDescending(s => s.Customer); break;
                        case "Discount1": data = data.OrderByDescending(s => s.Discount1D); break;
                        case "Discount2": data = data.OrderByDescending(s => s.Discount2D); break;
                        case "OrderBy": data = data.OrderByDescending(s => s.OrderBy); break;
                        case "OrderDate": data = data.OrderByDescending(s => s.OrderDateD); break;
                        case "PackedQuantity": data = data.OrderByDescending(s => s.PackedQuantityD); break;
                        case "Quantity": data = data.OrderByDescending(s => s.QuantityD); break;
                        case "UnitPrice": data = data.OrderByDescending(s => s.UnitPriceD); break;
                        case "UnservedQuantity": data = data.OrderByDescending(s => s.UnservedQuantityD); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<ItemDetailSalesVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        private async Task<IList<ItemDetailCountVM>> GetItemCountAsync(int id)
        {
            var counstsVM = from count in context.PhysicalCountHistories
                                                 .Include(s => s.Warehouse)
                            join user in userManager.Users on count.UserCreated equals user.Id
                            where count.ItemId == id
                            select new ItemDetailCountVM
                            {
                                AdjustmentBy = SystemFormat.Name(user.FirstName, user.LastName, user.Nickname),
                                AdjustmentDateD = count.DateCreated,
                                QuantityFromD = count.QuantityFrom,
                                QuantityToD = count.QuantityTo,
                                Remarks = count.Remarks + "",
                                Warehouse = count.Warehouse.Code
                            };
            return await counstsVM.ToListAsync();
        }

        public async Task<ForDataTables<ItemDetailCountVM>> GetItemCountDTAsync(DataTablesRequestForm form, int id)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetItemCountAsync(id)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.AdjustmentBy.ToLower().Contains(form.SearchValue)
                                    || x.AdjustmentDate.ToLower().Contains(form.SearchValue)
                                    || x.QuantityAdjustedD.ToString().Contains(form.SearchValue)
                                    || x.QuantityFromD.ToString().Contains(form.SearchValue)
                                    || x.QuantityToD.ToString().Contains(form.SearchValue)
                                    || x.Remarks.ToLower().Contains(form.SearchValue)
                                    || x.Warehouse.ToLower().Contains(form.SearchValue));
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
                        case "AdjustmentBy": data = data.OrderBy(s => s.AdjustmentBy); break;
                        case "AdjustmentDate": data = data.OrderBy(s => s.AdjustmentDateD); break;
                        case "QuantityAdjusted": data = data.OrderBy(s => s.QuantityAdjustedD); break;
                        case "QuantityFrom": data = data.OrderBy(s => s.QuantityFromD); break;
                        case "QuantityTo": data = data.OrderBy(s => s.QuantityToD); break;
                        case "Remarks": data = data.OrderBy(s => s.Remarks); break;
                        case "Warehouse": data = data.OrderBy(s => s.Warehouse); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "AdjustmentBy": data = data.OrderByDescending(s => s.AdjustmentBy); break;
                        case "AdjustmentDate": data = data.OrderByDescending(s => s.AdjustmentDateD); break;
                        case "QuantityAdjusted": data = data.OrderByDescending(s => s.QuantityAdjustedD); break;
                        case "QuantityFrom": data = data.OrderByDescending(s => s.QuantityFromD); break;
                        case "QuantityTo": data = data.OrderByDescending(s => s.QuantityToD); break;
                        case "Remarks": data = data.OrderByDescending(s => s.Remarks); break;
                        case "Warehouse": data = data.OrderByDescending(s => s.Warehouse); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<ItemDetailCountVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        private async Task<IList<ItemDetailPriceVM>> GetItemPriceAsync(int id)
        {
            var pricesVM = from price in context.ItemPrices
                           join user in userManager.Users on price.UserCreated equals user.Id
                           where price.ItemId == id
                           select new ItemDetailPriceVM
                           {
                               NetPriceD = price.NetSellingPrice,
                               RetailPriceD = price.RetailSellingPrice,
                               UpdateBy = SystemFormat.Name(user.FirstName, user.LastName, user.Nickname),
                               UpdateDateD = price.DateCreated
                           };
            return await pricesVM.ToListAsync();
        }

        public async Task<ForDataTables<ItemDetailPriceVM>> GetItemPriceDTAsync(DataTablesRequestForm form, int id)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetItemPriceAsync(id)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.NetPriceD.ToString().Contains(form.SearchValue)
                                    || x.RetailPriceD.ToString().Contains(form.SearchValue)
                                    || x.UpdateBy.ToLower().Contains(form.SearchValue)
                                    || x.UpdateDate.ToLower().Contains(form.SearchValue));
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
                        case "NetPrice": data = data.OrderBy(s => s.NetPriceD); break;
                        case "RetailPrice": data = data.OrderBy(s => s.RetailPriceD); break;
                        case "UpdateBy": data = data.OrderBy(s => s.UpdateBy); break;
                        case "UpdateDate": data = data.OrderBy(s => s.UpdateDateD); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "NetPrice": data = data.OrderByDescending(s => s.NetPriceD); break;
                        case "RetailPrice": data = data.OrderByDescending(s => s.RetailPriceD); break;
                        case "UpdateBy": data = data.OrderByDescending(s => s.UpdateBy); break;
                        case "UpdateDate": data = data.OrderByDescending(s => s.UpdateDateD); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<ItemDetailPriceVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        private async Task<IList<ItemDetailTransferVM>> GetItemTransferAsync(int id)
        {
            var transferVM = from titem in context.TransferItemDetails
                             join user in userManager.Users on titem.UserCreated equals user.Id
                             where titem.Item.Id == id
                             select new ItemDetailTransferVM
                             {
                                 Destination = titem.TransferItem.Destination.Code,
                                 QuantityD = titem.Quantity,
                                 Remarks = titem.TransferItem.Remarks,
                                 Source = titem.TransferItem.Source.Code,
                                 TransferBy = SystemFormat.Name(user.FirstName, user.LastName, user.Nickname),
                                 TransferDateD = titem.TransferItem.DateCreated,
                                 TransferNumber = titem.TransferItem.Id
                             };
            return await transferVM.ToListAsync();
        }

        public async Task<ForDataTables<ItemDetailTransferVM>> GetItemTransferDTAsync(DataTablesRequestForm form, int id)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetItemTransferAsync(id)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Destination.ToLower().Contains(form.SearchValue)
                                    || x.QuantityD.ToString().Contains(form.SearchValue)
                                    || x.Remarks.ToLower().Contains(form.SearchValue)
                                    || x.Source.ToLower().Contains(form.SearchValue)
                                    || x.TransferBy.ToLower().Contains(form.SearchValue)
                                    || x.TransferDate.ToLower().Contains(form.SearchValue)
                                    || x.TransferNumber.ToString().Contains(form.SearchValue));
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
                        case "Quantity": data = data.OrderBy(s => s.QuantityD); break;
                        case "Remarks": data = data.OrderBy(s => s.Remarks); break;
                        case "Source": data = data.OrderBy(s => s.Source); break;
                        case "TransferBy": data = data.OrderBy(s => s.TransferBy); break;
                        case "TransferDate": data = data.OrderBy(s => s.TransferDateD); break;
                        case "TransferNumber": data = data.OrderBy(s => s.TransferNumber); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Destination": data = data.OrderByDescending(s => s.Destination); break;
                        case "Quantity": data = data.OrderByDescending(s => s.QuantityD); break;
                        case "Remarks": data = data.OrderByDescending(s => s.Remarks); break;
                        case "Source": data = data.OrderByDescending(s => s.Source); break;
                        case "TransferBy": data = data.OrderByDescending(s => s.TransferBy); break;
                        case "TransferDate": data = data.OrderByDescending(s => s.TransferDateD); break;
                        case "TransferNumber": data = data.OrderByDescending(s => s.TransferNumber); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<ItemDetailTransferVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        private async Task<IList<ItemDetailReturnVM>> GetItemReturnAsync(int id)
        {
            var returnsVM = from ritem in context.SalesReturnLineItems
                            join user in userManager.Users on ritem.SalesReturn.UserCreated equals user.Id
                            where ritem.Item.Id == id
                            select new ItemDetailReturnVM
                            {
                                Customer = ritem.SalesReturn.SalesOrder.Customer.Code,
                                ItemCode = ritem.Item.Code,
                                ItemName = ritem.Item.Name,
                                QuantityD = ritem.Quantity,
                                ReturnBy = SystemFormat.Name(user.FirstName, user.LastName, user.Nickname),
                                ReturnDateD = ritem.SalesReturn.DateCreated,
                                ReturnNumber = ritem.SalesReturn.SalesReturnNumber
                            };

            return await returnsVM.ToListAsync();
        }

        public async Task<ForDataTables<ItemDetailReturnVM>> GetItemReturnDTAsync(DataTablesRequestForm form, int id)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetItemReturnAsync(id)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Customer.ToLower().Contains(form.SearchValue)
                                    || x.ItemCode.ToLower().Contains(form.SearchValue)
                                    || x.ItemName.ToLower().Contains(form.SearchValue)
                                    || x.QuantityD.ToString().Contains(form.SearchValue)
                                    || x.ReturnBy.ToLower().Contains(form.SearchValue)
                                    || x.ReturnDate.ToLower().Contains(form.SearchValue)
                                    || x.ReturnNumber.ToLower().Contains(form.SearchValue));
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
                        case "ItemCode": data = data.OrderBy(s => s.ItemCode); break;
                        case "ItemName": data = data.OrderBy(s => s.ItemName); break;
                        case "Quantity": data = data.OrderBy(s => s.QuantityD); break;
                        case "ReturnBy": data = data.OrderBy(s => s.ReturnBy); break;
                        case "ReturnDate": data = data.OrderBy(s => s.ReturnDateD); break;
                        case "ReturnNumber": data = data.OrderBy(s => s.ReturnNumber); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Customer": data = data.OrderByDescending(s => s.Customer); break;
                        case "ItemCode": data = data.OrderByDescending(s => s.ItemCode); break;
                        case "ItemName": data = data.OrderByDescending(s => s.ItemName); break;
                        case "Quantity": data = data.OrderByDescending(s => s.QuantityD); break;
                        case "ReturnBy": data = data.OrderByDescending(s => s.ReturnBy); break;
                        case "ReturnDate": data = data.OrderByDescending(s => s.ReturnDateD); break;
                        case "ReturnNumber": data = data.OrderBy(s => s.ReturnNumber); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<ItemDetailReturnVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }
    }
}