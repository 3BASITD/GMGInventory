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

        public async Task<List<JsonOptionsModel>> GetOptions(int ItemTypeId, int? IncludeId)
        {
            return await context.Brands
                .Where(s => (s.ItemTypeId == ItemTypeId && s.IsActive) || s.Id == IncludeId)
                .Select(s => new JsonOptionsModel
                {
                    Id = s.Id.ToString(),
                    Text = s.Name,
                    Selected = IncludeId != null && IncludeId == s.Id
                })
                .ToListAsync();
        }


        public async Task<List<BrandViewVM>> GetAllBrandVMAsync()
        {
            var brand = await context.Brands.Include(s => s.ItemType).ToListAsync();
            var brandViewVM = mapper.Map<List<BrandViewVM>>(brand);
            foreach (BrandViewVM ivm in brandViewVM)
            {
                ivm.UserCreated = await GetUserName(ivm.UserCreated);
                ivm.UserModified = await GetUserName(ivm.UserModified);
            }
            return brandViewVM;
        }

        public async Task<BrandViewVM?> GetBrandViewVMAsync(int? id)
        {
            var brand = await context.Brands.Include(s => s.ItemType).Where(s => s.Id == id).FirstOrDefaultAsync();
            if (brand == null)
            {
                return null;
            }
            var brandViewVM = mapper.Map<BrandViewVM>(brand);
            brandViewVM.UserCreated = await GetUserName(brandViewVM.UserCreated);
            brandViewVM.UserModified = await GetUserName(brandViewVM.UserModified);
            return brandViewVM;
        }

        public async Task<BrandVM?> GetBrandVMAsync(int? id)
        {
            var brand = await context.Brands.Include(s => s.ItemType).Where(s => s.Id == id).FirstOrDefaultAsync();
            if (brand == null)
            {
                return null;
            }
            var brandViewVM = mapper.Map<BrandVM>(brand);
            return brandViewVM;
        }

        public async Task<List<Brand>> GetAllActiveBrandAsync(int includeId)
        {
            return await context.Brands.Where(s => s.IsActive || s.Id == includeId).ToListAsync();
            //return await context.Areas.ToListAsync();
        }

        public async Task<bool> IsBrandCodeExist(string code)
        {
            return await context.Brands.AnyAsync(c => c.Code == code);
        }

        public async Task<bool> IsBrandInUse(int id)
        {
            return await context.Brands.AnyAsync(s => s.Id == id);
        }

        public async Task<bool> IsBrandIdExistAsync(int id)
        {
            return await context.Brands.AnyAsync(s => s.Id == id);
        }

        public async Task DeactivateBrandAsync(int id)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var brand = await context.Brands.FirstAsync(s => s.Id == id);
            brand.IsActive = false;
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task ReactivateBrandAsync(int id)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var brand = await context.Brands.FirstAsync(s => s.Id == id);
            brand.IsActive = true;
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task AddAsync(BrandVM brandVM)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var brand = mapper.Map<Brand>(brandVM);
            brand.IsActive = true;
            await context.Brands.AddAsync(brand);
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task UpdateAsync(int id, BrandVM brandVM)
        {
            var brand = context.Brands.Where(s => s.Id == id).FirstOrDefault();
            if (brand == null)
            {
                throw new DbUpdateConcurrencyException();
            }
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            mapper.Map(brandVM, brand);
            context.Brands.Update(brand);
            await context.SaveChangesAsync(currentUserId);
        }
    }
}