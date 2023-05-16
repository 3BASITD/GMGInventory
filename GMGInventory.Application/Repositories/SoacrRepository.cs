using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq;

namespace GMGInventory.Application.Repositories
{
    public class SoacrRepository : ISoacrRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly string currentUser;

        public SoacrRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
            , UserManager<User> userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.currentUser = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
        }


        #region for SOACR/Index
        private async Task<IList<SOACRVM>> SOACRList()
        {
            var soacr = from soa in context.SOACRs
                        let soalit = context.SOACRLineItems.Where(s => s.SOACRId == soa.Id).ToList()
                        select new SOACRVM
                        {
                            Id = soa.Id,
                            Customer = soa.Customer.Name,
                            Type = soa.SOACRType.Code,
                            SOAFromD = soa.SOAFrom,
                            SOAToD = soa.SOATo,
                            TotalAmountD = soa.AmountToPay,
                            TBRD = soalit.Sum(s => s.TBRAmount),
                            CreditD = soalit.Sum(s => s.CreditAmount),
                            DebitD = soalit.Sum(s => s.DebitAmount),
                            PaidAmountD = soalit.Sum(s => s.PaidAmount),
                            FreightChargeD = soalit.Sum(s => s.FreightCost)
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

        //public async Task<SoaDetailsPVM> PrintCRDetails(int id)
        //{
        //    var items = await GetSoaDetails(id);

        //    var soaVM = (from soa in context.SOACRs
        //                                      .Include(s => s.Customer)
        //                 join sl in context.SOACRLineItems
        //                             on soa.Id equals sl.SOACRId into soali
        //                 from soaliGroup in soali.DefaultIfEmpty()
        //                 join i in context.Invoices
        //                                  on soaliGroup.InvoiceId equals i.Id
        //                 where soa.Id == id
        //                 select new SoaDetailsPVM
        //                 {
        //                     Id = soa.Id,
        //                     Contents = items,
        //                     DateFromD = soa.SOAFrom,
        //                     DateToD = soa.SOATo,
        //                     SoaNo = soa.Id,
        //                     Agent = soa.Customer.Agent.FirstName,
        //                     Customer = soa.Customer.Name,
        //                     CreditTerm = soa.Customer.CreditTerm.Name,
        //                     CRDateD = soa.DateModified ?? soa.DateCreated,
        //                     CustomerCode = soa.Customer.Code
        //                     //}).FirstOrDefault();
        //                 }).First();

        //    return new SoaDetailsPVM
        //    {
        //        Id = soaVM.Id,
        //        Contents = items,
        //        DateFromD = soaVM.DateFromD,
        //        DateToD = soaVM.DateToD,
        //        SoaNo = soaVM.SoaNo,
        //        Agent = soaVM.Agent,
        //        Customer = soaVM.Customer,
        //        CreditTerm = soaVM.CreditTerm,
        //        TotalAmountD = items.Sum(s => s.InvoiceAmountDeductedD),
        //        TotalInvoiceAmountD = items.Sum(s => s.InvoiceAmountDeductedD),
        //        CRDateD = soaVM.CRDateD,
        //        CustomerCode = soaVM.CustomerCode
        //    };
        //}

        public async Task<int> GenerateSOAAsync(GenerateSOAVM soaVM)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var soaType = await context.SOACRTypes.Where(s => s.Name == "Statement of Account").Select(s => s.Id).FirstAsync();

            var invoices = await (from invoice in context.Invoices
                                  where invoice.SalesOrder.Customer.Id == soaVM.Customer
                                  && soaVM.DateFrom <= (invoice.DateModified ?? invoice.DateCreated)
                                  && (invoice.DateModified ?? invoice.DateCreated) <= soaVM.DateTo
                                  select invoice).ToListAsync();
            var soa = new SOACR
            {
                CustomerId = soaVM.Customer,
                SOACRTypeId = soaType,
                SOAFrom = soaVM.DateFrom,
                SOATo = soaVM.DateTo,
                AmountToPay = invoices.Sum(s => s.Amount),
                TotalCMAAmount = 0,
                TotalDMAAmount = 0,
                TotalDMRAmount = 0,
                TotalBalance = 0,
                TotalFreightCost = 0,
                TotalAmount = 0,
                OverPayment = 0,
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
                    TBRAmount = 0,
                    CreditAmount = 0,
                    DebitAmount = 0,
                    FreightCost = 0,
                    FreightWaybill = "",
                    Balance = 0,
                    IsPaid = false
                };
                await context.SOACRLineItems.AddAsync(inv);
                await context.SaveChangesAsync(currentUserId);
            }

            return soa.Id;
        }

		#endregion

		#region for SOACT/Invoice
		private async Task<IList<SOACRInvoices>> GetInvoiceListAsync(ListFilterSalesModel filter)
		{
			var invoices = from inv in context.Invoices
						   where (filter.Customer == null || filter.Customer == inv.SalesOrder.Customer.Id)
							  && (filter.Agent == null || filter.Agent == inv.SalesOrder.Agent.Id)
							  && filter.DateFrom <= (inv.DateModified ?? inv.DateCreated).Date
							  && (inv.DateModified ?? inv.DateCreated).Date <= filter.DateTo
							  && inv.Status.Name == "Invoice Submitted"
						   select new SOACRInvoices
						   {
                               Agent = inv.SalesOrder.Agent.FirstName + " " + inv.SalesOrder.Agent.LastName,
							   Customer = inv.SalesOrder.Customer.Name,
							   InvoiceDateD = inv.DateModified ?? inv.DateCreated,
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

        public SoacrVM? GetSoacrVM(int? id)
        {
            var soaVM = (from soa in context.SOACRs
                                            .Include(s => s.Customer)
                         join st in context.SOACRTypes
                                          on soa.SOACRTypeId equals st.Id
                         //let soalit = context.SOACRLineItems.Where(s => s.SOACRId == soa.Id).Sum(s => s.TBRAmount)
                         //let soalic = context.SOACRLineItems.Where(s => s.SOACRId == soa.Id).Sum(s => s.CreditAmount)
                         //let soalid = context.SOACRLineItems.Where(s => s.SOACRId == soa.Id).Sum(s => s.DebitAmount)
                         //let soalip = context.SOACRLineItems.Where(s => s.SOACRId == soa.Id).Sum(s => s.PaidAmount)
                         //let soalif = context.SOACRLineItems.Where(s => s.SOACRId == soa.Id).Sum(s => s.FreightCost)
                         //let soalib = context.SOACRLineItems.Where(s => s.SOACRId == soa.Id).Sum(s => s.Balance)
                         let soal = context.SOACRLineItems.Where(s => s.SOACRId == soa.Id).ToList()

                         where soa.Id == id
                         select new SoacrVM
                         {
                             Id = soa.Id,
                             CustomerId = soa.CustomerId,
                             CustomerName = soa.Customer.Name,
                             Type = st.Name,
                             SOAFromD = soa.SOAFrom,
                             SOAToD = soa.SOATo,
                             TotalAmountD = soa.TotalAmount,
                             InvoiceAmountD = soa.TotalAmount == null ? 0 : soa.TotalAmount.Value,
                             TBRD = soal.Sum(s => s.TBRAmount),
                             CreditD = soal.Sum(s => s.CreditAmount),
                             DebitD = soal.Sum(s => s.DebitAmount),
                             PaidAmountD = soal.Sum(s => s.PaidAmount),
                             FreightChargeD = soal.Sum(s => s.FreightCost),
                             //BalanceD = soalib,
                             //OverPaymentD = soa.OverPayment
                         }).FirstOrDefault();
            return soaVM;
        }

        public InvoiceVM? GetInvoiceVM(int? id)
        {
            var soaVM = (from inv in context.Invoices
                         join soali in context.SOACRLineItems
                                    on inv.Id equals soali.InvoiceId
                         join soacr in context.SOACRs
                                    on soali.SOACRId equals soacr.Id into soacrs
                         from soacrsGroup in soacrs.DefaultIfEmpty()

                         where inv.Id == id
                         select new InvoiceVM
                         {
                             Id = inv.Id,
                             Customer = soacrsGroup.Customer.Name,
                             InvoiceDateD = inv.DateCreated,
                             InvoiceNumber = inv.InvoiceNumber,
                             InvoiceType = inv.InvoiceType.Name,
                             TotalAmountD = inv.Amount
                         }).FirstOrDefault();
            return soaVM;
        }

        public async Task<IList<SoacrVM>> GetAll2Async(SoaFilterVM filter)
        {
            var soas = from soa in context.SOACRs
                                            .Include(s => s.Customer)
                       join sl in context.SOACRLineItems
                                        on soa.Id equals sl.SOACRId into soali
                       from soaliGroup in soali.DefaultIfEmpty()
                       join inv in context.Invoices
                                        on soaliGroup.InvoiceId equals inv.Id
                       join cust in context.Customers
                                        on soa.CustomerId equals cust.Id
                       join agent in context.Agents
                                        on cust.AgentId equals agent.Id

                       join st in context.SOACRTypes
                                        on soa.SOACRTypeId equals st.Id

                       where (filter.CustomerId == null || filter.CustomerId == soa.CustomerId)
                           && (filter.AgentId == null || filter.AgentId == agent.Id)
                       && filter.DateFrom <= (inv.DateModified ?? inv.DateCreated).Date
                       && (inv.DateModified ?? inv.DateCreated).Date <= filter.DateTo

                       let soalit = context.SOACRLineItems.Where(s => s.SOACRId == soa.Id)

                       select new SoacrVM
                       {
                           Id = soa.Id,
                           CustomerId = soa.CustomerId,
                           CustomerName = soa.Customer.Name,
                           Type = st.Code,
                           SOAFromD = soa.SOAFrom,
                           SOAToD = soa.SOATo,
                           TotalAmountD = soa.TotalAmount,
                           InvoiceDateD = inv.DateModified ?? inv.DateCreated,
                           InvoiceNumber = inv.InvoiceNumber,
                           InvoiceType = inv.InvoiceType.Name,
                           InvoiceAmountD = inv.Amount,
                           TBRD = soalit.Sum(s => s.TBRAmount),
                           CreditD = soalit.Sum(s => s.CreditAmount),
                           DebitD = soalit.Sum(s => s.CreditAmount),
                           PaidAmountD = soalit.Sum(s => s.PaidAmount),
                           FreightChargeD = soalit.Sum(s => s.FreightCost),
                           //BalanceD = soalit.Sum(s => s.Balance),
                           //OverPaymentD = soa.OverPayment
                       };
            var grpsoas = from invoice in await soas.ToListAsync()
                          orderby invoice.Id ascending, invoice.CustomerId ascending, invoice.CustomerName ascending,
                          invoice.Type, invoice.SOAFromD, invoice.SOAToD, invoice.TotalAmountD, invoice.OverPaymentD,
                          invoice.InvoiceDateD, invoice.InvoiceNumber, invoice.InvoiceType
                          group invoice by new
                          {
                              invoice.Id,
                              invoice.CustomerId,
                              invoice.CustomerName,
                              invoice.Type,
                              invoice.SOAFromD,
                              invoice.SOAToD,
                              invoice.TotalAmountD,
                              invoice.OverPaymentD,
                              invoice.InvoiceDateD,
                              invoice.InvoiceNumber,
                              invoice.InvoiceType
                          } into i
                          select new SoacrVM
                          {
                              Id = i.Key.Id,
                              CustomerId = i.Key.CustomerId,
                              CustomerName = i.Key.CustomerName,
                              Type = i.Key.Type,
                              SOAFromD = i.Key.SOAFromD,
                              SOAToD = i.Key.SOAToD,
                              TotalAmountD = i.Key.TotalAmountD,
                              TBRD = i.Sum(s => s.TBRD),
                              CreditD = i.Sum(s => s.CreditD),
                              DebitD = i.Sum(s => s.DebitD),
                              PaidAmountD = i.Sum(s => s.PaidAmountD),
                              FreightChargeD = i.Sum(s => s.FreightChargeD),
                              //BalanceD = i.Sum(s => s.BalanceD),
                              //OverPaymentD = i.Key.OverPaymentD,
                              InvoiceDateD = i.Key.InvoiceDateD,
                              InvoiceNumber = i.Key.InvoiceNumber,
                              InvoiceType = i.Key.InvoiceType,
                              InvoiceAmountD = i.Sum(s => s.InvoiceAmountD)
                          };
            return grpsoas.ToList();
        }

        public async Task<ForDataTables<SoacrVM>> GetAllForDataTables2Async(DataTablesRequestForm form, SoaFilterVM filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetAll2Async(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.CustomerName.ToLower().Contains(form.SearchValue)
                                    || x.CRNumber.ToString().Contains(form.SearchValue)
                                    || x.Type.ToLower().Contains(form.SearchValue)
                                    || x.SOAFrom.ToString().Contains(form.SearchValue)
                                    || x.SOATo.ToString().Contains(form.SearchValue)
                                    || x.TotalAmount.ToString().Contains(form.SearchValue)
                                    || x.TBR.ToString().Contains(form.SearchValue)
                                    || x.Credit.ToString().Contains(form.SearchValue)
                                    || x.Debit.ToString().Contains(form.SearchValue)
                                    || x.PaidAmount.ToString().Contains(form.SearchValue)
                                    || x.FreightCharge.ToString().Contains(form.SearchValue)
                                    || x.OverPayment.ToLower().Contains(form.SearchValue));
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
                        case "CustomerName": data = data.OrderBy(s => s.CustomerName); break;
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
                        case "CustomerName": data = data.OrderByDescending(s => s.CustomerName); break;
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
            return new ForDataTables<SoacrVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public SoacrLineItemVM? GetSoaInvoiceDtlVM(int? id)
        {
            var soaVM = (from soa in context.SOACRs
                                            .Include(s => s.Customer)
                         join sl in context.SOACRLineItems
                                     on soa.Id equals sl.SOACRId into soali
                         from soaliGroup in soali.DefaultIfEmpty()
                         join i in context.Invoices
                                          on soaliGroup.InvoiceId equals i.Id
                         where soaliGroup.Id == id
                         select new SoacrLineItemVM
                         {
                             Id = soaliGroup.Id,
                             CustomerId = soa.CustomerId,
                             TBRD = soaliGroup.TBRAmount,
                             DebitD = soaliGroup.DebitAmount,
                             CreditD = soaliGroup.CreditAmount
                         }).FirstOrDefault();
            return soaVM;
        }

        public async Task<IList<SoacrVM>> GetSOAInvoice(/*int id*/)
        {
            var inv = from soa in context.SOACRs
                                            .Include(s => s.Customer)
                      join sl in context.SOACRLineItems
                                  on soa.Id equals sl.SOACRId into soali
                      from soaliGroup in soali.DefaultIfEmpty()
                      join i in context.Invoices
                                       on soaliGroup.InvoiceId equals i.Id
                      //where soa.Id == id
                      select new SoacrVM
                      {
                          Id = soa.Id,
                          InvoiceDateD = i.DateCreated,
                          InvoiceNumber = i.InvoiceNumber,
                          Type = i.InvoiceType.Name,
                          InvoiceAmountD = i.Amount,
                          FreightChargeD = soaliGroup.FreightCost
                      };
            return await inv.ToListAsync();
        }

        public async Task<IList<SoacrVM>> GetSoaDetailsAsync(int id)
        {
            //var soaVM = from soa in context.SOACRs
            //                                .Include(s => s.Customer)
            //            join sl in context.SOACRLineItems
            //                        on soa.Id equals sl.SOACRId into soali
            //            from soaliGroup in soali.DefaultIfEmpty()
            //            join i in context.Invoices
            //                             on soaliGroup.InvoiceId equals i.Id
            //            where (soa.Id == id) 
            //            select new SoacrVM
            //            {
            //                Id = soaliGroup.Id,
            //                DateFromD = soa.SOAFrom,
            //                DateToD = soa.SOATo,
            //                CreditTerm = soa.Customer.CreditTerm.Name,
            //                SoaNo = soa.Id,
            //                Agent = soa.Customer.Agent.FirstName,
            //                CustomerName = soa.Customer.Name,
            //                InvoiceType = i.InvoiceType.Name,
            //                CustomerId = soa.CustomerId,
            //                InvoiceId = soaliGroup.InvoiceId,
            //                InvoiceDateD = i.DateCreated,
            //                InvoiceNumber = i.InvoiceNumber.Remove(0, 4),
            //                InvoiceAmountD = i.Amount,
            //                //InvoiceAmountDeductedD = i.Amount - soaliGroup.FreightCost - soaliGroup.DebitAmount - soaliGroup.CreditAmount,
            //                TBRD = soaliGroup.TBRAmount,
            //                CreditD = soaliGroup.CreditAmount,
            //                DebitD = soaliGroup.DebitAmount,
            //                PaidAmountD = soaliGroup.PaidAmount,
            //                FreightChargeD = soaliGroup.FreightCost,
            //                //BalanceD = soaliGroup.Balance
            //            };
            //return await soaVM.ToListAsync();

            var soa = from sl in context.SOACRLineItems
                      where sl.SOACR.Id == id
                      select new SoacrVM
                      {
                          Id = sl.Id,
                          DateFromD = sl.SOACR.SOAFrom,
                          DateToD = sl.SOACR.SOATo,
                          CreditTerm = sl.SOACR.Customer.CreditTerm.Name,
                          SoaNo = sl.SOACR.Id,
                          Agent = sl.Invoice.SalesOrder.Agent.FirstName,
                          CustomerName = sl.SOACR.Customer.Name,
                          InvoiceType = sl.Invoice.InvoiceType.Name,
                          CustomerId = sl.SOACR.CustomerId,
                          InvoiceId = sl.InvoiceId,
                          InvoiceDateD = sl.Invoice.DateModified ?? sl.Invoice.DateCreated,
                          InvoiceNumber = sl.Invoice.InvoiceNumber.Remove(0, 4),
                          InvoiceAmountD = sl.Invoice.Amount,
                          TBRD = sl.TBRAmount,
                          CreditD = sl.CreditAmount,
                          DebitD = sl.DebitAmount,
                          PaidAmountD = sl.PaidAmount,
                          FreightChargeD = sl.FreightCost
                      };
            return await soa.ToListAsync();
        }

        public async Task<ForDataTables<SoacrVM>> GetSoaDetailsDTAsync(DataTablesRequestForm form, int id)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetSoaDetailsAsync(id)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.InvoiceDate.ToLower().Contains(form.SearchValue)
                                    || x.InvoiceNumber.ToLower().Contains(form.SearchValue)
                                    || x.InvoiceAmountD.ToString().Contains(form.SearchValue)
                                    || x.TBRD.ToString().Contains(form.SearchValue)
                                    || x.CreditD.ToString().Contains(form.SearchValue)
                                    || x.DebitD.ToString().Contains(form.SearchValue)
                                    || (x.PaidAmountD ?? 0).ToString().Contains(form.SearchValue)
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
                        case "InvoiceAmount": data = data.OrderBy(s => s.InvoiceAmountD); break;
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
                        case "InvoiceAmount": data = data.OrderByDescending(s => s.InvoiceAmountD); break;
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
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<SoacrVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public async Task<IList<SoacrVM>> GetSoaDetails(int id)
        {
            var soaVM = from soa in context.SOACRs
                                              .Include(s => s.Customer)
                        join sl in context.SOACRLineItems
                                    on soa.Id equals sl.SOACRId into soali
                        from soaliGroup in soali.DefaultIfEmpty()
                        join i in context.Invoices
                                         on soaliGroup.InvoiceId equals i.Id
                        where (soa.Id == id)
                        select new SoacrVM
                        {
                            Id = soa.Id,
                            DateFromD = soa.SOAFrom,
                            DateToD = soa.SOATo,
                            CreditTerm = soa.Customer.CreditTerm.Name,
                            SoaNo = soa.Id,
                            Agent = soa.Customer.Agent.FirstName,
                            CustomerName = soa.Customer.Name,
                            InvoiceType = i.InvoiceType.Name,
                            CustomerId = soa.CustomerId,
                            InvoiceId = soaliGroup.InvoiceId,
                            WaybillNo = soaliGroup.FreightWaybill ?? "-",
                            InvoiceNumber = i.InvoiceNumber.Remove(0, 4),
                            InvoiceAmountD = i.Amount,
                            //InvoiceAmountDeductedD = i.Amount - soaliGroup.FreightCost - soaliGroup.DebitAmount - soaliGroup.CreditAmount,
                            InvoiceDateD = i.DateModified ?? i.DateCreated,
                            TBRD = soaliGroup.TBRAmount,
                            CreditD = soaliGroup.CreditAmount,
                            DebitD = soaliGroup.DebitAmount,
                            PaidAmountD = soaliGroup.PaidAmount,
                            FreightChargeD = soaliGroup.FreightCost,
                            //BalanceD = soaliGroup.Balance,
                            TotalAmountD = i.Amount
                        };
            return await soaVM.ToListAsync();
        }

        public async Task<SoaDetailsPVM> PrintSoaDetails(int id)
        {
            var items = await GetSoaDetails(id);

            var soaVM = (from soa in context.SOACRs
                                              .Include(s => s.Customer)
                         join sl in context.SOACRLineItems
                                     on soa.Id equals sl.SOACRId into soali
                         from soaliGroup in soali.DefaultIfEmpty()
                         join i in context.Invoices
                                          on soaliGroup.InvoiceId equals i.Id
                         where soa.Id == id
                         select new SoaDetailsPVM
                         {
                             Id = soa.Id,
                             Contents = items,
                             DateFromD = soa.SOAFrom,
                             DateToD = soa.SOATo,
                             SoaNo = soa.Id,
                             Agent = soa.Customer.Agent.FirstName,
                             Customer = soa.Customer.Name,
                             CreditTerm = soa.Customer.CreditTerm.Name
                             //}).FirstOrDefault();
                         }).First();

            return new SoaDetailsPVM
            {
                Id = soaVM.Id,
                Contents = items,
                DateFromD = soaVM.DateFromD,
                DateToD = soaVM.DateToD,
                SoaNo = soaVM.SoaNo,
                Agent = soaVM.Agent,
                Customer = soaVM.Customer,
                CreditTerm = soaVM.CreditTerm,
                TotalAmountD = items.Sum(s => s.InvoiceAmountDeductedD),
                TotalInvoiceAmountD = items.Sum(s => s.InvoiceAmountDeductedD)

            };
        }

        public async Task<IList<SoacrVM>> GetContractInvoices2(SoaFilterVM filter)
        {
            var contract = from inv in context.Invoices
                           join soali in context.SOACRLineItems
                                      on inv.Id equals soali.InvoiceId
                           join soacr in context.SOACRs
                                      on soali.SOACRId equals soacr.Id into soacrs
                           from soacrsGroup in soacrs.DefaultIfEmpty()
                           join invli in context.InvoiceLineItems
                                      on inv.Id equals invli.InvoiceId

                           where (filter.CustomerId == soacrsGroup.CustomerId)

                           && soacrsGroup.SOACRTypeId == 2

                           select new SoacrVM
                           {
                               Id = inv.Id,
                               Customer = soacrsGroup.Customer.Name,
                               InvoiceDateD = inv.DateModified ?? inv.DateCreated,
                               InvoiceNumber = inv.InvoiceNumber,
                               InvoiceType = inv.InvoiceType.Name,
                               TotalAmountD = invli.Quantity * invli.UnitPrice
                           };
            //return await contract.ToListAsync();

            var contractGroup = from cont in await contract.ToListAsync()
                                orderby cont.Id, cont.Customer
                                group cont by new
                                {
                                    cont.Id,
                                    cont.Customer,
                                    cont.InvoiceDateD,
                                    cont.InvoiceNumber,
                                    cont.InvoiceType
                                }
                                into i
                                select new SoacrVM
                                {
                                    Id = i.Key.Id,
                                    Customer = i.Key.Customer,
                                    InvoiceDateD = i.Key.InvoiceDateD,
                                    InvoiceNumber = i.Key.InvoiceNumber,
                                    InvoiceType = i.Key.InvoiceType,
                                    TotalAmountD = i.Sum(s => s.TotalAmountD)


                                };

            return contractGroup.ToList();


        }

        public async Task<ForDataTables<SoacrVM>> GetContractReceipt2DTAsync(DataTablesRequestForm form, SoaFilterVM filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetContractInvoices2(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.InvoiceDate.ToString().Contains(form.SearchValue)
                                    || x.InvoiceNumber.ToString().Contains(form.SearchValue)
                                    || x.InvoiceType.ToLower().Contains(form.SearchValue)
                                    || x.TotalAmount.ToString().Contains(form.SearchValue));
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
                        case "InvoiceDate": data = data.OrderBy(s => s.InvoiceDate); break;
                        case "InvoiceNumber": data = data.OrderBy(s => s.InvoiceNumber); break;
                        case "InvoiceType": data = data.OrderBy(s => s.InvoiceType); break;
                        case "TotalAmount": data = data.OrderBy(s => s.TotalAmount); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "InvoiceDate": data = data.OrderByDescending(s => s.InvoiceDate); break;
                        case "InvoiceNumber": data = data.OrderByDescending(s => s.InvoiceNumber); break;
                        case "InvoiceType": data = data.OrderByDescending(s => s.InvoiceType); break;
                        case "TotalAmount": data = data.OrderByDescending(s => s.TotalAmount); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<SoacrVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }
        public async Task<IList<SoacrVM>> GetSoaDetails2(int id, int custId)
        {
            var soadetails = (await GetSoaDetails(id)).Select(s => s.InvoiceId);

            var contract = from inv in context.Invoices
                           join soali in context.SOACRLineItems
                                      on inv.Id equals soali.InvoiceId
                           join soacr in context.SOACRs
                                      on soali.SOACRId equals soacr.Id into soacrs
                           from soacrsGroup in soacrs.DefaultIfEmpty()
                           join invli in context.InvoiceLineItems
                                      on inv.Id equals invli.InvoiceId

                           where custId == soacrsGroup.Customer.Id && (!soadetails.Any(s => s == inv.Id))
                           && soacrsGroup.SOACRTypeId == 2

                           select new SoacrVM
                           {
                               Id = inv.Id,
                               Customer = soacrsGroup.Customer.Name,
                               InvoiceDateD = inv.DateModified ?? inv.DateCreated,
                               InvoiceNumber = inv.InvoiceNumber,
                               InvoiceType = inv.InvoiceType.Name,
                               TotalAmountD = invli.Quantity * invli.UnitPrice
                           };
            //return await contract.ToListAsync();



            var contractGroup = from cont in await contract.ToListAsync()
                                orderby cont.Id, cont.Customer
                                group cont by new
                                {
                                    cont.Id,
                                    cont.Customer,
                                    cont.InvoiceDateD,
                                    cont.InvoiceNumber,
                                    cont.InvoiceType
                                }
                                into i
                                select new SoacrVM
                                {
                                    Id = i.Key.Id,
                                    Customer = i.Key.Customer,
                                    InvoiceDateD = i.Key.InvoiceDateD,
                                    InvoiceNumber = i.Key.InvoiceNumber,
                                    InvoiceType = i.Key.InvoiceType,
                                    TotalAmountD = i.Sum(s => s.TotalAmountD)


                                };

            return contractGroup.ToList();
        }

        public async Task<SoaDetailsPVM> GetContractInvoicesCustomer2(int id, int custId)
        {
            var items = await GetSoaDetails2(id, custId);

            /*weirdo ng gumawa neto tapos need niya lang id
             var soaVM = (from soa in context.SOACRs
                                             .Include(s => s.Customer)
                        join sl in context.SOACRLineItems
                                    on soa.Id equals sl.SOACRId into soali
                        from soaliGroup in soali.DefaultIfEmpty()
                        join i in context.Invoices
                                         on soaliGroup.InvoiceId equals i.Id
                        where (soa.Id == id)
                        select new SoacrVM
                        {
                            Id = soa.Id,
                            DateFromD = soa.SOAFrom,
                            DateToD = soa.SOATo,
                            CreditTerm = soa.Customer.CreditTerm.Name,
                            SoaNo = soa.Id,
                            Agent = soa.Customer.Agent.FirstName,
                            CustomerName = soa.Customer.Name,
                            InvoiceType = i.InvoiceType.Name,
                            CustomerId = soa.CustomerId,
                            InvoiceId = soaliGroup.InvoiceId,
                            WaybillNo = soaliGroup.FreightWaybill ?? "-",
                            InvoiceNumber = i.InvoiceNumber,
                            InvoiceAmountD = i.Amount,// - soaliGroup.FreightCost,
                            InvoiceDateD = i.DateModified ?? i.DateCreated,
                            TBRD = soaliGroup.TBRAmount,
                            CreditD = soaliGroup.CreditAmount,
                            DebitD = soaliGroup.DebitAmount,
                            PaidAmountD = soaliGroup.PaidAmount,
                            FreightChargeD = soaliGroup.FreightCost,
                            //BalanceD = soaliGroup.Balance,
                            TotalAmountD = i.Amount
                            //}).FirstOrDefault();
                        }).First();
            */


            return new SoaDetailsPVM
            {
                //Id = soaVM.Id, // weird
                Id = id,
                Contents = items
            };
        }

        //public async Task<IList<SoacrVM>> GetContractInvoices3(int? id)
        //{
        //    var contract = from inv in context.Invoices
        //                   join soali in context.SOACRLineItems
        //                              on inv.Id equals soali.InvoiceId
        //                   join soacr in context.SOACRs
        //                              on soali.SOACRId equals soacr.Id into soacrs
        //                   from soacrsGroup in soacrs.DefaultIfEmpty()
        //                   join invli in context.InvoiceLineItems
        //                              on inv.Id equals invli.InvoiceId

        //                   where (id == soacrsGroup.CustomerId)

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

        //public async Task<SoaDetailsPVM> GetCounterReceiptInvoices(int? id)
        //{
        //    var items = await GetContractInvoices3(id);
        //    var cust = context.Customers.Where(s => s.Id == id).FirstOrDefault();

        //    return new SoaDetailsPVM
        //    {
        //        CustomerId = cust == null? null : cust.Id,
        //        CustomerCode = cust == null? String.Empty : cust.Code,
        //        Customer = cust == null ? String.Empty : cust.Name,
        //        Contents = items
        //    };
        //}

        private async Task<List<SoacrInvoiceVM>> GetCustomerInvoiceToAdd(int? customerId)
        {
            var invoiceInSOACR = from sl in context.SOACRLineItems
                                     //where sl.SOACR.CustomerId == customerId
                                 select sl.InvoiceId;
            var invoice = from inv in context.Invoices
                          where inv.SalesOrder.Customer.Id == customerId
                          && !invoiceInSOACR.Contains(inv.Id)
                          && inv.Status.Name == "Invoice Submitted"
                          select new SoacrInvoiceVM
                          {
                              Id = inv.Id,
                              InvoiceDateD = inv.DateModified ?? inv.DateCreated,
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
            var pageList = form.PageSize == -1 ? data : data.Skip(form.Skip).Take(form.PageSize); // added checking for All
            var dataList = pageList.ToList();
            return new ForDataTables<SoacrInvoiceVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList
            };
        }





        public async Task<IList<InvoiceVM>> GetContractInvoicesCustomer(int id, int soaId)
        {
            var soadetails = (await GetSoaDetails(soaId)).Select(s => s.InvoiceId);

            var contract = from inv in context.Invoices
                           join soali in context.SOACRLineItems
                                      on inv.Id equals soali.InvoiceId
                           join soacr in context.SOACRs
                                      on soali.SOACRId equals soacr.Id into soacrs
                           from soacrsGroup in soacrs.DefaultIfEmpty()
                           join invli in context.InvoiceLineItems
                                      on inv.Id equals invli.InvoiceId

                           where soacrsGroup.CustomerId == id && (!soadetails.Any(s => s == inv.Id))
                           && soacrsGroup.SOACRTypeId == 2

                           select new InvoiceVM
                           {
                               Id = inv.Id,
                               Customer = soacrsGroup.Customer.Name,
                               InvoiceDateD = inv.DateModified ?? inv.DateCreated,
                               InvoiceNumber = inv.InvoiceNumber,
                               InvoiceType = inv.InvoiceType.Name,
                               TotalAmountD = invli.Quantity * invli.UnitPrice
                           };
            //return await contract.ToListAsync();



            var contractGroup = from cont in await contract.ToListAsync()
                                orderby cont.Id, cont.Customer
                                group cont by new
                                {
                                    cont.Id,
                                    cont.Customer,
                                    cont.InvoiceDateD,
                                    cont.InvoiceNumber,
                                    cont.InvoiceType
                                }
                                into i
                                select new InvoiceVM
                                {
                                    Id = i.Key.Id,
                                    Customer = i.Key.Customer,
                                    InvoiceDateD = i.Key.InvoiceDateD,
                                    InvoiceNumber = i.Key.InvoiceNumber,
                                    InvoiceType = i.Key.InvoiceType,
                                    TotalAmountD = i.Sum(s => s.TotalAmountD)


                                };

            return contractGroup.ToList();
        }

        public async Task<ForDataTables<InvoiceVM>> GetContractReceiptCustomerDTAsync(DataTablesRequestForm form, int id, int soaId)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetContractInvoicesCustomer(id, soaId)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.InvoiceDate.ToString().Contains(form.SearchValue)
                                    || x.InvoiceNumber.ToString().Contains(form.SearchValue)
                                    || x.InvoiceType.ToLower().Contains(form.SearchValue)
                                    || x.TotalAmount.ToString().Contains(form.SearchValue));
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
                        case "InvoiceDate": data = data.OrderBy(s => s.InvoiceDate); break;
                        case "InvoiceNumber": data = data.OrderBy(s => s.InvoiceNumber); break;
                        case "InvoiceType": data = data.OrderBy(s => s.InvoiceType); break;
                        case "TotalAmount": data = data.OrderBy(s => s.TotalAmount); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "InvoiceDate": data = data.OrderByDescending(s => s.InvoiceDate); break;
                        case "InvoiceNumber": data = data.OrderByDescending(s => s.InvoiceNumber); break;
                        case "InvoiceType": data = data.OrderByDescending(s => s.InvoiceType); break;
                        case "TotalAmount": data = data.OrderByDescending(s => s.TotalAmount); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<InvoiceVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public async Task<IList<InvoiceVM>> GetContractReceiptDetailsAsync(int id)
        {
            var contract = from inv in context.Invoices
                           join soali in context.SOACRLineItems
                                      on inv.Id equals soali.InvoiceId
                           join soacr in context.SOACRs
                                      on soali.SOACRId equals soacr.Id into soacrs
                           from soacrsGroup in soacrs.DefaultIfEmpty()

                           where inv.Id == id
                           select new InvoiceVM
                           {
                               Id = inv.Id,
                               Customer = soacrsGroup.Customer.Name,
                               InvoiceNumber = inv.InvoiceNumber,
                               TotalAmountD = inv.Amount,
                               TBRD = soali.TBRAmount,
                               CreditD = soali.CreditAmount,
                               DebitD = soali.DebitAmount,
                               PaidAmountD = soali.PaidAmount,
                               FreightChargeD = soali.FreightCost,
                               //BalanceD = soali.Balance
                           };
            return await contract.ToListAsync();
        }

        public async Task<ForDataTables<InvoiceVM>> GetContractReceiptDetailsDTAsync(DataTablesRequestForm form, int id)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetContractReceiptDetailsAsync(id)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.InvoiceNumber.ToString().Contains(form.SearchValue)
                                    || x.TotalAmount.ToString().Contains(form.SearchValue)
                                    || x.TBR.ToString().Contains(form.SearchValue)
                                    || x.Credit.ToString().Contains(form.SearchValue)
                                    || x.Debit.ToString().Contains(form.SearchValue)
                                    || x.PaidAmount.ToString().Contains(form.SearchValue)
                                    || x.FreightCharge.ToString().Contains(form.SearchValue)
                                    || x.Balance.ToString().Contains(form.SearchValue));
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
                        case "InvoiceNumber": data = data.OrderBy(s => s.InvoiceNumber); break;
                        case "TotalAmount": data = data.OrderBy(s => s.TotalAmountD); break;
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
                        case "InvoiceNumber": data = data.OrderByDescending(s => s.InvoiceNumber); break;
                        case "TotalAmount": data = data.OrderByDescending(s => s.TotalAmountD); break;
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
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<InvoiceVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public async Task AddInvoicesAsync(int soaId, int[] invoiceIds)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            foreach (var invoiceId in invoiceIds)
            {
                var invoiceAmount = context.InvoiceLineItems.Where(s => s.InvoiceId == invoiceId).Sum(s => s.Quantity * s.UnitPrice);
                var soali = new SOACRLineItem
                {
                    SOACRId = soaId,
                    InvoiceId = invoiceId,
                    InvoiceAmount = invoiceAmount,
                    PaidAmount = 0,
                    TBRAmount = 0,
                    CreditAmount = 0,
                    DebitAmount = 0,
                    FreightCost = 0,
                    FreightWaybill = "0",
                    Balance = 0,
                    IsPaid = false
                };
                await context.SOACRLineItems.AddAsync(soali);
                await context.SaveChangesAsync(currentUserId);
            }
        }

        //public async Task<int> AddCrAsync(SoacrVM soaVM, int[] invoiceIds)
        //{
        //    string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
        //    var dateFrom = soaVM.SOAFromD;
        //    var dateTo = soaVM.SOAToD;

        //    var soa = new SOACR
        //    {
        //        CustomerId = soaVM.CustomerId,
        //        SOACRTypeId = 2,
        //        SOAFrom = null,
        //        SOATo = null,
        //        AmountToPay = soaVM.InvoiceAmountD,
        //        TotalCMAAmount = soaVM.CreditD,
        //        TotalDMAAmount = soaVM.DebitD,
        //        TotalDMRAmount = soaVM.DMRD,
        //        TotalBalance = soaVM.BalanceD,
        //        TotalFreightCost = soaVM.FreightChargeD,
        //        TotalAmount = soaVM.TotalAmountD,
        //        OverPayment = soaVM.OverPaymentD,
        //    };
        //    await context.SOACRs.AddAsync(soa);
        //    await context.SaveChangesAsync(currentUserId);


        //    foreach (var invoiceId in invoiceIds)
        //    {
        //        var invoiceAmount = context.InvoiceLineItems.Where(s => s.InvoiceId == invoiceId).Sum(s => s.Quantity * s.UnitPrice);
        //        var soali = new SOACRLineItem
        //        {
        //            SOACRId = soa.Id,
        //            InvoiceId = invoiceId,
        //            InvoiceAmount = invoiceAmount,
        //            PaidAmount = 0,
        //            TBRAmount = 0,
        //            CreditAmount = 0,
        //            DebitAmount = 0,
        //            FreightCost = 0,
        //            FreightWaybill = "0",
        //            Balance = 0,
        //            IsPaid = false
        //        };
        //        await context.SOACRLineItems.AddAsync(soali);
        //        await context.SaveChangesAsync(currentUserId);
        //    }

        //    return soa.Id;

        //}

        public async Task<int> AddCRAsync(int CustomerId, int[] InvoiceIds)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var soaType = await context.SOACRTypes.Where(s => s.Name == "Counter Receipt").Select(s => s.Id).FirstAsync();
            var invoices = await context.Invoices.Where(s => InvoiceIds.Contains(s.Id)).ToListAsync();

            var soa = new SOACR
            {
                CustomerId = CustomerId,
                SOACRTypeId = soaType,
                AmountToPay = invoices.Sum(s => s.Amount),
                TotalCMAAmount = 0,
                TotalDMAAmount = 0,
                TotalDMRAmount = 0,
                TotalBalance = 0,
                TotalFreightCost = 0,
                TotalAmount = 0,
                OverPayment = 0,
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
                    TBRAmount = 0,
                    CreditAmount = 0,
                    DebitAmount = 0,
                    FreightCost = 0,
                    FreightWaybill = "",
                    Balance = 0,
                    IsPaid = false
                };
                await context.SOACRLineItems.AddAsync(soali);
                await context.SaveChangesAsync(currentUserId);
            }

            return soa.Id;
        }

        public async Task AddCustomerPaymentAsync(PaymentVM paymentVM)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var cp = new CustomerPayment
            {
                CustomerId = paymentVM.CustomerId,
                CRNumber = paymentVM.CRNumber,
                Remarks = paymentVM.Remarks,
                OverPayment = paymentVM.OverPaymentD,
                TKTSolicitation = paymentVM.TKTD,
                Tax = paymentVM.TaxD,
                CashAdvance = paymentVM.CashAdvancedD,
                Discount = paymentVM.DiscountD,
                OffSet = paymentVM.OffsetD,
                Cash = paymentVM.CashAmountD,
                BackLoad = paymentVM.BackLoadD,
                SOACRId = paymentVM.SOANo
            };
            await context.CustomerPayments.AddAsync(cp);
            await context.SaveChangesAsync(currentUserId);

            var sc = new SOACRCheck
            {
                CustomerPaymentId = cp.Id,
                BankName = paymentVM.BankName,
                BankBranch = paymentVM.BankBranch,
                CheckNumber = paymentVM.CheckNo,
                CheckAmount = paymentVM.CheckAmountD,
                CheckDate = paymentVM.CheckDateD
            };

            await context.SOACRChecks.AddAsync(sc);
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task UpdateAsync(SoacrLineItemVM soaVM)
        {
            var soa = context.SOACRLineItems.Where(s => s.Id == soaVM.Id).FirstOrDefault();
            if (soa == null)
            {
                throw new DbUpdateConcurrencyException();
            }

            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            soa.Id = soaVM.Id;
            soa.InvoiceId = soaVM.InvoiceId;
            soa.TBRAmount = soaVM.TBRD;
            soa.DebitAmount = soaVM.DebitD;
            soa.CreditAmount = soaVM.CreditD;
            soa.FreightCost = soaVM.FreightChargeD;

            context.SOACRLineItems.Update(soa);
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task<bool> IsSOAIdExist(int id)
        {
            return await context.SOACRs.AnyAsync(c => c.Id == id);
        }

        public async Task<SoacrPaymentDetails> GetPaymentDetails(SoacrPaymentFilter filter)
        {
            var payment = await context.CustomerPayments
                                       .Where(s => s.CustomerId == filter.CustomerId
                                           && s.SOACRId == filter.SOACRId
                                           && s.InvoiceId == filter.InvoiceId)
                                       .FirstOrDefaultAsync();
            if (payment == null)
            {
                string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
                payment = new CustomerPayment
                {
                    BackLoad = 0,
                    Cash = 0,
                    CashAdvance = 0,
                    CRNumber = "",
                    CustomerId = filter.CustomerId == null ? 0 : filter.CustomerId.Value,
                    Discount = 0,
                    InvoiceId = filter.InvoiceId,
                    OffSet = 0,
                    OverPayment = 0,
                    Remarks = "",
                    SOACRId = filter.SOACRId == null ? 0 : filter.SOACRId.Value,
                    Tax = 0,
                    TKTSolicitation = 0,
                    TotalAmountPaid = 0,
                    TotalInvoiceAmount = 0
                };
                await context.CustomerPayments.AddAsync(payment);
                await context.SaveChangesAsync(currentUserId);
            }
            return new SoacrPaymentDetails
            {
                BackLoad = payment.BackLoad,
                CashAdvance = payment.CashAdvance,
                CashAmount = payment.Cash,
                CustomerReceiptNumber = payment.CRNumber,
                Discount = payment.Discount,
                Id = payment.Id,
                Offset = payment.OffSet,
                OverPayment = payment.OverPayment,
                Remarks = payment.Remarks,
                Tax = payment.Tax,
                TKTSolicitation = payment.TKTSolicitation
            };
        }

        public async Task<bool> UpdatePaymentDetails(SoacrPaymentDetails payment)
        {
            var customerPayment = await context.CustomerPayments
                                               .Where(s => s.Id == payment.Id)
                                               .FirstOrDefaultAsync();
            if (customerPayment == null)
            {
                return false;
            }

            customerPayment.BackLoad = payment.BackLoad;
            customerPayment.CashAdvance = payment.CashAdvance;
            customerPayment.Cash = payment.CashAmount;
            customerPayment.CRNumber = payment.CustomerReceiptNumber;
            customerPayment.Discount = payment.Discount;
            customerPayment.OffSet = payment.Offset;
            customerPayment.OverPayment = payment.OverPayment;
            customerPayment.Remarks = payment.Remarks + "";
            customerPayment.Tax = payment.Tax;
            customerPayment.TKTSolicitation = payment.TKTSolicitation;
            customerPayment.TotalAmountPaid = payment.BackLoad + payment.CashAdvance + payment.CashAmount - payment.Discount - payment.Offset + payment.OverPayment - payment.Tax - payment.TKTSolicitation;


            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

            var soacrLineItem = await context.SOACRLineItems
                                             .Where(s => s.SOACRId == customerPayment.SOACRId
                                                    && s.InvoiceId == customerPayment.InvoiceId)
                                             .FirstOrDefaultAsync();
            if (soacrLineItem != null)
            {
                customerPayment.TotalInvoiceAmount = soacrLineItem.InvoiceAmount;
                soacrLineItem.PaidAmount = customerPayment.TotalAmountPaid;
                soacrLineItem.IsPaid = soacrLineItem.PaidAmount > soacrLineItem.InvoiceAmount;
                context.SOACRLineItems.Update(soacrLineItem);
            }
            context.CustomerPayments.Update(customerPayment);
            await context.SaveChangesAsync(currentUserId);

            return true;
        }

        private async Task<IList<SoacrPaymentCheck>> GetPaymentChecks(int customerPaymentId)
        {
            var soacrChecks = from check in context.SOACRChecks
                              where check.CustomerPaymentId == customerPaymentId
                              select new SoacrPaymentCheck
                              {
                                  BankBranch = check.BankBranch,
                                  BankName = check.BankName,
                                  CheckAmountD = check.CheckAmount,
                                  CheckDateD = check.CheckDate,
                                  CheckNumber = check.CheckNumber,
                                  Id = check.Id
                              };
            return await soacrChecks.ToListAsync();
        }

        public async Task<ForDataTables<SoacrPaymentCheck>> GetPaymentChecksDTAsync(DataTablesRequestForm form, int customerPaymentId)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetPaymentChecks(customerPaymentId)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.BankBranch.ToLower().Contains(form.SearchValue)
                                    || x.BankName.ToLower().Contains(form.SearchValue)
                                    || x.CheckAmountD.ToString().Contains(form.SearchValue)
                                    || x.CheckDate.ToLower().Contains(form.SearchValue)
                                    || x.CheckNumber.ToLower().Contains(form.SearchValue));
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
                        case "BankBranch": data = data.OrderBy(s => s.BankBranch); break;
                        case "BankName": data = data.OrderBy(s => s.BankName); break;
                        case "CheckAmount": data = data.OrderBy(s => s.CheckAmountD); break;
                        case "CheckDate": data = data.OrderBy(s => s.CheckDateD); break;
                        case "CheckNumber": data = data.OrderBy(s => s.CheckNumber); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "BankBranch": data = data.OrderByDescending(s => s.BankBranch); break;
                        case "BankName": data = data.OrderByDescending(s => s.BankName); break;
                        case "CheckAmount": data = data.OrderByDescending(s => s.CheckAmountD); break;
                        case "CheckDate": data = data.OrderByDescending(s => s.CheckDateD); break;
                        case "CheckNumber": data = data.OrderByDescending(s => s.CheckNumber); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<SoacrPaymentCheck>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public async Task<bool> AddPaymentCheck(SoacrPaymentCheck check)
        {
            var soacrCheck = new SOACRCheck()
            {
                BankBranch = check.BankBranch,
                BankName = check.BankName,
                CheckAmount = check.CheckAmountD,
                CheckDate = check.CheckDateD,
                CheckNumber = check.CheckNumber,
                CustomerPaymentId = check.CustomerPaymentId == null ? 0 : check.CustomerPaymentId.Value
            };
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            await context.SOACRChecks.AddAsync(soacrCheck);
            await context.SaveChangesAsync(currentUserId);

            return true;
        }

        public async Task<bool> RemovePaymentCheck(int soaCheckId)
        {
            var soacrCheck = await context.SOACRChecks.Where(s => s.Id == soaCheckId).FirstOrDefaultAsync();
            if (soacrCheck == null)
            {
                return false;
            }
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            context.SOACRChecks.Remove(soacrCheck);
            await context.SaveChangesAsync(currentUserId);

            return true;
        }
        public async Task<bool> IsCheckNumberInUse(string checkNumber)
        {
            return await context.SOACRChecks.AnyAsync(s => s.CheckNumber == checkNumber);
        }

        public async Task<bool> IsSOALineItemIdExist(int id)
        {
            return await context.SOACRLineItems.AnyAsync(c => c.Id == id);
        }

        public async Task<bool> IsSoacrTypeCR(int id)
        {
            return await context.SOACRs.AnyAsync(c => c.Id == id && c.SOACRTypeId == 2);
        }



    }
}