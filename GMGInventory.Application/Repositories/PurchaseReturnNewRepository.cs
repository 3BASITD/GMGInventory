using DocumentFormat.OpenXml.Wordprocessing;
using GMGInventory.Application.Contracts;
using GMGInventory.Application.Helpers;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using GMGInventory.Data.Migrations;
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
        private readonly string currentUserId;

        public PurchaseReturnNewReporitory(ApplicationDbContext context
                                        , IHttpContextAccessor httpContextAccessor
                                        , UserManager<User> userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
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
                                UnitId = receive.OrderItem.RequestItem.Item.Unit.Id
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
            var procurementHelper = new ProcurementHelper();

            var docNumber = await procurementHelper.GetNewReturn(context, "submitted");
            return docNumber;
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
                               SupplierId = retrn.PurchaseReceive.PurchaseOrder.Supplier.Id,
                               RRNumber = retrn.PurchaseReceive.DocumentNumber,
                               RRId = retrn.PurchaseReceive.Id,
                               PONumber = retrn.PurchaseReceive.PurchaseOrder.DocumentNumber,
                               POId = retrn.PurchaseReceive.PurchaseOrder.Id,
                               Warehouse = retrn.Warehouse.Code,
                               WarehouseId = retrn.Warehouse.Id,
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

        public async Task<IList<APIPurchaseReturn>> GetListAPI(DateTime? dt)
        {
            DateTime dtNow = DateTime.Now;
            DateTime dtYesterday = dtNow.AddDays(-1);
            var preturns = from preturn in context.PurchaseReturnNews
                           join userc in userManager.Users on preturn.UserCreated equals userc.Id
                           join userm in userManager.Users on preturn.UserModified equals userm.Id into um
                           from userm in um.DefaultIfEmpty()
                           where ((dt != null && preturn.ReturnDate.Date == dt.Value.Date)
                           || (dt == null && (preturn.BarcotechRequestDate == null
                           || preturn.BarcotechRequestDate > dtYesterday)))
                           && preturn.Status.Code.Trim().ToLower() == "approved"
                           select new APIPurchaseReturn
                           {
                               Id = preturn.Id,
                               DocumentNumber = preturn.DocumentNumber,
                               ReturnDate = preturn.ReturnDate,
                               UserCreated = userc.FirstName + " " + userc.LastName,
                               DateCreated = preturn.DateCreated,
                               LastUserModified = userm == null ? null : userm.FirstName + " " + userm.LastName,
                               LastDateModified = preturn.DateModified,
                               LineItems = from item in preturn.PurchasReturnLineItem
                                           select new APIPurchaseLineItem
                                           {
                                               Id = item.Id,
                                               ItemCode = item.ReceivingItem.OrderItem.RequestItem.ItemId != null
                                                      ? item.ReceivingItem.OrderItem.RequestItem.Item.Code
                                                      : "-1",
                                               Quantity = item.Quantity
                                           }
                           };
            var resultPReturns = await preturns.ToListAsync();

            foreach (var preturn in resultPReturns)
            {
                var retrievedpreturn = context.PurchaseReturnNews.Where(s => s.Id == preturn.Id).First();
                if (retrievedpreturn.BarcotechRequestDate == null)
                {
                    retrievedpreturn.BarcotechRequestDate = dtNow;
                    context.SaveChanges();
                }
            }

            return resultPReturns;
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
                              RRId = retrn.PurchaseReceive.Id,
                              PONumber = retrn.PurchaseReceive.PurchaseOrder.DocumentNumber,
                              POId = retrn.PurchaseReceive.PurchaseOrder.Id,
                              Supplier = retrn.PurchaseReceive.PurchaseOrder.Supplier.Name,
                              SupplierId = retrn.PurchaseReceive.PurchaseOrder.Supplier.Id,
                              CurrencyId = retrn.PurchaseReceive.PurchaseOrder.Currency.Id,
                              Currency = retrn.PurchaseReceive.PurchaseOrder.Currency.Name,
                              CurrencyCode = retrn.PurchaseReceive.PurchaseOrder.Currency.Name,
                              CurrencyRateD = retrn.PurchaseReceive.PurchaseOrder.CurrencyRate,
                              Warehouse = retrn.Warehouse.Name,
                              WarehouseId = retrn.Warehouse.Id,
                              IsSameWarehouse = retrn.PurchaseReceive.WarehouseId == retrn.WarehouseId,
                              DRNumber = retrn.PurchaseReceive.DeliveryReceiptNumber,
                              SINumber = retrn.PurchaseReceive.SalesInvoiceNumber,
                              Status = retrn.Status.Code,
                              ReturnDateD = retrn.ReturnDate,
                              Remarks = retrn.Remarks,
                              ApprovedBy = retrn.ApproverId == null ? null : (retrn.Approver.FirstName + " " + retrn.Approver.LastName),
                              DisapprovedBy = retrn.DisapproverId == null ? null : (retrn.Disapprover.FirstName + " " + retrn.Disapprover.LastName),
                              DisapprovedReason = retrn.DisapprovedReason,
                              ResubmitId = retrn.ResubmitId,
                              ResubmitFromId = resubmitfrom == null ? null : resubmitfrom.Id,
                              UserCreated = userc.FirstName + " " + userc.LastName,
                              DateCreatedD = retrn.DateCreated,
                              UserModified = userm == null ? null : (userm.FirstName + " " + userm.LastName),
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
            try
            {
                var status = await context.PurchaseStatuses.Where(s => s.Name.ToLower() == form.Status.ToLower()).FirstOrDefaultAsync();
                if (status == null)
                {
                    return new ResponseModel<int> { Status = "Error", Message = "Invalid status", NewRow = -1 };
                }
                else
                {
                    var previousRetrn = await context.PurchaseReturnNews
                                                     .Where(s => s.Id == form.ResubmitId)
                                                     .FirstOrDefaultAsync();

                    var procurementHelper = new ProcurementHelper();

                    var docNumber = previousRetrn == null
                                  ? await procurementHelper.GetNewReturn(context, form.Status)
                                  : previousRetrn.DocumentNumber.Split("-D")[0];

                    var retrn = new PurchaseReturnNew
                    {
                        DocumentNumber = docNumber,
                        PurchaseReceiveId = form.ReceivingId,
                        ReturnDate = form.DateReturn,
                        Remarks = form.Remarks,
                        StatusId = status.Id,
                        WarehouseId = form.WarehouseId
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

                    return new ResponseModel<int>
                    {
                        Status = "Success",
                        Message = form.Status.ToLower() == "draft" ? "Purchase return saved as draft." : "Purchase return successfully submitted.",
                        NewRow = retrn.Id
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<int> { Status = "Error", Message = SystemFormat.GetInnermostExceptionMessage(ex), NewRow = -1 };
            }
        }

        public async Task<ResponseModel> SaveItemsAsync(IList<PurchaseReturnNewLineItemVM> Items, int returnId, string? status = null)
        {
            try
            {
                var retrn = await context.PurchaseReturnNews
                                         .Include(s => s.Status)
                                         .Where(s => s.Id == returnId)
                                         .FirstOrDefaultAsync();

                if (retrn == null)
                {
                    return new ResponseModel { Status = "Error", Message = "Invalid purchase return" };
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(status))
                    {
                        int? statusId = context.PurchaseStatuses.Where(s => s.Name.ToLower() == status.ToLower()).Select(s => s.Id).FirstOrDefault();
                        if (statusId != null)
                            retrn.StatusId = statusId;

                        if (retrn.DocumentNumber.StartsWith("D") && retrn.Status.Name.ToLower() == "draft" && status.ToLower() != "draft")
                        {
                            var procurementHelper = new ProcurementHelper();
                            var docNumber = await procurementHelper.GetNewReturn(context, status);
                            retrn.DocumentNumber = docNumber;

                            await context.SaveChangesAsync(currentUserId);
                        }
                    }

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

                    return new ResponseModel { Status = "Success", Message = "Return item(s) successfully saved" };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { Status = "Error", Message = SystemFormat.GetInnermostExceptionMessage(ex) };
            }
        }

        public async Task<ResponseModel> ApproveAsync(int returnId)
        {
            try
            {
                var retrn = context.PurchaseReturnNews.Where(s => s.Id == returnId).FirstOrDefault();
                if (retrn == null)
                {
                    return new ResponseModel { Status = "Error", Message = "Invalid purchase return" };
                }
                else
                {
                    int statusId = context.PurchaseStatuses.Where(s => s.Name.ToLower() == "approved").Select(s => s.Id).First();

                    if (!string.IsNullOrWhiteSpace(retrn.ApproverId))
                    {
                        return new ResponseModel { Status = "Error", Message = "Purchase return already approved" };
                    }
                    else
                    {
                        retrn.ApproverId = currentUserId;
                        retrn.StatusId = statusId;

                        context.PurchaseReturnNews.Update(retrn);
                        await context.SaveChangesAsync(currentUserId);

                        return new ResponseModel { Status = "Success", Message = "Purchase return successfully approved." };
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { Status = "Error", Message = SystemFormat.GetInnermostExceptionMessage(ex) };
            }
        }

        public async Task<ResponseModel> DisapproveAsync(int returnId, string? reason)
        {
            try
            {
                var retrn = context.PurchaseReturnNews.Where(s => s.Id == returnId).FirstOrDefault();
                if (retrn == null)
                {
                    return new ResponseModel { Status = "Error", Message = "Invalid purchase return" };
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(retrn.DisapproverId))
                    {
                        return new ResponseModel { Status = "Error", Message = "Purchase return already disapproved" };
                    }
                    else
                    {
                        var tempDocumentNumber = retrn.DocumentNumber + "-D";
                        var latestDisapprovedReturn = context.PurchaseReturnNews
                                                             .Where(s => s.DocumentNumber.StartsWith(tempDocumentNumber))
                                                             .OrderByDescending(s => s.DocumentNumber)
                                                             .Select(s => s.DocumentNumber)
                                                             .FirstOrDefault();
                        var latestDisapprovedDocumentNumber = latestDisapprovedReturn == null
                                                            ? 0
                                                            : int.Parse(latestDisapprovedReturn.Replace(tempDocumentNumber, ""));
                        var disapprovedDocumentNumber = tempDocumentNumber + string.Format("{0:00}", latestDisapprovedDocumentNumber + 1);

                        int statusId = context.PurchaseStatuses.Where(s => s.Name.ToLower() == "disapproved").Select(s => s.Id).First();

                        retrn.DocumentNumber = disapprovedDocumentNumber;
                        retrn.DisapproverId = currentUserId;
                        retrn.DisapprovedReason = reason;
                        retrn.StatusId = statusId;

                        context.PurchaseReturnNews.Update(retrn);
                        await context.SaveChangesAsync(currentUserId);

                        return new ResponseModel { Status = "Success", Message = "Purchase return successfully disapproved" };
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { Status = "Error", Message = SystemFormat.GetInnermostExceptionMessage(ex) };
            }
        }

        public async Task<ResponseModel> DeleteAsync(int returnId)
        {
            try
            {
                var existingReturn = await context.PurchaseReturnNews
                                                     .Include(s => s.Status)
                                                     .Where(s => s.Id == returnId)
                                                     .FirstOrDefaultAsync();

                if (existingReturn == null)
                {
                    return new ResponseModel { Status = "Error", Message = "Invalid Purchase Return. Record can not be found" };
                }
                else if (existingReturn.Status.Name.ToLower() != "draft")
                {
                    return new ResponseModel { Status = "Error", Message = "Only draft can be deleted." };
                }
                else
                {
                    var previousReturn = await context.PurchaseReturnNews
                                                      .Where(s => s.ResubmitId == returnId)
                                                      .FirstOrDefaultAsync();
                    if (previousReturn != null)
                    {
                        var statusDisapproved = await context.PurchaseStatuses
                                                             .Where(s => s.Name.ToLower() == "disapproved")
                                                             .FirstAsync();
                        previousReturn.ResubmitId = null;
                        previousReturn.StatusId = statusDisapproved.Id;
                        await context.SaveChangesAsync(currentUserId);
                    }

                    var receivingItems = await context.PurchaseReturnNewLineItems
                                                      .Where(s => s.PurchaseReturnId == returnId)
                                                      .ToListAsync();

                    context.PurchaseReturnNewLineItems.RemoveRange(receivingItems);

                    context.PurchaseReturnNews.Remove(existingReturn);
                    await context.SaveChangesAsync(currentUserId);

                    return new ResponseModel { Status = "Success", Message = "Purchase Return successfully deleted" };
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