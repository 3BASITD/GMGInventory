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
    public class PurchaseOrderNewReporitory : IPurchaseOrderNewRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;

        public PurchaseOrderNewReporitory(ApplicationDbContext context
                                        , IHttpContextAccessor httpContextAccessor
                                        , UserManager<User> userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
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

        public async Task<IList<JsonOptionsModel>> GetPurchaseRequestOptions(int RequestTypeId, int? RequestPurposeId, int? SelectedId)
        {
            var requests = from prli in context.PurchaseRequestLineItems
                           let poli = prli.PurchaseOrderLineItems
                                          .Where(a => a.PurchaseOrder.Status.Name.ToLower() == "approved")
                                          .Select(a => (int?)a.Quantity)
                                          .Sum() ?? 0
                           where poli != prli.Quantity
                           select prli.PurchaseRequestId;


            return await context.PurchaseRequests
                                .Include(s => s.Status)
                                .Where(s => s.TypeId == RequestTypeId
                                         && (RequestPurposeId == null || s.PurposeId == RequestPurposeId)
                                         && s.Status.Name.ToLower() == "approved"
                                         && requests.Contains(s.Id))
                                .Select(s => new JsonOptionsModel
                                {
                                    Id = s.Id.ToString(),
                                    Text = s.DocumentNumber,
                                    Selected = SelectedId != null && s.Id == SelectedId
                                })
                                .OrderBy(s => s.Text)
                                .ToListAsync();
        }

        public async Task<IList<JsonPurchaseItemOptionsModel>> GetPurchaseRequestItemOptions(int RequestId, int? SelectedId)
        {
                var requests = from prli in context.PurchaseRequestLineItems
                               let poli = prli.PurchaseOrderLineItems
                                              .Where(a => a.PurchaseOrder.Status.Name.ToLower() == "approved")
                                              .Select(a => (int?)a.Quantity)
                                              .Sum() ?? 0
                               where prli.PurchaseRequestId == RequestId
                               && poli != prli.Quantity
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
                                   Served = poli,
                                   Selected = SelectedId != null && prli.Id == SelectedId
                               };
                return await requests.ToListAsync();
        }


        public async Task<string> GetNewDocumentNumber(int? TypeId)
        {
            var purchaseType = await context.PurchaseOrderTypes.Where(s => s.Id == TypeId).FirstOrDefaultAsync();

            var typeCode = purchaseType == null ? "" : $"{purchaseType.Code}-";
            var docNumberPrefix = $"{typeCode}PO{DateTime.Now.Year}-";
            var lastDocument = await context.PurchaseOrderNews
                                            .Where(s => s.DocumentNumber.StartsWith(docNumberPrefix))
                                            .OrderByDescending(s => s.DocumentNumber)
                                            .Select(s => s.DocumentNumber)
                                            .FirstOrDefaultAsync();

            int.TryParse(lastDocument?.Replace(docNumberPrefix, "") ?? "0", out int docNumberLast);
            return $"{docNumberPrefix}{docNumberLast + 1:D4}";
        }

        public async Task<IList<PurchaseOrderNewVM>> GetListAsync(PurchaseOrderFilter filter)
        {
            var orderVM = from order in context.PurchaseOrderNews
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
                              Supplier = order.Supplier.Code,
                              Term = order.Term.Name,
                              DeliveryMode = order.DeliveryMode.Code,
                              Status = order.Status.Code,
                              OrderDateD = order.OrderDate
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
                    s.Term,
                    s.DeliveryMode,
                    s.Status,
                    s.OrderDate
                }.Any(a => a != null && a.ToLower().Contains(form.SearchValue)));
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
                            case "OrderDate": data = data.OrderBy(s => s.OrderDateD); break;
                            default: break;
                        }
                    }
                    else if (form.SortColumnDirection == "desc")
                    {
                        switch (form.SortColumn)
                        {
                            case "OrderDate": data = data.OrderByDescending(s => s.OrderDateD); break;
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

        public async Task<PurchaseOrderNewDetailedVM?> GetDetails(int orderId)
        {
            var details = from order in context.PurchaseOrderNews
                          let resubmitfrom = context.PurchaseOrderNews
                                                    .Where(s => s.ResubmitId == order.Id)
                                                    .FirstOrDefault()
                          join userc in userManager.Users on order.UserCreated equals userc.Id
                          join userm in userManager.Users on order.UserModified equals userm.Id into m
                          from userm in m.DefaultIfEmpty()
                          let item = order.PurchaseOrderLineItems.FirstOrDefault()
                          where order.Id == orderId
                          select new PurchaseOrderNewDetailedVM
                          {
                              Id = order.Id,
                              DocumentNumber = order.DocumentNumber,
                              Type = order.Type.Name,
                              TypeId = order.Type.Id,
                              RequestType = order.Type.RequestType.Name,
                              RequestTypeId = order.Type.RequestType.Id,
                              RequestPurpose = item == null 
                                             ? null 
                                             : item.RequestItem.PurchaseRequest.Purpose.Name,
                              RequestPurposeId = item == null 
                                               ? null 
                                               : item.RequestItem.PurchaseRequest.Purpose.Id,
                              Supplier = order.Supplier.Name,
                              SupplierId = order.Supplier.Id,
                              SupplierContactPerson = order.Supplier.ContactPerson,
                              SupplierContactNumber = order.Supplier.OfficeTelNo,
                              SupplierAddress = order.Supplier.AddressLine1,
                              Term = order.Term.Name,
                              TermId = order.Term.Id,
                              DeliveryMode = order.DeliveryMode.Name,
                              DeliveryModeId = order.DeliveryMode.Id,
                              DeliveryAddress = order.DeliveryAddress.AddressLine1,
                              DeliveryAddressId = order.DeliveryAddress.Id,
                              DeliveryName = order.DeliveryAddress.Name,
                              DeliveryContactPerson = order.DeliveryAddress.ContactPerson,
                              DeliveryContactNumber = order.DeliveryAddress.ContactNumber,
                              Status = order.Status.Code,
                              OrderDateD = order.OrderDate,
                              Discount1D = order.Discount1,
                              Discount2D = order.Discount2,
                              Currency = order.Supplier.Currency.Code,
                              CurrencyId = order.Supplier.Currency.Id,
                              CurrencyRateD = order.Supplier.Currency.Rate,
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
                }.Any(a => a != null && a.ToLower().Contains(form.SearchValue)));
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

        public async Task<ResponseModel<int>> AddAsync(PurchaseOrderSaveVM form, List<PurchaseOrderLineItemVM> Items)
        {
            var response = new ResponseModel<int>();

            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

            var type = await context.PurchaseOrderTypes.Where(s => s.Id == form.TypeId).FirstOrDefaultAsync();
            if (type == null)
            {
                response.Status = "Error";
                response.Message = "Invalid type.";
                response.NewRow = -1;
            }
            else
            {

                var status = await context.PurchaseStatuses.Where(s => s.Name.ToLower() == form.Status.ToLower()).FirstOrDefaultAsync();
                if (status == null)
                {
                    response.Status = "Error";
                    response.Message = "Invalid status.";
                    response.NewRow = -1;
                }
                else
                {
                    var previousOrder = await context.PurchaseOrderNews
                                                     .Where(s => s.Id == form.ResubmitId)
                                                     .FirstOrDefaultAsync();

                    var docNumber = previousOrder == null
                                  ? await GetNewDocumentNumber(form.TypeId)
                                  : previousOrder.DocumentNumber;

                    var order = new PurchaseOrderNew
                    {
                        DocumentNumber = docNumber,
                        TypeId = form.TypeId,
                        SupplierId = form.SupplierId,
                        TermId = form.TermsId,
                        DeliveryModeId = form.DeliveryModeId,
                        DeliveryAddressId = form.DeliveryAddressId,
                        OrderDate = form.DateOrdered,
                        CanvasSummaryReference = form.Reference + "",
                        Remarks = form.Remarks,
                        StatusId = status.Id
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

                    response.Status = "Success";
                    response.Message = form.Status.ToLower() == "draft" ? "Purchase order saved as draft." : "Purchase order successfully submitted.";
                    response.NewRow = order.Id;
                }
            }

            return response;
        }

        public async Task<ResponseModel> SaveItemsAsync(IList<PurchaseOrderLineItemVM> Items, int orderId, string? status = null)
        {
            var response = new ResponseModel();

            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

            var order = await context.PurchaseOrderNews.Where(s => s.Id == orderId).FirstOrDefaultAsync();

            if (order == null)
            {
                response.Status = "Error";
                response.Message = "Invalid purchase order";
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(status))
                {
                    int statusId = context.PurchaseStatuses.Where(s => s.Name.ToLower() == status.ToLower()).Select(s => s.Id).First();
                    order.StatusId = statusId;
                }

                try
                {
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
                    var fitems = Items.Select(s => s.Id);
                    var ritems = citems.Where(s => fitems.Contains(s.Id) == false);
                    // remove unavailable items
                    context.PurchaseOrderNewLineItems.RemoveRange(ritems);

                    await context.SaveChangesAsync(currentUserId);

                    response.Status = "Success";
                    response.Message = "Order item(s) successfully saved";
                }
                catch (Exception ex)
                {
                    response.Status = "Error";
                    response.Message = SystemFormat.GetInnermostExceptionMessage(ex);
                }
            }
            return response;
        }

        public async Task<ResponseModel> ApproveAsync(int orderId)
        {
            var response = new ResponseModel();

            var order = context.PurchaseOrderNews.Where(s => s.Id == orderId).FirstOrDefault();
            if (order == null)
            {
                response.Status = "Error";
                response.Message = "Invalid purchase order";
            }
            else
            {
                string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
                int statusId = context.PurchaseStatuses.Where(s => s.Name.ToLower() == "approved").Select(s => s.Id).First();

                if (!string.IsNullOrWhiteSpace(order.ApproverId))
                {
                    response.Status = "Error";
                    response.Message = "Purchase order already approved.";
                }
                else
                {
                    order.ApproverId = currentUserId;
                    order.StatusId = statusId;

                    context.PurchaseOrderNews.Update(order);
                    await context.SaveChangesAsync(currentUserId);

                    response.Status = "Success";
                    response.Message = "Purchase order successfully approved.";
                }
            }
            return response;
        }

        public async Task<ResponseModel> DisapproveAsync(int orderId, string? reason)
        {
            var response = new ResponseModel();


            var order = context.PurchaseOrderNews.Where(s => s.Id == orderId).FirstOrDefault();
            if (order == null)
            {
                response.Status = "Error";
                response.Message = "Invalid purchase order";
            }
            else
            {
                string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
                int statusId = context.PurchaseStatuses.Where(s => s.Name.ToLower() == "disapproved").Select(s => s.Id).First();

                if (!string.IsNullOrWhiteSpace(order.DisapproverId))
                {
                    response.Status = "Error";
                    response.Message = "Purchase order already disapproved.";
                }
                else
                {
                    order.DisapproverId = currentUserId;
                    order.DisapprovedReason = reason;
                    order.StatusId = statusId;

                    context.PurchaseOrderNews.Update(order);
                    await context.SaveChangesAsync(currentUserId);

                    response.Status = "Success";
                    response.Message = "Purchase order successfully disapproved.";
                }
            }

            return response;
        }

        public async Task<ResponseModel> DeleteAsync(int requestId)
        {
            var response = new ResponseModel();
            try
            {
                var existingRequest = await context.PurchaseRequests
                                                   .Include(s => s.Status)
                                                   .FirstOrDefaultAsync(pr => pr.Id == requestId);

                if (existingRequest == null)
                {
                    response.Status = "Error";
                    response.Message = "Purchase request not found.";
                }
                else if (existingRequest.Status.Name.ToLower() != "draft")
                {
                    response.Status = "Error";
                    response.Message = "Only draft can be deleted.";
                }
                else
                {
                    string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
                    var resubmittedOrder = await context.PurchaseRequests
                                                        .Where(s => s.ResubmitId == existingRequest.Id)
                                                        .FirstOrDefaultAsync();
                    if (resubmittedOrder != null)
                    {
                        resubmittedOrder.ResubmitId = null;
                        await context.SaveChangesAsync(currentUserId);
                    }

                    context.PurchaseRequests.Remove(existingRequest);
                    await context.SaveChangesAsync(currentUserId);

                    response.Status = "Success";
                    response.Message = "Purchase request successfully deleted.";
                }
            }
            catch (Exception)
            {
                response.Status = "Error";
                response.Message = "An error occurred while deleting the purchase request.";
            }

            return response;
        }
    }
}