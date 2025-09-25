using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Application.Repositories
{
    public class ItemPriceRepository : IItemPriceRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;

        public ItemPriceRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
            , UserManager<User> userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        public async Task<ItemPriceVM?> GetAsync(int? id)
        {
            //var WarehouseInventory = await (from inv in context.ItemInventories
            //                                where inv.ItemId == id
            //                                select inv.Quantity).FirstAsync();
            var WarehouseInventory = await (from inv in context.ItemInventories
                                            where inv.ItemId == id
                                            select (int?)inv.Quantity).SumAsync() ?? 0;

            var OrderQuantity = await (from soli in context.SalesOrderLineItems
                                       where soli.ItemId == id
                                       select (decimal?)soli.Quantity).SumAsync() ?? 0;

            var PackedQuantity = await (from pli in context.PackLineItems
                                        where pli.ItemId == id
                                        select (decimal?)pli.Quantity).SumAsync() ?? 0;
            var InvoicedQuantity = await (from inv in context.InvoiceLineItems
                                          where inv.ItemId == id
                                          select (decimal?)inv.Quantity).SumAsync() ?? 0;

            var ReturnedQuantity = await (from rli in context.SalesReturnLineItems
                                          where rli.ItemId == id
                                          select (decimal?)rli.Quantity).SumAsync() ?? 0;

            var ServedQuantity = PackedQuantity - ReturnedQuantity;
            var UnservedQuantity = Math.Max(0, OrderQuantity - WarehouseInventory);

            var priceVM = from item in context.Items
                          from price in context.ItemPrices
                                               .Where(s => s.ItemId == item.Id
                                                        && s.DateCreated <= DateTime.Now)
                                               .OrderByDescending(s => s.DateCreated)
                                               .Take(1)
                                               .DefaultIfEmpty()
                          join user in userManager.Users on price.UserCreated equals user.Id
                          where item.Id == id
                          select new ItemPriceVM
                          {
                              Brand = item.Brand.Code,
                              Category = item.Category.Code,
                              Code = item.Code,
                              Id = item.Id,
                              ItemType = item.ItemType.Code,
                              ModelNumber = string.IsNullOrWhiteSpace(item.ModelNumber) ? "-" : item.ModelNumber,
                              Name = item.Name,
                              NetPrice = price.NetSellingPrice,
                              PartNumber = string.IsNullOrWhiteSpace(item.PartNumber) ? "-" : item.PartNumber,
                              QuantityInventoryD = WarehouseInventory,
                              QuantityServedD = (int?)ServedQuantity,
                              QuantityInvoiceD = (int?)InvoicedQuantity,
                              RetailPrice = price.RetailSellingPrice,
                              Size = item.Size,
                              Unit = item.Unit.Code
                          };
            if (priceVM == null)
            {
                return null;
            }
            return await priceVM.FirstOrDefaultAsync();
        }

        public async Task<IList<ItemPriceVM>> GetAllItemPriceAsync(ItemFilterVM filter)
        {
            var WarehouseInventory = from inv in context.ItemInventories
                                     group inv by inv.ItemId into invgrp
                                     select new
                                     {
                                         ItemId = invgrp.Key,
                                         Quantity = invgrp.Sum(s => s.Quantity)
                                     };

            var pricesVM = (from item in context.Items
                            from price in context.ItemPrices
                                                 .Where(s => s.ItemId == item.Id
                                                          && s.DateCreated <= DateTime.Now)
                                                 .OrderByDescending(s => s.DateCreated)
                                                 .Take(1)
                                                 .DefaultIfEmpty()
                            join user in userManager.Users on price.UserCreated equals user.Id
                            join inventory in WarehouseInventory on item.Id equals inventory.ItemId
                            let quantityPacked = context.PackLineItems
                                                        .Where(s => s.ItemId == item.Id)
                                                        .Sum(s => (int?)s.Quantity) ?? 0
                            let quantityInvoiced = context.InvoiceLineItems
                                                          .Where(s => s.ItemId == item.Id)
                                                          .Sum(s => (int?)s.Quantity) ?? 0
                            let quantityReturned = context.SalesReturnLineItems
                                                          .Where(s => s.ItemId == item.Id)
                                                          .Sum(s => (int?)s.Quantity) ?? 0
                            where (!filter.ActiveOnly || (filter.ActiveOnly && item.IsActive))
                                && (filter.BrandId == null || filter.BrandId == item.BrandId || (filter.BrandId == 0 && item.Brand.Code.ToLower().Contains((filter.BrandKeyword + "").ToLower())))
                                && (filter.ItemTypeId == null || filter.ItemTypeId == item.ItemTypeId || (filter.ItemTypeId == 0 && item.ItemType.Code.ToLower().Contains((filter.ItemTypeKeyword + "").ToLower())))
                                && (filter.ItemId == null || filter.ItemId == 0 || filter.ItemId == item.Id)
                                && (filter.ItemKeyword == null || item.Code.ToLower().Contains((filter.ItemKeyword + "").ToLower()))
                                && (filter.ItemNameKeyword == null || item.Name.ToLower().Contains((filter.ItemNameKeyword + "").ToLower()))
                                && (filter.ModelNumber == null || (item.ModelNumber + "").ToLower().Contains(filter.ModelNumber.ToLower()))
                                && (filter.Size == null || item.Size.ToLower().Contains(filter.Size.ToLower()))
                            select new ItemPriceVM
                            {
                                Brand = item.Brand.Code,
                                BrandName = item.Brand.Name,
                                InterchangeNumber = item.InterchangeNumber,
                                Category = item.Category.Code,
                                Code = item.Code,
                                Id = item.Id,
                                ItemType = item.ItemType.Code,
                                ModelNumber = string.IsNullOrWhiteSpace(item.ModelNumber) ? "-" : item.ModelNumber + "",
                                Name = item.Name,
                                NetPrice = price == null ? 0 : price.NetSellingPrice,
                                PartNumber = string.IsNullOrWhiteSpace(item.PartNumber) ? "-" : item.PartNumber + "",
                                QuantityInventoryD = inventory.Quantity,
                                QuantityServedD = quantityPacked - quantityReturned,
                                QuantityInvoiceD = quantityInvoiced,
                                RetailPrice = price == null ? 0 : price.RetailSellingPrice,
                                Size = item.Size,
                                Unit = item.Unit.Code
                            });
            return await pricesVM.ToListAsync();
        }

        public async Task<ForDataTables<ItemPriceVM>> GetAllItemPriceDataTablesAsync(DataTablesRequestForm form, ItemFilterVM filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetAllItemPriceAsync(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Brand.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.Category.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.Code.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.ItemType.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.ModelNumber.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.Name.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.NetPrice.ToString().Contains(form.SearchValue)
                                    || x.PartNumber.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.QuantityD.ToString().Contains(form.SearchValue)
                                    || x.RetailPrice.ToString().Contains(form.SearchValue)
                                    || x.Size.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.Unit.ToLower().Contains(form.SearchValue.ToLower()));
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
                        case "Brand": data = data.OrderBy(s => s.Brand); break;
                        case "Category": data = data.OrderBy(s => s.Category); break;
                        case "Code": data = data.OrderBy(s => s.Code); break;
                        case "ItemType": data = data.OrderBy(s => s.ItemType); break;
                        case "ModelNumber": data = data.OrderBy(s => s.ModelNumber); break;
                        case "Name": data = data.OrderBy(s => s.Name); break;
                        case "NetPrice": data = data.OrderBy(s => s.NetPrice); break;
                        case "PartNumber": data = data.OrderBy(s => s.PartNumber); break;
                        case "Quantity": data = data.OrderBy(s => s.QuantityD); break;
                        case "RetailPrice": data = data.OrderBy(s => s.RetailPrice); break;
                        case "Size": data = data.OrderBy(s => s.Size); break;
                        case "Unit": data = data.OrderBy(s => s.Unit); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Brand": data = data.OrderByDescending(s => s.Brand); break;
                        case "Category": data = data.OrderByDescending(s => s.Category); break;
                        case "Code": data = data.OrderByDescending(s => s.Code); break;
                        case "ItemType": data = data.OrderByDescending(s => s.ItemType); break;
                        case "ModelNumber": data = data.OrderByDescending(s => s.ModelNumber); break;
                        case "Name": data = data.OrderByDescending(s => s.Name); break;
                        case "NetPrice": data = data.OrderByDescending(s => s.NetPrice); break;
                        case "PartNumber": data = data.OrderByDescending(s => s.PartNumber); break;
                        case "Quantity": data = data.OrderByDescending(s => s.QuantityD); break;
                        case "RetailPrice": data = data.OrderByDescending(s => s.RetailPrice); break;
                        case "Size": data = data.OrderByDescending(s => s.Size); break;
                        case "Unit": data = data.OrderByDescending(s => s.Unit); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<ItemPriceVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public async Task<ItemPricePVM> GetAllItemPricePrintAsync(ItemFilterVM filter)
        {
            var items = await GetAllItemPriceAsync(filter);
            var brand = await context.Brands.Where(s => s.Id == filter.BrandId).FirstOrDefaultAsync();
            var category = await context.Categories.Where(s => s.Id == filter.CategoryId).FirstOrDefaultAsync();
            var itemType = await context.ItemTypes.Where(s => s.Id == filter.ItemTypeId).FirstOrDefaultAsync();

            return new ItemPricePVM
            {
                Contents = items,
                Brand = filter.BrandId == null || brand == null ? "All" : brand.Name,
                Category = filter.CategoryId == null || category == null ? "All" : category.Name,
                ItemType = filter.ItemTypeId == null || itemType == null ? "All" : itemType.Name
            };
        }

        public async Task<IList<ItemPriceUpdateVM>> GetAllItemPriceUpdateAsync(ItemFilterVM filter)
        {
            var pricesVM = await (from item in context.Items
                                  join price in context.ItemPrices on item.Id equals price.ItemId
                                  join user in userManager.Users on price.UserCreated equals user.Id
                                  where ((!filter.ActiveOnly || (filter.ActiveOnly && item.IsActive))
                                      && (filter.CategoryId == null || filter.CategoryId == item.CategoryId)
                                      && (filter.ItemTypeId == null || filter.ItemTypeId == item.ItemTypeId)
                                      && (filter.BrandId == null || filter.BrandId == item.BrandId))
                                  select new ItemPriceUpdateVM
                                  {
                                      Code = item.Code,
                                      Id = item.Id,
                                      Name = item.Name,
                                      NetPriceD = price == null ? 0m : price.NetSellingPrice,
                                      RetailPriceD = price == null ? 0m : price.RetailSellingPrice,
                                      UserCreated = SystemFormat.Name(user.FirstName, user.LastName, user.Nickname),
                                      DateCreatedD = price.DateCreated,
                                      IsLatest = false
                                  }).ToListAsync();
            var pricesVM2 = pricesVM.OrderBy(s => s.Code)
                                    .ThenByDescending(s => s.DateCreatedD)
                                    .GroupBy(s => s.Code)
                                    .SelectMany(s => s.Select((j, i) => new ItemPriceUpdateVM
                                    {
                                        Code = j.Code,
                                        Id = j.Id,
                                        Name = j.Name,
                                        NetPriceD = j.NetPriceD,
                                        RetailPriceD = j.RetailPriceD,
                                        UserCreated = j.UserCreated,
                                        DateCreatedD = j.DateCreatedD,
                                        IsLatest = i == 0,
                                        Temp = i
                                    })).ToList();
            return pricesVM2;
        }

        public async Task<ForDataTables<ItemPriceUpdateVM>> GetAllItemPriceUpdateDataTablesAsync(DataTablesRequestForm form, ItemFilterVM filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetAllItemPriceUpdateAsync(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Code.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.Name.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.NetPrice.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.RetailPrice.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.UserCreated.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.DateCreated.ToLower().Contains(form.SearchValue.ToLower()));
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
                        case "NetPrice": data = data.OrderBy(s => s.NetPriceD); break;
                        case "RetailPrice": data = data.OrderBy(s => s.RetailPriceD); break;
                        case "UserCreated": data = data.OrderBy(s => s.UserCreated); break;
                        case "DateCreated": data = data.OrderBy(s => s.DateCreatedD); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Code": data = data.OrderByDescending(s => s.Code); break;
                        case "Name": data = data.OrderByDescending(s => s.Name); break;
                        case "NetPrice": data = data.OrderByDescending(s => s.NetPriceD); break;
                        case "RetailPrice": data = data.OrderByDescending(s => s.RetailPriceD); break;
                        case "UserCreated": data = data.OrderByDescending(s => s.UserCreated); break;
                        case "DateCreated": data = data.OrderByDescending(s => s.DateCreatedD); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<ItemPriceUpdateVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public async Task<bool?> UpdateAsync(int? id, decimal retailPrice, decimal netPrice)
        {
            if (!context.Items.Any(s => s.Id == id))
            {
                return null;
            }
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var itemPrice = new ItemPrice
            {
                ItemId = id == null ? 0 : id.Value,
                RetailSellingPrice = retailPrice,
                NetSellingPrice = netPrice
            };
            context.ItemPrices.Add(itemPrice);
            await context.SaveChangesAsync(currentUserId);
            return true;
        }
    }
}