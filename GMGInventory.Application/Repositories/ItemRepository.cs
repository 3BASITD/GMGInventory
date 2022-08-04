using GMGInventory.Application.Configurations;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
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

        public async Task<List<JsonOptionsModel>> GetOptions(ItemFilterVM filter, int? IncludeId)
        {
            return await context.Items
                                .Where(s => (!filter.ActiveOnly || (filter.ActiveOnly && s.IsActive))
                                    & (filter.CategoryId == null || s.CategoryId == filter.CategoryId)
                                    & (filter.ItemTypeId == null || s.ItemTypeId == filter.ItemTypeId)
                                    & (filter.BrandId == null || s.BrandId == filter.BrandId))
                                .Select(s => new JsonOptionsModel
                                {
                                    Id = s.Id.ToString(),
                                    Text = s.Name,
                                    Selected = IncludeId != null && IncludeId == s.Id
                                })
                                .ToListAsync();
        }

        public IQueryable<ItemViewVM> GetAllItemVM(ItemFilterVM filter)
        {
            var itemsVM = (from item in context.Items
                           from price in context.ItemPrices.Where(s => s.ItemId == item.Id && s.DateCreated <= DateTime.Now).OrderByDescending(s => s.DateCreated).Take(1).DefaultIfEmpty()
                           join inventory in context.ItemInventories on item.Id equals inventory.ItemId
                           where ((!filter.ActiveOnly || (filter.ActiveOnly && item.IsActive))
                               && (filter.CategoryId == null || filter.CategoryId == item.CategoryId)
                               && (filter.ItemTypeId == null || filter.ItemTypeId == item.ItemTypeId)
                               && (filter.BrandId == null || filter.BrandId == item.BrandId)
                               && (filter.WarehouseId == null || filter.WarehouseId == inventory.WarehouseId))
                           select new ItemViewVM
                           {
                               Brand = item.Brand.Code,
                               Category = item.Category.Code,
                               Code = item.Code,
                               Id = item.Id,
                               InterchangeNumber = item.InterchangeNumber,
                               ItemType = item.ItemType.Code,
                               IsActive = item.IsActive,
                               ModelNumber = item.ModelNumber,
                               Name = item.Name,
                               NetPrice = price == null ? "0.00" : price.NetSellingPrice.ToString("N0"),
                               PartNumber = item.PartNumber,
                               Quantity = inventory.Quantity,
                               RetailPrice = price == null ? "0.00" : price.RetailSellingPrice.ToString("N0"),
                               Size = item.Size,
                               UnitCode = item.Unit.Code,
                               Warehouse = inventory.Warehouse.Name
                           });
            return itemsVM;
        }

        public ItemViewVM? GetItemViewVM(int? id)
        {
            var itemVM = (from item in context.Items
                          from price in context.ItemPrices.Where(s => s.ItemId == item.Id && s.DateCreated <= DateTime.Now).OrderByDescending(s => s.DateCreated).Take(1).DefaultIfEmpty()
                          join inventory in context.ItemInventories on item.Id equals inventory.ItemId
                          where item.Id == id
                          select new ItemViewVM
                          {
                              Brand = item.Brand.Code,
                              Category = item.Category.Code,
                              Code = item.Code,
                              Id = item.Id,
                              InterchangeNumber = item.InterchangeNumber,
                              ItemType = item.ItemType.Code,
                              IsActive = item.IsActive,
                              ModelNumber = item.ModelNumber,
                              Name = item.Name,
                              NetPrice = price == null ? "0.00" : price.NetSellingPrice.ToString("N0"),
                              PartNumber = item.PartNumber,
                              Quantity = inventory.Quantity,
                              RetailPrice = price == null ? "0.00" : price.RetailSellingPrice.ToString("N0"),
                              Size = item.Size,
                              UnitCode = item.Unit.Code,
                              Warehouse = inventory.Warehouse.Name
                          }).FirstOrDefault();
            return itemVM;
        }

        public async Task<ItemVM> AddAsync(ItemVM itemVM)
        {
            string userId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            //var item = mapper.Map<Item>(itemVM);
            //await context.Items.AddAsync(item);
            //await context.SaveChangesAsync(userId);
            return itemVM;
        }

        public async Task<bool> Exists(int id)
        {
            return await context.Items.AnyAsync(s => s.Id == id);
        }

        public async Task DeleteAsync(int id)
        {
            var item = context.Items.Where(s => s.Id == id).First();
            context.Items.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ItemVM itemVM)
        {
            string userId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var item = await context.Items.Where(s => s.Id == itemVM.Id).FirstAsync();
            //mapper.Map(itemVM, item);
            context.Items.Update(item);
            await context.SaveChangesAsync(userId);
        }
    }
}