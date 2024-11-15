using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using GMGInventory.Data.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.ComponentModel.DataAnnotations;
using System.Linq.Dynamic.Core;

namespace GMGInventory.Application.Repositories
{
    public class PurchaseRequestReporitory : IPurchaseRequestRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;

        public PurchaseRequestReporitory(ApplicationDbContext context
                                       , IHttpContextAccessor httpContextAccessor
                                       , UserManager<User> userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
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

        private async Task<IList<PurchaseRequestVM>> GetListAsync()
        {
            var requestVM = from request in context.PurchaseRequests
                                                   .Include(s => s.Type)
                                                   .Include(s => s.Purpose)
                                                   .Include(s => s.Region)
                                                   .Include(s => s.Status)
                            select new PurchaseRequestVM
                            {
                                Id = request.Id,
                                DocumentNumber = request.DocumentNumber,
                                Type = request.Type.Name,
                                Purpose = request.Purpose == null
                                        ? request.PurposeDefinition + ""
                                        : request.Purpose.Name,
                                Region = request.Region.Name,
                                Status = request.Status.Code,
                                RequestDateD = request.RequestDate
                            };
            return await requestVM.ToListAsync();
        }

        public async Task<ForDataTables<PurchaseRequestVM>> GetListDTAsync(DataTablesRequestForm form)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetListAsync()).AsQueryable();
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
                    s.RequestDate
                }.Any(a => a != null && a.Contains(form.SearchValue, StringComparison.CurrentCultureIgnoreCase)));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                if (form.SortColumn.Contains("date", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (form.SortColumnDirection == "asc")
                    {
                        switch (form.SortColumn)
                        {
                            case "RequestDate": data = data.OrderBy(s => s.RequestDateD); break;
                            default: break;
                        }
                    }
                    else if (form.SortColumnDirection == "desc")
                    {
                        switch (form.SortColumn)
                        {
                            case "RequestDate": data = data.OrderByDescending(s => s.RequestDateD); break;
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
                                                 .Include(s => s.Type)
                                                 .Include(s => s.Purpose)
                                                 .Include(s => s.Region)
                                                 .Include(s => s.Status)
                          let resubmitfrom = context.PurchaseRequests
                                                    .Where(s => s.ResubmitId == request.Id)
                                                    .FirstOrDefault()
                          join userc in userManager.Users on request.UserCreated equals userc.Id into c
                          from userc in c.DefaultIfEmpty()
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
                              Region = request.Region == null ? "" : request.Region.Name,
                              RegionId = request.Region == null ? (int?)null : request.Region.Id,
                              Status = request.Status.Code,
                              RequestDateD = request.RequestDate,
                              Remarks = request.Remarks,
                              ApprovedBy1 = request.Approver1Id == null ? null : request.Approver1.FirstName + " " + request.Approver1.LastName,
                              ApprovedBy2 = request.Approver2Id == null ? null : request.Approver2.FirstName + " " + request.Approver2.LastName,
                              DisapprovedBy = request.DisapproverId == null ? null : request.Disapprover.FirstName + " " + request.Disapprover.LastName,
                              DisapprovedReason = request.DisapprovedReason,
                              ResubmitId = request.ResubmitId,
                              ResubmitFromId = resubmitfrom == null ? null : resubmitfrom.Id,
                              UserCreated = userc.FirstName + " " + userc.LastName,
                              DateCreatedD = request.DateCreated,
                              UserModified = userm.FirstName + " " + userm.LastName,
                              DateModifiedD = request.DateModified
                          };
            return await details.FirstOrDefaultAsync();
        }

        public async Task<IList<PurchaseRequestLineItemVM>> GetItemListAsync(int purchaseRequestId)
        {
            var requestVM = from request in context.PurchaseRequestLineItems
                                                   .Include(s => s.SalesOrder)
                                                   .Include(s => s.Item)
                                                   .Include(s => s.Unit)
                            where request.PurchaseRequestId == purchaseRequestId
                            select new PurchaseRequestLineItemVM
                            {
                                Id = request.Id,
                                PurchaseRequestId = request.PurchaseRequestId,
                                LineNumber = request.LineNumber,
                                SalesOrderId = request.SalesOrderId,
                                SalesOrderNumber = request.SalesOrder.SalesOrderNumber,
                                ItemId = request.ItemId,
                                ItemCode = request.Item.Code,
                                ItemPartNumber = string.IsNullOrWhiteSpace(request.Item.PartNumber) ? "-" : request.Item.PartNumber,
                                ItemDescription = request.ItemDescription,
                                StockOnHandD = request.StockOnHand,
                                QuantityD = request.Quantity,
                                UnitId = request.UnitId,
                                UnitCode = request.Unit.Code,
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
                    s.ItemCode,
                    s.ItemPartNumber,
                    s.ItemDescription,
                    s.StockOnHand,
                    s.Quantity,
                    s.UnitCode,
                    s.Purpose,
                }.Any(a => a != null && a.Contains(form.SearchValue, StringComparison.CurrentCultureIgnoreCase)));
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

        public async Task<ResponseModel<int>> AddAsync(PurchaseRequestSaveVM form, List<PurchaseRequestLineItemVM> Items)
        {
            var response = new ResponseModel<int>();

            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

            var type = await context.PurchaseRequestTypes.Where(s => s.Id == form.TypeId).FirstOrDefaultAsync();
            if (type == null)
            {
                response.Status = "Error";
                response.Message = "Invalid type.";
                response.NewRow = -1;
            }
            else
            {

                var status = await context.PurchaseStatuses.Where(s => s.Name == form.Status).FirstOrDefaultAsync();
                if (status == null)
                {
                    response.Status = "Error";
                    response.Message = "Invalid status.";
                    response.NewRow = -1;
                }
                else
                {
                    var previousRequest = await context.PurchaseRequests
                                                       .Where(s => s.Id == form.ResubmitId)
                                                       .FirstOrDefaultAsync();

                    var docNumber = "";
                    if (previousRequest == null)
                    {
                        var docNumberPrefix = $"{type.Code}-PR{DateTime.Now.Year}-";
                        var lastDocument = await context.PurchaseRequests
                                                        .Where(s => s.DocumentNumber.StartsWith(docNumberPrefix))
                                                        .OrderByDescending(s => s.DocumentNumber)
                                                        .Select(s => s.DocumentNumber)
                                                        .FirstOrDefaultAsync();

                        int.TryParse(lastDocument?.Replace(docNumberPrefix, "") ?? "0", out int docNumberLast);
                        docNumber = $"{docNumberPrefix}{(docNumberLast + 1).ToString("D4")}";
                    }
                    else
                    {
                        docNumber = previousRequest.DocumentNumber;
                    }


                    var request = new PurchaseRequest
                    {
                        DocumentNumber = docNumber,
                        TypeId = form.TypeId,
                        PurposeId = form.PurposeId,
                        PurposeDefinition = form.PurposeDefinition,
                        RegionId = form.RegionId,
                        RequestDate = form.DateRequested,
                        Remarks = form.Remarks,
                        StatusId = status.Id,
                        ResubmitId = form.ResubmitId
                    };
                    await context.PurchaseRequests.AddAsync(request);
                    await context.SaveChangesAsync(currentUserId);

                    if (previousRequest != null)
                    {
                        var statusResubmitted = await context.PurchaseStatuses
                                                             .Where(s => s.Name == "Resubmitted")
                                                             .FirstOrDefaultAsync();
                        previousRequest.ResubmitId = request.Id;
                        if (statusResubmitted != null)
                        {
                            previousRequest.StatusId = statusResubmitted.Id;
                        }
                        context.PurchaseRequests.Update(previousRequest);
                        await context.SaveChangesAsync(currentUserId);
                    }

                    await SaveItemsAsync(Items, request.Id);

                    response.Status = "Success";
                    response.Message = form.Status == "Draft" ? "Purchase request saved as draft." : "Purchase request successfully submitted.";
                    response.NewRow = request.Id;
                }
            }

            return response;
        }

        public async Task<ResponseModel> SaveItemsAsync(IList<PurchaseRequestLineItemVM> Items, int requestId, string? status = null)
        {
            var response = new ResponseModel();

            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

            var request = await context.PurchaseRequests.Where(s => s.Id == requestId).FirstOrDefaultAsync();

            if (request == null)
            {
                response.Status = "Error";
                response.Message = "Invalid purchase request";
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(status))
                {
                    int statusId = context.PurchaseStatuses.Where(s => s.Name == status).Select(s => s.Id).First();
                    request.StatusId = statusId;
                }


                //var existingItems = context.PurchaseRequestLineItems.Where(s => s.PurchaseRequestId == requestId);
                //context.PurchaseRequestLineItems.RemoveRange(existingItems);

                //var newItems = Items.Select(s => new PurchaseRequestLineItem
                //{
                //    PurchaseRequestId = requestId,
                //    LineNumber = s.LineNumber,
                //    SalesOrderId = s.SalesOrderId,
                //    ItemId = s.ItemId,
                //    ItemDescription = s.ItemDescription,
                //    StockOnHand = s.StockOnHandD,
                //    Quantity = s.QuantityD,
                //    UnitId = s.UnitId,
                //    Purpose = s.Purpose + ""
                //});
                //await context.PurchaseRequestLineItems.AddRangeAsync(newItems);
                //await context.SaveChangesAsync(currentUserId);

                try
                {
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
                                           StockOnHand = s.StockOnHandD,
                                           Quantity = s.QuantityD,
                                           UnitId = s.UnitId,
                                           Purpose = s.Purpose + ""
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
                        dbitem.StockOnHand = ei.StockOnHandD;
                        dbitem.Quantity = ei.QuantityD;

                        context.PurchaseRequestLineItems.Update(dbitem);
                    }

                    // delete item
                    // get items not in form
                    var fitems = Items.Select(s => s.Id);
                    var ritems = citems.Where(s => fitems.Contains(s.Id) == false);
                    // remove unavailable items
                    context.PurchaseRequestLineItems.RemoveRange(ritems);

                    await context.SaveChangesAsync(currentUserId);

                    response.Status = "Success";
                    response.Message = "Request item(s) successfully saved";
                }
                catch (Exception ex)
                {
                    response.Status = "Error";
                    response.Message = ex.Message;
                }
            }
            return response;
        }

        public async Task<ResponseModel> ApproveAsync(int requestId)
        {
            var response = new ResponseModel();

            var request = context.PurchaseRequests.Where(s => s.Id == requestId).FirstOrDefault();
            if (request == null)
            {
                response.Status = "Error";
                response.Message = "Invalid purchase request";
            }
            else
            {
                string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
                int statusId = context.PurchaseStatuses.Where(s => s.Name == "Approved").Select(s => s.Id).First();

                if (!string.IsNullOrWhiteSpace(request.Approver1Id) && !string.IsNullOrWhiteSpace(request.Approver2Id))
                {
                    response.Status = "Error";
                    response.Message = "Purchase request already approved.";
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(request.Approver1Id))
                        request.Approver1Id = currentUserId;
                    else
                        request.Approver2Id = currentUserId;

                    request.StatusId = statusId;

                    context.PurchaseRequests.Update(request);
                    await context.SaveChangesAsync(currentUserId);

                    response.Status = "Success";
                    response.Message = "Purchase request successfully approved.";
                }
            }
            return response;
        }

        public async Task<ResponseModel> DisapproveAsync(int requestId, string? reason)
        {
            var response = new ResponseModel();


            var request = context.PurchaseRequests.Where(s => s.Id == requestId).FirstOrDefault();
            if (request == null)
            {
                response.Status = "Error";
                response.Message = "Invalid purchase request";
            }
            else
            {
                string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
                int statusId = context.PurchaseStatuses.Where(s => s.Name == "Disapproved").Select(s => s.Id).First();

                if (!string.IsNullOrWhiteSpace(request.DisapproverId))
                {
                    response.Status = "Error";
                    response.Message = "Purchase request already disapproved.";
                }
                else
                {
                    request.DisapproverId = currentUserId;
                    request.DisapprovedReason = reason;
                    request.StatusId = statusId;

                    context.PurchaseRequests.Update(request);
                    await context.SaveChangesAsync(currentUserId);

                    response.Status = "Success";
                    response.Message = "Purchase request successfully disapproved.";
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
                    .FirstOrDefaultAsync(pr => pr.Id == requestId);

                if (existingRequest == null)
                {
                    response.Status = "Error";
                    response.Message = "Purchase request not found.";
                    return response;
                }

                context.PurchaseRequests.Remove(existingRequest);
                await context.SaveChangesAsync();

                response.Status = "Success";
                response.Message = "Purchase request successfully deleted.";
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