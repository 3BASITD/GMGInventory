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
            var _name = (Name + "").ToLower().Replace("\n", "").Replace("\r", "").Trim();
            var name = _name.Length > 100 ? _name[..100] : _name;
            var code = _name.Length > 10 ? _name[..10] : _name;
            var category = await context.Categories
                                        .Where(s => s.Name.ToLower().Replace("\n", "").Replace("\r", "").Trim() == name
                                                    || s.Code.ToLower().Replace("\n", "").Replace("\r", "").Trim() == code)
                                        .FirstOrDefaultAsync();
            return category?.Id;
        }



        public async Task<IList<CategoryVM>> GetAllCategoryAsync()
        {

            var categories = from category in context.Categories

                             join userc in userManager.Users on category.UserCreated equals userc.Id
                             join userm in userManager.Users on category.UserModified equals userm.Id into m
                             from userm in m.DefaultIfEmpty()

                             select new CategoryVM
                             {
                                 Id = category.Id,
                                 Name = category.Name,
                                 Code = category.Code,
                                 IsActive = category.IsActive,
                                 DateCreatedD = category.DateCreated,
                                 DateModifiedD = category.DateModified == null ? null : category.DateModified.Value,
                                 UserCreated = SystemFormat.Name(userc.FirstName, userc.LastName, userc.Nickname),
                                 UserModified = userm == null ? "-" : SystemFormat.Name(userm.FirstName, userm.LastName, userm.Nickname)
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
                data = data.Where(x => x.Code.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.Name.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.UserCreated.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.DateCreated.ToString().ToLower().Contains(form.SearchValue.ToLower())
                                    || x.UserModified.ToLower().ToLower().Contains(form.SearchValue.ToLower())
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
                        case "Code": data = data.OrderBy(s => s.Code); break;
                        case "Name": data = data.OrderBy(s => s.Name); break;
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
                        case "Code": data = data.OrderByDescending(s => s.Code); break;
                        case "Name": data = data.OrderByDescending(s => s.Name); break;
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
                cvm.UserCreated = await GetNickname(cvm.UserCreated);
                cvm.UserModified = await GetNickname(cvm.UserModified);
            }
            return categoriesVM;
        }

        public async Task<IList<APIGeneric>> GetListAPI()
        {
            var categories = from category in context.Categories
                             where category.IsActive == true
                             select new APIGeneric
                             {
                                 Id = category.Id,
                                 Code = category.Code,
                                 Name = category.Name
                             };

            return await categories.ToListAsync();
        }

        public async Task<GenericAuditVM?> GetGenericVMAsync(int? id)
        {
            var category = await context.Categories.Where(s => s.Id == id).FirstOrDefaultAsync();
            if (category == null)
            {
                return null;
            }
            var categoryVM = mapper.Map<GenericAuditVM>(category);
            categoryVM.UserCreated = await GetNickname(categoryVM.UserCreated);
            categoryVM.UserModified = await GetNickname(categoryVM.UserModified);
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
                Code = categoryVM.Code,
                Name = categoryVM.Name,
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

            category.Code = categoryVM.Code;
            category.Name = categoryVM.Name;
            category.IsActive = true;
            context.Categories.Update(category);
            await context.SaveChangesAsync(currentUserId);
        }


        public async Task<bool> UploadCombinationExistAsync(CategoryVM categoryVM)
        {
            var _code = categoryVM.Code.ToLower().Replace("\r", "").Replace("\n", "").Trim();
            var code = _code.Length > 10 ? _code[..10] : _code;
            var _name = categoryVM.Name.ToLower().Replace("\r", "").Replace("\n", "").Trim();
            var name = _name.Length > 100 ? _name[..100] : _name;

            return (await GetAllGenericVMAsync()).Any(s => s.Code.ToLower().Replace("\r","").Replace("\n","").Trim() == code
                                                        && s.Name.ToLower().Replace("\r", "").Replace("\n", "").Trim() == name);
        }


        public async Task<bool> IsCodeExist(string Code)
        {
            var _code = Code.ToLower().Replace("\r", "").Replace("\n", "").Trim();
            var code = _code.Length > 10 ? _code[..10] : _code;

            return await context.Categories.AnyAsync(c => c.Code.ToLower().Replace("\r", "").Replace("\n", "").Trim() == code);
        }

    }
}