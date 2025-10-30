using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Linq;

namespace GMGInventory.Application.Repositories
{
    public class PurchaseReceiveReporitory : IPurchaseReceiveRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;

        public PurchaseReceiveReporitory(ApplicationDbContext context
                                       , IHttpContextAccessor httpContextAccessor
                                       , UserManager<User> userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        public async Task<IList<PurchaseReceiveVM>> GetListAsync(PurchaseReceiveFilter filter)
        {
            var receiveVM = (from receive in context.PurchaseReceivings
                             where (filter.SupplierId == null || receive.PurchaseOrder.SupplierId == filter.SupplierId)
                             && (filter.WarehouseId == null || receive.WarehouseId == filter.WarehouseId)
                             && (filter.DateFrom == null || receive.ReceiveDate >= filter.DateFrom)
                             && (filter.DateTo == null || receive.ReceiveDate <= filter.DateTo)
                             && (filter.StatusId == null || receive.StatusId == filter.StatusId)
                             select new PurchaseReceiveVM
                             {
                                 Id = receive.Id,
                                 DocumentNumber = receive.DocumentNumber,
                                 Supplier = receive.PurchaseOrder.Supplier.Code,
                                 Term = receive.PurchaseOrder.Term.Name,
                                 PONumber = receive.PurchaseOrder.DocumentNumber,
                                 Warehouse = receive.Warehouse.Code,
                                 Status = receive.Status.Code,
                                 ReceiveDateD = receive.ReceiveDate,
                                 Remarks = receive.Remarks,
                                 HashMicroMoveId = receive.HashMicroMoveId,
                                 HashMicroMoveName = receive.HashMicroMoveName,
                                 HashMicroMoveStatus = receive.HashMicroMoveStatus,
                                 HashMicroMoveBy = receive.HashMicroMoveBy == null ? null : receive.HashMicroMoveBy.FirstName + " " + receive.HashMicroMoveBy.LastName,
                                 HashMicroMoveDateD = receive.HashMicroMoveDate
                             });
            return await receiveVM.ToListAsync();
        }

        public async Task<ForDataTables<PurchaseReceiveVM>> GetListDTAsync(DataTablesRequestForm form, PurchaseReceiveFilter filter)
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
                    s.Supplier,
                    s.Term,
                    s.PONumber,
                    s.Warehouse,
                    s.Status,
                    s.ReceiveDate,
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
                            case "ReceiveDate": data = data.OrderBy(s => s.ReceiveDateD); break;
                            default: break;
                        }
                    }
                    else if (form.SortColumnDirection == "desc")
                    {
                        switch (form.SortColumn)
                        {
                            case "ReceiveDate": data = data.OrderByDescending(s => s.ReceiveDateD); break;
                            default: break;
                        }
                    }
                }
                else
                {
                    data = data.OrderBy(form.SortColumn + " " + form.SortColumnDirection);
                }
            }
            var totalReceivingRecord = await context.PurchaseReceivings
                                                    .Where(s => s.Status.Code.ToLower() == "approved"
                                                             && filter.DateFrom <= s.ReceiveDate.Date
                                                             && s.ReceiveDate.Date <= filter.DateTo)
                                                    .CountAsync();
            var totalReceivingNotMovedRecord = await context.PurchaseReceivings
                                                            .Where(s => s.Status.Code.ToLower() == "approved"
                                                                     && filter.DateFrom <= s.ReceiveDate.Date
                                                                     && s.ReceiveDate.Date <= filter.DateTo
                                                                     && s.HashMicroMoveById == null)
                                                            .CountAsync();

            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<PurchaseReceiveVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList,
                TotalQuantity = totalReceivingRecord,
                TotalQuantity2 = totalReceivingNotMovedRecord
            };
        }

        public async Task<PurchaseReceiveDetailsVM?> GetDetails(int receivingId)
        {
            var details = from receive in context.PurchaseReceivings

                          let resubmitfrom = context.PurchaseReceivings
                                                    .Where(s => s.ResubmitId == receive.Id)
                                                    .FirstOrDefault()

                          let po = context.PurchaseOrderNewLineItems
                                                  .Where(s => s.PurchaseOrderId == receive.PurchaseOrderId)
                                                  .FirstOrDefault()

                          join userc in userManager.Users on receive.UserCreated equals userc.Id
                          join userm in userManager.Users on receive.UserModified equals userm.Id into m
                          from userm in m.DefaultIfEmpty()
                          where receive.Id == receivingId
                          select new PurchaseReceiveDetailsVM
                          {
                              Id = receive.Id,
                              DocumentNumber = receive.DocumentNumber,
                              PONumber = receive.PurchaseOrder.DocumentNumber,
                              POId = receive.PurchaseOrder.Id,
                              Supplier = receive.PurchaseOrder.Supplier.Name,
                              SupplierId = receive.PurchaseOrder.Supplier.Id,
                              Term = receive.PurchaseOrder.Term.Name,
                              TermId = receive.PurchaseOrder.Term.Id,
                              DRNumber = receive.DeliveryReceiptNumber,
                              SINumber = receive.SalesInvoiceNumber,
                              Discount1D = receive.PurchaseOrder.Discount1,
                              Discount2D = receive.PurchaseOrder.Discount2,
                              Discount3D = receive.PurchaseOrder.Discount3,
                              IsVATIncluded = receive.PurchaseOrder.IsVATIncluded ? "Yes" : "No",
                              PurchaseOrderType = receive.PurchaseOrder.Type.Name,
                              Warehouse = receive.Warehouse.Code,
                              WarehouseId = receive.Warehouse.Id,
                              Status = receive.Status.Code,
                              ReceiveDateD = receive.ReceiveDate,
                              Remarks = receive.Remarks,
                              Currency = receive.PurchaseOrder.Currency.Name,
                              CurrencyId = receive.PurchaseOrder.Currency.Id,
                              CurrencyRateD = receive.PurchaseOrder.CurrencyRate,
                              ApprovedBy = receive.ApproverId == null ? null : SystemFormat.Name(receive.Approver.FirstName, receive.Approver.LastName, receive.Approver.Nickname),
                              DisapprovedBy = receive.DisapproverId == null ? null : SystemFormat.Name(receive.Disapprover.FirstName, receive.Disapprover.LastName, receive.Disapprover.Nickname),
                              DisapprovedReason = receive.DisapprovedReason,
                              ResubmitId = receive.ResubmitId,
                              ResubmitFromId = resubmitfrom == null ? null : resubmitfrom.Id,
                              UserCreated = SystemFormat.Name(userc.FirstName, userc.LastName, userc.Nickname),
                              DateCreatedD = receive.DateCreated,
                              UserModified = userm == null ? null : SystemFormat.Name(userm.FirstName, userm.LastName, userm.Nickname),
                              DateModifiedD = receive.DateModified,
                              HashMicroMoveId = receive.HashMicroMoveId,
                              HashMicroMoveName = receive.HashMicroMoveName,
                              HashMicroMoveStatus = receive.HashMicroMoveStatus,
                              HashMicroMoveBy = receive.HashMicroMoveBy == null ? null : receive.HashMicroMoveBy.FirstName + " " + receive.HashMicroMoveBy.LastName,
                              HashMicroMoveDateD = receive.HashMicroMoveDate
                          };
            return await details.FirstOrDefaultAsync();
        }

        public async Task<IList<PurchaseReceiveLineItemVM>> GetItemsFromPOListAsync(int purchaseOrderId) // for create
        {
            var receiveVM = from order in context.PurchaseOrderNewLineItems
                            let receive = context.PurchaseReceivingLineItems
                                         .Where(s => s.OrderItemId == order.Id).FirstOrDefault()

                            where order.PurchaseOrderId == purchaseOrderId

                            select new PurchaseReceiveLineItemVM
                            {
                                Id = receive == null ? 0 : receive.Id,
                                ToReceive = receive != null,
                                PurchaseReceivingId = receive != null ? receive.PurchaseReceivingId : 0,
                                LineNumber = order.LineNumber,
                                OrderItemId = order.Id,
                                ItemId = order.RequestItem.Item == null ? null : order.RequestItem.Item.Id,
                                ItemCode = order.RequestItem.Item == null ? null : order.RequestItem.Item.Code,
                                ItemPartNumber = order.RequestItem.Item == null ? null : order.RequestItem.Item.PartNumber,
                                ItemDescription = order.RequestItem.ItemDescription,
                                QuantityD = receive == null ? 0 : receive.Quantity,
                                OrderQuantityD = order.Quantity,
                                ServedQuantityD = context.PurchaseReceivingLineItems
                                                       .Where(a => a.PurchaseReceiving.Status.Name.ToLower() == "approved"
                                                                    && a.OrderItemId == order.Id)
                                                       .Select(a => a.Quantity)
                                                       .Sum(),

                                UnitId = order.RequestItem.Item == null ? order.RequestItem.Unit.Id : order.RequestItem.Item.Unit.Id,
                                UnitCode = order.RequestItem.Item == null ? order.RequestItem.Unit.Code : order.RequestItem.Item.Unit.Code,
                                UnitCostD = order.UnitCost
                            };
            return await receiveVM.ToListAsync();
        }

        public async Task<IList<PurchaseReceiveLineItemVM>> GetWithPOItemsListAsync(int purchaseReceivingId) // for details
        {
            var orderId = await context.PurchaseReceivings
                                             .Where(s => s.Id == purchaseReceivingId)
                                             .Select(s => s.PurchaseOrderId)
                                             .FirstOrDefaultAsync();

            var receiveVM = from order in context.PurchaseOrderNewLineItems
                            let receive = context.PurchaseReceivingLineItems
                                         .Where(s => s.OrderItemId == order.Id && s.PurchaseReceivingId == purchaseReceivingId).FirstOrDefault()

                            where order.PurchaseOrderId == orderId

                            select new PurchaseReceiveLineItemVM
                            {
                                Id = receive == null ? 0 : receive.Id,
                                ToReceive = receive != null,
                                PurchaseReceivingId = receive != null ? receive.PurchaseReceivingId : 0,
                                LineNumber = order.LineNumber,
                                OrderItemId = order.Id,
                                ItemId = order.RequestItem.Item == null ? null : order.RequestItem.Item.Id,
                                ItemCode = order.RequestItem.Item == null ? null : order.RequestItem.Item.Code,
                                ItemPartNumber = order.RequestItem.Item == null ? null : order.RequestItem.Item.PartNumber,
                                ItemDescription = order.RequestItem.ItemDescription,
                                QuantityD = receive == null ? 0 : receive.Quantity,
                                OrderQuantityD = order.Quantity,
                                ServedQuantityD = context.PurchaseReceivingLineItems
                                                       .Where(a => a.PurchaseReceiving.Status.Name.ToLower() == "approved"
                                                                    && a.OrderItemId == order.Id)
                                                       .Select(a => a.Quantity)
                                                       .Sum(),

                                UnitId = order.RequestItem.Item == null ? order.RequestItem.Unit.Id : order.RequestItem.Item.Unit.Id,
                                UnitCode = order.RequestItem.Item == null ? order.RequestItem.Unit.Code : order.RequestItem.Item.Unit.Code,
                                UnitCostD = order.UnitCost
                            };
            return await receiveVM.ToListAsync();
        }
        public async Task<IList<PurchaseReceiveLineItemVM>> GetItemListAsync(int purchaseReceivingId) // for printing 
        {
            var receiveVM = from receive in context.PurchaseReceivingLineItems
                            join order in context.PurchaseOrderNewLineItems on receive.OrderItemId equals order.Id

                            where receive.PurchaseReceivingId == purchaseReceivingId
                            orderby order.LineNumber

                            select new PurchaseReceiveLineItemVM
                            {
                                Id = receive == null ? 0 : receive.Id,
                                ToReceive = receive != null,
                                PurchaseReceivingId = receive != null ? receive.PurchaseReceivingId : 0,
                                LineNumber = order.LineNumber,
                                OrderItemId = order.Id,
                                ItemId = order.RequestItem.Item == null ? null : order.RequestItem.Item.Id,
                                ItemCode = order.RequestItem.Item == null ? null : order.RequestItem.Item.Code,
                                ItemPartNumber = order.RequestItem.Item == null ? null : order.RequestItem.Item.PartNumber,
                                ItemDescription = order.RequestItem.ItemDescription,
                                QuantityD = receive == null ? 0 : receive.Quantity,
                                OrderQuantityD = order.Quantity,
                                UnitId = order.RequestItem.Item == null ? order.RequestItem.Unit.Id : order.RequestItem.Item.Unit.Id,
                                UnitCode = order.RequestItem.Item == null ? order.RequestItem.Unit.Code : order.RequestItem.Item.Unit.Code,
                                UnitCostD = order.UnitCost
                            };
            return await receiveVM.ToListAsync();
        }

        public async Task<ForDataTables<PurchaseReceiveLineItemVM>> GetItemListDTAsync(DataTablesRequestForm form, int purchaseReceiveId)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetItemListAsync(purchaseReceiveId)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(s => new[]
                {
                    s.LineNumber + "",
                    s.ItemCode,
                    s.ItemDescription,
                    s.Quantity,
                    s.UnitCode,
                }.Any(a => a != null && a.ToLower().Contains(form.SearchValue.ToLower())));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                data = data.OrderBy(form.SortColumn + " " + form.SortColumnDirection);
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<PurchaseReceiveLineItemVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }
        public async Task<ResponseModel> SaveItemsAsync(IList<PurchaseReceiveLineItemVM> Items, int receiveId, string? status = null)
        {
            var response = new ResponseModel();

            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

            var request = await context.PurchaseReceivings.Where(s => s.Id == receiveId).FirstOrDefaultAsync();

            if (request == null)
            {
                response.Status = "Error";
                response.Message = "Invalid purchase receive";
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(status))
                {
                    int statusId = context.PurchaseStatuses.Where(s => s.Name.ToLower() == status.ToLower()).Select(s => s.Id).First();
                    request.StatusId = statusId;
                }

                try
                {
                    var citems = context.PurchaseReceivingLineItems.Where(s => s.PurchaseReceivingId == receiveId);
                    // add item
                    // check items not in db
                    var items = citems.Select(s => s.Id);
                    // add excluding items already available
                    var newitems = Items.Where(s => items.Contains(s.Id) == false)
                                       .Select(s => new PurchaseReceivingLineItem
                                       {
                                           PurchaseReceivingId = receiveId,
                                           OrderItemId = s.OrderItemId,
                                           Quantity = s.QuantityD
                                       });
                    context.PurchaseReceivingLineItems.AddRange(newitems);

                    // update item
                    // get items available in db
                    var existitems = Items.Where(s => items.Contains(s.Id));
                    foreach (var ei in existitems)
                    {
                        // update all available in db
                        var dbitem = await citems.Where(s => s.Id == ei.Id).FirstAsync();
                        //dbitem.LineNumber = ei.LineNumber;
                        dbitem.Quantity = ei.QuantityD;
                        //dbitem.ItemDescription = ei.ItemDescription ?? "";

                        context.PurchaseReceivingLineItems.Update(dbitem);
                    }

                    // delete item
                    // get items not in form
                    var fitems = Items.Select(s => s.Id);
                    var ritems = citems.Where(s => fitems.Contains(s.Id) == false);
                    // remove unavailable items
                    context.PurchaseReceivingLineItems.RemoveRange(ritems);

                    await context.SaveChangesAsync(currentUserId);

                    response.Status = "Success";
                    response.Message = "Receive item(s) successfully saved";
                }
                catch (Exception ex)
                {
                    response.Status = "Error";
                    response.Message = SystemFormat.GetInnermostExceptionMessage(ex);
                }
            }
            return response;
        }

        public async Task<ResponseModel<int>> AddAsync(PurchaseReceiveSaveVM form, List<PurchaseReceiveLineItemVM> Items)
        {
            var response = new ResponseModel<int>();
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

            var status = await context.PurchaseStatuses.Where(s => s.Name.ToLower() == form.Status.ToLower()).FirstOrDefaultAsync();
            if (status == null)
            {
                response.Status = "Error";
                response.Message = "Invalid status.";
                response.NewRow = -1;
            }
            else
            {
                var purchaseOrder = await context.PurchaseOrderNews
                                                   .Where(s => s.Id == form.PurchaseOrderId)
                                                   .FirstOrDefaultAsync();
                var previousReceive = await context.PurchaseReceivings
                                                      .Where(s => s.Id == form.ResubmitId)
                                                      .FirstOrDefaultAsync();

                var receive = new PurchaseReceiving
                {
                    DocumentNumber = form.DocumentNumber,
                    DeliveryReceiptNumber = form.DeliveryReceiptNumber,
                    SalesInvoiceNumber = form.SalesInvoiceNumber,
                    ReceiveDate = form.DateReceived,
                    Remarks = form.Remarks,
                    StatusId = status.Id,
                    ResubmitId = form.ResubmitId,

                    PurchaseOrderId = purchaseOrder!.Id,
                    WarehouseId = purchaseOrder!.DeliveryAddressId
                };
                await context.PurchaseReceivings.AddAsync(receive);
                await context.SaveChangesAsync(currentUserId);

                if (previousReceive != null)
                {
                    var statusResubmitted = await context.PurchaseStatuses
                                                         .Where(s => s.Name.ToLower() == "resubmitted")
                                                         .FirstOrDefaultAsync();
                    previousReceive.ResubmitId = receive.Id;
                    if (statusResubmitted != null)
                    {
                        previousReceive.StatusId = statusResubmitted.Id;
                    }
                    context.PurchaseReceivings.Update(previousReceive);
                    await context.SaveChangesAsync(currentUserId);
                }

                await SaveItemsAsync(Items, receive.Id);

                response.Status = "Success";
                response.Message = form.Status.ToLower() == "draft" ? "Purchase receive saved as draft." : "Purchase receive successfully submitted.";
                response.NewRow = receive.Id;
            }

            return response;
        }

        public async Task<IList<JsonOptionsModel>> GetPurchaseOrderOptions(int? SelectedId)
        {
            var incompleteOrders = (from poli in context.PurchaseOrderNewLineItems
                                    let recq = (from prli in context.PurchaseReceivingLineItems
                                                where prli.OrderItemId == poli.Id
                                                select (int?)prli.Quantity).Sum() ?? 0
                                    let retq = (from prli in context.PurchaseReturnNewLineItems
                                                where prli.ReceivingItem.OrderItemId == poli.Id
                                                select (int?)prli.Quantity).Sum() ?? 0
                                    where poli.Quantity - recq + retq > 0
                                    select new
                                    {
                                        Id = poli.PurchaseOrder.Id,
                                        Text = poli.PurchaseOrder.DocumentNumber
                                    })
                                    .Distinct()
                                    .OrderBy(s => s.Text)
                                    .Select(x => new JsonOptionsModel
                                    {
                                        Id = x.Id.ToString(),
                                        Text = x.Text,
                                        Selected = SelectedId != null && x.Id == SelectedId
                                    });

            return await incompleteOrders.ToListAsync();
        }

        public async Task<Select2Model<JsonPurchaseOptionModel>> GetPurchaseOrderOptions2(string search, int page)
        {
            var orders =
                from poli in context.PurchaseOrderNewLineItems
                where poli.PurchaseOrder.Status.Name.ToLower() == "approved"
                   && (string.IsNullOrWhiteSpace(search) || poli.PurchaseOrder.DocumentNumber.Contains(search))
                group poli by new { poli.PurchaseOrder.Id, poli.PurchaseOrder.DocumentNumber } into g
                orderby g.Key.Id ascending
                select new
                {
                    g.Key.Id,
                    g.Key.DocumentNumber,
                    TotalQuantity = g.Sum(s => s.Quantity)
                };

            var filteredOrders = await orders
                .Skip((page - 1) * 50)
                .Take(50)
                .ToListAsync();

            var filteredOrdersOn = filteredOrders.Select(s =>
            {
                var recq = context.PurchaseReceivingLineItems
                                  .Where(prli => prli.OrderItem.PurchaseOrder.Id == s.Id
                                              && prli.PurchaseReceiving.Status.Name.ToLower() == "approved")
                                  .Sum(prli => (int?)prli.Quantity) ?? 0;

                var retq = context.PurchaseReturnNewLineItems
                                  .Where(prli => prli.ReceivingItem.OrderItem.PurchaseOrder.Id == s.Id
                                              && prli.PurchaseReturn.Status.Name.ToLower() == "approved")
                                  .Sum(prli => (int?)prli.Quantity) ?? 0;

                var alreadyOn = string.Join(", ", context.PurchaseReceivingLineItems
                                                         .Where(prli => prli.OrderItem.PurchaseOrder.Id == s.Id 
                                                                     && prli.PurchaseReceiving.Status.Name.ToLower() == "approved")
                                                         .Select(prli => prli.PurchaseReceiving.DocumentNumber)
                                                         .Distinct()
                                                         .ToList());

                return new JsonPurchaseOptionModel
                {
                    Id = s.Id.ToString(),
                    Text = s.DocumentNumber,
                    TotalQuantity = s.TotalQuantity,
                    TotalIncompleteQuantity = s.TotalQuantity - recq + retq,
                    Disabled = (s.TotalQuantity - recq + retq) <= 0,
                    AlreadyOn = alreadyOn
                };
            }).ToList();


            return new Select2Model<JsonPurchaseOptionModel>
            {
                //incomplete_results = false,
                items = filteredOrdersOn,
                total_count = orders.Count()
            };
        }

        public async Task<ResponseModel> ApproveAsync(int receivingId)
        {
            var response = new ResponseModel();

            var receive = context.PurchaseReceivings.Where(s => s.Id == receivingId).FirstOrDefault();
            if (receive == null)
            {
                response.Status = "Error";
                response.Message = "Invalid purchase receive";
            }
            else
            {
                string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
                int statusId = context.PurchaseStatuses.Where(s => s.Name.ToLower() == "approved").Select(s => s.Id).First();

                if (!string.IsNullOrWhiteSpace(receive.ApproverId))
                {
                    response.Status = "Error";
                    response.Message = "Purchase receive already approved.";
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(receive.ApproverId))
                        receive.ApproverId = currentUserId;

                    receive.StatusId = statusId;

                    context.PurchaseReceivings.Update(receive);

                    var preceiveItems = await context.PurchaseReceivingLineItems
                        .Include(x => x.PurchaseReceiving)
                        .Include(x => x.OrderItem).ThenInclude(x => x.RequestItem)
                        .Where(s => s.PurchaseReceivingId == receivingId).ToListAsync();

                    foreach (var receiving in preceiveItems)
                    {
                        var itemId = receiving.OrderItem.RequestItem.ItemId;
                        if (itemId != null)
                        {
                            var warehouseId = receiving.PurchaseReceiving.WarehouseId;

                            var itemInventory = await context.ItemInventories.Where(s => s.WarehouseId == warehouseId && s.ItemId == itemId).FirstOrDefaultAsync();

                            var previousCount = await context.PhysicalCountHistories.Where(s => s.WarehouseId == warehouseId && s.ItemId == itemId).OrderByDescending(s => s.Id).FirstOrDefaultAsync();

                            var countHistory = new PhysicalCountHistory
                            {
                                ItemId = itemId ?? 0,
                                QuantityFrom = previousCount != null ? previousCount.QuantityTo : itemInventory != null ? itemInventory.Quantity : 0,
                                QuantityTo = previousCount != null ? previousCount.QuantityTo + receiving.Quantity : receiving.Quantity,
                                Remarks = "Purchase Receiving",
                                WarehouseId = warehouseId
                            };
                            context.PhysicalCountHistories.Add(countHistory);

                            if (itemInventory != null)
                            {
                                itemInventory.Quantity += receiving.Quantity;
                                context.ItemInventories.Update(itemInventory);
                            }
                            else
                            {
                                var newItemInventory = new ItemInventory
                                {
                                    ItemId = itemId ?? 0,
                                    Quantity = receiving.Quantity,
                                    WarehouseId = warehouseId
                                };
                                await context.ItemInventories.AddAsync(newItemInventory);
                            }
                        }
                    }
                    ;
                    await context.SaveChangesAsync(currentUserId);
                }
                response.Status = "Success";
                response.Message = "Purchase receive successfully approved.";
            }
            return response;
        }

        public async Task<ResponseModel> DisapproveAsync(int receivingId, string? reason)
        {
            var response = new ResponseModel();


            var receive = context.PurchaseReceivings.Where(s => s.Id == receivingId).FirstOrDefault();
            if (receive == null)
            {
                response.Status = "Error";
                response.Message = "Invalid purchase receive";
            }
            else
            {
                string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
                int statusId = context.PurchaseStatuses.Where(s => s.Name.ToLower() == "disapproved").Select(s => s.Id).First();

                if (!string.IsNullOrWhiteSpace(receive.DisapproverId))
                {
                    response.Status = "Error";
                    response.Message = "Purchase receive already disapproved.";
                }
                else
                {
                    receive.DisapproverId = currentUserId;
                    receive.DisapprovedReason = reason;
                    receive.StatusId = statusId;

                    context.PurchaseReceivings.Update(receive);
                    await context.SaveChangesAsync(currentUserId);

                    response.Status = "Success";
                    response.Message = "Purchase receive successfully disapproved.";
                }
            }

            return response;
        }

        public async Task<string> GetNewDocumentNumber()
        {
            var docNumberPrefix = $"RR{DateTime.Now.Year.ToString().Substring(2)}-";
            var lastDocument = await context.PurchaseReceivings
                                            .Where(s => s.DocumentNumber.StartsWith(docNumberPrefix))
                                            .OrderByDescending(s => s.DocumentNumber)
                                            .Select(s => s.DocumentNumber)
                                            .FirstOrDefaultAsync();

            int.TryParse(lastDocument?.Replace(docNumberPrefix, "") ?? "0", out int docNumberLast);
            return $"{docNumberPrefix}{docNumberLast + 1:D6}";
        }

        public async Task<IList<JsonPurchaseItemOptionsModel>> GetPurchaseOrderItemOptions(int PurchaseOrderId)
        {
            return await context.PurchaseOrderNewLineItems
                                  .Include(s => s.RequestItem)
                                .Where(s => s.PurchaseOrderId == PurchaseOrderId)
                                .Select(s => new JsonPurchaseItemOptionsModel
                                {
                                    Id = s.Id.ToString(),
                                    Text = s.RequestItem.ItemDescription,
                                    ItemId = s.RequestItem.Item == null ? null : s.RequestItem.Item.Id,
                                    ItemCode = s.RequestItem.Item == null ? null : s.RequestItem.Item.Code,
                                    ItemDescription = s.RequestItem.ItemDescription,
                                    PartNumber = s.RequestItem.Item == null ? null : s.RequestItem.Item.PartNumber,
                                    Quantity = s.Quantity,
                                    Unit = s.RequestItem.Item == null ? s.RequestItem.Unit.Name : s.RequestItem.Item.Unit.Name,
                                    UnitId = s.RequestItem.Item == null ? s.RequestItem.Unit.Id : s.RequestItem.Item.Unit.Id,
                                    UnitCost = s.UnitCost,
                                    LineNumber = s.LineNumber
                                })
                                .OrderBy(s => s.LineNumber)
                                .ToListAsync();
        }

        public async Task<ResponseModelData<int>> AddAPI(PurchaseReceiveFormAPIVM form)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

            var status = await context.PurchaseStatuses.Where(s => s.Name.ToLower() == "draft").FirstAsync();

            var purchaseOrder = await context.PurchaseOrderNews
                                             .Where(s => s.Id == form.PurchaseOrderId)
                                             .FirstOrDefaultAsync();
            if (purchaseOrder == null)
                return new ResponseModelData<int>
                {
                    Status = "Error",
                    Message = "Invalid Purchase Order. Record cannot be found",
                    Data = -1
                };

            var purchaseOrderItems = from poli in context.PurchaseOrderNewLineItems
                                     let recq = (from prli in context.PurchaseReceivingLineItems
                                                 where prli.OrderItemId == poli.Id
                                                 select (int?)prli.Quantity).Sum() ?? 0
                                     let retq = (from prli in context.PurchaseReturnNewLineItems
                                                 where prli.ReceivingItem.OrderItemId == poli.Id
                                                 select (int?)prli.Quantity).Sum() ?? 0
                                     where poli.Quantity - recq + retq > 0
                                     select poli.Id;

            if (form.Items.Any(s => !purchaseOrderItems.Contains(s.OrderItemId)))
                return new ResponseModelData<int>
                {
                    Status = "Error",
                    Message = "Cannot proceed because there is a problem with one or more items",
                    Data = -1
                };

                var receive = new PurchaseReceiving
                {
                    DocumentNumber = await GetNewDocumentNumber(),
                    PurchaseOrderId = purchaseOrder.Id,
                    DeliveryReceiptNumber = form.DeliveryReceiptNumber,
                    SalesInvoiceNumber = form.SalesInvoiceNumber,
                    ReceiveDate = form.DateReceived,
                    Remarks = form.Remarks,
                    StatusId = status.Id
                };
                await context.PurchaseReceivings.AddAsync(receive);
                await context.SaveChangesAsync(currentUserId);


            var newitems = form.Items
                               .Select(s => new PurchaseReceivingLineItem
                               {
                                   PurchaseReceivingId = receive.Id,
                                   OrderItemId = s.OrderItemId,
                                   Quantity = s.Quantity
                               });
            context.PurchaseReceivingLineItems.AddRange(newitems);
            await context.SaveChangesAsync(currentUserId);

            return new ResponseModelData<int>
            {
                Status = "Success",
                Message = "Purchase receive saved as draft.",
                Data = receive.Id
            };
        }
    }
}