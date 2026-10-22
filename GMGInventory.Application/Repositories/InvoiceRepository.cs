using DocumentFormat.OpenXml.Spreadsheet;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Dynamic.Core;

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

        public async Task<Select2Model<JsonNameStatusOptionsModel>> GetInvoiceBySalesOrder(string search, int page, int? salesOrderId)
        {
            var invoices = from invoice in context.Invoices
                           where (salesOrderId == null
                              || invoice.SalesOrder.Id == salesOrderId)
                              && (string.IsNullOrWhiteSpace(search)
                              || invoice.InvoiceNumber.ToLower().Contains(search.ToLower())
                              || invoice.ControlNumber.ToLower().Contains(search.ToLower()))
                              && (invoice.Status.Code.Trim().ToLower() == "invoiced" || invoice.Status.Code.Trim().ToLower() == "draft")
                           orderby invoice.ControlNumber ascending
                           select new JsonNameStatusOptionsModel
                           {
                               Id = invoice.Id.ToString(),
                               Text = invoice.InvoiceNumber,
                               Name = invoice.ControlNumber,
                               Status = invoice.Status.Code
                           };
            var invoicesFiltered = await invoices.Skip((page - 1) * 50).Take(50).ToListAsync();
            return new Select2Model<JsonNameStatusOptionsModel>
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
                                 //Agent = invoice.SalesOrder.Agent.FirstName
                                 //      + (invoice.SalesOrder.Agent.MiddleName == null ? "" : " " + invoice.SalesOrder.Agent.MiddleName)
                                 //      + " " + invoice.SalesOrder.Agent.LastName,
                                 Agent = invoice.SalesOrder.Agent.Code,
                                 AgentId = invoice.SalesOrder.Agent.Id,
                                 Branch = invoice.SalesOrder.Customer.Branch,
                                 ControlNumber = invoice.ControlNumber,
                                 CustomerAddress = invoice.SalesOrder
                                                          .Customer
                                                          .ShippingAddresses
                                                          .Where(s => s.IsDefault == true)
                                                          .Select(s => s.Address)
                                                          .FirstOrDefault(),
                                 CustomerCode = invoice.SalesOrder.Customer.Code,
                                 CustomerId = invoice.SalesOrder.Customer.Id,
                                 CustomerName = invoice.SalesOrder.Customer.Name,
                                 Id = invoice.Id,
                                 InvoiceDateD = invoice.InvoiceDate,
                                 InvoiceDueDateD = invoice.DueDate,
                                 InvoiceNumber = invoice.InvoiceNumber,
                                 InvoiceType = invoice.InvoiceType.Name,
                                 InvoiceTypeId = invoice.InvoiceType.Id,
                                 PackNumber = invoice.PackId,
                                 PaymentTerms = invoice.SalesOrder.Customer.CreditTerm == null ? null : invoice.SalesOrder.Customer.CreditTerm.Name,
                                 PONumber = invoice.SalesOrder.PurchaseOrder,
                                 PriorityD = invoice.SalesOrder.Customer.Priority,
                                 Remarks = invoice.Remarks,
                                 SONumber = invoice.SalesOrder.SalesOrderNumber,
                                 SalesOrderId = invoice.SalesOrder.Id,
                                 Status = invoice.Status.Code,
                                 TIN = invoice.SalesOrder.Customer.TIN,
                                 UserCreated = SystemFormat.Name(userc.FirstName, userc.LastName, userc.Nickname),
                                 DateCreatedD = invoice.DateCreated,
                                 UserModified = userm == null ? null : SystemFormat.Name(userm.FirstName, userm.LastName, userm.Nickname),
                                 DateModifiedD = invoice.DateModified,
                                 HashMicroMoveId = invoice.HashMicroMoveId,
                                 HashMicroMoveName = invoice.HashMicroMoveName,
                                 HashMicroMoveStatus = invoice.HashMicroMoveStatus,
                                 HashMicroMoveBy = invoice.HashMicroMoveBy == null ? null : invoice.HashMicroMoveBy.FirstName + " " + invoice.HashMicroMoveBy.LastName,
                                 HashMicroMoveDateD = invoice.HashMicroMoveDate
                             });
            return await invoiceVM.FirstOrDefaultAsync();
        }

        public async Task<InvoiceDetailsWithItemsVM> GetDetailsWithItemsAsync(int Id)
        {
            var invoiceVM = (from invoice in context.Invoices
                                                    .Include(s => s.SalesOrder).ThenInclude(s => s.Customer).ThenInclude(s => s.CreditTerm)
                                                    .Include(s => s.SalesOrder).ThenInclude(s => s.Agent)
                             join soli in context.SalesOrderLineItems on invoice.SalesOrderId equals soli.SalesOrderId
                             join item in context.Items on soli.ItemId equals item.Id
                             join userc in userManager.Users on invoice.UserCreated equals userc.Id
                             join userm in userManager.Users on invoice.UserModified equals userm.Id into um
                             from userm in um.DefaultIfEmpty()
                             where invoice.Id == Id
                             select new InvoiceDetailsWithItemsVM
                             {
                                 //Agent = invoice.SalesOrder.Agent.FirstName
                                 //      + (invoice.SalesOrder.Agent.MiddleName == null ? "" : " " + invoice.SalesOrder.Agent.MiddleName)
                                 //      + " " + invoice.SalesOrder.Agent.LastName,
                                 Agent = invoice.SalesOrder.Agent.Code,
                                 AgentId = invoice.SalesOrder.Agent.Id,
                                 ControlNumber = invoice.ControlNumber,
                                 CustomerAddress = invoice.SalesOrder
                                                          .Customer
                                                          .ShippingAddresses
                                                          .Where(s => s.IsDefault == true)
                                                          .Select(s => s.Address)
                                                          .FirstOrDefault(),
                                 CustomerCode = invoice.SalesOrder.Customer.Code,
                                 CustomerId = invoice.SalesOrder.Customer.Id,
                                 CustomerName = invoice.SalesOrder.Customer.Name,
                                 Id = invoice.Id,
                                 InvoiceDateD = invoice.InvoiceDate,
                                 InvoiceDueDateD = invoice.DueDate,
                                 InvoiceNumber = invoice.InvoiceNumber,
                                 InvoiceType = invoice.InvoiceType.Name,
                                 InvoiceTypeId = invoice.InvoiceTypeId,
                                 PackNumber = invoice.PackId,
                                 PaymentTerms = invoice.SalesOrder.Customer.CreditTerm == null ? null : invoice.SalesOrder.Customer.CreditTerm.Name,
                                 PONumber = invoice.SalesOrder.PurchaseOrder,
                                 PriorityD = invoice.SalesOrder.Customer.Priority,
                                 Remarks = invoice.Remarks,
                                 SONumber = invoice.SalesOrder.SalesOrderNumber,
                                 SalesOrderId = invoice.SalesOrder.Id,
                                 Status = invoice.Status.Code,
                                 TIN = invoice.SalesOrder.Customer.TIN,
                                 UserCreated = SystemFormat.Name(userc.FirstName, userc.LastName, userc.Nickname),
                                 DateCreatedD = invoice.DateCreated,
                                 UserModified = userm == null ? null : SystemFormat.Name(userm.FirstName, userm.LastName, userm.Nickname),
                                 DateModifiedD = invoice.DateModified,
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
            invoiceDetails.TotalAmoutDueD = invoiceDetails.DiscountAmountD == 0 ? invoiceDetails.TotalAmountD : invoiceDetails.TotalAmountD - invoiceDetails.DiscountAmountD;

            invoiceDetails.AmountNetVatD = invoiceDetails.TotalAmoutDueD / 1.12m;
            invoiceDetails.WithHoldingTaxD = invoiceDetails.AmountNetVatD * 0.01m;
            invoiceDetails.LessVatD = invoiceDetails.AmountNetVatD * 0.12m;
            invoiceDetails.AddVatD = Math.Round(invoiceDetails.AmountNetVatD * 0.12m, 2, MidpointRounding.ToZero);

           

            

            //if (invoiceDetails.DiscountAmountD == 0)
            //{
            //    invoiceDetails.TotalAmoutDueD = invoiceDetails.TotalAmountD - invoiceDetails.WithHoldingTaxD;
            //}
            //else
            //{
            //    invoiceDetails.TotalAmoutDueD = invoiceDetails.TotalAmountD - invoiceDetails.DiscountAmountD;
            //}

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
                              //Agent = pack.SalesOrder.Agent.FirstName
                              //      + (pack.SalesOrder.Agent.MiddleName == null ? "" : " " + pack.SalesOrder.Agent.MiddleName)
                              //      + " " + pack.SalesOrder.Agent.LastName,
                              Agent = pack.SalesOrder.Agent.Code,
                              AgentId = pack.SalesOrder.Agent.Id,
                              CustomerAddress = pack.SalesOrder
                                                    .Customer
                                                    .ShippingAddresses
                                                    .Where(s => s.IsDefault == true)
                                                    .Select(s => s.Address)
                                                    .FirstOrDefault(),
                              CustomerCode = pack.SalesOrder.Customer.Code,
                              CustomerId = pack.SalesOrder.Customer.Id,
                              CustomerName = pack.SalesOrder.Customer.Name,
                              Id = pack.Id,
                              PackNumber = pack.Id,
                              PaymentTerms = pack.SalesOrder.Customer.CreditTerm == null ? null : pack.SalesOrder.Customer.CreditTerm.Name,
                              PONumber = pack.SalesOrder.PurchaseOrder,
                              PriorityD = pack.SalesOrder.Customer.Priority,
                              SONumber = pack.SalesOrder.SalesOrderNumber,
                              SalesOrderId = pack.SalesOrder.Id,
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
                           || (filter.StatusCode.Trim().ToLower() == "draft" && inv.Status.Name.ToLower() == "invoice draft" && inv.UserCreated == currentUserId)
                           || (filter.StatusCode.Trim().ToLower() == "posted" && inv.Status.Name.ToLower() == "invoice submitted")
                           || (filter.StatusCode.Trim().ToLower() == "cancelled" && inv.Status.Name.ToLower() == "cancelled invoice"))
                           orderby inv.Id descending
                           select new InvoiceVM
                           {
                               BIRInvoiceNumber = inv.ControlNumber,
                               Customer = inv.SalesOrder.Customer.Name,
                               CustomerId = inv.SalesOrder.Customer.Id,
                               Id = inv.Id,
                               InvoiceDateD = inv.InvoiceDate,
                               InvoiceDueDateD = inv.DueDate == null ? inv.InvoiceDate : inv.DueDate.Value,
                               InvoiceNumber = inv.InvoiceNumber,
                               InvoiceType = inv.InvoiceType.Code,
                               PackNumber = inv.PackId,
                               PriorityD = inv.SalesOrder.Customer.Priority,
                               SONumber = inv.SalesOrder.SalesOrderNumber,
                               SalesOrderId = inv.SalesOrder.Id,
                               Status = inv.Status.Code,
                               TotalAmountD = (from invli in inv.InvoiceLineItems
                                               join order in context.SalesOrders on invli.Invoice.SalesOrderId equals order.Id
                                               join orderli in context.SalesOrderLineItems on order.Id equals orderli.SalesOrderId
                                               let discount1 = (100 - orderli.Discount1) / 100
                                               let discount2 = (100 - orderli.Discount2) / 100
                                               where invli.ItemId == orderli.ItemId && invli.IsPromo == orderli.IsPromo
                                               select invli.Quantity * invli.UnitPrice * discount1 * discount2).Sum(),
                               HashMicroMoveId = inv.HashMicroMoveId,
                               HashMicroMoveName = inv.HashMicroMoveName,
                               HashMicroMoveStatus = inv.HashMicroMoveStatus,
                               HashMicroMoveBy = inv.HashMicroMoveBy == null ? null : inv.HashMicroMoveBy.FirstName + " " + inv.HashMicroMoveBy.LastName,
                               HashMicroMoveDateD = inv.HashMicroMoveDate
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
                data = data.Where(s => new[]
                {
                    s.BIRInvoiceNumber,
                    s.Customer,
                    s.Id + "",
                    s.InvoiceDate,
                    s.InvoiceNumber,
                    s.InvoiceType,
                    s.PackNumber + "",
                    s.Priority,
                    s.SONumber,
                    s.Status,
                    s.TotalAmount
                }.Any(a => a != null && a.ToLower().Contains(form.SearchValue.ToLower())));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                var direction = form.SortColumnDirection.ToLower();
                var sortExpr = $"{form.SortColumn} {direction}";
                IOrderedQueryable<InvoiceVM> orderedData = null;

                if (form.SortColumn.ToLower().Contains("date") || form.SortColumn == "Priority" || form.SortColumn == "TotalAmount")
                {
                    orderedData = direction == "asc" ? form.SortColumn switch
                    {
                        "InvoiceDate" => data.OrderBy(s => s.InvoiceDateD),
                        "Priority" => data.OrderBy(s => s.PriorityD),
                        "TotalAmount" => data.OrderBy(s => s.TotalAmountD),
                        _ => data.OrderBy(sortExpr)
                    }
                    : form.SortColumn switch
                    {
                        "InvoiceDate" => data.OrderByDescending(s => s.InvoiceDateD),
                        "Priority" => data.OrderByDescending(s => s.PriorityD),
                        "TotalAmount" => data.OrderByDescending(s => s.TotalAmountD),
                        _ => data.OrderBy(sortExpr)
                    };

                    data = orderedData.ThenBy($"Priority {direction}")
                                      .ThenBy($"InvoiceNumber {direction}");
                }
                else
                {
                    data = data.OrderBy($"{sortExpr}, Priority {direction}, InvoiceNumber {direction}");
                }
            }
            var totalInvoicedRecord = await context.Invoices
                                                   .Where(s => (s.Status.Code.Trim().ToLower() == "invoiced" || s.Status.Code.Trim().ToLower() == "draft")
                                                            && filter.DateFrom <= s.InvoiceDate.Date
                                                            && s.InvoiceDate.Date <= filter.DateTo)
                                                   .CountAsync();
            var totalInvoicedNotMovedRecord = await context.Invoices
                                                           .Where(s => (s.Status.Code.Trim().ToLower() == "invoiced" || s.Status.Code.Trim().ToLower() == "draft")
                                                                    && filter.DateFrom <= s.InvoiceDate.Date
                                                                    && s.InvoiceDate.Date <= filter.DateTo
                                                                    && s.HashMicroMoveById == null)
                                                           .CountAsync();
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<InvoiceVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList,
                TotalQuantity = totalInvoicedRecord,
                TotalQuantity2 = totalInvoicedNotMovedRecord
            };
        }

        public async Task<IList<InvoiceItemVM>> GetItemsAsync(int invoiceId)
        {
            var itemVM = from invoicel in context.InvoiceLineItems
                         join item in context.Items on invoicel.ItemId equals item.Id
                         join invoice in context.Invoices on invoicel.InvoiceId equals invoice.Id
                         join order in context.SalesOrders on invoice.SalesOrderId equals order.Id
                         let orderl = context.SalesOrderLineItems
                             .Where(s => s.SalesOrderId == order.Id
                                      && s.ItemId == invoicel.ItemId
                                      && s.IsPromo == invoicel.IsPromo)
                             .OrderBy(s => s.Id)
                             .FirstOrDefault()
                         let pack = context.PackLineItems
                             .Where(s => s.PackId == invoice.PackId && s.ItemId == item.Id)
                             .Sum(s => (int?)s.Quantity) ?? 0
                         let price = context.ItemPrices
                             .Where(s => s.ItemId == invoicel.ItemId && s.DateCreated <= order.DateCreated)
                             .OrderByDescending(s => s.DateCreated)
                             .FirstOrDefault()
                         where invoicel.InvoiceId == invoiceId && orderl != null
                         select new InvoiceItemVM
                         {
                             Discount1D = orderl.Discount1,
                             Discount2D = orderl.Discount2,
                             Id = invoicel.Id,
                             ItemId = item.Id,
                             ItemCode = item.Code,
                             ItemName = item.Name + (orderl.ItemPromo == null ? "" : " (" + orderl.ItemPromo.Name + ")"),
                             OldItemCode = item.OldCode == null || item.OldCode == "" ? "-" : item.OldCode + "",
                             PartNumber = item.PartNumber == null || item.PartNumber == "" ? "-" : item.PartNumber + "",
                             BrandName = item.Brand == null || item.Brand.Name == null || item.Brand.Name == "" ? "-" : item.Brand.Name + "",
                             NetPriceD = invoicel.UnitPrice,
                             QuantityD = invoicel.Quantity,
                             QuantityOfPack = pack,
                             Unit = item.Unit.Code,
                             SellingPriceType = price != null && price.RetailSellingPrice > price.NetSellingPrice ? "Gross" : "Net",
                             ReferenceNumber = invoicel.Invoice.ControlNumber,
                             ItemPromoId = orderl.ItemPromoId,
                             IsPromo = invoicel.IsPromo,
                             Model = item.ModelNumber,
                             Size = item.Size,

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
                                    || x.ItemCode.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.ItemName.ToLower().Contains(form.SearchValue.ToLower())
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
                //    var warehouseId = context.Warehouses.Where(x => x.Code.Trim().ToLower() == "main").Select(x => x.Id).FirstOrDefault();
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
                                          .Where(s => s.SalesOrderId == salesOrderId && s.ItemId == item.Item && s.IsPromo == item.isPromo)
                                          .FirstAsync();
                InvoiceLineItem lineItem = new()
                {
                    InvoiceId = invoiceId,
                    ItemId = item.Item,
                    Quantity = item.Quantity,
                    UnitPrice = soItem.SellingPrice,
                    IsPromo = item.isPromo

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

        public async Task<ResponseModel<int>> UpdateAsync(InvoiceFormVM form)
        {
            var invoice = await context.Invoices.Include(s => s.Status).Where(s => s.Id == form.Id).FirstOrDefaultAsync();
            if (invoice == null)
            {
                return new ResponseModel<int> { Status = "Error", Message = "Unknown invoice" };
            }
            else
            {
                form.PackId = invoice.PackId;
                if (form.SavingAs == "delete")
                {
                    if (invoice.Status.Name.ToLower() != "invoice draft")
                    {
                        return new ResponseModel<int> { Status = "Error", Message = "Cannot delete " + invoice.Status.Name };
                    }
                    else
                    {
                        await DeleteInvoiceItems(form, true);
                        context.Invoices.Remove(invoice);
                        await context.SaveChangesAsync(currentUserId);
                        return new ResponseModel<int> { Status = "Success", Message = "Successfully delete invoice." };
                    }
                }
                else if (form.SavingAs == "cancel")
                {
                    await DeleteInvoiceItems(form, false);
                    int statusId = await context.Statuses
                                                .Where(s => s.Name.ToLower() == "cancelled invoice")
                                                .Select(s => s.Id)
                                                .FirstAsync();
                    invoice.StatusId = statusId;
                    context.Invoices.Update(invoice);
                    await context.SaveChangesAsync(currentUserId);
                    return new ResponseModel<int> { Status = "Success", Message = "Successfully cancelled invoice." };
                }
                else
                {
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
                    invoice.DueDate = form.DueDate;
                    invoice.Remarks = form.Remarks;
                    context.Invoices.Update(invoice);
                    await context.SaveChangesAsync(currentUserId);

                    //await AddInvoiceItems(invoice.Id, invoice.SalesOrderId, form);
                    //var soitems = context.SalesOrderLineItems.Where(s => s.Id == invoice.SalesOrderId);
                    //var citems = await context.InvoiceLineItems
                    //                          .Where(s => s.InvoiceId == invoice.Id)
                    //                          .ToListAsync();

                    var soLines = await context.SalesOrderLineItems
                        .Where(s => s.SalesOrderId == invoice.SalesOrderId)
                        .ToListAsync();

                    var dbItems = await context.InvoiceLineItems
                        .Where(s => s.InvoiceId == invoice.Id)
                        .ToListAsync();

                    // Add or update
                    foreach (var fi in form.Items)
                    {
                        var dbItem = dbItems.FirstOrDefault(s => s.ItemId == fi.Item && s.IsPromo == fi.isPromo);

                        if (dbItem != null)
                        {
                            dbItem.Quantity = fi.Quantity;   // tracked, no Update() needed
                        }
                        else
                        {
                            var soLine = soLines.FirstOrDefault(s => s.ItemId == fi.Item && s.IsPromo == fi.isPromo);
                            if (soLine == null)
                            {
                                return new ResponseModel<int>
                                {
                                    Status = "Error",
                                    Message = "Item not found on the sales order."
                                };
                            }

                            context.InvoiceLineItems.Add(new InvoiceLineItem
                            {
                                InvoiceId = invoice.Id,
                                ItemId = fi.Item,
                                Quantity = fi.Quantity,
                                UnitPrice = soLine.SellingPrice,
                                IsPromo = fi.isPromo
                            });
                        }
                    }

                    // add item
                    // check items not in db
                    //var items = citems.Select(s => s.ItemId);
                    //// add excluding items already available
                    //var newitems = from i in form.Items
                    //               join s in context.SalesOrderLineItems on i.Item equals s.ItemId
                    //               where items.Contains(i.Item) == false
                    //               && s.SalesOrderId == invoice.SalesOrderId
                    //               select new InvoiceLineItem
                    //               {
                    //                   InvoiceId = invoice.Id,
                    //                   ItemId = i.Item,
                    //                   Quantity = i.Quantity,
                    //                   UnitPrice = s.SellingPrice
                    //               };
                    //context.InvoiceLineItems.AddRange(newitems);

                    //// update item
                    //// get items available in db
                    //var existitems = form.Items.Where(s => items.Contains(s.Item));
                    //foreach (var ei in existitems)
                    //{
                    //    // update all available in db
                    //    var dbitem = citems
                    //        .Where(s => s.ItemId == ei.Item && s.IsPromo == ei.isPromo)
                    //        .FirstOrDefault();
                    //    if (dbitem == null)
                    //    {
                    //        dbitem = new InvoiceLineItem
                    //        {
                    //            IsPromo = ei.isPromo,
                    //            ItemId = ei.Item,
                    //            Quantity = ei.Quantity
                    //        };
                    //        context.InvoiceLineItems.Add(dbitem);
                    //    }
                    //    else
                    //    {
                    //        dbitem.Quantity = ei.Quantity;
                    //        context.InvoiceLineItems.Update(dbitem);
                    //    }
                    //}

                    // delete item
                    // get items not in form
                    //var fitems = form.Items.Select(s => s.Item);
                    //var ritems = citems.Where(s => fitems.Contains(s.ItemId) == false);
                    //// remove unavailable items
                    //context.InvoiceLineItems.RemoveRange(ritems);

                    //await context.SaveChangesAsync(currentUserId);

                    //return new ResponseModel<int>
                    //{
                    //    Status = "Success",
                    //    Message = "Sales Invoice successfully " + (form.SavingAs == "post" ? "posted"
                    //                                             : form.SavingAs == "update" ? "updated"
                    //                                             : "saved"),
                    //    NewRow = invoice.Id
                    //};

                    var toRemove = dbItems
                          .Where(d => !form.Items.Any(f => f.Item == d.ItemId && f.isPromo == d.IsPromo))
                          .ToList();
                    context.InvoiceLineItems.RemoveRange(toRemove);

                    // One save for header + items
                    await context.SaveChangesAsync(currentUserId);

                    return new ResponseModel<int>
                    {
                        Status = "Success",
                        Message = "Sales Invoice successfully " + (form.SavingAs == "post" ? "posted"
                                                                 : form.SavingAs == "update" ? "updated"
                                                                 : "saved"),
                        NewRow = invoice.Id
                    };
                }
            }
        }

        public async Task<ResponseModel> CancelAsync(int? Id)
        {
            try
            {
                var invoice = await context.Invoices
                                           .Include(s => s.Status)
                                           .Where(s => s.Id == Id)
                                           .FirstOrDefaultAsync();

                if (invoice == null)
                    return new ResponseModel { Status = "Error", Message = "Invalid Record, Invoice cannot be found" };

                if (invoice.Status.Code.Trim().ToLower() != "invoiced")
                    return new ResponseModel { Status = "Error", Message = "Only invoiced record can be cancelled" };

                var usedInSOACR = await context.SOACRLineItems.AnyAsync(s => s.InvoiceId == Id);
                if (usedInSOACR)
                    return new ResponseModel { Status = "Error", Message = "Invoice already used in SOA/CR. Cannot cancel" };
                var usedInCustomerPayment = await context.CustomerPaymentDetails.AnyAsync(s => s.InvoiceId == Id);
                if (usedInCustomerPayment)
                    return new ResponseModel { Status = "Error", Message = "Invoice already used in Payment. Cannot cancel" };

                var draftStatus = await context.Statuses
                                               .Where(s => s.Name.Trim().ToLower() == "invoice draft")
                                               .Select(s => s.Id)
                                               .FirstAsync();

                invoice.StatusId = draftStatus;

                context.Invoices.Update(invoice);
                await context.SaveChangesAsync(currentUserId);

                return new ResponseModel { Status = "Success", Message = "Invoice successfully cancelled" };
            }
            catch (Exception ex)
            {
                return new ResponseModel { Status = "Error", Message = SystemFormat.GetInnermostExceptionMessage(ex) };
            }
        }

        public async Task<ResponseModel> DeleteAsync(int? Id)
        {
            try
            {
                var invoice = await context.Invoices
                                           .Include(s => s.Status)
                                           .Where(s => s.Id == Id)
                                           .FirstOrDefaultAsync();

                if (invoice == null)
                    return new ResponseModel { Status = "Error", Message = "Invalid Record, Invoice cannot be found" };

                if (invoice.Status.Code.Trim().ToLower() != "draft")
                    return new ResponseModel { Status = "Error", Message = "Only draft record can be deleted" };

                context.Invoices.Remove(invoice);
                await context.SaveChangesAsync(currentUserId);

                return new ResponseModel { Status = "Success", Message = "Invoice successfully deleted" };
            }
            catch (Exception ex)
            {
                return new ResponseModel { Status = "Error", Message = SystemFormat.GetInnermostExceptionMessage(ex) };
            }
        }
    }
}