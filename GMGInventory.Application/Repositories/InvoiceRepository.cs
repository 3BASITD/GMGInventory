using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Policy;

namespace GMGInventory.Application.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly string currentUserId;

        public InvoiceRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
            , UserManager<User> userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
        }

        public async Task<Select2Model<JsonInvoiceOptionsModel>> GetOptions(string search, int page)
        {
            var invoices = from inv in context.Invoices
                           join soali in context.SOACRLineItems
                                      on inv.Id equals soali.InvoiceId
                           join soacr in context.SOACRs
                                      on soali.SOACRId equals soacr.Id into soacrs
                           from soacr in soacrs.DefaultIfEmpty()
                           let pay = context.CustomerPaymentDetails
                                            .Where(s => s.InvoiceId == inv.Id
                                                     && s.CustomerPayment.SOACRId == soali.SOACRId)
                                            .Select(s => s.PaidAmount)
                                            .Sum()
                           let tbr = context.SOACRLineItemTBRs.Where(s => s.SOACRLineItemId == soali.Id).Sum(s => s.Amount)
                           where (string.IsNullOrWhiteSpace(search)
                              || inv.InvoiceNumber.ToLower().Contains(search.ToLower()))
                           select new JsonInvoiceOptionsModel
                           {
                               Id = inv.Id.ToString(),
                               Customer = soacr.Customer.Name,
                               InvoiceNumber = inv.InvoiceNumber,
                               TotalAmountD = inv.InvoiceLineItems.Sum(s => s.Quantity * s.UnitPrice),
                               TBRD = tbr,
                               CreditD = soali.CreditAmount,
                               DebitD = soali.DebitAmount,
                               PaidAmountD = pay,
                               FreightChargeD = soali.FreightCost
                           };
            var invoicesFiltered = await invoices.Skip((page - 1) * 50).Take(50).ToListAsync();
            return new Select2Model<JsonInvoiceOptionsModel>
            {
                //incomplete_results = false,
                items = invoicesFiltered,
                total_count = invoices.Count()
            };
        }

        public async Task<Select2Model<JsonOptionsModel>> GetOptionsInvoiceNumber(string search, int page)
        {
            var invoices = from invoice in context.Invoices
                           where (string.IsNullOrWhiteSpace(search)
                              || invoice.InvoiceNumber.ToLower().Contains(search.ToLower()))
                           orderby invoice.InvoiceNumber ascending
                           select new JsonOptionsModel
                           {
                               Id = invoice.Id.ToString(),
                               Text = invoice.InvoiceNumber
                           };
            var invoicesFiltered = await invoices.Skip((page - 1) * 50).Take(50).ToListAsync();
            return new Select2Model<JsonOptionsModel>
            {
                //incomplete_results = false,
                items = invoicesFiltered,
                total_count = invoices.Count()
            };
        }

        public async Task<Select2Model<JsonOptionsModel>> GetOptionsBIRInvoiceNumber(string search, int page)
        {
            var invoices = from invoice in context.Invoices
                           where (string.IsNullOrWhiteSpace(search)
                              || invoice.ControlNumber.ToLower().Contains(search.ToLower()))
                           orderby invoice.ControlNumber ascending
                           select new JsonOptionsModel
                           {
                               Id = invoice.Id.ToString(),
                               Text = invoice.ControlNumber
                           };
            var invoicesFiltered = await invoices.Skip((page - 1) * 50).Take(50).ToListAsync();
            return new Select2Model<JsonOptionsModel>
            {
                //incomplete_results = false,
                items = invoicesFiltered,
                total_count = invoices.Count()
            };
        }

        public async Task<Select2Model<JsonNameOptionsModel>> GetInvoiceBySalesOrder(string search, int page, int? salesOrderId)
        {
            var invoices = from invoice in context.Invoices
                           where (salesOrderId == null
                              || invoice.SalesOrder.Id == salesOrderId)
                              && (string.IsNullOrWhiteSpace(search)
                              || invoice.InvoiceNumber.ToLower().Contains(search.ToLower())
                              || invoice.ControlNumber.ToLower().Contains(search.ToLower()))
                           orderby invoice.ControlNumber ascending
                           select new JsonNameOptionsModel
                           {
                               Id = invoice.Id.ToString(),
                               Text = invoice.InvoiceNumber,
                               Name = invoice.ControlNumber
                           };
            var invoicesFiltered = await invoices.Skip((page - 1) * 50).Take(50).ToListAsync();
            return new Select2Model<JsonNameOptionsModel>
            {
                //incomplete_results = false,
                items = invoicesFiltered,
                total_count = invoices.Count()
            };
        }

        public async Task<IList<JsonOptionsModel>> GetInvoiceTypeOptions(int? IncludeId)
        {
            return await context.InvoiceTypes
                                .Select(s => new JsonOptionsModel
                                {
                                    Id = s.Id.ToString(),
                                    Text = s.Name,
                                    Selected = IncludeId == s.Id
                                })
                                .OrderBy(s => s.Text)
                                .ToListAsync();
        }

        public async Task<InvoiceDetailsVM?> GetDetailsAsync(int? Id)
        {
            var invoiceVM = (from invoice in context.Invoices
                                                    .Include(s => s.SalesOrder).ThenInclude(s => s.Customer).ThenInclude(s => s.CreditTerm)
                                                    .Include(s => s.SalesOrder).ThenInclude(s => s.Agent)
                             join userc in userManager.Users on invoice.UserCreated equals userc.Id
                             join userm in userManager.Users on invoice.UserModified equals userm.Id into um
                             from userm in um.DefaultIfEmpty()
                             where invoice.Id == Id
                             select new InvoiceDetailsVM
                             {
                                 Agent = invoice.SalesOrder.Agent.FirstName
                                       + (invoice.SalesOrder.Agent.MiddleName == null ? "" : " " + invoice.SalesOrder.Agent.MiddleName)
                                       + " " + invoice.SalesOrder.Agent.LastName,
                                 ControlNumber = invoice.ControlNumber,
                                 CurrentUsersDraft = invoice.Status.Name.ToLower() == "invoice draft" && invoice.UserCreated == currentUserId,
                                 CustomerAddress = invoice.SalesOrder
                                                          .Customer
                                                          .ShippingAddresses
                                                          .Where(s => s.IsDefault == true)
                                                          .Select(s => s.Address)
                                                          .FirstOrDefault(),
                                 CustomerCode = invoice.SalesOrder.Customer.Code,
                                 CustomerName = invoice.SalesOrder.Customer.Name,
                                 Id = invoice.Id,
                                 InvoiceDateD = invoice.InvoiceDate,
                                 InvoiceNumber = invoice.InvoiceNumber,
                                 InvoiceType = invoice.InvoiceType.Name,
                                 InvoiceTypeId = invoice.InvoiceType.Id,
                                 PackNumber = invoice.PackId,
                                 PaymentTerms = invoice.SalesOrder.Customer.CreditTerm == null ? null : invoice.SalesOrder.Customer.CreditTerm.Name,
                                 PONumber = invoice.SalesOrder.PurchaseOrder,
                                 Remarks = invoice.Remarks,
                                 SONumber = invoice.SalesOrder.SalesOrderNumber,
                                 Status = invoice.Status.Code,
                                 TIN = invoice.SalesOrder.Customer.TIN,
                                 UserCreated = SystemFormat.Name(userc.FirstName, userc.LastName, userc.Nickname),
                                 DateCreatedD = invoice.DateCreated,
                                 UserModified = userm == null ? null : SystemFormat.Name(userm.FirstName, userm.LastName, userm.Nickname),
                                 DateModifiedD = invoice.DateModified
                             });
            return await invoiceVM.FirstOrDefaultAsync();
        }

        public async Task<InvoiceDetailsWithItemsVM> GetDetailsWithItemsAsync(int Id)
        {
            var invoiceVM = (from invoice in context.Invoices
                                                    .Include(s => s.SalesOrder).ThenInclude(s => s.Customer).ThenInclude(s => s.CreditTerm)
                                                    .Include(s => s.SalesOrder).ThenInclude(s => s.Agent)
                             join userc in userManager.Users on invoice.UserCreated equals userc.Id
                             join userm in userManager.Users on invoice.UserModified equals userm.Id into um
                             from userm in um.DefaultIfEmpty()
                             where invoice.Id == Id
                             select new InvoiceDetailsWithItemsVM
                             {
                                 Agent = invoice.SalesOrder.Agent.FirstName
                                       + (invoice.SalesOrder.Agent.MiddleName == null ? "" : " " + invoice.SalesOrder.Agent.MiddleName)
                                       + " " + invoice.SalesOrder.Agent.LastName,
                                 ControlNumber = invoice.ControlNumber,
                                 CustomerAddress = invoice.SalesOrder
                                                          .Customer
                                                          .ShippingAddresses
                                                          .Where(s => s.IsDefault == true)
                                                          .Select(s => s.Address)
                                                          .FirstOrDefault(),
                                 CustomerCode = invoice.SalesOrder.Customer.Code,
                                 CustomerName = invoice.SalesOrder.Customer.Name,
                                 Id = invoice.Id,
                                 InvoiceDateD = invoice.InvoiceDate,
                                 InvoiceNumber = invoice.InvoiceNumber,
                                 InvoiceType = invoice.InvoiceType.Name,
                                 InvoiceTypeId = invoice.InvoiceTypeId,
                                 PackNumber = invoice.PackId,
                                 PaymentTerms = invoice.SalesOrder.Customer.CreditTerm == null ? null : invoice.SalesOrder.Customer.CreditTerm.Name,
                                 PONumber = invoice.SalesOrder.PurchaseOrder,
                                 Remarks = invoice.Remarks,
                                 SONumber = invoice.SalesOrder.SalesOrderNumber,
                                 Status = invoice.Status.Code,
                                 TIN = invoice.SalesOrder.Customer.TIN,
                                 UserCreated = SystemFormat.Name(userc.FirstName, userc.LastName, userc.Nickname),
                                 DateCreatedD = invoice.DateCreated,
                                 UserModified = userm == null ? null : SystemFormat.Name(userm.FirstName, userm.LastName, userm.Nickname),
                                 DateModifiedD = invoice.DateModified
                             });
            var invoiceDetails = await invoiceVM.FirstAsync();
            var items = await GetItemsAsync(Id);
            invoiceDetails.Items = items;
            invoiceDetails.TotalD = items.Sum(s => s.GrossPriceNoDiscountD);
            invoiceDetails.DiscountedTotalD = items.Sum(s => s.GrossPriceD);
            invoiceDetails.DiscountPercentD = items.Count() > 0 && items.Sum(s => s.GrossPriceD + s.GrossPriceNoDiscountD) > 0
                                            ? (100 - (items.Sum(s => s.GrossPriceD) / items.Sum(s => s.GrossPriceNoDiscountD) * 100))
                                            : 0;
            invoiceDetails.TotalAmountD = items.Sum(s => s.GrossPriceNoDiscountD);
            return invoiceDetails;
        }

        public async Task<InvoiceDetailsVM?> GetDetailsByPackAsync(int? Id)
        {
            var packVM = (from pack in context.Packs
                                              .Include(s => s.SalesOrder).ThenInclude(s => s.Customer).ThenInclude(s => s.CreditTerm)
                                              .Include(s => s.SalesOrder).ThenInclude(s => s.Agent)
                          where pack.Id == Id
                          select new InvoiceDetailsVM
                          {
                              Agent = pack.SalesOrder.Agent.FirstName
                                    + (pack.SalesOrder.Agent.MiddleName == null ? "" : " " + pack.SalesOrder.Agent.MiddleName)
                                    + " " + pack.SalesOrder.Agent.LastName,
                              CustomerAddress = pack.SalesOrder
                                                    .Customer
                                                    .ShippingAddresses
                                                    .Where(s => s.IsDefault == true)
                                                    .Select(s => s.Address)
                                                    .FirstOrDefault(),
                              CustomerCode = pack.SalesOrder.Customer.Code,
                              CustomerName = pack.SalesOrder.Customer.Name,
                              Id = pack.Id,
                              PackNumber = pack.Id,
                              PaymentTerms = pack.SalesOrder.Customer.CreditTerm == null ? null : pack.SalesOrder.Customer.CreditTerm.Name,
                              PONumber = pack.SalesOrder.PurchaseOrder,
                              SONumber = pack.SalesOrder.SalesOrderNumber,
                              Status = pack.Status.Code,
                              TIN = pack.SalesOrder.Customer.TIN
                          });
            return await packVM.FirstOrDefaultAsync();
        }

        private async Task<IList<InvoiceVM>> GetListAsync(ListFilterCommonModel filter)
        {
            var invoices = from inv in context.Invoices
                           where (filter.Status == null || inv.StatusId == filter.Status)
                           && filter.DateFrom <= inv.InvoiceDate.Date
                           && inv.InvoiceDate.Date <= filter.DateTo
                           && (filter.StatusCode == null
                           || (filter.StatusCode.ToLower() == "draft" && inv.Status.Name.ToLower() == "invoice draft" && inv.UserCreated == currentUserId)
                           || (filter.StatusCode.ToLower() == "posted" && inv.Status.Name.ToLower() == "invoice submitted")
                           || (filter.StatusCode.ToLower() == "cancelled" && inv.Status.Name.ToLower() == "cancelled invoice"))
                           orderby inv.Id descending
                           select new InvoiceVM
                           {
                               BIRInvoiceNumber = inv.ControlNumber,
                               CurrentUsersDraft = inv.Status.Name.ToLower() == "invoice draft" && inv.UserCreated == currentUserId,
                               Customer = inv.SalesOrder.Customer.Name,
                               Id = inv.Id,
                               InvoiceDateD = inv.InvoiceDate,
                               InvoiceNumber = inv.InvoiceNumber,
                               InvoiceType = inv.InvoiceType.Code,
                               PackNumber = inv.PackId,
                               SONumber = inv.SalesOrder.SalesOrderNumber,
                               Status = inv.Status.Code,
                               TotalAmountD = (from invli in inv.InvoiceLineItems
                                               join order in context.SalesOrders on invli.Invoice.SalesOrderId equals order.Id
                                               join orderli in context.SalesOrderLineItems on order.Id equals orderli.SalesOrderId
                                               where invli.ItemId == orderli.ItemId
                                               select (invli.Quantity * invli.UnitPrice * (100 - orderli.Discount1) / 100) * (100 - orderli.Discount2) / 100).Sum(),

                           };

            return await invoices.ToListAsync();
        }

        public async Task<ForDataTables<InvoiceVM>> GetListDTAsync(DataTablesRequestForm form, ListFilterCommonModel filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetListAsync(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.BIRInvoiceNumber.ToLower().Contains(form.SearchValue)
                                    || x.Customer.ToLower().Contains(form.SearchValue)
                                    || x.Id.ToString().Contains(form.SearchValue)
                                    || x.InvoiceDate.ToLower().Contains(form.SearchValue)
                                    || x.InvoiceNumber.ToLower().Contains(form.SearchValue)
                                    || x.InvoiceType.ToLower().Contains(form.SearchValue)
                                    || x.PackNumber.ToString().Contains(form.SearchValue)
                                    || x.SONumber.ToLower().Contains(form.SearchValue)
                                    || x.Status.ToLower().Contains(form.SearchValue)
                                    || x.TotalAmountD.ToString().Contains(form.SearchValue));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                if (form.SortColumnDirection == "asc")
                {
                    switch (form.SortColumn)
                    {
                        case "BIRInvoiceNumber": data = data.OrderBy(s => s.BIRInvoiceNumber); break;
                        case "Customer": data = data.OrderBy(s => s.Customer); break;
                        case "Id": data = data.OrderBy(s => s.Id); break;
                        case "InvoiceDate": data = data.OrderBy(s => s.InvoiceDateD); break;
                        case "InvoiceNumber": data = data.OrderBy(s => s.InvoiceNumber); break;
                        case "InvoiceType": data = data.OrderBy(s => s.InvoiceType); break;
                        case "PackNumber": data = data.OrderBy(s => s.PackNumber); break;
                        case "SONumber": data = data.OrderBy(s => s.SONumber); break;
                        case "Status": data = data.OrderBy(s => s.Status); break;
                        case "TotalAmount": data = data.OrderBy(s => s.TotalAmountD); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "BIRInvoiceNumber": data = data.OrderByDescending(s => s.BIRInvoiceNumber); break;
                        case "Customer": data = data.OrderByDescending(s => s.Customer); break;
                        case "Id": data = data.OrderByDescending(s => s.Id); break;
                        case "InvoiceDate": data = data.OrderByDescending(s => s.InvoiceDateD); break;
                        case "InvoiceNumber": data = data.OrderByDescending(s => s.InvoiceNumber); break;
                        case "InvoiceType": data = data.OrderByDescending(s => s.InvoiceType); break;
                        case "PackNumber": data = data.OrderByDescending(s => s.PackNumber); break;
                        case "SONumber": data = data.OrderByDescending(s => s.SONumber); break;
                        case "Status": data = data.OrderByDescending(s => s.Status); break;
                        case "TotalAmount": data = data.OrderByDescending(s => s.TotalAmountD); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<InvoiceVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList
            };
        }

        public async Task<IList<InvoiceItemVM>> GetItemsAsync(int invoiceId)
        {
            var itemVM = from invoicel in context.InvoiceLineItems
                         join item in context.Items on invoicel.ItemId equals item.Id
                         join invoice in context.Invoices on invoicel.InvoiceId equals invoice.Id
                         join order in context.SalesOrders on invoice.SalesOrderId equals order.Id
                         join orderl in context.SalesOrderLineItems on order.Id equals orderl.SalesOrderId
                         let pack = context.PackLineItems.Where(s => s.PackId == invoice.PackId && s.ItemId == item.Id).Sum(s => (int?)s.Quantity) ?? 0
                         let price = context.ItemPrices.Where(s => s.ItemId == orderl.ItemId && s.DateCreated <= order.DateCreated).OrderByDescending(s => s.DateCreated).First()
                         where invoicel.InvoiceId == invoiceId
                         && orderl.ItemId == invoicel.ItemId
                         select new InvoiceItemVM
                         {
                             Discount1D = orderl.Discount1,
                             Discount2D = orderl.Discount2,
                             Id = invoicel.Id,
                             ItemId = item.Id,
                             ItemCode = item.Code,
                             ItemName = item.Name,
                             NetPriceD = invoicel.UnitPrice,
                             QuantityD = invoicel.Quantity,
                             QuantityOfPack = pack,
                             Unit = item.Unit.Code,
                             SellingPriceType = price.RetailSellingPrice > price.NetSellingPrice ? "Gross" : "Net",
                             ReferenceNumber = invoicel.Invoice.ControlNumber
                         };
            return await itemVM.ToListAsync();
        }

        public async Task<ForDataTables<InvoiceItemVM>> GetItemsDTAsync(DataTablesRequestForm form, int invoiceId)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetItemsAsync(invoiceId)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Discount1D.ToString().Contains(form.SearchValue)
                                    || x.Discount2D.ToString().Contains(form.SearchValue)
                                    || x.ItemCode.ToLower().Contains(form.SearchValue)
                                    || x.ItemName.ToLower().Contains(form.SearchValue)
                                    || x.NetPriceD.ToString().Contains(form.SearchValue)
                                    || x.Quantity.ToString().Contains(form.SearchValue));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                if (form.SortColumnDirection == "asc")
                {
                    switch (form.SortColumn)
                    {
                        case "Discount1": data = data.OrderBy(s => s.Discount1D); break;
                        case "Discount2": data = data.OrderBy(s => s.Discount2D); break;
                        case "GrossPrice": data = data.OrderBy(s => s.GrossPriceD); break;
                        case "ItemCode": data = data.OrderBy(s => s.ItemCode); break;
                        case "ItemName": data = data.OrderBy(s => s.ItemName); break;
                        case "NetPrice": data = data.OrderBy(s => s.NetPrice); break;
                        case "Quantity": data = data.OrderBy(s => s.Quantity); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Discount1": data = data.OrderByDescending(s => s.Discount1D); break;
                        case "Discount2": data = data.OrderByDescending(s => s.Discount2D); break;
                        case "GrossPrice": data = data.OrderByDescending(s => s.GrossPriceD); break;
                        case "ItemCode": data = data.OrderByDescending(s => s.ItemCode); break;
                        case "ItemName": data = data.OrderByDescending(s => s.ItemName); break;
                        case "NetPrice": data = data.OrderByDescending(s => s.NetPrice); break;
                        case "Quantity": data = data.OrderByDescending(s => s.Quantity); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<InvoiceItemVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public async Task<bool> IsBIRInvoiceNumberInUser(int? invoiceId, string BIRInvoiceNumber)
        {
            return await context.Invoices.AnyAsync(s => (invoiceId == null || s.Id != invoiceId) && s.ControlNumber == BIRInvoiceNumber);
        }

        public async Task<bool> IsInvoiceInUse(int? invoiceId)
        {
            var isInSalesReturn = await context.SalesReturnLineItems.AnyAsync(s => s.InvoiceLineItem!.InvoiceId == invoiceId);
            var isInSOACR = await context.SOACRLineItems.AnyAsync(s => s.InvoiceId == invoiceId);
            return isInSalesReturn || isInSOACR;
        }

        public async Task<int> SaveAsync(InvoiceFormVM form)
        {
            var packVM = await context.Packs.Include(s => s.SalesOrder).Where(s => s.Id == form.PackId).FirstOrDefaultAsync();
            if (packVM != null)
            {

                var invoiceType = await context.InvoiceTypes.Where(s => s.Id == form.InvoiceType).FirstAsync();

                //var lastInvoice = await context.Invoices
                //                               .Where(s => s.InvoiceTypeId == form.InvoiceType)
                //                               .OrderByDescending(s => s.InvoiceNumber)
                //                               .FirstAsync();
                //var lastInvoiceNumber = lastInvoice.InvoiceNumber.Replace(invoiceType.Code + "-", "");
                //var newInvoiceNumber = Int32.Parse(lastInvoiceNumber) + 1;

                var invoiceNumberPrefix = $"{invoiceType.Code}-";
                var lastInvoiceNumber = await context.Invoices
                                                .Where(s => s.InvoiceNumber.StartsWith(invoiceNumberPrefix))
                                                .OrderByDescending(s => s.InvoiceNumber)
                                                .Select(s => s.InvoiceNumber)
                                                .FirstOrDefaultAsync();

                int.TryParse(lastInvoiceNumber?.Replace(invoiceNumberPrefix, "") ?? "0", out int invoiceNumberLast);

                int statusId = await context.Statuses
                                            .Where(s => ((s.Name.ToLower() == "invoice draft" && form.SavingAs.ToLower() == "save")
                                                      || (s.Name.ToLower() == "invoice submitted" && form.SavingAs.ToLower() == "post")))
                                            .Select(s => s.Id)
                                            .FirstAsync();

                Invoice invoice = new()
                {
                    InvoiceDate = form.InvoiceDate,
                    ControlNumber = form.BIRInvoiceNumber,
                    InvoiceNumber = $"{invoiceNumberPrefix}{invoiceNumberLast + 1:D7}",
                    InvoiceTypeId = form.InvoiceType,
                    PackId = form.PackId,
                    Remarks = form.Remarks,
                    SalesOrderId = packVM.SalesOrderId,
                    StatusId = statusId
                };

                await context.Invoices.AddAsync(invoice);

                var packItems = await context.PackLineItems.Where(s => s.PackId == invoice.PackId).ToListAsync();

                //foreach (var packItem in packItems)
                //{
                //    var warehouseId = context.Warehouses.Where(x => x.Code.ToLower() == "main").Select(x => x.Id).FirstOrDefault();
                //    var itemInventory = await context.ItemInventories.Where(s => s.WarehouseId == warehouseId && s.ItemId == packItem.ItemId).FirstOrDefaultAsync();
                //    var previousCount = await context.PhysicalCountHistories.Where(s => s.WarehouseId == warehouseId && s.ItemId == packItem.ItemId).OrderByDescending(s => s.Id).FirstOrDefaultAsync();

                //    var countHistory = new PhysicalCountHistory
                //    {
                //        ItemId = packItem.ItemId,
                //        //QuantityFrom = previousCount != null ? previousCount.QuantityTo : itemInventory != null ? itemInventory.Quantity : 0,
                //        QuantityFrom = 0,
                //        //QuantityTo = previousCount != null ? previousCount.QuantityTo - packItem.Quantity : packItem.Quantity,
                //        QuantityTo = packItem.Quantity,
                //        Remarks = "Sales Invoice Posting",
                //        WarehouseId = warehouseId
                //    };
                //    context.PhysicalCountHistories.Add(countHistory);

                //    if (itemInventory != null)
                //    {
                //        itemInventory.Quantity -= packItem.Quantity;
                //        context.ItemInventories.Update(itemInventory);
                //    }
                //    else
                //    {
                //        var newItemInventory = new ItemInventory
                //        {
                //            ItemId = packItem.ItemId,
                //            Quantity = packItem.Quantity,
                //            WarehouseId = warehouseId
                //        };
                //        await context.ItemInventories.AddAsync(newItemInventory);
                //    }
                //}

                await context.SaveChangesAsync(currentUserId);

                await AddInvoiceItems(invoice.Id, packVM.SalesOrderId, form);

                return invoice.Id;
            }
            return -1;
        }

        private async Task AddInvoiceItems(int invoiceId, int salesOrderId, InvoiceFormVM form)
        {
            foreach (var item in form.Items)
            {
                var soItem = await context.SalesOrderLineItems
                                          .Where(s => s.SalesOrderId == salesOrderId && s.ItemId == item.Item)
                                          .FirstAsync();
                InvoiceLineItem lineItem = new()
                {
                    InvoiceId = invoiceId,
                    ItemId = item.Item,
                    Quantity = item.Quantity,
                    UnitPrice = soItem.SellingPrice
                };
                await context.InvoiceLineItems.AddAsync(lineItem);
                await context.SaveChangesAsync(currentUserId);
            }
        }

        private async Task DeleteInvoiceItems(InvoiceFormVM form, bool includeDeleteRecord)
        {
            var invoice = await context.Invoices.Where(s => s.Id == form.Id).FirstAsync();
            var invoiceLineItems = await context.InvoiceLineItems.Where(s => s.InvoiceId == form.Id).ToListAsync();
            foreach (var item in invoiceLineItems)
            {
                var soItem = await context.SalesOrderLineItems
                                          .Where(s => s.SalesOrderId == invoice.SalesOrderId && s.ItemId == item.ItemId)
                                          .FirstAsync();

                context.SalesOrderLineItems.Update(soItem);
                if (includeDeleteRecord)
                {
                    context.InvoiceLineItems.Remove(item);
                }

                await context.SaveChangesAsync(currentUserId);
            }

            int packId = await context.Statuses
                                          .Where(s => s.Name.ToLower() == "packing submitted")
                                          .Select(s => s.Id)
                                          .FirstAsync();

            var pack = await context.Packs.Where(s => s.Id == invoice.PackId).FirstAsync();
            pack.StatusId = packId;
            context.Packs.Update(pack);
            await context.SaveChangesAsync(currentUserId);

            var salesOrder = await context.SalesOrders.Where(s => s.Id == invoice.SalesOrderId).FirstAsync();
            salesOrder.StatusId = packId;
            context.SalesOrders.Update(salesOrder);
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task<int> UpdateAsync(InvoiceFormVM form)
        {
            var invoice = await context.Invoices.Include(s => s.Status).Where(s => s.Id == form.Id).FirstOrDefaultAsync();
            if (invoice == null)
            {
                return -1;
            }
            else
            {
                form.PackId = invoice.PackId;
                if (form.SavingAs == "delete")
                {
                    if (invoice.Status.Name.ToLower() != "invoice draft")
                    {
                        await DeleteInvoiceItems(form, false);
                        int statusId = await context.Statuses
                                                    .Where(s => s.Name.ToLower() == "cancelled invoice")
                                                    .Select(s => s.Id)
                                                    .FirstAsync();
                        invoice.StatusId = statusId;
                        context.Invoices.Update(invoice);
                        await context.SaveChangesAsync(currentUserId);
                        return -2;
                    }
                    else
                    {
                        await DeleteInvoiceItems(form, true);
                        context.Invoices.Remove(invoice);
                        await context.SaveChangesAsync(currentUserId);
                        return -3;
                    }
                }
                else
                {
                    await DeleteInvoiceItems(form, true);
                    if (await context.Agents.AnyAsync(s => s.Id == form.Agent))
                    {
                        var so = await context.SalesOrders.Where(s => s.Id == invoice.SalesOrderId).FirstOrDefaultAsync();
                        if (so != null)
                        {
                            so.AgentId = form.Agent;
                            context.SalesOrders.Update(so);
                        }
                    }
                    if (form.SavingAs != "update")
                    {
                        int statusId = await context.Statuses
                                                .Where(s => ((s.Name.ToLower() == "invoice draft" && form.SavingAs.ToLower() == "save")
                                                          || (s.Name.ToLower() == "invoice submitted" && form.SavingAs.ToLower() == "post")))
                                                .Select(s => s.Id)
                                                .FirstAsync();
                        invoice.StatusId = statusId;
                    }
                    invoice.ControlNumber = form.BIRInvoiceNumber;
                    invoice.InvoiceTypeId = form.InvoiceType;
                    invoice.InvoiceDate = form.InvoiceDate;
                    invoice.Remarks = form.Remarks;
                    context.Invoices.Update(invoice);
                    await context.SaveChangesAsync(currentUserId);

                    await AddInvoiceItems(invoice.Id, invoice.SalesOrderId, form);
                    return form.SavingAs == "post" ? -2 : invoice.Id;
                }
            }
        }
    }
}