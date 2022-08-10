using AutoMapper;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
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
            var priceVM = from item in context.Items
                          from price in context.ItemPrices.Where(s => s.ItemId == item.Id && s.DateCreated >= DateTime.Now).OrderByDescending(s => s.DateCreated).Take(1).DefaultIfEmpty()
                          join inventory in context.ItemInventories on item.Id equals inventory.Id
                          group inventory by new { item, price } into i
                          where i.Key.item.Id == id
                          select new ItemPriceVM
                          {
                              Brand = i.Key.item.Brand.Code,
                              Category = i.Key.item.Category.Code,
                              Code = i.Key.item.Code,
                              Id = i.Key.item.Id,
                              ItemType = i.Key.item.ItemType.Code,
                              ModelNumber = string.IsNullOrWhiteSpace(i.Key.item.ModelNumber) ? "-" : i.Key.item.ModelNumber,
                              Name = i.Key.item.Name,
                              NetPrice = i.Key.price.NetSellingPrice,
                              PartNumber = string.IsNullOrWhiteSpace(i.Key.item.PartNumber) ? "-" : i.Key.item.PartNumber,
                              Quantity = i.Sum(s => s.Quantity),
                              RetailPrice = i.Key.price.RetailSellingPrice,
                              Size = i.Key.item.Size,
                              Unit = i.Key.item.Unit.Code
                          };

            if (priceVM == null)
            {
                return null;
            }
            return await priceVM.FirstOrDefaultAsync();
        }

        public async Task<IList<ItemPriceVM>> GetAllItemPriceAsync(ItemFilterVM filter)
        {
            var pricesVM = (from item in context.Items
                            from price in context.ItemPrices.Where(s => s.ItemId == item.Id && s.DateCreated <= DateTime.Now).OrderByDescending(s => s.DateCreated).Take(1).DefaultIfEmpty()
                            let quantity = (from inventory in context.ItemInventories where inventory.ItemId == item.Id select inventory.Quantity).ToList()
                            join user in userManager.Users on price.UserCreated equals user.Id
                            where ((!filter.ActiveOnly || (filter.ActiveOnly && item.IsActive))
                                && (filter.CategoryId == null || filter.CategoryId == item.CategoryId)
                                && (filter.ItemTypeId == null || filter.ItemTypeId == item.ItemTypeId)
                                && (filter.BrandId == null || filter.BrandId == item.BrandId))
                            select new ItemPriceVM
                            {
                                Brand = item.Brand.Code,
                                Category = item.Category.Code,
                                Code = item.Code,
                                Id = item.Id,
                                ItemType = item.ItemType.Code,
                                ModelNumber = string.IsNullOrWhiteSpace(item.ModelNumber) ? "-" :item.ModelNumber,
                                Name = item.Name,
                                NetPrice = price == null ? 0 : price.NetSellingPrice,
                                PartNumber = string.IsNullOrWhiteSpace(item.PartNumber) ? "-" : item.PartNumber,
                                Quantity = quantity.Count() == 0 ? 0 : quantity.Sum(s => s),
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
                data = data.Where(x => x.Brand.ToLower().Contains(form.SearchValue)
                                    || x.Category.ToLower().Contains(form.SearchValue)
                                    || x.Code.ToLower().Contains(form.SearchValue)
                                    || x.ItemType.ToLower().Contains(form.SearchValue)
                                    || x.ModelNumber.ToLower().Contains(form.SearchValue)
                                    || x.Name.ToLower().Contains(form.SearchValue)
                                    || x.NetPrice.ToString().ToLower().Contains(form.SearchValue)
                                    || x.PartNumber.ToLower().Contains(form.SearchValue)
                                    || x.Quantity.ToString().ToLower().Contains(form.SearchValue)
                                    || x.RetailPrice.ToString().ToLower().Contains(form.SearchValue)
                                    || x.Size.ToLower().Contains(form.SearchValue)
                                    || x.Unit.ToLower().Contains(form.SearchValue));
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
                        case "Quantity": data = data.OrderBy(s => s.Quantity); break;
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
                        case "Quantity": data = data.OrderByDescending(s => s.Quantity); break;
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

        public async Task<IList<ItemPriceUpdateVM>> GetAllItemPriceUpdateAsync(ItemFilterVM filter)
        {
            var pricesVM = (from item in context.Items
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
                                NetPrice = price == null ? 0 : price.NetSellingPrice,
                                RetailPrice = price == null ? 0 : price.RetailSellingPrice,
                                UserCreated = user.Nickname,
                                DateCreated = price.DateCreated.ToString(SystemFormat.StringDateTime),
                                DateCreatedD = price.DateCreated
                            });
            return await pricesVM.ToListAsync();
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
                data = data.Where(x => x.Code.ToLower().Contains(form.SearchValue)
                                    || x.Name.ToLower().Contains(form.SearchValue)
                                    || x.NetPrice.ToString().ToLower().Contains(form.SearchValue)
                                    || x.RetailPrice.ToString().ToLower().Contains(form.SearchValue)
                                    || x.UserCreated.ToLower().Contains(form.SearchValue)
                                    || x.DateCreated.ToLower().Contains(form.SearchValue));
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
                        case "NetPrice": data = data.OrderBy(s => s.NetPrice); break;
                        case "RetailPrice": data = data.OrderBy(s => s.RetailPrice); break;
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
                        case "NetPrice": data = data.OrderByDescending(s => s.NetPrice); break;
                        case "RetailPrice": data = data.OrderByDescending(s => s.RetailPrice); break;
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
                ItemId = id.Value,
                RetailSellingPrice = retailPrice,
                NetSellingPrice = netPrice
            };
            context.ItemPrices.Add(itemPrice);
            await context.SaveChangesAsync(currentUserId);
            return true;
        }
    }
}