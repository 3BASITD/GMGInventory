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
    public class PurchaseRequestReporitory : IPurchaseRequestRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly string currentUserId;

        public PurchaseRequestReporitory(ApplicationDbContext context
                                       , IHttpContextAccessor httpContextAccessor
                                       , UserManager<User> userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
        }

        public async Task<string?> GetPurchaseRequestType(int? Id)
        {
            //var poRequestId = context.PurchaseOrderNewLineItems.Where(s => s.PurchaseOrderId == Id).Select(s => s.PurchaseRequestId).FirstOrDefault();

            //return await context.PurchaseRequests.Where(s => s.Id == poRequestId).Select(s => s.Type.Code).FirstOrDefaultAsync();

            var type = from t in context.PurchaseRequests
                       where t.Id == Id
                       select t.Type.Code;
            return await type.FirstOrDefaultAsync();
        }

        public async Task<IList<JsonCodeOptionsModel>> GetTypeOptions(int? SelectedId)
        {
            return await context.PurchaseRequestTypes
                                .Select(s => new JsonCodeOptionsModel
                                {
                                    Id = s.Id.ToString(),
                                    Text = s.Name,
                                    Code = s.Code,
                                    Selected = SelectedId != null && s.Id == SelectedId
                                })
                                .OrderBy(s => s.Id)
                                .ToListAsync();
        }

        public async Task<Select2Model<JsonCodeOptionsModel>> GetTypeOptions2(string search, int page)
        {
            var types = await context.PurchaseRequestTypes
                                     .Where(s => string.IsNullOrWhiteSpace(search)
                                              || (s.Code.ToLower().Contains(search.ToLower()))
                                              || (s.Name.ToLower().Contains(search.ToLower())))
                                     .Select(s => new JsonCodeOptionsModel
                                     {
                                         Id = s.Id.ToString(),
                                         Text = s.Name,
                                         Code = s.Code
                                     })
                                     .OrderBy(s => s.Text)
                                     .ToListAsync();
            var typesFiltered = types.Skip((page - 1) * 50).Take(50).ToList();
            return new Select2Model<JsonCodeOptionsModel>
            {
                //incomplete_results = false,
                items = typesFiltered,
                total_count = types.Count
            };
        }

        public async Task<IList<JsonCodeOptionsModel>> GetPurposeOptions(int TypeId, int? SelectedId)
        {
            return await context.PurchaseRequestPurposes
                                .Where(s => s.TypeId == TypeId || s.Id == SelectedId)
                                .Select(s => new JsonCodeOptionsModel
                                {
                                    Id = s.Id.ToString(),
                                    Text = s.Name,
                                    Code = s.Code,
                                    Selected = SelectedId != null && s.Id == SelectedId
                                })
                                .OrderBy(s => s.Id)
                                .ToListAsync();
        }

        public async Task<Select2Model<JsonCodeOptionsModel>> GetPurposeOptions2(string search, int page, int? TypeId)
        {
            var purposes = await context.PurchaseRequestPurposes
                                        .Where(s => (TypeId == null || s.TypeId == TypeId)
                                                 && (string.IsNullOrWhiteSpace(search)
                                                 || (s.Code.ToLower().Contains(search.ToLower()))
                                                 || (s.Name.ToLower().Contains(search.ToLower()))))
                                        .Select(s => new JsonCodeOptionsModel
                                        {
                                            Id = s.Id.ToString(),
                                            Text = s.Name,
                                            Code = s.Code
                                        })
                                        .OrderBy(s => s.Text)
                                        .ToListAsync();
            var purposesFiltered = purposes.Skip((page - 1) * 50).Take(50).ToList();
            return new Select2Model<JsonCodeOptionsModel>
            {
                //incomplete_results = false,
                items = purposesFiltered,
                total_count = purposes.Count
            };
        }

        public async Task<IList<PurchaseRequestVM>> GetListAsync(PurchaseRequestFilter filter)
        {
            var requestVM = from request in context.PurchaseRequests
                            where (filter.TypeId == null || request.TypeId == filter.TypeId)
                            && (filter.PurposeId == null || request.PurposeId == filter.PurposeId)
                            && (filter.RegionId == null || request.PurchaseRequestLineItems.Any(s => s.SalesOrder.Region.Id == filter.RegionId))
                            && (filter.DateFrom == null || filter.DateFrom.Value.Date <= request.RequestDate.Date)
                            && (filter.DateTo == null || request.RequestDate.Date <= filter.DateTo.Value.Date)
                            && (filter.StatusId == null || request.StatusId == filter.StatusId)
                            && (filter.FiledBy == null || filter.FiledBy.ToLower() != "me" || (filter.FiledBy.ToLower() == "me" && request.UserCreated == currentUserId))
                            let firstitem = request.PurchaseRequestLineItems.FirstOrDefault()
                            select new PurchaseRequestVM
                            {
                                Id = request.Id,
                                DocumentNumber = request.DocumentNumber,
                                Type = request.Type.Name,
                                Purpose = request.Purpose == null
                                        ? request.PurposeDefinition + ""
                                        : request.Purpose.Name,
                                Region = firstitem.SalesOrderId != null
                                       ? firstitem.SalesOrder.Region.Name
                                       : "",
                                Status = request.Status.Code,
                                RequestDateD = request.RequestDate,
                                Remarks = request.Remarks
                            };
            return await requestVM.ToListAsync();
        }

        public async Task<ForDataTables<PurchaseRequestVM>> GetListDTAsync(DataTablesRequestForm form, PurchaseRequestFilter filter)
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
                    s.Purpose,
                    s.Region,
                    s.Status,
                    s.RequestDate,
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
                            case "RequestDate": data = data.OrderBy(s => s.RequestDateD).ThenBy(s => s.DocumentNumber); break;
                            default: break;
                        }
                    }
                    else if (form.SortColumnDirection == "desc")
                    {
                        switch (form.SortColumn)
                        {
                            case "RequestDate": data = data.OrderByDescending(s => s.RequestDateD).ThenByDescending(s => s.DocumentNumber); break;
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
            return new ForDataTables<PurchaseRequestVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public async Task<PurchaseRequestDetailedVM?> GetDetails(int requestId)
        {
            var details = from request in context.PurchaseRequests
                          let resubmitfrom = context.PurchaseRequests
                                                    .Where(s => s.ResubmitId == request.Id)
                                                    .FirstOrDefault()
                          let firstitem = request.PurchaseRequestLineItems.First()
                          join userc in userManager.Users on request.UserCreated equals userc.Id
                          join userm in userManager.Users on request.UserModified equals userm.Id into m
                          from userm in m.DefaultIfEmpty()
                          where request.Id == requestId
                          select new PurchaseRequestDetailedVM
                          {
                              Id = request.Id,
                              DocumentNumber = request.DocumentNumber,
                              Type = request.Type.Name,
                              TypeId = request.Type.Id,
                              Purpose = request.Purpose == null ? request.PurposeDefinition + "" : request.Purpose.Name,
                              PurposeId = request.Purpose == null ? null : request.Purpose.Id,
                              Region = firstitem.SalesOrderId == null ? "" : firstitem.SalesOrder.Region.Name,
                              RegionId = firstitem.SalesOrderId == null ? (int?)null : firstitem.SalesOrder.Region.Id,
                              Status = request.Status.Code,
                              RequestDateD = request.RequestDate,
                              Remarks = request.Remarks,
                              ApprovedBy1 = request.Approver1Id == null ? null : request.Approver1.FirstName + " " + request.Approver1.LastName,
                              //ApprovedBy2 = request.Approver2Id == null ? null : request.Approver2.FirstName + " " + request.Approver2.LastName,
                              DisapprovedBy = request.DisapproverId == null ? null : request.Disapprover.FirstName + " " + request.Disapprover.LastName,
                              DisapprovedReason = request.DisapprovedReason,
                              ResubmitId = request.ResubmitId,
                              ResubmitFromId = resubmitfrom == null ? null : resubmitfrom.Id,
                              UserCreated = userc.FirstName + " " + userc.LastName,
                              DateCreatedD = request.DateCreated,
                              UserModified = userm == null ? null : userm.FirstName + " " + userm.LastName,
                              DateModifiedD = request.DateModified
                          };
            return await details.FirstOrDefaultAsync();
        }

        public async Task<IList<PurchaseRequestLineItemVM>> GetItemListAsync(int purchaseRequestId)
        {
            var requestVM = from request in context.PurchaseRequestLineItems
                            join soli in context.SalesOrderLineItems
                                      on new
                                      {
                                          request.ItemId,
                                          request.SalesOrderId
                                      }
                                      equals new
                                      {
                                          ItemId = (int?)soli.ItemId,
                                          SalesOrderId = (int?)soli.SalesOrderId
                                      }
                                      into sg
                            from soli in sg.DefaultIfEmpty()
                            let ii = context.ItemInventories
                                            .Where(s => s.ItemId == request.ItemId
                                                     && s.Warehouse.IsMain == true)
                                            .Sum(s => (int?)s.Quantity) ?? 0
                            let requests = context.PurchaseRequestLineItems
                                                  .Where(s => s.SalesOrderId == soli.SalesOrderId
                                                           && s.ItemId == soli.ItemId
                                                           && s.PurchaseRequest.Status.Name.ToLower() == "approved")
                                                  .Sum(s => (int?)s.Quantity) ?? 0
                            let po = request.PurchaseOrderLineItems
                                            .Select(s => new
                                            {
                                                Id = s.PurchaseOrder.Id,
                                                DocNum = s.PurchaseOrder.DocumentNumber
                                            })
                                            .Distinct()
                            where request.PurchaseRequestId == purchaseRequestId && (soli.IsPromo == null || soli.IsPromo == false)
                            select new PurchaseRequestLineItemVM
                            {
                                Id = request.Id,
                                PurchaseRequestId = request.PurchaseRequestId,
                                LineNumber = request.LineNumber,
                                SalesOrderId = request.SalesOrderId,
                                SalesOrderNumber = request.SalesOrder.SalesOrderNumber,
                                PONumber = string.Join(", ", po.Select(s => "<a href='/PurchaseOrder/Details/" + s.Id + "' class='link-info' target='_blank'>" + s.DocNum + "</a>")),
                                CustomerId = request.SalesOrder.Customer.Id,
                                Customer = request.SalesOrder.Customer.Name,
                                ItemId = request.ItemId,
                                ItemCode = request.Item == null ? null : request.Item.Code,
                                ItemPartNumber = (request.Item == null ? null : request.Item.PartNumber) + "",
                                ItemDescription = request.ItemDescription,
                                HaveStock = request.HaveStock == true,
                                StockOnHandD = ii,
                                QuantityD = request.Quantity,
                                MaxQuantityD = request.PurchaseRequest.Purpose.Name.ToLower() == "rush order"
                                             ? Math.Max(0, ((soli == null ? 0 : soli.Quantity) - (ii + requests)))
                                             : 1999999,
                                UnitId = request.Unit != null ? request.Unit.Id
                                       : request.Item != null ? request.Item.Unit.Id
                                       : 0,
                                UnitCode = request.Unit != null ? request.Unit.Code
                                       : request.Item != null ? request.Item.Unit.Code
                                       : "",
                                Purpose = request.Purpose
                            };
            return await requestVM.ToListAsync();
        }

        public async Task<ForDataTables<PurchaseRequestLineItemVM>> GetItemListDTAsync(DataTablesRequestForm form, int purchaseRequestId)
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
                    s.SalesOrderNumber,
                    s.Customer,
                    s.ItemCode,
                    s.ItemPartNumber,
                    s.ItemDescription,
                    s.StockOnHand,
                    s.Quantity,
                    s.UnitCode,
                    s.Purpose,
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
            return new ForDataTables<PurchaseRequestLineItemVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public async Task<IList<ItemFromSalesOrderVM>> GetSalesOrderItems(int SalesOrderId)
        {
            var itemVM = await (from order in context.SalesOrderLineItems
                                let inventory = context.ItemInventories
                                                       .Where(s => s.ItemId == order.ItemId && s.Warehouse.IsMain == true)
                                                       .Sum(s => (int?)s.Quantity) ?? 0
                                let packedQuantity = context.PackLineItems
                                                            .Where(s => s.Pack.SalesOrderId == order.SalesOrderId
                                                                     && s.ItemId == order.ItemId
                                                                     && s.Pack.Status.Code.Trim().ToLower() == "posted")
                                                            .Sum(s => (int?)s.Quantity) ?? 0
                                let returnedQuantity = context.SalesReturnLineItems
                                                              .Where(s => s.InvoiceLineItem != null
                                                                       && s.InvoiceLineItem.Invoice.SalesOrderId == order.SalesOrderId
                                                                       && s.ItemId == order.ItemId)
                                                              .Sum(s => (int?)s.Quantity) ?? 0
                                where order.SalesOrderId == SalesOrderId
                                 && (order.IsPromo == null || order.IsPromo == false)
                                orderby order.LineNumber ascending
                                select new ItemFromSalesOrderVM
                                {
                                    Id = order.Id,
                                    LineNumber = order.LineNumber,
                                    ItemId = order.Item.Id,
                                    ItemCode = order.Item.Code,
                                    ItemName = order.Item.Name,
                                    PartNumber = order.Item.PartNumber,
                                    InventoryCountD = inventory,
                                    OrderedQuantityD = order.Quantity,
                                    PackQuantityD = packedQuantity,
                                    ReturnedQuantityD = returnedQuantity,
                                    Unit = order.Item.Unit.Code,
                                    UnitId = order.Item.Unit.Id
                                }).ToListAsync();

            return itemVM;
        }

        public async Task<ResponseModel<int>> AddAsync(PurchaseRequestSaveVM form, List<PurchaseRequestLineItemVM> Items)
        {
            try
            {
                var type = await context.PurchaseRequestTypes.Where(s => s.Id == form.TypeId).FirstOrDefaultAsync();
                if (type == null)
                    return new ResponseModel<int> { Status = "Error", Message = "Invalid type", NewRow = -1 };

                var status = await context.PurchaseStatuses.Where(s => s.Name.ToLower() == form.Status.ToLower()).FirstOrDefaultAsync();
                if (status == null)
                    return new ResponseModel<int> { Status = "Error", Message = "Invalid status", NewRow = -1 };

                var previousRequest = await context.PurchaseRequests
                                                   .Where(s => s.Id == form.ResubmitId)
                                                   .FirstOrDefaultAsync();
                var procurementHelper = new ProcurementHelper();
                var docNumber = previousRequest == null
                              ? await procurementHelper.GetNewRequest(context, type.Code, form.Status)
                              : previousRequest.DocumentNumber.Split("-D")[0];

                var request = new PurchaseRequest
                {
                    DocumentNumber = docNumber,
                    TypeId = form.TypeId,
                    PurposeId = form.PurposeId,
                    PurposeDefinition = form.PurposeDefinition,
                    RequestDate = form.DateRequested,
                    Remarks = form.Remarks,
                    StatusId = status.Id
                };
                await context.PurchaseRequests.AddAsync(request);
                await context.SaveChangesAsync(currentUserId);

                if (previousRequest != null)
                {
                    var statusResubmitted = await context.PurchaseStatuses
                                                         .Where(s => s.Name.ToLower() == "resubmitted")
                                                         .FirstOrDefaultAsync();
                    previousRequest.ResubmitId = request.Id;
                    if (statusResubmitted != null)
                    {
                        previousRequest.StatusId = statusResubmitted.Id;
                    }
                    context.PurchaseRequests.Update(previousRequest);
                    await context.SaveChangesAsync(currentUserId);
                }

                ResponseModel itemsResponse = await SaveItemsAsync(Items, request.Id);
                if (itemsResponse.Status == "Error")
                {
                    context.PurchaseRequests.Remove(request);
                    await context.SaveChangesAsync();

                    return new ResponseModel<int>
                    {
                        Status = itemsResponse.Status,
                        Message = itemsResponse.Message
                    };
                }
                else
                {
                    return new ResponseModel<int>
                    {
                        Status = "Success",
                        Message = form.Status.ToLower() == "draft" ? "Purchase request saved as draft" : "Purchase request successfully submitted",
                        NewRow = request.Id
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<int> { Status = "Error", Message = SystemFormat.GetInnermostExceptionMessage(ex), NewRow = -1 };
            }
        }

        public async Task<ResponseModel> SaveItemsAsync(IList<PurchaseRequestLineItemVM> Items, int requestId, string? status = null)
        {
            try
            {
                var request = await context.PurchaseRequests
                                           .Include(s => s.Status)
                                           .Include(s => s.Type)
                                           .Where(s => s.Id == requestId)
                                           .FirstOrDefaultAsync();

                if (request == null)
                {
                    return new ResponseModel { Status = "Error", Message = "Invalid purchase request" };
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(status))
                    {
                        int? statusId = context.PurchaseStatuses.Where(s => s.Name.ToLower() == status.ToLower()).Select(s => s.Id).FirstOrDefault();
                        if (statusId != null)
                            request.StatusId = statusId;

                        if (request.DocumentNumber.StartsWith("D") && request.Status.Name.ToLower() == "draft" && status.ToLower() != "draft")
                        {
                            //var docNumberPrefix = $"{request.Type.Code}-PR{DateTime.Now.Year}-";

                            //var lastDocument = await context.PurchaseRequests
                            //                                .Where(s => s.DocumentNumber.StartsWith(docNumberPrefix))
                            //                                .OrderByDescending(s => s.DocumentNumber)
                            //                                .Select(s => s.DocumentNumber)
                            //                                .FirstOrDefaultAsync();

                            //int.TryParse(lastDocument?.Replace(docNumberPrefix, "") ?? "0", out int docNumberLast);
                            //request.DocumentNumber = $"{docNumberPrefix}{(docNumberLast + 1).ToString("D4")}";
                            var procurementHelper = new ProcurementHelper();
                            var docNumber = await procurementHelper.GetNewRequest(context, request.Type.Code, status);
                            request.DocumentNumber = docNumber;

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
                        var i = context.PurchaseRequestLineItems.Where(s => s.Id == rename.Id).FirstOrDefault();
                        if (i != null && i.ItemDescription != rename.ItemDescription)
                        {
                            i.ItemDescription = rename.ItemDescription;
                        }
                    }

                    var citems = context.PurchaseRequestLineItems.Where(s => s.PurchaseRequestId == requestId);
                    // add item
                    // check items not in db
                    var items = citems.Select(s => s.Id);
                    // add excluding items already available
                    var newitems = Items.Where(s => items.Contains(s.Id) == false)
                                        .Select(s => new PurchaseRequestLineItem
                                        {
                                            PurchaseRequestId = requestId,
                                            LineNumber = s.LineNumber,
                                            SalesOrderId = s.SalesOrderId,
                                            ItemId = s.ItemId,
                                            ItemDescription = s.ItemDescription,
                                            Quantity = s.QuantityD,
                                            UnitId = s.ItemId == null ? s.UnitId : null,
                                            Purpose = s.Purpose + "",
                                            HaveStock = s.HaveStock
                                        });
                    context.PurchaseRequestLineItems.AddRange(newitems);

                    // update item
                    // get items available in db
                    var existitems = Items.Where(s => items.Contains(s.Id));
                    foreach (var ei in existitems)
                    {
                        // update all available in db
                        var dbitem = await citems.Where(s => s.Id == ei.Id).FirstAsync();
                        dbitem.LineNumber = ei.LineNumber;
                        dbitem.Quantity = ei.QuantityD;
                        dbitem.UnitId = ei.ItemId == null ? ei.UnitId : null;
                        dbitem.HaveStock = ei.HaveStock;

                        context.PurchaseRequestLineItems.Update(dbitem);
                    }

                    // delete item
                    // get items not in form
                    var fitems = Items.Select(s => s.Id);
                    var ritems = citems.Where(s => fitems.Contains(s.Id) == false);
                    // remove unavailable items
                    context.PurchaseRequestLineItems.RemoveRange(ritems);

                    await context.SaveChangesAsync(currentUserId);

                    return new ResponseModel { Status = "Success", Message = "Request item(s) successfully saved" };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { Status = "Error", Message = SystemFormat.GetInnermostExceptionMessage(ex) };
            }
        }

        public async Task<ResponseModel> ApproveAsync(int requestId, int? approverNumber = null)
        {
            try
            {
                //approverNumber = approverNumber ?? 1;

                var request = context.PurchaseRequests
                                     .Include(s => s.Type)
                                     .Where(s => s.Id == requestId)
                                     .FirstOrDefault();
                if (request == null)
                {
                    return new ResponseModel { Status = "Error", Message = "Invalid purchase request" };
                }
                //else if (approverNumber != 1 && approverNumber != 2)
                //{
                //    response.Status = "Error";
                //    response.Message = "Invalid approver number";
                //}
                else
                {
                    int statusId = context.PurchaseStatuses.Where(s => s.Name.ToLower() == "approved").Select(s => s.Id).First();
                    //if (request.Type.Name.ToLower() == "non-automotive" || request.Type.Name.ToLower() == "automotive")
                    //{
                    //    string? current = approverNumber == 1 ? request.Approver1Id : request.Approver2Id;
                    //    string? other = approverNumber == 1 ? request.Approver2Id : request.Approver1Id;
                    //    string approverLabel = approverNumber == 1 ? "#1" : "#2";
                    //    string otherLabel = approverNumber == 1 ? "#2" : "#1";

                    //    if (other == currentUserId)
                    //    {
                    //        response.Status = "Error";
                    //        response.Message = $"You have already approved this purchase request as approver {otherLabel}.";
                    //        return response;
                    //    }

                    //    if (!string.IsNullOrWhiteSpace(current) && current != currentUserId)
                    //    {
                    //        var diffUser = await userManager.FindByIdAsync(current);
                    //        response.Status = "Error";
                    //        response.Message = $"Already have an approver {approverLabel}: {diffUser.FirstName} {diffUser.LastName}";
                    //        return response;
                    //    }

                    //    if (current == currentUserId)
                    //    {
                    //        response.Status = "Error";
                    //        response.Message = $"You have already approved this purchase request as approver {approverLabel}.";
                    //        return response;
                    //    }

                    //    if (approverNumber == 1)
                    //        request.Approver1Id = currentUserId;
                    //    else
                    //        request.Approver2Id = currentUserId;

                    //    if (!string.IsNullOrWhiteSpace(request.Approver1Id)
                    //        && !string.IsNullOrWhiteSpace(request.Approver2Id))
                    //    {
                    //        request.StatusId = statusId;
                    //    }

                    //    context.PurchaseRequests.Update(request);
                    //    await context.SaveChangesAsync(currentUserId);

                    //    response.Status = "Success";
                    //    response.Message = "Purchase request successfully approved.";
                    //    return response;
                    //}
                    //else
                    //{
                    if (!string.IsNullOrWhiteSpace(request.Approver1Id))
                    {
                        if (request.StatusId != statusId)
                        {
                            request.StatusId = statusId;

                            context.PurchaseRequests.Update(request);
                            await context.SaveChangesAsync(currentUserId);
                        }

                        return new ResponseModel { Status = "Error", Message = "Purchase request already approved. Try reloading the page" };
                    }
                    else
                    {
                        request.Approver1Id = currentUserId;
                        request.StatusId = statusId;

                        context.PurchaseRequests.Update(request);
                        await context.SaveChangesAsync(currentUserId);

                        return new ResponseModel { Status = "Success", Message = "Purchase request successfully approved" };
                    }
                    //}
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { Status = "Error", Message = SystemFormat.GetInnermostExceptionMessage(ex) };
            }
        }

        public async Task<ResponseModel> DisapproveAsync(int requestId, string? reason)
        {
            try
            {
                var request = context.PurchaseRequests.Where(s => s.Id == requestId).FirstOrDefault();
                if (request == null)
                {
                    return new ResponseModel { Status = "Error", Message = "Invalid purchase request" };
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(request.DisapproverId))
                    {
                        return new ResponseModel { Status = "Error", Message = "Purchase request already disapproved" };
                    }
                    else
                    {
                        var tempDocumentNumber = request.DocumentNumber + "-D";
                        var latestDisapprovedRequest = context.PurchaseRequests
                                                              .Where(s => s.DocumentNumber.StartsWith(tempDocumentNumber))
                                                              .OrderByDescending(s => s.DocumentNumber)
                                                              .Select(s => s.DocumentNumber)
                                                              .FirstOrDefault();
                        var latestDisapprovedDocumentNumber = latestDisapprovedRequest == null
                                                            ? 0
                                                            : int.Parse(latestDisapprovedRequest.Replace(tempDocumentNumber, ""));
                        var disapprovedDocumentNumber = tempDocumentNumber + string.Format("{0:00}", latestDisapprovedDocumentNumber + 1);

                        int statusId = context.PurchaseStatuses.Where(s => s.Name.ToLower() == "disapproved").Select(s => s.Id).First();

                        request.DocumentNumber = disapprovedDocumentNumber;
                        if (request.Approver1Id != null) request.Approver1Id = null;
                        if (request.Approver2Id != null) request.Approver2Id = null;
                        request.DisapproverId = currentUserId;
                        request.DisapprovedReason = reason;
                        request.StatusId = statusId;

                        context.PurchaseRequests.Update(request);
                        await context.SaveChangesAsync(currentUserId);

                        return new ResponseModel { Status = "Success", Message = "Purchase request successfully disapproved" };
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { Status = "Error", Message = SystemFormat.GetInnermostExceptionMessage(ex) };
            }
        }

        public async Task<ResponseModel> CancelAsync(int requestId)
        {
            try
            {
                var existingRequest = await context.PurchaseRequests
                                                   .Include(s => s.Status)
                                                   .Include(s => s.PurchaseRequestLineItems).ThenInclude(s => s.PurchaseOrderLineItems)
                                                   .Where(s => s.Id == requestId)
                                                   .FirstOrDefaultAsync();

                if (existingRequest == null)
                {
                    return new ResponseModel { Status = "Error", Message = "Invalid Purchase Request. Record can not be found" };
                }
                else if (existingRequest.Status.Name.ToLower() != "submitted")
                {
                    return new ResponseModel { Status = "Error", Message = "Only submitted can be cancelled" };
                }
                else if (existingRequest.PurchaseRequestLineItems.Sum(s => s.PurchaseOrderLineItems.Count) > 0)
                {
                    return new ResponseModel { Status = "Error", Message = "Record already used in Purchase Order. Cancel cannot proceed" };
                }
                else
                {
                    var draftStatus = await context.PurchaseStatuses.Where(s => s.Name.Trim().ToLower() == "draft").Select(s => s.Id).FirstAsync();

                    existingRequest.StatusId = draftStatus;

                    context.PurchaseRequests.Update(existingRequest);
                    await context.SaveChangesAsync(currentUserId);

                    return new ResponseModel { Status = "Success", Message = "Purchase Request successfully cancelled" };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { Status = "Error", Message = SystemFormat.GetInnermostExceptionMessage(ex) };
            }
        }

        public async Task<ResponseModel> DeleteAsync(int requestId)
        {
            try
            {
                var existingRequest = await context.PurchaseRequests
                                                   .Include(s => s.Status)
                                                   .Include(s => s.PurchaseRequestLineItems).ThenInclude(s => s.PurchaseOrderLineItems)
                                                   .Where(s => s.Id == requestId)
                                                   .FirstOrDefaultAsync();

                if (existingRequest == null)
                {
                    return new ResponseModel { Status = "Error", Message = "Invalid Purchase Request. Record can not be found" };
                }
                else if (existingRequest.Status.Name.ToLower() != "draft")
                {
                    return new ResponseModel { Status = "Error", Message = "Only draft can be deleted" };
                }
                else if (existingRequest.PurchaseRequestLineItems.Sum(s => s.PurchaseOrderLineItems.Count) > 0)
                {
                    return new ResponseModel { Status = "Error", Message = "Record already used in Purchase Order. Delete cannot proceed" };
                }
                else
                {
                    var previousRequest = await context.PurchaseRequests
                                                       .Where(s => s.ResubmitId == requestId)
                                                       .FirstOrDefaultAsync();
                    if (previousRequest != null)
                    {
                        var statusDisapproved = await context.PurchaseStatuses
                                                             .Where(s => s.Name.ToLower() == "disapproved")
                                                             .FirstAsync();
                        previousRequest.ResubmitId = null;
                        previousRequest.StatusId = statusDisapproved.Id;
                        await context.SaveChangesAsync(currentUserId);
                    }

                    var requestItems = await context.PurchaseRequestLineItems
                                                    .Where(s => s.PurchaseRequestId == requestId)
                                                    .ToListAsync();

                    context.PurchaseRequestLineItems.RemoveRange(requestItems);

                    context.PurchaseRequests.Remove(existingRequest);
                    await context.SaveChangesAsync(currentUserId);

                    return new ResponseModel { Status = "Success", Message = "Purchase Request successfully deleted" };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Status = "Error",
                    Message = SystemFormat.GetInnermostExceptionMessage(ex)
                };
            }
        }

    }
}