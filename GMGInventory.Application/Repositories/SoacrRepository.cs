using GMGInventory.Application.Contracts;
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

        public SoacrVM? GetSoacrVM(int? id)
        {
            var soaVM = (from soa in context.SOACRs
                                            .Include(s => s.Customer)
                         join st in context.SOACRTypes
                                          on soa.SOACRTypeId equals st.Id

                         let soalit = context.SOACRLineItems.Where(s => s.SOACRId == soa.Id).Sum(s => s.TBRAmount)
                         let soalic = context.SOACRLineItems.Where(s => s.SOACRId == soa.Id).Sum(s => s.CreditAmount)
                         let soalid = context.SOACRLineItems.Where(s => s.SOACRId == soa.Id).Sum(s => s.DebitAmount)
                         let soalip = context.SOACRLineItems.Where(s => s.SOACRId == soa.Id).Sum(s => s.PaidAmount)
                         let soalif = context.SOACRLineItems.Where(s => s.SOACRId == soa.Id).Sum(s => s.FreightCost)
                         let soalib = context.SOACRLineItems.Where(s => s.SOACRId == soa.Id).Sum(s => s.Balance)

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
                             TBRD = soalit,
                             CreditD = soalic,
                             DebitD = soalid,
                             PaidAmountD = soalip,
                             FreightChargeD = soalif,
                             BalanceD = soalib,
                             OverPaymentD = soa.OverPayment
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
                       && filter.DateFrom <= (inv.DateModified ?? inv.DateCreated)
                       && (inv.DateModified ?? inv.DateCreated) <= filter.DateTo

                       let soalit = context.SOACRLineItems.Where(s => s.SOACRId == soa.Id).ToList()

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
                           BalanceD = soalit.Sum(s => s.Balance),
                           OverPaymentD = soa.OverPayment
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
                              BalanceD = i.Sum(s => s.BalanceD),
                              OverPaymentD = i.Key.OverPaymentD,
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
                                    || x.CRNumber.ToLower().Contains(form.SearchValue)
                                    || x.Type.ToLower().Contains(form.SearchValue)
                                    || x.SOAFrom.ToLower().Contains(form.SearchValue)
                                    || x.SOATo.ToLower().Contains(form.SearchValue)
                                    || x.TotalAmount.ToLower().Contains(form.SearchValue)
                                    || x.TBR.ToLower().Contains(form.SearchValue)
                                    || x.Credit.ToLower().Contains(form.SearchValue)
                                    || x.Debit.ToLower().Contains(form.SearchValue)
                                    || x.PaidAmount.ToLower().Contains(form.SearchValue)
                                    || x.FreightCharge.ToLower().Contains(form.SearchValue)
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
                        case "SOAFrom": data = data.OrderBy(s => s.SOAFrom); break;
                        case "SOATo": data = data.OrderBy(s => s.SOATo); break;
                        case "TotalAmount": data = data.OrderBy(s => s.TotalAmount); break;
                        case "TBR": data = data.OrderBy(s => s.TBR); break;
                        case "Credit": data = data.OrderBy(s => s.Credit); break;
                        case "Debit": data = data.OrderBy(s => s.Debit); break;
                        case "PaidAmount": data = data.OrderBy(s => s.PaidAmount); break;
                        case "FreightCharge": data = data.OrderBy(s => s.FreightCharge); break;
                        case "Balance": data = data.OrderBy(s => s.Balance); break;
                        case "OverPayment": data = data.OrderBy(s => s.OverPayment); break;
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
                        case "SOAFrom": data = data.OrderByDescending(s => s.SOAFrom); break;
                        case "SOATo": data = data.OrderByDescending(s => s.SOATo); break;
                        case "TotalAmount": data = data.OrderByDescending(s => s.TotalAmount); break;
                        case "TBR": data = data.OrderByDescending(s => s.TBR); break;
                        case "Credit": data = data.OrderByDescending(s => s.Credit); break;
                        case "Debit": data = data.OrderByDescending(s => s.Debit); break;
                        case "PaidAmount": data = data.OrderByDescending(s => s.PaidAmount); break;
                        case "FreightCharge": data = data.OrderByDescending(s => s.FreightCharge); break;
                        case "Balance": data = data.OrderByDescending(s => s.Balance); break;
                        case "OverPayment": data = data.OrderByDescending(s => s.OverPayment); break;
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

        public async Task<IList<SoacrVM>> GetAllAsync(SoaFilterVM filter)
        {
            var agents = from soa in context.SOACRs
                                            .Include(s => s.Customer)
                         join cust in context.Customers
                                          on soa.CustomerId equals cust.Id
                         join agent in context.Agents
                                          on cust.AgentId equals agent.Id

                         join st in context.SOACRTypes
                                          on soa.SOACRTypeId equals st.Id

                         where (filter.CustomerId == null || filter.CustomerId == soa.CustomerId)
                             && (filter.AgentId == null || filter.AgentId == agent.Id)
                         //&& filter.DateFrom <= soa.SOAFrom
                         //&& soa.SOAFrom <= filter.DateTo

                         let soalit = context.SOACRLineItems.Where(s => s.SOACRId == soa.Id).ToList()

                         //let soalit = context.SOACRLineItems.Where(s => s.SOACRId == soa.Id).Sum(s => s.TBRAmount)
                         //let soalic = context.SOACRLineItems.Where(s => s.SOACRId == soa.Id).Sum(s => s.CreditAmount)
                         //let soalid = context.SOACRLineItems.Where(s => s.SOACRId == soa.Id).Sum(s => s.DebitAmount)
                         //let soalip = context.SOACRLineItems.Where(s => s.SOACRId == soa.Id).Sum(s => s.PaidAmount)
                         //let soalif = context.SOACRLineItems.Where(s => s.SOACRId == soa.Id).Sum(s => s.FreightCost)
                         //let soalib = context.SOACRLineItems.Where(s => s.SOACRId == soa.Id).Sum(s => s.Balance)

                         select new SoacrVM
                         {
                             Id = soa.Id,
                             CustomerId = soa.CustomerId,
                             CustomerName = soa.Customer.Name,
                             Type = st.Code,
                             SOAFromD = soa.SOAFrom,
                             SOAToD = soa.SOATo,
                             TotalAmountD = soa.TotalAmount,
                             TBRD = soalit.Sum(s => s.TBRAmount),
                             CreditD = soalit.Sum(s => s.CreditAmount),
                             DebitD = soalit.Sum(s => s.CreditAmount),
                             PaidAmountD = soalit.Sum(s => s.PaidAmount),
                             FreightChargeD = soalit.Sum(s => s.FreightCost),
                             BalanceD = soalit.Sum(s => s.Balance),
                             OverPaymentD = soa.OverPayment
                         };
            return await agents.ToListAsync();
        }

        public async Task<ForDataTables<SoacrVM>> GetAllForDataTablesAsync(DataTablesRequestForm form, SoaFilterVM filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetAllAsync(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.CustomerName.ToLower().Contains(form.SearchValue)
                                    || x.CRNumber.ToLower().Contains(form.SearchValue)
                                    || x.Type.ToLower().Contains(form.SearchValue)
                                    || x.SOAFrom.ToLower().Contains(form.SearchValue)
                                    || x.SOATo.ToLower().Contains(form.SearchValue)
                                    || x.TotalAmount.ToLower().Contains(form.SearchValue)
                                    || x.TBR.ToLower().Contains(form.SearchValue)
                                    || x.Credit.ToLower().Contains(form.SearchValue)
                                    || x.Debit.ToLower().Contains(form.SearchValue)
                                    || x.PaidAmount.ToLower().Contains(form.SearchValue)
                                    || x.FreightCharge.ToLower().Contains(form.SearchValue)
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
                        case "SOAFrom": data = data.OrderBy(s => s.SOAFrom); break;
                        case "SOATo": data = data.OrderBy(s => s.SOATo); break;
                        case "TotalAmount": data = data.OrderBy(s => s.TotalAmount); break;
                        case "TBR": data = data.OrderBy(s => s.TBR); break;
                        case "Credit": data = data.OrderBy(s => s.Credit); break;
                        case "Debit": data = data.OrderBy(s => s.Debit); break;
                        case "PaidAmount": data = data.OrderBy(s => s.PaidAmount); break;
                        case "FreightCharge": data = data.OrderBy(s => s.FreightCharge); break;
                        case "Balance": data = data.OrderBy(s => s.Balance); break;
                        case "OverPayment": data = data.OrderBy(s => s.OverPayment); break;
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
                        case "SOAFrom": data = data.OrderByDescending(s => s.SOAFrom); break;
                        case "SOATo": data = data.OrderByDescending(s => s.SOATo); break;
                        case "TotalAmount": data = data.OrderByDescending(s => s.TotalAmount); break;
                        case "TBR": data = data.OrderByDescending(s => s.TBR); break;
                        case "Credit": data = data.OrderByDescending(s => s.Credit); break;
                        case "Debit": data = data.OrderByDescending(s => s.Debit); break;
                        case "PaidAmount": data = data.OrderByDescending(s => s.PaidAmount); break;
                        case "FreightCharge": data = data.OrderByDescending(s => s.FreightCharge); break;
                        case "Balance": data = data.OrderByDescending(s => s.Balance); break;
                        case "OverPayment": data = data.OrderByDescending(s => s.OverPayment); break;
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
            var soaVM = from soa in context.SOACRs
                                            .Include(s => s.Customer)
                        join sl in context.SOACRLineItems
                                    on soa.Id equals sl.SOACRId into soali
                        from soaliGroup in soali.DefaultIfEmpty()
                        join i in context.Invoices
                                         on soaliGroup.InvoiceId equals i.Id
                        where soa.Id == id
                        select new SoacrVM
                        {
                            Id = soaliGroup.Id,
                            DateFromD = soa.SOAFrom,
                            DateToD = soa.SOATo,
                            CreditTerm = soa.Customer.CreditTerm.Name,
                            SoaNo = soa.Id,
                            Agent = soa.Customer.Agent.FirstName,
                            CustomerName = soa.Customer.Name,
                            InvoiceType = i.InvoiceType.Name,
                            CustomerId = soa.CustomerId,
                            InvoiceId = soaliGroup.InvoiceId,
                            InvoiceNumber = i.InvoiceNumber,
                            InvoiceAmountD = i.Amount,
                            TBRD = soaliGroup.TBRAmount,
                            CreditD = soaliGroup.CreditAmount,
                            DebitD = soaliGroup.DebitAmount,
                            PaidAmountD = soaliGroup.PaidAmount,
                            FreightChargeD = soaliGroup.FreightCost,
                            BalanceD = soaliGroup.Balance
                        };
            return await soaVM.ToListAsync();
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
                data = data.Where(x => x.InvoiceNumber.ToString().Contains(form.SearchValue)
                                    || x.InvoiceAmount.ToString().Contains(form.SearchValue)
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
                        case "InvoiceAmount": data = data.OrderBy(s => s.InvoiceAmount); break;
                        case "TBR": data = data.OrderBy(s => s.TBR); break;
                        case "Credit": data = data.OrderBy(s => s.Credit); break;
                        case "Debit": data = data.OrderBy(s => s.Debit); break;
                        case "PaidAmount": data = data.OrderBy(s => s.PaidAmount); break;
                        case "FreightCharge": data = data.OrderBy(s => s.FreightCharge); break;
                        case "Balance": data = data.OrderBy(s => s.Balance); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "InvoiceNumber": data = data.OrderByDescending(s => s.InvoiceNumber); break;
                        case "InvoiceAmount": data = data.OrderByDescending(s => s.InvoiceAmount); break;
                        case "TBR": data = data.OrderByDescending(s => s.TBR); break;
                        case "Credit": data = data.OrderByDescending(s => s.Credit); break;
                        case "Debit": data = data.OrderByDescending(s => s.Debit); break;
                        case "PaidAmount": data = data.OrderByDescending(s => s.PaidAmount); break;
                        case "FreightCharge": data = data.OrderByDescending(s => s.FreightCharge); break;
                        case "Balance": data = data.OrderByDescending(s => s.Balance); break;
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
                        where soa.Id == id
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
                            InvoiceAmountD = i.Amount,
                            InvoiceDateD = i.DateModified ?? i.DateCreated,
                            TBRD = soaliGroup.TBRAmount,
                            CreditD = soaliGroup.CreditAmount,
                            DebitD = soaliGroup.DebitAmount,
                            PaidAmountD = soaliGroup.PaidAmount,
                            FreightChargeD = soaliGroup.FreightCost,
                            BalanceD = soaliGroup.Balance,
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
                         }).FirstOrDefault();

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
                TotalAmountD = items.Sum(s => s.InvoiceAmountD)
            };
        }

        public async Task<SoaDetailsPVM> PrintCRDetails(int id)
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
                             CreditTerm = soa.Customer.CreditTerm.Name,
                             CRDateD = soa.DateModified ?? soa.DateCreated,
                             CustomerCode = soa.Customer.Code
                         }).FirstOrDefault();

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
                TotalAmountD = items.Sum(s => s.InvoiceAmountD),
                CRDateD = soaVM.CRDateD,
                CustomerCode = soaVM.CustomerCode
            };
        }

        public async Task<SoaDetailsPVM> PrintInvoice(SoaFilterVM filter)
        {
            var items = await GetContractInvoices(filter);
            var agent = await context.Customers.Where(s => s.AgentId == filter.AgentId).FirstOrDefaultAsync();
            var customer = await context.Customers.Where(s => s.Id == filter.CustomerId).FirstOrDefaultAsync();

            return new SoaDetailsPVM
            {
                DateFromD = filter.DateFrom.GetValueOrDefault(),
                DateToD = filter.DateTo.GetValueOrDefault(),
                Customer = filter.CustomerId == null || customer == null ? "All" : customer.Name,
                Agent = filter.AgentId == null || agent == null ? "All" : agent.Agent.FirstName,
                InvoiceContent = items,
                TotalAmountD = items.Sum(s => s.TotalAmountD)
            };
        }

        public async Task<IList<InvoiceVM>> GetContractInvoices(SoaFilterVM filter)
        {
            var contract = from inv in context.Invoices
                           join soali in context.SOACRLineItems
                                      on inv.Id equals soali.InvoiceId
                           join soacr in context.SOACRs
                                      on soali.SOACRId equals soacr.Id into soacrs
                           from soacrsGroup in soacrs.DefaultIfEmpty()

                           where (filter.CustomerId == null || filter.CustomerId == soacrsGroup.CustomerId)
                           && (filter.AgentId == null || filter.AgentId == soacrsGroup.Customer.AgentId)
                           && filter.DateFrom <= (inv.DateModified ?? inv.DateCreated)
                           && (inv.DateModified ?? inv.DateCreated) <= filter.DateTo
                           //&& soacrsGroup.SOACRTypeId == 2

                           select new InvoiceVM
                           {
                               Id = inv.Id,
                               Customer = soacrsGroup.Customer.Name,
                               InvoiceDateD = inv.DateModified ?? inv.DateCreated,
                               InvoiceNumber = inv.InvoiceNumber,
                               InvoiceType = inv.InvoiceType.Name,
                               TotalAmountD = inv.Amount
                           };
            return await contract.ToListAsync();
        }

        public async Task<ForDataTables<InvoiceVM>> GetContractReceiptDTAsync(DataTablesRequestForm form, SoaFilterVM filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetContractInvoices(filter)).AsQueryable();
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
                               BalanceD = soali.Balance
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
                        case "TotalAmount": data = data.OrderBy(s => s.TotalAmount); break;
                        case "TBR": data = data.OrderBy(s => s.TBR); break;
                        case "Credit": data = data.OrderBy(s => s.Credit); break;
                        case "Debit": data = data.OrderBy(s => s.Debit); break;
                        case "PaidAmount": data = data.OrderBy(s => s.PaidAmount); break;
                        case "FreightCharge": data = data.OrderBy(s => s.FreightCharge); break;
                        case "Balance": data = data.OrderBy(s => s.Balance); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "InvoiceNumber": data = data.OrderByDescending(s => s.InvoiceNumber); break;
                        case "TotalAmount": data = data.OrderByDescending(s => s.TotalAmount); break;
                        case "TBR": data = data.OrderByDescending(s => s.TBR); break;
                        case "Credit": data = data.OrderByDescending(s => s.Credit); break;
                        case "Debit": data = data.OrderByDescending(s => s.Debit); break;
                        case "PaidAmount": data = data.OrderByDescending(s => s.PaidAmount); break;
                        case "FreightCharge": data = data.OrderByDescending(s => s.FreightCharge); break;
                        case "Balance": data = data.OrderByDescending(s => s.Balance); break;
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

        public async Task AddAsync(SoacrVM soaVM)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

            var soa = new SOACR
            {
                Id = soaVM.Id,
                CustomerId = soaVM.CustomerId,
                SOACRTypeId = 1,
                SOAFrom = soaVM.SOAFromD,
                SOATo = soaVM.SOAToD,
                AmountToPay = soaVM.InvoiceAmountD,
                TotalCMAAmount = soaVM.CreditD,
                TotalDMAAmount = soaVM.DebitD,
                TotalDMRAmount = soaVM.DMRD,
                TotalBalance = soaVM.BalanceD,
                TotalFreightCost = soaVM.FreightChargeD,
                TotalAmount = soaVM.TotalAmountD,
                OverPayment = soaVM.OverPaymentD,
            };
            await context.SOACRs.AddAsync(soa);
            await context.SaveChangesAsync(currentUserId);

            var invoices = from invoice in context.InvoiceLineItems
                           where invoice.Invoice.SalesOrder.Customer.Id == soa.CustomerId
                           && soaVM.SOAFromD <= (invoice.Invoice.DateModified ?? invoice.Invoice.DateCreated)
                           && (invoice.Invoice.DateModified ?? invoice.Invoice.DateCreated) <= soaVM.DateToD

                           select new SOACRLineItem
                           {
                               InvoiceId = invoice.Invoice.Id,
                               InvoiceAmount = invoice.Quantity * invoice.UnitPrice
                           };

            var invoicesGrp = from invoice in await invoices.ToListAsync()
                              group invoice by new
                              {
                                  invoice.InvoiceId
                              }
                              into i
                              select new SOACRLineItem
                              {
                                  SOACRId = soa.Id,
                                  InvoiceId = i.Key.InvoiceId,
                                  InvoiceAmount = i.Sum(s => s.InvoiceAmount),
                                  PaidAmount = 0,
                                  TBRAmount = 0,
                                  CreditAmount = 0,
                                  DebitAmount = 0,
                                  FreightCost = 0,
                                  FreightWaybill = "0",
                                  Balance = 0,
                                  IsPaid = false
                              };



            await context.SOACRLineItems.AddRangeAsync(invoicesGrp);

            await context.SaveChangesAsync(currentUserId);
        }

        public async Task AddCustomerPaymentAsync(PaymentVM paymentVM)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var cp = new CustomerPayment
            {
                CustomerId = paymentVM.CustomerId,
                CRNumber = paymentVM.CRNumber,
                Remarks = paymentVM.Remarks,
                OverPay = paymentVM.OverPaymentD,
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

        public async Task<bool> IsCustomerPaymentIdExist(int id)
        {
            return await context.CustomerPayments.AnyAsync(c => c.Id == id);
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