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

        public IQueryable<BrandViewVM> GetAllBrandVM()
        {
            var brands = from brand in context.Brands
                         select new BrandViewVM
                         {
                             Id = brand.Id,
                             ItemType = brand.ItemType.Name,
                             ItemTypeId = brand.ItemType.Id,
                             Code = brand.Code,
                             DateCreated = brand.DateCreated.ToString("yyyy/dd/MM hh:mm:ss tt"),
                             DateModified = brand.DateModified == null ? "-" : brand.DateModified.Value.ToString("yyyy/dd/MM hh:mm:ss tt"),
                             IsActive = brand.IsActive,
                             Name = brand.Name,
                             UserCreated = userManager.FindByIdAsync(brand.UserCreated).Result.Nickname,
                             UserModified = brand.UserModified == null ? "-" : userManager.FindByIdAsync(brand.UserModified).Result.Nickname
                         };
            return brands;
        }


        public ForDataTables<BrandViewVM> GetListDataTables(DataTablesRequestForm form)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = GetAllBrandVM().ToList().AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Code.ToLower().Contains(form.SearchValue)
                                    || x.Name.ToLower().Contains(form.SearchValue)
                                    || x.ItemType.ToLower().Contains(form.SearchValue)
                                    || x.UserCreated.ToLower().Contains(form.SearchValue)
                                    || x.DateCreated.ToString().ToLower().Contains(form.SearchValue)
                                    || x.UserModified.ToString().ToLower().Contains(form.SearchValue)
                                    || x.DateModified.ToLower().Contains(form.SearchValue));
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
                        case "ItemType": data = data.OrderBy(s => s.ItemType); break;
                        case "UserCreated": data = data.OrderBy(s => s.UserCreated); break;
                        case "DateCreated": data = data.OrderBy(s => s.DateModified); break;
                        case "UserModified": data = data.OrderBy(s => s.UserModified); break;
                        case "DateModified": data = data.OrderBy(s => s.DateModified); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Code": data = data.OrderByDescending(s => s.Code); break;
                        case "Name": data = data.OrderByDescending(s => s.Name); break;
                        case "ItemType": data = data.OrderByDescending(s => s.ItemType); break;
                        case "UserCreated": data = data.OrderByDescending(s => s.UserCreated); break;
                        case "DateCreated": data = data.OrderByDescending(s => s.DateModified); break;
                        case "UserModified": data = data.OrderByDescending(s => s.UserModified); break;
                        case "DateModified": data = data.OrderByDescending(s => s.DateModified); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<BrandViewVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public BrandViewVM? GetBrandViewVM(int? id)
        {
            var brandVM = GetAllBrandVM().Where(s => s.Id == id).FirstOrDefault();
            if (brandVM == null)
            {
                return null;
            }
            return brandVM;
        }

        public async Task<bool> UploadCombinationExistAsync(BrandVM brandVM)
        {
            return await GetAllBrandVM().AnyAsync(s => s.Code == brandVM.Code && s.Name == brandVM.Name && s.ItemType == s.ItemType);
        }

        public async Task<BrandVM?> GetBrandVMAsync(int? id)
        {
            var brand = await context.Brands
                .Include(s => s.ItemType)
                .Where(s => s.Id == id)
                .FirstOrDefaultAsync();
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
        }

        public async Task<bool> IsBrandCodeExist(string code)
        {
            return await context.Brands.AnyAsync(c => c.Code == code);
        }

        public async Task<bool> IsBrandInUse(int id)
        {
            return await context.ItemTypes.AnyAsync(s => s.Id == id);
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
            //var itemType = mapper.Map<ItemType>(itemTypeVM);
            var brand = new Brand
            {
                Code = brandVM.Code,
                Name = brandVM.Name,
                ItemTypeId = brandVM.ItemTypeId,
                IsActive = true
            };
            await context.Brands.AddAsync(brand);
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task UpdateAsync(BrandVM brandVM)
        {
            var brand = context.Brands.Where(s => s.Id == brandVM.Id).FirstOrDefault();
            if (brand == null)
            {
                throw new DbUpdateConcurrencyException();
            }
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            mapper.Map(brandVM, brand);
            context.Brands.Update(brand);
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task<List<JsonOptionsModel>> GetOptions(int? IncludeId)
        {
            return await context.Brands
                .Where(s => s.IsActive || s.Id == IncludeId)
                .Select(s => new JsonOptionsModel
                {
                    Id = s.Id.ToString(),
                    Text = s.Name,
                    Selected = IncludeId != null && IncludeId == s.Id
                })
                .ToListAsync();
        }

    }
}
