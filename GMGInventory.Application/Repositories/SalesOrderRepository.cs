using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using GMGInventory.Data.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Dynamic.Core;

namespace GMGInventory.Application.Repositories
{
    public class SalesOrderRepository : ISalesOrderRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly IDeliveryMonitoringRepository _deliveryMonitoringRepository;

        public SalesOrderRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
            , UserManager<User> userManager
            , IDeliveryMonitoringRepository deliveryMonitoringRepository)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            _deliveryMonitoringRepository = deliveryMonitoringRepository;
        }

        public async Task<Select2Model<JsonPurchaseOrderOptionsModel>> GetOptions(string search, int page)
        {
            var orders = from order in context.PurchaseOrders
                                              .Include(s => s.Supplier)
                                              .Include(s => s.Status)
                         join user in userManager.Users on order.UserCreated equals user.Id
                         where order.Status.Code != "Draft"
                         && (string.IsNullOrWhiteSpace(search)
                         || (order.PONumber.Trim().ToLower().Contains(search.Trim().ToLower())))
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
                         && order.Status.Code.ToLower() != "force close"
                         && (string.IsNullOrWhiteSpace(search)
                         || (order.SalesOrderNumber.Trim().ToLower().Contains(search.Trim().ToLower())))
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
                             ForwarderName = order.Forwarder != null ? order.Forwarder.Name : null
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
                         || (order.SalesOrderNumber.Trim().ToLower().Contains(search.Trim().ToLower())))
                         && (regionId == null || order.Region.Id == regionId)
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
                             ForwarderName = order.Forwarder != null ? order.Forwarder.Name : null,
                             RegionId = order.RegionId
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
                         where order.Invoices.Any(s => (s.Status.Code.Trim().ToLower() == "invoiced" || s.Status.Code.Trim().ToLower() == "draft"))
                         && (string.IsNullOrWhiteSpace(search)
                         || order.SalesOrderNumber.Trim().ToLower().Contains(search.Trim().ToLower())
                         || order.Invoices.Any(s => s.InvoiceNumber.Trim().ToLower().Contains(search.Trim().ToLower()))
                         || order.Invoices.Any(s => s.ControlNumber.Trim().ToLower().Contains(search.Trim().ToLower())))
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
                             ForwarderName = order.Forwarder != null ? order.Forwarder.Name : null,
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

        public async Task<Select2Model<JsonOptionsModel>> GetStatusOptions2(string search, int page)
        {
            var status = context.SalesOrders
                                .Select(s => new JsonOptionsModel
                                {
                                    Id = s.Status.Id.ToString(),
                                    Text = s.Status.Name
                                })
                                .Distinct()
                                .OrderBy(s => s.Id);
            var statusFiltered = await status.Skip((page - 1) * 50).Take(50).ToListAsync();
            return new Select2Model<JsonOptionsModel>
            {
                items = statusFiltered,
                total_count = status.Count()
            };
        }

        public async Task<SalesOrderDetailsVM?> GetDetailsAsync(int? Id)
        {
            var now = DateTime.Now;
            const double nearDueThreshold = 0.70;

            bool hasInvoice = false;

            var invoice = context.InvoiceLineItems
                                    .Where(s => s.Invoice.SalesOrderId == Id)
                                    .Any(s => s.Invoice.Status.Code.Trim().ToLower() == "invoiced" || s.Invoice.Status.Code.Trim().ToLower() == "draft");

            if (invoice)
            {
                hasInvoice = true;
            }

            var customerInvoices = from inv in context.InvoiceLineItems
                                   where (inv.Invoice.Status.Code.Trim().ToLower() == "invoiced" || inv.Invoice.Status.Code.Trim().ToLower() == "draft")
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
                                       where (tbr.SalesReturnLineItem.InvoiceLineItem!.Invoice.Status.Code.Trim().ToLower() == "invoiced" || tbr.SalesReturnLineItem.InvoiceLineItem!.Invoice.Status.Code.Trim().ToLower() == "draft")
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

            var purchaseRequest = from pritem in context.PurchaseRequestLineItems
                                  join pr in context.PurchaseRequests on pritem.PurchaseRequestId equals pr.Id
                                  join poli in context.PurchaseOrderNewLineItems on pritem.Id equals poli.RequestItemId
                                  join po in context.PurchaseOrderNews on poli.PurchaseOrderId equals po.Id
                                  where pritem.SalesOrderId == Id
                                  select new
                                  {
                                      PRDocumentNumber = pr.DocumentNumber,
                                      PODocumentNumber = po.DocumentNumber
                                  };

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
                           let purchaserequest = purchaseRequest.FirstOrDefault()
                           where order.Id == Id
                           select new SalesOrderDetailsVM
                           {
                               AccountStatus = order.Customer.CustomerAccountStatus.Name,
                               AgentCode = order.Agent.Code,
                               AgentId = order.Agent.Id,
                               AgentName = order.Agent.FirstName + " " + order.Agent.LastName,
                               AppOrderSource = order.SalesOrderItemSources.Count() > 0
                                              ? order.SalesOrderItemSources.First().SourceOrderNumber
                                              : null,
                               ApprovedBy = approve.FirstName + " " + approve.LastName,
                               ApprovedDateD = order.ApprovedDate,
                               SubmittedDateD = order.SubmittedDate,
                               ResubmittedDateD = order.ResubmittedDate,
                               Branch = order.Customer.Branch,
                               CreditLimitD = order.Customer.CreditLimit,
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
                               Forwarder = order.Forwarder != null ? order.Forwarder.Name : null,
                               ForwarderId = order.Forwarder != null ? order.Forwarder.Id : null,
                               Id = order.Id,
                               OrderDateD = order.SalesOrderDate,
                               OutstandingBalanceD = invoiceAmount
                                                   - (paymentAmount + checkAmount)
                                                   - salesReturnAmount
                                                   - creditAmount
                                                   + debitAmount
                                                   + freightAmount,
                               OverallDiscountD = order.OverallDiscount,
                               PriorityD = order.Customer.Priority,
                               RegionId = order.Region.Id,
                               Region = order.Region.Name,
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
                               DateModifiedD = order.DateModified == null ? null : order.DateModified!.Value,
                               PONumber = purchaserequest == null ? null : purchaserequest.PODocumentNumber,
                               PRNumber = purchaserequest == null ? null : purchaserequest.PRDocumentNumber,
                               SlaStatus = (order.Status.Id == 2 || order.Status.Id == 3)
                                                        && order.ServiceLevelAgreement.HourAvailability > 0
                                                      ? (EF.Functions.DateDiffMinute(order.DateCreated, now)
                                                            >= order.ServiceLevelAgreement.HourAvailability * 60
                                                            ? "Overdue"
                                                            : EF.Functions.DateDiffMinute(order.DateCreated, now)
                                                                >= order.ServiceLevelAgreement.HourAvailability * 60 * nearDueThreshold
                                                                ? "Near Due"
                                                                : "On Track")
                                                      : "",
                               HaveInvoice = hasInvoice
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
                    Items = await GetItemsAsync(Id ?? 0, null)
                };
            }
        }

        //public async Task<IList<SalesOrderVM>> GetListAsync(SalesOrderFilterVM filter)
        //{
        //    var currentUser = await userManager.GetUserAsync(httpContextAccessor?.HttpContext?.User);
        //    var currentUserId = currentUser.Id;

        //    var users = userManager.Users;

        //    var DateFrom = filter.DateFrom == null ? DateTime.Today.AddYears(-1) : filter.DateFrom.Value.Date;
        //    var DateTo = filter.DateTo == null ? DateTime.Today : filter.DateTo.Value.Date;
        //    if (DateFrom > DateTo) (DateFrom, DateTo) = (DateTo, DateFrom);
        //    var FiledBy = filter.FiledBy == null ? "everyone" : filter.FiledBy.ToLower();

        //    var now = DateTime.Now;
        //    const double nearDueThreshold = 0.70;

        //    var filteredOrder = from order in context.SalesOrders
        //                        where (filter.AgentId == null || order.AgentId == filter.AgentId)
        //                        && (filter.CustomerId == null || order.CustomerId == filter.CustomerId)
        //                        && (filter.RegionId == null || order.Region.Id == filter.RegionId)
        //                        && DateFrom <= order.SalesOrderDate.Date
        //                        && order.SalesOrderDate.Date <= DateTo
        //                        && (filter.Status == null || filter.Status == order.Status.Id)
        //                        && (filter.FiledBy == null || FiledBy != "me" || (FiledBy == "me" && order.UserCreated == currentUserId))
        //                        select order;

        //    var orderIncompletePack = from soli in context.SalesOrderLineItems
        //                              join pli in context.PackLineItems on new { soli.SalesOrderId, soli.ItemId } equals new { pli.Pack.SalesOrderId, pli.ItemId } into p
        //                              from pli in p.DefaultIfEmpty()
        //                              where soli.Quantity > (pli == null ? 0 : pli.Quantity)
        //                              select soli.SalesOrderId;

        //    var saKpi = from so in context.SalesOrders
        //                join kpi in context.KPIs on so.Id equals kpi.SalesOrderId
        //                join user in users on kpi.UserCreated equals user.Id
        //                where kpi.Department == "SA"
        //                select new { Id = so.Id, Duration = kpi.KpiEnd - kpi.KpiStart, User = user.FirstName + " " + user.LastName };

        //    var opsKpi = from so in context.SalesOrders
        //                 join kpi in context.KPIs on so.Id equals kpi.SalesOrderId
        //                 join user in users on kpi.UserCreated equals user.Id
        //                 where kpi.Department == "OPS"
        //                 select new { Id = so.Id, Duration = kpi.KpiEnd - kpi.KpiStart, User = user.FirstName + " " + user.LastName };


        //    var orderVM = (from order in filteredOrder
        //                   join approvedby in users on order.ApprovedBy equals approvedby.Id into ab
        //                   from approvedby in ab.DefaultIfEmpty()
        //                   join disapprovedby in users on order.DisapprovedBy equals disapprovedby.Id into db
        //                   from disapprovedby in db.DefaultIfEmpty()
        //                   select new SalesOrderVM
        //                   {
        //                       CustomerId = order.Customer.Id,
        //                       CustomerCode = order.Customer.Code,
        //                       CustomerName = order.Customer.Name,
        //                       Id = order.Id,
        //                       OrderDateD = order.DateCreated,
        //                       SONumber = order.SalesOrderNumber,
        //                       Status = order.Status.Code,
        //                       TotalAmountD = order.SalesOrderLineItems.Sum(s =>
        //                                        s.Quantity
        //                                        * s.SellingPrice
        //                                        * (100 - s.Discount1) / 100
        //                                        * (100 - s.Discount2) / 100
        //                                        * (100 - (s.Discount3 == null ? 0 : s.Discount3.Value)) / 100
        //                                        * (100 - (s.Discount4 == null ? 0 : s.Discount4.Value)) / 100
        //                                        * (100 - (s.Discount5 == null ? 0 : s.Discount5.Value)) / 100
        //                                    ) - order.OverallDiscount,
        //                       CustomerAddress = order.Customer
        //                                              .ShippingAddresses
        //                                              .Where(s => s.IsDefault == true)
        //                                              .Select(s => s.Address)
        //                                              .FirstOrDefault() ?? null,
        //                       PriorityD = order.Customer.Priority,
        //                       Region = order.Region.Name,
        //                       Area = order.Customer.Area.Name,
        //                       RequiredDeliveryDateD = order.RequiredDeliveryDate,
        //                       Forwarder = order.Forwarder != null ? order.Forwarder.Name : null,
        //                       ApprovedDisapprovedBy = approvedby != null
        //                                             ? SystemFormat.Name(approvedby.FirstName, approvedby.LastName, approvedby.Nickname)
        //                                             : disapprovedby != null
        //                                             ? SystemFormat.Name(disapprovedby.FirstName, disapprovedby.LastName, disapprovedby.Nickname)
        //                                             : null,
        //                       ApprovedDisapprovedDateD = order.ApprovedDate ?? order.DisapprovedDate,
        //                       IsAllPacked = !orderIncompletePack.Contains(order.Id),
        //                       PurchaseRequestDetail = string.Join(", ", order.PurchaseRequestLineItems
        //                                                                      .Select(s => new
        //                                                                      {
        //                                                                          Id = s.PurchaseRequest.Id,
        //                                                                          Number = s.PurchaseRequest.DocumentNumber,
        //                                                                          Status = s.PurchaseRequest.Status.Name
        //                                                                      })
        //                                                                      .Distinct()
        //                                                                      .Select(s => $"<a href='/PurchaseRequest/Details/{s.Id}' class='link-info' target='_blank'>{s.Number}</a>-[{s.Status}]")),
        //                       ServiceLevelAgreement = order.ServiceLevelAgreement.Name,
        //                       ServiceLevelAgreementHr = order.ServiceLevelAgreement.HourAvailability,
        //                       WithLP = order.WithLP,
        //                       WithREYCONonDefective = order.WithREYCONonDefective,
        //                       WithREYCOJITLPDelivery = order.WithREYCOJITLPDelivery.Name,
        //                       SpecialInstruction = order.SpecialInstruction,
        //                       Remarks = order.Remarks,
        //                       OrderQueueNumber = order.SalesOrderItemSources
        //                                               .Select(s => s.SourceOrderNumber)
        //                                               .FirstOrDefault(),
        //                       TimeRemainingD = order.ServiceLevelAgreement.HourAvailability
        //                           - EF.Functions.DateDiffMinute(order.DateCreated, now) / 60.0,
        //                       ApproveDateD = order.ApprovedDate,
        //                       SaName = saKpi.Where(s => s.Id == order.Id).Select(s => s.User).FirstOrDefault(),
        //                       SaKpi = (saKpi.Where(s => s.Id == order.Id).Select(s => s.Duration).FirstOrDefault() ?? TimeSpan.Zero).ToString(@"hh\:mm\:ss"),
        //                       OpsName = opsKpi.Where(s => s.Id == order.Id).Select(s => s.User).FirstOrDefault(),
        //                       OpsKpi = (opsKpi.Where(s => s.Id == order.Id).Select(s => s.Duration).FirstOrDefault() ?? TimeSpan.Zero).ToString(@"hh\:mm\:ss"),

        //                       //SlaStatus = (order.Status.Id == 2 || order.Status.Id == 3)
        //                       //                         && order.ServiceLevelAgreement.HourAvailability > 0
        //                       //                       ? (EF.Functions.DateDiffMinute(order.DateCreated, now)
        //                       //                             >= order.ServiceLevelAgreement.HourAvailability * 60
        //                       //                             ? "Overdue"
        //                       //                             : EF.Functions.DateDiffMinute(order.DateCreated, now)
        //                       //                                 >= order.ServiceLevelAgreement.HourAvailability * 60 * nearDueThreshold
        //                       //                                 ? "Near Due"
        //                       //                                 : "On Track")
        //                       //                       : null
        //                   });

        //    var result = orderVM.ToList();

        //    foreach (var vm in result)
        //    {
        //        var isSubmitted = vm.Status == "SUBMITTED";
        //        var isApproved = vm.Status == "APPROVED";


        //        if ((!isSubmitted && !isApproved) || vm.ServiceLevelAgreementHr <= 0)
        //        {
        //            vm.SlaStatus = null;
        //            continue;
        //        }

        //        var limitMinutes = vm.ServiceLevelAgreementHr * 60;

        //        if (isApproved && vm.ApproveDateD.HasValue)
        //        {
        //            // SLA clock stops at approval time
        //            var minutesToApprove = BusinessMinutesBetween(vm.OrderDateD, vm.ApproveDateD.Value);

        //            vm.SlaStatus = minutesToApprove <= limitMinutes ? "Done" : "Overdue";

        //            // optional: freeze remaining time at the moment of approval
        //            vm.TimeRemainingD = vm.ServiceLevelAgreementHr - minutesToApprove / 60.0;
        //        }
        //        else
        //        {
        //            // still waiting for approval: the clock is running against now
        //            var elapsedMinutes = BusinessMinutesBetween(vm.OrderDateD, now);

        //            if (elapsedMinutes >= limitMinutes)
        //                vm.SlaStatus = "Overdue";
        //            else if (elapsedMinutes >= limitMinutes * nearDueThreshold)
        //                vm.SlaStatus = "Near Due";
        //            else
        //                vm.SlaStatus = "On Track";
        //        }
        //    }

        //    return result;
        //}

        public async Task<IList<SalesOrderVM>> GetListAsync(SalesOrderFilterVM filter)
        {
            var currentUser = await userManager.GetUserAsync(httpContextAccessor?.HttpContext?.User);
            var filedBy = filter.FiledBy?.ToLower() ?? "everyone";

            var now = DateTime.Now;
            const double nearDueThreshold = 0.70;

            var parameters = new[]
            {
                new SqlParameter("@DateFrom",   (object?)filter.DateFrom?.Date ?? DBNull.Value),
                new SqlParameter("@DateTo",     (object?)filter.DateTo?.Date   ?? DBNull.Value),
                new SqlParameter("@AgentId",    (object?)filter.AgentId    ?? DBNull.Value),
                new SqlParameter("@CustomerId", (object?)filter.CustomerId ?? DBNull.Value),
                new SqlParameter("@RegionId",   (object?)filter.RegionId   ?? DBNull.Value),
                new SqlParameter("@StatusId",   (object?)filter.Status     ?? DBNull.Value),
                new SqlParameter("@FiledByMe",  filedBy == "me"),
                new SqlParameter("@UserId",     currentUser.Id),
            };

            var rows = await context.Set<SalesOrderListRowVM>()
                .FromSqlRaw(@"EXEC dbo.SP_SO_GETLIST @DateFrom, @DateTo, @AgentId, @CustomerId, @RegionId, @StatusId, @FiledByMe, @UserId", parameters)
                .AsNoTracking()
                .ToListAsync();

            var orderVM = rows.Select(r => new SalesOrderVM
            {
                Id = r.Id,
                SONumber = r.SONumber,
                OrderDateD = r.OrderDateD,
                CustomerId = r.CustomerId,
                CustomerCode = r.CustomerCode,
                CustomerName = r.CustomerName,
                PriorityD = r.PriorityD,
                Area = r.Area,
                Region = r.Region,
                Status = r.Status,
                TotalAmountD = r.TotalAmountD ?? 0,
                CustomerAddress = r.CustomerAddress,
                RequiredDeliveryDateD = r.RequiredDeliveryDateD,
                Forwarder = r.Forwarder,
                ApprovedDisapprovedBy = r.ApprovedFirstName != null
                    ? SystemFormat.Name(r.ApprovedFirstName, r.ApprovedLastName, r.ApprovedNickname)
                    : r.DisapprovedFirstName != null
                        ? SystemFormat.Name(r.DisapprovedFirstName, r.DisapprovedLastName, r.DisapprovedNickname)
                        : null,
                ApprovedDisapprovedDateD = r.ApprovedDisapprovedDateD,
                ApproveDateD = r.ApproveDateD,
                IsAllPacked = r.IsAllPacked,
                PurchaseRequestDetail = BuildPurchaseRequestLinks(r.PurchaseRequestRaw),
                ServiceLevelAgreement = r.ServiceLevelAgreement,
                ServiceLevelAgreementHr = r.ServiceLevelAgreementHr ?? 0,
                WithLP = r.WithLP ?? false,
                WithREYCONonDefective = r.WithREYCONonDefective ?? false,
                WithREYCOJITLPDelivery = r.WithREYCOJITLPDelivery,
                SpecialInstruction = r.SpecialInstruction,
                Remarks = r.Remarks,
                OrderQueueNumber = r.OrderQueueNumber,
                SaName = r.SaName,
                SaKpi = r.SaKpi ?? "00:00:00",
                OpsName = r.OpsName,
                OpsKpi = r.OpsKpi ?? "00:00:00",
                // TimeRemainingD / SlaStatus: keep your existing post-processing loop (see business-hours logic)
            }).ToList();

            var result = orderVM.ToList();

            foreach (var vm in result)
            {
                var isSubmitted = vm.Status == "SUBMITTED";
                var isApproved = vm.Status == "APPROVED";


                if ((!isSubmitted && !isApproved) || vm.ServiceLevelAgreementHr <= 0)
                {
                    vm.SlaStatus = null;
                    continue;
                }

                var limitMinutes = vm.ServiceLevelAgreementHr * 60;

                if (isApproved && vm.ApproveDateD.HasValue)
                {
                    // SLA clock stops at approval time
                    var minutesToApprove = BusinessMinutesBetween(vm.OrderDateD, vm.ApproveDateD.Value);

                    vm.SlaStatus = minutesToApprove <= limitMinutes ? "Done" : "Overdue";

                    // optional: freeze remaining time at the moment of approval
                    vm.TimeRemainingD = vm.ServiceLevelAgreementHr - minutesToApprove / 60.0;
                }
                else
                {
                    // still waiting for approval: the clock is running against now
                    var elapsedMinutes = BusinessMinutesBetween(vm.OrderDateD, now);

                    if (elapsedMinutes >= limitMinutes)
                        vm.SlaStatus = "Overdue";
                    else if (elapsedMinutes >= limitMinutes * nearDueThreshold)
                        vm.SlaStatus = "Near Due";
                    else
                        vm.SlaStatus = "On Track";
                }
            }

            return result;
        }

        private static string BuildPurchaseRequestLinks(string raw)
        {
            if (string.IsNullOrEmpty(raw)) return string.Empty;
            return string.Join(", ", raw.Split(';').Select(x =>
            {
                var p = x.Split('|');
                return $"<a href='/PurchaseRequest/Details/{p[0]}' class='link-info' target='_blank'>{p[1]}</a>-[{p[2]}]";
            }));
        }

        public async Task<IList<APISalesOrder>> GetListAPI(DateTime? dt)
        {
            DateTime dtNow = DateTime.Now;
            DateTime dtYesterday = dtNow.AddDays(-1);

            var orders = from order in context.SalesOrders
                         join userc in userManager.Users on order.UserCreated equals userc.Id
                         join userm in userManager.Users on order.UserModified equals userm.Id into um
                         from userm in um.DefaultIfEmpty()
                         where ((dt != null && order.SalesOrderDate.Date == dt.Value.Date)
                         || (dt == null && (order.BarcotechRequestDate == null
                         || order.BarcotechRequestDate > dtYesterday)))
                         && order.ApprovedBy != null
                         && order.ApprovedDate != null
                         select new APISalesOrder
                         {
                             Id = order.Id,
                             AgentId = order.AgentId,
                             AgentCode = order.Agent.Code,
                             CustomerId = order.CustomerId,
                             CustomerCode = order.Customer.Code,
                             CustomerName = order.Customer.Name,
                             SalesOrderNumber = order.SalesOrderNumber,
                             OrderDate = order.SalesOrderDate,
                             UserCreated = userc.FirstName + " " + userc.LastName,
                             DateCreated = order.DateCreated,
                             LastUserModified = userm == null ? null : userm.FirstName + " " + userm.LastName,
                             LastDateModified = order.DateModified,
                             ServiceLevelAgreement = order.ServiceLevelAgreementId == null ? null : order.ServiceLevelAgreement.Name,
                             ServiceLevelAgreementId = order.ServiceLevelAgreementId == null ? null : order.ServiceLevelAgreement.Id,
                             SpecialInstruction = order.SpecialInstruction,
                             LineItems = from item in order.SalesOrderLineItems
                                         select new APISalesOrderLineItem
                                         {
                                             Id = item.Id,
                                             LineNumber = item.LineNumber,
                                             ItemCode = item.Item.Code,
                                             ItemId = item.Item.Id,
                                             Quantity = item.Quantity
                                         }
                         };
            var resultOrders = await orders.ToListAsync();
            foreach (var order in resultOrders)
            {
                var retrievedOrder = context.SalesOrders.Where(s => s.Id == order.Id).First();
                if (retrievedOrder.BarcotechRequestDate == null)
                {
                    retrievedOrder.BarcotechRequestDate = dtNow;
                    context.SaveChanges();
                }
            }

            return resultOrders;
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

        public async Task<IList<SalesOrderItemVM>> GetItemsAsync(int salesOrderId, string? caller)
        {
            var itemVM = await (from order in context.SalesOrderLineItems
                                                     .Include(s => s.Item)
                                                     .ThenInclude(s => s.Unit)

                                from promo in context.ItemPromo
                                                    .Where(pr => pr.Id == order.ItemPromoId)
                                                    .OrderByDescending(pr => pr.DateCreated)
                                                    .Take(1)
                                                    .DefaultIfEmpty()
                                let items = context.ItemInventories
                                                   .Where(s => s.ItemId == order.ItemId && s.Warehouse.IsMain == true && s.BinLocation != null && s.Quantity != 0)
                                                   .Select(s => new
                                                   {
                                                       BinLocation = s.BinLocation
                                                   })
                                                   .FirstOrDefault()
                                let price = context.ItemPrices
                                                   .Where(s => s.ItemId == order.ItemId
                                                            && s.DateCreated <= order.SalesOrder.ApprovedDate)
                                                   .OrderByDescending(s => s.DateCreated)
                                                   .FirstOrDefault()
                                let inventory = context.ItemInventories
                                                       .Where(s => s.ItemId == order.ItemId && s.Warehouse.IsMain == true)
                                                       .Sum(s => (int?)s.Quantity) ?? 0
                                let inventoryAllocated = context.ItemInventories
                                                                .Where(s => s.ItemId == order.ItemId && s.Warehouse.IsMain == true)
                                                                .Sum(s => (int?)s.AllocatedQuantity) ?? 0
                                let packedQuantity = context.PackLineItems
                                                            .Where(s => s.Pack.SalesOrderId == order.SalesOrderId
                                                                     && s.ItemId == order.ItemId
                                                                     && s.Pack.Status.Code.Trim().ToLower() == "posted")
                                                            .Sum(s => (int?)s.Quantity) ?? 0
                                let invoicedQuantity = context.InvoiceLineItems
                                                              .Where(s => s.Invoice.SalesOrderId == order.SalesOrderId
                                                                       && s.ItemId == order.ItemId)
                                                              .Sum(s => (int?)s.Quantity) ?? 0
                                let returnedQuantity = context.SalesReturnLineItems
                                                              .Where(s => s.InvoiceLineItem!.Invoice.SalesOrderId == order.SalesOrderId
                                                                       && s.ItemId == order.ItemId)
                                                              .Sum(s => (int?)s.Quantity) ?? 0
                                where order.SalesOrderId == salesOrderId && (caller != "PACK" || order.SalesOrder.Status.Code.ToLower() != "force close")
                                orderby order.LineNumber ascending
                                select new SalesOrderItemVM
                                {
                                    AllocatedQuantityD = inventoryAllocated,
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
                                    ReturnedQuantityD = returnedQuantity,
                                    RemainingQuantityD = order.Quantity - packedQuantity,
                                    RetailPriceD = price == null ? 0 : price.RetailSellingPrice,
                                    SellingPriceD = order.SellingPrice,
                                    Unit = order.Item.Unit.Code,
                                    ItemPromoId = order.ItemPromoId,
                                    IsPromo = order.IsPromo,
                                    BinLocation = items.BinLocation,

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
            var data = (await GetItemsAsync(salesOrderId, null)).AsQueryable();
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

        //public async Task<ResponseModel<int>> SaveAsync(SalesOrderFormVM form)
        //{
        //    //try
        //    //{
        //    string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

        //    var agent = await context.Agents.Where(s => s.Id == form.Agent).FirstOrDefaultAsync();
        //    if (agent == null) return new ResponseModel<int> { Status = "Error", Message = "Agent is invalid" };

        //    var customer = await context.Customers.Where(s => s.Id == form.Customer).FirstOrDefaultAsync();
        //    if (customer == null) return new ResponseModel<int> { Status = "Error", Message = "Customer is invalid" };

        //    var forwarder = await context.Forwarders.Where(s => s.Id == form.Forwarder).FirstOrDefaultAsync();
        //    if (forwarder == null) return new ResponseModel<int> { Status = "Error", Message = "Forwarder is invalid" };

        //    var region = await context.Regions.Where(s => s.Id == form.Region).FirstOrDefaultAsync();
        //    if (region == null) return new ResponseModel<int> { Status = "Error", Message = "Region is invalid" };

        //    var sla = await context.ServiceLevelAgreements.Where(s => s.Id == form.SericeLevelAgreement).FirstOrDefaultAsync();
        //    if (sla == null) return new ResponseModel<int> { Status = "Error", Message = "Service Level Agreement is invalid" };


        //    var orderVM = await context.SalesOrders.Where(s => s.Id == form.Id).FirstOrDefaultAsync();

        //    int statusId = context.Statuses
        //                          .Where(s => (form.Status.ToLower().Contains("submit") && s.Name.ToLower() == "sales order submitted")
        //                                   || (form.Status.ToLower().Contains("draft") && s.Name.ToLower() == "sales order draft"))
        //                          .Select(s => s.Id)
        //                          .First();
        //    if (orderVM == null)
        //    {

        //        var previous = await context.SalesOrders.Where(s => s.Id == form.ResubmitId).FirstOrDefaultAsync();
        //        string soNumber = "";
        //        string soResubbmitedBy = "";
        //        DateTime? soResubbmitedDate = null;

        //        if (previous == null)
        //        {
        //            //soNumber = SONumber();
        //            var warehouse = context.Warehouses
        //                                  .Where(s => s.IsMain == true).FirstOrDefault();


        //            var latestOrder = context.SalesOrders
        //                     .OrderByDescending(s => s.Id)
        //                     .Select(s => s.SalesOrderNumber)
        //                     .FirstOrDefault();

        //            if (latestOrder != null && latestOrder.Contains('.'))
        //            {
        //                var splitLatestOrder = latestOrder.Split('.');
        //                latestOrder = splitLatestOrder[0];
        //            }

        //            if (latestOrder != null && latestOrder.Contains('-'))
        //            {
        //                var splitLatestOrder = latestOrder.Split("-");
        //                latestOrder = splitLatestOrder[0];
        //            }

        //            // var latestOrderNumber = latestOrder == null ? 0 : int.Parse(latestOrder.Replace("SO", ""));



        //            int? latestOrderNumber = 0;

        //            if (latestOrder != null)
        //            {
        //                if (latestOrder.Contains("SO"))
        //                {
        //                    latestOrderNumber = int.Parse(latestOrder.Replace("SO", ""));
        //                }
        //                else if (latestOrder.Contains("PSG1"))
        //                {
        //                    latestOrderNumber = int.Parse(latestOrder.Replace("PSG1", ""));
        //                }
        //                else if (latestOrder.Contains("PSG2"))
        //                {
        //                    latestOrderNumber = int.Parse(latestOrder.Replace("PSG2", ""));
        //                }
        //                else if (latestOrder.Contains("CT38"))
        //                {
        //                    latestOrderNumber = int.Parse(latestOrder.Replace("CT38", ""));
        //                }
        //                else if (latestOrder.Contains("SEV"))
        //                {
        //                    latestOrderNumber = int.Parse(latestOrder.Replace("SEV", ""));
        //                }
        //            }


        //            if (warehouse != null && latestOrder != null)
        //            {
        //                if (warehouse.Name == WareHouseName.PASIG1.ToString())
        //                {
        //                    soNumber = string.Format("{0:PSG1000000}", latestOrderNumber + 1);
        //                }
        //                else if (warehouse.Name == WareHouseName.PASIG2.ToString())
        //                {
        //                    soNumber = string.Format("{0:PSG2000000}", latestOrderNumber + 1);
        //                }
        //                else if (warehouse.Name == WareHouseName.CETACIO.ToString())
        //                {
        //                    soNumber = string.Format("{0:CT38000000}", latestOrderNumber + 1);
        //                }
        //                else if (warehouse.Name == WareHouseName.SEVERINO.ToString())
        //                {
        //                    soNumber = string.Format("{0:SEV0000000}", latestOrderNumber + 1);
        //                }
        //            }

        //            var hasExisting = await context.SalesOrders.Where(s => s.SalesOrderNumber == soNumber).FirstOrDefaultAsync();

        //        }
        //        else if (!previous.SalesOrderNumber.Contains("-D"))
        //        {
        //            return new ResponseModel<int> { Status = "Error", Message = "Invalid resubmission. The previous sales order number is required to be in the valid format of a deactivated sales order" };
        //        }
        //        else
        //        {
        //            var tempSalesOrderNumber = previous.SalesOrderNumber.Split("-D");
        //            soNumber = tempSalesOrderNumber[0];
        //            soResubbmitedBy = currentUserId;
        //            soResubbmitedDate = DateTime.UtcNow;
        //        }


        //        //if (form.Status.ToLower().Contains("draft")
        //        // && await context.SalesOrders.AnyAsync(s => s.StatusId == statusId && s.SalesOrderNumber == soNumber))
        //        //{
        //        //    return new ResponseModel<int> { Status = "Error", Message = "Failed to add. A draft SO with the same SO Number already exists" };
        //        //}
        //        var temp = await context.SalesOrders.Where(s => s.SalesOrderNumber == soNumber && (form.ResubmitId == null || s.Id != form.ResubmitId)).ToListAsync();
        //        //if (await context.SalesOrders.AnyAsync(s => s.SalesOrderNumber == soNumber && (form.ResubmitId == null || s.Id != form.ResubmitId)))
        //        if (await context.SalesOrders.AnyAsync(s => s.SalesOrderNumber == soNumber))
        //            return new ResponseModel<int> { Status = "Error", Message = "Generated a conflicting Sales Order Number, Please try again. If error persist, consult IT" };

        //        orderVM = new SalesOrder
        //        {
        //            AgentId = agent.Id,
        //            CustomerId = customer.Id,
        //            RequiredDeliveryDate = form.DeliveryDate,
        //            PurchaseOrder = form.PurchaseOrder,
        //            Remarks = form.Remarks,
        //            SalesOrderDate = form.OrderDate,
        //            SalesOrderNumber = soNumber,
        //            SubmittedBy = currentUserId,
        //            SubmittedDate = form.OrderDate,
        //            ResubmittedBy = soResubbmitedBy,
        //            ResubmittedDate = soResubbmitedDate,
        //            StatusId = statusId,
        //            ForwarderId = form.Forwarder,
        //            RegionId = form.Region,
        //            ServiceLevelAgreementId = form.SericeLevelAgreement,
        //            OverallDiscount = form.OverallDiscount,
        //            WithLP = form.WithLP,
        //            WithREYCONonDefective = form.WithREYCONonDefective,
        //            WithREYCOJITLPDeliveryId = form.WithREYCOJITLPDelivery,
        //            SpecialInstruction = form.SpecialInstruction
        //        };
        //        await context.SalesOrders.AddAsync(orderVM);
        //        await context.SaveChangesAsync(currentUserId);

        //        if (previous != null)
        //        {
        //            context.SalesOrders.Remove(previous);

        //            previous.ResubmitId = orderVM.Id;
        //            previous.ResubmittedBy = currentUserId;
        //            previous.ResubmittedDate = DateTime.UtcNow;

        //            context.SalesOrders.Update(previous);
        //            await context.SaveChangesAsync(currentUserId);
        //        }
        //    }
        //    else
        //    {
        //        orderVM.AgentId = agent.Id;
        //        orderVM.CustomerId = customer.Id;
        //        orderVM.RequiredDeliveryDate = form.DeliveryDate;
        //        orderVM.Remarks = form.Remarks;
        //        orderVM.SalesOrderDate = form.OrderDate;
        //        orderVM.StatusId = statusId;
        //        orderVM.ForwarderId = form.Forwarder;
        //        orderVM.RegionId = form.Region;
        //        orderVM.ServiceLevelAgreementId = form.SericeLevelAgreement;
        //        orderVM.OverallDiscount = form.OverallDiscount;
        //        orderVM.WithLP = form.WithLP;
        //        orderVM.WithREYCONonDefective = form.WithREYCONonDefective;
        //        orderVM.WithREYCOJITLPDeliveryId = form.WithREYCOJITLPDelivery;
        //        orderVM.SpecialInstruction = form.SpecialInstruction;
        //        orderVM.SpecialInstruction = form.SpecialInstruction;

        //        context.SalesOrders.Update(orderVM);
        //        await context.SaveChangesAsync(currentUserId);
        //    }

        //    await SaveItemsAsync(form.Items, orderVM.Id);

        //    await SaveKpi(orderVM.Id, currentUserId, statusId);

        //    return new ResponseModel<int>
        //    {
        //        Status = "Success",
        //        Message = form.Status.ToLower() == "draft" ? "Sales order saved as draft." : "Sales order successfully submitted.",
        //        NewRow = orderVM.Id
        //    };
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    return new ResponseModel<int> { Status = "Error", Message = SystemFormat.GetInnermostExceptionMessage(ex) };
        //    //}
        //}

        public async Task<ResponseModel<int>> SaveAsync(SalesOrderFormVM form)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

            // ---------- Validation (unchanged) ----------
            var agent = await context.Agents.Where(s => s.Id == form.Agent).FirstOrDefaultAsync();
            if (agent == null) return new ResponseModel<int> { Status = "Error", Message = "Agent is invalid" };

            var customer = await context.Customers.Where(s => s.Id == form.Customer).FirstOrDefaultAsync();
            if (customer == null) return new ResponseModel<int> { Status = "Error", Message = "Customer is invalid" };

            var forwarder = await context.Forwarders.Where(s => s.Id == form.Forwarder).FirstOrDefaultAsync();
            if (forwarder == null) return new ResponseModel<int> { Status = "Error", Message = "Forwarder is invalid" };

            var region = await context.Regions.Where(s => s.Id == form.Region).FirstOrDefaultAsync();
            if (region == null) return new ResponseModel<int> { Status = "Error", Message = "Region is invalid" };

            var sla = await context.ServiceLevelAgreements.Where(s => s.Id == form.SericeLevelAgreement).FirstOrDefaultAsync();
            if (sla == null) return new ResponseModel<int> { Status = "Error", Message = "Service Level Agreement is invalid" };

            // ---------- Everything that writes runs in ONE transaction ----------
            var strategy = context.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                context.ChangeTracker.Clear(); // clean slate if the strategy retries

                await using var tx = await context.Database.BeginTransactionAsync();

                var orderVM = await context.SalesOrders.Where(s => s.Id == form.Id).FirstOrDefaultAsync();

                int statusId = await context.Statuses
                                      .Where(s => (form.Status.ToLower().Contains("submit") && s.Name.ToLower() == "sales order submitted")
                                               || (form.Status.ToLower().Contains("draft") && s.Name.ToLower() == "sales order draft"))
                                      .Select(s => s.Id)
                                      .FirstAsync();

                if (orderVM == null)
                {
                    var previous = await context.SalesOrders.Where(s => s.Id == form.ResubmitId).FirstOrDefaultAsync();
                    string soNumber;
                    string soResubbmitedBy = "";
                    DateTime? soResubbmitedDate = null;

                    if (previous == null)
                    {
                        // NEW SO: next number comes from the counter table
                        var (success, number, error) = await GenerateNewSoNumberAsync(currentUserId);
                        if (!success)
                            return new ResponseModel<int> { Status = "Error", Message = error };

                        soNumber = number;
                    }
                    else if (!previous.SalesOrderNumber.Contains("-D"))
                    {
                        return new ResponseModel<int> { Status = "Error", Message = "Invalid resubmission. The previous sales order number is required to be in the valid format of a deactivated sales order" };
                    }
                    else
                    {
                        var tempSalesOrderNumber = previous.SalesOrderNumber.Split("-D");
                        soNumber = tempSalesOrderNumber[0];
                        soResubbmitedBy = currentUserId;
                        soResubbmitedDate = DateTime.UtcNow;
                    }

                    // Safety net (the unique index is the real guard)
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
                        SubmittedBy = currentUserId,
                        SubmittedDate = form.OrderDate,
                        ResubmittedBy = soResubbmitedBy,
                        ResubmittedDate = soResubbmitedDate,
                        StatusId = statusId,
                        ForwarderId = form.Forwarder,
                        RegionId = form.Region,
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
                        context.SalesOrders.Remove(previous);

                        previous.ResubmitId = orderVM.Id;
                        previous.ResubmittedBy = currentUserId;
                        previous.ResubmittedDate = DateTime.UtcNow;

                        context.SalesOrders.Update(previous);
                        await context.SaveChangesAsync(currentUserId);
                    }
                }
                else
                {
                    // EDIT existing SO: no new number needed
                    orderVM.AgentId = agent.Id;
                    orderVM.CustomerId = customer.Id;
                    orderVM.RequiredDeliveryDate = form.DeliveryDate;
                    orderVM.Remarks = form.Remarks;
                    orderVM.SalesOrderDate = form.OrderDate;
                    orderVM.StatusId = statusId;
                    orderVM.ForwarderId = form.Forwarder;
                    orderVM.RegionId = form.Region;
                    orderVM.ServiceLevelAgreementId = form.SericeLevelAgreement;
                    orderVM.OverallDiscount = form.OverallDiscount;
                    orderVM.WithLP = form.WithLP;
                    orderVM.WithREYCONonDefective = form.WithREYCONonDefective;
                    orderVM.WithREYCOJITLPDeliveryId = form.WithREYCOJITLPDelivery;
                    orderVM.SpecialInstruction = form.SpecialInstruction;

                    context.SalesOrders.Update(orderVM);
                    await context.SaveChangesAsync(currentUserId);
                }

                await SaveItemsAsync(form.Items, orderVM.Id);

                await SaveKpi(orderVM.Id, currentUserId, statusId);

                await tx.CommitAsync();

                return new ResponseModel<int>
                {
                    Status = "Success",
                    Message = form.Status.ToLower() == "draft" ? "Sales order saved as draft." : "Sales order successfully submitted.",
                    NewRow = orderVM.Id
                };
            });
        }

        private async Task<(bool Success, string Number, string Error)> GenerateNewSoNumberAsync(string currentUserId)
        {
            var warehouse = await context.Warehouses.FirstOrDefaultAsync(s => s.IsMain == true);

            var (prefix, digits) = warehouse?.Name switch
            {
                nameof(WareHouseName.PASIG1) => ("PSG1", 6),
                nameof(WareHouseName.PASIG2) => ("PSG2", 6),
                nameof(WareHouseName.CETACIO) => ("CT38", 6),
                nameof(WareHouseName.SEVERINO) => ("SEV", 7),
                _ => ((string?)null, 0)
            };

            if (prefix == null)
                return (false, "", "Main warehouse is not configured.");

            // UPDLOCK + ROWLOCK: the counter row stays locked until the surrounding transaction
            // commits or rolls back, so no other request can read the same value.
            var counter = await context.SalesOrderCounters
                .FromSqlInterpolated($@"
                    SELECT * FROM dbo.SalesOrderCounters WITH (UPDLOCK, ROWLOCK)
                    WHERE Prefix = {prefix}")
                    .FirstOrDefaultAsync();

            if (counter == null)
                return (false, "", $"No counter row found for prefix '{prefix}'. Seed dbo.SalesOrderCounters first.");

            counter.LastNumber++;
            await context.SaveChangesAsync(currentUserId);   // use SaveChangesAsync() if your overload needs audited entities

            return (true, prefix + counter.LastNumber.ToString().PadLeft(digits, '0'), "");
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
                                   ItemPromoId = null,
                                   IsPromo = s.isPromo

                               });
            context.SalesOrderLineItems.AddRange(itemsVM);

            // update item
            // get items available in db
            var ditems = Items.Where(s => items.Contains(s.Item));

            var dbItems = await context.SalesOrderLineItems
                        .Where(s => s.SalesOrderId == salesOrderId)
                        .ToListAsync();

            foreach (var di in ditems)
            {
                var dbitem = await citemsVM
                    .Where(s => s.ItemId == di.Item && s.IsPromo == di.isPromo)
                    .FirstOrDefaultAsync();

                if (dbitem == null)
                {
                    // new item: doesn't exist in the old details yet
                    dbitem = new SalesOrderLineItem
                    {
                        // SalesOrderId = order.Id,   // set your parent key here
                        SalesOrderId = salesOrderId,
                        ItemId = di.Item,
                        LineNumber = di.LineNumber,
                        Discount1 = di.Discount1,
                        Discount2 = di.Discount2,
                        Discount3 = di.Discount3,
                        Discount4 = di.Discount4,
                        Discount5 = di.Discount5,
                        Quantity = di.Quantity,
                        SellingPrice = di.UnitPrice,
                        CustomerBrandDiscountId = di.CustomerBrandDiscountId,
                        ItemPromoId = null,
                        IsPromo = di.isPromo
                    };
                    context.SalesOrderLineItems.Add(dbitem);
                }
                else
                {
                    // existing item: update its values
                    dbitem.LineNumber = di.LineNumber;
                    dbitem.Discount1 = di.Discount1;
                    dbitem.Discount2 = di.Discount2;
                    dbitem.Discount3 = di.Discount3;
                    dbitem.Discount4 = di.Discount4;
                    dbitem.Discount5 = di.Discount5;
                    dbitem.Quantity = di.Quantity;
                    dbitem.SellingPrice = di.UnitPrice;
                    dbitem.CustomerBrandDiscountId = di.CustomerBrandDiscountId;
                    dbitem.ItemPromoId = null;
                    dbitem.IsPromo = di.isPromo;
                    context.SalesOrderLineItems.Update(dbitem);
                }
            }

            // delete item
            // get items not in form
            //var fitems = Items.Select(s => s.Item);
            //var ritems = citemsVM.Where(s => fitems.Contains(s.ItemId) == false);
            // remove unavailable items
            var toRemove = dbItems
                .Where(d => !Items.Any(f => f.Item == d.ItemId && f.isPromo == d.IsPromo))
                .ToList();

            context.SalesOrderLineItems.RemoveRange(toRemove);

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

            if (order.Status.Name.ToLower() != "sales order draft") return new ResponseModel { Status = "Error", Message = "Only draft can be deleted." };

            var pack = await context.Packs
                                    .Where(s => s.SalesOrderId == Id)
                                    .FirstOrDefaultAsync();
            if (pack != null) return new ResponseModel { Status = "Error", Message = "Sales Order already used in Pack. Delete cannot proceed" };

            var items = await context.SalesOrderLineItems
                                     .Where(s => s.SalesOrderId == Id)
                                     .ToListAsync();

            context.SalesOrderLineItems.RemoveRange(items);

            var source = await context.SalesOrderItemSources
                                      .Where(s => s.SalesOrderId == Id)
                                      .FirstOrDefaultAsync();
            if (source != null)
                context.SalesOrderItemSources.Remove(source);

            context.SalesOrders.Remove(order);

            await context.SaveChangesAsync(currentUserId);

            return new ResponseModel { Status = "Success", Message = "Successfully deleted sales order" };
        }

        public async Task<bool> ForceClose(int Id)
        {
            var orderVM = context.SalesOrders.Where(s => s.Id == Id).FirstOrDefault();
            if (orderVM == null)
            {
                return false;
            }
            else
            {
                var tempSalesOrderNumber = orderVM.SalesOrderNumber + "-F";
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
                int statusId = await context.Statuses.Where(s => s.Name.ToLower() == "force close").Select(s => s.Id).FirstAsync();
                orderVM.SalesOrderNumber = disapprovedOrderNumber;
                orderVM.StatusId = statusId;

                context.SalesOrders.Update(orderVM);
                await context.SaveChangesAsync(currentUserId);


                return true;
            }
        }

        public async Task<bool> ApproveAsync(int salesOrderId, string slaReason)
        {
            var orderVM = context.SalesOrders.Where(s => s.Id == salesOrderId).FirstOrDefault();

            var kpi = context.KPIs.Where(a => a.SalesOrderId == salesOrderId).FirstOrDefault();

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
                orderVM.SlaReason = slaReason;

                context.SalesOrders.Update(orderVM);

                if (kpi != null)
                {
                    var kpis = new KPI
                    {
                        SalesOrderId = kpi.SalesOrderId,
                        UserCreated = currentUserId,
                        KpiStart = kpi.KpiEnd,
                        KpiEnd = DateTime.Now,
                        Department = "OPS"
                    };

                    await context.KPIs.AddAsync(kpis);
                }
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
                var inventoryQuantity = 0;
                if (inventory == null)
                {
                    return false;
                }
                else if (inventory.Quantity >= quantity)
                {
                    inventoryQuantity = inventory.Quantity;
                    inventory.Quantity -= quantity;
                }
                else
                {
                    inventory.Quantity = 0;
                }
                context.SalesOrderLineItems.Update(soItem);
                context.ItemInventories.Update(inventory);
                await context.SaveChangesAsync(currentUserId);

                var countHistoryDestination = new PhysicalCountHistory
                {
                    ItemId = soItem.ItemId,
                    QuantityFrom = inventoryQuantity,
                    QuantityTo = inventory.Quantity,
                    Remarks = "Update Served SO Item",
                    WarehouseId = inventory.WarehouseId
                };
                context.PhysicalCountHistories.Add(countHistoryDestination);
                await context.SaveChangesAsync(currentUserId);
                return true;
            }
        }

        public async Task SaveKpi(int Id, string userId, int statusId)
        {
            var kpi = await context.KPIs.Where(s => s.SalesOrderId == Id).FirstOrDefaultAsync();

            if (kpi == null)
            {

                var kpis = new KPI
                {
                    SalesOrderId = Id,
                    UserCreated = userId,
                    KpiStart = DateTime.Now,
                    KpiEnd = statusId == 1 ? DateTime.Parse("1900-01-01") : DateTime.Now,
                    Department = "SA"
                };
                await context.KPIs.AddAsync(kpis);
                await context.SaveChangesAsync(userId);
            }
            else
            {
                kpi.KpiEnd = DateTime.Now;

                context.KPIs.Update(kpi);
                await context.SaveChangesAsync(userId);
            }
        }

        public async Task<ResponseModel> UpdatePod(int Id)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

            var order = await context.SalesOrders
                         .Include(s => s.Status)
                         .FirstOrDefaultAsync(s => s.Id == Id); ;
            if (order == null)
                return new ResponseModel { Status = "Error", Message = "Sales order not found" };

            var pack = await context.Packs
                           .FirstOrDefaultAsync(s => s.SalesOrderId == Id);

            if (pack == null)
                return new ResponseModel { Status = "Error", Message = "Pack not found" };

            var delivery = new DeliveryMonitoringFormVM
            {
                SalesOrderId = order.Id,
                DeliveryDate = order.RequiredDeliveryDate,
                ReceiveDate = DateTime.Now,
                DeliveryMethodId = 1,
                ForwarderId = order.ForwarderId,
                PackId = pack.Id,
                DeliveryStatusId = 3,
            };

            var result = await _deliveryMonitoringRepository.SaveDeliveryAsync(delivery);
            if (result.Status != "Success")
                return result;

            return new ResponseModel { Status = "Success", Message = "Successfully updated proof of order" };
        }

        public double BusinessMinutesBetween(DateTime start, DateTime end)
        {
            var workStart = new TimeSpan(8, 0, 0);
            var workEnd = new TimeSpan(17, 0, 0);

            if (end <= start) return 0;

            double total = 0;

            for (var day = start.Date; day <= end.Date; day = day.AddDays(1))
            {
                if (day.DayOfWeek == DayOfWeek.Sunday) continue;

                var windowStart = day + workStart;
                var windowEnd = day + workEnd;

                var s = start > windowStart ? start : windowStart;
                var e = end < windowEnd ? end : windowEnd;

                if (e > s) total += (e - s).TotalMinutes;
            }

            return total;
        }

        //public string SONumber()
        //{
        //    var warehouse = context.Warehouses
        //                                   .Where(s => s.IsMain == true).FirstOrDefault();
        //    if (warehouse == null)
        //        throw new InvalidOperationException("No main warehouse configured.");

        //    string prefix;
        //    int width;

        //    if (warehouse.Name == WareHouseName.PASIG1.ToString()) { prefix = "PSG1"; width = 6; }
        //    else if (warehouse.Name == WareHouseName.PASIG2.ToString()) { prefix = "PSG2"; width = 6; }
        //    else if (warehouse.Name == WareHouseName.CETACIO.ToString()) { prefix = "CT38"; width = 6; }
        //    else if (warehouse.Name == WareHouseName.SEVERINO.ToString()) { prefix = "SEV"; width = 7; }
        //    else throw new InvalidOperationException($"Unknown warehouse '{warehouse.Name}'.");

        //    var latest = context.SalesOrders
        //                        .Where(s => s.SalesOrderNumber.StartsWith(prefix))
        //                        .OrderByDescending(s => s.Id)
        //                        .Select(s => s.SalesOrderNumber)
        //                        .FirstOrDefault();

        //    int next = 1;
        //    if (latest != null)
        //    {
        //        // strip suffix like ".1" or "-A"
        //        var end = latest.IndexOfAny(new[] { '.', '-' });
        //        var core = end >= 0 ? latest[..end] : latest;

        //        if (int.TryParse(core.Substring(prefix.Length), out var n))
        //            next = n + 1;
        //    }

        //    return $"{prefix}{next.ToString().PadLeft(width, '0')}";
        //}

    }
}