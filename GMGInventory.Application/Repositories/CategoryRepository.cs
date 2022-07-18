using AutoMapper;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Application.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public CategoryRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
            , UserManager<User> userManager
            , IMapper mapper) : base(context, httpContextAccessor, userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<List<GenericAuditVM>> GetAllCategoryVMAsync()
        {
            var categories = await context.Categories.ToListAsync();
            var categoriesVM = mapper.Map<List<GenericAuditVM>>(categories);
            foreach (GenericAuditVM cvm in categoriesVM)
            {
                cvm.UserCreated = await GetUserName(cvm.UserCreated);
                cvm.UserModified = await GetUserName(cvm.UserModified);
            }
            return categoriesVM;
        }

        public async Task<GenericAuditVM?> GetCategoryVMAsync(int? id)
        {
            var category = await context.Categories.Where(s => s.Id == id).FirstOrDefaultAsync();
            if (category == null)
            {
                return null;
            }
            var categoryVM = mapper.Map<GenericAuditVM>(category);
            categoryVM.UserCreated = await GetUserName(categoryVM.UserCreated);
            categoryVM.UserModified = await GetUserName(categoryVM.UserModified);
            return categoryVM;
        }

        public async Task<List<Category>> GetAllActiveCategoryAsync(int includeId)
        {
            return await context.Categories.Where(s => s.IsActive || s.Id == includeId).ToListAsync();
        }

        public async Task<bool> IsCategoryCodeExist(string code)
        {
            return await context.Categories.AnyAsync(c => c.Code == code);
        }

        public async Task<bool> IsCategoryInUse(int id)
        {
            return await context.ItemTypes.AnyAsync(s => s.CategoryId == id);
        }
    }
}