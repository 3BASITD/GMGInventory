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
    public class SalesOrderRepository : ISalesOrderRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;

        public SalesOrderRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
            , UserManager<User> userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        public async Task<Select2Model<JsonPurchaseOrderOptionsModel>> GetOptions(string search, int page)
        {
            var orders = from order in context.PurchaseOrders
                                              .Include(s => s.Supplier)
                                              .Include(s => s.Status)
                         join user in userManager.Users on order.UserCreated equals user.Id
                         where order.Status.Code != "Draft"
                         && (string.IsNullOrWhiteSpace(search)
                         || (order.PONumber.ToLower().Trim().Contains(search.ToLower().Trim())))
                         orderby order.PONumber ascending
                         select new JsonPurchaseOrderOptionsModel
                         {
                             Id = order.Id.ToString(),
                             OrderBy = SystemFormat.Name(user.FirstName, user.LastName, user.Nickname),
                             OrderDateD = order.ReplenishDate,
                             Supplier = order.Supplier.Name,
                             Text = order.PONumber
                         };
            var ordersFiltered = await orders.Skip((page - 1) * 50).Take(50).ToListAsync();
            return new Select2Model<JsonPurchaseOrderOptionsModel>
            {
                //incomplete_results = false,
                items = ordersFiltered,
                total_count = orders.Count()
            };
        }

        public async Task<Select2Model<JsonSalesOrderOptionsModel>> GetSalesOrderOptions(string search, int page)
        {
            var orders = from order in context.SalesOrders
                         where order.Status.Code.ToLower() != "submitted"
                         && order.Status.Code.ToLower() != "disapproved"
                         && order.Status.Code.ToLower() != "draft"
                         && (string.IsNullOrWhiteSpace(search)
                         || (order.SalesOrderNumber.ToLower().Trim().Contains(search.ToLower().Trim())))
                         orderby order.SalesOrderNumber ascending
                         select new JsonSalesOrderOptionsModel
                         {
                             Id = order.Id.ToString(),
                             Text = order.SalesOrderNumber,
                             CustomerCode = order.Customer.Code,
                             CustomerName = order.Customer.Name,
                             CustomerAddress = order.Customer
                                                    .ShippingAddresses
                                                    .Where(s => s.IsDefault == true)
                                                    .Select(s => s.Address)
                                                    .FirstOrDefault(),
                             SalesOrderDateD = order.SalesOrderDate,
                             ForwarderName = order.SalesOrderDeliveryMonitorings
                                                  .Select(s => s.Forwarder.Name)
                                                  .FirstOrDefault()
                         };
            var ordersFiltered = await orders.Skip((page - 1) * 50).Take(50).ToListAsync();
            return new Select2Model<JsonSalesOrderOptionsModel>
            {
                items = ordersFiltered,
                total_count = orders.Count()
            };
        }

        public async Task<Select2Model<JsonSalesOrderOptionsModel>> GetSalesOrderUnservedOnlyOptions(string search, int page, int? regionId)
        {
            var orders = from order in context.SalesOrders
                             //let oli = from soli in order.SalesOrderLineItems
                             //          let ii = context.ItemInventories
                             //                .Where(s => s.ItemId == soli.ItemId
                             //                         && s.Warehouse.IsMain == true)
                             //                .Sum(s => (int?)s.Quantity) ?? 0
                             //          select new
                             //          {
                             //              soli.Quantity,
                             //              Stock = ii
                             //          }
                         where order.Status.Code.ToLower() != "submitted"
                         && order.Status.Code.ToLower() != "disapproved"
                         && order.Status.Code.ToLower() != "draft"
                         && (string.IsNullOrWhiteSpace(search)
                         || (order.SalesOrderNumber.ToLower().Trim().Contains(search.ToLower().Trim())))
                         && (regionId == null || order.Customer.Area.Region.Id == regionId)
                         //&& oli.Any(s => s.Quantity > s.Stock)
                         orderby order.SalesOrderNumber ascending
                         select new JsonSalesOrderOptionsModel
                         {
                             Id = order.Id.ToString(),
                             Text = order.SalesOrderNumber,
                             CustomerCode = order.Customer.Code,
                             CustomerName = order.Customer.Name,
                             CustomerAddress = order.Customer
                                                    .ShippingAddresses
                                                    .Where(s => s.IsDefault == true)
                                                    .Select(s => s.Address)
                                                    .FirstOrDefault(),
                             SalesOrderDateD = order.SalesOrderDate,
                             PurchaseOrder = order.PurchaseOrder,
                             ForwarderName = order.SalesOrderDeliveryMonitorings
                                                  .Select(s => s.Forwarder.Name)
                                                  .FirstOrDefault(),
                             RegionId = order.Customer.Area.RegionId
                         };
            var ordersFiltered = await orders.Skip((page - 1) * 50).Take(50).ToListAsync();
            return new Select2Model<JsonSalesOrderOptionsModel>
            {
                items = ordersFiltered,
                total_count = orders.Count()
            };
        }

        public async Task<Select2Model<JsonSalesOrderOptionsModel>> GetSalesOrderInvoicedOptions(string search, int page)
        {
            var orders = from order in context.SalesOrders
                         where order.Invoices.Any(s => s.Status.Code.ToLower() == "invoiced")
                         && (string.IsNullOrWhiteSpace(search)
                         || order.SalesOrderNumber.ToLower().Trim().Contains(search.ToLower().Trim())
                         || order.Invoices.Any(s => s.InvoiceNumber.ToLower().Trim().Contains(search.ToLower().Trim()))
                         || order.Invoices.Any(s => s.ControlNumber.ToLower().Trim().Contains(search.ToLower().Trim())))
                         orderby order.SalesOrderNumber ascending
                         select new JsonSalesOrderOptionsModel
                         {
                             Id = order.Id.ToString(),
                             Text = order.SalesOrderNumber,
                             CustomerId = order.Customer.Id,
                             CustomerCode = order.Customer.Code,
                             CustomerName = order.Customer.Name,
                             CustomerAddress = order.Customer
                                                    .ShippingAddresses
                                                    .Where(s => s.IsDefault == true)
                                                    .Select(s => s.Address)
                                                    .FirstOrDefault(),
                             SalesOrderDateD = order.SalesOrderDate,
                             ForwarderName = order.SalesOrderDeliveryMonitorings
                                                  .Select(s => s.Forwarder.Name)
                                                  .FirstOrDefault(),
                             Invoices = string.Join(", ", order.Invoices.Select(s => s.InvoiceNumber + " (" + s.ControlNumber + ")"))
                         };
            var ordersFiltered = await orders.Skip((page - 1) * 50).Take(50).ToListAsync();
            return new Select2Model<JsonSalesOrderOptionsModel>
            {
                items = ordersFiltered,
                total_count = orders.Count()
            };
        }

        public async Task<IList<JsonOptionsModel>> GetStatusOptions()
        {
            return await context.SalesOrders
                                .Select(s => new JsonOptionsModel
                                {
                                    Id = s.Status.Id.ToString(),
                                    Text = s.Status.Name
                                })
                                .Distinct()
                                .OrderBy(s => s.Id)
                                .ToListAsync();
        }

        public async Task<SalesOrderDetailsVM?> GetDetailsAsync(int? Id)
        {
            var customerInvoices = from inv in context.InvoiceLineItems
                                   where inv.Invoice.Status.Code.ToLower() == "invoiced"
                                   join oli in context.SalesOrderLineItems on new { inv.Invoice.SalesOrderId, inv.ItemId }
                                                                       equals new { oli.SalesOrderId, oli.ItemId }
                                   let discount1 = (100 - oli.Discount1) / 100
                                   let discount2 = (100 - oli.Discount2) / 100
                                   group new { inv.Quantity, inv.UnitPrice, discount1, discount2 } by inv.Invoice.SalesOrder.Customer.Id into g
                                   select new
                                   {
                                       CustomerId = g.Key,
                                       InvoiceAmount = g.Sum(s => (decimal?)(s.Quantity * s.UnitPrice * s.discount1 * s.discount2)) ?? 0
                                   };

            var customerChecks = from chk in context.SOACRChecks
                                 group chk by chk.CustomerPayment.CustomerId into g
                                 select new
                                 {
                                     CustomerId = g.Key,
                                     CheckAmount = g.Sum(s => (decimal?)s.CheckAmount) ?? 0
                                 };

            var customerPayments = from pay in context.CustomerPayments
                                   group pay by pay.CustomerId into g
                                   select new
                                   {
                                       CustomerId = g.Key,
                                       PaymentAmount = g.Sum(s => (decimal?)(s.BackLoad + s.CashAdvance + s.Cash + s.Discount + s.OffSet + s.OverPayment + s.Tax + s.TKTSolicitation) ?? 0)
                                   };

            var customerSalesReturns = from tbr in context.SOACRLineItemTBRs
                                       where tbr.SalesReturnLineItem.InvoiceLineItem!.Invoice.Status.Code.ToLower() == "invoiced"
                                       group tbr by tbr.SalesReturnLineItem.InvoiceLineItem!.Invoice.SalesOrder.Customer.Id into g
                                       select new
                                       {
                                           CustomerId = g.Key,
                                           SalesReturnAmount = g.Sum(s => (decimal?)(s.SalesReturnLineItem.Quantity
                                                                        * s.SalesReturnLineItem.SellingPrice
                                                                        * (100 - s.SalesReturnLineItem.Discount1) / 100
                                                                        * (100 - s.SalesReturnLineItem.Discount2) / 100)) ?? 0
                                       };

            var customerSOACR = from s in context.SOACRLineItems
                                group s by s.Invoice.SalesOrder.Customer.Id into g
                                select new
                                {
                                    CustomerId = g.Key,
                                    CreditMemoAmount = g.Sum(s => (decimal?)s.CreditAmount) ?? 0,
                                    DebitMemoAmount = g.Sum(s => (decimal?)s.DebitAmount) ?? 0,
                                    FreightAmount = g.Sum(s => (decimal?)s.FreightCost) ?? 0
                                };

            var orderIncompletePack = from soli in context.SalesOrderLineItems
                                      join pli in context.PackLineItems on new { soli.SalesOrderId, soli.ItemId } equals new { pli.Pack.SalesOrderId, pli.ItemId } into p
                                      from pli in p.DefaultIfEmpty()
                                      where soli.Quantity > (pli == null ? 0 : pli.Quantity)
                                      select soli.SalesOrderId;

            var orderVM = (from order in context.SalesOrders
                           join userc in userManager.Users on order.UserCreated equals userc.Id
                           join userm in userManager.Users on order.UserModified equals userm.Id into um
                           from userm in um.DefaultIfEmpty()
                           from approve in userManager.Users.Where(s => s.Id == order.ApprovedBy).DefaultIfEmpty()
                           from disapprove in userManager.Users.Where(s => s.Id == order.DisapprovedBy).DefaultIfEmpty()
                           let invoiceAmount = customerInvoices.Where(s => s.CustomerId == order.Customer.Id)
                                                               .Sum(s => (decimal?)s.InvoiceAmount) ?? 0
                           let checkAmount = customerChecks.Where(s => s.CustomerId == order.Customer.Id)
                                                           .Sum(s => (decimal?)s.CheckAmount) ?? 0
                           let paymentAmount = customerPayments.Where(s => s.CustomerId == order.Customer.Id)
                                                               .Sum(s => (decimal?)s.PaymentAmount) ?? 0
                           let salesReturnAmount = customerSalesReturns.Where(s => s.CustomerId == order.Customer.Id)
                                                                       .Sum(s => (decimal?)s.SalesReturnAmount) ?? 0
                           let creditAmount = customerSOACR.Where(s => s.CustomerId == order.Customer.Id)
                                                           .Sum(s => (decimal?)s.CreditMemoAmount) ?? 0
                           let debitAmount = customerSOACR.Where(s => s.CustomerId == order.Customer.Id)
                                                          .Sum(s => (decimal?)s.DebitMemoAmount) ?? 0
                           let freightAmount = customerSOACR.Where(s => s.CustomerId == order.Customer.Id)
                                                            .Sum(s => (decimal?)s.FreightAmount) ?? 0
                           let resubmitfrom = context.SalesOrders
                                                     .Where(s => s.ResubmitId == order.Id)
                                                     .FirstOrDefault()
                           where order.Id == Id
                           select new SalesOrderDetailsVM
                           {
                               AccountStatus = order.Customer.CustomerAccountStatus.Name,
                               AgentCode = order.Agent.Code,
                               AgentId = order.Agent.Id,
                               AgentName = order.Agent.FirstName + " " + order.Agent.LastName,
                               ApprovedBy = approve.FirstName + " " + approve.LastName,
                               ApprovedDateD = order.ApprovedDate,
                               Branch = order.Customer.Branch,
                               CreditTerms = order.Customer.CreditTerm.Name,
                               CustomerAddress = order.Customer
                                                      .ShippingAddresses
                                                      .Where(s => s.IsDefault == true)
                                                      .Select(s => s.Address)
                                                      .FirstOrDefault(),
                               CustomerCode = order.Customer.Code,
                               CustomerId = order.Customer.Id,
                               CustomerName = order.Customer.Name,
                               CustomerContactNumber = order.Customer
                                                            .ShippingAddresses
                                                            .Where(s => s.IsDefault == true)
                                                            .Select(s => s.PhoneNumber)
                                                            .FirstOrDefault(),
                               CustomerContactPerson = order.Customer
                                                            .ShippingAddresses
                                                            .Where(s => s.IsDefault == true)
                                                            .Select(s => s.ContactPerson)
                                                            .FirstOrDefault(),
                               DisapprovedBy = SystemFormat.Name(disapprove.FirstName, disapprove.LastName, disapprove.Nickname),
                               DisapprovedDateD = order.DisapprovedDate,
                               DisapprovedReason = order.DisapprovedReason + "",
                               Forwarder = order.SalesOrderDeliveryMonitorings.Select(s => s.Forwarder.Name).FirstOrDefault(),
                               ForwarderId = order.SalesOrderDeliveryMonitorings.Select(s => s.Forwarder.Id).FirstOrDefault(),
                               Id = order.Id,
                               OrderDateD = order.SalesOrderDate,
                               OutstandingBalanceD = invoiceAmount
                                                   - (paymentAmount + checkAmount)
                                                   - salesReturnAmount
                                                   - creditAmount
                                                   + debitAmount
                                                   + freightAmount,
                               OverallDiscountD = order.OverallDiscount,
                               PONumber = order.PurchaseOrder,
                               PriorityD = order.Customer.Priority,
                               Region = order.Customer.Area.Region.Name,
                               Remarks = order.Remarks + "",
                               RequiredDeliveryDateD = order.RequiredDeliveryDate,
                               ResubmitId = order.ResubmitId,
                               ResubmitFromId = resubmitfrom == null ? null : resubmitfrom.Id,
                               ServiceLevelAgreement = order.ServiceLevelAgreementId == null ? null : order.ServiceLevelAgreement.Name,
                               ServiceLevelAgreementId = order.ServiceLevelAgreementId == null ? null : order.ServiceLevelAgreement.Id,
                               ShippingAddress = "",
                               SONumber = order.SalesOrderNumber,
                               SpecialInstruction = order.SpecialInstruction,
                               Status = order.Status.Code,
                               WithLP = order.WithLP,
                               WithREYCONonDefective = order.WithREYCONonDefective,
                               WithREYCOJITLPDelivery = order.WithREYCOJITLPDeliveryId == null ? null : order.WithREYCOJITLPDelivery.Name,
                               WithREYCOJITLPDeliveryId = order.WithREYCOJITLPDeliveryId == null ? null : order.WithREYCOJITLPDelivery.Id,
                               IsAllPacked = !orderIncompletePack.Contains(order.Id),
                               UserCreated = userc.FirstName + " " + userc.LastName,
                               DateCreatedD = order.DateCreated,
                               UserModified = userm == null ? null : userm.FirstName + " " + userm.LastName,
                               DateModifiedD = order.DateModified == null ? null : order.DateModified!.Value
                           });

            return await orderVM.FirstOrDefaultAsync();
        }

        public async Task<SalesOrderPrintVM?> PrintDetailsAsync(int? Id)
        {
            var details = await GetDetailsAsync(Id);
            if (details == null)
            {
                return null;
            }
            else
            {
                return new SalesOrderPrintVM
                {
                    Details = details,
                    Items = await GetItemsAsync(Id ?? 0)
                };
            }
        }

        public async Task<IList<SalesOrderVM>> GetListAsync(SalesOrderFilterVM filter)
        {
            var currentUser = await userManager.GetUserAsync(httpContextAccessor?.HttpContext?.User);
            var currentUserId = currentUser.Id;

            var users = userManager.Users;

            var orderIncompletePack = from soli in context.SalesOrderLineItems
                                      join pli in context.PackLineItems on new { soli.SalesOrderId, soli.ItemId } equals new { pli.Pack.SalesOrderId, pli.ItemId } into p
                                      from pli in p.DefaultIfEmpty()
                                      where soli.Quantity > (pli == null ? 0 : pli.Quantity)
                                      select soli.SalesOrderId;


            var orderVM = (from order in context.SalesOrders
                           join user in users on order.UserCreated equals user.Id
                           join approvedby in users on order.ApprovedBy equals approvedby.Id into ab
                           from approvedby in ab.DefaultIfEmpty()
                           join disapprovedby in users on order.DisapprovedBy equals disapprovedby.Id into db
                           from disapprovedby in db.DefaultIfEmpty()
                           where (filter.AgentId == null || order.AgentId == filter.AgentId)
                           && (filter.CustomerId == null || order.CustomerId == filter.CustomerId)
                           && (filter.RegionId == null || order.Customer.Area.Region.Id == filter.RegionId)
                           && filter.DateFrom <= order.SalesOrderDate.Date
                           && order.SalesOrderDate.Date <= filter.DateTo
                           && (filter.Status == null || filter.Status == order.Status.Id)
                           && (filter.FiledBy == null || filter.FiledBy.ToLower() != "me" || (filter.FiledBy.ToLower() == "me" && order.UserCreated == currentUserId))
                           select new SalesOrderVM
                           {
                               CustomerId = order.Customer.Id,
                               CustomerCode = order.Customer.Code,
                               CustomerName = order.Customer.Name,
                               Id = order.Id,
                               OrderDateD = order.DateCreated,
                               SONumber = order.SalesOrderNumber,
                               Status = order.Status.Code,
                               TotalAmountD = order.SalesOrderLineItems.Sum(s => s.Quantity
                                                                               * s.SellingPrice
                                                                               * (100 - s.Discount1) / 100
                                                                               * (100 - s.Discount2) / 100),
                               CustomerAddress = order.Customer
                                                      .ShippingAddresses
                                                      .Where(s => s.IsDefault == true)
                                                      .Select(s => s.Address)
                                                      .FirstOrDefault() ?? null,
                               PriorityD = order.Customer.Priority,
                               Region = order.Customer.Area.Region.Name,
                               Area = order.Customer.Area.Name,
                               RequiredDeliveryDateD = order.RequiredDeliveryDate,
                               Forwarder = order.SalesOrderDeliveryMonitorings
                                                .OrderByDescending(s => s.DateCreated)
                                                .Select(s => s.Forwarder.Name)
                                                .FirstOrDefault(),
                               ApprovedDisapprovedBy = approvedby != null
                                                     ? SystemFormat.Name(approvedby.FirstName, approvedby.LastName, approvedby.Nickname)
                                                     : disapprovedby != null
                                                     ? SystemFormat.Name(disapprovedby.FirstName, disapprovedby.LastName, disapprovedby.Nickname)
                                                     : null,
                               ApprovedDisapprovedDateD = order.ApprovedDate ?? order.DisapprovedDate,
                               IsAllPacked = !orderIncompletePack.Contains(order.Id),
                               //PurchaseRequestDetail = string.Join(", ",
                               //                        context.PurchaseRequestLineItems
                               //                        .Where(prli => prli.SalesOrderId == order.Id)
                               //                        .Join(context.PurchaseRequests,
                               //                              prli => prli.PurchaseRequestId,
                               //                              pr => pr.Id,
                               //                              (prli, pr) => new { pr, prli })
                               //                        .Select(x => $"{x.pr.DocumentNumber}-[{x.pr.Status.Name}]")
                               //                        .Distinct()
                               //                        ),
                               PurchaseRequestDetail = string.Join(", ", order.PurchaseRequestLineItems
                                                                              .Select(s => new
                                                                              {
                                                                                  Id = s.PurchaseRequest.Id,
                                                                                  Number = s.PurchaseRequest.DocumentNumber,
                                                                                  Status = s.PurchaseRequest.Status.Name
                                                                              })
                                                                              .Distinct()
                                                                              .Select(s => $"<a href='/PurchaseRequest/Details/{s.Id}' class='text-info' target='_blank'>{s.Number}</a>-[{s.Status}]")),
                               ServiceLevelAgreement = order.ServiceLevelAgreement.Name,
                               WithLP = order.WithLP,
                               WithREYCONonDefective = order.WithREYCONonDefective,
                               WithREYCOJITLPDelivery = order.WithREYCOJITLPDelivery.Name,
                               SpecialInstruction = order.SpecialInstruction,
                               Remarks = order.Remarks,
                               OrderQueueNumber = order.SalesOrderLineItems
                                                       .Select(s => s.SalesOrderSource.SourceOrderNumber)
                                                       .FirstOrDefault()
                           });
            return await orderVM.ToListAsync();
        }

        public async Task<IList<APISalesOrder>> GetListAPI(DateTime? OrderDate)
        {
            if (OrderDate == null) OrderDate = DateTime.Today;

            var orders = from order in context.SalesOrders
                         where order.SalesOrderDate.Date == OrderDate.Value.Date
                         && order.ApprovedBy != null
                         && order.ApprovedDate != null
                         select new APISalesOrder
                         {
                             Id = order.Id,
                             AgentId = order.AgentId,
                             CustomerId = order.CustomerId,
                             SalesOrderNumber = order.SalesOrderNumber,
                             OrderDate = order.SalesOrderDate,
                             LineItems = from item in order.SalesOrderLineItems
                                         select new APISalesOrderLineItem
                                         {
                                             Id = item.Id,
                                             LineNumber = item.LineNumber,
                                             ItemId = item.ItemId,
                                             Quantity = item.Quantity
                                         }
                         };
            return await orders.ToListAsync();
        }

        public async Task<ForDataTables<SalesOrderVM>> GetListDTAsync(DataTablesRequestForm form, SalesOrderFilterVM filter)
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
                    s.CustomerCode,
                    s.CustomerName,
                    s.OrderDate,
                    s.SONumber,
                    s.Status,
                    s.TotalAmount,
                    s.CustomerAddress,
                    s.Priority,
                    s.Region,
                    s.Area,
                    s.RequiredDeliveryDate,
                    s.PurchaseRequestDetail,
                    s.Forwarder
                }.Any(a => a != null && a.ToLower().Contains(form.SearchValue.ToLower())));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                var direction = form.SortColumnDirection.ToLower();
                var sortExpr = $"{form.SortColumn} {direction}";
                IOrderedQueryable<SalesOrderVM> orderedData = null;

                if (form.SortColumn.ToLower().Contains("date") || form.SortColumn == "Priority" || form.SortColumn == "TotalAmount")
                {
                    orderedData = direction == "asc" ? form.SortColumn switch
                    {
                        "OrderDate" => data.OrderBy(s => s.OrderDateD),
                        "TotalAmount" => data.OrderBy(s => s.TotalAmountD),
                        "Priority" => data.OrderBy(s => s.PriorityD),
                        "RequiredDeliveryDate" => data.OrderBy(s => s.RequiredDeliveryDateD),
                        _ => data.OrderBy(sortExpr)
                    }
                    : form.SortColumn switch
                    {
                        "OrderDate" => data.OrderByDescending(s => s.OrderDateD),
                        "TotalAmount" => data.OrderByDescending(s => s.TotalAmountD),
                        "Priority" => data.OrderByDescending(s => s.PriorityD),
                        "RequiredDeliveryDate" => data.OrderByDescending(s => s.RequiredDeliveryDateD),
                        _ => data.OrderBy(sortExpr)
                    };

                    data = orderedData.ThenBy($"Priority {direction}")
                                      .ThenBy($"SONumber {direction}");
                }
                else
                {
                    data = data.OrderBy($"{sortExpr}, Priority {direction}, SONumber {direction}");
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<SalesOrderVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public async Task<IList<SalesOrderItemVM>> GetItemsAsync(int salesOrderId)
        {
            var itemVM = await (from order in context.SalesOrderLineItems
                                                     .Include(s => s.Item)
                                                     .ThenInclude(s => s.Unit)
                                from promo in context.ItemPromo
                                                    .Where(pr => pr.Id == order.ItemPromoId)
                                                    .OrderByDescending(pr => pr.DateCreated)
                                                    .Take(1)
                                                    .DefaultIfEmpty()
                                let price = context.ItemPrices
                                                   .Where(s => s.ItemId == order.ItemId
                                                            && s.DateCreated <= order.SalesOrder.ApprovedDate)
                                                   .OrderByDescending(s => s.DateCreated)
                                                   .FirstOrDefault()
                                let inventory = context.ItemInventories
                                                       .Where(s => s.ItemId == order.ItemId && s.Warehouse.IsMain == true)
                                                       .Sum(s => (int?)s.Quantity) ?? 0
                                let packedQuantity = context.PackLineItems
                                                            .Where(s => s.Pack.SalesOrderId == order.SalesOrderId
                                                                     && s.ItemId == order.ItemId)
                                                            .Sum(s => (int?)s.Quantity) ?? 0
                                let invoicedQuantity = context.InvoiceLineItems
                                                              .Where(s => s.Invoice.SalesOrderId == order.SalesOrderId
                                                                       && s.ItemId == order.ItemId)
                                                              .Sum(s => (int?)s.Quantity) ?? 0
                                let returnedQuantity = context.SalesReturnLineItems
                                                              .Where(s => s.InvoiceLineItem!.Invoice.SalesOrderId == order.SalesOrderId
                                                                       && s.ItemId == order.ItemId)
                                                              .Sum(s => (int?)s.Quantity) ?? 0
                                where order.SalesOrderId == salesOrderId
                                orderby order.LineNumber ascending
                                select new SalesOrderItemVM
                                {
                                    Discount1D = order.Discount1,
                                    Discount2D = order.Discount2,
                                    Discount3D = order.Discount3 == null ? 0 : order.Discount3!.Value,
                                    Discount4D = order.Discount4 == null ? 0 : order.Discount4!.Value,
                                    Discount5D = order.Discount5 == null ? 0 : order.Discount5!.Value,
                                    CustomerBrandDiscountId = order.CustomerBrandDiscountId,
                                    Id = order.Id,
                                    InventoryCountD = inventory,
                                    InvoicedQuantityD = invoicedQuantity,
                                    ItemCode = order.Item.Code,
                                    ItemId = order.Item.Id,
                                    ItemName = order.Item.Name + (order.ItemPromoId == null ? "" : " (" + promo.Name + ")"),
                                    ItemPartNumber = order.Item.PartNumber + "",
                                    ItemSize = order.Item.Size,
                                    LineNumber = order.LineNumber,
                                    NetPriceD = price == null ? 0 : price.NetSellingPrice,
                                    QuantityD = order.Quantity,
                                    PackedQuantityD = packedQuantity,
                                    RemainingQuantityD = order.Quantity - packedQuantity,
                                    RetailPriceD = price == null ? 0 : price.RetailSellingPrice,
                                    SellingPriceD = order.SellingPrice,
                                    ServedQuantityD = packedQuantity - returnedQuantity,
                                    Unit = order.Item.Unit.Code,
                                    ItemPromoId = order.ItemPromoId
                                }).ToListAsync();

            return itemVM;
        }

        public async Task<IList<CustomerBrandDiscountVM>> GetBrandDiscountAsync(int customerId, int brandId)
        {
            var brandDiscount = await (from custDiscount in context.CustomerBrandDiscount
                                       where (customerId == custDiscount.CustomerId && brandId == custDiscount.BrandId && custDiscount.IsActive)

                                       select new CustomerBrandDiscountVM
                                       {
                                           Discount = custDiscount.Discount,
                                           BrandId = brandId,
                                           CustomerId = customerId,
                                           Id = custDiscount.Id

                                       }).ToListAsync();

            return brandDiscount;
        }

        public async Task<ForDataTables<SalesOrderItemVM>> GetItemsDTAsync(DataTablesRequestForm form, int salesOrderId)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetItemsAsync(salesOrderId)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Discount1.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.Discount2.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.InventoryCount.ToString().Contains(form.SearchValue)
                                    || x.InvoicedQuantity.ToString().Contains(form.SearchValue)
                                    || x.ItemCode.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.ItemName.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.LineNumber.ToString().Contains(form.SearchValue)
                                    || x.NetPriceD.ToString().Contains(form.SearchValue)
                                    || x.PackedQuantity.ToString().Contains(form.SearchValue)
                                    || x.Quantity.ToString().Contains(form.SearchValue)
                                    || x.RetailPriceD.ToString().Contains(form.SearchValue)
                                    || x.SellingPriceD.ToString().Contains(form.SearchValue)
                                    || x.Unit.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.GrossPriceD.ToString().Contains(form.SearchValue)
                                    || x.ServedQuantityD.ToString().Contains(form.SearchValue)
                                    || x.UnservedQuantityD.ToString().Contains(form.SearchValue));
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
                        case "InventoryCount": data = data.OrderBy(s => s.InventoryCount); break;
                        case "InvoicedQuantity": data = data.OrderBy(s => s.InvoicedQuantity); break;
                        case "ItemCode": data = data.OrderBy(s => s.ItemCode); break;
                        case "ItemName": data = data.OrderBy(s => s.ItemName); break;
                        case "LineNumber": data = data.OrderBy(s => s.LineNumber); break;
                        case "Quantity": data = data.OrderBy(s => s.Quantity); break;
                        case "NetPrice": data = data.OrderBy(s => s.NetPriceD); break;
                        case "PackedQuantity": data = data.OrderBy(s => s.PackedQuantity); break;
                        case "SellingPrice": data = data.OrderBy(s => s.SellingPriceD); break;
                        case "ServedQuantity": data = data.OrderBy(s => s.ServedQuantityD); break;
                        case "RetailPrice": data = data.OrderBy(s => s.RetailPriceD); break;
                        case "Unit": data = data.OrderBy(s => s.Unit); break;
                        case "UnservedQuantity": data = data.OrderBy(s => s.UnservedQuantityD); break;
                        case "GrossPrice": data = data.OrderBy(s => s.GrossPriceD); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Discount1": data = data.OrderByDescending(s => s.Discount1D); break;
                        case "Discount2": data = data.OrderByDescending(s => s.Discount2D); break;
                        case "InventoryCount": data = data.OrderByDescending(s => s.InventoryCount); break;
                        case "InvoicedQuantity": data = data.OrderByDescending(s => s.InvoicedQuantity); break;
                        case "ItemCode": data = data.OrderByDescending(s => s.ItemCode); break;
                        case "ItemName": data = data.OrderByDescending(s => s.ItemName); break;
                        case "LineNumber": data = data.OrderByDescending(s => s.LineNumber); break;
                        case "Quantity": data = data.OrderByDescending(s => s.Quantity); break;
                        case "NetPrice": data = data.OrderByDescending(s => s.NetPriceD); break;
                        case "PackedQuantity": data = data.OrderByDescending(s => s.PackedQuantity); break;
                        case "SellingPrice": data = data.OrderByDescending(s => s.SellingPriceD); break;
                        case "ServedQuantity": data = data.OrderByDescending(s => s.ServedQuantityD); break;
                        case "RetailPrice": data = data.OrderByDescending(s => s.RetailPriceD); break;
                        case "Unit": data = data.OrderByDescending(s => s.Unit); break;
                        case "UnservedQuantity": data = data.OrderByDescending(s => s.UnservedQuantityD); break;
                        case "GrossPrice": data = data.OrderByDescending(s => s.GrossPriceD); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<SalesOrderItemVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public async Task<ResponseModel<int>> SaveAsync(SalesOrderFormVM form)
        {
            //try
            //{
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

            var agent = await context.Agents.Where(s => s.Id == form.Agent).FirstOrDefaultAsync();
            if (agent == null) return new ResponseModel<int> { Status = "Error", Message = "Agent is invalid" };

            var customer = await context.Customers.Where(s => s.Id == form.Customer).FirstOrDefaultAsync();
            if (customer == null) return new ResponseModel<int> { Status = "Error", Message = "Customer is invalid" };

            var forwarder = await context.Forwarders.Where(s => s.Id == form.Forwarder).FirstOrDefaultAsync();
            if (forwarder == null) return new ResponseModel<int> { Status = "Error", Message = "Forwarder is invalid" };

            var sla = await context.ServiceLevelAgreements.Where(s => s.Id == form.SericeLevelAgreement).FirstOrDefaultAsync();
            if (sla == null) return new ResponseModel<int> { Status = "Error", Message = "Service Level Agreement is invalid" };


            var orderVM = await context.SalesOrders.Where(s => s.Id == form.Id).FirstOrDefaultAsync();

            int statusId = context.Statuses
                                  .Where(s => (form.Status.ToLower().Contains("submit") && s.Name.ToLower() == "sales order submitted")
                                           || (form.Status.ToLower().Contains("draft") && s.Name.ToLower() == "sales order draft"))
                                  .Select(s => s.Id)
                                  .First();
            if (orderVM == null)
            {

                var previous = await context.SalesOrders.Where(s => s.Id == form.ResubmitId).FirstOrDefaultAsync();
                string soNumber = "";
                if (previous == null)
                {
                    var latestOrder = context.SalesOrders
                                             .OrderByDescending(s => s.SalesOrderNumber)
                                             .Select(s => s.SalesOrderNumber)
                                             .FirstOrDefault();

                    if (latestOrder != null && latestOrder.Contains("-D"))
                    {
                        var splitLatestOrder = latestOrder.Split("-D");
                        latestOrder = splitLatestOrder[0];
                    }

                    if (latestOrder != null && latestOrder.Contains('.'))
                    {
                        var splitLatestOrder = latestOrder.Split('.');
                        latestOrder = splitLatestOrder[0];
                    }

                    var latestOrderNumber = latestOrder == null ? 0 : int.Parse(latestOrder.Replace("SO", ""));
                    soNumber = string.Format("{0:SO00000000}", latestOrderNumber + 1);
                }
                else if (!previous.SalesOrderNumber.Contains("-D"))
                {
                    return new ResponseModel<int> { Status = "Error", Message = "Invalid resubmission. The previous sales order number is required to be in the valid format of a deactivated sales order" };
                }
                else
                {
                    var tempSalesOrderNumber = previous.SalesOrderNumber.Split("-D");
                    soNumber = tempSalesOrderNumber[0];
                }


                //if (form.Status.ToLower().Contains("draft")
                // && await context.SalesOrders.AnyAsync(s => s.StatusId == statusId && s.SalesOrderNumber == soNumber))
                //{
                //    return new ResponseModel<int> { Status = "Error", Message = "Failed to add. A draft SO with the same SO Number already exists" };
                //}
                var temp = await context.SalesOrders.Where(s => s.SalesOrderNumber == soNumber && (form.ResubmitId == null || s.Id != form.ResubmitId)).ToListAsync();
                //if (await context.SalesOrders.AnyAsync(s => s.SalesOrderNumber == soNumber && (form.ResubmitId == null || s.Id != form.ResubmitId)))
                if (await context.SalesOrders.AnyAsync(s => s.SalesOrderNumber == soNumber))
                    return new ResponseModel<int> { Status = "Error", Message = "Generated a conflicting Sales Order Number, Please try again. If error persist, consult IT" };

                orderVM = new SalesOrder
                {
                    AgentId = agent.Id,
                    CustomerId = customer.Id,
                    RequiredDeliveryDate = form.DeliveryDate,
                    PurchaseOrder = form.PurchaseOrder,
                    Remarks = form.Remarks,
                    SalesOrderDate = form.OrderDate,
                    SalesOrderNumber = soNumber,
                    StatusId = statusId,
                    ServiceLevelAgreementId = form.SericeLevelAgreement,
                    OverallDiscount = form.OverallDiscount,
                    WithLP = form.WithLP,
                    WithREYCONonDefective = form.WithREYCONonDefective,
                    WithREYCOJITLPDeliveryId = form.WithREYCOJITLPDelivery,
                    SpecialInstruction = form.SpecialInstruction
                };
                await context.SalesOrders.AddAsync(orderVM);
                await context.SaveChangesAsync(currentUserId);

                if (previous != null)
                {
                    //var soLineItems = await context.SalesOrderLineItems.Where(s => s.SalesOrderId == previous.Id).ToListAsync();
                    //if (soLineItems != null)
                    //{
                    //    context.SalesOrderLineItems.RemoveRange(soLineItems);
                    //    await context.SaveChangesAsync(currentUserId);
                    //}

                    var soDelivery = await context.SalesOrderDeliveryMonitorings.Where(s => s.SalesOrderId == previous.Id).FirstOrDefaultAsync();
                    if (soDelivery != null)
                    {
                        context.SalesOrderDeliveryMonitorings.Remove(soDelivery);
                        await context.SaveChangesAsync(currentUserId);
                    }

                    context.SalesOrders.Remove(previous);

                    previous.ResubmitId = orderVM.Id;
                    context.SalesOrders.Update(previous);
                    await context.SaveChangesAsync(currentUserId);
                }
                else
                {
                    SalesOrderDeliveryMonitoring salesOrderDelivery = new()
                    {
                        ForwarderId = form.Forwarder,
                        SalesOrderId = orderVM.Id
                    };

                    await context.SalesOrderDeliveryMonitorings.AddAsync(salesOrderDelivery);
                    await context.SaveChangesAsync(currentUserId);
                }
            }
            else
            {
                orderVM.AgentId = agent.Id;
                orderVM.CustomerId = customer.Id;
                orderVM.RequiredDeliveryDate = form.DeliveryDate;
                orderVM.Remarks = form.Remarks;
                orderVM.SalesOrderDate = form.OrderDate;
                orderVM.StatusId = statusId;
                orderVM.ServiceLevelAgreementId = form.SericeLevelAgreement;
                orderVM.OverallDiscount = form.OverallDiscount;
                orderVM.WithLP = form.WithLP;
                orderVM.WithREYCONonDefective = form.WithREYCONonDefective;
                orderVM.WithREYCOJITLPDeliveryId = form.WithREYCOJITLPDelivery;
                orderVM.SpecialInstruction = form.SpecialInstruction;

                context.SalesOrders.Update(orderVM);
                await context.SaveChangesAsync(currentUserId);

                var salesOrderDelivery = await context.SalesOrderDeliveryMonitorings
                                                      .Where(s => s.SalesOrderId == orderVM.Id)
                                                      .FirstOrDefaultAsync();
                if (salesOrderDelivery != null)
                {
                    salesOrderDelivery.ForwarderId = form.Forwarder;

                    context.SalesOrderDeliveryMonitorings.Update(salesOrderDelivery);
                    await context.SaveChangesAsync(currentUserId);
                }
                else
                {
                    salesOrderDelivery = new SalesOrderDeliveryMonitoring()
                    {
                        ForwarderId = form.Forwarder,
                        SalesOrderId = orderVM.Id
                    };

                    await context.SalesOrderDeliveryMonitorings.AddAsync(salesOrderDelivery);
                    await context.SaveChangesAsync(currentUserId);
                }
            }

            await SaveItemsAsync(form.Items, orderVM.Id);

            return new ResponseModel<int>
            {
                Status = "Success",
                Message = form.Status.ToLower() == "draft" ? "Sales order saved as draft." : "Sales order successfully submitted.",
                NewRow = orderVM.Id
            };
            //}
            //catch (Exception ex)
            //{
            //    return new ResponseModel<int> { Status = "Error", Message = SystemFormat.GetInnermostExceptionMessage(ex) };
            //}
        }

        public async Task SaveItemsAsync(IList<SalesOrderFormItemVM> Items, int salesOrderId)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var citemsVM = context.SalesOrderLineItems.Where(s => s.SalesOrderId == salesOrderId);

            Items = Items.OrderBy(x => x.LineNumber)
                         .Select((item, index) =>
                         {
                             item.LineNumber = index + 1;
                             return item;
                         }).ToList();

            // add item
            // check items not in db
            var items = citemsVM.Select(s => s.ItemId);
            // add excluding items already available
            var itemsVM = Items.Where(s => items.Contains(s.Item) == false)
                               .Select(s => new SalesOrderLineItem
                               {
                                   LineNumber = s.LineNumber,
                                   Discount1 = s.Discount1,
                                   Discount2 = s.Discount2,
                                   Discount3 = s.Discount3,
                                   Discount4 = s.Discount4,
                                   Discount5 = s.Discount5,
                                   ItemId = s.Item,
                                   Quantity = s.Quantity,
                                   SalesOrderId = salesOrderId,
                                   SellingPrice = s.UnitPrice,
                                   CustomerBrandDiscountId = s.CustomerBrandDiscountId,
                                   ItemPromoId = s.ItemPromoId

                               });
            context.SalesOrderLineItems.AddRange(itemsVM);

            // update item
            // get items available in db
            var ditems = Items.Where(s => items.Contains(s.Item));
            foreach (var di in ditems)
            {
                // update all available in db
                var dbitem = await citemsVM.Where(s => s.ItemId == di.Item).FirstAsync();
                dbitem.LineNumber = di.LineNumber;
                dbitem.Discount1 = di.Discount1;
                dbitem.Discount2 = di.Discount2;
                dbitem.Discount3 = di.Discount3;
                dbitem.Discount4 = di.Discount4;
                dbitem.Discount5 = di.Discount5;
                dbitem.Quantity = di.Quantity;
                dbitem.SellingPrice = di.UnitPrice;
                dbitem.CustomerBrandDiscountId = di.CustomerBrandDiscountId;
                dbitem.ItemPromoId = di.ItemPromoId;

                context.SalesOrderLineItems.Update(dbitem);
            }

            // delete item
            // get items not in form
            var fitems = Items.Select(s => s.Item);
            var ritems = citemsVM.Where(s => fitems.Contains(s.ItemId) == false);
            // remove unavailable items
            context.SalesOrderLineItems.RemoveRange(ritems);

            await context.SaveChangesAsync(currentUserId);
        }

        public async Task<ResponseModel> DeleteDraft(int Id)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var order = await context.SalesOrders
                                     .Include(s => s.Status)
                                     .Where(s => s.Id == Id)
                                     .FirstOrDefaultAsync();
            if (order == null) return new ResponseModel { Status = "Error", Message = "Sales Order cannot be found" };

            if (order.Status.Name.ToLower() != "sales order draft") return new ResponseModel { Status = "Error", Message = "Cannot delete sales order that are not draft" };

            context.SalesOrders.Remove(order);
            var items = await context.SalesOrderLineItems
                                     .Where(s => s.SalesOrderId == Id)
                                     .ToListAsync();

            context.SalesOrderLineItems.RemoveRange(items);
            var delivery = await context.SalesOrderDeliveryMonitorings
                                        .Where(s => s.SalesOrderId == Id)
                                        .FirstOrDefaultAsync();

            if (delivery != null)
                context.SalesOrderDeliveryMonitorings.Remove(delivery);

            await context.SaveChangesAsync(currentUserId);

            return new ResponseModel { Status = "Success", Message = "Successfully deleted sales order" };
        }

        public async Task<bool> ApproveAsync(int salesOrderId)
        {
            var orderVM = context.SalesOrders.Where(s => s.Id == salesOrderId).FirstOrDefault();
            if (orderVM == null)
            {
                return false;
            }
            else
            {
                string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
                int statusId = context.Statuses.Where(s => s.Name.ToLower() == "sales order approved").Select(s => s.Id).First();
                orderVM.ApprovedBy = currentUserId;
                orderVM.ApprovedDate = DateTime.Now;
                orderVM.DisapprovedBy = null;
                orderVM.DisapprovedDate = null;
                orderVM.StatusId = statusId;

                context.SalesOrders.Update(orderVM);
                await context.SaveChangesAsync(currentUserId);

                return true;
            }
        }

        public async Task<bool> DisapproveAsync(int salesOrderId, string reason)
        {
            var orderVM = context.SalesOrders.Where(s => s.Id == salesOrderId).FirstOrDefault();
            if (orderVM == null)
            {
                return false;
            }
            else
            {
                var tempSalesOrderNumber = orderVM.SalesOrderNumber + "-D";
                var latestDisapprovedOrder = context.SalesOrders
                                         .Where(s => s.SalesOrderNumber.StartsWith(tempSalesOrderNumber))
                                         .OrderByDescending(s => s.SalesOrderNumber)
                                         .Select(s => s.SalesOrderNumber)
                                         .FirstOrDefault();
                var latestDisapprovedOrderNumber = latestDisapprovedOrder == null
                                                 ? 0
                                                 : int.Parse(latestDisapprovedOrder.Replace(tempSalesOrderNumber, ""));
                var disapprovedOrderNumber = tempSalesOrderNumber + string.Format("{0:00}", latestDisapprovedOrderNumber + 1);

                string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
                int statusId = await context.Statuses.Where(s => s.Name.ToLower() == "sales order disapproved").Select(s => s.Id).FirstAsync();
                orderVM.SalesOrderNumber = disapprovedOrderNumber;
                orderVM.ApprovedBy = null;
                orderVM.ApprovedDate = null;
                orderVM.DisapprovedBy = currentUserId;
                orderVM.DisapprovedDate = DateTime.Now;
                orderVM.DisapprovedReason = reason;
                orderVM.StatusId = statusId;

                context.SalesOrders.Update(orderVM);
                await context.SaveChangesAsync(currentUserId);

                //var delivery = await context.SalesOrderDeliveryMonitorings.Where(s => s.SalesOrderId == salesOrderId).FirstOrDefaultAsync();
                //if (delivery != null)
                //{
                //    context.SalesOrderDeliveryMonitorings.Remove(delivery);
                //    await context.SaveChangesAsync(currentUserId);
                //}

                return true;
            }
        }

        public async Task<bool> UpdateServedSOItem(int salesOrderLineItemId, int quantity)
        {
            var soItem = await context.SalesOrderLineItems
                                      .Where(s => s.Id == salesOrderLineItemId)
                                      .FirstOrDefaultAsync();
            if (soItem == null)
            {
                return false;
            }
            else
            {
                string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
                var inventory = await context.ItemInventories
                                             .Where(s => s.ItemId == soItem.ItemId && s.Warehouse.IsMain == true)
                                             .FirstOrDefaultAsync();
                if (inventory == null)
                {
                    return false;
                }
                else if (inventory.Quantity >= quantity)
                {
                    inventory.Quantity -= quantity;
                }
                else
                {
                    inventory.Quantity = 0;
                }
                context.SalesOrderLineItems.Update(soItem);
                context.ItemInventories.Update(inventory);
                await context.SaveChangesAsync(currentUserId);
                return true;
            }
        }
    }
}