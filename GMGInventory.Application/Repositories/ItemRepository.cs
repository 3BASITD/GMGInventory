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

        public async Task<List<ItemViewVM>> GetAllAsync(ItemFilterVM itemFilterVM)
        {
            var test = await context.Currencies.ToListAsync();
            var itemsVM = await context.ItemInventories
                .Include(s => s.Item)
                .Include(s => s.Warehouse)
                .Where(s =>
                (itemFilterVM.CategoryId == null || itemFilterVM.CategoryId == s.Item.CategoryId) &&
                (itemFilterVM.ItemTypeId == null || itemFilterVM.ItemTypeId == s.Item.ItemTypeId) &&
                (itemFilterVM.BrandId == null || itemFilterVM.BrandId == s.Item.BrandId) &&
                (itemFilterVM.WarehouseId == null || itemFilterVM.WarehouseId == s.WarehouseId))
                .Select(s => new ItemViewVM
                {
            Id = s.Id
                })
                .ToListAsync();
            //var itemsVM = mapper.Map<List<ItemViewVM>>(items);
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