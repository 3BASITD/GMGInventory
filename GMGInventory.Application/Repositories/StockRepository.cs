using AutoMapper;
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
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        private readonly string currentUserId;

        public StockRepository(ApplicationDbContext context
                             , IHttpContextAccessor httpContextAccessor
                             , UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
        }

        private async Task<IList<StockVM>> GetStocks()
        {
            var stocks = from stock in context.ItemInventories
                         select new StockVM
                         {
                             ItemId = stock.Item.Id,
                             ItemCode = stock.Item.Code,
                             ItemName = stock.Item.Name,
                             WarehouseId = stock.Warehouse.Id,
                             Warehouse = stock.Warehouse.Name,
                             LocationCodeId = stock.LocationCode == null ? null : stock.LocationCode.Id,
                             LocationCode = stock.LocationCode == null ? null : stock.LocationCode.Name,
                             AllocatedQuantityD = stock.AllocatedQuantity,
                             QuantityD = stock.Quantity
                         };
            return await stocks.ToListAsync();
        }

        public async Task<ForDataTables<StockVM>> GetStocksDT(DataTablesRequestForm form)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetStocks()).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(s => new[]
                {
                    s.ItemCode,
                    s.ItemName,
                    s.Warehouse,
                    s.LocationCode,
                    s.AllocatedQuantityD.ToString(),
                    s.QuantityD.ToString()
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
                    "AllocatedQuantity" => isAsc ? data.OrderBy(s => s.AllocatedQuantityD) : data.OrderByDescending(s => s.AllocatedQuantityD),
                    "Quantity" => isAsc ? data.OrderBy(s => s.QuantityD) : data.OrderByDescending(s => s.QuantityD),
                    _ => data.OrderBy(form.SortColumn + " " + form.SortColumnDirection)
                };
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            //latest sync
            var latestSyncDate = await (from ii in context.ItemInventories
                                        join userc in userManager.Users on ii.UserCreated equals userc.Id
                                        join userm in userManager.Users on ii.UserModified equals userm.Id into um
                                        from userm in um.DefaultIfEmpty()
                                        orderby userm == null ? ii.DateCreated : ii.DateModified descending
                                        where ((userm == null && userc.FirstName == "Sync" && userc.LastName == "Barcotech")
                                        || (userm != null && userm.FirstName == "Sync" && userm.LastName == "Barcotech"))
                                        select userm == null ? ii.DateCreated : ii.DateModified).FirstOrDefaultAsync();
            return new ForDataTables<StockVM>
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