using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace GMGInventory.Application.Repositories
{
    public class PurchaseReturnNewReporitory : IPurchaseReturnNewRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;

        public PurchaseReturnNewReporitory(ApplicationDbContext context
                                        , IHttpContextAccessor httpContextAccessor
                                        , UserManager<User> userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        public async Task<IList<JsonOptionsModel>> GetPurchaseReceivingOptions(int? SelectedId)
        {
            return await context.PurchaseReceivings
                                .Include(s => s.Status)
                                .Include(s => s.PurchaseOrder)
                                .Where(s => s.Status.Name == "Approved")
                                .Select(s => new JsonOptionsModel
                                {
                                    Id = s.Id.ToString(),
                                    Text = s.DocumentNumber,
                                    Selected = SelectedId != null && s.Id == SelectedId
                                })
                                .OrderBy(s => s.Text)
                                .ToListAsync();
        }

        public async Task<IList<JsonPurchaseItemOptionsModel>> GetPurchaseReceiveItemOptions(int ReceiveId, int? SelectedId)
        {
            return await context.PurchaseOrderNewLineItems // palitan ng purchase receiving pag meron na
                                .Include(s => s.Item)
                                .Include(s => s.Unit)
                                .Where(s => s.PurchaseOrderId == ReceiveId)
                                .Select(s => new JsonPurchaseItemOptionsModel
                                {
                                    Id = s.Id.ToString(),
                                    Text = s.ItemDescription,
                                    LineNumber = s.LineNumber,
                                    ItemId = s.Item.Id,
                                    ItemCode = s.Item.Code,
                                    PartNumber = s.Item.PartNumber,
                                    Quantity = s.Quantity,
                                    Unit = s.Unit.Name,
                                    UnitId = s.Unit.Id,
                                    Selected = SelectedId != null && s.Id == SelectedId
                                })
                                .Where(s => s.UnorderedQuantity > 0)
                                .OrderBy(s => s.LineNumber)
                                .ToListAsync();
        }

        public async Task<IList<PurchaseReturnNewLineItemVM>> GetPurchaseReceiveItemList(int ReceiveId)
        {
            var receiveVM = from receive in context.PurchaseOrderNewLineItems
                                                   .Include(s => s.Item)
                                                   .Include(s => s.Unit)
                            where receive.PurchaseOrderId == ReceiveId
                            select new PurchaseReturnNewLineItemVM
                            {
                                LineNumber = receive.LineNumber,
                                ItemId = receive.Item.Id,
                                ItemCode = receive.Item.Code,
                                ItemPartNumber = receive.Item.PartNumber,
                                ItemDescription = receive.ItemDescription,
                                QuantityD = receive.Quantity,
                                UnitId = receive.Unit.Id,
                                UnitCode = receive.Unit.Code,
                                UnitCostD = receive.UnitCost
                            };

            return await receiveVM.ToListAsync();
        }

        public async Task<string> GetNewDocumentNumber()
        {
            var docNumberPrefix = $"RO{DateTime.Now.Year % 100:D2}";
            var lastDocument = await context.PurchaseRequests
                                            .Where(s => s.DocumentNumber.StartsWith(docNumberPrefix))
                                            .OrderByDescending(s => s.DocumentNumber)
                                            .Select(s => s.DocumentNumber)
                                            .FirstOrDefaultAsync();

            int.TryParse(lastDocument?.Replace(docNumberPrefix, "") ?? "0", out int docNumberLast);
            return $"{docNumberPrefix}{docNumberLast + 1:D6}";
        }

        private async Task<IList<PurchaseReturnNewVM>> GetListAsync()
        {
            var returnVM = from retrn in context.PurchaseReturnNews
                                                .Include(s => s.Supplier)
                                                .Include(s => s.PurchaseOrder)
                                                .Include(s => s.Warehouse)
                                                .Include(s => s.Status)
                           select new PurchaseReturnNewVM
                           {
                               Id = retrn.Id,
                               DocumentNumber = retrn.DocumentNumber,
                               Supplier = retrn.Supplier.Code,
                               PONumber = retrn.PurchaseOrder.DocumentNumber,
                               Warehouse = retrn.Warehouse.Code,
                               DRNumber = retrn.DeliveryReceiptNumber,
                               SINumber = retrn.SalesInvoiceNumber,
                               Status = retrn.Status.Code,
                               ReturnDateD = retrn.ReturnDate
                           };
            return await returnVM.ToListAsync();
        }

        public async Task<ForDataTables<PurchaseReturnNewVM>> GetListDTAsync(DataTablesRequestForm form)
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
                    s.Supplier,
                    s.PONumber,
                    s.Warehouse,
                    s.DRNumber,
                    s.SINumber,
                    s.Status,
                    s.ReturnDate,
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
                            case "ReturnDate": data = data.OrderBy(s => s.ReturnDateD); break;
                            default: break;
                        }
                    }
                    else if (form.SortColumnDirection == "desc")
                    {
                        switch (form.SortColumn)
                        {
                            case "ReturnDate": data = data.OrderByDescending(s => s.ReturnDateD); break;
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
            return new ForDataTables<PurchaseReturnNewVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public async Task<PurchaseReturnNewDetailedVM?> GetDetails(int returnId)
        {
            var details = from retrn in context.PurchaseReturnNews
                                               .Include(s => s.Supplier)
                                               .Include(s => s.Warehouse)
                                               .Include(s => s.PurchaseOrder)
                                               .Include(s => s.Status)
                          let resubmitfrom = context.PurchaseReturnNews
                                                    .Where(s => s.ResubmitId == retrn.Id)
                                                    .FirstOrDefault()
                          join userc in userManager.Users on retrn.UserCreated equals userc.Id into c
                          from userc in c.DefaultIfEmpty()
                          join userm in userManager.Users on retrn.UserModified equals userm.Id into m
                          from userm in m.DefaultIfEmpty()
                          where retrn.Id == returnId
                          select new PurchaseReturnNewDetailedVM
                          {
                              Id = retrn.Id,
                              DocumentNumber = retrn.DocumentNumber,
                              PONumber = retrn.PurchaseOrder.DocumentNumber,
                              Supplier = retrn.Supplier.Name,
                              Warehouse = retrn.Warehouse.Name,
                              DRNumber = retrn.DeliveryReceiptNumber,
                              SINumber = retrn.SalesInvoiceNumber,
                              Status = retrn.Status.Code,
                              ReturnDateD = retrn.ReturnDate,
                              Remarks = retrn.Remarks,
                              ApprovedBy = retrn.ApproverId == null ? null : retrn.Approver.FirstName + " " + retrn.Approver.LastName,
                              DisapprovedBy = retrn.DisapproverId == null ? null : retrn.Disapprover.FirstName + " " + retrn.Disapprover.LastName,
                              DisapprovedReason = retrn.DisapprovedReason,
                              ResubmitId = retrn.ResubmitId,
                              ResubmitFromId = resubmitfrom == null ? null : resubmitfrom.Id,
                              UserCreated = userc.FirstName + " " + userc.LastName,
                              DateCreatedD = retrn.DateCreated,
                              UserModified = userm.FirstName + " " + userm.LastName,
                              DateModifiedD = retrn.DateModified
                          };
            return await details.FirstOrDefaultAsync();
        }

        public async Task<IList<PurchaseReturnNewLineItemVM>> GetItemListAsync(int purchaseReturnId)
        {
            var orderVM = from retrn in context.PurchaseReturnNewLineItems
                                               .Include(s => s.Item)
                                               .Include(s => s.PurchaseReturn)
                                               .Include(s => s.Unit)
                          where retrn.PurchaseReturnId == purchaseReturnId
                          select new PurchaseReturnNewLineItemVM
                          {
                              Id = retrn.Id,
                              PurchaseReturnId = retrn.PurchaseReturnId,
                              LineNumber = retrn.LineNumber,
                              ItemId = retrn.Item.Id,
                              ItemCode = retrn.Item.Code,
                              ItemPartNumber = string.IsNullOrWhiteSpace(retrn.Item.PartNumber) ? "" : retrn.Item.PartNumber,
                              ItemDescription = retrn.ItemDescription,
                              QuantityD = retrn.Quantity,
                              UnitId = retrn.Unit.Id,
                              UnitCode = retrn.Unit.Code,
                              UnitCostD = retrn.UnitCost
                          };
            return await orderVM.ToListAsync();
        }

        public async Task<ForDataTables<PurchaseReturnNewLineItemVM>> GetItemListDTAsync(DataTablesRequestForm form, int purchaseReturnId)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetItemListAsync(purchaseReturnId)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(s => new[]
                {
                    s.LineNumber + "",
                    s.ItemCode,
                    s.ItemPartNumber,
                    s.ItemDescription,
                    s.Quantity,
                    s.UnitCode,
                    s.UnitCost,
                    s.TotalAmount
                }.Any(a => a != null && a.Contains(form.SearchValue, StringComparison.CurrentCultureIgnoreCase)));
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
            return new ForDataTables<PurchaseReturnNewLineItemVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public async Task<ResponseModel<int>> AddAsync(PurchaseReturnNewSaveVM form, List<PurchaseReturnNewLineItemVM> Items)
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

                var status = await context.PurchaseStatuses.Where(s => s.Name == form.Status).FirstOrDefaultAsync();
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
                                  ? await GetNewDocumentNumber()
                                  : previousOrder.DocumentNumber;

                    var order = new PurchaseOrderNew
                    {
                        DocumentNumber = docNumber,
                        TypeId = form.TypeId,
                        SupplierId = form.SupplierId,
                        TermId = form.TermsId,
                        CurrencyId = form.CurrencyId,
                        CurrencyRate = form.CurrencyRate,
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
                                                             .Where(s => s.Name == "Resubmitted")
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
                    response.Message = form.Status == "Draft" ? "Purchase return saved as draft." : "Purchase return successfully submitted.";
                    response.NewRow = order.Id;
                }
            }

            return response;
        }

        public async Task<ResponseModel> SaveItemsAsync(IList<PurchaseReturnNewLineItemVM> Items, int returnId, string? status = null)
        {
            var response = new ResponseModel();

            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

            var order = await context.PurchaseOrderNews.Where(s => s.Id == returnId).FirstOrDefaultAsync();

            if (order == null)
            {
                response.Status = "Error";
                response.Message = "Invalid purchase return";
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(status))
                {
                    int statusId = context.PurchaseStatuses.Where(s => s.Name == status).Select(s => s.Id).First();
                    order.StatusId = statusId;
                }

                try
                {
                    var citems = context.PurchaseOrderNewLineItems.Where(s => s.PurchaseOrderId == returnId);
                    // add item
                    // check items not in db
                    var items = citems.Select(s => s.Id);
                    // add excluding items already available
                    var newitems = Items.Where(s => items.Contains(s.Id) == false)
                                       .Select(s => new PurchaseOrderNewLineItem
                                       {
                                           PurchaseOrderId = returnId,
                                           LineNumber = s.LineNumber,
                                           ItemId = s.ItemId,
                                           ItemDescription = s.ItemDescription,
                                           Quantity = s.QuantityD,
                                           UnitId = s.UnitId,
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
                    response.Message = ex.Message;
                }
            }
            return response;
        }

        public async Task<ResponseModel> ApproveAsync(int returnId)
        {
            var response = new ResponseModel();

            var order = context.PurchaseOrderNews.Where(s => s.Id == returnId).FirstOrDefault();
            if (order == null)
            {
                response.Status = "Error";
                response.Message = "Invalid purchase order";
            }
            else
            {
                string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
                int statusId = context.PurchaseStatuses.Where(s => s.Name == "Approved").Select(s => s.Id).First();

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

        public async Task<ResponseModel> DisapproveAsync(int returnId, string? reason)
        {
            var response = new ResponseModel();


            var order = context.PurchaseOrderNews.Where(s => s.Id == returnId).FirstOrDefault();
            if (order == null)
            {
                response.Status = "Error";
                response.Message = "Invalid purchase order";
            }
            else
            {
                string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
                int statusId = context.PurchaseStatuses.Where(s => s.Name == "Disapproved").Select(s => s.Id).First();

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

        public async Task<ResponseModel> DeleteAsync(int returnId)
        {
            var response = new ResponseModel();
            try
            {
                var existingRequest = await context.PurchaseRequests
                                                   .Include(s => s.Status)
                                                   .FirstOrDefaultAsync(pr => pr.Id == returnId);

                if (existingRequest == null)
                {
                    response.Status = "Error";
                    response.Message = "Purchase request not found.";
                }
                else if (existingRequest.Status.Name != "Draft")
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