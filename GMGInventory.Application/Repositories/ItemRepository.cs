using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Xml.Linq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Application.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly IHostingEnvironment hostingEnvironment;

        public ItemRepository(ApplicationDbContext context
                            , IHttpContextAccessor httpContextAccessor
                            , UserManager<User> userManager
                            , IHostingEnvironment hostingEnvironment)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.hostingEnvironment = hostingEnvironment;
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
                                               .Where(s => s.ItemId == item.Id && s.Warehouse.IsMain == true)
                                               .Sum(s => (int?)s.Quantity) ?? 0
                        where item.IsActive == true
                           && (string.IsNullOrWhiteSpace(search)
                           || item.Code.ToLower().Contains(search.ToLower())
                           || item.Name.ToLower().Contains(search.ToLower())
                           || (item.OldCode + "").ToLower().Contains(search.ToLower())
                           || (item.PartNumber + "").ToLower().Contains(search.ToLower()))
                        orderby item.Code ascending
                        select new JsonItemOptionsModel
                        {
                            Id = item.Id.ToString(),
                            InventoryCount = inventory,
                            Text = item.Code,
                            Name = item.Name,
                            OldCode = item.OldCode,
                            PartNumber = item.PartNumber + "",
                            NetPriceD = price == null ? -999999 : price.NetSellingPrice,
                            QuantityD = 0,
                            RetailPriceD = price == null ? -999999 : price.RetailSellingPrice,
                            UnitId = item.Unit == null ? -1 : item.Unit.Id,
                            Unit = item.Unit == null ? "" : item.Unit.Code,
                            BrandId = item.BrandId
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
                                     .Where(s => s.PurchaseOrderId == id
                                              && (
                                                string.IsNullOrWhiteSpace(search)
                                                || s.Item.Code.ToLower().Contains(search.ToLower())
                                                || s.Item.Name.ToLower().Contains(search.ToLower())
                                                || (s.Item.OldCode + "").ToLower().Contains(search.ToLower())
                                                || (s.Item.PartNumber + "").ToLower().Contains(search.ToLower())
                                              )
                                           )
                                    .Select(s => new JsonItemOptionsModel
                                    {
                                        Id = s.Id.ToString(),
                                        Text = s.Item.Code,
                                        Name = s.Item.Name,
                                        OldCode = s.Item.OldCode,
                                        QuantityD = 0,
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
                                        .Where(s => s.ItemId == item.Id
                                                 && s.Warehouse.IsMain == true)
                                        .Sum(s => s.Quantity)
                        //let requests = context.PurchaseRequestLineItems
                        //                      .Where(s => s.SalesOrderId == soitem.SalesOrderId
                        //                               && s.ItemId == soitem.ItemId
                        //                               && s.PurchaseRequest.Status.Name.ToLower() == "approved")
                        //                      .Sum(s => (int?)s.Quantity) ?? 0
                        where item.IsActive == true
                           && soitem.SalesOrderId == SalesOrderId
                           && (string.IsNullOrWhiteSpace(search)
                           || item.Code.ToLower().Contains(search.ToLower())
                           || item.Name.ToLower().Contains(search.ToLower())
                           || (item.OldCode + "").ToLower().Contains(search.ToLower())
                           || (item.PartNumber + "").ToLower().Contains(search.ToLower()))
                        //&& soitem.Quantity > (ii + requests)
                        orderby item.Code ascending
                        select new JsonItemOptionsModel
                        {
                            Id = item.Id.ToString(),
                            InventoryCount = ii,
                            Text = item.Code,
                            Name = item.Name,
                            OldCode = item.OldCode,
                            PartNumber = item.PartNumber + "",
                            NetPriceD = price.NetSellingPrice,
                            //QuantityD = Math.Max(0, soitem.Quantity - (ii + requests)), // removed, corrected as so quantity - stock quantity
                            QuantityD = Math.Max(0, soitem.Quantity - ii),
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
            var item = await context.Items
                                    .Where(s => s.Code.ToLower().Replace("\n", "").Replace("\r", "").Trim() == (Code + "").ToLower().Replace("\n", "").Replace("\r", "").Trim())
                                    .FirstOrDefaultAsync();
            return item?.Id;
        }

        public async Task<Select2Model<JsonItemOptionsModel>> GetInWarehouseOptions(string search, int warehouse, int page)
        {
            var items = await (from item in context.ItemInventories
                               where item.WarehouseId == warehouse
                               && item.Item.IsActive == true
                               && (search == null
                               || item.Item.Code.ToLower().Contains(search.ToLower())
                               || item.Item.Name.ToLower().Contains(search.ToLower())
                               || (item.Item.OldCode + "").ToLower().Contains(search.ToLower())
                               || (item.Item.PartNumber + "").ToLower().Contains(search.ToLower()))
                               && item.Quantity > 0
                               orderby item.Item.Code ascending
                               select new JsonItemOptionsModel
                               {
                                   Id = item.ItemId.ToString(),
                                   Text = item.Item.Code,
                                   Name = item.Item.Name,
                                   OldCode = item.Item.OldCode,
                                   QuantityD = item.Quantity,
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
            var unit = await context.Units
                                    .Where(s => s.Code.ToLower().Replace("\n", "").Replace("\r", "").Trim() == (Code + "").ToLower().Replace("\n", "").Replace("\r", "").Trim())
                                    .FirstOrDefaultAsync();
            return unit?.Id;
        }

        public async Task<int?> GetUnitIdByNameAsync(string? Name)
        {
            var unit = await context.Units
                                    .Where(s => s.Name.ToLower().Replace("\n", "").Replace("\r", "").Trim() == (Name + "").ToLower().Replace("\n", "").Replace("\r", "").Trim())
                                    .FirstOrDefaultAsync();
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
            if (filter.IsTable != false)
            {
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
                                  && (filter.WarehouseId == null || filter.WarehouseId == inventory.WarehouseId)
                              select new ItemCommonVM
                              {
                                  Id = item.Id,
                                  Code = item.Code.Replace("\n", "").Replace("\r", "").Trim(),
                                  Name = item.Name.Replace("\n", "").Replace("\r", "").Trim(),
                                  OldCode = item.OldCode == null ? null : item.OldCode.Replace("\n", "").Replace("\r", "").Trim(),
                                  PartNumber = (item.PartNumber + "").Replace("\n", "").Replace("\r", "").Trim(),
                                  InterchangeNumber = (item.InterchangeNumber + "").Replace("\n", "").Replace("\r", "").Trim(),
                                  ModelNumber = (item.ModelNumber + "").Replace("\n", "").Replace("\r", "").Trim(),
                                  BeginningCostD = item.BeginningCost,
                                  Category = item.Category.Name.Replace("\n", "").Replace("\r", "").Trim(),
                                  CategoryId = item.CategoryId,
                                  ItemType = item.ItemType.Name.Replace("\n", "").Replace("\r", "").Trim(),
                                  ItemTypeId = item.ItemTypeId,
                                  Brand = item.Brand.Name.Replace("\n", "").Replace("\r", "").Trim(),
                                  BrandId = item.BrandId,
                                  Size = item.Size.Replace("\n", "").Replace("\r", "").Trim(),
                                  Unit = item.Unit.Name.Replace("\n", "").Replace("\r", "").Trim(),
                                  UnitId = item.UnitId,
                                  QuantityD = inventory.Quantity,
                                  RetailPriceD = price == null ? 0 : price.RetailSellingPrice,
                                  NetPriceD = price == null ? 0 : price.NetSellingPrice,
                                  Warehouse = inventory.Warehouse.Name,
                                  WarehouseId = inventory.WarehouseId,
                                  IsActive = item.IsActive,
                                  Image = item.Image
                              };
                return await itemsVM.ToListAsync();
            } else
            {
                var itemsVM = from item in context.Items
                              where (!filter.ActiveOnly || (filter.ActiveOnly && item.IsActive))
                                  && (filter.CategoryId == null || filter.CategoryId == item.CategoryId)
                                  && (filter.ItemTypeId == null || filter.ItemTypeId == item.ItemTypeId)
                                  && (filter.BrandId == null || filter.BrandId == item.BrandId)
                              select new ItemCommonVM
                              {
                                  Id = item.Id,
                                  Code = item.Code.Replace("\n", "").Replace("\r", "").Trim(),
                                  Name = item.Name.Replace("\n", "").Replace("\r", "").Trim(),
                                  OldCode = item.OldCode == null ? null : item.OldCode.Replace("\n", "").Replace("\r", "").Trim(),
                                  PartNumber = (item.PartNumber + "").Replace("\n", "").Replace("\r", "").Trim(),
                                  InterchangeNumber = (item.InterchangeNumber + "").Replace("\n", "").Replace("\r", "").Trim(),
                                  ModelNumber = (item.ModelNumber + "").Replace("\n", "").Replace("\r", "").Trim(),
                                  BeginningCostD = item.BeginningCost,
                                  Category = item.Category.Name.Replace("\n", "").Replace("\r", "").Trim(),
                                  CategoryId = item.CategoryId,
                                  ItemType = item.ItemType.Name.Replace("\n", "").Replace("\r", "").Trim(),
                                  ItemTypeId = item.ItemTypeId,
                                  Brand = item.Brand.Name.Replace("\n", "").Replace("\r", "").Trim(),
                                  BrandId = item.BrandId,
                                  Size = item.Size.Replace("\n", "").Replace("\r", "").Trim(),
                                  Unit = item.Unit.Name.Replace("\n", "").Replace("\r", "").Trim(),
                                  UnitId = item.UnitId,
                                  IsActive = item.IsActive,
                                  Image = item.Image
                              };
                return await itemsVM.ToListAsync();
            }
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
                    s.OldCode,
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
                }.Any(a => a != null && a.ToLower().Contains(form.SearchValue.ToLower())));
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
                                   OldCode = j.OldCode,
                                   PartNumber = j.PartNumber,
                                   InterchangeNumber = j.InterchangeNumber,
                                   ModelNumber = j.ModelNumber,
                                   BeginningCostD = j.BeginningCostD,
                                   Category = j.Category,
                                   CategoryId = j.CategoryId,
                                   ItemType = j.ItemType,
                                   ItemTypeId = j.ItemTypeId,
                                   Brand = j.Brand,
                                   BrandId = j.BrandId,
                                   Size = j.Size,
                                   Unit = j.Unit,
                                   UnitId = j.UnitId,
                                   QuantityD = j.QuantityD,
                                   RetailPriceD = j.RetailPriceD,
                                   NetPriceD = j.NetPriceD,
                                   Warehouse = j.Warehouse,
                                   WarehouseId = j.WarehouseId,
                                   IsActive = j.IsActive,
                                   GiveAction = i == 0,
                                   Image = j.Image
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

                          let locationCodes = context.ItemLocations
                                    .Where(s => s.ItemId == item.Id)
                                    .GroupBy(s => s.LocationCode.Code)
                                    .Select(x => new
                                    {
                                        Id = x.Key
                                    }).ToList()

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
                              BeginningCostD = item.BeginningCost,
                              ModelNumber = item.ModelNumber + "",
                              Name = item.Name,
                              NetPriceD = price == null ? 0 : price.NetSellingPrice,
                              PartNumber = item.PartNumber + "",
                              QuantityD = inventory.Quantity,
                              RetailPriceD = price == null ? 0 : price.RetailSellingPrice,
                              Size = item.Size,
                              UnitCode = item.Unit.Name,
                              Warehouse = inventory.Warehouse.Name,
                              LocationCodes = string.Join(",", locationCodes.Select(s => s.Id)),
                              OldCode = item.OldCode,
                              Image = item.Image
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
                                                 .Where(s => s.ItemId == item.Id && s.Warehouse.IsMain == true)
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
                              IsActive = item.IsActive,
                              OldCode = item.OldCode
                          };
            return await itemsVM.ToListAsync();
        }

        public async Task<bool> IsCodeExistAsync(string code)
        {
            return await context.Items.AnyAsync(s => s.Code.ToLower().Replace("\n", "").Replace("\r", "").Trim() == code.ToLower().Replace("\n", "").Replace("\r", "").Trim());
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
                BeginningCost = itemVM.BeginningCostD == 0 ? null : itemVM.BeginningCostD,
                ModelNumber = itemVM.ModelNumber + "",
                OldCode = itemVM.OldCode,
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
                WarehouseId = itemVM.WarehouseId == 0
                            ? context.Warehouses.Where(s => s.IsMain == true).Select(s => s.Id).FirstOrDefault()
                            : itemVM.WarehouseId
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
            item.BeginningCost = itemVM.BeginningCostD == 0 ? null : itemVM.BeginningCostD;
            item.ModelNumber = itemVM.ModelNumber + "";
            item.Name = itemVM.Name;
            item.OldCode = itemVM.OldCode;
            item.PartNumber = itemVM.PartNumber + "";
            item.Size = itemVM.Size + "";

            //context.Items.Update(item);
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
            //context.ItemInventories.Update(inventory);
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

                //context.ItemPrices.Update(price);
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

        public async Task<ResponseModel> ItemPromoAddAsync(ItemPromoVM itemPromoVM)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

            ResponseModel response = new();

            var itemPromo = await context.ItemPromo.Where(s => s.Id == itemPromoVM.Id).FirstOrDefaultAsync();
            if (itemPromo == null)
            {
                itemPromo = new ItemPromo
                {
                    ItemId = itemPromoVM.ItemId,
                    Name = itemPromoVM.Name,
                    PromoPrice = itemPromoVM.PromoPriceD,
                    EffectiveDate = itemPromoVM.EffectiveDateD,
                    ExpirationDate = itemPromoVM.ExpirationDateD,
                    IsActive = true


                };

                await context.ItemPromo.AddAsync(itemPromo);
                await context.SaveChangesAsync(currentUserId);

                response.Status = "Success";
                response.Message = "Item Promo successfully added";
            }
            else
            {
                itemPromo.Name = itemPromoVM.Name;
                itemPromo.PromoPrice = itemPromoVM.PromoPriceD;
                itemPromo.EffectiveDate = itemPromoVM.EffectiveDateD;
                itemPromo.ExpirationDate = itemPromoVM.ExpirationDateD;



                context.ItemPromo.Update(itemPromo);
                await context.SaveChangesAsync(currentUserId);

                response.Status = "Success";
                response.Message = "Item Promo successfully updated";
            }
            return response;
        }

        public async Task<ResponseModel> ReactivateItemPromoAsync(int Id)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            ResponseModel response = new();
            var itemPromo = await context.ItemPromo.Where(s => s.Id == Id).FirstOrDefaultAsync();
            if (itemPromo == null)
            {
                response.Status = "Error";
                response.Message = "Item Promo is invalid";
            }
            else
            {
                itemPromo.IsActive = true;
                context.ItemPromo.Update(itemPromo);
                await context.SaveChangesAsync(currentUserId);

                response.Status = "Success";
                response.Message = "Item Promo successfully reactivated";
            }

            return response;
        }

        public async Task<ResponseModel> DeleteOrDeactivateItemPromoAsync(int Id)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            ResponseModel response = new();
            var itemPromo = await context.ItemPromo.Where(s => s.Id == Id).FirstOrDefaultAsync();
            if (itemPromo == null)
            {
                response.Status = "Error";
                response.Message = "Item Promo is invalid";
            }

            else
            {
                if (itemPromo.EffectiveDate > DateTime.Now)
                {

                    context.ItemPromo.Remove(itemPromo);
                    await context.SaveChangesAsync(currentUserId);

                    response.Status = "Success";
                    response.Message = "Item Promo successfully deleted";
                }

                else 
                {
                    itemPromo.IsActive = false;
                    context.ItemPromo.Update(itemPromo);
                    await context.SaveChangesAsync(currentUserId);

                    response.Status = "Success";
                    response.Message = "Item Promo successfully deactivated";

                }


            }

            return response;
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
            var purchasesVM = from poli in context.PurchaseOrderNewLineItems
                              join prli in context.PurchaseRequestLineItems on poli.RequestItemId equals prli.Id
                              join user in userManager.Users on poli.PurchaseOrder.UserCreated equals user.Id
                              where prli.ItemId == id
                              select new ItemDetailPurchaseVM
                              {
                                  ConversionFactorD = poli.PurchaseOrder.CurrencyRate,
                                  Currency = poli.PurchaseOrder.Currency.Name,
                                  PurchaseNumber = poli.PurchaseOrder.DocumentNumber,
                                  QuantityD = poli.Quantity,
                                  ReplenishBy = user.FirstName + " " + user.LastName,
                                  ReplenishDateD = poli.PurchaseOrder.DateCreated,
                                  Status = poli.PurchaseOrder.Status.Code,
                                  Supplier = poli.PurchaseOrder.Supplier.Code,
                                  UnitCostD = poli.UnitCost * (100 - poli.PurchaseOrder.Discount1) / 100 * (100 - poli.PurchaseOrder.Discount2) / 100,
                                  Warehouse = poli.PurchaseOrder.DeliveryAddress.Code
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
                                    || x.ConvertedTotalCostD.ToString().Contains(form.SearchValue)
                                    || x.Currency.ToString().Contains(form.SearchValue)
                                    || x.PurchaseNumber.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.QuantityD.ToString().Contains(form.SearchValue)
                                    || x.ReplenishBy.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.ReplenishDate.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.Status.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.Supplier.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.TotalCostD.ToString().Contains(form.SearchValue.ToLower())
                                    || x.UnitCostD.ToString().Contains(form.SearchValue)
                                    || x.Warehouse.ToLower().Contains(form.SearchValue.ToLower()));
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
                        case "ConvertedTotalCost": data = data.OrderBy(s => s.ConvertedTotalCost); break;
                        case "Currency": data = data.OrderBy(s => s.Currency); break;
                        case "PurchaseNumber": data = data.OrderBy(s => s.PurchaseNumber); break;
                        case "Quantity": data = data.OrderBy(s => s.QuantityD); break;
                        case "ReplenishBy": data = data.OrderBy(s => s.ReplenishBy); break;
                        case "ReplenishDate": data = data.OrderBy(s => s.ReplenishDateD); break;
                        case "Status": data = data.OrderBy(s => s.Status); break;
                        case "Supplier": data = data.OrderBy(s => s.Supplier); break;
                        case "TotalCost": data = data.OrderBy(s => s.TotalCostD); break;
                        case "UnitCost": data = data.OrderBy(s => s.UnitCostD); break;
                        case "Warehouse": data = data.OrderBy(s => s.Warehouse); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "ConversionFactor": data = data.OrderByDescending(s => s.ConversionFactorD); break;
                        case "ConvertedTotalCost": data = data.OrderByDescending(s => s.ConvertedTotalCostD); break;
                        case "Currency": data = data.OrderByDescending(s => s.Currency); break;
                        case "PurchaseNumber": data = data.OrderByDescending(s => s.PurchaseNumber); break;
                        case "Quantity": data = data.OrderByDescending(s => s.QuantityD); break;
                        case "ReplenishBy": data = data.OrderByDescending(s => s.ReplenishBy); break;
                        case "ReplenishDate": data = data.OrderByDescending(s => s.ReplenishDateD); break;
                        case "Status": data = data.OrderByDescending(s => s.Status); break;
                        case "Supplier": data = data.OrderByDescending(s => s.Supplier); break;
                        case "TotalCost": data = data.OrderByDescending(s => s.TotalCostD); break;
                        case "UnitCost": data = data.OrderByDescending(s => s.UnitCostD); break;
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
                              //UnservedQuantityD = sitem.Quantity - inventory > 0 ? sitem.Quantity - inventory : 0
                              UnservedQuantityD = Math.Max(0, sitem.Quantity - inventory)
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
                data = data.Where(x => x.Agent.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.Customer.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.Discount1D.ToString().Contains(form.SearchValue)
                                    || x.Discount2D.ToString().Contains(form.SearchValue)
                                    || x.OrderBy.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.OrderDate.ToLower().Contains(form.SearchValue.ToLower())
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

        public async Task<IList<ItemDetailCountVM>> GetItemCountAsync(int id)
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
                data = data.Where(x => x.AdjustmentBy.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.AdjustmentDate.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.QuantityAdjustedD.ToString().Contains(form.SearchValue)
                                    || x.QuantityFromD.ToString().Contains(form.SearchValue)
                                    || x.QuantityToD.ToString().Contains(form.SearchValue)
                                    || x.Remarks.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.Warehouse.ToLower().Contains(form.SearchValue.ToLower()));
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
                                    || x.UpdateBy.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.UpdateDate.ToLower().Contains(form.SearchValue.ToLower()));
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

        private async Task<IList<ItemPromoVM>> GetItemPromoAsync(int id)
        {
            var promoVM = from promo in context.ItemPromo
                           join user in userManager.Users on promo.UserCreated equals user.Id
                           where promo.ItemId == id
                           select new ItemPromoVM
                           {
                               Id = promo.Id,
                               PromoPriceD = promo.PromoPrice,
                               ItemId = promo.ItemId,
                               Name = promo.Name,
                               EffectiveDateD = promo.EffectiveDate,
                               ExpirationDateD = promo.ExpirationDate,
                               UpdateBy = SystemFormat.Name(user.FirstName, user.LastName, user.Nickname),
                               UpdateDateD = promo.DateCreated,
                               IsActive = promo.IsActive
                           };
            return await promoVM.ToListAsync();
        }


        public async Task<ForDataTables<ItemPromoVM>> GetItemPromoDTAsync(DataTablesRequestForm form, int id)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetItemPromoAsync(id)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.PromoPriceD.ToString().Contains(form.SearchValue)
                                    || x.Name.ToString().Contains(form.SearchValue)

                                    || x.EffectiveDate.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.ExpirationDate.ToLower().Contains(form.SearchValue.ToLower()));
                                    
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
                        case "Price": data = data.OrderBy(s => s.PromoPriceD); break;
                        case "Name": data = data.OrderBy(s => s.Name); break;
                        case "EffectiveDate": data = data.OrderBy(s => s.EffectiveDateD); break;
                        case "ExpirationDate": data = data.OrderBy(s => s.ExpirationDateD); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Price": data = data.OrderByDescending(s => s.PromoPriceD); break;
                        case "Name": data = data.OrderByDescending(s => s.Name); break;
                        case "EffectiveDate": data = data.OrderByDescending(s => s.EffectiveDateD); break;
                        case "ExpirationDate": data = data.OrderByDescending(s => s.ExpirationDateD); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<ItemPromoVM>
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
                data = data.Where(x => x.Destination.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.QuantityD.ToString().Contains(form.SearchValue)
                                    || x.Remarks.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.Source.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.TransferBy.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.TransferDate.ToLower().Contains(form.SearchValue.ToLower())
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
                data = data.Where(x => x.Customer.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.ItemCode.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.ItemName.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.QuantityD.ToString().Contains(form.SearchValue)
                                    || x.ReturnBy.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.ReturnDate.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.ReturnNumber.ToLower().Contains(form.SearchValue.ToLower()));
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

        public async Task<ResponseModel> SaveImageAsync(ItemImageFormVM form)
        {
            try
            {
                if (form.File == null || form.File.Length == 0)
                    return new ResponseModel { Status = "Error", Message = "Invalid file" };

                var item = await context.Items.Where(s => s.Id == form.Id).FirstOrDefaultAsync();
                if (item == null)
                    return new ResponseModel { Status = "Error", Message = "Item not found" };

                var uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images\\Items");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(form.File.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                //using (var fileStream = new FileStream(filePath, FileMode.Create))
                //{
                //    await form.File.CopyToAsync(fileStream);
                //}

                var resized = "";

                using (var image = await Image.LoadAsync(form.File.OpenReadStream()))
                {
                    if (image.Width > 500 || image.Height > 500)
                    {
                        var originalSize = $"{image.Width}x{image.Height}";
                        double ratioX = (double)500 / image.Width;
                        double ratioY = (double)500 / image.Height;
                        double ratio = Math.Min(ratioX, ratioY);

                        int newWidth = (int)(image.Width * ratio);
                        int newHeight = (int)(image.Height * ratio);

                        image.Mutate(x => x.Resize(newWidth, newHeight));

                        resized = $" Image is too large, or larger than 500x500. Image is resized from {originalSize} to {newWidth}x{newHeight}";
                    }

                    await image.SaveAsync(filePath);
                }

                var savedFile = Path.Combine("images\\Items", uniqueFileName);

                string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

                item.Image = uniqueFileName;
                context.Items.Update(item);
                await context.SaveChangesAsync(currentUserId);

                return new ResponseModel { Status = "Success", Message = "Successfully updated item's image." + resized };
            }
            catch (Exception ex)
            {
                return new ResponseModel { Status = "Error", Message = SystemFormat.GetInnermostExceptionMessage(ex) };
            }
        }
    }
}