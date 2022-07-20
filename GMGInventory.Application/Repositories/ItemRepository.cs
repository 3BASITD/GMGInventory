using AutoMapper;
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
        private readonly IMapper mapper;

        public ItemRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
            , UserManager<User> userManager
            , IMapper mapper)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<ItemVM> AddAsync(ItemVM itemVM)
        {
            string userId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var item = mapper.Map<Item>(itemVM);
            await context.Items.AddAsync(item);
            await context.SaveChangesAsync(userId);
            return mapper.Map<ItemVM>(item);
        }

        public async Task DeleteAsync(int id)
        {
            var item = context.Items.Where(s => s.Id == id).First();
            context.Items.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            return await context.Items.AnyAsync(s => s.Id == id);
        }

        public async Task<List<ItemInformationVM>> GetAllInformationAsync()
        {
            var items = await context.Items.ToListAsync();
            var itemsVM = mapper.Map<List<ItemInformationVM>>(items);
            Action<ItemInformationVM> action = async s =>
            {
                s.UserCreated = await GetUserName(s.UserCreated);
                s.UserModified = await GetUserName(s.UserModified);
            };
            itemsVM.ForEach(action);
            return itemsVM;
        }

        public async Task<ItemInformationVM?> GetInformationAsync(int? id)
        {
            var item = await context.Items
                .Where(s => s.Id == id)
                .Include(s => s.Category)
                .Include(s => s.ItemType)
                .Include(s => s.Brand)
                .FirstOrDefaultAsync();
            if (item == null)
            {
                return null;
            }
            var itemVM = mapper.Map<ItemInformationVM>(item);
            itemVM.UserCreated = await GetUserName(itemVM.UserCreated);
            itemVM.UserModified = await GetUserName(itemVM.UserModified);
            return itemVM;
        }

        public async Task UpdateAsync(ItemVM itemVM)
        {
            string userId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var item = await context.Items.Where(s => s.Id == itemVM.Id).FirstAsync();
            mapper.Map(itemVM, item);
            context.Items.Update(item);
            await context.SaveChangesAsync(userId);
        }

        private async Task<string> GetUserName(string? userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            return user != null ? user.Nickname : "-";
        }
    }
}