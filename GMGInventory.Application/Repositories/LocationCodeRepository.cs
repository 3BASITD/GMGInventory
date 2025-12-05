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
    public class LocationCodeRepository : GenericRepository<LocationCode>, ILocationCodeRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public LocationCodeRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
            , UserManager<User> userManager
            , IMapper mapper) : base(context, httpContextAccessor, userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<IList<JsonNameOptionsModel>> GetOptions(int? IncludeId)
        {
            return await context.LocationCodes
                                .Where(s => s.IsActive || s.Id == IncludeId)
                                .Select(s => new JsonNameOptionsModel
                                {
                                    Id = s.Id.ToString(),
                                    Text = s.Code,
                                    Name = s.Name,
                                    Selected = IncludeId != null && IncludeId == s.Id
                                })
                                .OrderBy(s => s.Text)
                                .ToListAsync();
        }

        public async Task<IList<JsonOptionsModel>> GetOptions2(int? IncludeId, int itemId)
        {
            var itemLocationIds = context.ItemLocations.Where(x => x.ItemId == itemId).Select(x => x.LocationCodeId).ToList();

            return await context.LocationCodes

                .Where(s => !itemLocationIds.Contains(s.Id) && s.IsActive || s.Id == IncludeId)
                .Select(s => new JsonOptionsModel
                {
                    Id = s.Id.ToString(),
                    Text = (s.Code == s.Name ? "" : "(" + s.Code + ") ") + s.Name,
                    Selected = IncludeId != null && IncludeId == s.Id
                })
                .OrderBy(s => s.Text)
                .ToListAsync();
        }

        public async Task<ForDataTables<LocationCodeVM>> GetAllForDataTablesAsync(DataTablesRequestForm form)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetAllLocationCodeAsync()).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.LocationCode.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.LocationName.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.UserCreated.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.DateCreated.ToString().ToLower().Contains(form.SearchValue.ToLower())
                                    || x.UserModified.ToLower().Contains(form.SearchValue.ToLower())
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
                        case "LocationCode": data = data.OrderBy(s => s.LocationCode); break;
                        case "LocationName": data = data.OrderBy(s => s.LocationName); break;
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
                        case "LocationCode": data = data.OrderByDescending(s => s.LocationCode); break;
                        case "LocationName": data = data.OrderByDescending(s => s.LocationName); break;
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
            return new ForDataTables<LocationCodeVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }
        public async Task<IList<LocationCodeVM>> GetAllLocationCodeAsync()
        {

            var locations = from location in context.LocationCodes

                          join userc in userManager.Users on location.UserCreated equals userc.Id
                          join userm in userManager.Users on location.UserModified equals userm.Id into m
                          from userm in m.DefaultIfEmpty()

                          select new LocationCodeVM
                          {
                              Id = location.Id,
                              LocationName = location.Name,
                              LocationCode = location.Code,
                              IsActive = location.IsActive,
                              DateCreatedD = location.DateCreated,
                              DateModifiedD = location.DateModified == null ? null : location.DateModified.Value,
                              UserCreated = SystemFormat.Name(userc.FirstName, userc.LastName, userc.Nickname),
                              UserModified = userm == null ? "-" : SystemFormat.Name(userm.FirstName, userm.LastName, userm.Nickname)
                          };
            return await locations.ToListAsync();
        }

        public async Task<IList<APIGeneric>> GetListAPI()
        {
            var locationCodes = from locationCode in context.LocationCodes
                                where locationCode.IsActive == true
                                select new APIGeneric
                                {
                                    Id = locationCode.Id,
                                    Code = locationCode.Code,
                                    Name = locationCode.Name
                                };
            return await locationCodes.ToListAsync();
        }

        public async Task<bool> IsLocationCodeInUse(int id)
        {
            return await context.ItemTypes.AnyAsync(s => s.Id == id);
        }
        public async Task<bool> IsLocationCodeExist(string code)
        {
            return await context.LocationCodes.AnyAsync(c => c.Code == code);
        }

        public async Task<int> AddAsync(LocationCodeVM LocationCodeVM)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var location = new LocationCode
            {
                Code = LocationCodeVM.LocationCode.Length > 10 ? LocationCodeVM.LocationCode[..10] : LocationCodeVM.LocationCode,
                Name = LocationCodeVM.LocationName.Length > 100 ? LocationCodeVM.LocationName[..100] : LocationCodeVM.LocationName,
                IsActive = true
            };
            await context.LocationCodes.AddAsync(location);
            await context.SaveChangesAsync(currentUserId);

            return location.Id;
        }

        public async Task UpdateAsync(LocationCodeVM LocationCodeVM)
        {
            var location = context.LocationCodes.Where(s => s.Id == LocationCodeVM.Id).FirstOrDefault();
            if (location == null)
            {
                throw new DbUpdateConcurrencyException();
            }
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

            location.Id = LocationCodeVM.Id;
            location.Code = LocationCodeVM.LocationCode.Length > 10 ? LocationCodeVM.Code[..10] : LocationCodeVM.LocationCode;
            location.Name = LocationCodeVM.LocationName.Length > 100 ? LocationCodeVM.Name[..100] : LocationCodeVM.LocationName;
            location.IsActive = true;

            context.LocationCodes.Update(location);
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task<int?> GetIdAsync(string? Name)
        {
            var _name = (Name + "").ToLower().Replace("\n", "").Replace("\r", "").Trim();
            var name = _name.Length > 100 ? _name[..100] : _name;
            var code = _name.Length > 10 ? _name[..10] : _name;
            var locationCode = await context.LocationCodes
                                            .Where(s => s.Name.ToLower().Replace("\n", "").Replace("\r", "").Trim() == name
                                                     || s.Code.ToLower().Replace("\n", "").Replace("\r", "").Trim() == code)
                                            .FirstOrDefaultAsync();
            return locationCode?.Id;
        }
    }
}