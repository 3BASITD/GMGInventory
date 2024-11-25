using GMGInventory.Application.Contracts;
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
                                                .Include(s => s.Supplier)
                                                .Include(s => s.Term)
                                                .Include(s => s.PurchaseOrder)
                                                .Include(s => s.Warehouse)
                                                .Include(s => s.Status)
                           select new PurchaseReceiveVM
                           {
                               Id = receive.Id,
                               DocumentNumber = receive.DocumentNumber,
                               Supplier = receive.Supplier.Code,
                               Term = receive.Term.Name,
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
                                                 .Include(s => s.PurchaseOrder)
                                                 .Include(s => s.Supplier)
                                                 .Include(s => s.Term)
                                                 .Include(s => s.Warehouse)
                                                 .Include(s => s.Currency)
                                                 .Include(s => s.Status)
                          let resubmitfrom = context.PurchaseReceivings
                                                    .Where(s => s.ResubmitId == receive.Id)
                                                    .FirstOrDefault()

                          let po = context.PurchaseOrderNewLineItems
                                                  .Where(s => s.PurchaseOrderId == receive.PurchaseOrderId)
                                                  .FirstOrDefault()

                          join userc in userManager.Users on receive.UserCreated equals userc.Id into c
                          from userc in c.DefaultIfEmpty()
                          join userm in userManager.Users on receive.UserModified equals userm.Id into m
                          from userm in m.DefaultIfEmpty()
                          where receive.Id == requestId
                          select new PurchaseReceiveDetailsVM
                          {
                              Id = receive.Id,
                              DocumentNumber = receive.DocumentNumber,
                              PurchaseRequestType = po.PurchaseRequest.Type.Code,
                              Warehouse = receive.Warehouse.Code,
                              WarehouseId = receive.Warehouse.Id,
                              Status = receive.Status.Code,
                              ReceiveDateD = receive.ReceiveDate,
                              Remarks = receive.Remarks,
                              Currency = receive.Currency.Code,
                              CurrencyId = receive.Currency.Id,
                              CurrencyRateD = receive.CurrencyRate,
                              ApprovedBy = receive.ApproverId == null ? null : receive.Approver.FirstName + " " + receive.Approver.LastName,
                              DisapprovedBy = receive.DisapproverId == null ? null : receive.Disapprover.FirstName + " " + receive.Disapprover.LastName,
                              DisapprovedReason = receive.DisapprovedReason,
                              ResubmitId = receive.ResubmitId,
                              ResubmitFromId = resubmitfrom == null ? null : resubmitfrom.Id,
                              UserCreated = userc.FirstName + " " + userc.LastName,
                              DateCreatedD = receive.DateCreated,
                              UserModified = userm.FirstName + " " + userm.LastName,
                              DateModifiedD = receive.DateModified
                          };
            return await details.FirstOrDefaultAsync();
        }

        public async Task<IList<PurchaseReceiveLineItemVM>> GetItemListAsync(int purchaseReceivingId)
        {
            var receiveVM = from receive in context.PurchaseReceivingLineItems
                                                   .Include(s => s.Item)
                                                   .Include(s => s.Unit)
                            where receive.PurchaseReceivingId == purchaseReceivingId
                            select new PurchaseReceiveLineItemVM
                            {
                                Id = receive.Id,
                                PurchaseReceivingId = receive.PurchaseReceivingId,
                                LineNumber = receive.LineNumber,
                                ItemId = receive.ItemId,
                                ItemCode = receive.Item.Code,
                                ItemPartNumber = receive.Item.PartNumber,
                                ItemDescription = receive.ItemDescription,
                                QuantityD = receive.Quantity,
                                UnitId = receive.UnitId,
                                UnitCode = receive.Unit.Code,
                                UnitCost = receive.UnitCost
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
                    int statusId = context.PurchaseStatuses.Where(s => s.Name == status).Select(s => s.Id).First();
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
                                           LineNumber = s.LineNumber,
                                           ItemId = s.ItemId,
                                           ItemDescription = s.ItemDescription,
                                           Quantity = s.QuantityD,
                                           UnitId = s.UnitId,
                                           UnitCost = s.UnitCost
                                       });
                    context.PurchaseReceivingLineItems.AddRange(newitems);

                    // update item
                    // get items available in db
                    var existitems = Items.Where(s => items.Contains(s.Id));
                    foreach (var ei in existitems)
                    {
                        // update all available in db
                        var dbitem = await citems.Where(s => s.Id == ei.Id).FirstAsync();
                        dbitem.LineNumber = ei.LineNumber;
                        dbitem.Quantity = ei.QuantityD;

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
                    response.Message = ex.Message;
                }
            }
            return response;
        }

        public async Task<ResponseModel<int>> AddAsync(PurchaseReceiveSaveVM form, List<PurchaseReceiveLineItemVM> Items)
        {
            var response = new ResponseModel<int>();
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

            var status = await context.PurchaseStatuses.Where(s => s.Name == form.Status).FirstOrDefaultAsync();
            if (status == null)
            {
                response.Status = "Error";
                response.Message = "Invalid status.";
                response.NewRow = -1;
            }
            else
            {
                var purchaseOrder = await context.PurchaseOrderNews
                                                   .Where(s => s.Id == form.DocumentNumber)
                                                   .FirstOrDefaultAsync();
                var previousReceive = await context.PurchaseReceivings
                                                      .Where(s => s.Id == form.ResubmitId)
                                                      .FirstOrDefaultAsync();

                var receive = new PurchaseReceiving
                {
                    DocumentNumber = purchaseOrder.DocumentNumber,
                    PurchaseOrderId = purchaseOrder.Id,
                    SupplierId = form.SupplierId,
                    TermId = form.TermId,
                    WarehouseId = form.WarehouseId,
                    DeliveryReceiptNumber = form.DeliveryReceiptNumber,
                    SalesInvoiceNumber = form.SalesInvoiceNumber,

                    ReceiveDate = form.DateReceived,
                    Remarks = form.Remarks,
                    StatusId = status.Id,
                    ResubmitId = form.ResubmitId,
                    Discount1 = form.Discount1,
                    Discount2 = form.Discount2,

                    CurrencyId = purchaseOrder.CurrencyId,
                    CurrencyRate = purchaseOrder.CurrencyRate
                };
                await context.PurchaseReceivings.AddAsync(receive);
                try
                {
                    await context.SaveChangesAsync(currentUserId);
                }
                catch (Exception ex)
                {

                }

                if (previousReceive != null)
                {
                    var statusResubmitted = await context.PurchaseStatuses
                                                         .Where(s => s.Name == "Resubmitted")
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
                response.Message = form.Status == "Draft" ? "Purchase receive saved as draft." : "Purchase receive successfully submitted.";
                response.NewRow = receive.Id;
            }

            return response;
        }

        public async Task<IList<JsonOptionsModel>> GetPurchaseOrderOptions(int? SelectedId)
        {
            return await context.PurchaseOrderNews
                                .Include(s => s.Status)
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
                int statusId = context.PurchaseStatuses.Where(s => s.Name == "Approved").Select(s => s.Id).First();

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
                int statusId = context.PurchaseStatuses.Where(s => s.Name == "Disapproved").Select(s => s.Id).First();

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
            //var purchaseType = await context.PurchaseOrderTypes.Where(s => s.Id == TypeId).FirstOrDefaultAsync();

            //var typeCode = purchaseType == null ? "" : $"{purchaseType.Code}-";
            var docNumberPrefix = $"RR{DateTime.Now.Year}-";
            var lastDocument = await context.PurchaseReceivings
                                            .Where(s => s.DocumentNumber.StartsWith(docNumberPrefix))
                                            .OrderByDescending(s => s.DocumentNumber)
                                            .Select(s => s.DocumentNumber)
                                            .FirstOrDefaultAsync();

            int.TryParse(lastDocument?.Replace(docNumberPrefix, "") ?? "0", out int docNumberLast);
            return $"{docNumberPrefix}{docNumberLast + 1:D4}";
        }
    }
}