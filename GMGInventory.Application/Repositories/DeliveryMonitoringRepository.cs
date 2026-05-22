using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace GMGInventory.Application.Repositories
{
    public class DeliveryMonitoringRepository : IDeliveryMonitoringRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly string currentUserId;

        public DeliveryMonitoringRepository(ApplicationDbContext context
             , IHttpContextAccessor httpContextAccessor
             , UserManager<User> userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
        }

        private async Task<IList<DeliveryMonitoringVM>> GetListAsync(DeliveryMonitoringFilterVM filter)
        {
            var deliveries = from order in context.SalesOrders
                             join userc in userManager.Users on order.UserCreated equals userc.Id
                             where !string.IsNullOrWhiteSpace(order.ApprovedBy)
                             && filter.DateFrom.Date <= order.SalesOrderDate.Date
                             && order.SalesOrderDate.Date <= filter.DateTo.Date
                             select new DeliveryMonitoringVM
                             {
                                 SalesOrderId = order.Id,
                                 DateOrderD = order.SalesOrderDate,
                                 SalesOrderNumber = order.SalesOrderNumber,
                                 OrderQueueNumber = order.SalesOrderItemSources
                                                         .Select(s => s.SourceOrderNumber)
                                                         .FirstOrDefault(),
                                 ServiceLevelAgreement = order.ServiceLevelAgreement.Name,
                                 Customer = order.Customer.Name,
                                 SOPreparedBy = userc.FirstName + " " + userc.LastName,
                                 SpecialInstruction = order.SpecialInstruction,
                                 SOApprovedBy = order.ApprovedBy + "",
                                 Status1D = order.Status.Name,
                                 Status1Date = order.DateModified == null ? order.DateCreated : order.DateModified.Value
                             };
            return await deliveries.ToListAsync();
        }

        public async Task<ForDataTables<DeliveryMonitoringVM>> GetListDTAsync(DataTablesRequestForm form, DeliveryMonitoringFilterVM filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetListAsync(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(s => new[]
                {
                    s.DateOrder,
                    s.SalesOrderNumber,
                    s.OrderQueueNumber,
                    s.ServiceLevelAgreement,
                    s.Customer,
                    s.SOPreparedBy,
                    s.SpecialInstruction,
                    s.Forwarder,
                    s.SOApprovedBy
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
                    "DateOrder" => isAsc ? data.OrderBy(s => s.DateOrderD) : data.OrderByDescending(s => s.DateOrderD),
                    _ => data.OrderBy(form.SortColumn + " " + form.SortColumnDirection)
                };
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<DeliveryMonitoringVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }
    }
}