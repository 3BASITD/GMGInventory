using AutoMapper;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Application.Repositories
{
    public class RegionRepository : GenericRepository<Region>, IRegionRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public RegionRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
            , UserManager<User> userManager
            , IMapper mapper) : base(context, httpContextAccessor, userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<IList<RegionVM>> GetAllRegionAsync()
        {
            var regions = await context.Regions.ToListAsync();
            var regionsVM = mapper.Map<List<RegionVM>>(regions);
            foreach (RegionVM cvm in regionsVM)
            {
                cvm.UserCreated = await GetUserName(cvm.UserCreated);
                cvm.UserModified = await GetUserName(cvm.UserModified);
            }
            return regionsVM;
        }

        public async Task<RegionVM?> GetRegionVMAsync(int? id)
        {
            var regions = await context.Regions.Where(s => s.Id == id).FirstOrDefaultAsync();
            if (regions == null)
            {
                return null;
            }
            var regionsVM = mapper.Map<RegionVM>(regions);
            regionsVM.UserCreated = await GetUserName(regionsVM.UserCreated);
            regionsVM.UserModified = await GetUserName(regionsVM.UserModified);
            return regionsVM;
        }

        public async Task<IList<Region>> GetAllActiveRegionAsync(int includeId)
        {
            //return await context.Regions.Where(s => s.IsActive || s.Id == includeId).ToListAsync();
            var region = await context.Regions.ToListAsync();
            return region;
        }

        public async Task<bool> IsRegionCodeExist(string code)
        {
            return await context.Regions.AnyAsync(c => c.Code == code);
        }

        public async Task<bool> IsRegionInUse(int id)
        {
            return await context.Areas.AnyAsync(s => s.RegionId == id);
        }

        public async Task<IList<JsonOptionsModel>> GetOptions(int? IncludeId)
        {
            return await context.Regions
                .Select(s => new JsonOptionsModel
                {
                    Id = s.Id.ToString(),
                    Text = s.Name,
                    Selected = IncludeId != null && IncludeId == s.Id
                })
                .OrderBy(s => s.Text)
                .ToListAsync();
        }

        public async Task<int?> GetIdAsync(string? Name)
        {
            var region = await context.Regions.Where(s => s.Name == Name).FirstOrDefaultAsync();
            return region?.Id;
        }

        public async Task<ForDataTables<RegionVM>> GetAllForDataTablesAsync(DataTablesRequestForm form)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetAllLocationCodeAsync()).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Code.ToLower().Contains(form.SearchValue)
                                    || x.Name.ToLower().Contains(form.SearchValue)
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
                        case "Code": data = data.OrderBy(s => s.Code); break;
                        case "Name": data = data.OrderBy(s => s.Name); break;
                        case "IsActive": data = data.OrderBy(s => s.IsActive); break;
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
                        case "IsActive": data = data.OrderByDescending(s => s.IsActive); break;
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
            return new ForDataTables<RegionVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public async Task<IList<RegionVM>> GetAllLocationCodeAsync()
        {

            var regions = from region in context.Regions

                            join userc in userManager.Users on region.UserCreated equals userc.Id
                            join userm in userManager.Users on region.UserModified equals userm.Id into m
                            from um in m.DefaultIfEmpty()

                            select new RegionVM
                            {
                                Id = region.Id,
                                Name = region.Name,
                                Code = region.Code,
                                IsActive = region.IsActive,
                                DateCreatedD = region.DateCreated,
                                DateModifiedD = region.DateModified == null ? null : region.DateModified.Value,
                                UserCreated = userc.Nickname,
                                UserModified = um == null ? "-" : um.Nickname
                            };
            return await regions.ToListAsync();
        }

        public async Task AddAsync(RegionVM regionVM)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var location = new LocationCode
            {
                Code = regionVM.Code,
                Name = regionVM.Name,
                IsActive = true
            };
            await context.LocationCodes.AddAsync(location);
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task UpdateAsync(RegionVM regionVM)
        {
            var region = context.Regions.Where(s => s.Id == regionVM.Id).FirstOrDefault();
            if (region == null)
            {
                throw new DbUpdateConcurrencyException();
            }
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            region.Id = regionVM.Id;

            region.Code = regionVM.Code;
            region.Name = regionVM.Name;
            region.IsActive = true;
            context.Regions.Update(region);
            await context.SaveChangesAsync(currentUserId);
        }
    }
}