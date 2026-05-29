using GMGInventory.Application.Contracts;
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
        private readonly UserManager<User> userManager;
        private readonly string currentUserId;

        public DeliveryMonitoringRepository(ApplicationDbContext context
                                          , IHttpContextAccessor httpContextAccessor
                                          , UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
        }

        public async Task<IList<JsonNameOptionsModel>> GetStatusOptions(int? IncludeId)
        {
            return await context.SalesOrderDeliveryStatuses
                                .Select(s => new JsonNameOptionsModel
                                {
                                    Id = s.Id.ToString(),
                                    Text = s.Code,
                                    Name = s.Name,
                                    Selected = (IncludeId != null && IncludeId == s.Id) || (IncludeId == null && s.Id == 1)
                                })
                                .ToListAsync();
        }

        public async Task<Select2Model<JsonNameOptionsModel>> GetStatusOptions2(string search, int page)
        {
            var statuses = await context.SalesOrderDeliveryStatuses
                                        .Where(s => string.IsNullOrWhiteSpace(search)
                                                || (s.Code.ToLower().Contains(search.ToLower()))
                                                || (s.Name.ToLower().Contains(search.ToLower())))
                                        .Select(s => new JsonNameOptionsModel
                                        {
                                            Id = s.Id.ToString(),
                                            Text = s.Code,
                                            Name = s.Name
                                        })
                                        .OrderBy(s => s.Text)
                                        .ToListAsync();
            var statusesFiltered = statuses.Skip((page - 1) * 50).Take(50).ToList();
            return new Select2Model<JsonNameOptionsModel>
            {
                //incomplete_results = false,
                items = statusesFiltered,
                total_count = statuses.Count
            };
        }

        public async Task<IList<JsonNameOptionsModel>> GetMethodOptions(int? IncludeId)
        {
            return await context.SalesOrderDeliveryMethods
                                .Select(s => new JsonNameOptionsModel
                                {
                                    Id = s.Id.ToString(),
                                    Text = s.Code,
                                    Name = s.Name,
                                    Selected = (IncludeId != null && IncludeId == s.Id) || (IncludeId == null && s.Id == 1)
                                })
                                .ToListAsync();
        }

        public async Task<Select2Model<JsonNameOptionsModel>> GetMethodOptions2(string search, int page)
        {
            var methods = await context.SalesOrderDeliveryMethods
                                       .Where(s => string.IsNullOrWhiteSpace(search)
                                               || (s.Code.ToLower().Contains(search.ToLower()))
                                               || (s.Name.ToLower().Contains(search.ToLower())))
                                       .Select(s => new JsonNameOptionsModel
                                       {
                                           Id = s.Id.ToString(),
                                           Text = s.Code,
                                           Name = s.Name
                                       })
                                       .OrderBy(s => s.Text)
                                       .ToListAsync();
            var methodsFiltered = methods.Skip((page - 1) * 50).Take(50).ToList();
            return new Select2Model<JsonNameOptionsModel>
            {
                //incomplete_results = false,
                items = methodsFiltered,
                total_count = methods.Count
            };
        }

        private async Task<IList<DeliveryMonitoringVM>> GetListAsync(DeliveryMonitoringFilterVM filter)
        {
            var deliveries = from order in context.SalesOrders
                             join pick in context.Picks on order.Id equals pick.SalesOrderId into opick
                             from pick in opick.DefaultIfEmpty()
                             join pack in context.Packs on order.Id equals pack.SalesOrderId into opack
                             from pack in opack.DefaultIfEmpty()
                             join invoice in context.Invoices on new { SalesOrderId = order.Id, PackId = pack.Id } equals new { invoice.SalesOrderId, invoice.PackId } into oinvoice
                             from invoice in oinvoice.DefaultIfEmpty()
                             join delivery in context.SalesOrderDeliveries on new { SalesOrderId = order.Id, PackId = pack.Id } equals new { delivery.SalesOrderId, delivery.PackId } into odelivery
                             from delivery in odelivery.DefaultIfEmpty()
                             join userc in userManager.Users on order.UserCreated equals userc.Id
                             join approve in userManager.Users on order.ApprovedBy equals approve.Id into auser
                             from approve in auser.DefaultIfEmpty()
                             where order.Status.Code != "DRAFT"
                             && order.Status.Code != "SUBMITTED"
                             && order.Status.Code != "DISAPPROVED"
                             && filter.DateFrom.Date <= order.SalesOrderDate.Date
                             && order.SalesOrderDate.Date <= filter.DateTo.Date
                             select new DeliveryMonitoringVM
                             {
                                 SalesOrderId = order.Id,
                                 DateOrderD = order.SalesOrderDate,
                                 SalesOrderNumber = order.SalesOrderNumber,
                                 PackId = pack.Id,
                                 OrderQueueNumber = order.SalesOrderItemSources
                                                         .Select(s => s.SourceOrderNumber)
                                                         .FirstOrDefault(),
                                 ServiceLevelAgreement = order.ServiceLevelAgreement.Name,
                                 CustomerId = order.Customer.Id,
                                 Customer = order.Customer.Name,
                                 SOPreparedBy = userc.FirstName + " " + userc.LastName,
                                 SpecialInstruction = order.SpecialInstruction,
                                 ForwarderId = delivery.Forwarder.Id,
                                 Forwarder = delivery.Forwarder.Name,
                                 SOApprovedBy = approve == null ? null : approve.FirstName + " " + approve.LastName,
                                 Status1D = order.Status.Name,
                                 Status1Date = order.DateModified != null ? order.DateModified.Value
                                             : order.DateCreated,
                                 Status2D = pick.Status.ToUpper(),
                                 Status2Date = pick.PickDate,
                                 Status3D = pack.Status.Name,
                                 Status3Date = pack.DateModified != null ? pack.DateModified.Value
                                             : pack.DateCreated,
                                 Status4D = invoice.Status.Name,
                                 Status4Date = invoice.DateModified != null ? invoice.DateModified.Value
                                             : invoice.DateCreated,
                                 Status5D = delivery.SalesOrderDeliveryStatus.Name,
                                 Status5Date = delivery.DeliveryDate != null ? delivery.DeliveryDate
                                             : delivery.PickupDate != null ? delivery.PickupDate
                                             : delivery.ReceiveDate != null ? delivery.ReceiveDate
                                             : delivery.DateModified != null ? delivery.DateModified
                                             : delivery.DateCreated,
                                 DeliveryId = delivery.Id,
                                 DeliveryStatusId = delivery.SalesOrderDeliveryStatus.Id,
                                 DeliveryStatus = delivery.SalesOrderDeliveryStatus.Name,
                                 PickupDateD = delivery.PickupDate,
                                 DeliveryDateD = delivery.DeliveryDate,
                                 ReceiveDateD = delivery.ReceiveDate,
                                 DeliveryMethodId = delivery.SalesOrderDeliveryMethod.Id,
                                 DeliveryMethod = delivery.SalesOrderDeliveryMethod.Name,
                                 Waybill = delivery.Waybill,
                                 DriverId = delivery.Driver.Id,
                                 Driver = delivery.Driver != null ? delivery.Driver.FirstName + " " + delivery.Driver.LastName
                                        : null,
                                 VehicleId = delivery.Vehicle.Id,
                                 Vehicle = delivery.Vehicle.FleetPlateNumber,
                                 ReceiveBy = delivery.ReceivedBy,
                                 ReceiveContact = delivery.ReceiverContact,
                                 Remarks = delivery.Remarks
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
                    "DateOrder" => isAsc ? data.OrderBy(s => s.DateOrderD).ThenBy(s => s.Status1Date) : data.OrderByDescending(s => s.DateOrderD).ThenByDescending(s => s.Status1Date),
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

        public async Task<ResponseModel> SaveDeliveryAsync(DeliveryMonitoringFormVM form)
        {
            var salesOrder = await context.SalesOrders
                                          .Where(s => s.Id == form.SalesOrderId)
                                          .FirstOrDefaultAsync();
            if (salesOrder == null) return new ResponseModel { Status = "Error", Message = "Invalid record, sales order cannot be found" };

            var pack = await context.Packs
                                    .Where(s => s.Id == form.PackId)
                                    .FirstOrDefaultAsync();
            if (pack == null) return new ResponseModel { Status = "Error", Message = "Invalid record, pack cannot be found" };

            var status = await context.SalesOrderDeliveryStatuses
                                      .Where(s => s.Id == form.DeliveryStatusId)
                                      .FirstOrDefaultAsync();
            if (status == null) return new ResponseModel { Status = "Error", Message = "Invalid record, status cannot be found" };

            var method = await context.SalesOrderDeliveryMethods
                                      .Where(s => s.Id == form.DeliveryMethodId)
                                      .FirstOrDefaultAsync();
            if (method == null) return new ResponseModel { Status = "Error", Message = "Invalid record, method cannot be found" };

            var forwarder = await context.Forwarders
                                         .Where(s => s.Id == form.ForwarderId)
                                         .FirstOrDefaultAsync();
            if (form.ForwarderId != null && forwarder == null) return new ResponseModel { Status = "Error", Message = "Invalid record, forwarder cannot be found" };

            var vehicle = await context.Vehicles
                                       .Where(s => s.Id == form.VehicleId)
                                       .FirstOrDefaultAsync();
            if (form.VehicleId != null && vehicle == null) return new ResponseModel { Status = "Error", Message = "Invalid record, vehicle cannot be found" };

            var driver = await context.Drivers
                                      .Where(s => s.Id == form.DriverId)
                                      .FirstOrDefaultAsync();
            if (form.DriverId != null && driver == null) return new ResponseModel { Status = "Error", Message = "Invalid record, driver cannot be found" };

            var delivery = await context.SalesOrderDeliveries
                                        .Where(s => s.SalesOrderId == salesOrder.Id
                                                 && s.PackId == pack.Id)
                                        .FirstOrDefaultAsync();
            if (delivery == null)
            {
                delivery = new SalesOrderDelivery
                {
                    SalesOrderId = salesOrder.Id,
                    PackId = pack.Id,
                    SalesOrderDeliveryStatusId = status.Id,
                    PickupDate = form.PickupDate,
                    DeliveryDate = form.DeliveryDate,
                    ReceiveDate = form.ReceiveDate,
                    SalesOrderDeliveryMethodId = method.Id,
                    ForwarderId = forwarder == null ? null : forwarder.Id,
                    Waybill = form.Waybill,
                    VehicleId = vehicle == null ? null : vehicle.Id,
                    DriverId = driver == null ? null : driver.Id,
                    ReceivedBy = form.ReceiverName,
                    ReceiverContact = form.ReceiverContact,
                    Remarks = form.Remarks
                };
                await context.SalesOrderDeliveries.AddAsync(delivery);
                await context.SaveChangesAsync(currentUserId);

                return new ResponseModel { Status = "Success", Message = "Successfully added sales order delivery" };
            }
            else
            {
                delivery.SalesOrderDeliveryStatusId = status.Id;
                delivery.PickupDate = form.PickupDate;
                delivery.DeliveryDate = form.DeliveryDate;
                delivery.ReceiveDate = form.ReceiveDate;
                delivery.SalesOrderDeliveryMethodId = method.Id;
                delivery.ForwarderId = forwarder == null ? null : forwarder.Id;
                delivery.Waybill = form.Waybill;
                delivery.VehicleId = vehicle == null ? null : vehicle.Id;
                delivery.DriverId = driver == null ? null : driver.Id;
                delivery.ReceivedBy = form.ReceiverName;
                delivery.ReceiverContact = form.ReceiverContact;
                delivery.Remarks = form.Remarks;

                await context.SaveChangesAsync(currentUserId);

                return new ResponseModel { Status = "Success", Message = "Successfully updated sales order delivery" };
            }
        }
    }
}