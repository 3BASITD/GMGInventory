using DocumentFormat.OpenXml.Drawing.Charts;
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
    public class PurchaseOrderNewReporitory : IPurchaseOrderNewRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly string currentUserId;

        public PurchaseOrderNewReporitory(ApplicationDbContext context
                                        , IHttpContextAccessor httpContextAccessor
                                        , UserManager<User> userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
        }

        public async Task<Select2Model<JsonPurchaseOrderOptionsModel>> GetOptions(string search, int page)
        {
            var orders = from order in context.PurchaseOrderNews
                         join user in userManager.Users on order.UserCreated equals user.Id
                         where string.IsNullOrWhiteSpace(search)
                         && order.Status.Code.ToLower() != "draft"
                            || (order.DocumentNumber.ToLower().Contains(search.ToLower()))
                         orderby order.DocumentNumber ascending
                         select new JsonPurchaseOrderOptionsModel
                         {
                             Id = order.Id.ToString(),
                             OrderBy = SystemFormat.Name(user.FirstName, user.LastName, user.Nickname),
                             OrderDateD = order.OrderDate,
                             Supplier = order.Supplier.Name,
                             Text = order.DocumentNumber
                         };
            var ordersFiltered = await orders.Skip((page - 1) * 50).Take(50).ToListAsync();
            return new Select2Model<JsonPurchaseOrderOptionsModel>
            {
                //incomplete_results = false,
                items = ordersFiltered,
                total_count = orders.Count()
            };
        }

        public async Task<IList<JsonOrderTypeOptionsModel>> GetTypeOptions(int? SelectedId)
        {
            return await context.PurchaseOrderTypes
                                .Select(s => new JsonOrderTypeOptionsModel
                                {
                                    Id = s.Id.ToString(),
                                    Text = s.Name,
                                    Code = s.Code,
                                    RequestTypeId = s.RequestTypeId,
                                    Selected = SelectedId != null && s.Id == SelectedId
                                })
                                .OrderBy(s => s.Id)
                                .ToListAsync();
        }

        public async Task<Select2Model<JsonOrderTypeOptionsModel>> GetTypeOptions2(string search, int page)
        {
            var types = from type in context.PurchaseOrderTypes
                        orderby type.Id ascending
                        select new JsonOrderTypeOptionsModel
                        {
                            Id = type.Id.ToString(),
                            Text = type.Name,
                            Code = type.Code,
                            RequestTypeId = type.RequestTypeId
                        };
            var typesFiltered = await types.Skip((page - 1) * 50).Take(50).ToListAsync();
            return new Select2Model<JsonOrderTypeOptionsModel>
            {
                //incomplete_results = false,
                items = typesFiltered,
                total_count = types.Count()
            };
        }

        public async Task<IList<JsonOptionsModel>> GetDeliveryModeOptions(int? SelectedId)
        {
            return await context.PurchaseOrderDeliveryModes
                                .Select(s => new JsonOptionsModel
                                {
                                    Id = s.Id.ToString(),
                                    Text = s.Name,
                                    Selected = SelectedId != null && s.Id == SelectedId
                                })
                                .OrderBy(s => s.Id)
                                .ToListAsync();
        }

        public async Task<Select2Model<JsonOptionsModel>> GetDeliveryModeOptions2(string search, int page)
        {
            var modes = from mode in context.PurchaseOrderDeliveryModes
                        orderby mode.Id ascending
                        select new JsonOptionsModel
                        {
                            Id = mode.Id.ToString(),
                            Text = mode.Name
                        };
            var modesFiltered = await modes.Skip((page - 1) * 50).Take(50).ToListAsync();
            return new Select2Model<JsonOptionsModel>
            {
                //incomplete_results = false,
                items = modesFiltered,
                total_count = modes.Count()
            };
        }

        public async Task<Select2Model<JsonPurchaseOptionModel>> GetPurchaseRequestOptions(string search, int page, int RequestTypeId, int? RequestPurposeId, int? RegionId)
        {
            //var query = from pr in context.PurchaseRequests.AsNoTracking()
            //            where (search == null || pr.DocumentNumber.ToLower().Contains(search.ToLower()))
            //               && pr.TypeId == RequestTypeId
            //               && (RequestPurposeId == null || pr.PurposeId == RequestPurposeId)
            //               && pr.Status.Name.ToLower() == "approved"
            //               && (RegionId == null || pr.PurchaseRequestLineItems.Any(s=>s.SalesOrder.Customer.Area.Region.Id == RegionId))
            //            //&& pr.PurchaseRequestLineItems.Any(prli =>
            //            //     prli.Quantity !=
            //            //     prli.PurchaseOrderLineItems
            //            //         .Where(poli => poli.PurchaseOrder.Status.Name.ToLower() == "approved")
            //            //         .Sum(poli => ((int?)poli.Quantity) ?? 0)
            //            //                   && (RegionId == null ||
            //            //                       prli.SalesOrder
            //            //                           .SalesOrderLineItems
            //            //                           .Any(sol => sol.SalesOrder.Customer.Area.Region.Id == RegionId))
            //            //)
            //            let Quantity = pr.PurchaseRequestLineItems
            //                             .Sum(s => (int?)s.Quantity) ?? 0
            //            let OrderQuantity = pr.PurchaseRequestLineItems
            //                                  .SelectMany(x => x.PurchaseOrderLineItems)
            //                                  .Where(poli => poli.PurchaseOrder.Status.Name.ToLower() == "approved")
            //                                  .Sum(poli => (int?)poli.Quantity) ?? 0
            //            orderby pr.DocumentNumber ascending
            //            select new JsonPurchaseOptionModel
            //            {
            //                Id = pr.Id.ToString(),
            //                Text = pr.DocumentNumber,
            //                TotalQuantity = Quantity,
            //                TotalIncompleteQuantity = Math.Max(0, Quantity - OrderQuantity),
            //                Disabled = Quantity - OrderQuantity <= 0
            //            };
            //var queryFiltered = await query.Skip((page - 1) * 50).Take(50).ToListAsync();
            //return new Select2Model<JsonPurchaseOptionModel>
            //{
            //    items = queryFiltered,
            //    total_count = query.Count()
            //};


            var requests = from prli in context.PurchaseRequestLineItems
                           where (string.IsNullOrWhiteSpace(search) || prli.PurchaseRequest.DocumentNumber.Contains(search))
                              && prli.PurchaseRequest.TypeId == RequestTypeId
                              && (RequestPurposeId == null || prli.PurchaseRequest.PurposeId == RequestPurposeId)
                              /*&& prli.PurchaseRequest.Status.Name.ToLower() == "approved"*/
                              && (RegionId == null || prli.SalesOrder.Customer.Area.Region.Id == RegionId)
                           group prli by new { prli.PurchaseRequest.Id, prli.PurchaseRequest.DocumentNumber, Status = prli.PurchaseRequest.Status.Name } into g
                           orderby g.Key.Id ascending
                           select new
                           {
                               g.Key.Id,
                               DocumentNumber = g.Key.DocumentNumber + (g.Key.Status.ToLower() == "approved" ? "" : (" (" + g.Key.Status + ")")),
                               TotalQuantity = g.Sum(s => s.Quantity),
                               //ActiveOnlyEnabler = (ActiveOnly == true && g.Key.Status.ToLower() == "approved")
                               //                 || (ActiveOnly == false && g.Key.Status.ToLower() != "disapproved")
                           };

            var filteredRequests = await requests.Skip((page - 1) * 50)
                                                 .Take(50)
                                                 .ToListAsync();

            var filteredRequestsOn = filteredRequests.Select(s =>
            {
                var ordq = context.PurchaseOrderNewLineItems
                                  .Where(poli => poli.RequestItem.PurchaseRequest.Id == s.Id
                                              /*&& poli.PurchaseOrder.Status.Name.ToLower() == "approved"*/)
                                  .Sum(poli => (int?)poli.Quantity) ?? 0;

                var alreadyOn = string.Join(", ", context.PurchaseOrderNewLineItems
                                                         .Where(poli => poli.RequestItem.PurchaseRequest.Id == s.Id
                                                                     /*&& poli.PurchaseOrder.Status.Name.ToLower() == "approved"*/)
                                                         .Select(poli => poli.PurchaseOrder.DocumentNumber)
                                                         .Distinct()
                                                         .ToList());

                return new JsonPurchaseOptionModel
                {
                    Id = s.Id.ToString(),
                    Text = s.DocumentNumber,
                    TotalQuantity = s.TotalQuantity,
                    TotalIncompleteQuantity = s.TotalQuantity - ordq,
                    Disabled = (s.TotalQuantity - ordq) <= 0,// || s.ActiveOnlyEnabler == false,
                    AlreadyOn = alreadyOn
                };
            }).ToList();


            return new Select2Model<JsonPurchaseOptionModel>
            {
                //incomplete_results = false,
                items = filteredRequestsOn,
                total_count = requests.Count()
            };
        }

        public async Task<IList<JsonPurchaseItemOptionsModel>> GetPurchaseRequestItemOptions(int RequestId, int? SelectedId)
        {
            var requests = from prli in context.PurchaseRequestLineItems
                           let ponli = context.PurchaseOrderNewLineItems
                                              .Where(a => a.RequestItemId == prli.Id
                                                       /*&& a.PurchaseOrder.Status.Name.ToLower() == "approved"*/)
                                              .ToList()
                           let prevorder = context.PurchaseOrderNewLineItems
                                                  .Where(a => a.RequestItem.ItemId == prli.ItemId
                                                           /*&& a.PurchaseOrder.Status.Name.ToLower() == "approved"*/)
                                                  .OrderByDescending(a => a.DateCreated)
                                                  .Select(a => new
                                                  {
                                                      Cost = a.UnitCost,
                                                      Currency = a.PurchaseOrder.Currency.Name,
                                                      CurrencyRate = a.PurchaseOrder.CurrencyRate
                                                  })
                                                  .FirstOrDefault()
                           where prli.PurchaseRequestId == RequestId
                           select new JsonPurchaseItemOptionsModel
                           {
                               Id = prli.Id.ToString(),
                               Text = prli.ItemDescription,
                               ItemId = prli.Item == null ? null : prli.Item.Id,
                               ItemCode = prli.Item == null ? null : prli.Item.Code,
                               PartNumber = prli.Item == null ? null : prli.Item.PartNumber,
                               Quantity = prli.Quantity,
                               Unit = prli.Item == null ? prli.Unit.Name : prli.Item.Unit.Name,
                               UnitId = prli.Item == null ? prli.Unit.Id : prli.Item.Unit.Id,
                               SalesOrderId = prli.SalesOrder.Id,
                               SalesOrderNumber = prli.SalesOrder.SalesOrderNumber,
                               Served = ponli.Select(a => (int?)a.Quantity).Sum() ?? 0,
                               OrderedOn = ponli.Count() > 0 ? string.Join(", ", ponli.Select(s => s.PurchaseOrder.DocumentNumber)) : "",
                               Disabled = (ponli.Select(a => (int?)a.Quantity).Sum() ?? 0) >= prli.Quantity,
                               PreviousCurrency = prevorder == null ? null : prevorder.Currency,
                               PreviousCurrencyRateD = prevorder == null ? null : prevorder.CurrencyRate,
                               PreviousCostD = prevorder == null ? null : prevorder.Cost
                           };
            return await requests.ToListAsync();
        }


        public async Task<Select2Model<JsonSalesOrderOptionsModel>> GetSalesOrderUnservedOnlyOptions(string search, int page, int? regionId)
        {
            var orders = from order in context.SalesOrders
                         //let oli = from soli in order.SalesOrderLineItems
                         //          let ii = context.ItemInventories
                         //                .Where(s => s.ItemId == soli.ItemId
                         //                         && s.Warehouse.IsMain == true)
                         //                .Sum(s => (int?)s.Quantity) ?? 0
                         //          select new
                         //          {
                         //              soli.Quantity,
                         //              Stock = ii
                         //          }
                         where order.Status.Code.ToLower() != "submitted"
                         && order.Status.Code.ToLower() != "disapproved"
                         && order.Status.Code.ToLower() != "draft"
                         && (string.IsNullOrWhiteSpace(search)
                         || (order.SalesOrderNumber.Trim().ToLower().Contains(search.Trim().ToLower())))
                         && (regionId == null || order.Customer.Area.Region.Id == regionId)
                         //&& oli.Any(s => s.Quantity > s.Stock)
                         orderby order.SalesOrderNumber ascending
                         select new JsonSalesOrderOptionsModel
                         {
                             Id = order.Id.ToString(),
                             Text = order.SalesOrderNumber,
                             CustomerCode = order.Customer.Code,
                             CustomerName = order.Customer.Name,
                             CustomerAddress = order.Customer
                                                    .ShippingAddresses
                                                    .Where(s => s.IsDefault == true)
                                                    .Select(s => s.Address)
                                                    .FirstOrDefault(),
                             SalesOrderDateD = order.SalesOrderDate,
                             PurchaseOrder = order.PurchaseOrder,
                             ForwarderName = order.Forwarder != null ? order.Forwarder.Name : null,
                             RegionId = order.Customer.Area.RegionId
                         };
            var ordersFiltered = await orders.Skip((page - 1) * 50).Take(50).ToListAsync();
            return new Select2Model<JsonSalesOrderOptionsModel>
            {
                items = ordersFiltered,
                total_count = orders.Count()
            };
        }

        public async Task<string> GetNewDocumentNumber(int? TypeId)
        {
            var purchaseType = await context.PurchaseOrderTypes.Where(s => s.Id == TypeId).FirstOrDefaultAsync();

            var procurementHelper = new ProcurementHelper();

            var docNumber = await procurementHelper.GetNewOrder(context, purchaseType?.Code ?? "", "submitted");
            return docNumber;

        }

        public async Task<IList<PurchaseOrderNewVM>> GetListAsync(PurchaseOrderFilter filter)
        {
            var orderedQty = from poli in context.PurchaseOrderNewLineItems
                             group poli by poli.PurchaseOrderId into g
                             select new
                             {
                                 PurchaseOrderId = g.Key,
                                 Total = g.Sum(s => (int?)s.Quantity) ?? 0
                             };
            var receivedQty = from prli in context.PurchaseReceivingLineItems
                              group prli by prli.PurchaseReceiving.PurchaseOrderId into g
                              select new
                              {
                                  PurchaseOrderId = g.Key,
                                  Total = g.Sum(s => (int?)s.Quantity) ?? 0
                              };
            var orderVM = from order in context.PurchaseOrderNews
                          //join olq in orderedQty on order.Id equals olq.PurchaseOrderId into olqg
                          //from olq in olqg.DefaultIfEmpty()
                          //join rlq in receivedQty on order.Id equals rlq.PurchaseOrderId into rlqg
                          //from rlq in rlqg.DefaultIfEmpty()
                          let olq = orderedQty.Where(s => s.PurchaseOrderId == order.Id).Sum(s => s.Total)
                          let rlq = receivedQty.Where(s => s.PurchaseOrderId == order.Id).Sum(s => s.Total)
                          where (filter.TypeId == null || order.TypeId == filter.TypeId)
                          && (filter.ModeId == null || order.DeliveryModeId == filter.ModeId)
                          && (filter.SupplierId == null || order.Supplier.Id == filter.SupplierId)
                          && (filter.DateFrom == null || order.OrderDate >= filter.DateFrom)
                          && (filter.DateTo == null || order.OrderDate <= filter.DateTo)
                          && (filter.StatusId == null || order.StatusId == filter.StatusId)
                          select new PurchaseOrderNewVM
                          {
                              Id = order.Id,
                              DocumentNumber = order.DocumentNumber,
                              Type = order.Type.Code,
                              SupplierId = order.Supplier.Id,
                              Supplier = order.Supplier.Code,
                              SupplierName = order.Supplier.Name,
                              Term = order.Term.Name,
                              DeliveryMode = order.DeliveryMode.Code,
                              Status = order.Status.Code,
                              ReceivedStatus = olq - rlq <= 0 ? "COMPLETED"
                                             : rlq == 0 ? "NOTHING YET"
                                             : olq - rlq + " REMAINING",
                              OrderDateD = order.OrderDate,
                              Remarks = order.Remarks
                          };
            return await orderVM.ToListAsync();
        }

        public async Task<ForDataTables<PurchaseOrderNewVM>> GetListDTAsync(DataTablesRequestForm form, PurchaseOrderFilter filter)
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
                    s.DocumentNumber,
                    s.Type,
                    s.Supplier,
                    s.SupplierName,
                    s.Term,
                    s.DeliveryMode,
                    s.Status,
                    s.ReceivedStatus,
                    s.OrderDate,
                    s.Remarks
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
                            case "OrderDate": data = data.OrderBy(s => s.OrderDateD).ThenBy(s => s.DocumentNumber); break;
                            default: break;
                        }
                    }
                    else if (form.SortColumnDirection == "desc")
                    {
                        switch (form.SortColumn)
                        {
                            case "OrderDate": data = data.OrderByDescending(s => s.OrderDateD).ThenByDescending(s => s.DocumentNumber); break;
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
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<PurchaseOrderNewVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public async Task<IList<ItemFromPurchaseRequestVM>> GetPurchaseRequestItems(int PurchaseRequestId)
        {

            var itemVM = await (from request in context.PurchaseRequestLineItems
                                //join latest in itemLatestCost on request.ItemId equals latest.ItemId into latestg
                                //from latest in latestg.DefaultIfEmpty()
                                let orderQuantity = context.PurchaseOrderNewLineItems
                                                            .Where(s => s.RequestItemId == request.Id)
                                                            .Sum(s => (int?)s.Quantity) ?? 0
                                where request.PurchaseRequestId == PurchaseRequestId
                                select new ItemFromPurchaseRequestVM
                                {
                                    Id = request.Id,
                                    ItemId = request.Item.Id,
                                    ItemCode = request.Item.Code,
                                    ItemName = request.Item.Name,
                                    PartNumber = request.Item.PartNumber,
                                    RemainingQuantityD = Math.Max(0, request.Quantity - orderQuantity),
                                    RequestId = request.PurchaseRequest.Id,
                                    RequestQuantityD = request.Quantity,
                                    Unit = request.Item.Unit.Code,
                                    UnitId = request.Item.Unit.Id,
                                    //UnitCostD = latest == null ? 0 : latest.UnitCost
                                }).ToListAsync();

            var itemIds = itemVM.Select(s => s.ItemId).ToList();

            var itemLatestCost = await context.PurchaseOrderNewLineItems
                                              .Where(s => s.RequestItem.ItemId.HasValue && itemIds.Contains(s.RequestItem.ItemId.Value))
                                              .GroupBy(s => s.RequestItem.ItemId.Value)
                                              .Select(g => new
                                              {
                                                  ItemId = g.Key,
                                                  UnitCost = g.OrderByDescending(s => s.DateCreated)
                                                              .Select(s => s.UnitCost)
                                                              .FirstOrDefault()
                                              })
                                              .ToListAsync();
            var latestMap = itemLatestCost.ToDictionary(x => x.ItemId, x => x.UnitCost);
            foreach (var item in itemVM)
            {
                //var latest = itemLatestCost.Where(s => s.ItemId == item.ItemId).FirstOrDefault();

                //item.UnitCostD = latest == null ? 0 : latest.UnitCost;
                item.UnitCostD = latestMap.TryGetValue(item.ItemId, out var cost) ? cost : 0;
            }

            return itemVM;
        }

        public async Task<IList<APIPurchaseOrder>> GetListAPI(DateTime? dt)
        {
            DateTime dtNow = DateTime.Now;
            DateTime dtYesterday = dtNow.AddDays(-1);
            var orders = from order in context.PurchaseOrderNews
                         join userc in userManager.Users on order.UserCreated equals userc.Id
                         join userm in userManager.Users on order.UserModified equals userm.Id into um
                         from userm in um.DefaultIfEmpty()
                         where ((dt != null && order.OrderDate.Date == dt.Value.Date)
                         || (dt == null && (order.BarcotechRequestDate == null
                         || order.BarcotechRequestDate > dtYesterday)))
                       && order.Status.Code.Trim().ToLower() == "approved"
                         select new APIPurchaseOrder
                         {
                             Id = order.Id,
                             DocumentNumber = order.DocumentNumber,
                             SupplierCode = order.Supplier.Code,
                             SupplierId = order.Supplier.Id,
                             WarehouseCode = order.DeliveryAddress.Code,
                             WarehouseId = order.DeliveryAddress.Id,
                             OrderDate = order.OrderDate,
                             DueDate = order.DueDate,
                             UserCreated = userc.FirstName + " " + userc.LastName,
                             DateCreated = order.DateCreated,
                             LastUserModified = userm == null ? null : userm.FirstName + " " + userm.LastName,
                             LastDateModified = order.DateModified,
                             LineItems = from item in order.PurchaseOrderLineItems
                                         select new APIPurchaseLineItem
                                         {
                                             Id = item.Id,
                                             ItemCode = item.RequestItem.ItemId != null
                                                     ? item.RequestItem.Item.Code
                                                     : item.RequestItem.ItemDescription,
                                             ItemId = item.RequestItem.ItemId != null
                                                    ? item.RequestItem.Item.Id
                                                    : -1,
                                             Quantity = item.Quantity
                                         }
                         };
            var resultOrders = await orders.ToListAsync();

            foreach (var order in resultOrders)
            {
                var retrievedorder = context.PurchaseOrderNews.Where(s => s.Id == order.Id).First();
                if (retrievedorder.BarcotechRequestDate == null)
                {
                    retrievedorder.BarcotechRequestDate = dtNow;
                    context.SaveChanges();
                }
            }

            return resultOrders;
        }

        public async Task<PurchaseOrderNewDetailedVM?> GetDetails(int orderId)
        {
            var orderedQty = context.PurchaseOrderNewLineItems
                                    .Where(s => s.PurchaseOrderId == orderId)
                                    .Sum(s => (int?)s.Quantity) ?? 0;
            var receivedQty = context.PurchaseReceivingLineItems
                                     .Where(s => s.PurchaseReceiving.PurchaseOrderId == orderId)
                                     .Sum(s => (int?)s.Quantity) ?? 0;
            var details = from order in context.PurchaseOrderNews
                          let resubmitfrom = context.PurchaseOrderNews
                                                    .Where(s => s.ResubmitId == order.Id)
                                                    .FirstOrDefault()
                          join userc in userManager.Users on order.UserCreated equals userc.Id
                          join userm in userManager.Users on order.UserModified equals userm.Id into m
                          from userm in m.DefaultIfEmpty()
                          let item = order.PurchaseOrderLineItems.FirstOrDefault()
                          let firstrequest = order.PurchaseOrderLineItems.First().RequestItem.PurchaseRequest.PurchaseRequestLineItems.First()
                          where order.Id == orderId
                          select new PurchaseOrderNewDetailedVM
                          {
                              Id = order.Id,
                              DocumentNumber = order.DocumentNumber,
                              Type = order.Type.Name,
                              TypeId = order.Type.Id,
                              RequestType = order.Type.RequestType.Name,
                              RequestTypeId = order.Type.RequestType.Id,
                              RequestPurpose = order.Purpose.Name,
                              RequestPurposeId = order.Purpose.Id,
                              Region = order.Region == null ? null : order.Region.Name,
                              RegionId = order.Region == null ? null : order.Region.Id,
                              Supplier = order.Supplier.Name,
                              SupplierId = order.Supplier.Id,
                              SupplierContactPerson = order.Supplier.ContactPerson + "",
                              SupplierContactNumber = order.Supplier.OfficeTelNo + "",
                              SupplierAddress = order.Supplier.AddressLine1,
                              Term = order.Term.Name,
                              TermId = order.Term.Id,
                              DeliveryMode = order.DeliveryMode.Name,
                              DeliveryModeId = order.DeliveryMode.Id,
                              DeliveryAddress = order.DeliveryAddress.Address + "",
                              DeliveryAddressId = order.DeliveryAddress.Id,
                              DeliveryName = order.DeliveryAddress.Name,
                              DeliveryContactPerson = order.DeliveryAddress.ContactPerson + "",
                              DeliveryContactNumber = order.DeliveryAddress.ContactNumber + "",
                              Status = order.Status.Code + (order.Status.Code.ToLower() != "approved" ? "" : (
                                                orderedQty - receivedQty <= 0 ? " (Fully Received)"
                                              : receivedQty == 0 ? " (Not yet Received)"
                                              : " (Partially Received, " + (orderedQty - receivedQty) + "  Remaining)")),
                              OrderDateD = order.OrderDate,
                              Discount1D = order.Discount1,
                              Discount2D = order.Discount2,
                              Discount3D = order.Discount3,
                              IsVATIncluded = order.IsVATIncluded ? "Yes" : "No",
                              Currency = order.Currency.Name,
                              CurrencyId = order.Currency.Id,
                              CurrencyRateD = order.CurrencyRate,
                              Remarks = order.Remarks,
                              ApprovedBy = order.ApproverId == null ? null : order.Approver.FirstName + " " + order.Approver.LastName,
                              DisapprovedBy = order.DisapproverId == null ? null : order.Disapprover.FirstName + " " + order.Disapprover.LastName,
                              DisapprovedReason = order.DisapprovedReason,
                              ResubmitId = order.ResubmitId,
                              ResubmitFromId = resubmitfrom == null ? null : resubmitfrom.Id,
                              PurchaseRequestNumber = string.Join(", ", order.PurchaseOrderLineItems
                                                                             .Select(s => s.RequestItem.PurchaseRequest.DocumentNumber)
                                                                             .Distinct()),
                              DateReceivedD = order.PurchaseReceivings
                                                   .Where(s => s.Status.Name.ToLower() == "approved")
                                                   .OrderByDescending(s => s.ReceiveDate)
                                                   .Select(s => (DateTime?)s.ReceiveDate)
                                                   .FirstOrDefault(),
                              IsReceived = order.PurchaseReceivings.Count() > 0,
                              UserCreated = SystemFormat.Name(userc.FirstName, userc.LastName, userc.Nickname),
                              DateCreatedD = order.DateCreated,
                              UserModified = userm == null ? null : SystemFormat.Name(userm.FirstName, userm.LastName, userm.Nickname),
                              DateModifiedD = order.DateModified
                          };
            return await details.FirstOrDefaultAsync();
        }

        public async Task<IList<PurchaseOrderLineItemVM>> GetItemListAsync(int purchaseOrderId)
        {
            var orderVM = from order in context.PurchaseOrderNewLineItems
                          //.Include(s => s.PurchaseRequest)
                          //.Include(s => s.Item)
                          //.Include(s => s.Unit)
                          where order.PurchaseOrderId == purchaseOrderId
                          select new PurchaseOrderLineItemVM
                          {
                              Id = order.Id,
                              PurchaseOrderId = order.PurchaseOrderId,
                              LineNumber = order.LineNumber,
                              PurchaseRequestId = order.RequestItem.PurchaseRequest.Id,
                              PurchaseRequestNumber = order.RequestItem.PurchaseRequest.DocumentNumber,
                              SalesOrderId = order.RequestItem.SalesOrder.Id,
                              SalesOrderNumber = order.RequestItem.SalesOrder.SalesOrderNumber,
                              RequestItemId = order.RequestItem.Id,
                              ItemId = order.RequestItem.Item == null ? null : order.RequestItem.Item.Id,
                              ItemCode = order.RequestItem.Item == null ? null : order.RequestItem.Item.Code,
                              ItemPartNumber = order.RequestItem.Item == null ? null : order.RequestItem.Item.PartNumber,
                              ItemDescription = order.RequestItem.ItemDescription,
                              QuantityD = order.Quantity,
                              UnitId = order.RequestItem.Item == null ? order.RequestItem.Unit.Id : order.RequestItem.Item.Unit.Id,
                              UnitCode = order.RequestItem.Item == null ? order.RequestItem.Unit.Code : order.RequestItem.Item.Unit.Code,
                              UnitCostD = order.UnitCost,
                              ServedQuantityD = context.PurchaseOrderNewLineItems
                                                       .Where(a => a.RequestItem.ItemId == order.RequestItem.ItemId
                                                                && a.RequestItem.ItemDescription == order.RequestItem.ItemDescription
                                                                && a.PurchaseOrder.Status.Name.ToLower() == "approved")
                                                       .Select(a => a.Quantity)
                                                       .Sum(),
                              RequestQuantityD = context.PurchaseRequestLineItems
                                                        .Where(a => a.ItemId == order.RequestItem.ItemId
                                                                 && a.ItemDescription == order.RequestItem.ItemDescription
                                                                 && a.PurchaseRequest.Status.Name.ToLower() == "approved"
                                                                 && a.PurchaseRequest.Id == order.RequestItem.PurchaseRequest.Id)
                                                        .Select(a => a.Quantity)
                                                        .Sum(),
                              CostUpdateCount = order.PurchaseOrderNewLineItemNewCosts.Count,
                              QuantityUpdateCount = order.PurchaseOrderNewLineItemNewQuantities.Count
                          };
            return await orderVM.ToListAsync();
        }

        public async Task<ForDataTables<PurchaseOrderLineItemVM>> GetItemListDTAsync(DataTablesRequestForm form, int purchaseRequestId)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetItemListAsync(purchaseRequestId)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(s => new[]
                {
                    s.LineNumber + "",
                    s.PurchaseRequestNumber,
                    s.ItemCode,
                    s.ItemPartNumber,
                    s.ItemDescription,
                    s.Quantity,
                    s.UnitCode,
                    s.UnitCost,
                    s.TotalAmount
                }.Any(a => a != null && a.ToLower().Contains(form.SearchValue.ToLower())));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                if (form.SortColumn == "Quantity" || form.SortColumn == "UnitCost" || form.SortColumn == "TotalAmount")
                {
                    if (form.SortColumnDirection == "asc")
                    {
                        switch (form.SortColumn)
                        {
                            case "Quantity": data = data.OrderBy(s => s.QuantityD); break;
                            case "UnitCost": data = data.OrderBy(s => s.UnitCostD); break;
                            case "TotalAmount": data = data.OrderBy(s => s.TotalAmountD); break;
                            default: break;
                        }
                    }
                    else if (form.SortColumnDirection == "desc")
                    {
                        switch (form.SortColumn)
                        {
                            case "Quantity": data = data.OrderByDescending(s => s.QuantityD); break;
                            case "UnitCost": data = data.OrderByDescending(s => s.UnitCostD); break;
                            case "TotalAmount": data = data.OrderByDescending(s => s.TotalAmountD); break;
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
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<PurchaseOrderLineItemVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public async Task<ResponseModel<int>> AddAsync(PurchaseOrderSaveVM form, IList<PurchaseOrderLineItemVM> Items)
        {
            try
            {

                var type = await context.PurchaseOrderTypes.Where(s => s.Id == form.TypeId).FirstOrDefaultAsync();
                if (type == null)
                {
                    return new ResponseModel<int> { Status = "Error", Message = "Invalid type", NewRow = -1 };
                }
                else
                {

                    var status = await context.PurchaseStatuses.Where(s => s.Name.ToLower() == form.Status.ToLower()).FirstOrDefaultAsync();
                    if (status == null)
                    {
                        return new ResponseModel<int> { Status = "Error", Message = "Invalid status", NewRow = -1 };
                    }
                    else
                    {
                        var previousOrder = await context.PurchaseOrderNews
                                                         .Where(s => s.Id == form.ResubmitId)
                                                         .FirstOrDefaultAsync();
                        var procurementHelper = new ProcurementHelper();

                        var docNumber = previousOrder == null
                                      ? await procurementHelper.GetNewOrder(context, type.Code, form.Status)
                                      : previousOrder.DocumentNumber.Split("-D")[0];

                        var order = new PurchaseOrderNew
                        {
                            DocumentNumber = docNumber,
                            TypeId = form.TypeId,
                            PurposeId = form.PurposeId,
                            SupplierId = form.SupplierId,
                            TermId = form.TermsId,
                            RegionId = form.RegionId,
                            CurrencyId = form.CurrencyId,
                            CurrencyRate = form.CurrencyRate,
                            DeliveryModeId = form.DeliveryModeId,
                            DeliveryAddressId = form.DeliveryAddressId,
                            OrderDate = form.DateOrdered,
                            CanvasSummaryReference = form.Reference + "",
                            Remarks = form.Remarks,
                            StatusId = status.Id,
                            Discount1 = form.Discount1,
                            Discount2 = form.Discount2,
                            Discount3 = form.Discount3,
                            IsVATIncluded = form.IsVATIncluded
                        };
                        await context.PurchaseOrderNews.AddAsync(order);
                        await context.SaveChangesAsync(currentUserId);

                        if (previousOrder != null)
                        {
                            var statusResubmitted = await context.PurchaseStatuses
                                                                 .Where(s => s.Name.ToLower() == "resubmitted")
                                                                 .FirstOrDefaultAsync();
                            previousOrder.ResubmitId = order.Id;
                            if (statusResubmitted != null)
                            {
                                previousOrder.StatusId = statusResubmitted.Id;
                            }
                            context.PurchaseOrderNews.Update(previousOrder);
                            await context.SaveChangesAsync(currentUserId);
                        }

                        await SaveItemsAsync(Items, order.Id);

                        return new ResponseModel<int>
                        {
                            Status = "Success",
                            Message = form.Status.ToLower() == "draft" ? "Purchase order saved as draft." : "Purchase order successfully submitted.",
                            NewRow = order.Id
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<int> { Status = "Error", Message = SystemFormat.GetInnermostExceptionMessage(ex), NewRow = -1 };
            }
        }

        public async Task<ResponseModel> SaveItemsAsync(IList<PurchaseOrderLineItemVM> Items, int orderId, string? status = null)
        {
            try
            {
                var order = await context.PurchaseOrderNews
                                         .Include(s => s.Status)
                                         .Include(s => s.Type)
                                         .Where(s => s.Id == orderId)
                                         .FirstOrDefaultAsync();

                if (order == null)
                {
                    return new ResponseModel { Status = "Error", Message = "Invalid purchase order" };
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(status))
                    {
                        int? statusId = context.PurchaseStatuses.Where(s => s.Name.ToLower() == status.ToLower()).Select(s => s.Id).FirstOrDefault();
                        if (statusId != null)
                            order.StatusId = statusId;

                        if (order.DocumentNumber.StartsWith("D") && order.Status.Name.ToLower() == "draft" && status.ToLower() != "draft")
                        {
                            var procurementHelper = new ProcurementHelper();
                            var docNumber = await procurementHelper.GetNewOrder(context, order.Type.Code, status);
                            order.DocumentNumber = docNumber;

                            await context.SaveChangesAsync(currentUserId);
                        }
                    }

                    Items = Items.OrderBy(x => x.LineNumber)
                                 .Select((item, index) =>
                                 {
                                     item.LineNumber = index + 1;
                                     return item;
                                 }).ToList();

                    foreach (var rename in Items)
                    {
                        var i = context.PurchaseRequestLineItems.Where(s => s.Id == rename.RequestItemId).FirstOrDefault();
                        if (i != null && i.ItemDescription != rename.ItemDescription)
                        {
                            i.ItemDescription = rename.ItemDescription;
                        }
                    }

                    var citems = context.PurchaseOrderNewLineItems.Where(s => s.PurchaseOrderId == orderId);
                    // add item
                    // check items not in db
                    var items = citems.Select(s => s.Id);
                    // add excluding items already available
                    var newitems = Items.Where(s => items.Contains(s.Id) == false)
                                       .Select(s => new PurchaseOrderNewLineItem
                                       {
                                           PurchaseOrderId = orderId,
                                           RequestItemId = s.RequestItemId,
                                           LineNumber = s.LineNumber,
                                           Quantity = s.QuantityD,
                                           UnitCost = s.UnitCostD
                                       });
                    context.PurchaseOrderNewLineItems.AddRange(newitems);

                    // update item
                    // get items available in db
                    var existitems = Items.Where(s => items.Contains(s.Id));
                    foreach (var ei in existitems)
                    {
                        // update all available in db
                        var dbitem = await citems.Where(s => s.Id == ei.Id).FirstAsync();
                        dbitem.RequestItemId = ei.RequestItemId;
                        dbitem.LineNumber = ei.LineNumber;
                        dbitem.Quantity = ei.QuantityD;
                        dbitem.UnitCost = ei.UnitCostD;

                        context.PurchaseOrderNewLineItems.Update(dbitem);
                    }

                    // delete item
                    // get items not in form
                    var fitems = Items.Select(s => s.Id).ToList();
                    var ritems = citems.Where(s => fitems.Contains(s.Id) == false);
                    if (ritems.Any())
                    {
                        // remove unavailable items
                        context.PurchaseOrderNewLineItems.RemoveRange(ritems);
                    }

                    await context.SaveChangesAsync(currentUserId);

                    return new ResponseModel { Status = "Success", Message = "Order item(s) successfully saved" };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { Status = "Error", Message = SystemFormat.GetInnermostExceptionMessage(ex) };
            }
        }

        public async Task<ResponseModel> UpdateCostAsync(POItemCostVM cost)
        {
            try
            {
                if (cost.OldCostD == cost.NewCostD)
                {
                    return new ResponseModel { Status = "Error", Message = "No change for cost, nothing so save" };
                }
                else
                {
                    var poitem = await context.PurchaseOrderNewLineItems
                                              .Include(s => s.RequestItem)
                                              .Where(s => s.Id == cost.POItemId)
                                              .FirstOrDefaultAsync();

                    if (poitem == null)
                    {
                        return new ResponseModel { Status = "Error", Message = "Invalid Item Record" };
                    }
                    else if (poitem.UnitCost != cost.OldCostD)
                    {
                        return new ResponseModel { Status = "Error", Message = "Invalid old cost. The old cost should be " + poitem.UnitCost + ", but you are saving it as " + cost.OldCostD + ". Please reload the page and try again." };
                    }
                    else
                    {
                        var po = await context.PurchaseOrderNews
                                              .Include(s => s.Status)
                                              .Where(s => s.Id == poitem.PurchaseOrderId)
                                              .FirstOrDefaultAsync();
                        if (po == null)
                        {
                            return new ResponseModel { Status = "Error", Message = "Invalid Record" };
                        }
                        else if (po.Status.Name.ToLower() != "approved")
                        {
                            return new ResponseModel { Status = "Error", Message = "This function is only available for approved PO" };
                        }
                        else if (await context.PurchaseOrderNewLineItemNewCosts
                                              .Where(s => s.OrderItemId == poitem.Id)
                                              .CountAsync() >= 3)
                        {
                            return new ResponseModel { Status = "Error", Message = "You are only allowed to update the cost for a maximum of 3 times only." };
                        }
                        else
                        {
                            var newcost = new PurchaseOrderNewLineItemNewCost
                            {
                                OrderItemId = poitem.Id,
                                PreviousCost = poitem.UnitCost,
                                NewCost = cost.NewCostD,
                                Remarks = cost.Remarks
                            };

                            await context.PurchaseOrderNewLineItemNewCosts.AddAsync(newcost);
                            await context.SaveChangesAsync(currentUserId);

                            poitem.UnitCost = cost.NewCostD;
                            context.PurchaseOrderNewLineItems.Update(poitem);
                            await context.SaveChangesAsync(currentUserId);

                            return new ResponseModel { Status = "Success", Message = "Unit cost for " + poitem.RequestItem.ItemDescription + " is now changed to " + cost.NewCost };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { Status = "Error", Message = SystemFormat.GetInnermostExceptionMessage(ex) };
            }
        }


        public async Task<IList<POItemCostVM>> GetCostUpdateListAsync(int orderItemId)
        {
            var item = from pocost in context.PurchaseOrderNewLineItemNewCosts
                       join user in userManager.Users on pocost.UserCreated equals user.Id
                       where pocost.OrderItemId == orderItemId
                       select new POItemCostVM
                       {
                           Id = pocost.Id,
                           Item = pocost.OrderItem.RequestItem.ItemDescription,
                           OldCostD = pocost.PreviousCost,
                           NewCostD = pocost.NewCost,
                           Remarks = pocost.Remarks,
                           UserCreated = user.FirstName + " " + user.LastName,
                           DateCreatedD = pocost.DateCreated
                       };
            return await item.ToListAsync();
        }

        public async Task<ResponseModel> UpdateQuantityAsync(POItemQuantityVM quantity)
        {
            try
            {
                if (quantity.OldQuantity == quantity.NewQuantity)
                {
                    return new ResponseModel { Status = "Error", Message = "No change for quantity, nothing so save" };
                }
                else
                {
                    var poitem = await context.PurchaseOrderNewLineItems
                                              .Include(s => s.RequestItem)
                                              .Where(s => s.Id == quantity.POItemId)
                                              .FirstOrDefaultAsync();
                    if (poitem == null)
                    {
                        return new ResponseModel { Status = "Error", Message = "Invalid Item Record" };
                    }
                    else if (poitem.Quantity != quantity.OldQuantity)
                    {
                        return new ResponseModel { Status = "Error", Message = "Invalid Old Quantity" };
                    }
                    else
                    {
                        var po = await context.PurchaseOrderNews
                                              .Include(s => s.Status)
                                              .Where(s => s.Id == poitem.PurchaseOrderId)
                                              .FirstOrDefaultAsync();
                        if (po == null)
                        {
                            return new ResponseModel { Status = "Error", Message = "Invalid Record" };
                        }
                        else if (po.Status.Name.ToLower() != "approved")
                        {
                            return new ResponseModel { Status = "Error", Message = "This function is only available for approved PO" };
                        }
                        else if (await context.PurchaseOrderNewLineItemNewQuantities
                                              .Where(s => s.OrderItemId == poitem.Id)
                                              .CountAsync() >= 3)
                        {
                            return new ResponseModel { Status = "Error", Message = "You are only allowed to update the quantity for a maximum of 3 times only." };
                        }
                        else
                        {
                            var qty = new PurchaseOrderNewLineItemNewQuantity
                            {
                                OrderItemId = poitem.Id,
                                PreviousQuantity = poitem.Quantity,
                                NewQuantity = quantity.NewQuantity,
                                Remarks = quantity.Remarks
                            };

                            await context.PurchaseOrderNewLineItemNewQuantities.AddAsync(qty);
                            await context.SaveChangesAsync(currentUserId);

                            var itemId = poitem.RequestItem.ItemId;

                            poitem.Quantity = quantity.NewQuantity;
                            context.PurchaseOrderNewLineItems.Update(poitem);
                            await context.SaveChangesAsync(currentUserId);

                            return new ResponseModel { Status = "Success", Message = "Quantity for " + poitem.RequestItem.ItemDescription + " is now changed to " + quantity.NewQuantity };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { Status = "Error", Message = SystemFormat.GetInnermostExceptionMessage(ex) };
            }
        }

        public async Task<IList<POItemQuantityVM>> GetQuantityUpdateListAsync(int orderItemId)
        {
            var item = from poqty in context.PurchaseOrderNewLineItemNewQuantities
                       join user in userManager.Users on poqty.UserCreated equals user.Id
                       where poqty.OrderItemId == orderItemId
                       select new POItemQuantityVM
                       {
                           Id = poqty.Id,
                           Item = poqty.OrderItem.RequestItem.ItemDescription,
                           OldQuantity = poqty.PreviousQuantity,
                           NewQuantity = poqty.NewQuantity,
                           Remarks = poqty.Remarks,
                           UserCreated = user.FirstName + " " + user.LastName,
                           DateCreatedD = poqty.DateCreated
                       };
            return await item.ToListAsync();
        }
        public async Task<ResponseModel> ApproveAsync(int orderId)
        {
            try
            {
                var order = context.PurchaseOrderNews.Where(s => s.Id == orderId).FirstOrDefault();
                if (order == null)
                {
                    return new ResponseModel { Status = "Error", Message = "Invalid purchase order" };
                }
                else
                {
                    int statusId = context.PurchaseStatuses.Where(s => s.Name.ToLower() == "approved").Select(s => s.Id).First();

                    if (!string.IsNullOrWhiteSpace(order.ApproverId))
                    {
                        return new ResponseModel { Status = "Error", Message = "Purchase order already approved." };
                    }
                    else
                    {
                        order.ApproverId = currentUserId;
                        order.StatusId = statusId;

                        context.PurchaseOrderNews.Update(order);
                        await context.SaveChangesAsync(currentUserId);

                        return new ResponseModel { Status = "Success", Message = "Purchase order successfully approved." };
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { Status = "Error", Message = SystemFormat.GetInnermostExceptionMessage(ex) };
            }
        }

        public async Task<ResponseModel> DisapproveAsync(int orderId, string? reason)
        {
            try
            {
                var order = context.PurchaseOrderNews.Where(s => s.Id == orderId).FirstOrDefault();
                if (order == null)
                {
                    return new ResponseModel { Status = "Error", Message = "Invalid purchase order" };
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(order.DisapproverId))
                    {
                        return new ResponseModel { Status = "Error", Message = "Purchase order already disapproved." };
                    }
                    else
                    {
                        var tempDocumentNumber = order.DocumentNumber + "-D";
                        var latestDisapprovedOrder = context.PurchaseOrderNews
                                                            .Where(s => s.DocumentNumber.StartsWith(tempDocumentNumber))
                                                            .OrderByDescending(s => s.DocumentNumber)
                                                            .Select(s => s.DocumentNumber)
                                                            .FirstOrDefault();
                        var latestDisapprovedDocumentNumber = latestDisapprovedOrder == null
                                                            ? 0
                                                            : int.Parse(latestDisapprovedOrder.Replace(tempDocumentNumber, ""));
                        var disapprovedDocumentNumber = tempDocumentNumber + string.Format("{0:00}", latestDisapprovedDocumentNumber + 1);

                        int statusId = context.PurchaseStatuses.Where(s => s.Name.ToLower() == "disapproved").Select(s => s.Id).First();

                        order.DocumentNumber = disapprovedDocumentNumber;
                        order.DisapproverId = currentUserId;
                        order.DisapprovedReason = reason;
                        order.StatusId = statusId;

                        context.PurchaseOrderNews.Update(order);
                        await context.SaveChangesAsync(currentUserId);

                        return new ResponseModel { Status = "Success", Message = "Purchase order successfully disapproved." };
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { Status = "Error", Message = SystemFormat.GetInnermostExceptionMessage(ex) };
            }
        }

        public async Task<ResponseModel> CancelAsync(int orderId)
        {
            try
            {
                var existingOrder = await context.PurchaseOrderNews
                                                 .Include(s => s.Status)
                                                 .Include(s => s.PurchaseReceivings)
                                                 .FirstOrDefaultAsync(pr => pr.Id == orderId);

                if (existingOrder == null)
                {
                    return new ResponseModel { Status = "Error", Message = "Invalid Purchase Order. Record can not be found" };
                }
                else if (existingOrder.Status.Name.ToLower() != "submitted")
                {
                    return new ResponseModel { Status = "Error", Message = "Only submitted can be cancelled" };
                }
                else if (existingOrder.PurchaseReceivings.Count > 0)
                {
                    var usedIn = existingOrder.PurchaseReceivings.Select(s => s.DocumentNumber);
                    return new ResponseModel { Status = "Error", Message = "Record already used in Purchase Receiving. Cancel cannot proceed<ul><li>" + string.Join("</li><li>", usedIn) + "</li>" };
                }
                else
                {
                    var draftStatus = await context.PurchaseStatuses.Where(s => s.Name.Trim().ToLower() == "draft").Select(s => s.Id).FirstAsync();

                    existingOrder.StatusId = draftStatus;

                    context.PurchaseOrderNews.Update(existingOrder);
                    await context.SaveChangesAsync(currentUserId);

                    return new ResponseModel { Status = "Success", Message = "Purchase Order successfully cancelled" };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { Status = "Error", Message = SystemFormat.GetInnermostExceptionMessage(ex) };
            }
        }

        public async Task<ResponseModel> DeleteAsync(int orderId)
        {
            try
            {
                var existingOrder = await context.PurchaseOrderNews
                                                 .Include(s => s.Status)
                                                 .Include(s => s.PurchaseReceivings)
                                                 .FirstOrDefaultAsync(pr => pr.Id == orderId);

                if (existingOrder == null)
                {
                    return new ResponseModel { Status = "Error", Message = "Invalid Purchase Order. Record can not be found" };
                }
                else if (existingOrder.Status.Name.ToLower() != "draft")
                {
                    return new ResponseModel { Status = "Error", Message = "Only draft can be deleted" };
                }
                else if (existingOrder.PurchaseReceivings.Count > 0)
                {
                    return new ResponseModel { Status = "Error", Message = "Record already used in Purchase Receiving. Cancel cannot proceed" };
                }
                else
                {

                    var previousOrder = await context.PurchaseOrderNews
                                                         .Where(s => s.ResubmitId == orderId)
                                                         .FirstOrDefaultAsync();
                    if (previousOrder != null)
                    {
                        var statusDisapproved = await context.PurchaseStatuses
                                                             .Where(s => s.Name.ToLower() == "disapproved")
                                                             .FirstAsync();
                        previousOrder.ResubmitId = null;
                        previousOrder.StatusId = statusDisapproved.Id;
                        await context.SaveChangesAsync(currentUserId);
                    }

                    var orderItems = await context.PurchaseOrderNewLineItems
                                                      .Where(s => s.PurchaseOrderId == orderId)
                                                      .ToListAsync();

                    context.PurchaseOrderNewLineItems.RemoveRange(orderItems);

                    context.PurchaseOrderNews.Remove(existingOrder);
                    await context.SaveChangesAsync(currentUserId);

                    return new ResponseModel { Status = "Success", Message = "Purchase Order successfully deleted" };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { Status = "Error", Message = SystemFormat.GetInnermostExceptionMessage(ex) };
            }
        }
    }
}