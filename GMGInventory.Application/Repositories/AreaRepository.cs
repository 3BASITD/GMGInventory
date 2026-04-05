using AutoMapper;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

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
                .OrderBy(s => s.Text)
                .ToListAsync();
        }

        public async Task<Select2Model<JsonCustomerOptionsModel>> GetOptions2(string search, int page)
        {
            var areas = await context.Areas
                                     .Include(s => s.Region)
                                     .Where(s => string.IsNullOrWhiteSpace(search)
                                              || (s.Code.ToLower().Contains(search.ToLower()))
                                              || (s.Name.ToLower().Contains(search.ToLower())))
                                     .Select(s => new JsonCustomerOptionsModel
                                     {
                                         Id = s.Id.ToString(),
                                         Text = s.Code,
                                         Name = s.Name,
                                         Region = s.Region.Name,
                                     })
                                     .OrderBy(s => s.Text)
                                     .ToListAsync();
            var areasFiltered = areas.Skip((page - 1) * 50).Take(50).ToList();
            return new Select2Model<JsonCustomerOptionsModel>
            {
                //incomplete_results = false,
                items = areasFiltered,
                total_count = areas.Count
            };
        }

        public async Task<Select2Model<JsonCustomerOptionsModel>> GetByRegionOptions(string search, int page, int? region)
        {
            var areas = await context.Areas
                                     .Where(s => (string.IsNullOrWhiteSpace(search)
                                              || (s.Code.ToLower().Contains(search.ToLower()))
                                              || (s.Name.ToLower().Contains(search.ToLower())))
                                              && s.RegionId == region)
                                     .Select(s => new JsonCustomerOptionsModel
                                     {
                                         Id = s.Id.ToString(),
                                         Text = s.Name,
                                         Name = s.Code
                                     })
                                     .OrderBy(s => s.Text)
                                     .ToListAsync();
            var areasFiltered = areas.Skip((page - 1) * 50).Take(50).ToList();
            return new Select2Model<JsonCustomerOptionsModel>
            {
                //incomplete_results = false,
                items = areasFiltered,
                total_count = areas.Count
            };
        }

        public async Task<int?> GetIdAsync(string? Name)
        {
            var area = await context.Areas.Where(s => s.Name.ToLower() == Name.ToLower()).FirstOrDefaultAsync();
            return area?.Id;
        }


        public async Task<IList<AreaViewVM>> GetAllAreaAsync()
        {

            var agents = from area in context.Areas
                         join region in context.Regions
                                    on area.RegionId equals region.Id
                         join userc in userManager.Users on area.UserCreated equals userc.Id
                         join userm in userManager.Users on area.UserModified equals userm.Id into m
                         from userm in m.DefaultIfEmpty()

                         select new AreaViewVM
                         {
                             Id = area.Id,
                             Name = area.Name,
                             Code = area.Code,
                             CommissionRateD = area.CommissionRate,
                             RegionName = region.Name,
                             IsActive = area.IsActive,
                             DateCreatedD = area.DateCreated,
                             DateModifiedD = area.DateModified == null ? null : area.DateModified,
                             UserCreated = userc.FirstName + " " + userc.LastName,
                             UserModified = userm == null ? null : userm.FirstName + " " + userm.LastName
                         };
            return await agents.ToListAsync();
        }

        public async Task<ForDataTables<AreaViewVM>> GetAllForDataTablesAsync(DataTablesRequestForm form)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetAllAreaAsync()).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(s => new[]
                {
                    s.Code,
                    s.Name,
                    s.CommissionRateD.ToString(),
                    s.RegionName,
                    s.UserCreated,
                    s.DateCreated,
                    s.UserModified,
                    s.DateModified
                }.Any(a => a != null && a.ToLower().Contains(form.SearchValue.ToLower())));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                bool isAsc = form.SortColumnDirection == "asc";
                data = form.SortColumn switch
                {
                    "CommissionRate" => isAsc ? data.OrderBy(s => s.CommissionRateD) : data.OrderByDescending(s => s.CommissionRateD),
                    "DateCreated" => isAsc ? data.OrderBy(s => s.DateCreatedD) : data.OrderByDescending(s => s.DateCreatedD),
                    "DateModified" => isAsc ? data.OrderBy(s => s.DateModifiedD) : data.OrderByDescending(s => s.DateModifiedD),
                    _ => data.OrderBy(form.SortColumn + " " + form.SortColumnDirection)
                };
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
                ivm.UserCreated = await GetNickname(ivm.UserCreated);
                ivm.UserModified = await GetNickname(ivm.UserModified);
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
            areasViewVM.UserCreated = await GetNickname(areasViewVM.UserCreated);
            areasViewVM.UserModified = await GetNickname(areasViewVM.UserModified);
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
            return (await GetAllAreaAsync()).Any(s => s.Code == areaVM.Code
                                                   && s.Name == areaVM.Name
                                                   && s.CommissionRate == areaVM.CommissionRate
                                                   && s.RegionName == areaVM.RegionName);
        }

        public async Task<bool> IsCodeExist(string code)
        {
            return await context.Areas.AnyAsync(c => c.Code == code);
        }
    }
}