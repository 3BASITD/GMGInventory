using GMGInventory.Application.Contracts;
using GMGInventory.Application.Helpers;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace GMGInventory.Application.Repositories
{
    public class OrderRecommendationRepository : IOrderRecommendationRepository
    {
        private readonly ApplicationDbContext context;
        private readonly string currentUser;

        public OrderRecommendationRepository(ApplicationDbContext context
                                  , IHttpContextAccessor httpContextAccessor
                                  , UserManager<User> userManager)
        {
            this.context = context;
            this.currentUser = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
        }

        private async Task<IList<OrderRecommendationVM>> GetOrderRecommendations(OrderRecommendationFilterVM filter)
        {
            if (filter.DateFrom == null) filter.DateFrom = DateTime.Today.AddDays(-6);
            if (filter.DateTo == null) filter.DateTo = DateTime.Today;
            if (filter.DateFrom > filter.DateTo) (filter.DateFrom, filter.DateTo) = (filter.DateTo, filter.DateFrom);

            var dtNow = DateTime.Today;

            var lastWeekStart = dtNow.AddDays(-7);
            var lastMonthStart = dtNow.AddDays(-30);
            var last2MonthsStart = dtNow.AddDays(-60);
            var lastYearStart = dtNow.AddDays(-365);

            var quantities = await (from ii in context.ItemInventories
                                    join item in context.Items on ii.ItemId equals item.Id
                                    join icls in context.ItemClasses on item.ItemClassId equals icls.Id into gicls
                                    from icls in gicls.DefaultIfEmpty()
                                    join whse in context.Warehouses on ii.WarehouseId equals whse.Id
                                    join loc in context.LocationCodes on ii.LocationCodeId equals loc.Id into gloc
                                    from loc in gloc.DefaultIfEmpty()
                                    join ili in context.InvoiceLineItems on ii.ItemId equals ili.ItemId into gili
                                    from ili in gili.DefaultIfEmpty()
                                    join inv in context.Invoices on ili.InvoiceId equals inv.Id into ginv
                                    from inv in ginv.DefaultIfEmpty()
                                    where (inv == null || inv.Status.Name == "INVOICE DRAFT" || inv.Status.Name == "INVOICE SUBMITTED")
                                    && (inv == null || inv.Pack.WarehouseId == ii.WarehouseId)
                                    && (filter.ItemClassId == null || item.ItemClassId == filter.ItemClassId)
                                    && (filter.BrandId == null || item.BrandId == filter.BrandId)
                                    group new { ili, inv } by new
                                    {
                                        ii.ItemId,
                                        ItemCode = item.Code,
                                        ItemName = item.Name,
                                        item.PartNumber,
                                        ItemClassId = icls == null ? (int?)null : icls.Id,
                                        ItemClass = icls == null ? null : icls.Name,
                                        WarehouseId = whse.Id,
                                        Warehouse = whse.Name,
                                        LocationCodeId = loc == null ? (int?)null : loc.Id,
                                        LocationCode = loc == null ? null : loc.Name,
                                        Stock = ii.Quantity,
                                        item.BeginningCost
                                    } into g
                                    select new OrderRecommendationVM
                                    {
                                        ItemId = g.Key.ItemId,
                                        ItemCode = g.Key.ItemCode,
                                        ItemName = g.Key.ItemName,
                                        PartNumber = g.Key.PartNumber,
                                        ItemClassId = g.Key.ItemClassId,
                                        ItemClass = g.Key.ItemClass,
                                        WarehouseId = g.Key.WarehouseId,
                                        Warehouse = g.Key.Warehouse,
                                        LocationCodeId = g.Key.LocationCodeId,
                                        LocationCode = g.Key.LocationCode,
                                        StockOnHandD = g.Key.Stock,
                                        LastWeekQuantityD = g.Where(s => s.inv != null && s.inv.InvoiceDate >= lastWeekStart).Sum(s => (int?)s.ili.Quantity) ?? 0,
                                        LastMonthQuantityD = g.Where(s => s.inv != null && s.inv.InvoiceDate >= lastMonthStart).Sum(s => (int?)s.ili.Quantity) ?? 0,
                                        Last2MonthsQuantityD = g.Where(s => s.inv != null && s.inv.InvoiceDate >= last2MonthsStart).Sum(s => (int?)s.ili.Quantity) ?? 0,
                                        LastYearQuantityD = g.Where(s => s.inv != null && s.inv.InvoiceDate >= lastYearStart).Sum(s => (int?)s.ili.Quantity) ?? 0,
                                        OverallQuantityD = g.Sum(s => (int?)s.ili.Quantity) ?? 0,
                                        BeginningCostD = g.Key.BeginningCost
                                    }).OrderBy(s => s.ItemCode).ToListAsync();

            quantities = quantities.Where(s => filter.MovementCategory == null || s.MovementCategory == filter.MovementCategory).ToList();

            for (int i = 0; i < quantities.Count; i++)
            {
                quantities[i].ItemNumberD = i + 1;
            }

            return quantities;
        }

        public async Task<ForDataTables<OrderRecommendationVM>> GetOrderRecommendationsDT(DataTablesRequestForm form, OrderRecommendationFilterVM filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetOrderRecommendations(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(s => new[]
                {
                    s.ItemNumberD.ToString(),
                    s.ItemCode,
                    s.ItemName
                }.Any(a => a != null && a.Trim().ToLower().Contains((form.SearchValue + "").Trim().ToLower())));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                bool isAsc = form.SortColumnDirection == "asc";
                data = form.SortColumn switch
                {
                    "ItemNumber" => isAsc ? data.OrderBy(s => s.ItemNumberD) : data.OrderByDescending(s => s.ItemNumberD),
                    _ => data.OrderBy(form.SortColumn + " " + form.SortColumnDirection)
                };
            }
            //pagination
            var pagedList = data.Skip(form.Skip).Take(form.PageSize).ToList();

            // loads data for selected page
            var itemsOnPagedList = pagedList.Select(s => s.ItemId).ToList();

            var latestCost = from poli in context.PurchaseOrderNewLineItems
                             where itemsOnPagedList.Contains(poli.RequestItem.Item.Id)
                             && poli.PurchaseOrder.Status.Code.Trim().ToLower() == "approved"
                             group poli by poli.RequestItem.Item.Id into g
                             select new
                             {
                                 ItemId = g.Key,
                                 g.OrderByDescending(s => s.PurchaseOrder.OrderDate).First().UnitCost
                             };

            var latestCostList = latestCost.ToList();
            var latestCostDict = latestCostList.ToDictionary(x => x.ItemId, x => x.UnitCost);
            foreach (var pl in pagedList)
            {
                var cost = latestCostDict.GetValueOrDefault(pl.ItemId);
                pl.LatestOrderCostD = cost;
            }

            return new ForDataTables<OrderRecommendationVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pagedList
            };
        }

        public async Task<IList<OrderRecommendationHistoryVM>> GetPurchaseHistories()
        {
            var orders = from order in context.PurchaseOrderNews
                         where order.Status.Name == "APPROVED"
                         group order by order.OrderDate.Year into g
                         select new OrderRecommendationHistoryVM
                         {
                             Year = g.Key,

                             JanuaryD = g.Count(s => s.OrderDate.Month == 1),
                             FebruaryD = g.Count(s => s.OrderDate.Month == 2),
                             MarchD = g.Count(s => s.OrderDate.Month == 3),
                             AprilD = g.Count(s => s.OrderDate.Month == 4),
                             MayD = g.Count(s => s.OrderDate.Month == 5),
                             JuneD = g.Count(s => s.OrderDate.Month == 6),
                             JulyD = g.Count(s => s.OrderDate.Month == 7),
                             AugustD = g.Count(s => s.OrderDate.Month == 8),
                             SeptemberD = g.Count(s => s.OrderDate.Month == 9),
                             OctoberD = g.Count(s => s.OrderDate.Month == 10),
                             NovemberD = g.Count(s => s.OrderDate.Month == 11),
                             DecemberD = g.Count(s => s.OrderDate.Month == 12),

                             AverageD = (decimal)Math.Round(g.Count() / 12.0, 2)
                         };
            return await orders.OrderBy(s => s.Year).ToListAsync();
        }

        public async Task<IList<OrderRecommendationHistoryVM>> GetIssuanceHistories()
        {
            var invoices = from invoice in context.Invoices
                           where invoice.Status.Name == "INVOICE DRAFT" || invoice.Status.Name == "INVOICE SUBMITTED"
                           group invoice by invoice.InvoiceDate.Year into g
                           select new OrderRecommendationHistoryVM
                           {
                               Year = g.Key,

                               JanuaryD = g.Count(s => s.InvoiceDate.Month == 1),
                               FebruaryD = g.Count(s => s.InvoiceDate.Month == 2),
                               MarchD = g.Count(s => s.InvoiceDate.Month == 3),
                               AprilD = g.Count(s => s.InvoiceDate.Month == 4),
                               MayD = g.Count(s => s.InvoiceDate.Month == 5),
                               JuneD = g.Count(s => s.InvoiceDate.Month == 6),
                               JulyD = g.Count(s => s.InvoiceDate.Month == 7),
                               AugustD = g.Count(s => s.InvoiceDate.Month == 8),
                               SeptemberD = g.Count(s => s.InvoiceDate.Month == 9),
                               OctoberD = g.Count(s => s.InvoiceDate.Month == 10),
                               NovemberD = g.Count(s => s.InvoiceDate.Month == 11),
                               DecemberD = g.Count(s => s.InvoiceDate.Month == 12),

                               AverageD = (decimal)Math.Round(g.Count() / 12.0, 2)
                           };
            return await invoices.OrderBy(s => s.Year).ToListAsync();
        }

        public async Task<OrderRecommendationNewVM> AddRequestAsync(IList<OrderRecommendationNewItemsVM> recos)
        {
            var requestId = 0;
            if (recos.Count == 0) return new OrderRecommendationNewVM { Status = "Nothing to add" };

            var requestitems = new List<PurchaseRequestLineItem>();

            var itemIds = recos.Select(s => s.ItemId).Distinct().ToList();
            var items = await context.Items
                                     .Where(s => itemIds.Contains(s.Id))
                                     .ToDictionaryAsync(s => s.Id);
            foreach (var reco in recos)
            {
                if (!items.TryGetValue(reco.ItemId, out var item))
                {
                    reco.Status = "Invalid item, record cannot be found";
                    continue;
                }

                reco.ItemCode = item.Code;
                reco.ItemName = item.Name;
                reco.PartNumber = item.PartNumber;

                if (reco.Quantity == 0)
                {
                    reco.Status = "Cannot request a 0 quantity";
                    continue;
                }
                if (reco.Quantity == null || reco.Quantity <= 0)
                {
                    reco.Status = reco.Quantity == 0
                                ? "Cannot request a 0 quantity"
                                : "Invalid quantity";
                    continue;
                }

                requestitems.Add(new PurchaseRequestLineItem
                {
                    Item = item,
                    ItemId = item.Id,
                    ItemDescription = item.Name,
                    Quantity = reco.Quantity.Value,
                    UnitId = item.UnitId,
                    Purpose = ""
                });

                reco.Status = "Added to request";
            }

            try
            {
                var status = await context.PurchaseStatuses
                                          .Where(s => s.Name.Trim().ToLower() == "draft")
                                          .FirstAsync();
                var type = await context.PurchaseRequestTypes
                                        .Where(s => s.Name.Trim().ToLower() == "automotive")
                                        .FirstAsync();
                var purpose = await context.PurchaseRequestPurposes
                                           .Where(s => s.Name.Trim().ToLower() == "stocking")
                                           .FirstAsync();

                var procurementHelper = new ProcurementHelper();
                var request = new PurchaseRequest
                {
                    DocumentNumber = await procurementHelper.GetNewRequest(context, type.Code, status.Name),
                    TypeId = type.Id,
                    PurposeId = purpose.Id,
                    RequestDate = DateTime.Today,
                    Remarks = "Order Recommendation",
                    StatusId = status.Id
                };
                await context.PurchaseRequests.AddAsync(request);
                await context.SaveChangesAsync(currentUser);
                requestId = request.Id;

                var ctr = 1;
                foreach (var requestitem in requestitems.OrderBy(s => s.Item.Code))
                {
                    requestitem.PurchaseRequestId = request.Id;
                    requestitem.LineNumber = ctr;
                    ctr++;
                }
                await context.PurchaseRequestLineItems.AddRangeAsync(requestitems);
                await context.SaveChangesAsync(currentUser);

                return new OrderRecommendationNewVM
                {
                    PurchaseRequestId = request.Id,
                    DocumentNumber = request.DocumentNumber,
                    Status = "Successfully created request",
                    Items = recos
                };
            }
            catch (Exception ex)
            {
                var request = await context.PurchaseRequests.Where(s => s.Id == requestId).FirstOrDefaultAsync();
                if (request != null)
                {
                    context.PurchaseRequests.Remove(request);
                    await context.SaveChangesAsync(currentUser);
                }

                return new OrderRecommendationNewVM
                {
                    Status = SystemFormat.GetInnermostExceptionMessage(ex),
                    Items = recos
                };
            }
        }
    }
}