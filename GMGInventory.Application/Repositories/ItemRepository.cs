using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;

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
                        from price in context.ItemPrices
                                             .Where(s => s.ItemId == item.Id
                                                      && s.DateCreated <= DateTime.Now)
                                             .OrderByDescending(s => s.DateCreated)
                                             .Take(1)
                                             .DefaultIfEmpty()
                        let inventory = context.ItemInventories
                                               .Where(s => s.ItemId == item.Id && s.Warehouse.Code.ToLower().Contains("main"))
                                               .Sum(s => s.Quantity)
                        let soli = context.SalesOrderLineItems
                                          .Where(s => s.ItemId == item.Id)
                                          .Sum(s => s.ServedQuantity)
                        let inv = context.InvoiceLineItems
                                          .Where(s => s.ItemId == item.Id)
                                          .Sum(s => s.Quantity)
                        where item.IsActive == true
                           && (string.IsNullOrWhiteSpace(search)
                           || item.Code.ToLower().Contains(search.ToLower())
                           || item.Name.ToLower().Contains(search.ToLower()))
                        orderby item.Code ascending
                        select new JsonItemOptionsModel
                        {
                            Id = item.Id.ToString(),
                            InventoryCount = inventory,// + soli - inv,
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
                        let inventory = context.ItemInventories
                                               .Where(s => s.ItemId == item.Id && s.Warehouse.Code.ToLower().Contains("main"))
                                               .Sum(s => s.Quantity)
                        let soli = context.SalesOrderLineItems
                                          .Where(s => s.ItemId == item.Id)
                                          .Sum(s => s.ServedQuantity)
                        let inv = context.InvoiceLineItems
                                          .Where(s => s.ItemId == item.Id)
                                          .Sum(s => s.Quantity)
                        where item.IsActive == true
                           && soitem.SalesOrderId == SalesOrderId
                           //&& soitem.UnservedQuantity > 0
                           && (string.IsNullOrWhiteSpace(search)
                           || item.Code.ToLower().Contains(search.ToLower())
                           || item.Name.ToLower().Contains(search.ToLower()))
                        orderby item.Code ascending
                        select new JsonItemOptionsModel
                        {
                            Id = item.Id.ToString(),
                            InventoryCount = inventory,// + soli - inv,
                            Text = item.Code,
                            Name = item.Name,
                            PartNumber = item.PartNumber + "",
                            NetPriceD = price.NetSellingPrice,
                            Quantity = soitem.UnservedQuantity,
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
                               let served = context.SalesOrderLineItems
                                                   .Where(s => s.ItemId == item.Id && whouse != null && whouse.Code.ToLower().Contains("main"))
                                                   .Sum(s => s.ServedQuantity)
                               let inv = context.InvoiceLineItems
                                                 .Where(s => s.ItemId == item.Id)
                                                 .Sum(s => s.Quantity)
                               where item.WarehouseId == warehouse
                               && item.Item.IsActive == true
                               && (search == null
                               || item.Item.Code.ToLower().Contains(search.ToLower())
                               || item.Item.Name.ToLower().Contains(search.ToLower()))
                               && (item.Quantity + served) > 0
                               orderby item.Item.Code ascending
                               select new JsonItemOptionsModel
                               {
                                   Id = item.ItemId.ToString(),
                                   Text = item.Item.Code,
                                   Name = item.Item.Name,
                                   Quantity = item.Quantity,// + served - inv,
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
            //                         group inv by inv.ItemId into invgrp
            //                         select new
            //                         {
            //                             ItemId = invgrp.Key,
            //                             Quantity = invgrp.Sum(s => s.Quantity),
            //                             Warehouse = string.Join(", ", invgrp.Select(s => s.Warehouse.Name)),
            //                             HasWarehouse = filter.WarehouseId == null || invgrp.Any(s => s.WarehouseId == filter.WarehouseId)
            //                         };
            var WarehouseInventory = from inv in context.ItemInventories
                                     select new
                                     {
                                         ItemId = inv.ItemId,
                                         Quantity = inv.Quantity,
                                         Warehouse = inv.Warehouse.Name,
                                         HasWarehouse = filter.WarehouseId == null || inv.WarehouseId == filter.WarehouseId
                                     };
            var ServedQuantity = from soli in context.SalesOrderLineItems
                                 group soli by soli.ItemId into soligrp
                                 select new
                                 {
                                     ItemId = soligrp.Key,
                                     Quantity = soligrp.Sum(s => s.ServedQuantity)
                                 };
            var InvoicedQuantity = from inv in context.InvoiceLineItems
                                   group inv by inv.ItemId into invgrp
                                   select new
                                   {
                                       ItemId = invgrp.Key,
                                       Quantity = invgrp.Sum(s => s.Quantity)
                                   };
            var itemsVM = await (from item in context.Items
                                 from price in context.ItemPrices
                                                      .Where(s => s.ItemId == item.Id
                                                               && s.DateCreated.Date <= DateTime.Now)
                                                      .OrderByDescending(s => s.DateCreated)
                                                      .Take(1)
                                                      .DefaultIfEmpty()
                                 join inventory in WarehouseInventory on item.Id equals inventory.ItemId
                                 join soli in ServedQuantity on item.Id equals soli.ItemId into soligrp
                                 from subsoli in soligrp.DefaultIfEmpty()
                                 join inv in InvoicedQuantity on item.Id equals inv.ItemId into invgrp
                                 from subinv in invgrp.DefaultIfEmpty()
                                 where (!filter.ActiveOnly || (filter.ActiveOnly && item.IsActive))
                                     && (filter.CategoryId == null || filter.CategoryId == item.CategoryId)
                                     && (filter.ItemTypeId == null || filter.ItemTypeId == item.ItemTypeId)
                                     && (filter.BrandId == null || filter.BrandId == item.BrandId)
                                     && inventory.HasWarehouse == true
                                 select new ItemCommonVM
                                 {
                                     Brand = item.Brand.Name,
                                     BrandId = item.Brand.Id,
                                     Category = item.Category.Name,
                                     CategoryId = item.Category.Id,
                                     Code = item.Code,
                                     Id = item.Id,
                                     ItemType = item.ItemType.Name,
                                     ItemTypeId = item.ItemType.Id,
                                     InterchangeNumber = item.InterchangeNumber + "",
                                     ModelNumber = item.ModelNumber + "",
                                     Name = item.Name,
                                     NetPriceD = price == null ? 0 : price.NetSellingPrice,
                                     PartNumber = item.PartNumber + "",
                                     QuantityInventoryD = inventory.Quantity,
                                     //QuantityServedD = subsoli != null ? subsoli.Quantity : null,
                                     //QuantityInvoiceD = subinv != null ? subinv.Quantity : null,
                                     RetailPriceD = price == null ? 0 : price.RetailSellingPrice,
                                     Size = item.Size,
                                     Unit = item.Unit.Name,
                                     UnitId = item.Unit.Id,
                                     Warehouse = inventory.Warehouse,
                                     IsActive = item.IsActive,
                                     GiveAction = false
                                 }).ToListAsync();
            return itemsVM;
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
                data = data.Where(x => x.Code.ToLower().Contains(form.SearchValue)
                                    || x.Name.ToLower().Contains(form.SearchValue)
                                    || x.InterchangeNumber.ToLower().Contains(form.SearchValue)
                                    || x.Name.ToLower().Contains(form.SearchValue)
                                    || x.NetPrice.ToString().Contains(form.SearchValue)
                                    || x.PartNumber.ToLower().Contains(form.SearchValue)
                                    || x.QuantityTotalD.ToString().Contains(form.SearchValue)
                                    || x.RetailPrice.ToString().Contains(form.SearchValue)
                                    || x.Warehouse.ToLower().Contains(form.SearchValue));
            }


            data = data.ToList().OrderBy(s => s.Id)
                            .GroupBy(s => s.Id)
                            .SelectMany(s => s.Select((j, i) => new ItemCommonVM
                            {
                                Brand = j.Brand,
                                BrandId = j.BrandId,
                                Category = j.Category,
                                CategoryId = j.CategoryId,
                                Code = j.Code,
                                Id = j.Id,
                                ItemType = j.ItemType,
                                ItemTypeId = j.ItemTypeId,
                                InterchangeNumber = j.InterchangeNumber,
                                ModelNumber = j.ModelNumber,
                                Name = j.Name,
                                NetPriceD = j.NetPriceD,
                                PartNumber = j.PartNumber,
                                QuantityInventoryD = j.QuantityInventoryD,
                                QuantityServedD = j.QuantityServedD,
                                QuantityInvoiceD = j.QuantityInvoiceD,
                                RetailPriceD = j.RetailPriceD,
                                Size = j.Size,
                                Unit = j.Unit,
                                UnitId = j.UnitId,
                                Warehouse = j.Warehouse,
                                IsActive = j.IsActive,
                                GiveAction = i == 0
                            })).AsQueryable();
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
                        case "InterchangeNumber": data = data.OrderBy(s => s.InterchangeNumber); break;
                        case "Name": data = data.OrderBy(s => s.Name); break;
                        case "NetPrice": data = data.OrderBy(s => s.NetPriceD); break;
                        case "PartNumber": data = data.OrderBy(s => s.PartNumber); break;
                        case "Quantity": data = data.OrderBy(s => s.QuantityTotalD); break;
                        case "RetailPrice": data = data.OrderBy(s => s.RetailPriceD); break;
                        case "Warehouse": data = data.OrderBy(s => s.Warehouse); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Code": data = data.OrderByDescending(s => s.Code); break;
                        case "InterchangeNumber": data = data.OrderByDescending(s => s.InterchangeNumber); break;
                        case "Name": data = data.OrderByDescending(s => s.Name); break;
                        case "NetPrice": data = data.OrderByDescending(s => s.NetPriceD); break;
                        case "PartNumber": data = data.OrderByDescending(s => s.PartNumber); break;
                        case "Quantity": data = data.OrderByDescending(s => s.QuantityTotalD); break;
                        case "RetailPrice": data = data.OrderByDescending(s => s.RetailPriceD); break;
                        case "Warehouse": data = data.OrderByDescending(s => s.Warehouse); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
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
                                                 .Sum(s => s.Quantity)
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

        public async Task<bool> UploadCombinationExistAsync(ItemFileVM itemVM)
        {
            var itemMatch = from item in context.Items
                            join inventory in context.ItemInventories on item.Id equals inventory.ItemId
                            where item.Code == itemVM.Code
                            && item.Name == itemVM.Name
                            && item.PartNumber + "" == itemVM.PartNumber + ""
                            && item.InterchangeNumber + "" == itemVM.InterchangeNumber + ""
                            && inventory.Warehouse.Name == itemVM.Warehouse
                            && item.ModelNumber + "" == itemVM.ModelNumber + ""
                            && item.Category.Name == itemVM.Category
                            && item.ItemType.Name == itemVM.ItemType
                            && item.Brand.Name == itemVM.Brand
                            && item.Size == itemVM.Size
                            && item.Unit.Name == itemVM.Unit
                            && inventory.Quantity == itemVM.Quantity
                            select item;
            return await itemMatch.AnyAsync();
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

            var defaultWarehouse = await context.Warehouses
                                                .Where(s => s.Code.ToLower().Contains("main"))
                                                .Select(s => s.Id)
                                                .FirstOrDefaultAsync();
            var itemInventories = new ItemInventory
            {
                ItemId = item.Id,
                Quantity = itemVM.QuantityD,
                WarehouseId = defaultWarehouse
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
            item.ModelNumber = itemVM.ModelNumber + "";
            item.Name = itemVM.Name;
            item.PartNumber = itemVM.PartNumber + "";
            item.Size = itemVM.Size + "";
           // item.UnitId = itemVM.UnitId;

            context.Items.Update(item);
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task DeleteAsync(int id)
        {
            var itemInventories = context.ItemInventories.Where(s=>s.ItemId ==id).ToList();
            if (itemInventories.Count > 0)
            {
                context.ItemInventories.RemoveRange(itemInventories);
            }

            var itemPrices = context.ItemPrices.Where(s => s.ItemId == id).ToList();
            if(itemPrices.Count > 0)
            {
                context.ItemPrices.RemoveRange(itemPrices);
            }

            var physicalCount = context.PhysicalCountHistories.Where(s => s.ItemId == id).ToList();
            if(physicalCount.Count > 0)
            {
                context.PhysicalCountHistories.RemoveRange(physicalCount);
            }

            var stockCard = context.StockCards.Where(s => s.ItemId == id).ToList();
            if(stockCard.Count > 0)
            {
                context.StockCards.RemoveRange(stockCard);
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
                                                     .Include(s => s.Currency)
                                                     .Include(s => s.DeliveryAddress)
                                                     .Include(s => s.Status)
                                                     .Include(s => s.Supplier)

                              join purchaseItem in context.PurchaseOrderNewLineItems on purchase.Id equals purchaseItem.PurchaseOrderId
                              join purchaseRequest in context.PurchaseRequestLineItems on purchaseItem.RequestItemId equals purchaseRequest.Id
                              join user in userManager.Users on purchase.UserCreated equals user.Id

                              where purchaseRequest.ItemId == id
                              select new ItemDetailPurchaseVM
                              {
                                  ConversionFactorD = purchase.CurrencyRate,
                                  Currency = purchase.Currency.Code,
                                  PurchaseNumber = purchase.DocumentNumber,
                                  QuantityD = purchaseItem.Quantity,
                                  ReplenishBy = user.Nickname,
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
            //                      ReplenishBy = user.Nickname,
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
                          join user in userManager.Users on sitem.SalesOrder.UserCreated equals user.Id
                          where sitem.Item.Id == id
                          select new ItemDetailSalesVM
                          {
                              Agent = sitem.SalesOrder.Agent.Code,
                              Customer = sitem.SalesOrder.Customer.Name,
                              Discount1D = sitem.Discount1,
                              Discount2D = sitem.Discount2,
                              OrderBy = user.Nickname,
                              OrderDateD = sitem.SalesOrder.DateCreated,
                              PackedQuantityD = sitem.PackedQuantity,
                              QuantityD = sitem.Quantity,
                              SalesNumber = sitem.SalesOrder.SalesOrderNumber,
                              UnitPriceD = sitem.SellingPrice,
                              UnservedQuantityD = sitem.UnservedQuantity
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
                                AdjustmentBy = user.Nickname,
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
                               UpdateBy = user.Nickname,
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
                                 TransferBy = user.Nickname,
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
                                Customer = ritem.SalesReturn.Customer.Code,
                                ItemCode = ritem.Item.Code,
                                ItemName = ritem.Item.Name,
                                QuantityD = ritem.Quantity,
                                ReturnBy = user.Nickname,
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