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
    public class ReasonOfReturnRepository : GenericRepository<ReasonOfReturn>, IReasonOfReturnRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public ReasonOfReturnRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
            , UserManager<User> userManager
            , IMapper mapper) : base(context, httpContextAccessor, userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<ForDataTables<ReasonOfReturnVM>> GetAllForDataTablesAsync(DataTablesRequestForm form)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetAllReasonOfReturnAsync()).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.ReasonOfReturnCode.ToLower().Contains(form.SearchValue)
                                    || x.ReasonOfReturnName.ToLower().Contains(form.SearchValue)
                                    || x.UserCreated.ToLower().Contains(form.SearchValue)
                                    || x.DateCreated.ToString().ToLower().Contains(form.SearchValue)
                                    || x.UserModified.ToLower().Contains(form.SearchValue)
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
                        case "ReasonOfReturnCode": data = data.OrderBy(s => s.ReasonOfReturnCode); break;
                        case "ReasonOfReturnName": data = data.OrderBy(s => s.ReasonOfReturnName); break;
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
                        case "ReasonOfReturnCode": data = data.OrderByDescending(s => s.ReasonOfReturnCode); break;
                        case "ReasonOfReturnName": data = data.OrderByDescending(s => s.ReasonOfReturnName); break;
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
            return new ForDataTables<ReasonOfReturnVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }
        public async Task<IList<ReasonOfReturnVM>> GetAllReasonOfReturnAsync()
        {

            var reasons = from reason in context.ReasonOfReturns

                             join userc in userManager.Users on reason.UserCreated equals userc.Id
                             join userm in userManager.Users on reason.UserModified equals userm.Id into m
                             from userm in m.DefaultIfEmpty()

                             select new ReasonOfReturnVM
                             {
                                 Id = reason.Id,
                                 ReasonOfReturnName = reason.Name,
                                 ReasonOfReturnCode = reason.Code,
                                 IsActive = reason.IsActive,
                                 DateCreatedD = reason.DateCreated,
                                 DateModifiedD = reason.DateModified == null ? null : reason.DateModified.Value,
                                 UserCreated = SystemFormat.Name(userc.FirstName, userc.LastName, userc.Nickname),
                                 UserModified = userm == null ? "-" : SystemFormat.Name(userm.FirstName, userm.LastName, userm.Nickname)
                             };
            return await reasons.ToListAsync();
        }

        public async Task<bool> IsReasonOfReturnInUse(int id)
        {
            return await context.ItemTypes.AnyAsync(s => s.Id == id);
        }
        public async Task<bool> IsReasonOfReturnCodeExist(string code)
        {
            return await context.ReasonOfReturns.AnyAsync(c => c.Code == code);
        }

        public async Task AddAsync(ReasonOfReturnVM reasonOfReturnVM)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var reason = new ReasonOfReturn
            {
                Code = reasonOfReturnVM.Code,
                Name = reasonOfReturnVM.Name,
                IsActive = true
            };
            await context.ReasonOfReturns.AddAsync(reason);
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task UpdateAsync(ReasonOfReturnVM reasonOfReturnVM)
        {
            var reason = context.ReasonOfReturns.Where(s => s.Id == reasonOfReturnVM.Id).FirstOrDefault();
            if (reason == null)
            {
                throw new DbUpdateConcurrencyException();
            }
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            reason.Id = reasonOfReturnVM.Id;

            reason.Code = reasonOfReturnVM.Code;
            reason.Name = reasonOfReturnVM.Name;
            reason.IsActive = true;
            context.ReasonOfReturns.Update(reason);
            await context.SaveChangesAsync(currentUserId);
        }


    }
}