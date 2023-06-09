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

        public async Task<IList<JsonOptionsModel>> GetOptions(int? IncludeId)
        {
            return await context.Categories
                .Where(s => s.IsActive || s.Id == IncludeId)
                .Select(s => new JsonOptionsModel
                {
                    Id = s.Id.ToString(),
                    Text = (s.Code == s.Name ? "" : "(" + s.Code + ") ") + s.Name,
                    Selected = IncludeId != null && IncludeId == s.Id
                })
                .OrderBy(s => s.Text)
                .ToListAsync();
        }

        public async Task<int?> GetIdAsync(string? Name)
        {
            var category = await context.Categories.Where(s => s.Name == Name).FirstOrDefaultAsync();
            return category?.Id;
        }


        
        public async Task<IList<CategoryVM>> GetAllCategoryAsync()
        {

            var categories = from category in context.Categories
                        
                         join userc in userManager.Users on category.UserCreated equals userc.Id
                         join userm in userManager.Users on category.UserModified equals userm.Id into m
                         from um in m.DefaultIfEmpty()

                         select new CategoryVM
                         {
                             Id = category.Id,
                             CategoryName = category.Name,
                             CategoryCode = category.Code,
                             IsActive = category.IsActive,
                             DateCreatedD = category.DateCreated,
                             DateModifiedD = category.DateModified == null ? null : category.DateModified.Value,
                             UserCreated = userc.Nickname,
                             UserModified = um == null ? "-" : um.Nickname
                         };
            return await categories.ToListAsync();
        }

        public async Task<ForDataTables<CategoryVM>> GetAllForDataTablesAsync(DataTablesRequestForm form)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetAllCategoryAsync()).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.CategoryCode.ToLower().Contains(form.SearchValue)
                                    || x.CategoryName.ToLower().Contains(form.SearchValue)
                                    || x.UserCreated.ToLower().Contains(form.SearchValue)
                                    || x.DateCreated.ToString().ToLower().Contains(form.SearchValue)
                                    || x.UserModified.ToLower().ToLower().Contains(form.SearchValue)
                                    || x.DateModified.ToString().Contains(form.SearchValue));
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
                        case "CategoryCode": data = data.OrderBy(s => s.CategoryCode); break;
                        case "CategoryName": data = data.OrderBy(s => s.CategoryName); break;
                        case "UserCreated": data = data.OrderBy(s => s.UserCreated); break;
                        case "DateCreated": data = data.OrderBy(s => s.DateCreatedD); break;
                        case "UserModified": data = data.OrderBy(s => s.UserModified); break;
                        case "DateModified": data = data.OrderBy(s => s.DateModifiedD); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "CategoryCode": data = data.OrderByDescending(s => s.CategoryCode); break;
                        case "CategoryName": data = data.OrderByDescending(s => s.CategoryName); break;
                        case "UserCreated": data = data.OrderByDescending(s => s.UserCreated); break;
                        case "DateCreated": data = data.OrderByDescending(s => s.DateCreatedD); break;
                        case "UserModified": data = data.OrderByDescending(s => s.UserModified); break;
                        case "DateModified": data = data.OrderByDescending(s => s.DateModifiedD); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<CategoryVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }


        public async Task<IList<GenericAuditVM>> GetAllGenericVMAsync()
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

        public async Task<GenericAuditVM?> GetGenericVMAsync(int? id)
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

        public async Task<CategoryVM?> GetCategoryVMAsync(int? id)
        {
            var category = await context.Categories.Where(s => s.Id == id).FirstOrDefaultAsync();
            if (category == null)
            {
                return null;
            }
            var categoryVM = mapper.Map<CategoryVM>(category);
            return categoryVM;
        }

        public async Task<IList<Category>> GetAllActiveCategoryAsync(int includeId)
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


        public async Task AddAsync(CategoryVM categoryVM)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var category = new Category
            {
                Code = categoryVM.CategoryCode,
                Name = categoryVM.CategoryName,
                IsActive = true

            };
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task UpdateAsync(CategoryVM categoryVM)
        {
            var category = context.Categories.Where(s => s.Id == categoryVM.Id).FirstOrDefault();
            if (category == null)
            {
                throw new DbUpdateConcurrencyException();
            }
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            category.Id = categoryVM.Id;

            category.Code = categoryVM.CategoryCode;
            category.Name = categoryVM.CategoryName;
            category.IsActive = true;
            context.Categories.Update(category);
            await context.SaveChangesAsync(currentUserId);
        }


        public async Task<bool> UploadCombinationExistAsync(CategoryVM categoryVM)
        {
            return (await GetAllGenericVMAsync()).Any(s => s.Code == categoryVM.CategoryCode && s.Name == categoryVM.CategoryName);
        }


        public async Task<bool> IsCodeExist(string code)
        {
            return await context.Categories.AnyAsync(c => c.Code == code);
        }

    }
}