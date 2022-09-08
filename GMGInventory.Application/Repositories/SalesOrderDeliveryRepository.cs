using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;

namespace GMGInventory.Application.Repositories
{
    public class SalesOrderDeliveryRepository : ISalesOrderDeliveryRepository
    {
        private readonly ApplicationDbContext context;

        public SalesOrderDeliveryRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        private async Task<IList<SalesOrderDeliveryVM>> GetListAsync(SalesOrderDeliveryFilterVM filter)
        {
            if (filter.DateTo == null)
            {
                if (filter.DateFrom == null) { filter.DateTo = DateTime.Now; }
                else { filter.DateTo = filter.DateFrom.Value.AddYears(1); }
            }
            if (filter.DateFrom == null)
            {
                filter.DateFrom = filter.DateTo.Value.AddYears(-1);
            }

            var salesOrder = from so in context.SalesOrders
                                               .Include(s => s.Customer)
                             join sdm in context.SalesOrderDeliveryMonitorings
                                     .Include(s => s.Forwarder) on so.Id equals sdm.SalesOrderId into monitoring
                             from sdm in monitoring.DefaultIfEmpty()
                             where (filter.AgentId == null || filter.AgentId == so.AgentId)
                                && (filter.CustomerId == null || filter.CustomerId == so.CustomerId)
                                && filter.DateFrom <= so.DeliveryDate
                                && so.DeliveryDate <= filter.DateTo
                             select new SalesOrderDeliveryVM
                             {
                                 CustomerCode = so.Customer.Code,
                                 CustomerName = so.Customer.Name,
                                 DateDeliveredD = so.DeliveryDate,
                                 Forwarder = sdm.Forwarder.Name,
                                 Id = so.Id,
                                 SalesOrderDateD = so.DateCreated,
                                 SalesOrderId = so.Id,
                                 SONumber = so.SalesOrderNumber,
                                 WaybillNumber = sdm.Waybill
                             };

            return await salesOrder.ToListAsync();
        }

        public async Task<ForDataTables<SalesOrderDeliveryVM>> GetListDataTablesAsync(DataTablesRequestForm form, SalesOrderDeliveryFilterVM filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetListAsync(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.CustomerCode.ToLower().Contains(form.SearchValue)
                                    || x.CustomerName.ToLower().Contains(form.SearchValue)
                                    || x.DateDelivered.ToLower().Contains(form.SearchValue)
                                    || x.Forwarder.ToLower().Contains(form.SearchValue)
                                    || x.SalesOrderDate.ToLower().Contains(form.SearchValue)
                                    || x.SONumber.ToLower().Contains(form.SearchValue)
                                    || x.WaybillNumber.ToLower().Contains(form.SearchValue));
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
                        case "CustomerCode": data = data.OrderBy(s => s.CustomerCode); break;
                        case "CustomerName": data = data.OrderBy(s => s.CustomerName); break;
                        case "DateDelivered": data = data.OrderBy(s => s.DateDeliveredD); break;
                        case "Forwarder": data = data.OrderBy(s => s.Forwarder); break;
                        case "SalesOrderDate": data = data.OrderBy(s => s.SalesOrderDateD); break;
                        case "SONumber": data = data.OrderBy(s => s.SONumber); break;
                        case "WaybillNumber": data = data.OrderBy(s => s.WaybillNumber); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "CustomerCode": data = data.OrderByDescending(s => s.CustomerCode); break;
                        case "CustomerName": data = data.OrderByDescending(s => s.CustomerName); break;
                        case "DateDelivered": data = data.OrderByDescending(s => s.DateDeliveredD); break;
                        case "Forwarder": data = data.OrderByDescending(s => s.Forwarder); break;
                        case "SalesOrderDate": data = data.OrderByDescending(s => s.SalesOrderDateD); break;
                        case "SONumber": data = data.OrderByDescending(s => s.SONumber); break;
                        case "WaybillNumber": data = data.OrderByDescending(s => s.WaybillNumber); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<SalesOrderDeliveryVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList
            };
        }
    }
}