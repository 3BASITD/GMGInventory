using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Application.Repositories
{
    public class SoacrRepository : ISoacrRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly string currentUserId;

        public SoacrRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
            , UserManager<User> userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
        }


        #region for SOACR/Index
        private async Task<IList<SOACRVM>> SOACRList()
        {
            try
            {
                var soacr = from soa in context.SOACRs
                            select new SOACRVM
                            {
                                Id = soa.Id,
                                Customer = soa.Customer.Name,
                                Type = soa.SOACRType.Code,
                                SOAFromD = soa.SOAFrom,
                                SOAToD = soa.SOATo
                            };
                var soacrl = await soacr.ToListAsync();
                foreach (var scl in soacrl)
                {
                    var soadetails = await GetSOACRItemsAsync(scl.Id);
                    scl.TotalAmountD = soadetails.Sum(s => s.InvoiceAmountD);
                    scl.TBRD = soadetails.Sum(s => s.TBRD);
                    scl.CreditD = soadetails.Sum(s => s.CreditD);
                    scl.DebitD = soadetails.Sum(s => s.DebitD);
                    scl.PaidAmountD = soadetails.Sum(s => s.PaidAmountD);
                    scl.FreightChargeD = soadetails.Sum(s => s.FreightChargeD);
                    scl.BalanceD = soadetails.Sum(s => s.BalanceD);
                    scl.OverPaymentD = soadetails.Sum(s => s.OverPaymentD);
                }
                return soacrl;
            }
            catch (Exception ex)
            {
                return [
                    new() {
                        Id = -1,
                        Customer = ex.Message
                    }
                ];
            }
        }

        public async Task<ForDataTables<SOACRVM>> SOACRListDT(DataTablesRequestForm form)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await SOACRList()).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Id.ToString().Contains(form.SearchValue)
                                    || x.Customer.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.Type.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.SOAFrom.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.SOATo.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.TotalAmountD.ToString().Contains(form.SearchValue)
                                    || x.TBRD.ToString().Contains(form.SearchValue)
                                    || x.CreditD.ToString().Contains(form.SearchValue)
                                    || x.DebitD.ToString().Contains(form.SearchValue)
                                    || x.PaidAmountD.ToString().Contains(form.SearchValue)
                                    || x.FreightChargeD.ToString().Contains(form.SearchValue)
                                    || x.BalanceD.ToString().Contains(form.SearchValue)
                                    || x.OverPaymentD.ToString().Contains(form.SearchValue));
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
                        case "Id": data = data.OrderBy(s => s.Id); break;
                        case "Customer": data = data.OrderBy(s => s.Customer); break;
                        case "Type": data = data.OrderBy(s => s.Type); break;
                        case "SOAFrom": data = data.OrderBy(s => s.SOAFromD); break;
                        case "SOATo": data = data.OrderBy(s => s.SOAToD); break;
                        case "TotalAmount": data = data.OrderBy(s => s.TotalAmountD); break;
                        case "TBR": data = data.OrderBy(s => s.TBRD); break;
                        case "Credit": data = data.OrderBy(s => s.CreditD); break;
                        case "Debit": data = data.OrderBy(s => s.DebitD); break;
                        case "PaidAmount": data = data.OrderBy(s => s.PaidAmountD); break;
                        case "FreightCharge": data = data.OrderBy(s => s.FreightChargeD); break;
                        case "Balance": data = data.OrderBy(s => s.BalanceD); break;
                        case "OverPayment": data = data.OrderBy(s => s.OverPaymentD); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Id": data = data.OrderByDescending(s => s.Id); break;
                        case "Customer": data = data.OrderByDescending(s => s.Customer); break;
                        case "Type": data = data.OrderByDescending(s => s.Type); break;
                        case "SOAFrom": data = data.OrderByDescending(s => s.SOAFromD); break;
                        case "SOATo": data = data.OrderByDescending(s => s.SOAToD); break;
                        case "TotalAmount": data = data.OrderByDescending(s => s.TotalAmountD); break;
                        case "TBR": data = data.OrderByDescending(s => s.TBRD); break;
                        case "Credit": data = data.OrderByDescending(s => s.CreditD); break;
                        case "Debit": data = data.OrderByDescending(s => s.DebitD); break;
                        case "PaidAmount": data = data.OrderByDescending(s => s.PaidAmountD); break;
                        case "FreightCharge": data = data.OrderByDescending(s => s.FreightChargeD); break;
                        case "Balance": data = data.OrderByDescending(s => s.BalanceD); break;
                        case "OverPayment": data = data.OrderByDescending(s => s.OverPaymentD); break;
                        default: break;
                    }
                }
            }
            //pagination
            //var pageList = data.ToList(); // for testing

            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<SOACRVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public async Task<int> GenerateSOAAsync(GenerateSOAVM soaVM)
        {
            var soaType = await context.SOACRTypes.Where(s => s.Name.ToLower() == "statement of account").Select(s => s.Id).FirstAsync();

            var invoiceInSOACR = from sl in context.SOACRLineItems
                                 select sl.InvoiceId;

            var invoices = await (from invoice in context.Invoices
                                  where invoice.SalesOrder.Customer.Id == soaVM.Customer
                                  && !invoiceInSOACR.Contains(invoice.Id)
                                  && soaVM.DateFrom <= invoice.InvoiceDate.Date
                                  && invoice.InvoiceDate.Date <= soaVM.DateTo
                                  select invoice).ToListAsync();

            if (invoices.Count == 0)
            {
                return 0;
            }
            var soa = new SOACR
            {
                CustomerId = soaVM.Customer,
                SOACRTypeId = soaType,
                SOAFrom = soaVM.DateFrom,
                SOATo = soaVM.DateTo
            };
            await context.SOACRs.AddAsync(soa);
            await context.SaveChangesAsync(currentUserId);

            foreach (var invoice in invoices)
            {
                var inv = new SOACRLineItem
                {
                    SOACRId = soa.Id,
                    InvoiceId = invoice.Id,
                    CreditAmount = 0,
                    DebitAmount = 0,
                    FreightCost = 0
                };
                await context.SOACRLineItems.AddAsync(inv);
                await context.SaveChangesAsync(currentUserId);
            }

            return soa.Id;
        }

        #endregion

        #region for SOACR/Invoice
        private async Task<IList<SOACRInvoices>> GetInvoiceListAsync(ListFilterSalesModel filter)
        {
            var invoices = from inv in context.Invoices
                           where
                           (filter.Customer == null || filter.Customer == inv.SalesOrder.Customer.Id)
                              && (filter.Agent == null || filter.Agent == inv.SalesOrder.Agent.Id)
                              && filter.DateFrom <= inv.InvoiceDate.Date
                              && inv.InvoiceDate.Date <= filter.DateTo

                           select new SOACRInvoices
                           {
                               Agent = inv.SalesOrder.Agent.FirstName + " " + inv.SalesOrder.Agent.LastName,
                               Customer = inv.SalesOrder.Customer.Name,
                               InvoiceDateD = inv.InvoiceDate,
                               InvoiceNumber = inv.InvoiceNumber, //.Remove(0, 4),
                               InvoiceType = inv.InvoiceType.Name,
                               TotalAmountD = inv.InvoiceLineItems.Sum(s => s.Quantity * s.UnitPrice)
                           };

            return await invoices.ToListAsync();
        }

        public async Task<ForDataTables<SOACRInvoices>> GetInvoiceListDTAsync(DataTablesRequestForm form, ListFilterSalesModel filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetInvoiceListAsync(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();

            //  var test = data.Select(x => x.InvoiceNumber = "DR-0549638")
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Agent.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.Customer.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.InvoiceDate.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.InvoiceNumber.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.InvoiceType.ToLower().Contains(form.SearchValue.ToLower())
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
                        case "Agent": data = data.OrderBy(s => s.Agent); break;
                        case "Customer": data = data.OrderBy(s => s.Customer); break;
                        case "InvoiceDate": data = data.OrderBy(s => s.InvoiceDateD); break;
                        case "InvoiceNumber": data = data.OrderBy(s => s.InvoiceNumber); break;
                        case "InvoiceType": data = data.OrderBy(s => s.InvoiceType); break;
                        case "TotalAmount": data = data.OrderBy(s => s.TotalAmountD); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Agent": data = data.OrderByDescending(s => s.Agent); break;
                        case "Customer": data = data.OrderByDescending(s => s.Customer); break;
                        case "InvoiceDate": data = data.OrderByDescending(s => s.InvoiceDateD); break;
                        case "InvoiceNumber": data = data.OrderByDescending(s => s.InvoiceNumber); break;
                        case "InvoiceType": data = data.OrderByDescending(s => s.InvoiceType); break;
                        case "TotalAmount": data = data.OrderByDescending(s => s.TotalAmountD); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<SOACRInvoices>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public async Task<SOACRInvoicesPVM> PrintInvoice(ListFilterSalesModel filter)
        {
            var invoices = await GetInvoiceListAsync(filter);
            var agent = await context.Agents.Where(s => s.Id == filter.Agent).FirstOrDefaultAsync();
            var customer = await context.Customers.Where(s => s.Id == filter.Customer).FirstOrDefaultAsync();

            return new SOACRInvoicesPVM
            {
                DateFromD = filter.DateFrom.GetValueOrDefault(),
                DateToD = filter.DateTo.GetValueOrDefault(),
                Customer = filter.Customer == null || customer == null ? "All" : customer.Name,
                Agent = filter.Agent == null || agent == null ? "All" : agent.FirstName + " " + agent.LastName,
                Invoices = invoices,
                TotalAmountD = invoices.Sum(s => s.TotalAmountD)
            };
        }
        #endregion

        #region for SOACR/Generate CR
        private async Task<List<SoacrInvoiceVM>> GetCustomerInvoiceToAdd(int? customerId)
        {
            var invoice = from inv in context.Invoices
                          join soal in context.SOACRLineItems on inv.Id equals soal.InvoiceId into soali
                          from soaInvoice in soali.DefaultIfEmpty()
                          where inv.SalesOrder.Customer.Id == customerId
                          && soaInvoice == null
                          && inv.Status.Name.ToLower() == "invoice submitted"
                          select new SoacrInvoiceVM
                          {
                              Id = inv.Id,
                              InvoiceDateD = inv.InvoiceDate,
                              InvoiceNumber = inv.InvoiceNumber,
                              ControlNumber = inv.ControlNumber,
                              InvoiceType = inv.InvoiceType.Name,
                              TotalAmountD = (from invli in inv.InvoiceLineItems
                                              join order in context.SalesOrders on invli.Invoice.SalesOrderId equals order.Id
                                              join orderli in context.SalesOrderLineItems on order.Id equals orderli.SalesOrderId
                                              let discount1 = (100 - orderli.Discount1) / 100
                                              let discount2 = (100 - orderli.Discount2) / 100
                                              where invli.ItemId == orderli.ItemId
                                              select invli.Quantity * invli.UnitPrice * discount1 * discount2).Sum()
                          };

            return await invoice.ToListAsync();
        }

        public async Task<ForDataTables<SoacrInvoiceVM>> GetCustomerInvoiceToAddDT(DataTablesRequestForm form, int customerId)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetCustomerInvoiceToAdd(customerId)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.InvoiceDate.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.InvoiceNumber.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.ControlNumber.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.InvoiceType.ToLower().Contains(form.SearchValue.ToLower())
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
                        case "InvoiceDate": data = data.OrderBy(s => s.InvoiceDateD); break;
                        case "InvoiceNumber": data = data.OrderBy(s => s.InvoiceNumber); break;
                        case "ControlNumber": data = data.OrderBy(s => s.ControlNumber); break;
                        case "InvoiceType": data = data.OrderBy(s => s.InvoiceType); break;
                        case "TotalAmount": data = data.OrderBy(s => s.TotalAmountD); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "InvoiceDate": data = data.OrderByDescending(s => s.InvoiceDateD); break;
                        case "InvoiceNumber": data = data.OrderByDescending(s => s.InvoiceNumber); break;
                        case "ControlNumber": data = data.OrderByDescending(s => s.ControlNumber); break;
                        case "InvoiceType": data = data.OrderByDescending(s => s.InvoiceType); break;
                        case "TotalAmount": data = data.OrderByDescending(s => s.TotalAmountD); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = form.PageSize == -1 ? data : data.Skip(form.Skip).Take(form.PageSize); // with added pagesize for All
            var dataList = pageList.ToList();
            return new ForDataTables<SoacrInvoiceVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList
            };
        }

        public async Task<int> GenerateCRAsync(int CustomerId, int[] InvoiceIds)
        {
            var soaType = await context.SOACRTypes.Where(s => s.Name.ToLower() == "counter receipt").Select(s => s.Id).FirstAsync();
            var invoices = await context.Invoices.Where(s => InvoiceIds.Contains(s.Id)).ToListAsync();

            var soa = new SOACR
            {
                CustomerId = CustomerId,
                SOACRTypeId = soaType
            };
            await context.SOACRs.AddAsync(soa);
            await context.SaveChangesAsync(currentUserId);

            foreach (var invoice in invoices)
            {
                var soali = new SOACRLineItem
                {
                    SOACRId = soa.Id,
                    InvoiceId = invoice.Id,
                    CreditAmount = 0,
                    DebitAmount = 0,
                    FreightCost = 0
                };
                await context.SOACRLineItems.AddAsync(soali);
                await context.SaveChangesAsync(currentUserId);
            }

            return soa.Id;
        }
        #endregion

        #region SOACR/Detail
        public async Task<SOACRDetailsVM?> GetSOACRDetails(int? id, bool loadItems = false)
        {
            var soaVM = from soa in context.SOACRs
                        where soa.Id == id
                        select new SOACRDetailsVM
                        {
                            Id = soa.Id,
                            Customer = soa.Customer.Name,
                            CustomerId = soa.Customer.Id,
                            Type = soa.SOACRType.Name,
                            DateFromD = soa.SOAFrom,
                            DateToD = soa.SOATo
                        };

            var details = await soaVM.FirstOrDefaultAsync();
            if (details != null && loadItems)
            {
                details.Items = (await GetSOACRItemsAsync(id!.Value)).ToList();
            }
            return details;
        }

        private async Task<IList<SOACRItemsVM>> GetSOACRItemsAsync(int id)
        {
            var soa = from sl in context.SOACRLineItems
                      let inv = (from ili in sl.Invoice.InvoiceLineItems
                                 join soli in context.SalesOrderLineItems on new { ili.Invoice.SalesOrderId, ili.ItemId }
                                                                      equals new { soli.SalesOrderId, soli.ItemId }
                                 let discount1 = (100 - soli.Discount1) / 100
                                 let discount2 = (100 - soli.Discount2) / 100
                                 select (decimal?)ili.Quantity * ili.UnitPrice * discount1 * discount2).Sum() ?? 0
                      let tbr = context.SOACRLineItemTBRs
                                       .Where(s => s.SOACRLineItemId == sl.Id)
                                       .Sum(s => (decimal?)s.Amount) ?? 0
                      let pay = context.CustomerPaymentDetails
                                       .Where(s => s.InvoiceId == sl.InvoiceId
                                                && s.CustomerPayment.SOACR.Id == sl.SOACR.Id)
                                       .Sum(s => (decimal?)s.PaidAmount) ?? 0
                      where sl.SOACR.Id == id
                      select new SOACRItemsVM
                      {
                          Id = sl.Id,
                          InvoiceDateD = sl.Invoice.InvoiceDate,
                          InvoiceNumber = sl.Invoice.InvoiceNumber,
                          ControlNumber = sl.Invoice.ControlNumber,
                          InvoiceAmountD = inv,
                          InvoiceType = sl.Invoice.InvoiceType.Code,
                          InvoiceId = sl.Invoice.Id,
                          TBRD = tbr,
                          CreditD = sl.CreditAmount,
                          CreditReferenceNumber = sl.CreditReferenceNumber,
                          DebitD = sl.DebitAmount,
                          DebitReferenceNumber = sl.DebitReferenceNumber,
                          PaidAmountD = pay,
                          FreightChargeD = sl.FreightCost,
                          FreightReferenceNumber = sl.FreightReferenceNumber
                      };
            return await soa.ToListAsync();
        }

        public async Task<ForDataTables<SOACRItemsVM>> GetSOACRItemsDTAsync(DataTablesRequestForm form, int id)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetSOACRItemsAsync(id)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.InvoiceDate.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.InvoiceNumber.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.ControlNumber.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.InvoiceAmountD.ToString().Contains(form.SearchValue)
                                    || x.InvoiceType.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.TBRD.ToString().Contains(form.SearchValue)
                                    || x.CreditD.ToString().Contains(form.SearchValue)
                                    || x.DebitD.ToString().Contains(form.SearchValue)
                                    || x.PaidAmountD.ToString().Contains(form.SearchValue)
                                    || x.FreightChargeD.ToString().Contains(form.SearchValue)
                                    || x.BalanceD.ToString().Contains(form.SearchValue));
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
                        case "InvoiceDate": data = data.OrderBy(s => s.InvoiceDateD); break;
                        case "InvoiceNumber": data = data.OrderBy(s => s.InvoiceNumber); break;
                        case "ControlNumber": data = data.OrderBy(s => s.ControlNumber); break;
                        case "InvoiceAmount": data = data.OrderBy(s => s.InvoiceAmountD); break;
                        case "InvoiceType": data = data.OrderBy(s => s.InvoiceType); break;
                        case "TBR": data = data.OrderBy(s => s.TBRD); break;
                        case "Credit": data = data.OrderBy(s => s.CreditD); break;
                        case "Debit": data = data.OrderBy(s => s.DebitD); break;
                        case "PaidAmount": data = data.OrderBy(s => s.PaidAmountD); break;
                        case "FreightCharge": data = data.OrderBy(s => s.FreightChargeD); break;
                        case "Balance": data = data.OrderBy(s => s.BalanceD); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "InvoiceDate": data = data.OrderByDescending(s => s.InvoiceDateD); break;
                        case "InvoiceNumber": data = data.OrderByDescending(s => s.InvoiceNumber); break;
                        case "ControlNumber": data = data.OrderByDescending(s => s.ControlNumber); break;
                        case "InvoiceAmount": data = data.OrderByDescending(s => s.InvoiceAmountD); break;
                        case "InvoiceType": data = data.OrderByDescending(s => s.InvoiceType); break;
                        case "TBR": data = data.OrderByDescending(s => s.TBRD); break;
                        case "Credit": data = data.OrderByDescending(s => s.CreditD); break;
                        case "Debit": data = data.OrderByDescending(s => s.DebitD); break;
                        case "PaidAmount": data = data.OrderByDescending(s => s.PaidAmountD); break;
                        case "FreightCharge": data = data.OrderByDescending(s => s.FreightChargeD); break;
                        case "Balance": data = data.OrderByDescending(s => s.BalanceD); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = form.PageSize == -1 ? data : data.Skip(form.Skip).Take(form.PageSize); // with added pagesize for All
            var dataList = pageList.ToList();
            return new ForDataTables<SOACRItemsVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList
            };
        }

        public async Task UpdateSOACRInvoiceAsync(SOACRItemsForm soaVM)
        {
            var soa = context.SOACRLineItems.Where(s => s.Id == soaVM.Id).FirstOrDefault();
            if (soa == null)
            {
                throw new DbUpdateConcurrencyException();
            }

            soa.DebitAmount = soaVM.Debit;
            soa.DebitReferenceNumber = soaVM.DebitReference;
            soa.CreditAmount = soaVM.Credit;
            soa.CreditReferenceNumber = soaVM.CreditReference;
            soa.FreightCost = soaVM.FreightCharge;
            soa.FreightReferenceNumber = soaVM.FreightReference;

            context.SOACRLineItems.Update(soa);
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task<bool> IsSOACRLineItemIdExist(int id)
        {
            return await context.SOACRLineItems.AnyAsync(c => c.Id == id);
        }

        public async Task<bool> IsCheckNumberInUse(string checkNumber)
        {
            return await context.SOACRChecks.AnyAsync(s => s.CheckNumber == checkNumber);
        }

        public async Task<bool> IsCollectorReceiptNumberInUse(string collectorReceiptNumber)
        {
            return await context.CustomerPayments.AnyAsync(s => s.CRNumber == collectorReceiptNumber);
        }

        public async Task<bool> AddPaymentDetails(SOACRPaymentDetails payment)
        {
            var soacr = await context.SOACRs.Where(s => s.Id == payment.SOACRId).FirstAsync();
            //var soacrli = await context.SOACRLineItems.Where(s => payment.InvoiceIds.Contains(s.Id)).ToListAsync();
            var invoices = await context.Invoices.Where(s => payment.InvoiceIds.Contains(s.Id)).ToListAsync();
            var totalChecks = payment.Checks == null || payment.Checks.Count == 0 ? 0 : payment.Checks.Sum(s => s.CheckAmount);
            var customerPayment = new CustomerPayment
            {
                BackLoad = payment.BackLoad,
                CashAdvance = payment.CashAdvance,
                Cash = payment.CashAmount,
                CRNumber = payment.CollectorReceiptNumber,
                CustomerId = soacr.CustomerId,
                Discount = payment.Discount,
                OffSet = payment.Offset,
                OverPayment = payment.OverPayment,// 0,
                Remarks = payment.Remarks + "",
                SOACRId = soacr.Id,
                Tax = payment.Tax,
                TKTSolicitation = payment.TKTSolicitation
            };

            await context.CustomerPayments.AddAsync(customerPayment);
            await context.SaveChangesAsync(currentUserId);

            if (payment.Checks != null && payment.Checks.Count > 0)
            {
                foreach (var Check in payment.Checks)
                {
                    var soacrCheck = new SOACRCheck()
                    {
                        BankBranch = Check.BankBranch,
                        BankName = Check.BankName,
                        CheckAmount = Check.CheckAmount,
                        CheckDate = Check.CheckDate,
                        CheckNumber = Check.CheckNumber,
                        CustomerPaymentId = customerPayment.Id
                    };
                    await context.SOACRChecks.AddAsync(soacrCheck);
                    await context.SaveChangesAsync(currentUserId);
                }
            }

            var PaymentAmount = payment.BackLoad
                              + payment.CashAdvance
                              + payment.CashAmount
                              + payment.Discount
                              + payment.Offset
                              + payment.OverPayment
                              + payment.Tax
                              + payment.TKTSolicitation
                              + (payment.Checks?.Sum(s => (decimal?)s.CheckAmount) ?? 0);

            Array.Sort(payment.InvoiceIds);
            foreach (var InvoiceId in payment.InvoiceIds)
            {
                var soali = from sl in context.SOACRLineItems
                            let inv = (from ili in sl.Invoice.InvoiceLineItems
                                       join soli in context.SalesOrderLineItems on new { ili.Invoice.SalesOrderId, ili.ItemId }
                                                                            equals new { soli.SalesOrderId, soli.ItemId }
                                       let discount1 = (100 - soli.Discount1) / 100
                                       let discount2 = (100 - soli.Discount2) / 100
                                       select (decimal?)ili.Quantity * ili.UnitPrice * discount1 * discount2).Sum() ?? 0
                            let tbr = context.SOACRLineItemTBRs
                                             .Where(s => s.SOACRLineItemId == sl.Id)
                                             .Sum(s => (decimal?)s.Amount) ?? 0
                            let pay = context.CustomerPaymentDetails
                                             .Where(s => s.InvoiceId == sl.InvoiceId
                                                      && s.CustomerPayment.SOACR.Id == sl.SOACR.Id)
                                             .Sum(s => (decimal?)s.PaidAmount) ?? 0
                            where sl.SOACR.Id == payment.SOACRId
                            && sl.InvoiceId == InvoiceId
                            select inv - tbr - sl.CreditAmount + sl.DebitAmount - pay + sl.FreightCost;

                var forPayment = soali.FirstOrDefault();
                var toPay = 0m;
                if (forPayment <= PaymentAmount)
                {
                    toPay = forPayment;
                    PaymentAmount -= forPayment;
                }
                else
                {
                    toPay = PaymentAmount;
                    PaymentAmount = 0;
                }

                var paymentDetails = new CustomerPaymentDetail
                {
                    InvoiceId = InvoiceId,
                    CustomerPaymentId = customerPayment.Id,
                    PaidAmount = toPay
                };
                await context.CustomerPaymentDetails.AddAsync(paymentDetails);
                await context.SaveChangesAsync(currentUserId);
            }

            return true;
        }

        public async Task<SOACRDetailsPVM> PrintCRDetails(int id)
        {
            var items = (await GetSOACRItemsAsync(id)).OrderBy(s => s.InvoiceDateD).ToList();

            var soacrVM = from soacr in context.SOACRs
                          where soacr.Id == id
                          select new SOACRDetailsPVM
                          {
                              SOACRNumber = soacr.Id,
                              CustomerCode = soacr.Customer.Code,
                              CustomerName = soacr.Customer.Name,
                              Contents = items
                          };

            return await soacrVM.FirstAsync();
        }

        public async Task<SOACRDetailsPVM> PrintSOADetails(int id)
        {
            var items = (await GetSOACRItemsAsync(id)).OrderBy(s => s.InvoiceDateD).ToList();

            var soacrVM = from soacr in context.SOACRs
                          let litems = soacr.SOACRLineItems.Where(s => s.SOACRId == id).ToList()
                          where soacr.Id == id
                          select new SOACRDetailsPVM
                          {
                              SOACRNumber = soacr.Id,
                              Agent = litems.First().Invoice.SalesOrder.Agent.FirstName + " " + litems.First().Invoice.SalesOrder.Agent.LastName,
                              AgentCode = litems.First().Invoice.SalesOrder.Agent.Code,
                              CustomerCode = soacr.Customer.Code,
                              CustomerName = soacr.Customer.Name,
                              Contents = items,
                              CreditTerm = soacr.Customer.CreditTerm.Name,
                              DateFromD = soacr.SOAFrom,
                              DateToD = soacr.SOATo
                          };

            return await soacrVM.FirstAsync();
        }

        public async Task AddInvoicesAsync(int soaId, int[] invoiceIds)
        {
            foreach (var invoiceId in invoiceIds)
            {
                var invoice = await context.Invoices.Where(s => s.Id == invoiceId).FirstAsync();
                var soali = new SOACRLineItem
                {
                    SOACRId = soaId,
                    InvoiceId = invoiceId,
                    CreditAmount = 0,
                    DebitAmount = 0,
                    FreightCost = 0
                };
                await context.SOACRLineItems.AddAsync(soali);
                await context.SaveChangesAsync(currentUserId);
            }
        }
        public async Task<Select2Model<SOACRInvoiceTBRVM>> GetInvoicesTBRAsync(string search, int page, int CustomerId)
        {
            var invoices = from ret in context.SalesReturnLineItems
                           let tbr = context.SOACRLineItemTBRs.Where(s => s.SalesReturnLineItemId == ret.Id).ToList()
                           where (string.IsNullOrWhiteSpace(search) 
                               || ret.InvoiceLineItem!.Invoice.InvoiceNumber.ToLower().Contains(search.ToLower()) 
                               || ("tbr".Contains(search.ToLower()) && ret.InvoiceLineItemId == null))
                           //&& ((ret.SalesReturn.SalesOrder == null && ret.SalesReturn.Customer!.Id == CustomerId)
                           //|| (ret.SalesReturn.SalesOrder != null && ret.SalesReturn.SalesOrder.Customer.Id == CustomerId))
                           && ((ret.SalesReturn.SalesOrder != null && ret.SalesReturn.SalesOrder.CustomerId == CustomerId)
                           || ret.SalesReturn.CustomerId == CustomerId)
                           let discount1 = (100 - ret.Discount1) / 100
                           let discount2 = (100 - ret.Discount2) / 100
                           select new SOACRInvoiceTBRVM
                           {
                               Id = ret.Id,
                               Text = ret.SalesReturn.SalesReturnNumber + " (" + (ret.InvoiceLineItem == null ? "TBR" : ret.InvoiceLineItem!.Invoice.InvoiceNumber) + ")",
                               InvoiceAmount = (ret.Quantity * ret.SellingPrice * discount1 * discount2)
                                             - (tbr.Count == 0 ? 0 : tbr.Sum(s => s.Amount))
                           };

            var invoicesFiltered = await invoices.Skip((page - 1) * 50).Take(50).ToListAsync();
            return new Select2Model<SOACRInvoiceTBRVM>
            {
                //incomplete_results = false,
                items = invoicesFiltered,
                total_count = invoices.Count()
            };
        }

        public async Task AddInvoiceTBRAsync(int soaLineItemId, int salesReturnId, decimal amount)
        {
            var tbr = new SOACRLineItemTBR
            {
                Amount = amount,
                SalesReturnLineItemId = salesReturnId,
                SOACRLineItemId = soaLineItemId
            };

            await context.SOACRLineItemTBRs.AddAsync(tbr);
            await context.SaveChangesAsync(currentUserId);
        }

        private async Task<IList<SOACRInvoiceTBRHistoryVM>> GetInvoiceTBRHistoryAsync(int soaLineItemId)
        {
            var tbrh = from tbr in context.SOACRLineItemTBRs
                       where tbr.SOACRLineItemId == soaLineItemId
                       join user in userManager.Users on tbr.UserCreated equals user.Id
                       select new SOACRInvoiceTBRHistoryVM
                       {
                           AmountD = tbr.Amount,
                           DateCreatedD = tbr.DateCreated,
                           InvoiceNumber = tbr.SalesReturnLineItem.SalesReturn.SalesReturnNumber + " (" + (tbr.SalesReturnLineItem.InvoiceLineItem == null ? "TBR" : tbr.SalesReturnLineItem.InvoiceLineItem!.Invoice.InvoiceNumber) + ")",
                           UserCreated = SystemFormat.Name(user.FirstName, user.LastName, user.Nickname)
                       };
            return await tbrh.ToListAsync();
        }

        public async Task<ForDataTables<SOACRInvoiceTBRHistoryVM>> GetInvoiceTBRHistoryDTAsync(DataTablesRequestForm form, int soaLineItemId)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetInvoiceTBRHistoryAsync(soaLineItemId)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.AmountD.ToString().Contains(form.SearchValue)
                                    || x.DateCreated.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.InvoiceNumber.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.UserCreated.ToLower().Contains(form.SearchValue.ToLower()));
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
                        case "Amount": data = data.OrderBy(s => s.AmountD); break;
                        case "DateCreated": data = data.OrderBy(s => s.DateCreatedD); break;
                        case "InvoiceNumber": data = data.OrderBy(s => s.InvoiceNumber); break;
                        case "UserCreated": data = data.OrderBy(s => s.UserCreated); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Amount": data = data.OrderByDescending(s => s.AmountD); break;
                        case "DateCreated": data = data.OrderByDescending(s => s.DateCreatedD); break;
                        case "InvoiceNumber": data = data.OrderByDescending(s => s.InvoiceNumber); break;
                        case "UserCreated": data = data.OrderByDescending(s => s.UserCreated); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = form.PageSize == -1 ? data : data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<SOACRInvoiceTBRHistoryVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList
            };
        }
        #endregion
    }
}