using AutoMapper;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Application.Repositories
{
    public class BrandRepository : GenericRepository<Brand>, IBrandRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public BrandRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
            , UserManager<User> userManager
            , IMapper mapper) : base(context, httpContextAccessor, userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<List<BrandViewVM>> GetAllBrandVMAsync()
        {
            var brands = await context.Brands.Include(s => s.ItemType).ToListAsync();
            var brandsViewVM = mapper.Map<List<BrandViewVM>>(brands);
            foreach (BrandViewVM bvm in brandsViewVM)
            {
                bvm.UserCreated = await GetUserName(bvm.UserCreated);
                bvm.UserModified = await GetUserName(bvm.UserModified);
            }
            return brandsViewVM;
        }

        public async Task<BrandViewVM?> GetBrandVMAsync(int? id)
        {
            var brands = await context.Brands.Include(s => s.ItemType).Where(s => s.Id == id).FirstOrDefaultAsync();
            if (brands == null)
            {
                return null;
            }
            var brandViewVM = mapper.Map<BrandViewVM>(brands);
            brandViewVM.UserCreated = await GetUserName(brandViewVM.UserCreated);
            brandViewVM.UserModified = await GetUserName(brandViewVM.UserModified);
            return brandViewVM;
        }

        public async Task<List<Brand>> GetAllActiveBrandAsync(int includeId)
        {
            return await context.Brands.Where(s => s.IsActive || s.Id == includeId).ToListAsync();
        }

        public async Task<bool> IsBrandCodeExist(string code)
        {
            return await context.Brands.AnyAsync(c => c.Code == code);
        }

        public async Task<bool> IsBrandInUse(int id)
        {
            return await context.Brands.AnyAsync(s => s.ItemTypeId == id)
                || await context.Items.AnyAsync(s => s.ItemTypeId == id);
        }
    }
}