using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using GMGInventory.Application.Contracts;
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
    public class SalesReturnRepository : ISalesReturnRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly string currentUserId;

        public SalesReturnRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
            , UserManager<User> userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
        }

        public async Task<SalesReturnVM?> GetDetailsAsync(int? Id)
        {
            var returnVM = from ret in context.SalesReturns
                           join userc in userManager.Users on ret.UserCreated equals userc.Id
                           join userm in userManager.Users on ret.UserModified equals userm.Id into um
                           from userm in um.DefaultIfEmpty()
                           where ret.Id == Id
                           //&& ret.SalesReturnLineItems.Any(s=> (s.InvoiceLineItem!.Invoice.Status.Code.Trim().ToLower() == "invoiced" || s.InvoiceLineItem!.Invoice.Status.Code.Trim().ToLower() == "draft"))
                           select new SalesReturnVM
                           {
                               BIRInvoiceNumber = string.Join(",", ret.SalesReturnLineItems.Select(s => s.InvoiceLineItem.Invoice.ControlNumber)),
                               CustomerId = ret.SalesOrder != null ? ret.SalesOrder.Customer.Id 
                                          : ret.Customer != null ? ret.Customer.Id 
                                          : null,
                               CustomerCode = ret.SalesOrder != null ? ret.SalesOrder.Customer.Code 
                                            : ret.Customer != null ? ret.Customer.Code 
                                            : null,
                               CustomerName = ret.SalesOrder != null ? ret.SalesOrder.Customer.Name 
                                            : ret.Customer != null ? ret.Customer.Name 
                                            : null,
                               CustomerAddress = ret.SalesOrder != null ? ret.SalesOrder
                                                                             .Customer
                                                                             .ShippingAddresses
                                                                             .Where(s => s.IsDefault == true)
                                                                             .Select(s => s.Address)
                                                                             .FirstOrDefault() 
                                               : ret.Customer != null ? ret.Customer
                                                                           .ShippingAddresses
                                                                           .Where(s => s.IsDefault == true)
                                                                           .Select(s => s.Address)
                                                                           .FirstOrDefault() 
                                               : null,
                               Id = ret.Id,
                               Remarks = ret.Remarks,
                               SOId = ret.SalesOrder == null ? null : ret.SalesOrder.Id,
                               SONumber = ret.SalesOrder == null ? null : ret.SalesOrder.SalesOrderNumber,
                               SRNumber = ret.SalesReturnNumber,
                               ReturnDateD = ret.ReturnDate,
                               UserCreated = SystemFormat.Name(userc.FirstName, userc.LastName, userc.Nickname),
                               DateCreatedD = ret.DateCreated,
                               UserModified = userm == null ? null : SystemFormat.Name(userm.FirstName, userm.LastName, userm.Nickname),
                               DateModifiedD = ret.DateModified == null ? null : ret.DateModified.Value
                           };
            return await returnVM.FirstOrDefaultAsync();
        }

        private async Task<IList<SalesReturnVM>> GetListAsync()
        {
            var returns = from ret in context.SalesReturns
                          join user in userManager.Users on ret.UserCreated equals user.Id
                          select new SalesReturnVM
                          {
                              BIRInvoiceNumber = string.Join(", ", ret.SalesReturnLineItems.Select(s => s.InvoiceLineItem == null ? "" : "<a href='/SalesInvoice/Details/" + s.InvoiceLineItem.Invoice.Id + "' class='link-info' target='_blank'>" + s.InvoiceLineItem.Invoice.ControlNumber + "</a>")),
                              CustomerId = ret.SalesOrder != null ? ret.SalesOrder.Customer.Id 
                                         : ret.Customer != null ? ret.Customer.Id 
                                         : null,
                              CustomerCode = ret.SalesOrder != null ? ret.SalesOrder.Customer.Code 
                                           : ret.Customer != null ? ret.Customer.Code 
                                           : null,
                              CustomerName = ret.SalesOrder != null ? ret.SalesOrder.Customer.Name 
                                           : ret.Customer != null ? ret.Customer.Name 
                                           : null,
                              Id = ret.Id,
                              Remarks = string.IsNullOrWhiteSpace(ret.Remarks) ? null : ret.Remarks,
                              ReturnDateD = ret.ReturnDate,
                              SONumber = ret.SalesOrder == null ? null : "<a href='/SalesOrder/Details/" + ret.SalesOrder.Id + "' class='link-info' target='_blank'>" + ret.SalesOrder.SalesOrderNumber + "</a>",
                              SRNumber = ret.SalesReturnNumber
                          };

            return await returns.ToListAsync();
        }

        public async Task<ForDataTables<SalesReturnVM>> GetListDTAsync(DataTablesRequestForm form)
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
                    s.BIRInvoiceNumber,
                    s.CustomerCode,
                    s.CustomerName,
                    s.Id.ToString(),
                    s.Remarks,
                    s.ReturnDate,
                    s.SRNumber
                }.Any(a => a != null && a.ToLower().Contains(form.SearchValue.ToLower())));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                bool isAsc = form.SortColumnDirection == "asc";
                data = form.SortColumn switch
                {
                    "ReturnDate" => isAsc ? data.OrderBy(s => s.ReturnDateD) : data.OrderByDescending(s => s.ReturnDateD),
                    _ => data.OrderBy(form.SortColumn + " " + form.SortColumnDirection)
                };
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<SalesReturnVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList
            };
        }

        public async Task<IList<APISalesReturn>> GetListAPI(DateTime? dt)
        {
            DateTime dtNow = DateTime.Now;
            DateTime dtYesterday = dtNow.AddDays(-1);
            var sreturns = from sreturn in context.SalesReturns
                           join userc in userManager.Users on sreturn.UserCreated equals userc.Id
                           join userm in userManager.Users on sreturn.UserModified equals userm.Id into um
                           from userm in um.DefaultIfEmpty()
                           where ((dt != null && sreturn.ReturnDate.Date == dt.Value.Date)
                           || (dt == null && (sreturn.BarcotechRequestDate == null
                           || sreturn.BarcotechRequestDate > dtYesterday)))
                           select new APISalesReturn
                           {
                               Id = sreturn.Id,
                               SalesOrderId = sreturn.SalesOrderId,
                               SalesOrderNumber = sreturn.SalesOrder == null ? null : sreturn.SalesOrder.SalesOrderNumber,
                               SalesReturnNumber = sreturn.SalesReturnNumber,
                               ReturnDate = sreturn.ReturnDate,
                               UserCreated = userc.FirstName + " " + userc.LastName,
                               DateCreated = sreturn.DateCreated,
                               LastUserModified = userm == null ? null : userm.FirstName + " " + userm.LastName,
                               LastDateModified = sreturn.DateModified,
                               LineItems = from item in sreturn.SalesReturnLineItems
                                           select new APISalesReturnLineItem
                                           {
                                               Id = item.Id,
                                               ItemId = item.ItemId,
                                               ItemCode = item.Item.Code,
                                               Quantity = item.Quantity,
                                               ReasonOfReturnId = item.ReasonOfReturnId
                                           }
                           };
            var resultSReturns = await sreturns.ToListAsync();

            foreach (var sreturn in resultSReturns)
            {
                var retrievedsreturn = context.SalesReturns.Where(s => s.Id == sreturn.Id).First();
                if (retrievedsreturn.BarcotechRequestDate == null)
                {
                    retrievedsreturn.BarcotechRequestDate = dtNow;
                    context.SaveChanges();
                }
            }

            return resultSReturns;
        }

        public async Task<IList<SalesReturnItemVM>> GetItemsAsync(int salesReturnId)
        {
            var itemVM = from rturn in context.SalesReturnLineItems
                         where rturn.SalesReturnId == salesReturnId
                         select new SalesReturnItemVM
                         {
                             SellingPriceD = rturn.SellingPrice,
                             Discount1D = rturn.Discount1,
                             Discount2D = rturn.Discount2,
                             Id = rturn.Id,
                             ReasonOfReturn = rturn.ReasonOfReturn.Name,
                             ItemId = rturn.Item.Id,
                             ItemCode = rturn.Item.Code,
                             ItemName = rturn.Item.Name + (rturn.ItemPromo == null ? "" : " (" + rturn.ItemPromo.Name + ")"),
                             Quantity = rturn.Quantity,
                             ReferenceNumber = rturn.InvoiceLineItemId == null ? "TBR" : (rturn.InvoiceLineItem!.Invoice.ControlNumber + (rturn.InvoiceLineItem!.Invoice.Status.Code.Trim().ToLower() == "draft" ? " (DRAFT)" : "")),
                             Unit = rturn.Item.Unit.Code
                         };
            return await itemVM.ToListAsync();
        }

        public async Task<string> GetNextFormattedNumber()
        {

            var lastSalesReturn = await context.SalesReturns
                                               .OrderByDescending(s => s.SalesReturnNumber)
                                               .Select(s => s.SalesReturnNumber)
                                               .FirstOrDefaultAsync() ?? "SR-0000000";
            var lastSRNumber = int.Parse(lastSalesReturn.Replace("SR-", ""));
            return "SR-" + string.Format("{0:0000000}", lastSRNumber + 1);
        }

        public async Task<ResponseModel<int>> SaveAsync(SalesReturnFormVM form)
        {
            ResponseModel<int> response = new();

            if (await context.SalesReturns.Where(s => s.Id != form.Id && s.SalesReturnNumber == form.ReturnNumber).AnyAsync())
            {
                response.Status = "Error";
                response.Message = "Return number is already in use";
            }
            else
            {
                var salesReturn = await context.SalesReturns.Where(s => s.Id == form.Id).FirstOrDefaultAsync();
                var salesOrder = await context.SalesOrders.Where(s => s.Id == form.SalesOrder).FirstOrDefaultAsync();

                if (salesReturn == null)
                {
                    salesReturn = new SalesReturn
                    {
                        SalesReturnNumber = form.ReturnNumber,
                        SalesOrderId = form.SalesOrder,
                        CustomerId =salesOrder != null && salesOrder.CustomerId == form.Customer ? null : form.Customer,
                        Remarks = form.Remarks + "",
                        ReturnDate = form.ReturnDate
                    };

                    context.SalesReturns.Add(salesReturn);
                    await context.SaveChangesAsync(currentUserId);

                    var warehouseId = await context.Warehouses
                                                   .Where(s => s.IsMain == true)
                                                   .Select(s => s.Id)
                                                   .FirstAsync();

                    foreach (var item in form.Items)
                    {
                        var salesReturnLineItem = new SalesReturnLineItem
                        {
                            Discount1 = item.Discount1,
                            Discount2 = item.Discount2,
                            InvoiceLineItemId = item.Invoice,
                            ReasonOfReturnId = item.ReasonOfReturn,
                            ItemId = item.Item,
                            Quantity = item.Quantity,
                            SalesReturnId = salesReturn.Id,
                            SellingPrice = item.SellingPrice,
                            ItemPromoId = item.ItemPromoId
                           
                        };

                        context.SalesReturnLineItems.Add(salesReturnLineItem);

                        var itemInventory = await context.ItemInventories
                                                         .Where(s => s.ItemId == item.Item && s.WarehouseId == warehouseId)
                                                         .FirstOrDefaultAsync();

                        if (itemInventory != null)
                        {
                            var oldInventoryQuantity = itemInventory.Quantity;
                            itemInventory.Quantity += item.Quantity;
                            context.ItemInventories.Update(itemInventory);
                            await context.SaveChangesAsync(currentUserId);

                            var countHistory = new PhysicalCountHistory
                            {
                                ItemId = item.Item,
                                QuantityFrom = oldInventoryQuantity,
                                QuantityTo = itemInventory.Quantity,
                                Remarks = "Sales Return",
                                WarehouseId = warehouseId
                            };
                            context.PhysicalCountHistories.Add(countHistory);
                            await context.SaveChangesAsync(currentUserId);
                        }
                        else
                        {
                            itemInventory = new ItemInventory
                            {
                                ItemId = item.Item,
                                Quantity = item.Quantity,
                                WarehouseId = warehouseId
                            };
                            await context.ItemInventories.AddAsync(itemInventory);
                            await context.SaveChangesAsync(currentUserId);

                            var countHistory = new PhysicalCountHistory
                            {
                                ItemId = item.Item,
                                QuantityFrom = 0,
                                QuantityTo = item.Quantity,
                                Remarks = "Sales Return",
                                WarehouseId = warehouseId
                            };
                            context.PhysicalCountHistories.Add(countHistory);
                            await context.SaveChangesAsync(currentUserId);
                        }
                    }
                    await context.SaveChangesAsync(currentUserId);

                    response.Status = "Success";
                    response.Message = "Sales Return successfully added";
                    response.NewRow = salesReturn.Id;
                }
                else
                {
                    salesReturn.SalesReturnNumber = form.ReturnNumber;
                    salesReturn.SalesOrderId = form.SalesOrder;
                    salesReturn.CustomerId = salesOrder != null && salesOrder.CustomerId == form.Customer ? null : form.Customer;
                    salesReturn.ReturnDate = form.ReturnDate;
                    salesReturn.Remarks = form.Remarks + "";

                    context.SalesReturns.Update(salesReturn);
                    await context.SaveChangesAsync(currentUserId);

                    response.Status = "Success";
                    response.Message = "Sales Return successfully updated";
                    response.NewRow = salesReturn.Id;
                }
            }

            return response;
        }

        public async Task<SalesReturnDetailPVM> PrintDetails(int returnId)
        {
            return new SalesReturnDetailPVM
            {
                Contents = await GetItemsAsync(returnId),
                Details = await GetDetailsAsync(returnId) ?? new SalesReturnVM()
            };
        }
    }
}