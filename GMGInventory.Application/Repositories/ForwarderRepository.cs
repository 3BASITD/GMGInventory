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
    public class ForwarderRepository : GenericRepository<Forwarder>, IForwarderRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public ForwarderRepository(ApplicationDbContext context
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
            return await context.Forwarders
                                .Select(s => new JsonOptionsModel
                                {
                                    Id = s.Id.ToString(),
                                    Text = s.Name,
                                    Selected = IncludeId != null && IncludeId == s.Id
                                })
                                .OrderBy(s => s.Text)
                                .ToListAsync();
        }


        public async Task<IList<ForwarderViewVM>> GetAllForwarderAsync()
        {
            var forwarders = from forward in context.Forwarders
                         join userc in userManager.Users on forward.UserCreated equals userc.Id
                         join userm in userManager.Users on forward.UserModified equals userm.Id into m
                         from userm in m.DefaultIfEmpty()

                         select new ForwarderViewVM
                         {
                             Id = forward.Id,
                             Name = forward.Name,
                             Address = forward.Address,
                             IsActive = forward.IsActive,
                             PhoneNumber = forward.PhoneNumber,
                             FaxNumber = forward.FaxNumber,
                             DateCreatedD = forward.DateCreated,
                             DateModifiedD = forward.DateModified,
                             UserCreated = userc.FirstName + " " + userc.LastName,
                             UserModified = userm == null ? null : (userm.FirstName + " " + userm.LastName)
                         };
            return await forwarders.ToListAsync();
        }

        public async Task<ForDataTables<ForwarderViewVM>> GetAllForDataTablesAsync(DataTablesRequestForm form)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetAllForwarderAsync()).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(s => new[]
                {
                    s.Name,
                    s.Address,
                    s.UserCreated,
                    s.DateCreated,
                    s.UserModified,
                    s.DateModified
                }.Any(a => a != null && a.Trim().ToLower().Contains((form.SearchValue + "").Trim().ToLower())));
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
                        case "Name": data = data.OrderBy(s => s.Name); break;
                        case "Address": data = data.OrderBy(s => s.Address); break;
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
                        case "Name": data = data.OrderByDescending(s => s.Name); break;
                        case "Address": data = data.OrderByDescending(s => s.Address); break;
                        case "UserCreated": data = data.OrderByDescending(s => s.UserCreated); break;
                        case "DateCreated": data = data.OrderByDescending(s => s.DateCreatedD); break;
                        case "UserModified": data = data.OrderByDescending(s => s.UserModified); break;
                        case "DateModified": data = data.OrderByDescending(s => s.DateModifiedD); break;
                        default: break;
                    }
                }
            }
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                bool isAsc = form.SortColumnDirection == "asc";
                data = form.SortColumn switch
                {
                    "DateCreated" => isAsc ? data.OrderBy(s => s.DateCreatedD) : data.OrderByDescending(s => s.DateCreatedD),
                    "DateModified" => isAsc ? data.OrderBy(s => s.DateModifiedD) : data.OrderByDescending(s => s.DateModifiedD),
                    _ => data.OrderBy(form.SortColumn + " " + form.SortColumnDirection)
                };
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<ForwarderViewVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }


        public async Task<bool> UploadCombinationExistAsync(ForwarderVM forwarderVM)
        {
            return (await GetAllForwarderAsync()).Any(s => s.Name == forwarderVM.Name
                                                        && s.Address == forwarderVM.Address 
                                                        && s.PhoneNumber == forwarderVM.PhoneNumber 
                                                        && s.FaxNumber == forwarderVM.FaxNumber);
        }




        public async Task<IList<ForwarderViewVM>> GetAllForwarderVMAsync()
        {
            var forwarders = await context.Forwarders.ToListAsync();
            var forwardersViewVM = mapper.Map<List<ForwarderViewVM>>(forwarders);
            foreach (ForwarderViewVM ivm in forwardersViewVM)
            {
                ivm.UserCreated = await GetNickname(ivm.UserCreated);
                ivm.UserModified = await GetNickname(ivm.UserModified);
            }
            return forwardersViewVM;
        }

        public async Task<IList<APIGeneric>> GetListAPI()
        {
            var forwarders = from forwarder in context.Forwarders
                             where forwarder.IsActive == true
                             select new APIGeneric
                             {
                                 Id = forwarder.Id,
                                 Name = forwarder.Name
                             };
            return await forwarders.ToListAsync();
        }

        public async Task<ForwarderViewVM?> GetForwarderViewVMAsync(int? id)
        {
            var forwarders = await context.Forwarders
                .Where(s => s.Id == id).FirstOrDefaultAsync();
            if (forwarders == null)
            {
                return null;
            }
            var forwardersViewVM = mapper.Map<ForwarderViewVM>(forwarders);
            forwardersViewVM.UserCreated = await GetNickname(forwardersViewVM.UserCreated);
            forwardersViewVM.UserModified = await GetNickname(forwardersViewVM.UserModified);
            return forwardersViewVM;
        }

        public async Task<ForwarderVM?> GetForwarderVMAsync(int? id)
        {
            var forwarders = await context.Forwarders
                .Where(s => s.Id == id).FirstOrDefaultAsync();
            if (forwarders == null)
            {
                return null;
            }
            var forwardersViewVM = mapper.Map<ForwarderVM>(forwarders);
            return forwardersViewVM;
        }

        public async Task<IList<Forwarder>> GetAllActiveForwarderAsync(int includeId)
        {
            //return await context.Suppliers.Where(s => s.IsActive || s.Id == includeId).ToListAsync();
            var forwarders = await context.Forwarders.ToListAsync();
            return forwarders;
        }

        public async Task<bool> IsForwarderCodeExist(string code)
        {
            return await context.Forwarders.AnyAsync(c => c.Name == code);
        }

        public async Task<bool> IsForwarderInUse(int id)
        {
            return await context.SalesOrders.AnyAsync(s => s.ForwarderId == id);
        }

        public async Task<bool> IsForwarderIdExistAsync(int id)
        {
            return await context.Forwarders.AnyAsync(s => s.Id == id);
        }

        public async Task DeactivateForwarderAsync(int id)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var forwarders = await context.Forwarders.FirstAsync(s => s.Id == id);
            forwarders.IsActive = false;
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task ReactivateForwarderAsync(int id)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var forwarders = await context.Forwarders.FirstAsync(s => s.Id == id);
            forwarders.IsActive = true;
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task AddAsync(ForwarderVM forwarderVM)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

            var forwarder = new Forwarder
            {
                Name = forwarderVM.Name,
                Address = forwarderVM.Address + "",
                PhoneNumber = forwarderVM.PhoneNumber + "",
                FaxNumber = forwarderVM.FaxNumber + "",
                IsActive = true
            };
            await context.Forwarders.AddAsync(forwarder);
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task UpdateAsync(ForwarderVM forwarderVM)
        {
            var forwarder = context.Forwarders.Where(s => s.Id == forwarderVM.Id).FirstOrDefault();
            if (forwarder == null)
            {
                throw new DbUpdateConcurrencyException();
            }

            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            //forwarder.Id = forwarderVM.Id;
            forwarder.Name = forwarderVM.Name;
            forwarder.Address = forwarderVM.Address + "";
            forwarder.PhoneNumber = forwarderVM.PhoneNumber + "";
            forwarder.FaxNumber = forwarderVM.FaxNumber + "";
            forwarder.IsActive = true;

            context.Forwarders.Update(forwarder);
            await context.SaveChangesAsync(currentUserId);
        }


    }
}