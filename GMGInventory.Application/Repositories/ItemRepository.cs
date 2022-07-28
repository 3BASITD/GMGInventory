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

        public async Task<ItemViewVM?> GetAsync(int? id)
        {
            var itemVM = await context.ItemPrices
                .Include(s => s.Item)
                    .ThenInclude(s => s.Category)
                .Include(s => s.Item)
                    .ThenInclude(s => s.ItemType)
                .Include(s => s.Item)
                    .ThenInclude(s => s.Brand)
                .Where(s => s.Item.Id == id)
                .Select(s => new ItemViewVM
                {
                    Id = s.Item.Id,
                    Code = s.Item.Code,
                    Name = s.Item.Name,

                })
                .FirstOrDefaultAsync();
            return itemVM;
        }

        public IQueryable<ItemViewVM> GetAll(ItemFilterVM itemFilterVM)
        {
            var itemsVM = (from item in context.Items
                           from price in context.ItemPrices.Where(s => s.ItemId == item.Id && s.EffectiveDate <= DateTime.Now).OrderByDescending(s => s.EffectiveDate).Take(1).DefaultIfEmpty()
                           join inventory in context.ItemInventories on item.Id equals inventory.ItemId
                           where ((itemFilterVM.CategoryId == null || itemFilterVM.CategoryId == item.CategoryId)
                               && (itemFilterVM.ItemTypeId == null || itemFilterVM.ItemTypeId == item.ItemTypeId)
                               && (itemFilterVM.BrandId == null || itemFilterVM.BrandId == item.BrandId)
                               && (itemFilterVM.WarehouseId == null || itemFilterVM.WarehouseId == inventory.WarehouseId))
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
                               NetPrice = price == null ? 0 : price.NetSellingPrice,
                               PartNumber = item.PartNumber,
                               Quantity = inventory.Quantity,
                               RetailPrice = price == null ? 0 : price.RetailSellingPrice,
                               Size = item.Size,
                               IsNonInventory = item.IsNonInventory,
                               UnitCode = item.Unit.Code,
                               Warehouse = inventory.Warehouse.Name
                           });
            return itemsVM;
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