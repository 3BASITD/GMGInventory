using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using GMGInventory.Data.Migrations;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using static Humanizer.In;
using static Humanizer.On;

namespace GMGInventory.Application.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;

        public CustomerRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
            , UserManager<User> userManager) : base(context, httpContextAccessor, userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        public async Task<Select2Model<JsonCustomerOptionsModel>> GetOptions(string search, int page)
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
                                       //InvoiceAmount = g.Sum(s => (decimal?)(s.Quantity * s.UnitPrice * s.discount1 * s.discount2 / 100)) ?? 0
                                       InvoiceAmount = g.Sum(s => (decimal?)(s.Quantity * s.UnitPrice * s.discount1 * s.discount2)) ?? 0
                                   };
            var customerPayments = from pay in context.CustomerPaymentDetails
                                   where pay.Invoice.Status.Code.ToLower() == "invoiced"
                                   group pay by pay.Invoice.SalesOrder.Customer.Id into g
                                   select new
                                   {
                                       CustomerId = g.Key,
                                       PaymentAmount = g.Sum(s => (decimal?)(s.PaidAmount) ?? 0)
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
                                where s.Invoice.Status.Code.ToLower() == "invoiced"
                                group s by s.Invoice.SalesOrder.Customer.Id into g
                                select new
                                {
                                    CustomerId = g.Key,
                                    CreditMemoAmount = g.Sum(s => (decimal?)s.CreditAmount) ?? 0,
                                    DebitMemoAmount = g.Sum(s => (decimal?)s.DebitAmount) ?? 0,
                                    FreightAmount = g.Sum(s => (decimal?)s.FreightCost) ?? 0
                                };

            var customers = from customer in context.Customers
                            let invoiceAmount = customerInvoices.Where(s => s.CustomerId == customer.Id)
                                                                .Sum(s => (decimal?)s.InvoiceAmount) ?? 0
                            let paymentAmount = customerPayments.Where(s => s.CustomerId == customer.Id)
                                                                .Sum(s => (decimal?)s.PaymentAmount) ?? 0
                            let salesReturnAmount = customerSalesReturns.Where(s => s.CustomerId == customer.Id)
                                                                        .Sum(s => (decimal?)s.SalesReturnAmount) ?? 0
                            let creditAmount = customerSOACR.Where(s => s.CustomerId == customer.Id)
                                                            .Sum(s => (decimal?)s.CreditMemoAmount) ?? 0
                            let debitAmount = customerSOACR.Where(s => s.CustomerId == customer.Id)
                                                           .Sum(s => (decimal?)s.DebitMemoAmount) ?? 0
                            let freightAmount = customerSOACR.Where(s => s.CustomerId == customer.Id)
                                                             .Sum(s => (decimal?)s.FreightAmount) ?? 0
                            where string.IsNullOrWhiteSpace(search)
                               || customer.Code.ToLower().Contains(search.ToLower())
                               || customer.Name.ToLower().Contains(search.ToLower())
                            orderby customer.Code ascending
                            select new JsonCustomerOptionsModel
                            {
                                Id = customer.Id.ToString(),
                                Text = customer.Code,
                                Name = customer.Name,
                                Address = customer.ShippingAddresses
                                                  .Where(s => s.IsDefault == true)
                                                  .Select(s => s.Address)
                                                  .FirstOrDefault() ?? "N/A",
                                CreditTerm = customer.CreditTerm.Name,
                                Agent = customer.Agent.FirstName + " " + customer.Agent.LastName,
                                OutstandingBalanceD = invoiceAmount
                                                    - paymentAmount
                                                    - salesReturnAmount
                                                    - creditAmount
                                                    + debitAmount
                                                    + freightAmount
                            };

            var customersList = await customers.ToListAsync();
            var customersFiltered = customersList.Skip((page - 1) * 50).Take(50).ToList();
            return new Select2Model<JsonCustomerOptionsModel>
            {
                //incomplete_results = false,
                items = customersFiltered,
                total_count = customersList.Count
            };
        }

        public async Task<Select2Model<JsonCustomerOptionsModel>> GetByAgentOptions(string search, int page, int? agent)
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

            var customerPayments = from pay in context.CustomerPaymentDetails
                                   where pay.Invoice.Status.Code.ToLower() == "invoiced"
                                   group pay by pay.Invoice.SalesOrder.Customer.Id into g
                                   select new
                                   {
                                       CustomerId = g.Key,
                                       PaymentAmount = g.Sum(s => (decimal?)(s.PaidAmount) ?? 0)
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
                                where s.Invoice.Status.Code.ToLower() == "invoiced"
                                group s by s.Invoice.SalesOrder.Customer.Id into g
                                select new
                                {
                                    CustomerId = g.Key,
                                    CreditMemoAmount = g.Sum(s => (decimal?)s.CreditAmount) ?? 0,
                                    DebitMemoAmount = g.Sum(s => (decimal?)s.DebitAmount) ?? 0,
                                    FreightAmount = g.Sum(s => (decimal?)s.FreightCost) ?? 0
                                };


            var customers = from customer in context.Customers
                            let invoiceAmount = customerInvoices.Where(s => s.CustomerId == customer.Id)
                                                                .Sum(s => (decimal?)s.InvoiceAmount) ?? 0
                            let paymentAmount = customerPayments.Where(s => s.CustomerId == customer.Id)
                                                                .Sum(s => (decimal?)s.PaymentAmount) ?? 0
                            let salesReturnAmount = customerSalesReturns.Where(s => s.CustomerId == customer.Id)
                                                                        .Sum(s => (decimal?)s.SalesReturnAmount) ?? 0
                            let creditAmount = customerSOACR.Where(s => s.CustomerId == customer.Id)
                                                            .Sum(s => (decimal?)s.CreditMemoAmount) ?? 0
                            let debitAmount = customerSOACR.Where(s => s.CustomerId == customer.Id)
                                                           .Sum(s => (decimal?)s.DebitMemoAmount) ?? 0
                            let freightAmount = customerSOACR.Where(s => s.CustomerId == customer.Id)
                                                             .Sum(s => (decimal?)s.FreightAmount) ?? 0
                            where (string.IsNullOrWhiteSpace(search)
                            || customer.Code.ToLower().Contains(search.ToLower())
                            || customer.Name.ToLower().Contains(search.ToLower()))
                            && (agent == null || customer.AgentId == agent)
                            orderby customer.Code ascending
                            select new JsonCustomerOptionsModel
                            {
                                Id = customer.Id.ToString(),
                                Text = customer.Code,
                                Name = customer.Name,
                                Address = customer.ShippingAddresses
                                                  .Where(s => s.IsDefault == true)
                                                  .Select(s => s.Address)
                                                  .FirstOrDefault() ?? "N/A",
                                CreditTerm = customer.CreditTerm.Name,
                                Agent = customer.Agent.FirstName + " " + customer.Agent.LastName,
                                AgentCode = customer.Agent.Code,
                                AgentId = customer.Agent.Id,
                                OutstandingBalanceD = invoiceAmount
                                                    - paymentAmount
                                                    - salesReturnAmount
                                                    - creditAmount
                                                    + debitAmount
                                                    + freightAmount,
                                CustomerStatus = customer.CustomerAccountStatus.Name,
                                Region = customer.Area.Region.Name
                            };

            var customersList = await customers.ToListAsync();

            var customersFiltered = customersList.Skip((page - 1) * 50).Take(50).ToList();
            return new Select2Model<JsonCustomerOptionsModel>
            {
                //incomplete_results = false,
                items = customersFiltered,
                total_count = customersList.Count
            };
        }

        public async Task<Select2Model<JsonCustomerOptionsModel>> GetOptionsWithOrder(string search, int page)
        {
            var customerInvoices = from inv in context.InvoiceLineItems
                                   where inv.Invoice.Status.Code.ToLower() == "invoiced"
                                   join oli in context.SalesOrderLineItems on new { inv.Invoice.SalesOrderId, inv.ItemId } equals new { oli.SalesOrderId, oli.ItemId }
                                   let discount1 = (100 - oli.Discount1) / 100
                                   let discount2 = (100 - oli.Discount2) / 100
                                   group new { inv.Quantity, inv.UnitPrice, discount1, discount2 } by inv.Invoice.SalesOrder.Customer.Id into g
                                   select new
                                   {
                                       CustomerId = g.Key,
                                       InvoiceAmount = g.Sum(s => (decimal?)(s.Quantity * s.UnitPrice * s.discount1 * s.discount2)) ?? 0
                                   };

            var customerPayments = from pay in context.CustomerPaymentDetails
                                   where pay.Invoice.Status.Code.ToLower() == "invoiced"
                                   group pay by pay.Invoice.SalesOrder.Customer.Id into g
                                   select new
                                   {
                                       CustomerId = g.Key,
                                       PaymentAmount = g.Sum(s => (decimal?)(s.PaidAmount) ?? 0)
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
                                where s.Invoice.Status.Code.ToLower() == "invoiced"
                                group s by s.Invoice.SalesOrder.Customer.Id into g
                                select new
                                {
                                    CustomerId = g.Key,
                                    CreditMemoAmount = g.Sum(s => (decimal?)s.CreditAmount) ?? 0,
                                    DebitMemoAmount = g.Sum(s => (decimal?)s.DebitAmount) ?? 0,
                                    FreightAmount = g.Sum(s => (decimal?)s.FreightCost) ?? 0
                                };


            var customers = from customer in context.Customers
                            let invoiceAmount = customerInvoices.Where(s => s.CustomerId == customer.Id)
                                                                .Sum(s => (decimal?)s.InvoiceAmount) ?? 0
                            let paymentAmount = customerPayments.Where(s => s.CustomerId == customer.Id)
                                                                .Sum(s => (decimal?)s.PaymentAmount) ?? 0
                            let salesReturnAmount = customerSalesReturns.Where(s => s.CustomerId == customer.Id)
                                                                        .Sum(s => (decimal?)s.SalesReturnAmount) ?? 0
                            let creditAmount = customerSOACR.Where(s => s.CustomerId == customer.Id)
                                                            .Sum(s => (decimal?)s.CreditMemoAmount) ?? 0
                            let debitAmount = customerSOACR.Where(s => s.CustomerId == customer.Id)
                                                           .Sum(s => (decimal?)s.DebitMemoAmount) ?? 0
                            let freightAmount = customerSOACR.Where(s => s.CustomerId == customer.Id)
                                                             .Sum(s => (decimal?)s.FreightAmount) ?? 0
                            where (string.IsNullOrEmpty(search)
                            || customer.Code.ToLower().Contains(search.ToLower())
                            || customer.Name.ToLower().Contains(search.ToLower()))
                            && customer.SalesOrders
                                       .Any(s => s.Status.Code.ToLower() != "submitted"
                                              && s.Status.Code.ToLower() != "disapproved"
                                              && s.Status.Code.ToLower() != "draft")
                            orderby customer.Code ascending
                            select new JsonCustomerOptionsModel
                            {
                                Id = customer.Id.ToString(),
                                Text = customer.Code,
                                Name = customer.Name,
                                Address = customer.ShippingAddresses
                                                  .Where(s => s.IsDefault == true)
                                                  .Select(s => s.Address)
                                                  .FirstOrDefault() ?? "N/A",
                                CreditTerm = customer.CreditTerm.Name,
                                Agent = customer.Agent.FirstName + " " + customer.Agent.LastName,
                                OutstandingBalanceD = invoiceAmount
                                                    - paymentAmount
                                                    - salesReturnAmount
                                                    - creditAmount
                                                    + debitAmount
                                                    + freightAmount
                            };

            var customersList = await customers.ToListAsync();
            var customersFiltered = customersList.Skip((page - 1) * 50).Take(50).ToList();
            return new Select2Model<JsonCustomerOptionsModel>
            {
                //incomplete_results = false,
                items = customersFiltered,
                total_count = customersList.Count
            };
        }

        public async Task<IList<JsonOptionsModel>> GetOptions2(int? IncludeId)
        {
            return await context.Customers
                .Where(s => s.IsActive || s.Id == IncludeId)
                .Select(s => new JsonOptionsModel
                {
                    Id = s.Id.ToString(),
                    Text = s.Name,
                    Selected = IncludeId != null && IncludeId == s.Id
                })
                .OrderBy(s => s.Text)
                .ToListAsync();
        }


        public async Task<int?> GetIdAsync(string? Name)
        {
            var customers = await context.Customers
                                         .Where(s => s.Name.ToLower() == Name.ToLower())
                                         .FirstOrDefaultAsync();
            return customers?.Id;
        }

        public async Task<int?> GetIdByCodeAsync(string? Code)
        {
            var customers = await context.Customers.Where(s => s.Code == Code).FirstOrDefaultAsync();
            return customers?.Id;
        }
        public CustomerVM? GetCustomerDtlVM(int? id)
        {
            var customerVM = (from cust in context.Customers
                              join userc in userManager.Users on cust.UserCreated equals userc.Id
                              join userm in userManager.Users on cust.UserModified equals userm.Id into um
                              from userm in um.DefaultIfEmpty()
                              where cust.Id == id
                              select new CustomerVM
                              {
                                  Id = cust.Id,
                                  Code = cust.Code,
                                  Name = cust.Name,
                                  AgentId = cust.Agent.Id,
                                  AgentName = cust.Agent.FirstName + (cust.Agent.MiddleName == null ? "" : " " + cust.Agent.MiddleName) + " " + cust.Agent.LastName,
                                  AreaId = cust.Area.Id,
                                  AreaName = cust.Area.Name,
                                  CommissionRateD = cust.CommissionRate == null ? cust.Area.CommissionRate : cust.CommissionRate,
                                  CommissionRateIsArea = cust.CommissionRate == null,
                                  CustomerAccountStatusId = cust.CustomerAccountStatus.Id,
                                  CustomerAccountStatusName = cust.CustomerAccountStatus.Name,
                                  CustomerTypeId = cust.CustomerType.Id,
                                  CustomerTypeName = cust.CustomerType.Name,
                                  CreditLimitD = cust.CreditLimit,
                                  CreditTermId = cust.CreditTerm.Id,
                                  CreditTermName = cust.CreditTerm.Name,
                                  Email = cust.Email,
                                  RegionId = cust.Area.RegionId,
                                  RegionName = cust.Area.Region.Name,
                                  TIN = cust.TIN,
                                  VAT = cust.VAT,
                                  Branch = cust.Branch,
                                  Priorities = cust.Priorities,
                                  Remarks = cust.Remarks,
                                  IsActive = cust.IsActive,
                                  CreatedBy = SystemFormat.Name(userc.FirstName, userc.LastName, userc.Nickname),
                                  DateCreatedD = cust.DateCreated,
                                  ModifiedBy = userm == null ? null : SystemFormat.Name(userm.FirstName, userm.LastName, userm.Nickname),
                                  DateModifiedD = cust.DateModified
                              }).FirstOrDefault();
            return customerVM;
        }

        public async Task<IList<CustomerVM>> GetAllCustomerAsync()
        {
            var customerInvoices = from inv in context.InvoiceLineItems
                                   where inv.Invoice.Status.Code.ToLower() == "invoiced"
                                   join oli in context.SalesOrderLineItems on new { inv.Invoice.SalesOrderId, inv.ItemId } equals new { oli.SalesOrderId, oli.ItemId }
                                   let discount1 = (100 - oli.Discount1) / 100
                                   let discount2 = (100 - oli.Discount2) / 100
                                   group new { inv.Quantity, inv.UnitPrice, discount1, discount2 } by inv.Invoice.SalesOrder.Customer.Id into g
                                   select new
                                   {
                                       CustomerId = g.Key,
                                       InvoiceAmount = g.Sum(s => (decimal?)(s.Quantity * s.UnitPrice * s.discount1 * s.discount2)) ?? 0
                                   };

            var customerPayments = from pay in context.CustomerPaymentDetails
                                   where pay.Invoice.Status.Code.ToLower() == "invoiced"
                                   group pay by pay.Invoice.SalesOrder.Customer.Id into g
                                   select new
                                   {
                                       CustomerId = g.Key,
                                       PaymentAmount = g.Sum(s => (decimal?)(s.PaidAmount) ?? 0)
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
                                where s.Invoice.Status.Code.ToLower() == "invoiced"
                                group s by s.Invoice.SalesOrder.Customer.Id into g
                                select new
                                {
                                    CustomerId = g.Key,
                                    CreditMemoAmount = g.Sum(s => (decimal?)s.CreditAmount) ?? 0,
                                    DebitMemoAmount = g.Sum(s => (decimal?)s.DebitAmount) ?? 0,
                                    FreightAmount = g.Sum(s => (decimal?)s.FreightCost) ?? 0
                                };


            var customers = from customer in context.Customers
                            join userc in userManager.Users on customer.UserCreated equals userc.Id
                            join userm in userManager.Users on customer.UserModified equals userm.Id into um
                            from userm in um.DefaultIfEmpty()
                            let invoiceAmount = customerInvoices.Where(s => s.CustomerId == customer.Id)
                                                                .Sum(s => (decimal?)s.InvoiceAmount) ?? 0
                            let paymentAmount = customerPayments.Where(s => s.CustomerId == customer.Id)
                                                                .Sum(s => (decimal?)s.PaymentAmount) ?? 0
                            let salesReturnAmount = customerSalesReturns.Where(s => s.CustomerId == customer.Id)
                                                                        .Sum(s => (decimal?)s.SalesReturnAmount) ?? 0
                            let creditAmount = customerSOACR.Where(s => s.CustomerId == customer.Id)
                                                            .Sum(s => (decimal?)s.CreditMemoAmount) ?? 0
                            let debitAmount = customerSOACR.Where(s => s.CustomerId == customer.Id)
                                                           .Sum(s => (decimal?)s.DebitMemoAmount) ?? 0
                            let freightAmount = customerSOACR.Where(s => s.CustomerId == customer.Id)
                                                             .Sum(s => (decimal?)s.FreightAmount) ?? 0
                            select new CustomerVM
                            {
                                Id = customer.Id,
                                Code = customer.Code,
                                Name = customer.Name,
                                AgentId = customer.AgentId,
                                AgentName = customer.Agent.FirstName + " " + customer.Agent.LastName,
                                AreaId = customer.AreaId,
                                AreaName = customer.Area.Name,
                                CommissionRateD = customer.CommissionRate == null ? customer.Area.CommissionRate : customer.CommissionRate,
                                CommissionRateIsArea = customer.CommissionRate == null,
                                CustomerAccountStatusId = customer.CustomerAccountStatusId,
                                CustomerAccountStatusName = customer.CustomerAccountStatus.Name,
                                CustomerTypeId = customer.CustomerTypeId,
                                CustomerTypeName = customer.CustomerType.Name,
                                CreditLimitD = customer.CreditLimit,
                                CreditTermId = customer.CreditTermId,
                                CreditTermName = customer.CreditTerm.Name,
                                Email = customer.Email,
                                OutstandingBalanceD = invoiceAmount
                                                    //- (paymentAmount + checkAmount)
                                                    - paymentAmount
                                                    - salesReturnAmount
                                                    - creditAmount
                                                    + debitAmount
                                                    + freightAmount,
                                RegionId = customer.Area.RegionId,
                                RegionName = customer.Area.Region.Name,
                                TIN = customer.TIN,
                                VAT = customer.VAT,
                                Remarks = customer.Remarks,
                                IsActive = customer.IsActive,
                                CreatedBy = userc.FirstName + " " + userc.LastName,
                                DateCreatedD = customer.DateCreated,
                                ModifiedBy = userm == null ? null : userm.FirstName + " " + userm.LastName,
                                DateModifiedD = customer.DateModified == null ? null : customer.DateModified
                            };
            return await customers.ToListAsync();
        }

        public async Task<ForDataTables<CustomerVM>> GetListDataTables(DataTablesRequestForm form)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            //var data = context.AuditLogs.Include(s=>s.User).AsQueryable();
            var data = (await GetAllCustomerAsync()).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Code.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.Name.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.AgentName.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.CreditLimit.ToString().Contains(form.SearchValue)
                                    || x.OutstandingBalance.ToString().Contains(form.SearchValue));
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
                        case "Code": data = data.OrderBy(s => s.Code); break;
                        case "Name": data = data.OrderBy(s => s.Name); break;
                        case "AgentName": data = data.OrderBy(s => s.AgentName); break;
                        case "CreditLimit": data = data.OrderBy(s => s.CreditLimitD); break;
                        case "OutstandingBalance": data = data.OrderBy(s => s.OutstandingBalanceD); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Code": data = data.OrderByDescending(s => s.Code); break;
                        case "Name": data = data.OrderByDescending(s => s.Name); break;
                        case "AgentName": data = data.OrderByDescending(s => s.AgentName); break;
                        case "CreditLimit": data = data.OrderByDescending(s => s.CreditLimitD); break;
                        case "OutstandingBalance": data = data.OrderByDescending(s => s.OutstandingBalanceD); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<CustomerVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList
            };
        }

        public async Task<IList<Customer>> GetAllActiveCustomerAsync(int includeId)
        {
            return await context.Customers.Where(s => s.IsActive || s.Id == includeId).ToListAsync();
            //var areas = await context.Areas.ToListAsync();
            //return areas;
        }

        public async Task<bool> IsCustomerCodeExist(string code)
        {
            return await context.Customers.AnyAsync(c => c.Code == code);
        }

        public async Task<bool> IsCustomerInUse(int id)
        {
            return await context.Areas.AnyAsync(s => s.Id == id);
        }

        public async Task<bool> IsCodeExistAsync(string code)
        {
            return await context.Customers.AnyAsync(s => s.Code == code);
        }

        public async Task<bool> IsCustomerIdExistAsync(int id)
        {
            return await context.Customers.AnyAsync(s => s.Id == id);
        }

        public async Task DeactivateCustomerAsync(int id)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var customers = await context.Customers.FirstAsync(s => s.Id == id);
            customers.IsActive = false;
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task ReactivateCustomerAsync(int id)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var customers = await context.Customers.FirstAsync(s => s.Id == id);
            customers.IsActive = true;
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task UpdateAsync(CustomerVM custVM)
        {
            var cust = context.Customers.Where(s => s.Id == custVM.Id).FirstOrDefault();
            var area = context.Areas.Where(s => s.Id == custVM.AreaId).FirstOrDefault();

            if (cust == null || area == null) throw new DbUpdateConcurrencyException();

            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            cust.Code = custVM.Code;
            cust.Name = custVM.Name;
            cust.IsActive = true;

            cust.AgentId = custVM.AgentId;
            cust.AreaId = custVM.AreaId;
            cust.CommissionRate = custVM.UseAreaRate == false ? custVM.CommissionRateD : null;
            cust.CreditLimit = custVM.CreditLimitD;
            cust.CreditTermId = custVM.CreditTermId;
            cust.CustomerAccountStatusId = custVM.CustomerAccountStatusId;
            cust.CustomerTypeId = custVM.CustomerTypeId;
            cust.Email = custVM.Email;
            cust.Remarks = custVM.Remarks;
            cust.TIN = custVM.TIN;
            cust.VAT = custVM.VAT;
            cust.Branch = custVM.Branch;
            cust.Priorities = custVM.Priorities;
            //cust.Address = custVM.Address;
            //cust.ContactNumber = custVM.ContactNumber;
            //cust.ContactPerson = custVM.ContactPerson;
            //cust.FaxNumber = custVM.FaxNumber;

            context.Customers.Update(cust);
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task AddAsync(CustomerVM custVM)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);


            var cust = new Customer
            {
                Code = custVM.Code,
                Name = custVM.Name,
                IsActive = true,

                AgentId = custVM.AgentId,
                AreaId = custVM.AreaId,
                CommissionRate = custVM.UseAreaRate == false ? custVM.CommissionRateD : null,
                CreditLimit = custVM.CreditLimitD,
                CreditTermId = custVM.CreditTermId,
                CustomerAccountStatusId = custVM.CustomerAccountStatusId,
                CustomerTypeId = custVM.CustomerTypeId,
                Email = custVM.Email,
                Remarks = custVM.Remarks,
                TIN = custVM.TIN,
                VAT = custVM.VAT,
                Branch = custVM.Branch,
                Priorities = custVM.Priorities
                //Address = custVM.Address,
                //ContactNumber = custVM.ContactNumber,
                //ContactPerson = custVM.ContactPerson,
                //FaxNumber = custVM.FaxNumber,
            };
            await context.Customers.AddAsync(cust);
            await context.SaveChangesAsync(currentUserId);
        }

        //public async Task AddForwarderAsync(CustomerVM forwarderVM)
        //{
        //    string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
        //    var forwarders = new Forwarder
        //    {
        //        CustomerId = forwarderVM.Id,
        //        Name = forwarderVM.ForwarderName,
        //        Address = forwarderVM.ForwarderAddress,
        //        PhoneNumber = forwarderVM.ForwarderPhoneNo,
        //        FaxNumber = forwarderVM.ForwarderFaxNo
        //    };
        //    await context.Forwarders.AddAsync(forwarders);
        //    await context.SaveChangesAsync(currentUserId);
        //}

        private async Task<IList<CustomerVM>> GetCustomerDetailsAsync(int id)
        {
            var customerVM = from cust in context.Customers
                                 //.Include(s => s.Area)
                                 //.Include(s => s.CustomerType)
                                 //.Include(s => s.Agent)
                                 //.Include(s => s.CreditTerm)
                             select new CustomerVM
                             {
                                 Code = cust.Code,
                                 Name = cust.Name,
                                 IsActive = true,

                                 AgentId = cust.AgentId,
                                 AreaId = cust.AreaId,
                                 CreditLimitD = cust.CreditLimit,
                                 CreditTermId = cust.CreditTermId,
                                 CustomerAccountStatusId = cust.CustomerAccountStatusId,
                                 CustomerTypeId = cust.CustomerTypeId,
                                 Email = cust.Email,
                                 Remarks = cust.Remarks,
                                 TIN = cust.TIN,
                                 VAT = cust.VAT,
                                 Branch = cust.Branch,
                                 Priorities = cust.Priorities
                                 //Address = cust.ShippingAddresses
                                 //              .Where(a => a.IsDefault == true)
                                 //              .Select(a => a.Address)
                                 //              .FirstOrDefault() ?? "N/A",
                                 //ContactNumber = cust.ShippingAddresses
                                 //                    .Where(a => a.IsDefault == true)
                                 //                    .Select(a => a.PhoneNumber)
                                 //                    .FirstOrDefault() ?? "N/A",
                                 //ContactPerson = cust.ShippingAddresses
                                 //                    .Where(a => a.IsDefault == true)
                                 //                    .Select(a => a.ContactPerson)
                                 //                    .FirstOrDefault() ?? "N/A",
                                 //FaxNumber = cust.ShippingAddresses
                                 //                .Where(a => a.IsDefault == true)
                                 //                .Select(a => a.FaxNumber)
                                 //                .FirstOrDefault() ?? "N/A"
                             };
            return await customerVM.ToListAsync();
        }

        public async Task<ForDataTables<CustomerVM>> GetCustomerDetailsDTAsync(DataTablesRequestForm form, int id)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetCustomerDetailsAsync(id)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.AgentName.ToLower().Contains(form.SearchValue.ToLower())
                                   || x.Code.ToLower().Contains(form.SearchValue.ToLower())
                                   || x.Name.ToLower().Contains(form.SearchValue.ToLower()));
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
                        case "AgentName": data = data.OrderBy(s => s.AgentName); break;
                        case "Code": data = data.OrderBy(s => s.Code); break;
                        case "CreditLimit": data = data.OrderBy(s => s.CreditLimit); break;
                        case "Name": data = data.OrderBy(s => s.Name); break;
                        case "OutstandingBalance": data = data.OrderBy(s => s.OutstandingBalance); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "AgentName": data = data.OrderByDescending(s => s.AgentName); break;
                        case "Code": data = data.OrderByDescending(s => s.Code); break;
                        case "CreditLimit": data = data.OrderByDescending(s => s.CreditLimit); break;
                        case "Name": data = data.OrderByDescending(s => s.Name); break;
                        case "OutstandingBalance": data = data.OrderByDescending(s => s.OutstandingBalance); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<CustomerVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        private async Task<IList<CustomerShippingVM>> GetCustomerShippingAsync(int id)
        {
            var customerVM = from shipping in context.ShippingAddresses
                             where shipping.CustomerId == id
                             select new CustomerShippingVM
                             {
                                 Id = shipping.Id,
                                 CustomerId = id,
                                 Address = shipping.Address,
                                 ContactPerson = shipping.ContactPerson,
                                 FaxNumber = shipping.FaxNumber ?? "N/A",
                                 PhoneNumber = shipping.PhoneNumber ?? "N/A",
                                 IsDefault = shipping.IsDefault,
                                 IsActive = shipping.IsActive
                             };
            return await customerVM.ToListAsync();
        }

        public async Task<IList<CustomerBrandDiscountVM>> GetCustomerBrandDiscountAsync(int id)
        {
            var customerVM = from brandDiscount in context.CustomerBrandDiscount
                             join brand in context.Brands on brandDiscount.BrandId equals brand.Id
                             join customer in context.Customers on brandDiscount.CustomerId equals customer.Id
                             join userc in userManager.Users on brandDiscount.UserCreated equals userc.Id
                             join userm in userManager.Users on brandDiscount.UserModified equals userm.Id into um
                             from userm in um.DefaultIfEmpty()
                             where brandDiscount.CustomerId == id 
                             select new CustomerBrandDiscountVM
                             {
                                 Id = brandDiscount.Id,
                                 CustomerId = id,
                                 CustomerName = customer.Name,
                                 Discount = brandDiscount.Discount,
                                 BrandName = brand.Name,
                                 BrandId = brand.Id,
                                 DateCreatedD = brandDiscount.DateCreated,
                                 DateModifiedD = brandDiscount.DateModified,
                                 CreatedBy = SystemFormat.Name(userc.FirstName, userc.LastName, userc.Nickname),
                                 ModifiedBy = userm == null ? null : SystemFormat.Name(userm.FirstName, userm.LastName, userm.Nickname),
                                 IsActive = brandDiscount.IsActive

                             };
            return await customerVM.ToListAsync();
        }

        public async Task<ForDataTables<CustomerShippingVM>> GetCustomerShippingDTAsync(DataTablesRequestForm form, int id)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetCustomerShippingAsync(id)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Address.ToString().Contains(form.SearchValue)
                                    || x.ContactPerson.ToString().Contains(form.SearchValue)
                                    || x.FaxNumber.ToString().Contains(form.SearchValue)
                                    || x.PhoneNumber.ToString().Contains(form.SearchValue));
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
                        case "Address": data = data.OrderBy(s => s.Address); break;
                        case "ContactPerson": data = data.OrderBy(s => s.ContactPerson); break;
                        case "FaxNumber": data = data.OrderBy(s => s.FaxNumber); break;
                        case "PhoneNumber": data = data.OrderBy(s => s.PhoneNumber); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Address": data = data.OrderByDescending(s => s.Address); break;
                        case "ContactPerson": data = data.OrderByDescending(s => s.ContactPerson); break;
                        case "FaxNumber": data = data.OrderByDescending(s => s.FaxNumber); break;
                        case "PhoneNumber": data = data.OrderByDescending(s => s.PhoneNumber); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<CustomerShippingVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public async Task<ForDataTables<CustomerBrandDiscountVM>> GetCustomerBrandDiscountDTAsync(DataTablesRequestForm form, int id)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetCustomerBrandDiscountAsync(id)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.BrandName.ToString().Contains(form.SearchValue)
                                    || x.Discount.ToString().Contains(form.SearchValue)
                                    || x.DateCreated.ToString().Contains(form.SearchValue)
                                    || x.CreatedBy.ToString().Contains(form.SearchValue));
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
                        case "BrandName": data = data.OrderBy(s => s.BrandName); break;
                        case "Discount": data = data.OrderBy(s => s.Discount); break;
                        case "DateCreated": data = data.OrderBy(s => s.DateCreated); break;
                        case "CreatedBy": data = data.OrderBy(s => s.CreatedBy); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "BrandName": data = data.OrderByDescending(s => s.BrandName); break;
                        case "Discount": data = data.OrderByDescending(s => s.Discount); break;
                        case "DateCreated": data = data.OrderByDescending(s => s.DateCreated); break;
                        case "CreatedBy": data = data.OrderByDescending(s => s.CreatedBy); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<CustomerBrandDiscountVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public async Task<ResponseModel> SaveShippingAsync(CustomerShippingVM shippingVM)
        {
            ResponseModel response = new();
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

            var checkShipping = await context.ShippingAddresses.Where(s => (shippingVM.Id == null || (shippingVM.Id != null && shippingVM.Id != s.Id)) && s.Address.Trim().ToLower() == shippingVM.Address.Trim().ToLower()).FirstOrDefaultAsync();
            if (checkShipping != null)
            {
                if (checkShipping.CustomerId == shippingVM.CustomerId)
                {
                    response.Status = "Error";
                    response.Message = "Address already added";
                }
                else
                {
                    response.Status = "Error";
                    response.Message = "Address is used by different customer";
                }
            }
            else
            {

                var shipping = await context.ShippingAddresses.Where(s => s.Id == shippingVM.Id).FirstOrDefaultAsync();
                if (shipping == null)
                {
                    shipping = new ShippingAddress
                    {
                        CustomerId = shippingVM.CustomerId,
                        Address = shippingVM.Address,
                        PhoneNumber = shippingVM.PhoneNumber,
                        FaxNumber = shippingVM.FaxNumber,
                        ContactPerson = shippingVM.ContactPerson,
                        IsActive = true,
                        IsDefault = false
                    };

                    await context.ShippingAddresses.AddAsync(shipping);
                    await context.SaveChangesAsync(currentUserId);

                    response.Status = "Success";
                    response.Message = "Address successfully added";
                }
                else
                {
                    shipping.CustomerId = shippingVM.CustomerId;
                    shipping.Address = shippingVM.Address;
                    shipping.PhoneNumber = shippingVM.PhoneNumber;
                    shipping.FaxNumber = shippingVM.FaxNumber;
                    shipping.ContactPerson = shippingVM.ContactPerson;

                    context.ShippingAddresses.Update(shipping);
                    await context.SaveChangesAsync(currentUserId);

                    response.Status = "Success";
                    response.Message = "Address successfully updated";
                }
            }

            return response;
        }

        public async Task<ResponseModel> SaveDiscountAsync(CustomerBrandDiscountVM brandDiscountVM)
        {
            ResponseModel response = new();
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

            var checkDiscount = await context.CustomerBrandDiscount.Where(s => (brandDiscountVM.Id == null || (brandDiscountVM.Id != null && brandDiscountVM.Id != s.Id)) 
                                                                               && (s.CustomerId == brandDiscountVM.CustomerId && s.BrandId == brandDiscountVM.BrandId && s.IsActive)).FirstOrDefaultAsync();
            if (checkDiscount != null)
            {
                if (checkDiscount.CustomerId == brandDiscountVM.CustomerId)
                {
                    response.Status = "Error";
                    response.Message = "Brand already added";
                }
                
            }
            else
            {

                var brandDiscount = await context.CustomerBrandDiscount.Where(s => s.Id == brandDiscountVM.Id).FirstOrDefaultAsync();
                if (brandDiscount == null)
                {
                    brandDiscount = new CustomerBrandDiscount
                    {
                        CustomerId = brandDiscountVM.CustomerId,
                        BrandId = brandDiscountVM.BrandId,
                        Discount = brandDiscountVM.Discount,
                        IsActive = true


                    };

                    await context.CustomerBrandDiscount.AddAsync(brandDiscount);
                    await context.SaveChangesAsync(currentUserId);

                    response.Status = "Success";
                    response.Message = "Discount successfully added";
                }
                else
                {
                    brandDiscount.CustomerId = brandDiscountVM.CustomerId;
                    brandDiscount.BrandId = brandDiscountVM.BrandId;
                    brandDiscount.Discount = brandDiscountVM.Discount;
                    

                    context.CustomerBrandDiscount.Update(brandDiscount);
                    await context.SaveChangesAsync(currentUserId);

                    response.Status = "Success";
                    response.Message = "Discount successfully updated";
                }
            }

            return response;
        }

        public async Task<ResponseModel> SetDefaultShippingAsync(int Id)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            ResponseModel response = new();

            var shipping = await context.ShippingAddresses.Where(s => s.Id == Id).FirstOrDefaultAsync();
            if (shipping == null)
            {
                response.Status = "Error";
                response.Message = "Address is invalid";
            }
            else
            {
                var others = await context.ShippingAddresses
                                          .Where(s => s.CustomerId == shipping.CustomerId && s.IsDefault == true)
                                          .ToListAsync();
                foreach (var other in others)
                {
                    other.IsDefault = false;
                }
                context.ShippingAddresses.UpdateRange(others);

                shipping.IsDefault = true;
                context.ShippingAddresses.Update(shipping);

                await context.SaveChangesAsync(currentUserId);

                response.Status = "Success";
                response.Message = "Default address successfully updated";
            }

            return response;
        }

        public async Task<ResponseModel> DeleteOrDeactivateShippingAsync(int Id)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            ResponseModel response = new();
            var shipping = await context.ShippingAddresses.Where(s => s.Id == Id).FirstOrDefaultAsync();
            if (shipping == null)
            {
                response.Status = "Error";
                response.Message = "Address is invalid";
            }
            else if (shipping.IsDefault == true)
            {
                response.Status = "Error";
                response.Message = "Cannot delete default address";
            }
            else
            {
                if (await context.ShippingAddresses.AnyAsync(s => s.CustomerId == shipping.CustomerId
                                                               && s.Id != shipping.Id
                                                               && s.IsActive == true))
                {
                    if (await context.ReceptaclePrintHistories.AnyAsync(s => s.ShippingAddressId == shipping.Id))
                    {
                        shipping.IsActive = false;
                        context.ShippingAddresses.Update(shipping);
                        await context.SaveChangesAsync(currentUserId);

                        response.Status = "Success";
                        response.Message = "Address successfully deactivated";
                    }
                    else
                    {
                        context.ShippingAddresses.Remove(shipping);
                        await context.SaveChangesAsync(currentUserId);

                        response.Status = "Success";
                        response.Message = "Address successfully deleted";
                    }
                }
                else
                {
                    response.Status = "Error";
                    response.Message = "Cannot delete because this is the only active address of the customer";
                }
            }

            return response;
        }

        public async Task<ResponseModel> DeleteOrDeactivateDiscountAsync(int Id)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            ResponseModel response = new();
            var discount = await context.CustomerBrandDiscount.Where(s => s.Id == Id).FirstOrDefaultAsync();
            if (discount == null)
            {
                response.Status = "Error";
                response.Message = "Discount is invalid";
            }
            
            else
            {
                
                    if (await context.SalesOrderLineItems.AnyAsync(s => s.CustomerBrandDiscountId == discount.Id))
                    {

                        discount.IsActive = false;
                        context.CustomerBrandDiscount.Update(discount);
                        await context.SaveChangesAsync(currentUserId);

                        response.Status = "Success";
                        response.Message = "Discount successfully deactivated";


                    }
                    else
                    {
                        context.CustomerBrandDiscount.Remove(discount);
                        await context.SaveChangesAsync(currentUserId);

                        response.Status = "Success";
                        response.Message = "Address successfully deleted";


                    }
                   
                
                
            }

            return response;
        }

        public async Task<ResponseModel> ReactivateShippingAsync(int Id)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            ResponseModel response = new();
            var shipping = await context.ShippingAddresses.Where(s => s.Id == Id).FirstOrDefaultAsync();
            if (shipping == null)
            {
                response.Status = "Error";
                response.Message = "Address is invalid";
            }
            else
            {
                shipping.IsActive = true;
                shipping.IsDefault = false;
                context.ShippingAddresses.Update(shipping);
                await context.SaveChangesAsync(currentUserId);

                response.Status = "Success";
                response.Message = "Address successfully reactivated";
            }

            return response;
        }

        public async Task<ResponseModel> ReactivateDiscountAsync(int Id)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            ResponseModel response = new();
            var discount = await context.CustomerBrandDiscount.Where(s => s.Id == Id).FirstOrDefaultAsync();
            if (discount == null)
            {
                response.Status = "Error";
                response.Message = "Discount is invalid";
            }
            else
            {
                discount.IsActive = true;
                context.CustomerBrandDiscount.Update(discount);
                await context.SaveChangesAsync(currentUserId);

                response.Status = "Success";
                response.Message = "Discount successfully reactivated";
            }

            return response;
        }

        private async Task<IList<CustomerVM>> GetCustomerPaymentAsync(int id)
        {
            var customerVM = from customer in context.Customers
                             join cp in context.CustomerPayments on customer.Id equals cp.CustomerId
                             join userc in userManager.Users on cp.UserCreated equals userc.Id
                             join userm in userManager.Users on cp.UserModified equals userm.Id into um
                             from userm in um.DefaultIfEmpty()
                             let soalit = context.SOACRLineItems.Where(s => s.SOACRId == cp.SOACRId).ToList()
                             let tbr = context.SOACRLineItemTBRs.Where(s => soalit.Select(a => a.Id).Contains(s.SOACRLineItemId)).Sum(s => s.Amount)
                             let invamt = (from ili in context.InvoiceLineItems
                                           join inv in context.Invoices on ili.InvoiceId equals inv.Id
                                           join sli in context.SOACRLineItems on inv.Id equals sli.InvoiceId
                                           join soa in context.SOACRs on sli.SOACRId equals soa.Id
                                           where cp.SOACRId == soa.Id
                                           select new
                                           {
                                               Amount = ili.Quantity * ili.UnitPrice
                                           }).ToList()
                             let totalPayment = cp.BackLoad + cp.CashAdvance + cp.Cash + cp.Discount + cp.OffSet + cp.OverPayment + cp.Tax + cp.TKTSolicitation + (cp.SOACRChecks.Sum(s => (decimal?)s.CheckAmount) ?? 0)
                             //let invamttotal = invamt.Sum(s => s.Amount) - soalit.Sum(s => s.CreditAmount + s.DebitAmount + s.FreightCost) - tbr - totalPayment
                             let invamttotal = totalPayment - invamt.Sum(s=>s.Amount) + cp.OverPayment
                             where cp.CustomerId == id
                             select new CustomerVM
                             {
                                 AmountPaidD = totalPayment,
                                 CRNo = cp.CRNumber,
                                 PaymentRemarks = cp.Remarks,
                                 //OverPaymentD = 0 > invamttotal ? invamttotal * -1 : 0,
                                 OverPaymentD = invamttotal,
                                 SOANo = cp.SOACRId,
                                 TotalInvoiceAmountD = invamt.Sum(s => s.Amount),
                                 CreatedBy = SystemFormat.Name(userc.FirstName, userc.LastName, userc.Nickname),
                                 DateCreatedD = cp.DateCreated,
                                 ModifiedBy = userm == null ? null : SystemFormat.Name(userm.FirstName, userm.LastName, userm.Nickname),
                                 DateModifiedD = cp.DateCreated
                             };
            return await customerVM.ToListAsync();
        }

        public async Task<ForDataTables<CustomerVM>> GetCustomerPaymentDTAsync(DataTablesRequestForm form, int id)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetCustomerPaymentAsync(id)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.AmountPaid.ToString().Contains(form.SearchValue)
                                    || x.CRNo.ToString().Contains(form.SearchValue)
                                    || x.OverPayment.ToString().Contains(form.SearchValue)
                                    || x.PaymentRemarks.ToString().Contains(form.SearchValue)
                                    || x.SOANo.ToString().Contains(form.SearchValue)
                                    || x.TotalInvoiceAmount.ToString().Contains(form.SearchValue)
                                    || x.CreatedBy.ToString().Contains(form.SearchValue)
                                    || x.DateCreated.ToString().Contains(form.SearchValue)
                                    || (x.ModifiedBy != null && x.ModifiedBy.ToString().Contains(form.SearchValue))
                                    || (x.DateModified != null && x.DateModified.ToString().Contains(form.SearchValue)));
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
                        case "AmountPaid": data = data.OrderBy(s => s.AmountPaid); break;
                        case "CRNo": data = data.OrderBy(s => s.CRNo); break;
                        case "OverPayment": data = data.OrderBy(s => s.OverPayment); break;
                        case "PaymentRemarks": data = data.OrderBy(s => s.PaymentRemarks); break;
                        case "SOANo": data = data.OrderBy(s => s.SOANo); break;
                        case "TotalInvoiceAmount": data = data.OrderBy(s => s.TotalInvoiceAmountD); break;
                        case "CreatedBy": data = data.OrderBy(s => s.CreatedBy); break;
                        case "DateCreated": data = data.OrderBy(s => s.DateCreatedD); break;
                        case "ModifiedBy": data = data.OrderBy(s => s.ModifiedBy); break;
                        case "DateModified": data = data.OrderBy(s => s.DateModifiedD); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "AmountPaid": data = data.OrderByDescending(s => s.AmountPaid); break;
                        case "CRNo": data = data.OrderByDescending(s => s.CRNo); break;
                        case "OverPayment": data = data.OrderByDescending(s => s.OverPayment); break;
                        case "PaymentRemarks": data = data.OrderByDescending(s => s.PaymentRemarks); break;
                        case "SOANo": data = data.OrderByDescending(s => s.SOANo); break;
                        case "TotalInvoiceAmount": data = data.OrderByDescending(s => s.TotalInvoiceAmount); break;
                        case "CreatedBy": data = data.OrderByDescending(s => s.CreatedBy); break;
                        case "DateCreated": data = data.OrderByDescending(s => s.DateCreated); break;
                        case "ModifiedBy": data = data.OrderByDescending(s => s.ModifiedBy); break;
                        case "DateModified": data = data.OrderByDescending(s => s.DateModified); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<CustomerVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        private IQueryable<CustomerVM> GetCustomerReceivable(int Id)
        {

            return from inv in context.Invoices
                   where inv.SalesOrder.Customer.Id == Id
                      && inv.Status.Code.ToLower() == "invoiced"
                   select new CustomerVM
                   {
                       InvoiceNo = inv.InvoiceNumber,
                       BIRInvoiceNo = inv.ControlNumber,
                       InvoicedDateD = inv.InvoiceDate,
                       InvoicedAmountD = ((from ili in inv.InvoiceLineItems
                                         join soli in context.SalesOrderLineItems on new { ili.Invoice.SalesOrderId, ili.ItemId } equals new { soli.SalesOrderId, soli.ItemId }
                                         let discount1 = (100 - soli.Discount1) / 100
                                         let discount2 = (100 - soli.Discount2) / 100
                                         select (decimal?)(ili.Quantity * ili.UnitPrice * discount1 * discount2)
                                         ).Sum() ?? 0)
                                       - (context.CustomerPaymentDetails
                                                 .Where(s => s.InvoiceId == inv.Id)
                                                 .Sum(s => (decimal?)s.PaidAmount) ?? 0)
                                       - ((from tbr in context.SOACRLineItemTBRs
                                           let discount1 = (100 - tbr.SalesReturnLineItem.Discount1) / 100
                                           let discount2 = (100 - tbr.SalesReturnLineItem.Discount2) / 100
                                           where tbr.SalesReturnLineItem.InvoiceLineItem!.Invoice.Id == inv.Id
                                           select (decimal?)(tbr.SalesReturnLineItem.Quantity * tbr.SalesReturnLineItem.SellingPrice * discount1 * discount2)).Sum() ?? 0)
                                       - (context.SOACRLineItems
                                                 .Where(s => s.InvoiceId == inv.Id)
                                                 .Sum(s => (decimal?)s.CreditAmount) ?? 0)
                                       + (context.SOACRLineItems
                                                 .Where(s => s.InvoiceId == inv.Id)
                                                 .Sum(s => (decimal?)s.DebitAmount) ?? 0)
                                       + (context.SOACRLineItems
                                                 .Where(s => s.InvoiceId == inv.Id)
                                                 .Sum(s => (decimal?)s.FreightCost) ?? 0)
                   };
        }

        private async Task<IList<CustomerVM>> GetCustomerReceivableAsync(int Id)
        {
            return await GetCustomerReceivable(Id).ToListAsync();
        }

        public async Task<ForDataTables<CustomerVM>> GetCustomerReceivableDTAsync(DataTablesRequestForm form, int id)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetCustomerReceivableAsync(id)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.InvoiceNo.ToString().Contains(form.SearchValue)
                                    || x.BIRInvoiceNo.ToString().Contains(form.SearchValue)
                                    || x.InvoicedDateD.ToString().Contains(form.SearchValue)
                                    || x.InvoicedAmount.ToString().Contains(form.SearchValue));
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
                        case "InvoiceNo": data = data.OrderBy(s => s.InvoiceNo); break;
                        case "BIRInvoiceNo": data = data.OrderBy(s => s.BIRInvoiceNo); break;
                        case "InvoicedDate": data = data.OrderBy(s => s.InvoicedDateD); break;
                        case "InvoicedAmount": data = data.OrderBy(s => s.InvoicedAmount); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "InvoiceNo": data = data.OrderByDescending(s => s.InvoiceNo); break;
                        case "BIRInvoiceNo": data = data.OrderByDescending(s => s.BIRInvoiceNo); break;
                        case "InvoicedDate": data = data.OrderByDescending(s => s.InvoicedDateD); break;
                        case "InvoicedAmount": data = data.OrderByDescending(s => s.InvoicedAmount); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<CustomerVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public async Task<bool> UploadCombinationExistAsync(CustomerVM customerVM)
        {
            return (await GetAllCustomerAsync()).Any(s => s.Code == customerVM.Code
                                                       && s.Name == customerVM.Name
                                                       && s.CommissionRate == customerVM.CommissionRate
                                                       && s.RegionName == customerVM.RegionName
                                                       && s.AreaName == customerVM.AreaName
                                                       && s.CustomerTypeName == customerVM.CustomerTypeName
                                                       && s.Email == customerVM.Email
                                                       && s.AgentName == customerVM.AgentName
                                                       && s.TIN == customerVM.TIN
                                                       && s.VAT == customerVM.VAT
                                                       && s.CreditTermName == customerVM.CreditTermName
                                                       && s.CreditLimit == customerVM.CreditLimit
                                                       && s.Remarks == customerVM.Remarks);
        }
    }
}