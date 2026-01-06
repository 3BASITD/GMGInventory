using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace GMGInventory.Application.Repositories
{
    public class SyncRepository : ISyncRepository
    {
        private readonly ApplicationDbContext context;

        public SyncRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<ResponseModel> SaveBarcotechStatus(BarcotechSync sync)
        {
            var syncStatus = await context.BarcotechSyncs
                                          .Where(s => s.ModuleName == sync.ModuleName 
                                                   && s.PrimaryKey == sync.PrimaryKey)
                                          .FirstOrDefaultAsync();
            await context.BarcotechSyncs.AddAsync(sync);
            await context.SaveChangesAsync();
            return new ResponseModel();
        }

        private async Task<IList<BarcotechSyncVM>> GetBarcotechStatusListAsync()
        {
            var syncStatus = from sync in context.BarcotechSyncs
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

        public async Task<ForDataTables<BarcotechSyncVM>> GetBarcotechStatusListDTAsync(DataTablesRequestForm form)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetBarcotechStatusListAsync()).AsQueryable();
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