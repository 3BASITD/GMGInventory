using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using GMGInventory.Data.Migrations;
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

        public async Task<IList<SalesOrderDeliveryVM>> GetListAsync(SalesOrderDeliveryFilterVM filter)
        {
            var salesOrder = from so in context.SalesOrders
                                               .Include(s => s.Customer)
                             join sdm in context.SalesOrderDeliveryMonitorings
                                                .Include(s => s.Forwarder) on so.Id equals sdm.SalesOrderId into monitoring
                             from sdm in monitoring.DefaultIfEmpty()
                             join userc in userManager.Users on sdm.UserCreated equals userc.Id
                             join userm in userManager.Users on sdm.UserModified equals userm.Id into m
                             from userm in m.DefaultIfEmpty()
                             where (filter.AgentId == null || filter.AgentId == so.AgentId)
                                && (filter.CustomerId == null || filter.CustomerId == so.CustomerId)
                                && (filter.RegionId == null || filter.RegionId == so.Customer.Area.RegionId)
                                && filter.DateFrom <= so.DateCreated.Date
                                && so.DateCreated.Date <= filter.DateTo
                             select new SalesOrderDeliveryVM
                             {
                                 CreatedBy = userc.FirstName + " " + userc.LastName,
                                 CreatedDateD = sdm.DateCreated,
                                 CustomerCode = so.Customer.Code,
                                 CustomerName = so.Customer.Name,
                                 CustomerAddress = so.Customer.ShippingAddresses
                                                  .Where(s => s.IsDefault == true)
                                                  .Select(s => s.Address)
                                                  .FirstOrDefault() ?? "N/A",
                                 DateDeliverD = sdm.DateDeliver,
                                 DatePickupD = sdm.DatePickup,
                                 Forwarder = sdm.Forwarder.Name,
                                 ForwarderId = sdm.ForwarderId,
                                 Id = so.Id,
                                 ModifiedBy = userm == null ? null : userm.FirstName + " " + userm.LastName,
                                 ModifiedDateD = sdm.DateModified,
                                 PONumber = so.PurchaseOrder,
                                 Remarks = string.IsNullOrWhiteSpace(sdm.Remarks) ? null : sdm.Remarks,
                                 SalesOrderDateD = so.DateCreated,
                                 SalesOrderId = so.Id,
                                 SONumber = so.SalesOrderNumber,
                                 WaybillNumber = string.IsNullOrWhiteSpace(sdm.Waybill) ? null : sdm.Waybill,
                                 ServiceLevelAgreementD = sdm.ServiceLevelAgreement,
                                 SalesOrderStatus = so.Status.Code,
                                 RequiredDeliveryDateD = so.RequiredDeliveryDate,
                                 Representative = string.IsNullOrWhiteSpace(sdm.Representative) ? null : sdm.Representative,
                                 DateReceiveD = sdm.DateReceive
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
                data = data.Where(x => x.CustomerCode.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.CustomerName.ToLower().Contains(form.SearchValue.ToLower())
                                    || (x.DateDeliver + "").ToLower().Contains(form.SearchValue.ToLower())
                                    || (x.DatePickup + "").ToLower().Contains(form.SearchValue.ToLower())
                                    || (x.Forwarder + "").ToLower().Contains(form.SearchValue.ToLower())
                                    || (x.Remarks + "").ToLower().Contains(form.SearchValue.ToLower())
                                    || (x.PONumber + "").ToLower().Contains(form.SearchValue.ToLower())
                                    || x.SalesOrderDate.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.SONumber.ToLower().Contains(form.SearchValue.ToLower())
                                    || (x.WaybillNumber + "").ToLower().Contains(form.SearchValue.ToLower())
                                    || (x.ServiceLevelAgreement + "").ToLower().Contains(form.SearchValue.ToLower())
                                    || x.SalesOrderStatus.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.RequiredDeliveryDate.ToLower().Contains(form.SearchValue.ToLower())
                                    || (x.Representative + "").ToLower().Contains(form.SearchValue.ToLower())
                                    || (x.DateReceive + "").ToLower().Contains(form.SearchValue.ToLower()));
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
                        case "DateDeliver": data = data.OrderBy(s => s.DateDeliverD); break;
                        case "DatePickup": data = data.OrderBy(s => s.DatePickupD); break;
                        case "Forwarder": data = data.OrderBy(s => s.Forwarder); break;
                        case "PONumber": data = data.OrderBy(s => s.PONumber); break;
                        case "Remarks": data = data.OrderBy(s => s.Remarks); break;
                        case "SalesOrderDate": data = data.OrderBy(s => s.SalesOrderDateD); break;
                        case "SONumber": data = data.OrderBy(s => s.SONumber); break;
                        case "WaybillNumber": data = data.OrderBy(s => s.WaybillNumber); break;
                        case "ServiceLevelAgreement": data = data.OrderBy(s => s.ServiceLevelAgreementD); break;
                        case "SalesOrderStatus": data = data.OrderBy(s => s.SalesOrderStatus); break;
                        case "RequiredDeliveryDate": data = data.OrderBy(s => s.RequiredDeliveryDateD); break;
                        case "Representative": data = data.OrderBy(s => s.Representative); break;
                        case "DateReceive": data = data.OrderBy(s => s.DateReceiveD); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "CustomerCode": data = data.OrderByDescending(s => s.CustomerCode); break;
                        case "CustomerName": data = data.OrderByDescending(s => s.CustomerName); break;
                        case "DateDeliver": data = data.OrderByDescending(s => s.DateDeliverD); break;
                        case "DatePickup": data = data.OrderByDescending(s => s.DatePickupD); break;
                        case "Forwarder": data = data.OrderByDescending(s => s.Forwarder); break;
                        case "PONumber": data = data.OrderByDescending(s => s.PONumber); break;
                        case "Remarks": data = data.OrderByDescending(s => s.Remarks); break;
                        case "SalesOrderDate": data = data.OrderByDescending(s => s.SalesOrderDateD); break;
                        case "SONumber": data = data.OrderByDescending(s => s.SONumber); break;
                        case "WaybillNumber": data = data.OrderByDescending(s => s.WaybillNumber); break;
                        case "ServiceLevelAgreement": data = data.OrderByDescending(s => s.ServiceLevelAgreementD); break;
                        case "SalesOrderStatus": data = data.OrderByDescending(s => s.SalesOrderStatus); break;
                        case "RequiredDeliveryDate": data = data.OrderByDescending(s => s.RequiredDeliveryDateD); break;
                        case "Representative": data = data.OrderByDescending(s => s.Representative); break;
                        case "DateReceive": data = data.OrderByDescending(s => s.DateReceiveD); break;
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
                        DateDeliver = form.DateDeliver,
                        DatePickup = form.DatePickup,
                        SalesOrderId = form.SalesOrderId,
                        ForwarderId = form.Forwarder,
                        Waybill = form.Waybill,
                        Remarks = form.Remarks + "",
                        ServiceLevelAgreement = form.ServiceLevelAgreement,
                        Representative = form.Representative,
                        DateReceive = form.DateReceive
                    };
                    await context.SalesOrderDeliveryMonitorings.AddAsync(delivery);
                    message = "Delivery details added successfully";
                }
                else
                {
                    delivery.DateDeliver = form.DateDeliver;
                    delivery.DatePickup = form.DatePickup;
                    delivery.ForwarderId = form.Forwarder;
                    delivery.Waybill = form.Waybill;
                    delivery.Remarks = form.Remarks + "";
                    delivery.ServiceLevelAgreement = form.ServiceLevelAgreement;
                    delivery.Representative = form.Representative;
                    delivery.DateReceive = form.DateReceive;
                    context.SalesOrderDeliveryMonitorings.Update(delivery);
                    message = "Delivery details updated successfully";
                }

                await context.SaveChangesAsync(currentUserId);

                return message;
            }
            else
            {
                return "Invalid";
            }
        }
    }
}