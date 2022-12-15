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
    public class AreaRepository : GenericRepository<Area>, IAreaRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public AreaRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
            , UserManager<User> userManager
            , IMapper mapper) : base(context, httpContextAccessor, userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<int?> GetIdAsync(string? Name)
        {
            var area = await context.Areas.Where(s => s.Name == Name).FirstOrDefaultAsync();
            return area?.Id;
        }


        public async Task<IList<AreaViewVM>> GetAllAsync()
        {

            var agents = from area in context.Areas
                         join region in context.Regions
                                    on area.RegionId equals region.Id
                        
                         join userc in userManager.Users on area.UserCreated equals userc.Id
                         join userm in userManager.Users on area.UserModified equals userm.Id into m
                         from um in m.DefaultIfEmpty()

                         select new AreaViewVM
                         {
                             Id = area.Id,
                             Name = area.Name,
                             Code = area.Code,
                             CommissionRateD = area.CommissionRate,
                             RegionName = region.Name,
                             IsActive = area.IsActive,
                             DateCreated = area.DateCreated.ToString(SystemFormat.StringDateTime),
                             DateCreatedD = area.DateCreated,
                             DateModified = area.DateModified == null ? "-" : area.DateModified.Value.ToString(SystemFormat.StringDateTime),
                             DateModifiedD = area.DateModified == null ? null : area.DateModified.Value,
                             UserCreated = userc.Nickname,
                             UserModified = um == null ? "-" : um.Nickname
                         };
            return await agents.ToListAsync();
        }

        public async Task<ForDataTables<AreaViewVM>> GetAllForDataTablesAsync(DataTablesRequestForm form)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetAllAsync()).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Code.ToLower().Contains(form.SearchValue)
                                    || x.Name.ToLower().Contains(form.SearchValue)
                                    || x.CommissionRate.ToLower().Contains(form.SearchValue)
                                    || x.RegionName.ToLower().Contains(form.SearchValue)
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
                        case "CommissionRate": data = data.OrderBy(s => s.CommissionRate); break;
                        case "RegionName": data = data.OrderBy(s => s.RegionName); break;
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
                        case "CommissionRate": data = data.OrderByDescending(s => s.CommissionRate); break;
                        case "RegionName": data = data.OrderByDescending(s => s.RegionName); break;
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
            return new ForDataTables<AreaViewVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public async Task<IList<AreaViewVM>> GetAllAreaVMAsync()
        {
            var areas = await context.Areas.Include(s => s.Region).ToListAsync();
            var areasViewVM = mapper.Map<List<AreaViewVM>>(areas);
            foreach (AreaViewVM ivm in areasViewVM)
            {
                ivm.UserCreated = await GetUserName(ivm.UserCreated);
                ivm.UserModified = await GetUserName(ivm.UserModified);
            }
            return areasViewVM;
        }

        public async Task<AreaViewVM?> GetAreaViewVMAsync(int? id)
        {
            var areas = await context.Areas.Include(s => s.Region).Where(s => s.Id == id).FirstOrDefaultAsync();
            if (areas == null)
            {
                return null;
            }
            var areasViewVM = mapper.Map<AreaViewVM>(areas);
            areasViewVM.UserCreated = await GetUserName(areasViewVM.UserCreated);
            areasViewVM.UserModified = await GetUserName(areasViewVM.UserModified);
            return areasViewVM;
        }

        public async Task<AreaVM?> GetAreaVMAsync(int? id)
        {
            var area = await context.Areas.Include(s => s.Region).Where(s => s.Id == id).FirstOrDefaultAsync();
            if (area == null)
            {
                return null;
            }
            var areaViewVM = mapper.Map<AreaVM>(area);
            return areaViewVM;
        }

        public async Task<IList<Area>> GetAllActiveAreaAsync(int includeId)
        {
            return await context.Areas.Where(s => s.IsActive || s.Id == includeId).ToListAsync();
            //var areas = await context.Areas.ToListAsync();
            //return areas;
        }

        public async Task<bool> IsAreaCodeExist(string code)
        {
            return await context.Areas.AnyAsync(c => c.Code == code);
        }

        public async Task<bool> IsAreaInUse(int id)
        {
            return await context.Regions.AnyAsync(s => s.Id == id)
             || await context.Customers.AnyAsync(s => s.AreaId == id);
        }

        public async Task<bool> IsAreaIdExistAsync(int id)
        {
            return await context.Areas.AnyAsync(s => s.Id == id);
        }

        public async Task DeactivateAreaAsync(int id)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var area = await context.Areas.FirstAsync(s => s.Id == id);
            area.IsActive = false;
            await context.SaveChangesAsync(currentUserId);
        }


        public async Task ReactivateAreaAsync(int id)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var area = await context.Areas.FirstAsync(s => s.Id == id);
            area.IsActive = true;
            await context.SaveChangesAsync(currentUserId);
        }



        public async Task AddAsync(AreaVM areaVM)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

            var area = new Area
            {
                Code = areaVM.Code,
                Name = areaVM.Name,
                CommissionRate = areaVM.CommissionRateD,
                RegionId = areaVM.RegionId,
                IsActive = true
            };
            await context.Areas.AddAsync(area);
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task UpdateAsync(AreaVM areaVM)
        {
            var area = context.Areas.Where(s => s.Id == areaVM.Id).FirstOrDefault();
            if (area == null)
            {
                throw new DbUpdateConcurrencyException();
            }

            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            area.Id = areaVM.Id;
            area.Code = areaVM.Code;
            area.Name = areaVM.Name;
            area.CommissionRate = areaVM.CommissionRateD;
            area.RegionId = areaVM.RegionId;
            area.IsActive = true;

            context.Areas.Update(area);
            await context.SaveChangesAsync(currentUserId);
        }





        public async Task<bool> UploadCombinationExistAsync(AreaVM areaVM)
        {
            return (await GetAllAsync()).Any(s => s.Code == areaVM.Code && s.Name == areaVM.Name &&
            s.CommissionRate == areaVM.CommissionRate && s.RegionName == areaVM.RegionName);
        }

        public async Task<bool> IsCodeExist(string code)
        {
            return await context.Areas.AnyAsync(c => c.Code == code);
        }



        public async Task<IList<JsonOptionsModel>> GetOptions(int? IncludeId)
        {
            return await context.Areas
                .Where(s => s.IsActive || s.Id == IncludeId)
                .Select(s => new JsonOptionsModel
                {
                    Id = s.Id.ToString(),
                    Text = s.Name,
                    Selected = IncludeId != null && IncludeId == s.Id
                })
                .ToListAsync();
        }


        public async Task<Select2Model<JsonAddressOptionsModel>> GetOptions2(string search, int page)
        {
            var areas = await context.Areas
                                         .Include(s => s.Region)
                                         .Where(s => string.IsNullOrWhiteSpace(search)
                                                 || (s.Code.ToLower().Contains(search.ToLower())))
                                         .Select(s => new JsonAddressOptionsModel
                                         {
                                             Id = s.Id.ToString(),
                                             Text = s.Code,
                                             Name = s.Name,
                                             Region  = s.Region.Name,
                                         })
                                         .ToListAsync();
            var areasFiltered = areas.Skip((page - 1) * 50).Take(50).ToList();
            return new Select2Model<JsonAddressOptionsModel>
            {
                //incomplete_results = false,
                items = areasFiltered,
                total_count = areas.Count
            };
        }
    }
}