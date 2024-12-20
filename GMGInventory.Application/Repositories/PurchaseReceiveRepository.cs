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

        private async Task<IList<PurchaseReceiveVM>> GetListAsync()
        {
            var receiveVM = (from receive in context.PurchaseReceivings
                                                //.Include(s => s.Supplier)
                                                //.Include(s => s.Term)
                                                //.Include(s => s.PurchaseOrder)
                                                //.Include(s => s.Warehouse)
                                                //.Include(s => s.Status)
                           select new PurchaseReceiveVM
                           {
                               Id = receive.Id,
                               DocumentNumber = receive.DocumentNumber,
                               Supplier = receive.PurchaseOrder.Supplier.Code,
                               Term = receive.PurchaseOrder.Term.Name,
                               PONumber = receive.PurchaseOrder.DocumentNumber,
                               Warehouse = receive.Warehouse.Code,
                               Status = receive.Status.Code,
                               ReceiveDateD = receive.ReceiveDate
                           });
            return await receiveVM.ToListAsync();
        }

        public async Task<ForDataTables<PurchaseReceiveVM>> GetListDTAsync(DataTablesRequestForm form)
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
                    s.Term,
                    s.PONumber,
                    s.Warehouse,
                    s.Status,
                    s.ReceiveDate
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
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<PurchaseReceiveVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public async Task<PurchaseReceiveDetailsVM?> GetDetails(int requestId)
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
                          where receive.Id == requestId
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
                              //PurchaseRequestType = po.PurchaseOrder.PurchaseRequest.Type.Code,
                              Warehouse = receive.Warehouse.Code,
                              WarehouseId = receive.Warehouse.Id,
                              Status = receive.Status.Code,
                              ReceiveDateD = receive.ReceiveDate,
                              Remarks = receive.Remarks,
                              Currency = receive.PurchaseOrder.Currency.Code,
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
                              DateModifiedD = receive.DateModified
                          };
            return await details.FirstOrDefaultAsync();
        }

        public async Task<IList<PurchaseReceiveLineItemVM>> GetItemListAsync(int purchaseReceivingId)
        {
            var receiveVM = from receive in context.PurchaseReceivingLineItems
                                                   //.Include(s => s.Item)
                                                   //.Include(s => s.Unit)
                            where receive.PurchaseReceivingId == purchaseReceivingId
                            select new PurchaseReceiveLineItemVM
                            {
                                Id = receive.Id,
                                PurchaseReceivingId = receive.PurchaseReceivingId,
                                LineNumber = receive.OrderItem.LineNumber,
                                OrderItemId = receive.OrderItem.RequestItem.ItemId ?? 0,
                                ItemCode = receive.OrderItem.RequestItem.Item.Code,
                                ItemPartNumber = receive.OrderItem.RequestItem.Item.PartNumber,
                                ItemDescription = receive.OrderItem.RequestItem.ItemDescription,
                                QuantityD = receive.Quantity,
                                UnitId = receive.OrderItem.RequestItem.UnitId,
                                UnitCode = receive.OrderItem.RequestItem.Unit.Code,
                                UnitCost = receive.OrderItem.UnitCost
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
                }.Any(a => a != null && a.ToLower().Contains(form.SearchValue)));
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
                                           //LineNumber = s.LineNumber,
                                           //ItemId = s.ItemId,
                                           //ItemDescription = s.ItemDescription ?? "",
                                           Quantity = s.QuantityD,
                                           //UnitId = s.UnitId,
                                           //UnitCost = s.UnitCost
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
                    //SupplierId = purchaseOrder!.SupplierId,
                    //TermId = purchaseOrder!.TermId,
                    WarehouseId = purchaseOrder!.DeliveryAddressId,
                   // Discount1 = purchaseOrder!.Discount1,
                   // Discount2 = purchaseOrder!.Discount2,
                    //CurrencyId = purchaseOrder!.CurrencyId,
                    //CurrencyRate = purchaseOrder.CurrencyRate
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
            var pos = from porder in context.PurchaseOrderNews

                      let receiving = context.PurchaseReceivings
                                            .Where(s => s.PurchaseOrderId == porder.Id)
                                            .Select(s => s.Status.Name).FirstOrDefault()

                      where porder.Status.Name.ToLower() == "approved" && receiving.ToLower() != "approved"
                      select new JsonOptionsModel
                      {
                          Id = porder.Id.ToString(),
                          Text = porder.DocumentNumber,
                          Selected = SelectedId != null && porder.Id == SelectedId
                      };
                        
            return await pos.OrderBy(s => s.Text).ToListAsync();
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
                    await context.SaveChangesAsync(currentUserId);

                    response.Status = "Success";
                    response.Message = "Purchase receive successfully approved.";
                }
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
                                //
                                //.Include(s => s.Unit)
                                .Where(s => s.PurchaseOrderId == PurchaseOrderId)
                                .Select(s => new JsonPurchaseItemOptionsModel
                                {
                                    Id = s.Id.ToString(),
                                    Text = s.RequestItem.ItemDescription,
                                    ItemId = s.RequestItem.Item.Id,
                                    ItemCode = s.RequestItem.Item.Code,
                                    ItemDescription = s.RequestItem.ItemDescription,
                                    PartNumber = s.RequestItem.Item.PartNumber,
                                    Quantity = s.Quantity,
                                    Unit = s.RequestItem.Unit.Name,
                                    UnitId = s.RequestItem.Unit.Id,
                                    UnitCost = s.UnitCost,
                                    LineNumber = s.LineNumber
                                    //Served = context.PurchaseOrderNewLineItems
                                    //                .Where(a => a.ItemId == s.ItemId
                                    //                         && a.ItemDescription == s.ItemDescription
                                    //                         && a.PurchaseOrder.Status.Name.ToLower() == "approved")
                                    //                .Select(a => a.Quantity)
                                    //                .Sum()
                                    //  Selected = SelectedId != null && s.Id == SelectedId
                                })
                                .OrderBy(s => s.LineNumber)
                                .ToListAsync();
        }
    }
}