using AutoMapper;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Application.Repositories
{
    public class ItemTypeRepository : GenericRepository<ItemType>, IItemTypeRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public ItemTypeRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
            , UserManager<User> userManager
            , IMapper mapper) : base(context, httpContextAccessor, userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<List<ItemTypeViewVM>> GetAllItemTypeVMAsync()
        {
            var itemTypes = await context.ItemTypes.Include(s => s.Category).ToListAsync();
            var itemTypesViewVM = mapper.Map<List<ItemTypeViewVM>>(itemTypes);
            foreach (ItemTypeViewVM ivm in itemTypesViewVM)
            {
                ivm.UserCreated = await GetUserName(ivm.UserCreated);
                ivm.UserModified = await GetUserName(ivm.UserModified);
            }
            return itemTypesViewVM;
        }

        public async Task<ItemTypeViewVM?> GetItemTypeViewVMAsync(int? id)
        {
            var itemType = await context.ItemTypes
                .Where(s => s.Id == id)
                .Include(s => s.Category)
                .FirstOrDefaultAsync();
            if (itemType == null)
            {
                return null;
            }
            var itemTypeViewVM = mapper.Map<ItemTypeViewVM>(itemType);
            itemTypeViewVM.UserCreated = await GetUserName(itemTypeViewVM.UserCreated);
            itemTypeViewVM.UserModified = await GetUserName(itemTypeViewVM.UserModified);
            return itemTypeViewVM;
        }

        public async Task<ItemTypeVM?> GetItemTypeVMAsync(int? id)
        {
            var itemType = await context.ItemTypes.Include(s => s.Category).Where(s => s.Id == id).FirstOrDefaultAsync();
            if (itemType == null)
            {
                return null;
            }
            var itemTypeViewVM = mapper.Map<ItemTypeVM>(itemType);
            return itemTypeViewVM;
        }

        public async Task<List<ItemType>> GetAllActiveItemTypeAsync(int includeId)
        {
            return await context.ItemTypes.Where(s => s.IsActive || s.Id == includeId).ToListAsync();
        }

        public async Task<bool> IsItemTypeCodeExist(string code)
        {
            return await context.ItemTypes.AnyAsync(c => c.Code == code);
        }

        public async Task<bool> IsItemTypeInUse(int id)
        {
            return await context.Brands.AnyAsync(s => s.ItemTypeId == id) 
                || await context.Items.AnyAsync(s => s.ItemTypeId == id);
        }

        public async Task<bool> IsItemTypeIdExistAsync(int id)
        {
            return await context.ItemTypes.AnyAsync(s=>s.Id == id);
        }

        public async Task DeactivateItemTypeAsync(int id)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var itemType = await context.ItemTypes.FirstAsync(s => s.Id == id);
            itemType.IsActive = false;
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task ReactivateItemTypeAsync(int id)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var itemType = await context.ItemTypes.FirstAsync(s => s.Id == id);
            itemType.IsActive = true;
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task AddAsync(ItemTypeVM itemTypeVM)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var itemType = mapper.Map<ItemType>(itemTypeVM);
            itemType.IsActive = true;
            await context.ItemTypes.AddAsync(itemType);
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task UpdateAsync(int id, ItemTypeVM itemTypeVM)
        {
            var itemType = context.ItemTypes.Where(s => s.Id == id).FirstOrDefault();
            if (itemType == null)
            {
                throw new DbUpdateConcurrencyException();
            }
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            mapper.Map(itemTypeVM, itemType);
            context.ItemTypes.Update(itemType);
            await context.SaveChangesAsync(currentUserId);
        }
    }
}