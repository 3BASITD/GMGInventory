using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Application.Repositories
{
    public class SalesOrderDeliveryRepository : ISalesOrderDeliveryRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;

        public SalesOrderDeliveryRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
            , UserManager<User> userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        private async Task<IList<SalesOrderDeliveryVM>> GetListAsync(SalesOrderDeliveryFilterVM filter)
        {
            var salesOrder = from so in context.SalesOrders
                                               .Include(s => s.Customer)
                             join sdm in context.SalesOrderDeliveryMonitorings
                                                .Include(s => s.Forwarder) on so.Id equals sdm.SalesOrderId into monitoring
                             from sdm in monitoring.DefaultIfEmpty()
                             where (filter.AgentId == null || filter.AgentId == so.AgentId)
                                && (filter.CustomerId == null || filter.CustomerId == so.CustomerId)
                                && filter.DateFrom <= (sdm.DateModified ?? sdm.DateCreated)
                                && (sdm.DateModified ?? sdm.DateCreated) <= filter.DateTo
                             select new SalesOrderDeliveryVM
                             {
                                 CustomerCode = so.Customer.Code,
                                 CustomerName = so.Customer.Name,
                                 DateDeliveredD = sdm.DateModified ?? sdm.DateCreated,
                                 Forwarder = sdm == null ? "-" : sdm.Forwarder.Name,
                                 ForwarderId = sdm == null ? 0 : sdm.ForwarderId,
                                 Id = so.Id,
                                 Remarks = sdm == null || sdm.Remarks.Trim() == "" ? "-" : sdm.Remarks,
                                 SalesOrderDateD = so.DateCreated,
                                 SalesOrderId = so.Id,
                                 SONumber = so.SalesOrderNumber,
                                 WaybillNumber = sdm == null ? "-" : sdm.Waybill
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
                                    || x.Remarks.ToLower().Contains(form.SearchValue)
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
                        case "Remarks": data = data.OrderBy(s => s.Remarks); break;
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
                        case "Remarks": data = data.OrderByDescending(s => s.Remarks); break;
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

        public async Task<string> EditAsync(SalesOrderDeliveryFormVM form)
        {
            if (await context.SalesOrders.AnyAsync(s => s.Id == form.SalesOrderId))
            {
                string message = "";
                string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
                var delivery = await context.SalesOrderDeliveryMonitorings.Where(s => s.SalesOrderId == form.SalesOrderId).FirstOrDefaultAsync();
                if (delivery == null)
                {
                    delivery = new SalesOrderDeliveryMonitoring()
                    {
                        SalesOrderId = form.SalesOrderId,
                        ForwarderId = form.Forwarder,
                        Waybill = form.Waybill,
                        Remarks = form.Remarks + ""
                    };
                    await context.SalesOrderDeliveryMonitorings.AddAsync(delivery);
                    message = "Delivery details added successfully";
                }
                else
                {
                    delivery.ForwarderId = form.Forwarder;
                    delivery.Waybill = form.Waybill;
                    delivery.Remarks = form.Remarks + "";
                    context.SalesOrderDeliveryMonitorings.Update(delivery);
                    message = "Delivery details updated successfully";
                }

                return message;
            }
            else
            {
                return "Invalid";
            }
        }
    }
}