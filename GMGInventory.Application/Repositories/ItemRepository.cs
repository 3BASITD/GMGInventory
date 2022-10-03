using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
                        let inventory = context.ItemInventories.Where(s => s.ItemId == item.Id).Sum(s => s.Quantity)
                        where item.IsActive == true
                           && (string.IsNullOrWhiteSpace(search)
                           || item.Code.ToLower().Contains(search.ToLower()))
                        select new JsonItemOptionsModel
                        {
                            Id = item.Id.ToString(),
                            InventoryCount = inventory,
                            Text = item.Code,
                            Name = item.Name,
                            NetPriceD = price.NetSellingPrice,
                            Quantity = 0,
                            RetailPriceD = price.RetailSellingPrice,
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

        public async Task<int?> GetIdAsync(string? Code)
        {
            var item = await context.Items.Where(s => s.Code == Code).FirstOrDefaultAsync();
            return item?.Id;
        }

        public async Task<Select2Model<JsonItemOptionsModel>> GetInWarehouseOptions(string search, int warehouse, int page)
        {
            var items = await context.ItemInventories.Include(s => s.Item).ThenInclude(s => s.Unit)
                                .Where(s => s.WarehouseId == warehouse
                                         && (string.IsNullOrWhiteSpace(search)
                                         || (s.Item.Code.ToLower().Contains(search.ToLower()))
                                         || (s.Item.Name.ToLower().Contains(search.ToLower()))))
                                .Select(s => new JsonItemOptionsModel
                                {
                                    Id = s.Item.Id.ToString(),
                                    Text = s.Item.Code,
                                    Name = s.Item.Name,
                                    Quantity = s.Quantity,
                                    Unit = s.Item.Unit.Code
                                })
                                .ToListAsync();
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
                                .ToListAsync();
        }

        public async Task<int?> GetUnitIdAsync(string? Code)
        {
            var unit = await context.Units.Where(s => s.Code == Code).FirstOrDefaultAsync();
            return unit?.Id;
        }

        public async Task<IList<ItemListVM>> GetAllAsync(ItemFilterVM filter)
        {
            var itemsVM = from item in context.Items
                          from price in context.ItemPrices
                                               .Where(s => s.ItemId == item.Id
                                                        && s.DateCreated <= DateTime.Now)
                                               .OrderByDescending(s => s.DateCreated)
                                               .Take(1)
                                               .DefaultIfEmpty()
                          join inventory in context.ItemInventories
                                                   .Include(s => s.Warehouse) on item.Id equals inventory.ItemId
                          where ((!filter.ActiveOnly || (filter.ActiveOnly && item.IsActive))
                              && (filter.CategoryId == null || filter.CategoryId == item.CategoryId)
                              && (filter.ItemTypeId == null || filter.ItemTypeId == item.ItemTypeId)
                              && (filter.BrandId == null || filter.BrandId == item.BrandId)
                              && (filter.WarehouseId == null || filter.WarehouseId == inventory.WarehouseId))
                          select new ItemListVM
                          {
                              BrandId = item.BrandId,
                              CategoryId = item.CategoryId,
                              Code = item.Code,
                              Id = item.Id,
                              ItemTypeId = item.ItemTypeId,
                              InterchangeNumber = item.InterchangeNumber + "",
                              ModelNumber = item.ModelNumber + "",
                              Name = item.Name,
                              NetPriceD = price == null ? 0 : price.NetSellingPrice,
                              PartNumber = item.PartNumber + "",
                              Quantity = inventory.Quantity,
                              RetailPriceD = price == null ? 0 : price.RetailSellingPrice,
                              Size = item.Size,
                              UnitId = item.UnitId,
                              Warehouse = inventory.Warehouse.Name,
                              IsActive = item.IsActive
                          };
            return await itemsVM.ToListAsync();
        }

        public async Task<ForDataTables<ItemListVM>> GetAllForDataTablesAsync(DataTablesRequestForm form, ItemFilterVM filter)
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
                                    || x.Quantity.ToString().Contains(form.SearchValue)
                                    || x.RetailPrice.ToString().Contains(form.SearchValue)
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
                        case "Code": data = data.OrderBy(s => s.Code); break;
                        case "InterchangeNumber": data = data.OrderBy(s => s.InterchangeNumber); break;
                        case "Name": data = data.OrderBy(s => s.Name); break;
                        case "NetPrice": data = data.OrderBy(s => s.NetPriceD); break;
                        case "PartNumber": data = data.OrderBy(s => s.PartNumber); break;
                        case "Quantity": data = data.OrderBy(s => s.Quantity); break;
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
                        case "Quantity": data = data.OrderByDescending(s => s.Quantity); break;
                        case "RetailPrice": data = data.OrderByDescending(s => s.RetailPriceD); break;
                        case "Warehouse": data = data.OrderByDescending(s => s.Warehouse); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<ItemListVM>
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
                              Quantity = inventory.Quantity,
                              RetailPriceD = price == null ? 0 : price.RetailSellingPrice,
                              Size = item.Size,
                              UnitCode = item.Unit.Name,
                              Warehouse = inventory.Warehouse.Name
                          }).FirstOrDefault();
            return itemVM;
        }

        public async Task<IList<ItemFileVM>> GetAllFileAsync()
        {
            var itemsVM = context.Items
                                 .Include(s => s.Category)
                                 .Include(s => s.ItemType)
                                 .Include(s => s.Brand)
                                 .Include(s => s.Unit)
                                 .Select(s => new ItemFileVM
                                 {
                                     Brand = s.Brand.Name,
                                     Category = s.Category.Name,
                                     Code = s.Code,
                                     ItemType = s.ItemType.Name,
                                     InterchangeNumber = s.InterchangeNumber + "",
                                     ModelNumber = s.ModelNumber + "",
                                     Name = s.Name,
                                     PartNumber = s.PartNumber + "",
                                     Size = s.Size,
                                     Unit = s.Unit.Code,
                                     IsActive = s.IsActive
                                 });
            return await itemsVM.ToListAsync();
        }

        public async Task<bool> UploadCombinationExistAsync(ItemFileVM itemVM)
        {
            return (await GetAllFileAsync())//.Where(s=>s.IsActive == true)
                                            .Any(s => s.Code == itemVM.Code
                                                   && s.Name == itemVM.Name
                                                   && s.PartNumber + "" == itemVM.PartNumber + ""
                                                   && s.InterchangeNumber + "" == itemVM.InterchangeNumber + ""
                                                   && s.ModelNumber + "" == itemVM.ModelNumber + ""
                                                   && s.Category == itemVM.Category
                                                   && s.ItemType == itemVM.ItemType
                                                   && s.Brand == itemVM.Brand
                                                   && s.Size + "" == itemVM.Size + ""
                                                   && s.Unit == itemVM.Unit);
        }

        public async Task<bool> IsCodeExistAsync(string code)
        {
            return await context.Items.AnyAsync(s => s.Code == code);
        }

        public async Task<bool> IsInUseAsync(int id)
        {
            return await context.InvoiceLineItems.AnyAsync(s => s.ItemId == id)
                || await context.ItemInventories.AnyAsync(s => s.ItemId == id)
                || await context.ItemPrices.AnyAsync(s => s.ItemId == id)
                || await context.PackLineItems.AnyAsync(s => s.ItemId == id)
                || await context.PackReturnLineItems.AnyAsync(s => s.ItemId == id)
                || await context.PhysicalCountHistories.AnyAsync(s => s.ItemId == id)
                || await context.PurchaseOrderLineItems.AnyAsync(s => s.ItemId == id)
                || await context.SalesOrderLineItems.AnyAsync(s => s.ItemId == id)
                || await context.SalesReturnLineItems.AnyAsync(s => s.ItemId == id)
                || await context.StockCards.AnyAsync(s => s.ItemId == id)
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
                Quantity = 0,
                WarehouseId = defaultWarehouse
            };
            await context.ItemInventories.AddAsync(itemInventories);
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
            item.UnitId = itemVM.UnitId;

            context.Items.Update(item);
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task DeleteAsync(int id)
        {
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

        private async Task<IList<ItemDetailWarehouseVM>> GetItemWarehouseAsync(int id)
        {
            var warehousesVM = from warehouse in context.Warehouses
                               join inventory in context.ItemInventories on warehouse.Id equals inventory.WarehouseId
                               where inventory.ItemId == id
                               select new ItemDetailWarehouseVM
                               {
                                   Code = warehouse.Code,
                                   Id = inventory.Id,
                                   Name = warehouse.Name,
                                   Quantity = inventory.Quantity,
                               };
            return await warehousesVM.ToListAsync();
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
                        case "Code": data = data.OrderBy(s => s.Code); break;
                        case "Name": data = data.OrderBy(s => s.Name); break;
                        case "Quantity": data = data.OrderBy(s => s.Quantity); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Code": data = data.OrderByDescending(s => s.Code); break;
                        case "Name": data = data.OrderByDescending(s => s.Name); break;
                        case "Quantity": data = data.OrderByDescending(s => s.Quantity); break;
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
            var purchasesVM = from purchase in context.PurchaseOrders
                                                      .Include(s => s.Currency)
                                                      .Include(s => s.Destination)
                                                      .Include(s => s.Status)
                                                      .Include(s => s.Supplier)
                              join purchaseItem in context.PurchaseOrderLineItems on purchase.Id equals purchaseItem.PurchaseOrderId
                              join user in userManager.Users on purchase.UserCreated equals user.Id
                              where purchaseItem.Item.Id == id
                              select new ItemDetailPurchaseVM
                              {
                                  ConversionFactorD = purchase.CurrencyRate,
                                  Currency = purchase.Currency.Code,
                                  PurchaseNumber = purchase.PONumber,
                                  Quantity = purchaseItem.Quantity,
                                  ReplenishBy = user.Nickname,
                                  ReplenishDateD = purchase.DateCreated,
                                  Status = purchase.Status.Code,
                                  Supplier = purchase.Supplier.Code,
                                  UnitCostD = 0, // tinanggal na sa db
                                  UnitPriceD = purchaseItem.UnitPrice,
                                  Warehouse = purchase.Destination.Code
                              };
            return await purchasesVM.ToListAsync();
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
                                    || x.Quantity.ToString().Contains(form.SearchValue)
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
                        case "Quantity": data = data.OrderBy(s => s.Quantity); break;
                        case "ReplenishBy": data = data.OrderBy(s => s.ReplenishBy); break;
                        case "ReplenishDate": data = data.OrderBy(s => s.ReplenishDate); break;
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
                        case "Quantity": data = data.OrderByDescending(s => s.Quantity); break;
                        case "ReplenishBy": data = data.OrderByDescending(s => s.ReplenishBy); break;
                        case "ReplenishDate": data = data.OrderByDescending(s => s.ReplenishDate); break;
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
            var salesVM = from sales in context.SalesOrders
                                               .Include(s => s.Agent)
                                               .Include(s => s.Customer)
                          join salesItem in context.SalesOrderLineItems
                                                   .Include(s => s.Item) on sales.Id equals salesItem.SalesOrderId
                          join user in userManager.Users on sales.UserCreated equals user.Id
                          where salesItem.Item.Id == id
                          select new ItemDetailSalesVM
                          {
                              Agent = sales.Agent.Code,
                              Customer = sales.Agent.Code,
                              Discount1D = salesItem.Discount1,
                              Discount2D = salesItem.Discount2,
                              OrderBy = user.Nickname,
                              OrderDateD = sales.DateCreated,
                              PackedQuantity = salesItem.PackedQuantity,
                              Quantity = salesItem.Quantity,
                              SalesNumber = sales.SalesOrderNumber,
                              UnitPriceD = salesItem.SellingPrice
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
                                    || x.PackedQuantity.ToString().Contains(form.SearchValue)
                                    || x.Quantity.ToString().Contains(form.SearchValue)
                                    || x.UnitPriceD.ToString().Contains(form.SearchValue)
                                    || x.UnservedQuantity.ToString().Contains(form.SearchValue));
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
                        case "OrderDate": data = data.OrderBy(s => s.OrderDate); break;
                        case "PackedQuantity": data = data.OrderBy(s => s.PackedQuantity); break;
                        case "Quantity": data = data.OrderBy(s => s.Quantity); break;
                        case "UnitPrice": data = data.OrderBy(s => s.UnitPriceD); break;
                        case "UnservedQuantity": data = data.OrderBy(s => s.UnservedQuantity); break;
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
                        case "OrderDate": data = data.OrderByDescending(s => s.OrderDate); break;
                        case "PackedQuantity": data = data.OrderByDescending(s => s.PackedQuantity); break;
                        case "Quantity": data = data.OrderByDescending(s => s.Quantity); break;
                        case "UnitPrice": data = data.OrderByDescending(s => s.UnitPriceD); break;
                        case "UnservedQuantity": data = data.OrderByDescending(s => s.UnservedQuantity); break;
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
                                QuantityFrom = count.QuantityFrom,
                                QuantityTo = count.QuantityTo,
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
                                    || x.QuantityAdjusted.ToString().Contains(form.SearchValue)
                                    || x.QuantityFrom.ToString().Contains(form.SearchValue)
                                    || x.QuantityTo.ToString().Contains(form.SearchValue)
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
                        case "AdjustmentDate": data = data.OrderBy(s => s.AdjustmentDate); break;
                        case "QuantityAdjusted": data = data.OrderBy(s => s.QuantityAdjusted); break;
                        case "QuantityFrom": data = data.OrderBy(s => s.QuantityFrom); break;
                        case "QuantityTo": data = data.OrderBy(s => s.QuantityTo); break;
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
                        case "AdjustmentDate": data = data.OrderByDescending(s => s.AdjustmentDate); break;
                        case "QuantityAdjusted": data = data.OrderByDescending(s => s.QuantityAdjusted); break;
                        case "QuantityFrom": data = data.OrderByDescending(s => s.QuantityFrom); break;
                        case "QuantityTo": data = data.OrderByDescending(s => s.QuantityTo); break;
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
            var transfersVM = from transfer in context.TransferItems
                                                      .Include(s => s.Destination)
                                                      .Include(s => s.Source)
                              join transferItem in context.TransferItemDetails on transfer.Id equals transferItem.TransferItemId
                              join user in userManager.Users on transfer.UserCreated equals user.Id
                              where transferItem.ItemId == id
                              select new ItemDetailTransferVM
                              {
                                  Destination = transfer.Destination.Code,
                                  Quantity = transferItem.Quantity,
                                  Remarks = transfer.Remarks,
                                  Source = transfer.Source.Code,
                                  TransferBy = user.Nickname,
                                  TransferDateD = transfer.DateCreated,
                                  TransferNumber = transfer.Id
                              };
            return await transfersVM.ToListAsync();
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
                                    || x.Quantity.ToString().Contains(form.SearchValue)
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
                        case "Quantity": data = data.OrderBy(s => s.Quantity); break;
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
                        case "Quantity": data = data.OrderByDescending(s => s.Quantity); break;
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
            var returnsVM = from rturn in context.SalesReturns
                                                 .Include(s => s.Customer)
                            join rturnItem in context.SalesReturnLineItems
                                                     .Include(s => s.Item) on rturn.Id equals rturnItem.SalesReturnId
                            join user in userManager.Users on rturn.UserCreated equals user.Id
                            where rturnItem.Item.Id == id
                            select new ItemDetailReturnVM
                            {
                                Customer = rturn.Customer.Code,
                                ItemCode = rturnItem.Item.Code,
                                ItemName = rturnItem.Item.Name,
                                Quantity = rturnItem.Quantity,
                                ReturnBy = user.Nickname,
                                ReturnDateD = rturn.DateCreated,
                                ReturnNumber = rturn.Number
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
                                    || x.Quantity.ToString().Contains(form.SearchValue)
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
                        case "Quantity": data = data.OrderBy(s => s.Quantity); break;
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
                        case "Quantity": data = data.OrderByDescending(s => s.Quantity); break;
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