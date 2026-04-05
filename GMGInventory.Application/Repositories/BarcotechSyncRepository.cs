using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace GMGInventory.Application.Repositories
{
    public class BarcotechSyncRepository : IBarcotechSyncRepository
    {
        private readonly ApplicationDbContext context;

        public BarcotechSyncRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        private async Task<IList<BarcotechSyncVM>> GetBarcotechStatusListAsync(BarcotechSyncFilter filter)
        {
            var syncStatus = from sync in context.BarcotechSyncs.AsNoTracking()
                             where (filter.Module == null || sync.ModuleName.Trim().ToLower() == filter.Module.Trim().ToLower())
                             && ((filter.IncludeSuccess == true && sync.SyncStatus.Trim().ToLower() == "success")
                             || (filter.IncludeFail == true && sync.SyncStatus.Trim().ToLower() == "fail")
                             || (filter.IncludeException == true && sync.SyncStatus.Trim().ToLower() == "exception"))
                             select new BarcotechSyncVM
                             {
                                 Id = sync.Id,
                                 ModuleName = sync.ModuleName,
                                 PrimaryKey = sync.PrimaryKey,
                                 SyncStatus = sync.SyncStatus,
                                 Remarks = sync.Remarks,
                                 DatePerformedD = sync.DatePerformed
                             };
            return await syncStatus.ToListAsync();
        }

        public async Task<ForDataTables<BarcotechSyncVM>> GetBarcotechStatusListDTAsync(DataTablesRequestForm form, BarcotechSyncFilter filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetBarcotechStatusListAsync(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(s => new[]
                {
                    s.ModuleName,
                    s.PrimaryKey,
                    s.SyncStatus,
                    s.Remarks + "",
                    s.DatePerformed
                }.Any(a => a != null && a.ToLower().Contains(form.SearchValue.ToLower())));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                if (form.SortColumn.ToLower().Contains("date"))
                {
                    if (form.SortColumnDirection == "asc")
                    {
                        switch (form.SortColumn)
                        {
                            case "DatePerformed": data = data.OrderBy(s => s.DatePerformedD); break;
                            default: break;
                        }
                    }
                    else if (form.SortColumnDirection == "desc")
                    {
                        switch (form.SortColumn)
                        {
                            case "DatePerformed": data = data.OrderByDescending(s => s.DatePerformedD); break;
                            default: break;
                        }
                    }
                }
                else
                {
                    data = data.OrderBy(form.SortColumn + " " + form.SortColumnDirection);
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<BarcotechSyncVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList
            };
        }
    }
}