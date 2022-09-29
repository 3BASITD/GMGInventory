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
            var soaVM = (from c in context.Customers
                         join soa in context.SOACRs
                                          on c.Id equals soa.CustomerId into custSoa
                         from custSoaGrouping in custSoa.DefaultIfEmpty()
                         join st in context.SOACRTypes
                                          on custSoaGrouping.SOACRTypeId equals st.Id
                         join sl in context.SOACRLineItems
                                     on custSoaGrouping.Id equals sl.SOACRId into custSoaSL
                         from custSoaGroupingSL in custSoaSL.DefaultIfEmpty()
                         join i in context.Invoices
                                          on custSoaGroupingSL.InvoiceId equals i.Id

                         where custSoaGrouping.Id == id
                         select new SoacrVM
                         {

                             Id = custSoaGrouping.Id,
                             CRNumber = custSoaGrouping.Id.ToString(),
                             CustomerName = custSoaGrouping.Customer.Name,
                             Type = st.Code,
                             SOAFrom = custSoaGrouping.SOAFrom.ToString(),
                             SOATo = custSoaGrouping.SOATo.ToString(),
                             TotalAmount = custSoaGrouping.TotalAmount.ToString(),
                             TBR = custSoaGroupingSL.TBRAmount.ToString(),
                             Credit = custSoaGroupingSL.CreditAmount.ToString(),
                             Debit = custSoaGroupingSL.DebitAmount.ToString(),
                             PaidAmount = custSoaGroupingSL.PaidAmount.ToString(),
                             FreightCharge = custSoaGroupingSL.FreightCost.ToString(),
                             Balance = custSoaGroupingSL.Balance,
                             OverPayment = custSoaGrouping.OverPayment.ToString(),
                             InvoiceNumber = i.InvoiceNumber.ToString(),
                             InvoiceAmount = i.Amount.ToString()

                         }).FirstOrDefault();
            return soaVM;
        }



        private IQueryable<SoacrVM> GetList()
        {

            return from c in context.Customers
                   join soa in context.SOACRs                
                                    on c.Id equals soa.CustomerId into custSoa
                   from custSoaGrouping in custSoa.DefaultIfEmpty()
                   join st in context.SOACRTypes
                                    on custSoaGrouping.SOACRTypeId equals st.Id
                   join sl in context.SOACRLineItems
                               on custSoaGrouping.Id equals sl.SOACRId into custSoaSL
                   from custSoaGroupingSL in custSoaSL.DefaultIfEmpty()
                   join i in context.Invoices
                                    on custSoaGroupingSL.InvoiceId equals i.Id
                   
                   select new SoacrVM
                   {

                       Id = custSoaGrouping.Id,
                       CRNumber = custSoaGrouping.Id.ToString(),
                       CustomerName = custSoaGrouping.Customer.Name,
                       Type = st.Code,
                       SOAFrom = custSoaGrouping.SOAFrom.ToString(),
                       SOATo = custSoaGrouping.SOATo.ToString(),
                       TotalAmount = custSoaGrouping.TotalAmount.ToString(),
                       TBR = custSoaGroupingSL.TBRAmount.ToString(),
                       Credit = custSoaGroupingSL.CreditAmount.ToString(),
                       Debit = custSoaGroupingSL.DebitAmount.ToString(),
                       PaidAmount = custSoaGroupingSL.PaidAmount.ToString(),
                       FreightCharge = custSoaGroupingSL.FreightCost.ToString(),
                       Balance = custSoaGroupingSL.Balance,
                       OverPayment = custSoaGrouping.OverPayment.ToString(),
                       InvoiceNumber = i.InvoiceNumber.ToString(),
                       InvoiceAmount = i.Amount.ToString()

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
    }
}