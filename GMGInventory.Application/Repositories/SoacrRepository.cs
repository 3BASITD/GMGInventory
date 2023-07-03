using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using GMGInventory.Data.Migrations;
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
            var soacr = from soa in context.SOACRs
                        let soalit = context.SOACRLineItems.Where(s => s.SOACRId == soa.Id).ToList()
                        //let over = context.CustomerPayments.Where(s => s.SOACRId == soa.Id).Sum(s => s.OverPayment)
                        let paid = context.CustomerPayments.Where(s => s.SOACRId == soa.Id).Sum(s => s.TotalAmountPaid)
                        let tbr = context.SOACRLineItemTBRs.Where(s => soalit.Select(a => a.Id).Contains(s.SOACRLineItemId)).Sum(s => s.Amount)
                        select new SOACRVM
                        {
                            Id = soa.Id,
                            Customer = soa.Customer.Name,
                            Type = soa.SOACRType.Code,
                            SOAFromD = soa.SOAFrom,
                            SOAToD = soa.SOATo,
                            TotalAmountD = soalit.Sum(s => s.Invoice.Amount),
                            TBRD = tbr,
                            CreditD = soalit.Sum(s => s.CreditAmount),
                            DebitD = soalit.Sum(s => s.DebitAmount),
                            PaidAmountD = paid,
                            FreightChargeD = soalit.Sum(s => s.FreightCost),
                            //OverPaymentD = over
                            OverPaymentD = Math.Max(0, paid - soalit.Sum(s => s.Invoice.Amount))
                        };
            return await soacr.ToListAsync();
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
                                    || x.Customer.ToLower().Contains(form.SearchValue)
                                    || x.Type.ToLower().Contains(form.SearchValue)
                                    || x.SOAFrom.ToLower().Contains(form.SearchValue)
                                    || x.SOATo.ToLower().Contains(form.SearchValue)
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
            var soaType = await context.SOACRTypes.Where(s => s.Name == "Statement of Account").Select(s => s.Id).FirstAsync();

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
                SOATo = soaVM.DateTo,
                AmountToPay = invoices.Sum(s => s.Amount),
                //TotalCMAAmount = 0,
                //TotalDMAAmount = 0,
                //TotalTBRAmount = 0,
                //TotalBalance = 0,
                //TotalFreightCost = 0,
                //TotalAmount = 0,
                //OverPayment = 0,
            };
            await context.SOACRs.AddAsync(soa);
            await context.SaveChangesAsync(currentUserId);

            foreach (var invoice in invoices)
            {
                var inv = new SOACRLineItem
                {
                    SOACRId = soa.Id,
                    InvoiceId = invoice.Id,
                    InvoiceAmount = invoice.Amount,
                    PaidAmount = 0,
                    //TBRAmount = 0,
                    CreditAmount = 0,
                    DebitAmount = 0,
                    FreightCost = 0,
                    FreightWaybill = "",
                    IsPaid = false
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
                           where (filter.Customer == null || filter.Customer == inv.SalesOrder.Customer.Id)
                              && (filter.Agent == null || filter.Agent == inv.SalesOrder.Agent.Id)
                              && filter.DateFrom <= inv.InvoiceDate.Date
                              && inv.InvoiceDate.Date <= filter.DateTo
                              && inv.Status.Name == "Invoice Submitted"
                           select new SOACRInvoices
                           {
                               Agent = inv.SalesOrder.Agent.FirstName + " " + inv.SalesOrder.Agent.LastName,
                               Customer = inv.SalesOrder.Customer.Name,
                               InvoiceDateD = inv.InvoiceDate,
                               InvoiceNumber = inv.InvoiceNumber, //.Remove(0, 4),
                               InvoiceType = inv.InvoiceType.Name,
                               TotalAmountD = inv.Amount
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
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Agent.ToLower().Contains(form.SearchValue)
                                    || x.Customer.ToLower().Contains(form.SearchValue)
                                    || x.InvoiceDate.ToLower().Contains(form.SearchValue)
                                    || x.InvoiceNumber.ToLower().Contains(form.SearchValue)
                                    || x.InvoiceType.ToLower().Contains(form.SearchValue)
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
                          && inv.Status.Name == "Invoice Submitted"
                          select new SoacrInvoiceVM
                          {
                              Id = inv.Id,
                              InvoiceDateD = inv.InvoiceDate,
                              InvoiceNumber = inv.InvoiceNumber,
                              InvoiceType = inv.InvoiceType.Name,
                              TotalAmountD = inv.Amount
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
                data = data.Where(x => x.InvoiceDate.ToLower().Contains(form.SearchValue)
                                    || x.InvoiceNumber.ToLower().Contains(form.SearchValue)
                                    || x.InvoiceType.ToLower().Contains(form.SearchValue)
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
            var soaType = await context.SOACRTypes.Where(s => s.Name == "Counter Receipt").Select(s => s.Id).FirstAsync();
            var invoices = await context.Invoices.Where(s => InvoiceIds.Contains(s.Id)).ToListAsync();

            var soa = new SOACR
            {
                CustomerId = CustomerId,
                SOACRTypeId = soaType,
                AmountToPay = invoices.Sum(s => s.Amount),
                //TotalCMAAmount = 0,
                //TotalDMAAmount = 0,
                //TotalTBRAmount = 0,
                //TotalBalance = 0,
                //TotalFreightCost = 0,
                //TotalAmount = 0,
                //OverPayment = 0,
            };
            await context.SOACRs.AddAsync(soa);
            await context.SaveChangesAsync(currentUserId);

            foreach (var invoice in invoices)
            {
                var soali = new SOACRLineItem
                {
                    SOACRId = soa.Id,
                    InvoiceId = invoice.Id,
                    InvoiceAmount = invoice.Amount,
                    PaidAmount = 0,
                    //TBRAmount = 0,
                    CreditAmount = 0,
                    DebitAmount = 0,
                    FreightCost = 0,
                    FreightWaybill = "",
                    IsPaid = false
                };
                await context.SOACRLineItems.AddAsync(soali);
                await context.SaveChangesAsync(currentUserId);
            }

            return soa.Id;
        }
        #endregion

        #region SOACR/Detail
        public async Task<SOACRDetailsVM?> GetSOACRDetails(int? id)
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
            return await soaVM.FirstOrDefaultAsync();
        }

        private async Task<IList<SOACRItemsVM>> GetSOACRItemsAsync(int id)
        {
            var soa = from sl in context.SOACRLineItems
                      let tbr = context.SOACRLineItemTBRs.Where(s => s.SOACRLineItemId == sl.Id).Sum(s => s.Amount)
                      where sl.SOACR.Id == id
                      select new SOACRItemsVM
                      {
                          Id = sl.Id,
                          InvoiceDateD = /*sl.Invoice.DateModified ?? */sl.Invoice.DateCreated,
                          InvoiceNumber = sl.Invoice.InvoiceNumber,
                          InvoiceAmountD = sl.Invoice.Amount,
                          InvoiceType = sl.Invoice.InvoiceType.Code,
                          InvoiceId = sl.Invoice.Id,
                          IsPaid = sl.IsPaid ?? false,
                          TBRD = tbr,
                          CreditD = sl.CreditAmount,
                          DebitD = sl.DebitAmount,
                          PaidAmountD = sl.PaidAmount,
                          FreightChargeD = sl.FreightCost,
                          FreightWaybill = sl.FreightWaybill + ""
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
                data = data.Where(x => x.InvoiceDate.ToLower().Contains(form.SearchValue)
                                    || x.InvoiceNumber.ToLower().Contains(form.SearchValue)
                                    || x.InvoiceAmountD.ToString().Contains(form.SearchValue)
                                    || x.InvoiceType.ToLower().Contains(form.SearchValue)
                                    || x.TBRD.ToString().Contains(form.SearchValue)
                                    || x.CreditD.ToString().Contains(form.SearchValue)
                                    || x.DebitD.ToString().Contains(form.SearchValue)
                                    || x.PaidAmountD.ToString().Contains(form.SearchValue)
                                    || x.FreightChargeD.ToString().Contains(form.SearchValue)
                                    || x.FreightWaybill.ToLower().Contains(form.SearchValue)
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
                        case "InvoiceAmount": data = data.OrderBy(s => s.InvoiceAmountD); break;
                        case "InvoiceType": data = data.OrderBy(s => s.InvoiceType); break;
                        case "TBR": data = data.OrderBy(s => s.TBRD); break;
                        case "Credit": data = data.OrderBy(s => s.CreditD); break;
                        case "Debit": data = data.OrderBy(s => s.DebitD); break;
                        case "PaidAmount": data = data.OrderBy(s => s.PaidAmountD); break;
                        case "FreightCharge": data = data.OrderBy(s => s.FreightChargeD); break;
                        case "FreightWaybill": data = data.OrderBy(s => s.FreightWaybill); break;
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
                        case "InvoiceAmount": data = data.OrderByDescending(s => s.InvoiceAmountD); break;
                        case "InvoiceType": data = data.OrderByDescending(s => s.InvoiceType); break;
                        case "TBR": data = data.OrderByDescending(s => s.TBRD); break;
                        case "Credit": data = data.OrderByDescending(s => s.CreditD); break;
                        case "Debit": data = data.OrderByDescending(s => s.DebitD); break;
                        case "PaidAmount": data = data.OrderByDescending(s => s.PaidAmountD); break;
                        case "FreightCharge": data = data.OrderByDescending(s => s.FreightChargeD); break;
                        case "FreightWaybill": data = data.OrderByDescending(s => s.FreightWaybill); break;
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
            soa.CreditAmount = soaVM.Credit;
            soa.FreightCost = soaVM.FreightCharge;
            soa.FreightWaybill = soaVM.FreightWaybill;

            context.SOACRLineItems.Update(soa);
            await context.SaveChangesAsync(currentUserId);

            await SetSOACRLineItemIfPaid(soa);
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
                TKTSolicitation = payment.TKTSolicitation,
                //TotalAmountPaid = payment.BackLoad + payment.CashAdvance + payment.CashAmount - payment.Discount - payment.Offset + payment.OverPayment
                //                - payment.Tax - payment.TKTSolicitation + totalChecks,
                TotalAmountPaid = payment.BackLoad + payment.CashAdvance + payment.CashAmount + payment.Discount + payment.Offset + payment.OverPayment
                                + payment.Tax + payment.TKTSolicitation + totalChecks,
                //TotalInvoiceAmount = soacrli.Sum(s => s.InvoiceAmount)
                TotalInvoiceAmount = invoices.Sum(s => s.Amount)
            };

            await context.CustomerPayments.AddAsync(customerPayment);
            await context.SaveChangesAsync(currentUserId);

            soacr.AmountToPay += customerPayment.TotalAmountPaid;
            //soacr.TotalBalance = soacr.TotalAmount - soacr.TotalTBRAmount - soacr.TotalCMAAmount + soacr.TotalDMAAmount - soacr.AmountToPay + soacr.TotalFreightCost;

            context.SOACRs.Update(soacr);
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

            var PaymentAmount = customerPayment.TotalAmountPaid;

            Array.Sort(payment.InvoiceIds);

            foreach (var InvoiceId in payment.InvoiceIds)
            {
                if (PaymentAmount > 0)
                {
                    var soacrLineItem = await context.SOACRLineItems
                                                     .Where(s => s.InvoiceId == InvoiceId)
                                                     .FirstOrDefaultAsync();
                    if (soacrLineItem != null)
                    {
                        var invoice = await context.Invoices.Where(s => s.Id == InvoiceId).FirstAsync();
                        //if (soacrLineItem.InvoiceAmount >= soacrLineItem.PaidAmount) // check if soa line item can pay
                        //InvoiceAmountD - TBRD - CreditD + DebitD - PaidAmountD + FreightChargeD
                        var tbrs = await context.SOACRLineItemTBRs
                                                .Where(s => s.SalesReturnLineItemId == soacrLineItem.Id)
                                                .SumAsync(s => (decimal?)s.Amount) ?? 0;
                        //if (soacrLineItem.InvoiceAmount >= soacrLineItem.PaidAmount)
                        if (soacrLineItem.InvoiceAmount >= tbrs + soacrLineItem.CreditAmount - soacrLineItem.DebitAmount + soacrLineItem.PaidAmount - soacrLineItem.FreightCost)
                        {
                            //var toPay = soacrLineItem.InvoiceAmount - soacrLineItem.PaidAmount;
                            var toPay = soacrLineItem.InvoiceAmount - tbrs - soacrLineItem.CreditAmount + soacrLineItem.DebitAmount - soacrLineItem.PaidAmount + soacrLineItem.FreightCost;
                            if (toPay >= PaymentAmount)
                            {
                                soacrLineItem.PaidAmount += PaymentAmount;
                                invoice.PaidAmount += PaymentAmount;
                                PaymentAmount = 0;
                            }
                            else
                            {
                                soacrLineItem.PaidAmount += toPay;
                                invoice.PaidAmount += toPay;
                                PaymentAmount -= toPay;

                                //if (payment.InvoiceIds.Last() == InvoiceId) // overpayment to last soa line item
                                //{
                                //    //soacrLineItem.PaidAmount += PaymentAmount;
                                //    //invoice.PaidAmount += PaymentAmount;
                                //    customerPayment.OverPayment += PaymentAmount;
                                //    context.CustomerPayments.Update(customerPayment);
                                //}
                            }
                        }

                        //soacrLineItem.IsPaid = soacrLineItem.PaidAmount >= soacrLineItem.InvoiceAmount;
                        //context.SOACRLineItems.Update(soacrLineItem);
                        await SetSOACRLineItemIfPaid(soacrLineItem);

                        context.Invoices.Update(invoice);
                        await context.SaveChangesAsync(currentUserId);
                    }

                    var paymentDetails = new CustomerPaymentDetail
                    {
                        CustomerPaymentId = customerPayment.Id,
                        InvoiceId = InvoiceId
                    };

                    await context.CustomerPaymentDetails.AddAsync(paymentDetails);
                    await context.SaveChangesAsync(currentUserId);
                }
            }

            return true;
        }

        public async Task<SOACRDetailsPVM> PrintCRDetails(int id)
        {
            var items = (await GetSOACRItemsAsync(id)).OrderByDescending(s => s.InvoiceDateD).ToList();

            var soacrVM = from soacr in context.SOACRs
                          let litems = context.SOACRLineItems.Where(s => s.SOACRId == id).ToList()
                          //let tbr = context.SOACRLineItemTBRs.Where(s=>litems.Select(a=>a.Id).Contains(s.SOACRLineItemId)).Sum(s=>s.Amount)
                          where soacr.Id == id
                          select new SOACRDetailsPVM
                          {
                              SOACRNumber = soacr.Id,
                              CustomerCode = soacr.Customer.Code,
                              CustomerName = soacr.Customer.Name,
                              Contents = items,
                              TotalInvoiceAmountD = litems.Sum(s => s.InvoiceAmount),
                              ////TotalTBRD = litems.Sum(s => s.TBRAmount),
                              //TotalTBRD = tbr,
                              //TotalCreditD = litems.Sum(s => s.CreditAmount),
                              //TotalDebitD = litems.Sum(s => s.DebitAmount),
                              //TotalPaidAmountD = litems.Sum(s => s.PaidAmount),
                              TotalFreightCostD = litems.Sum(s => s.FreightCost)
                          };

            return await soacrVM.FirstAsync();
        }

        public async Task<SOACRDetailsPVM> PrintSOADetails(int id)
        {
            var items = (await GetSOACRItemsAsync(id)).OrderByDescending(s => s.InvoiceDateD).ToList();

            var soacrVM = from soacr in context.SOACRs
                          let litems = context.SOACRLineItems.Where(s => s.SOACRId == id).ToList()
                          //let tbr = context.SOACRLineItemTBRs.Where(s => litems.Select(a => a.Id).Contains(s.SOACRLineItemId)).Sum(s => s.Amount)
                          where soacr.Id == id
                          select new SOACRDetailsPVM
                          {
                              SOACRNumber = soacr.Id,
                              Agent = litems.First().Invoice.SalesOrder.Agent.FirstName + " " + litems.First().Invoice.SalesOrder.Agent.LastName,
                              CustomerCode = soacr.Customer.Code,
                              CustomerName = soacr.Customer.Name,
                              Contents = items,
                              CreditTerm = soacr.Customer.CreditTerm.Name,
                              DateFromD = soacr.SOAFrom,
                              DateToD = soacr.SOATo,
                              TotalInvoiceAmountD = litems.Sum(s => s.InvoiceAmount),
                              ////TotalTBRD = litems.Sum(s => s.TBRAmount),
                              //TotalTBRD = tbr,
                              //TotalCreditD = litems.Sum(s => s.CreditAmount),
                              //TotalDebitD = litems.Sum(s => s.DebitAmount),
                              //TotalPaidAmountD = litems.Sum(s => s.PaidAmount),
                              TotalFreightCostD = litems.Sum(s => s.FreightCost)
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
                    InvoiceAmount = invoice.Amount,
                    PaidAmount = 0,
                    //TBRAmount = 0,
                    CreditAmount = 0,
                    DebitAmount = 0,
                    FreightCost = 0,
                    FreightWaybill = "0",
                    IsPaid = false
                };
                await context.SOACRLineItems.AddAsync(soali);
                await context.SaveChangesAsync(currentUserId);
            }
        }
        public async Task<Select2Model<SOACRInvoiceTBRVM>> GetInvoicesTBRAsync(string search, int page, int CustomerId)
        {
            var invoices = from ret in context.SalesReturnLineItems
                           let tbr = context.SOACRLineItemTBRs.Where(s => s.SalesReturnLineItem.InvoiceId == ret.Invoice!.Id).ToList()
                           where (string.IsNullOrWhiteSpace(search) || ret.Invoice!.InvoiceNumber.ToLower().Contains(search.ToLower()) || ("tbr".Contains(search.ToLower()) && ret.InvoiceId == null))
                           && ret.SalesReturn.Customer.Id == CustomerId
                           select new SOACRInvoiceTBRVM
                           {
                               Id = ret.Id,
                               Text = ret.SalesReturn.SalesReturnNumber + " (" + (ret.Invoice == null ? "TBR" : ret.Invoice!.InvoiceNumber) + ")",
                               InvoiceAmount = ret.Quantity * (((100m - ret.Discount1 - ret.Discount2) * ret.SellingPrice) / 100m) - (tbr.Count == 0 ? 0 : tbr.Sum(s => s.Amount))
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
                       join usr in userManager.Users on tbr.UserCreated equals usr.Id
                       select new SOACRInvoiceTBRHistoryVM
                       {
                           AmountD = tbr.Amount,
                           DateCreatedD = tbr.DateCreated,
                           InvoiceNumber = tbr.SalesReturnLineItem.SalesReturn.SalesReturnNumber + " (" + (tbr.SalesReturnLineItem.Invoice == null ? "TBR" : tbr.SalesReturnLineItem.Invoice!.InvoiceNumber) + ")",
                           UserCreated = usr.Nickname
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
                                    || x.DateCreated.ToLower().Contains(form.SearchValue)
                                    || x.InvoiceNumber.ToLower().Contains(form.SearchValue)
                                    || x.UserCreated.ToLower().Contains(form.SearchValue));
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
            var pageList = form.PageSize == -1 ? data : data.Skip(form.Skip).Take(form.PageSize); // with added pagesize for All
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

        private async Task SetSOACRLineItemIfPaid(SOACRLineItem soacrLineItem)
        {
            var tbr = await context.SOACRLineItemTBRs.Where(s => s.SOACRLineItemId == soacrLineItem.Id).SumAsync(s => s.Amount);
            var comp = soacrLineItem.InvoiceAmount - tbr - soacrLineItem.CreditAmount + soacrLineItem.DebitAmount - soacrLineItem.PaidAmount + soacrLineItem.FreightCost;
            soacrLineItem.IsPaid = comp <= 0;
            context.SOACRLineItems.Update(soacrLineItem);
            await context.SaveChangesAsync(currentUserId);
        }







        //public InvoiceVM? GetInvoiceVM(int? id)
        //{
        //    var soaVM = (from inv in context.Invoices
        //                 join soali in context.SOACRLineItems
        //                            on inv.Id equals soali.InvoiceId
        //                 join soacr in context.SOACRs
        //                            on soali.SOACRId equals soacr.Id into soacrs
        //                 from soacrsGroup in soacrs.DefaultIfEmpty()

        //                 where inv.Id == id
        //                 select new InvoiceVM
        //                 {
        //                     Id = inv.Id,
        //                     Customer = soacrsGroup.Customer.Name,
        //                     InvoiceDateD = inv.DateCreated,
        //                     InvoiceNumber = inv.InvoiceNumber,
        //                     InvoiceType = inv.InvoiceType.Name,
        //                     TotalAmountD = inv.Amount
        //                 }).FirstOrDefault();
        //    return soaVM;
        //}

        //public async Task<IList<SoacrVM>> GetAll2Async(SoaFilterVM filter)
        //{
        //    var soas = from soa in context.SOACRs
        //                                    .Include(s => s.Customer)
        //               join sl in context.SOACRLineItems
        //                                on soa.Id equals sl.SOACRId into soali
        //               from soaliGroup in soali.DefaultIfEmpty()
        //               join inv in context.Invoices
        //                                on soaliGroup.InvoiceId equals inv.Id
        //               join cust in context.Customers
        //                                on soa.CustomerId equals cust.Id
        //               join agent in context.Agents
        //                                on cust.AgentId equals agent.Id

        //               join st in context.SOACRTypes
        //                                on soa.SOACRTypeId equals st.Id

        //               where (filter.CustomerId == null || filter.CustomerId == soa.CustomerId)
        //                   && (filter.AgentId == null || filter.AgentId == agent.Id)
        //               && filter.DateFrom <= (inv.DateModified ?? inv.DateCreated).Date
        //               && (inv.DateModified ?? inv.DateCreated).Date <= filter.DateTo

        //               let soalit = context.SOACRLineItems.Where(s => s.SOACRId == soa.Id)

        //               select new SoacrVM
        //               {
        //                   Id = soa.Id,
        //                   CustomerId = soa.CustomerId,
        //                   CustomerName = soa.Customer.Name,
        //                   Type = st.Code,
        //                   SOAFromD = soa.SOAFrom,
        //                   SOAToD = soa.SOATo,
        //                   TotalAmountD = soa.TotalAmount,
        //                   InvoiceDateD = inv.DateModified ?? inv.DateCreated,
        //                   InvoiceNumber = inv.InvoiceNumber,
        //                   InvoiceType = inv.InvoiceType.Name,
        //                   InvoiceAmountD = inv.Amount,
        //                   TBRD = soalit.Sum(s => s.TBRAmount),
        //                   CreditD = soalit.Sum(s => s.CreditAmount),
        //                   DebitD = soalit.Sum(s => s.CreditAmount),
        //                   PaidAmountD = soalit.Sum(s => s.PaidAmount),
        //                   FreightChargeD = soalit.Sum(s => s.FreightCost),
        //                   //BalanceD = soalit.Sum(s => s.Balance),
        //                   //OverPaymentD = soa.OverPayment
        //               };
        //    var grpsoas = from invoice in await soas.ToListAsync()
        //                  orderby invoice.Id ascending, invoice.CustomerId ascending, invoice.CustomerName ascending,
        //                  invoice.Type, invoice.SOAFromD, invoice.SOAToD, invoice.TotalAmountD, invoice.OverPaymentD,
        //                  invoice.InvoiceDateD, invoice.InvoiceNumber, invoice.InvoiceType
        //                  group invoice by new
        //                  {
        //                      invoice.Id,
        //                      invoice.CustomerId,
        //                      invoice.CustomerName,
        //                      invoice.Type,
        //                      invoice.SOAFromD,
        //                      invoice.SOAToD,
        //                      invoice.TotalAmountD,
        //                      invoice.OverPaymentD,
        //                      invoice.InvoiceDateD,
        //                      invoice.InvoiceNumber,
        //                      invoice.InvoiceType
        //                  } into i
        //                  select new SoacrVM
        //                  {
        //                      Id = i.Key.Id,
        //                      CustomerId = i.Key.CustomerId,
        //                      CustomerName = i.Key.CustomerName,
        //                      Type = i.Key.Type,
        //                      SOAFromD = i.Key.SOAFromD,
        //                      SOAToD = i.Key.SOAToD,
        //                      TotalAmountD = i.Key.TotalAmountD,
        //                      TBRD = i.Sum(s => s.TBRD),
        //                      CreditD = i.Sum(s => s.CreditD),
        //                      DebitD = i.Sum(s => s.DebitD),
        //                      PaidAmountD = i.Sum(s => s.PaidAmountD),
        //                      FreightChargeD = i.Sum(s => s.FreightChargeD),
        //                      //BalanceD = i.Sum(s => s.BalanceD),
        //                      //OverPaymentD = i.Key.OverPaymentD,
        //                      InvoiceDateD = i.Key.InvoiceDateD,
        //                      InvoiceNumber = i.Key.InvoiceNumber,
        //                      InvoiceType = i.Key.InvoiceType,
        //                      InvoiceAmountD = i.Sum(s => s.InvoiceAmountD)
        //                  };
        //    return grpsoas.ToList();
        //}

        //public async Task<ForDataTables<SoacrVM>> GetAllForDataTables2Async(DataTablesRequestForm form, SoaFilterVM filter)
        //{
        //    int totalRecord = 0;
        //    int filterRecord = 0;
        //    var data = (await GetAll2Async(filter)).AsQueryable();
        //    //get total count of data in table
        //    totalRecord = data.Count();
        //    // search data when search value found
        //    if (!string.IsNullOrWhiteSpace(form.SearchValue))
        //    {
        //        data = data.Where(x => x.CustomerName.ToLower().Contains(form.SearchValue)
        //                            || x.CRNumber.ToString().Contains(form.SearchValue)
        //                            || x.Type.ToLower().Contains(form.SearchValue)
        //                            || x.SOAFrom.ToString().Contains(form.SearchValue)
        //                            || x.SOATo.ToString().Contains(form.SearchValue)
        //                            || x.TotalAmount.ToString().Contains(form.SearchValue)
        //                            || x.TBR.ToString().Contains(form.SearchValue)
        //                            || x.Credit.ToString().Contains(form.SearchValue)
        //                            || x.Debit.ToString().Contains(form.SearchValue)
        //                            || x.PaidAmount.ToString().Contains(form.SearchValue)
        //                            || x.FreightCharge.ToString().Contains(form.SearchValue)
        //                            || x.OverPayment.ToLower().Contains(form.SearchValue));
        //    }
        //    // get total count of records after search
        //    filterRecord = data.Count();
        //    //sort data
        //    if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
        //    {
        //        if (form.SortColumnDirection == "asc")
        //        {
        //            switch (form.SortColumn)
        //            {
        //                case "Id": data = data.OrderBy(s => s.Id); break;
        //                case "CustomerName": data = data.OrderBy(s => s.CustomerName); break;
        //                case "Type": data = data.OrderBy(s => s.Type); break;
        //                case "SOAFrom": data = data.OrderBy(s => s.SOAFromD); break;
        //                case "SOATo": data = data.OrderBy(s => s.SOAToD); break;
        //                case "TotalAmount": data = data.OrderBy(s => s.TotalAmountD); break;
        //                case "TBR": data = data.OrderBy(s => s.TBRD); break;
        //                case "Credit": data = data.OrderBy(s => s.CreditD); break;
        //                case "Debit": data = data.OrderBy(s => s.DebitD); break;
        //                case "PaidAmount": data = data.OrderBy(s => s.PaidAmountD); break;
        //                case "FreightCharge": data = data.OrderBy(s => s.FreightChargeD); break;
        //                case "Balance": data = data.OrderBy(s => s.BalanceD); break;
        //                case "OverPayment": data = data.OrderBy(s => s.OverPaymentD); break;
        //                default: break;
        //            }
        //        }
        //        else if (form.SortColumnDirection == "desc")
        //        {
        //            switch (form.SortColumn)
        //            {
        //                case "Id": data = data.OrderByDescending(s => s.Id); break;
        //                case "CustomerName": data = data.OrderByDescending(s => s.CustomerName); break;
        //                case "Type": data = data.OrderByDescending(s => s.Type); break;
        //                case "SOAFrom": data = data.OrderByDescending(s => s.SOAFromD); break;
        //                case "SOATo": data = data.OrderByDescending(s => s.SOAToD); break;
        //                case "TotalAmount": data = data.OrderByDescending(s => s.TotalAmountD); break;
        //                case "TBR": data = data.OrderByDescending(s => s.TBRD); break;
        //                case "Credit": data = data.OrderByDescending(s => s.CreditD); break;
        //                case "Debit": data = data.OrderByDescending(s => s.DebitD); break;
        //                case "PaidAmount": data = data.OrderByDescending(s => s.PaidAmountD); break;
        //                case "FreightCharge": data = data.OrderByDescending(s => s.FreightChargeD); break;
        //                case "Balance": data = data.OrderByDescending(s => s.BalanceD); break;
        //                case "OverPayment": data = data.OrderByDescending(s => s.OverPaymentD); break;
        //                default: break;
        //            }
        //        }
        //    }
        //    //pagination
        //    var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
        //    return new ForDataTables<SoacrVM>
        //    {
        //        Draw = form.Draw,
        //        RecordsTotal = totalRecord,
        //        RecordsFiltered = filterRecord,
        //        Data = pageList
        //    };
        //}

        ////public SoacrLineItemVM? GetSoaInvoiceDtlVM(int? id)
        ////{
        ////    var soaVM = (from soa in context.SOACRs
        ////                                    .Include(s => s.Customer)
        ////                 join sl in context.SOACRLineItems
        ////                             on soa.Id equals sl.SOACRId into soali
        ////                 from soaliGroup in soali.DefaultIfEmpty()
        ////                 join i in context.Invoices
        ////                                  on soaliGroup.InvoiceId equals i.Id
        ////                 where soaliGroup.Id == id
        ////                 select new SoacrLineItemVM
        ////                 {
        ////                     Id = soaliGroup.Id,
        ////                     CustomerId = soa.CustomerId,
        ////                     TBRD = soaliGroup.TBRAmount,
        ////                     DebitD = soaliGroup.DebitAmount,
        ////                     CreditD = soaliGroup.CreditAmount
        ////                 }).FirstOrDefault();
        ////    return soaVM;
        ////}

        //public async Task<IList<SoacrVM>> GetSOAInvoice(/*int id*/)
        //{
        //    var inv = from soa in context.SOACRs
        //                                    .Include(s => s.Customer)
        //              join sl in context.SOACRLineItems
        //                          on soa.Id equals sl.SOACRId into soali
        //              from soaliGroup in soali.DefaultIfEmpty()
        //              join i in context.Invoices
        //                               on soaliGroup.InvoiceId equals i.Id
        //              //where soa.Id == id
        //              select new SoacrVM
        //              {
        //                  Id = soa.Id,
        //                  InvoiceDateD = i.DateCreated,
        //                  InvoiceNumber = i.InvoiceNumber,
        //                  Type = i.InvoiceType.Name,
        //                  InvoiceAmountD = i.Amount,
        //                  FreightChargeD = soaliGroup.FreightCost
        //              };
        //    return await inv.ToListAsync();
        //}

        //public async Task<IList<SoacrVM>> GetSoaDetails(int id)
        //{
        //    var soaVM = from soa in context.SOACRs
        //                                      .Include(s => s.Customer)
        //                join sl in context.SOACRLineItems
        //                            on soa.Id equals sl.SOACRId into soali
        //                from soaliGroup in soali.DefaultIfEmpty()
        //                join i in context.Invoices
        //                                 on soaliGroup.InvoiceId equals i.Id
        //                where (soa.Id == id)
        //                select new SoacrVM
        //                {
        //                    Id = soa.Id,
        //                    DateFromD = soa.SOAFrom,
        //                    DateToD = soa.SOATo,
        //                    CreditTerm = soa.Customer.CreditTerm.Name,
        //                    SoaNo = soa.Id,
        //                    Agent = soa.Customer.Agent.FirstName,
        //                    CustomerName = soa.Customer.Name,
        //                    InvoiceType = i.InvoiceType.Name,
        //                    CustomerId = soa.CustomerId,
        //                    InvoiceId = soaliGroup.InvoiceId,
        //                    WaybillNo = soaliGroup.FreightWaybill ?? "-",
        //                    InvoiceNumber = i.InvoiceNumber.Remove(0, 4),
        //                    InvoiceAmountD = i.Amount,
        //                    //InvoiceAmountDeductedD = i.Amount - soaliGroup.FreightCost - soaliGroup.DebitAmount - soaliGroup.CreditAmount,
        //                    InvoiceDateD = i.DateModified ?? i.DateCreated,
        //                    TBRD = soaliGroup.TBRAmount,
        //                    CreditD = soaliGroup.CreditAmount,
        //                    DebitD = soaliGroup.DebitAmount,
        //                    PaidAmountD = soaliGroup.PaidAmount,
        //                    FreightChargeD = soaliGroup.FreightCost,
        //                    //BalanceD = soaliGroup.Balance,
        //                    TotalAmountD = i.Amount
        //                };
        //    return await soaVM.ToListAsync();
        //}

        //public async Task<IList<SoacrVM>> GetContractInvoices2(SoaFilterVM filter)
        //{
        //    var contract = from inv in context.Invoices
        //                   join soali in context.SOACRLineItems
        //                              on inv.Id equals soali.InvoiceId
        //                   join soacr in context.SOACRs
        //                              on soali.SOACRId equals soacr.Id into soacrs
        //                   from soacrsGroup in soacrs.DefaultIfEmpty()
        //                   join invli in context.InvoiceLineItems
        //                              on inv.Id equals invli.InvoiceId

        //                   where (filter.CustomerId == soacrsGroup.CustomerId)

        //                   && soacrsGroup.SOACRTypeId == 2

        //                   select new SoacrVM
        //                   {
        //                       Id = inv.Id,
        //                       Customer = soacrsGroup.Customer.Name,
        //                       InvoiceDateD = inv.DateModified ?? inv.DateCreated,
        //                       InvoiceNumber = inv.InvoiceNumber,
        //                       InvoiceType = inv.InvoiceType.Name,
        //                       TotalAmountD = invli.Quantity * invli.UnitPrice
        //                   };
        //    //return await contract.ToListAsync();

        //    var contractGroup = from cont in await contract.ToListAsync()
        //                        orderby cont.Id, cont.Customer
        //                        group cont by new
        //                        {
        //                            cont.Id,
        //                            cont.Customer,
        //                            cont.InvoiceDateD,
        //                            cont.InvoiceNumber,
        //                            cont.InvoiceType
        //                        }
        //                        into i
        //                        select new SoacrVM
        //                        {
        //                            Id = i.Key.Id,
        //                            Customer = i.Key.Customer,
        //                            InvoiceDateD = i.Key.InvoiceDateD,
        //                            InvoiceNumber = i.Key.InvoiceNumber,
        //                            InvoiceType = i.Key.InvoiceType,
        //                            TotalAmountD = i.Sum(s => s.TotalAmountD)


        //                        };

        //    return contractGroup.ToList();


        //}

        //public async Task<ForDataTables<SoacrVM>> GetContractReceipt2DTAsync(DataTablesRequestForm form, SoaFilterVM filter)
        //{
        //    int totalRecord = 0;
        //    int filterRecord = 0;
        //    var data = (await GetContractInvoices2(filter)).AsQueryable();
        //    //get total count of data in table
        //    totalRecord = data.Count();
        //    // search data when search value found
        //    if (!string.IsNullOrWhiteSpace(form.SearchValue))
        //    {
        //        data = data.Where(x => x.InvoiceDate.ToString().Contains(form.SearchValue)
        //                            || x.InvoiceNumber.ToString().Contains(form.SearchValue)
        //                            || x.InvoiceType.ToLower().Contains(form.SearchValue)
        //                            || x.TotalAmount.ToString().Contains(form.SearchValue));
        //    }
        //    // get total count of records after search
        //    filterRecord = data.Count();
        //    //sort data
        //    if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
        //    {
        //        if (form.SortColumnDirection == "asc")
        //        {
        //            switch (form.SortColumn)
        //            {
        //                case "InvoiceDate": data = data.OrderBy(s => s.InvoiceDate); break;
        //                case "InvoiceNumber": data = data.OrderBy(s => s.InvoiceNumber); break;
        //                case "InvoiceType": data = data.OrderBy(s => s.InvoiceType); break;
        //                case "TotalAmount": data = data.OrderBy(s => s.TotalAmount); break;
        //                default: break;
        //            }
        //        }
        //        else if (form.SortColumnDirection == "desc")
        //        {
        //            switch (form.SortColumn)
        //            {
        //                case "InvoiceDate": data = data.OrderByDescending(s => s.InvoiceDate); break;
        //                case "InvoiceNumber": data = data.OrderByDescending(s => s.InvoiceNumber); break;
        //                case "InvoiceType": data = data.OrderByDescending(s => s.InvoiceType); break;
        //                case "TotalAmount": data = data.OrderByDescending(s => s.TotalAmount); break;
        //                default: break;
        //            }
        //        }
        //    }
        //    //pagination
        //    var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
        //    return new ForDataTables<SoacrVM>
        //    {
        //        Draw = form.Draw,
        //        RecordsTotal = totalRecord,
        //        RecordsFiltered = filterRecord,
        //        Data = pageList
        //    };
        //}
        //public async Task<IList<SoacrVM>> GetSoaDetails2(int id, int custId)
        //{
        //    var soadetails = (await GetSoaDetails(id)).Select(s => s.InvoiceId);

        //    var contract = from inv in context.Invoices
        //                   join soali in context.SOACRLineItems
        //                              on inv.Id equals soali.InvoiceId
        //                   join soacr in context.SOACRs
        //                              on soali.SOACRId equals soacr.Id into soacrs
        //                   from soacrsGroup in soacrs.DefaultIfEmpty()
        //                   join invli in context.InvoiceLineItems
        //                              on inv.Id equals invli.InvoiceId

        //                   where custId == soacrsGroup.Customer.Id && (!soadetails.Any(s => s == inv.Id))
        //                   && soacrsGroup.SOACRTypeId == 2

        //                   select new SoacrVM
        //                   {
        //                       Id = inv.Id,
        //                       Customer = soacrsGroup.Customer.Name,
        //                       InvoiceDateD = inv.DateModified ?? inv.DateCreated,
        //                       InvoiceNumber = inv.InvoiceNumber,
        //                       InvoiceType = inv.InvoiceType.Name,
        //                       TotalAmountD = invli.Quantity * invli.UnitPrice
        //                   };
        //    //return await contract.ToListAsync();



        //    var contractGroup = from cont in await contract.ToListAsync()
        //                        orderby cont.Id, cont.Customer
        //                        group cont by new
        //                        {
        //                            cont.Id,
        //                            cont.Customer,
        //                            cont.InvoiceDateD,
        //                            cont.InvoiceNumber,
        //                            cont.InvoiceType
        //                        }
        //                        into i
        //                        select new SoacrVM
        //                        {
        //                            Id = i.Key.Id,
        //                            Customer = i.Key.Customer,
        //                            InvoiceDateD = i.Key.InvoiceDateD,
        //                            InvoiceNumber = i.Key.InvoiceNumber,
        //                            InvoiceType = i.Key.InvoiceType,
        //                            TotalAmountD = i.Sum(s => s.TotalAmountD)


        //                        };

        //    return contractGroup.ToList();
        //}

        ////public async Task<SoaDetailsPVM> GetContractInvoicesCustomer2(int id, int custId)
        ////{
        ////    var items = await GetSoaDetails2(id, custId);

        ////    /*weirdo ng gumawa neto tapos need niya lang id
        ////     var soaVM = (from soa in context.SOACRs
        ////                                     .Include(s => s.Customer)
        ////                join sl in context.SOACRLineItems
        ////                            on soa.Id equals sl.SOACRId into soali
        ////                from soaliGroup in soali.DefaultIfEmpty()
        ////                join i in context.Invoices
        ////                                 on soaliGroup.InvoiceId equals i.Id
        ////                where (soa.Id == id)
        ////                select new SoacrVM
        ////                {
        ////                    Id = soa.Id,
        ////                    DateFromD = soa.SOAFrom,
        ////                    DateToD = soa.SOATo,
        ////                    CreditTerm = soa.Customer.CreditTerm.Name,
        ////                    SoaNo = soa.Id,
        ////                    Agent = soa.Customer.Agent.FirstName,
        ////                    CustomerName = soa.Customer.Name,
        ////                    InvoiceType = i.InvoiceType.Name,
        ////                    CustomerId = soa.CustomerId,
        ////                    InvoiceId = soaliGroup.InvoiceId,
        ////                    WaybillNo = soaliGroup.FreightWaybill ?? "-",
        ////                    InvoiceNumber = i.InvoiceNumber,
        ////                    InvoiceAmountD = i.Amount,// - soaliGroup.FreightCost,
        ////                    InvoiceDateD = i.DateModified ?? i.DateCreated,
        ////                    TBRD = soaliGroup.TBRAmount,
        ////                    CreditD = soaliGroup.CreditAmount,
        ////                    DebitD = soaliGroup.DebitAmount,
        ////                    PaidAmountD = soaliGroup.PaidAmount,
        ////                    FreightChargeD = soaliGroup.FreightCost,
        ////                    //BalanceD = soaliGroup.Balance,
        ////                    TotalAmountD = i.Amount
        ////                    //}).FirstOrDefault();
        ////                }).First();
        ////    */


        ////    return new SoaDetailsPVM
        ////    {
        ////        //Id = soaVM.Id, // weird
        ////        Id = id,
        ////        Contents = items
        ////    };
        ////}

        ////public async Task<IList<SoacrVM>> GetContractInvoices3(int? id)
        ////{
        ////    var contract = from inv in context.Invoices
        ////                   join soali in context.SOACRLineItems
        ////                              on inv.Id equals soali.InvoiceId
        ////                   join soacr in context.SOACRs
        ////                              on soali.SOACRId equals soacr.Id into soacrs
        ////                   from soacrsGroup in soacrs.DefaultIfEmpty()
        ////                   join invli in context.InvoiceLineItems
        ////                              on inv.Id equals invli.InvoiceId

        ////                   where (id == soacrsGroup.CustomerId)

        ////                   && soacrsGroup.SOACRTypeId == 2

        ////                   select new SoacrVM
        ////                   {
        ////                       Id = inv.Id,
        ////                       Customer = soacrsGroup.Customer.Name,
        ////                       InvoiceDateD = inv.DateModified ?? inv.DateCreated,
        ////                       InvoiceNumber = inv.InvoiceNumber,
        ////                       InvoiceType = inv.InvoiceType.Name,
        ////                       TotalAmountD = invli.Quantity * invli.UnitPrice
        ////                   };
        ////    //return await contract.ToListAsync();

        ////    var contractGroup = from cont in await contract.ToListAsync()
        ////                        orderby cont.Id, cont.Customer
        ////                        group cont by new
        ////                        {
        ////                            cont.Id,
        ////                            cont.Customer,
        ////                            cont.InvoiceDateD,
        ////                            cont.InvoiceNumber,
        ////                            cont.InvoiceType
        ////                        }
        ////                        into i
        ////                        select new SoacrVM
        ////                        {
        ////                            Id = i.Key.Id,
        ////                            Customer = i.Key.Customer,
        ////                            InvoiceDateD = i.Key.InvoiceDateD,
        ////                            InvoiceNumber = i.Key.InvoiceNumber,
        ////                            InvoiceType = i.Key.InvoiceType,
        ////                            TotalAmountD = i.Sum(s => s.TotalAmountD)


        ////                        };

        ////    return contractGroup.ToList();
        ////}

        ////public async Task<SoaDetailsPVM> GetCounterReceiptInvoices(int? id)
        ////{
        ////    var items = await GetContractInvoices3(id);
        ////    var cust = context.Customers.Where(s => s.Id == id).FirstOrDefault();

        ////    return new SoaDetailsPVM
        ////    {
        ////        CustomerId = cust == null? null : cust.Id,
        ////        CustomerCode = cust == null? String.Empty : cust.Code,
        ////        Customer = cust == null ? String.Empty : cust.Name,
        ////        Contents = items
        ////    };
        ////}





        //public async Task<IList<InvoiceVM>> GetContractInvoicesCustomer(int id, int soaId)
        //{
        //    var soadetails = (await GetSoaDetails(soaId)).Select(s => s.InvoiceId);

        //    var contract = from inv in context.Invoices
        //                   join soali in context.SOACRLineItems
        //                              on inv.Id equals soali.InvoiceId
        //                   join soacr in context.SOACRs
        //                              on soali.SOACRId equals soacr.Id into soacrs
        //                   from soacrsGroup in soacrs.DefaultIfEmpty()
        //                   join invli in context.InvoiceLineItems
        //                              on inv.Id equals invli.InvoiceId

        //                   where soacrsGroup.CustomerId == id && (!soadetails.Any(s => s == inv.Id))
        //                   && soacrsGroup.SOACRTypeId == 2

        //                   select new InvoiceVM
        //                   {
        //                       Id = inv.Id,
        //                       Customer = soacrsGroup.Customer.Name,
        //                       InvoiceDateD = inv.DateModified ?? inv.DateCreated,
        //                       InvoiceNumber = inv.InvoiceNumber,
        //                       InvoiceType = inv.InvoiceType.Name,
        //                       TotalAmountD = invli.Quantity * invli.UnitPrice
        //                   };
        //    //return await contract.ToListAsync();



        //    var contractGroup = from cont in await contract.ToListAsync()
        //                        orderby cont.Id, cont.Customer
        //                        group cont by new
        //                        {
        //                            cont.Id,
        //                            cont.Customer,
        //                            cont.InvoiceDateD,
        //                            cont.InvoiceNumber,
        //                            cont.InvoiceType
        //                        }
        //                        into i
        //                        select new InvoiceVM
        //                        {
        //                            Id = i.Key.Id,
        //                            Customer = i.Key.Customer,
        //                            InvoiceDateD = i.Key.InvoiceDateD,
        //                            InvoiceNumber = i.Key.InvoiceNumber,
        //                            InvoiceType = i.Key.InvoiceType,
        //                            TotalAmountD = i.Sum(s => s.TotalAmountD)


        //                        };

        //    return contractGroup.ToList();
        //}

        //public async Task<ForDataTables<InvoiceVM>> GetContractReceiptCustomerDTAsync(DataTablesRequestForm form, int id, int soaId)
        //{
        //    int totalRecord = 0;
        //    int filterRecord = 0;
        //    var data = (await GetContractInvoicesCustomer(id, soaId)).AsQueryable();
        //    //get total count of data in table
        //    totalRecord = data.Count();
        //    // search data when search value found
        //    if (!string.IsNullOrWhiteSpace(form.SearchValue))
        //    {
        //        data = data.Where(x => x.InvoiceDate.ToString().Contains(form.SearchValue)
        //                            || x.InvoiceNumber.ToString().Contains(form.SearchValue)
        //                            || x.InvoiceType.ToLower().Contains(form.SearchValue)
        //                            || x.TotalAmount.ToString().Contains(form.SearchValue));
        //    }
        //    // get total count of records after search
        //    filterRecord = data.Count();
        //    //sort data
        //    if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
        //    {
        //        if (form.SortColumnDirection == "asc")
        //        {
        //            switch (form.SortColumn)
        //            {
        //                case "InvoiceDate": data = data.OrderBy(s => s.InvoiceDate); break;
        //                case "InvoiceNumber": data = data.OrderBy(s => s.InvoiceNumber); break;
        //                case "InvoiceType": data = data.OrderBy(s => s.InvoiceType); break;
        //                case "TotalAmount": data = data.OrderBy(s => s.TotalAmount); break;
        //                default: break;
        //            }
        //        }
        //        else if (form.SortColumnDirection == "desc")
        //        {
        //            switch (form.SortColumn)
        //            {
        //                case "InvoiceDate": data = data.OrderByDescending(s => s.InvoiceDate); break;
        //                case "InvoiceNumber": data = data.OrderByDescending(s => s.InvoiceNumber); break;
        //                case "InvoiceType": data = data.OrderByDescending(s => s.InvoiceType); break;
        //                case "TotalAmount": data = data.OrderByDescending(s => s.TotalAmount); break;
        //                default: break;
        //            }
        //        }
        //    }
        //    //pagination
        //    var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
        //    return new ForDataTables<InvoiceVM>
        //    {
        //        Draw = form.Draw,
        //        RecordsTotal = totalRecord,
        //        RecordsFiltered = filterRecord,
        //        Data = pageList
        //    };
        //}

        //public async Task<IList<InvoiceVM>> GetContractReceiptDetailsAsync(int id)
        //{
        //    var contract = from inv in context.Invoices
        //                   join soali in context.SOACRLineItems
        //                              on inv.Id equals soali.InvoiceId
        //                   join soacr in context.SOACRs
        //                              on soali.SOACRId equals soacr.Id into soacrs
        //                   from soacrsGroup in soacrs.DefaultIfEmpty()

        //                   where inv.Id == id
        //                   select new InvoiceVM
        //                   {
        //                       Id = inv.Id,
        //                       Customer = soacrsGroup.Customer.Name,
        //                       InvoiceNumber = inv.InvoiceNumber,
        //                       TotalAmountD = inv.Amount,
        //                       TBRD = soali.TBRAmount,
        //                       CreditD = soali.CreditAmount,
        //                       DebitD = soali.DebitAmount,
        //                       PaidAmountD = soali.PaidAmount,
        //                       FreightChargeD = soali.FreightCost,
        //                       //BalanceD = soali.Balance
        //                   };
        //    return await contract.ToListAsync();
        //}

        //public async Task<ForDataTables<InvoiceVM>> GetContractReceiptDetailsDTAsync(DataTablesRequestForm form, int id)
        //{
        //    int totalRecord = 0;
        //    int filterRecord = 0;
        //    var data = (await GetContractReceiptDetailsAsync(id)).AsQueryable();
        //    //get total count of data in table
        //    totalRecord = data.Count();
        //    // search data when search value found
        //    if (!string.IsNullOrWhiteSpace(form.SearchValue))
        //    {
        //        data = data.Where(x => x.InvoiceNumber.ToString().Contains(form.SearchValue)
        //                            || x.TotalAmount.ToString().Contains(form.SearchValue)
        //                            || x.TBR.ToString().Contains(form.SearchValue)
        //                            || x.Credit.ToString().Contains(form.SearchValue)
        //                            || x.Debit.ToString().Contains(form.SearchValue)
        //                            || x.PaidAmount.ToString().Contains(form.SearchValue)
        //                            || x.FreightCharge.ToString().Contains(form.SearchValue)
        //                            || x.Balance.ToString().Contains(form.SearchValue));
        //    }
        //    // get total count of records after search
        //    filterRecord = data.Count();
        //    //sort data
        //    if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
        //    {
        //        if (form.SortColumnDirection == "asc")
        //        {
        //            switch (form.SortColumn)
        //            {
        //                case "InvoiceNumber": data = data.OrderBy(s => s.InvoiceNumber); break;
        //                case "TotalAmount": data = data.OrderBy(s => s.TotalAmountD); break;
        //                case "TBR": data = data.OrderBy(s => s.TBRD); break;
        //                case "Credit": data = data.OrderBy(s => s.CreditD); break;
        //                case "Debit": data = data.OrderBy(s => s.DebitD); break;
        //                case "PaidAmount": data = data.OrderBy(s => s.PaidAmountD); break;
        //                case "FreightCharge": data = data.OrderBy(s => s.FreightChargeD); break;
        //                case "Balance": data = data.OrderBy(s => s.BalanceD); break;
        //                default: break;
        //            }
        //        }
        //        else if (form.SortColumnDirection == "desc")
        //        {
        //            switch (form.SortColumn)
        //            {
        //                case "InvoiceNumber": data = data.OrderByDescending(s => s.InvoiceNumber); break;
        //                case "TotalAmount": data = data.OrderByDescending(s => s.TotalAmountD); break;
        //                case "TBR": data = data.OrderByDescending(s => s.TBRD); break;
        //                case "Credit": data = data.OrderByDescending(s => s.CreditD); break;
        //                case "Debit": data = data.OrderByDescending(s => s.DebitD); break;
        //                case "PaidAmount": data = data.OrderByDescending(s => s.PaidAmountD); break;
        //                case "FreightCharge": data = data.OrderByDescending(s => s.FreightChargeD); break;
        //                case "Balance": data = data.OrderByDescending(s => s.BalanceD); break;
        //                default: break;
        //            }
        //        }
        //    }
        //    //pagination
        //    var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
        //    return new ForDataTables<InvoiceVM>
        //    {
        //        Draw = form.Draw,
        //        RecordsTotal = totalRecord,
        //        RecordsFiltered = filterRecord,
        //        Data = pageList
        //    };
        //}

        ////public async Task<int> AddCrAsync(SoacrVM soaVM, int[] invoiceIds)
        ////{
        ////    var dateFrom = soaVM.SOAFromD;
        ////    var dateTo = soaVM.SOAToD;

        ////    var soa = new SOACR
        ////    {
        ////        CustomerId = soaVM.CustomerId,
        ////        SOACRTypeId = 2,
        ////        SOAFrom = null,
        ////        SOATo = null,
        ////        AmountToPay = soaVM.InvoiceAmountD,
        ////        TotalCMAAmount = soaVM.CreditD,
        ////        TotalDMAAmount = soaVM.DebitD,
        ////        TotalDMRAmount = soaVM.DMRD,
        ////        TotalBalance = soaVM.BalanceD,
        ////        TotalFreightCost = soaVM.FreightChargeD,
        ////        TotalAmount = soaVM.TotalAmountD,
        ////        OverPayment = soaVM.OverPaymentD,
        ////    };
        ////    await context.SOACRs.AddAsync(soa);
        ////    await context.SaveChangesAsync(currentUserId);


        ////    foreach (var invoiceId in invoiceIds)
        ////    {
        ////        var invoiceAmount = context.InvoiceLineItems.Where(s => s.InvoiceId == invoiceId).Sum(s => s.Quantity * s.UnitPrice);
        ////        var soali = new SOACRLineItem
        ////        {
        ////            SOACRId = soa.Id,
        ////            InvoiceId = invoiceId,
        ////            InvoiceAmount = invoiceAmount,
        ////            PaidAmount = 0,
        ////            TBRAmount = 0,
        ////            CreditAmount = 0,
        ////            DebitAmount = 0,
        ////            FreightCost = 0,
        ////            FreightWaybill = "0",
        ////            Balance = 0,
        ////            IsPaid = false
        ////        };
        ////        await context.SOACRLineItems.AddAsync(soali);
        ////        await context.SaveChangesAsync(currentUserId);
        ////    }

        ////    return soa.Id;

        ////}

        //public async Task AddCustomerPaymentAsync(PaymentVM paymentVM)
        //{
        //    var cp = new CustomerPayment
        //    {
        //        CustomerId = paymentVM.CustomerId,
        //        CRNumber = paymentVM.CRNumber,
        //        Remarks = paymentVM.Remarks,
        //        OverPayment = paymentVM.OverPaymentD,
        //        TKTSolicitation = paymentVM.TKTD,
        //        Tax = paymentVM.TaxD,
        //        CashAdvance = paymentVM.CashAdvancedD,
        //        Discount = paymentVM.DiscountD,
        //        OffSet = paymentVM.OffsetD,
        //        Cash = paymentVM.CashAmountD,
        //        BackLoad = paymentVM.BackLoadD,
        //        SOACRId = paymentVM.SOANo
        //    };
        //    await context.CustomerPayments.AddAsync(cp);
        //    await context.SaveChangesAsync(currentUserId);

        //    var sc = new SOACRCheck
        //    {
        //        CustomerPaymentId = cp.Id,
        //        BankName = paymentVM.BankName,
        //        BankBranch = paymentVM.BankBranch,
        //        CheckNumber = paymentVM.CheckNo,
        //        CheckAmount = paymentVM.CheckAmountD,
        //        CheckDate = paymentVM.CheckDateD
        //    };

        //    await context.SOACRChecks.AddAsync(sc);
        //    await context.SaveChangesAsync(currentUserId);
        //}

        //public async Task<bool> IsSOAIdExist(int id)
        //{
        //    return await context.SOACRs.AnyAsync(c => c.Id == id);
        //}

        ////public async Task<SoacrPaymentDetails> GetPaymentDetails(SoacrPaymentFilter filter)
        ////{
        ////    var payment = await context.CustomerPayments
        ////                               .Where(s => s.CustomerId == filter.CustomerId
        ////                                   && s.SOACRId == filter.SOACRId
        ////                                   && s.InvoiceId == filter.InvoiceId)
        ////                               .FirstOrDefaultAsync();
        ////    if (payment == null)
        ////    {
        ////        payment = new CustomerPayment
        ////        {
        ////            BackLoad = 0,
        ////            Cash = 0,
        ////            CashAdvance = 0,
        ////            CRNumber = "",
        ////            CustomerId = filter.CustomerId == null ? 0 : filter.CustomerId.Value,
        ////            Discount = 0,
        ////            InvoiceId = filter.InvoiceId,
        ////            OffSet = 0,
        ////            OverPayment = 0,
        ////            Remarks = "",
        ////            SOACRId = filter.SOACRId == null ? 0 : filter.SOACRId.Value,
        ////            Tax = 0,
        ////            TKTSolicitation = 0,
        ////            TotalAmountPaid = 0,
        ////            TotalInvoiceAmount = 0
        ////        };
        ////        await context.CustomerPayments.AddAsync(payment);
        ////        await context.SaveChangesAsync(currentUserId);
        ////    }
        ////    return new SoacrPaymentDetails
        ////    {
        ////        BackLoad = payment.BackLoad,
        ////        CashAdvance = payment.CashAdvance,
        ////        CashAmount = payment.Cash,
        ////        CustomerReceiptNumber = payment.CRNumber,
        ////        Discount = payment.Discount,
        ////        Id = payment.Id,
        ////        Offset = payment.OffSet,
        ////        OverPayment = payment.OverPayment,
        ////        Remarks = payment.Remarks,
        ////        Tax = payment.Tax,
        ////        TKTSolicitation = payment.TKTSolicitation
        ////    };
        ////}

        ////private async Task<IList<SoacrPaymentCheck>> GetPaymentChecks(int customerPaymentId)
        ////{
        ////    var soacrChecks = from check in context.SOACRChecks
        ////                      where check.CustomerPaymentId == customerPaymentId
        ////                      select new SoacrPaymentCheck
        ////                      {
        ////                          BankBranch = check.BankBranch,
        ////                          BankName = check.BankName,
        ////                          CheckAmountD = check.CheckAmount,
        ////                          CheckDateD = check.CheckDate,
        ////                          CheckNumber = check.CheckNumber,
        ////                          Id = check.Id
        ////                      };
        ////    return await soacrChecks.ToListAsync();
        ////}

        ////public async Task<ForDataTables<SoacrPaymentCheck>> GetPaymentChecksDTAsync(DataTablesRequestForm form, int customerPaymentId)
        ////{
        ////    int totalRecord = 0;
        ////    int filterRecord = 0;
        ////    var data = (await GetPaymentChecks(customerPaymentId)).AsQueryable();
        ////    //get total count of data in table
        ////    totalRecord = data.Count();
        ////    // search data when search value found
        ////    if (!string.IsNullOrWhiteSpace(form.SearchValue))
        ////    {
        ////        data = data.Where(x => x.BankBranch.ToLower().Contains(form.SearchValue)
        ////                            || x.BankName.ToLower().Contains(form.SearchValue)
        ////                            || x.CheckAmountD.ToString().Contains(form.SearchValue)
        ////                            || x.CheckDate.ToLower().Contains(form.SearchValue)
        ////                            || x.CheckNumber.ToLower().Contains(form.SearchValue));
        ////    }
        ////    // get total count of records after search
        ////    filterRecord = data.Count();
        ////    //sort data
        ////    if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
        ////    {
        ////        if (form.SortColumnDirection == "asc")
        ////        {
        ////            switch (form.SortColumn)
        ////            {
        ////                case "BankBranch": data = data.OrderBy(s => s.BankBranch); break;
        ////                case "BankName": data = data.OrderBy(s => s.BankName); break;
        ////                case "CheckAmount": data = data.OrderBy(s => s.CheckAmountD); break;
        ////                case "CheckDate": data = data.OrderBy(s => s.CheckDateD); break;
        ////                case "CheckNumber": data = data.OrderBy(s => s.CheckNumber); break;
        ////                default: break;
        ////            }
        ////        }
        ////        else if (form.SortColumnDirection == "desc")
        ////        {
        ////            switch (form.SortColumn)
        ////            {
        ////                case "BankBranch": data = data.OrderByDescending(s => s.BankBranch); break;
        ////                case "BankName": data = data.OrderByDescending(s => s.BankName); break;
        ////                case "CheckAmount": data = data.OrderByDescending(s => s.CheckAmountD); break;
        ////                case "CheckDate": data = data.OrderByDescending(s => s.CheckDateD); break;
        ////                case "CheckNumber": data = data.OrderByDescending(s => s.CheckNumber); break;
        ////                default: break;
        ////            }
        ////        }
        ////    }
        ////    //pagination
        ////    var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
        ////    return new ForDataTables<SoacrPaymentCheck>
        ////    {
        ////        Draw = form.Draw,
        ////        RecordsTotal = totalRecord,
        ////        RecordsFiltered = filterRecord,
        ////        Data = pageList
        ////    };
        ////}

        ////public async Task<bool> AddPaymentCheck(SoacrPaymentCheck check)
        ////{
        ////    var soacrCheck = new SOACRCheck()
        ////    {
        ////        BankBranch = check.BankBranch,
        ////        BankName = check.BankName,
        ////        CheckAmount = check.CheckAmountD,
        ////        CheckDate = check.CheckDateD,
        ////        CheckNumber = check.CheckNumber,
        ////        CustomerPaymentId = check.CustomerPaymentId == null ? 0 : check.CustomerPaymentId.Value
        ////    };
        ////    await context.SOACRChecks.AddAsync(soacrCheck);
        ////    await context.SaveChangesAsync(currentUserId);

        ////    return true;
        ////}

        ////public async Task<bool> RemovePaymentCheck(int soaCheckId)
        ////{
        ////    var soacrCheck = await context.SOACRChecks.Where(s => s.Id == soaCheckId).FirstOrDefaultAsync();
        ////    if (soacrCheck == null)
        ////    {
        ////        return false;
        ////    }
        ////    context.SOACRChecks.Remove(soacrCheck);
        ////    await context.SaveChangesAsync(currentUserId);

        ////    return true;
        ////}


        //public async Task<bool> IsSoacrTypeCR(int id)
        //{
        //    return await context.SOACRs.AnyAsync(c => c.Id == id && c.SOACRTypeId == 2);
        //}



    }
}