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

        public async Task<IList<JsonOptionsModel>> GetOptions(int? IncludeId)
        {
            return await context.ItemTypes
                //.Where(s => (s.CategoryId == CategoryId && s.IsActive) || s.Id == IncludeId)
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
            var itemType = await context.ItemTypes.Where(s => s.Name == Name).FirstOrDefaultAsync();
            return itemType?.Id;
        }

        public async Task<IList<ItemTypeViewVM>> GetListAsync()
        {
            var itemTypes = from itemType in context.ItemTypes
                            join userc in userManager.Users on itemType.UserCreated equals userc.Id
                            join userm in userManager.Users on itemType.UserModified equals userm.Id into m
                            from um in m.DefaultIfEmpty()
                            select new ItemTypeViewVM
                            {
                                Id = itemType.Id,
                                Category = itemType.Category.Name,
                                CategoryId = itemType.Category.Id,
                                Code = itemType.Code,
                                DateCreated = itemType.DateCreated.ToString(SystemFormat.StringDateTime),
                                DateCreatedD = itemType.DateCreated,
                                DateModified = itemType.DateModified == null ? "-" : itemType.DateModified.Value.ToString(SystemFormat.StringDateTime),
                                DateModifiedD = itemType.DateModified == null ? null : itemType.DateModified.Value,
                                IsActive = itemType.IsActive,
                                Name = itemType.Name,
                                UserCreated = userc.Nickname,
                                UserModified = um == null ? "-" : um.Nickname
                            };
            return await itemTypes.ToListAsync();
        }

        public async Task<ForDataTables<ItemTypeViewVM>> GetListDTAsync(DataTablesRequestForm form)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetListAsync()).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Code.ToLower().Contains(form.SearchValue)
                                    || x.Name.ToLower().Contains(form.SearchValue)
                                    || x.Category.ToLower().Contains(form.SearchValue)
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
                        case "Category": data = data.OrderBy(s => s.Category); break;
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
                        case "NetPrice": data = data.OrderByDescending(s => s.Category); break;
                        case "PartNumber": data = data.OrderByDescending(s => s.UserCreated); break;
                        case "Quantity": data = data.OrderByDescending(s => s.DateCreatedD); break;
                        case "RetailPrice": data = data.OrderByDescending(s => s.UserModified); break;
                        case "Size": data = data.OrderByDescending(s => s.DateModifiedD); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<ItemTypeViewVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public async Task<ItemTypeViewVM?> GetItemTypeViewVM(int? id)
        {
            var itemTypeVM = (await GetListAsync()).Where(s => s.Id == id).FirstOrDefault();
            if (itemTypeVM == null)
            {
                return null;
            }
            return itemTypeVM;
        }

        public async Task<bool> UploadCombinationExistAsync(ItemTypeVM itemTypeVM)
        {
            return (await GetListAsync()).Any(s => s.Code == itemTypeVM.Code && s.Name == itemTypeVM.Name && s.Category == s.Category);
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

        public async Task<IList<ItemType>> GetAllActiveItemTypeAsync(int includeId)
        {
            return await context.ItemTypes.Where(s => s.IsActive || s.Id == includeId).ToListAsync();
        }

        public async Task<bool> IsCodeExist(string code)
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
            return await context.ItemTypes.AnyAsync(s => s.Id == id);
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
            //var itemType = mapper.Map<ItemType>(itemTypeVM);
            var itemType = new ItemType
            {
                Code = itemTypeVM.Code,
                Name = itemTypeVM.Name,
                CategoryId = itemTypeVM.CategoryId,
                IsActive = true
            };
            await context.ItemTypes.AddAsync(itemType);
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task UpdateAsync(ItemTypeVM itemTypeVM)
        {
            var itemType = context.ItemTypes.Where(s => s.Id == itemTypeVM.Id).FirstOrDefault();
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