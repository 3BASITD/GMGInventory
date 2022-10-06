using AutoMapper;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

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
                         join cp in context.CustomerPayments
                                          on soa.Id equals cp.SOACRId into soacust
                         from soacustGroup in soacust.DefaultIfEmpty()
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
                             CRNumber = soacustGroup.CRNumber,
                             CustomerName = soa.Customer.Name,
                             Type = st.Code,
                             SOAFrom = soa.SOAFrom.ToString(),
                             SOATo = soa.SOATo.ToString(),
                             TotalAmount = soa.TotalAmount.ToString(),
                             TBR = soalit.ToString(),
                             Credit = soalic.ToString(),
                             Debit = soalid.ToString(),
                             PaidAmount = soalip.ToString(),
                             FreightCharge = soalif.ToString(),
                             Balance = soalib,
                             OverPayment = soa.OverPayment.ToString()

                         }).FirstOrDefault();
            return soaVM;
        }



        private IQueryable<SoacrVM> GetList()
        {

            return from soa in context.SOACRs
                                            .Include(s => s.Customer)
                   join cp in context.CustomerPayments
                                    on soa.Id equals cp.SOACRId into soacust
                   from soacustGroup in soacust.DefaultIfEmpty()
                   join st in context.SOACRTypes
                                    on soa.SOACRTypeId equals st.Id


                   let soalit = context.SOACRLineItems.Where(s => s.SOACRId == soa.Id).Sum(s => s.TBRAmount)
                   let soalic = context.SOACRLineItems.Where(s => s.SOACRId == soa.Id).Sum(s => s.CreditAmount)
                   let soalid = context.SOACRLineItems.Where(s => s.SOACRId == soa.Id).Sum(s => s.DebitAmount)
                   let soalip = context.SOACRLineItems.Where(s => s.SOACRId == soa.Id).Sum(s => s.PaidAmount)
                   let soalif = context.SOACRLineItems.Where(s => s.SOACRId == soa.Id).Sum(s => s.FreightCost)
                   let soalib = context.SOACRLineItems.Where(s => s.SOACRId == soa.Id).Sum(s => s.Balance)


                   select new SoacrVM
                   {

                       Id = soa.Id,
                       CRNumber = soacustGroup.CRNumber,
                       CustomerName = soa.Customer.Name,
                       Type = st.Code,
                       SOAFrom = soa.SOAFrom.ToString(),
                       SOATo = soa.SOATo.ToString(),
                       TotalAmount = soa.TotalAmount.ToString(),
                       TBR = soalit.ToString(),
                       Credit = soalic.ToString(),
                       Debit = soalid.ToString(),
                       PaidAmount = soalip.ToString(),
                       FreightCharge = soalif.ToString(),
                       Balance = soalib,
                       OverPayment = soa.OverPayment.ToString()

                   };
        }


        public ForDataTables<SoacrVM> GetListDataTables(DataTablesRequestForm form)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            //var data = context.AuditLogs.Include(s=>s.User).AsQueryable();
            var data = GetList();
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
                        case "CRNumber": data = data.OrderBy(s => s.CRNumber); break;
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
                        case "CRNumber": data = data.OrderByDescending(s => s.CRNumber); break;
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
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<SoacrVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList
            };
        }



        private async Task<IList<SoacrVM>> GetSoaDetailsAsync(int id)
        {
            var soaVM = from soa in context.SOACRs
                                            .Include(s => s.Customer)
                        join cp in context.CustomerPayments
                                         on soa.Id equals cp.SOACRId into soacust
                        from soacustGroup in soacust.DefaultIfEmpty()

                        join sl in context.SOACRLineItems
                                    on soa.Id equals sl.SOACRId
                        join i in context.Invoices
                                         on sl.InvoiceId equals i.Id
                        where soa.Id == id
                        select new SoacrVM
                        {
                            InvoiceNumber = i.InvoiceNumber,
                            InvoiceAmount = i.Amount.ToString(),
                            TBR = sl.TBRAmount.ToString(),
                            Credit = sl.CreditAmount.ToString(),
                            Debit = sl.DebitAmount.ToString(),
                            PaidAmount = sl.PaidAmount.ToString(),
                            FreightCharge = sl.FreightCost.ToString(),
                            Balance = sl.Balance
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



    }
}