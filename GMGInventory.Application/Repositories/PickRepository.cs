using DocumentFormat.OpenXml.Drawing.Charts;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using GMGInventory.Data.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace GMGInventory.Application.Repositories
{
    public class PickRepository : IPickRepository
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;
        private readonly string currentUserId;

        public PickRepository(ApplicationDbContext context
                            , IHttpContextAccessor httpContextAccessor
                            , UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
        }

        private async Task<IList<PickVM>> GetListAsync()
        {
            var picks = from pick in context.Picks
                        join userc in userManager.Users on pick.UserCreated equals userc.Id
                        join userm in userManager.Users on pick.UserModified equals userm.Id into um
                        from user in um.DefaultIfEmpty()
                        select new PickVM
                        {
                            Id = pick.Id,
                            SalesOrderId = pick.SalesOrder.Id,
                            SalesOrderNumber = pick.SalesOrder.SalesOrderNumber,
                            Status = pick.Status,
                            PickedDateD = pick.PickDate,
                            SyncDateD = pick.DateModified == null ? pick.DateCreated : pick.DateModified.Value
                        };

            return await picks.ToListAsync();
        }

        public async Task<ForDataTables<PickVM>> GetListDTAsync(DataTablesRequestForm form)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetListAsync()).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(s => new[]
                {
                    s.SalesOrderNumber,
                    s.Status,
                    s.PickedDate,
                    s.SyncDate
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
                    "PickedDate" => isAsc ? data.OrderBy(s => s.PickedDateD) : data.OrderByDescending(s => s.PickedDateD),
                    "SyncDate" => isAsc ? data.OrderBy(s => s.SyncDateD) : data.OrderByDescending(s => s.SyncDateD),
                    _ => data.OrderBy(form.SortColumn + " " + form.SortColumnDirection)
                };
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            //latest sync
            var latestSyncDate = await (from pick in context.Picks
                                        join userc in userManager.Users on pick.UserCreated equals userc.Id
                                        join userm in userManager.Users on pick.UserModified equals userm.Id into um
                                        from userm in um.DefaultIfEmpty()
                                        orderby userm == null ? pick.DateCreated : pick.DateModified descending
                                        where ((userm == null && userc.FirstName == "Sync" && userc.LastName == "Barcotech")
                                        || (userm != null && userm.FirstName == "Sync" && userm.LastName == "Barcotech"))
                                        select userm == null ? pick.DateCreated : pick.DateModified).FirstOrDefaultAsync();
            return new ForDataTables<PickVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList,
                AdditionalData = latestSyncDate == null ? "Not auto synched yet" : "Last Auto Sync: " + latestSyncDate.Value.ToString(SystemFormat.StringDateTime)
            };
        }
    }
}