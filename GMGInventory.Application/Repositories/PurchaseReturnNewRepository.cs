using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            var receivings = from receiveItems in context.PurchaseReceivingLineItems
                         let returnItems = receiveItems.PurchaseReturnLineItem
                                        .Where(a => a.PurchaseReturn.Status.Name.ToLower() == "approved")
                                        .Select(a => (int?)a.Quantity)
                                        .Sum() ?? 0
                         where returnItems != receiveItems.Quantity
                         select receiveItems.PurchaseReceivingId;


            return await context.PurchaseReceivings
                                .Include(s => s.Status)
                                .Where(s => s.Status.Name.ToLower() == "approved"
                                         && receivings.Contains(s.Id))
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
            var receiveVM = from receive in context.PurchaseReceivingLineItems
                            where receive.PurchaseReceivingId == ReceiveId
                            orderby receive.OrderItem.LineNumber ascending
                            select new JsonPurchaseItemOptionsModel
                            {
                                Id = receive.Id.ToString(),
                                Text = receive.OrderItem.RequestItem.ItemDescription,
                                LineNumber = receive.OrderItem.RequestItem.LineNumber,
                                ItemId = receive.OrderItem.RequestItem.Item.Id,
                                ItemCode = receive.OrderItem.RequestItem.Item.Code,
                                PartNumber = receive.OrderItem.RequestItem.Item.PartNumber,
                                Quantity = receive.Quantity,
                                Unit = receive.OrderItem.RequestItem.Item.Unit.Name,
                                UnitId = receive.OrderItem.RequestItem.Item.Unit.Id,
                                Selected = SelectedId != null && receive.Id == SelectedId
                            };
            return await receiveVM.Where(s => s.UnorderedQuantity > 0).ToListAsync();
        }

        public async Task<IList<PurchaseReturnNewLineItemVM>> GetPurchaseReceiveItemList(int ReceiveId)
        {
            var receiveVM = from receive in context.PurchaseReceivingLineItems
                            where receive.PurchaseReceivingId == ReceiveId
                            orderby receive.OrderItem.LineNumber ascending
                            select new PurchaseReturnNewLineItemVM
                            {
                                LineNumber = receive.OrderItem.LineNumber,
                                ItemId = receive.OrderItem.RequestItem.Item.Id,
                                ItemCode = receive.OrderItem.RequestItem.Item.Code,
                                ItemPartNumber = receive.OrderItem.RequestItem.Item.PartNumber,
                                ItemDescription = receive.OrderItem.RequestItem.ItemDescription,
                                QuantityD = receive.Quantity,
                                UnitId = receive.OrderItem.RequestItem.Item.Unit.Id,
                                UnitCode = receive.OrderItem.RequestItem.Item.Unit.Code,
                                UnitCostD = receive.OrderItem.UnitCost
                            };

            return await receiveVM.ToListAsync();
        }

        public async Task<string> GetNewDocumentNumber()
        {
            var docNumberPrefix = $"RO{DateTime.Now.Year % 100:D2}";
            var lastDocument = await context.PurchaseReturnNews
                                            .Where(s => s.DocumentNumber.StartsWith(docNumberPrefix))
                                            .OrderByDescending(s => s.DocumentNumber)
                                            .Select(s => s.DocumentNumber)
                                            .FirstOrDefaultAsync();

            int.TryParse(lastDocument?.Replace(docNumberPrefix, "") ?? "0", out int docNumberLast);
            return $"{docNumberPrefix}{docNumberLast + 1:D6}";
        }

        public async Task<IList<PurchaseReturnNewVM>> GetListAsync(PurchaseReturnFilter filter)
        {
            var returnVM = from retrn in context.PurchaseReturnNews
                           where (filter.SupplierId == null || retrn.PurchaseReceive.PurchaseOrder.SupplierId == filter.SupplierId)
                           && (filter.DateFrom == null || retrn.ReturnDate >= filter.DateFrom)
                           && (filter.DateTo == null || retrn.ReturnDate <= filter.DateTo)
                           && (filter.StatusId == null || retrn.StatusId == filter.StatusId)
                           select new PurchaseReturnNewVM
                           {
                               Id = retrn.Id,
                               DocumentNumber = retrn.DocumentNumber,
                               Supplier = retrn.PurchaseReceive.PurchaseOrder.Supplier.Code,
                               RRNumber = retrn.PurchaseReceive.DocumentNumber,
                               PONumber = retrn.PurchaseReceive.PurchaseOrder.DocumentNumber,
                               Warehouse = retrn.PurchaseReceive.Warehouse.Code,
                               DRNumber = retrn.PurchaseReceive.DeliveryReceiptNumber,
                               SINumber = retrn.PurchaseReceive.SalesInvoiceNumber,
                               Status = retrn.Status.Code,
                               ReturnDateD = retrn.ReturnDate,
                               Remarks = retrn.Remarks
                           };
            return await returnVM.ToListAsync();
        }

        public async Task<ForDataTables<PurchaseReturnNewVM>> GetListDTAsync(DataTablesRequestForm form, PurchaseReturnFilter filter)
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
                    s.PONumber,
                    s.Warehouse,
                    s.DRNumber,
                    s.SINumber,
                    s.Status,
                    s.ReturnDate,
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

        public async Task<IList<APIPurchaseReturn>> GetListAPI(DateTime? ReturnDate)
        {
            if (ReturnDate == null) ReturnDate = DateTime.Today;

            var preturns = from preturn in context.PurchaseReturnNews
                           where ReturnDate.Value.Date == preturn.ReturnDate.Date
                           && preturn.Approver != null
                           select new APIPurchaseReturn
                           {
                               Id = preturn.Id,
                               DocumentNumber = preturn.DocumentNumber,
                               ReturnDate = preturn.ReturnDate,
                               LineItems = from item in preturn.PurchasReturnLineItem
                                           select new APIPurchaseLineItem
                                           {
                                               Id = item.Id,
                                               ItemId = item.ReceivingItem.OrderItem.RequestItem.ItemId != null
                                                      ? item.ReceivingItem.OrderItem.RequestItem.ItemId.Value
                                                      : -1,
                                               Quantity = item.Quantity
                                           }
                           };

            return await preturns.ToListAsync();
        }

        public async Task<PurchaseReturnNewDetailedVM?> GetDetails(int returnId)
        {
            var details = from retrn in context.PurchaseReturnNews
                          let resubmitfrom = context.PurchaseReturnNews
                                                    .Where(s => s.ResubmitId == retrn.Id)
                                                    .FirstOrDefault()
                          join userc in userManager.Users on retrn.UserCreated equals userc.Id
                          join userm in userManager.Users on retrn.UserModified equals userm.Id into m
                          from userm in m.DefaultIfEmpty()
                          where retrn.Id == returnId
                          select new PurchaseReturnNewDetailedVM
                          {
                              Id = retrn.Id,
                              DocumentNumber = retrn.DocumentNumber,
                              RRNumber = retrn.PurchaseReceive.DocumentNumber,
                              PONumber = retrn.PurchaseReceive.PurchaseOrder.DocumentNumber,
                              Supplier = retrn.PurchaseReceive.PurchaseOrder.Supplier.Name,
                              CurrencyId = retrn.PurchaseReceive.PurchaseOrder.Currency.Id,
                              Currency = retrn.PurchaseReceive.PurchaseOrder.Currency.Name,
                              CurrencyCode = retrn.PurchaseReceive.PurchaseOrder.Currency.Name,
                              CurrencyRateD = retrn.PurchaseReceive.PurchaseOrder.CurrencyRate,
                              Warehouse = retrn.PurchaseReceive.Warehouse.Name,
                              DRNumber = retrn.PurchaseReceive.DeliveryReceiptNumber,
                              SINumber = retrn.PurchaseReceive.SalesInvoiceNumber,
                              Status = retrn.Status.Code,
                              ReturnDateD = retrn.ReturnDate,
                              Remarks = retrn.Remarks,
                              ApprovedBy = retrn.ApproverId == null ? null : SystemFormat.Name(retrn.Approver.FirstName, retrn.Approver.LastName, retrn.Approver.Nickname),
                              DisapprovedBy = retrn.DisapproverId == null ? null : SystemFormat.Name(retrn.Disapprover.FirstName, retrn.Disapprover.LastName, retrn.Disapprover.Nickname),
                              DisapprovedReason = retrn.DisapprovedReason,
                              ResubmitId = retrn.ResubmitId,
                              ResubmitFromId = resubmitfrom == null ? null : resubmitfrom.Id,
                              UserCreated = SystemFormat.Name(userc.FirstName, userc.LastName, userc.Nickname),
                              DateCreatedD = retrn.DateCreated,
                              UserModified = userm == null ? null : SystemFormat.Name(userm.FirstName, userm.LastName, userm.Nickname),
                              DateModifiedD = retrn.DateModified
                          };
            return await details.FirstOrDefaultAsync();
        }
        
        public async Task<IList<PurchaseReturnNewLineItemVM>> GetItemsFromRRListAsync(int purchaseReceivingId) // for create
        {
            var returnVM = from receive in context.PurchaseReceivingLineItems
                            let retrn = context.PurchaseReturnNewLineItems
                                         .Where(s => s.ReceivingItemId == receive.Id).FirstOrDefault()
                           let item = receive.OrderItem.RequestItem.Item

                           where receive.PurchaseReceivingId == purchaseReceivingId

                           select new PurchaseReturnNewLineItemVM
                           {
                               Id = retrn == null ? 0 : retrn.Id,
                               ToReturn = retrn != null,
                               ItemId = item == null ? null : item.Id,
                               ItemCode = item == null ? null : item.Code,
                               ItemPartNumber = item == null ? null : item.PartNumber,
                               ItemDescription = receive.OrderItem.RequestItem.ItemDescription,
                               LineNumber = receive.OrderItem.LineNumber,
                               QuantityD = retrn == null ? 0 : retrn.Quantity,
                               ReceivingItemId = receive.Id,
                               ReceivedQuantityD = receive.Quantity,
                               ServedQuantityD = context.PurchaseReturnNewLineItems
                                                       .Where(a => a.PurchaseReturn.Status.Name.ToLower() == "approved"
                                                                    && a.ReceivingItemId == receive.Id)
                                                       .Select(a => a.Quantity)
                                                       .Sum(),

                               UnitId = item == null ? receive.OrderItem.RequestItem.Unit.Id : item.Unit.Id,
                               UnitCode = item == null ? receive.OrderItem.RequestItem.Unit.Code : item.Unit.Code,
                               UnitCostD = receive.OrderItem.UnitCost
                           };
            var rvms = await returnVM.ToListAsync();
            foreach (var rvm in rvms)
                rvm.LineNumber = rvms.IndexOf(rvm) + 1;

            return rvms;
        }

        public async Task<IList<PurchaseReturnNewLineItemVM>> GetWithRRItemsListAsync(int purchaseReturnId) // details
        {
            var receiveId = await context.PurchaseReturnNews
                                         .Where(s => s.Id == purchaseReturnId)
                                         .Select(s => s.PurchaseReceiveId)
                                         .FirstOrDefaultAsync();
            var returnVM = from receive in context.PurchaseReceivingLineItems
                           let retrn = context.PurchaseReturnNewLineItems
                                        .Where(s => s.ReceivingItemId == receive.Id && s.PurchaseReturnId == purchaseReturnId).FirstOrDefault()
                           let item = receive.OrderItem.RequestItem.Item

                           where receive.PurchaseReceivingId == receiveId

                           select new PurchaseReturnNewLineItemVM
                           {
                               Id = retrn == null ? 0 : retrn.Id,
                               ToReturn = retrn != null,
                               ItemId = item == null ? null : item.Id,
                               ItemCode = item == null ? null : item.Code,
                               ItemPartNumber = item == null ? null : item.PartNumber,
                               ItemDescription = receive.OrderItem.RequestItem.ItemDescription,
                               LineNumber = receive.OrderItem.LineNumber,
                               QuantityD = retrn == null ? 0 : retrn.Quantity,
                               ReceivingItemId = receive.Id,
                               ReceivedQuantityD = receive.Quantity,
                               ServedQuantityD = context.PurchaseReturnNewLineItems
                                                       .Where(a => a.PurchaseReturn.Status.Name.ToLower() == "approved"
                                                                    && a.ReceivingItemId == receive.Id)
                                                       .Select(a => a.Quantity)
                                                       .Sum(),
                               UnitId = item == null ? receive.OrderItem.RequestItem.Unit.Id : item.Unit.Id,
                               UnitCode = item == null ? receive.OrderItem.RequestItem.Unit.Code : item.Unit.Code,
                               UnitCostD = receive.OrderItem.UnitCost
                           };
            var rvms = await returnVM.ToListAsync();
            foreach (var rvm in rvms)
                rvm.LineNumber = rvms.IndexOf(rvm) + 1;

            return rvms;
        } 
        public async Task<IList<PurchaseReturnNewLineItemVM>> GetItemListAsync(int purchaseReturnId)  // for printing 
        {
            var receiveId = await context.PurchaseReturnNews
                                         .Where(s => s.Id == purchaseReturnId)
                                         .Select(s => s.PurchaseReceiveId)
                                         .FirstOrDefaultAsync();

            var returnVM = from retrn in context.PurchaseReturnNewLineItems
                           join receive in context.PurchaseReceivingLineItems on retrn.ReceivingItemId equals receive.Id

                           where retrn.PurchaseReturnId == purchaseReturnId

                           let item = receive.OrderItem.RequestItem.Item

                           select new PurchaseReturnNewLineItemVM
                           {
                               Id = retrn == null ? 0 : retrn.Id,
                               ToReturn = retrn != null,
                               ItemId = item == null ? null : item.Id,
                               ItemCode = item == null ? null : item.Code,
                               ItemPartNumber = item == null ? null : item.PartNumber,
                               ItemDescription = receive.OrderItem.RequestItem.ItemDescription,
                               LineNumber = receive.OrderItem.LineNumber,
                               QuantityD = retrn == null ? 0 : retrn.Quantity,
                               ReceivingItemId = receive.Id,
                               ReceivedQuantityD = receive.Quantity,
                               UnitId = item == null ? receive.OrderItem.RequestItem.Unit.Id : item.Unit.Id,
                               UnitCode = item == null ? receive.OrderItem.RequestItem.Unit.Code : item.Unit.Code,
                               UnitCostD = receive.OrderItem.UnitCost
                           };
            var rvms = await returnVM.ToListAsync();
            foreach (var rvm in rvms)
                rvm.LineNumber = rvms.IndexOf(rvm) + 1;

            return rvms;
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

            var status = await context.PurchaseStatuses.Where(s => s.Name.ToLower() == form.Status.ToLower()).FirstOrDefaultAsync();
            if (status == null)
            {
                response.Status = "Error";
                response.Message = "Invalid status.";
                response.NewRow = -1;
            }
            else
            {
                var previousRetrn = await context.PurchaseReturnNews
                                                 .Where(s => s.Id == form.ResubmitId)
                                                 .FirstOrDefaultAsync();

                var docNumber = previousRetrn == null
                              ? await GetNewDocumentNumber()
                              : previousRetrn.DocumentNumber;

                var retrn = new PurchaseReturnNew
                {
                    DocumentNumber = docNumber,
                    PurchaseReceiveId = form.ReceivingId,
                    ReturnDate = form.DateReturn,
                    Remarks = form.Remarks,
                    StatusId = status.Id
                };
                await context.PurchaseReturnNews.AddAsync(retrn);
                await context.SaveChangesAsync(currentUserId);

                if (previousRetrn != null)
                {
                    var statusResubmitted = await context.PurchaseStatuses
                                                         .Where(s => s.Name.ToLower() == "resubmitted")
                                                         .FirstAsync();
                    previousRetrn.ResubmitId = retrn.Id;
                    previousRetrn.StatusId = statusResubmitted.Id;
                    context.PurchaseReturnNews.Update(previousRetrn);
                    await context.SaveChangesAsync(currentUserId);
                }

                await SaveItemsAsync(Items, retrn.Id);

                response.Status = "Success";
                response.Message = form.Status.ToLower() == "draft" ? "Purchase return saved as draft." : "Purchase return successfully submitted.";
                response.NewRow = retrn.Id;
            }

            return response;
        }

        public async Task<ResponseModel> SaveItemsAsync(IList<PurchaseReturnNewLineItemVM> Items, int returnId, string? status = null)
        {
            var response = new ResponseModel();

            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

            var retrn = await context.PurchaseReturnNews.Where(s => s.Id == returnId).FirstOrDefaultAsync();

            if (retrn == null)
            {
                response.Status = "Error";
                response.Message = "Invalid purchase return";
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(status))
                {
                    int statusId = context.PurchaseStatuses.Where(s => s.Name.ToLower() == status.ToLower()).Select(s => s.Id).First();
                    retrn.StatusId = statusId;
                }

                try
                {
                    Items = Items.Where(s => s.ToReturn).ToList();

                    var citems = context.PurchaseReturnNewLineItems.Where(s => s.PurchaseReturnId == returnId);
                    // add item
                    // check items not in db
                    var items = citems.Select(s => s.Id);
                    // add excluding items already available
                    var newitems = Items.Where(s => items.Contains(s.Id) == false)
                                       .Select(s => new PurchaseReturnNewLineItem
                                       {
                                           PurchaseReturnId = returnId,
                                           ReceivingItemId = s.ReceivingItemId,
                                           Quantity = s.QuantityD
                                       });
                    context.PurchaseReturnNewLineItems.AddRange(newitems);

                    // update item
                    // get items available in db
                    var existitems = Items.Where(s => items.Contains(s.Id));
                    foreach (var ei in existitems)
                    {
                        // update all available in db
                        var dbitem = await citems.Where(s => s.Id == ei.Id).FirstAsync();
                        dbitem.Quantity = ei.QuantityD;

                        context.PurchaseReturnNewLineItems.Update(dbitem);
                    }

                    // delete item
                    // get items not in form
                    var fitems = Items.Select(s => s.Id);
                    var ritems = citems.Where(s => fitems.Contains(s.Id) == false);
                    // remove unavailable items
                    context.PurchaseReturnNewLineItems.RemoveRange(ritems);

                    await context.SaveChangesAsync(currentUserId);

                    response.Status = "Success";
                    response.Message = "Return item(s) successfully saved";
                }
                catch (Exception ex)
                {
                    response.Status = "Error";
                    response.Message = SystemFormat.GetInnermostExceptionMessage(ex);
                }
            }
            return response;
        }

        public async Task<ResponseModel> ApproveAsync(int returnId)
        {
            var response = new ResponseModel();

            var retrn = context.PurchaseReturnNews.Where(s => s.Id == returnId).FirstOrDefault();
            if (retrn == null)
            {
                response.Status = "Error";
                response.Message = "Invalid purchase return";
            }
            else
            {
                string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
                int statusId = context.PurchaseStatuses.Where(s => s.Name.ToLower() == "approved").Select(s => s.Id).First();

                if (!string.IsNullOrWhiteSpace(retrn.ApproverId))
                {
                    response.Status = "Error";
                    response.Message = "Purchase return already approved.";
                }
                else
                {
                    retrn.ApproverId = currentUserId;
                    retrn.StatusId = statusId;

                    context.PurchaseReturnNews.Update(retrn);
                    await context.SaveChangesAsync(currentUserId);

                    response.Status = "Success";
                    response.Message = "Purchase return successfully approved.";
                }
            }
            return response;
        }

        public async Task<ResponseModel> DisapproveAsync(int returnId, string? reason)
        {
            var response = new ResponseModel();


            var retrn = context.PurchaseReturnNews.Where(s => s.Id == returnId).FirstOrDefault();
            if (retrn == null)
            {
                response.Status = "Error";
                response.Message = "Invalid purchase return";
            }
            else
            {
                string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
                int statusId = context.PurchaseStatuses.Where(s => s.Name.ToLower() == "disapproved").Select(s => s.Id).First();

                if (!string.IsNullOrWhiteSpace(retrn.DisapproverId))
                {
                    response.Status = "Error";
                    response.Message = "Purchase return already disapproved.";
                }
                else
                {
                    retrn.DisapproverId = currentUserId;
                    retrn.DisapprovedReason = reason;
                    retrn.StatusId = statusId;

                    context.PurchaseReturnNews.Update(retrn);
                    await context.SaveChangesAsync(currentUserId);

                    response.Status = "Success";
                    response.Message = "Purchase return successfully disapproved.";
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