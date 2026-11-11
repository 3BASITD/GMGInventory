using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using GMGInventory.Data.Migrations;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Dynamic.Core;

namespace GMGInventory.Application.Repositories
{
    public class ReportSalesRepository : IReportSalesRepository
    {
        private readonly ApplicationDbContext context;

        public ReportSalesRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        #region Agent Sales per Item

        public async Task<IList<AgentSalesPerItemRVM>> GetAgentSalesPerItem(ListFilterSalesModel filter)
        {
            var invoices = from ili in context.InvoiceLineItems
                           join soli in context.SalesOrderLineItems on new { ili.Invoice.SalesOrderId, ili.ItemId }
                                                                equals new { soli.SalesOrderId, soli.ItemId }
                           let discount1 = (100 - soli.Discount1) / 100
                           let discount2 = (100 - soli.Discount2) / 100
                           where (filter.DateFrom == null || filter.DateFrom <= ili.Invoice.InvoiceDate.Date)
                           && (filter.DateTo == null || ili.Invoice.InvoiceDate.Date <= filter.DateTo)
                           && (filter.Agent == null || ili.Invoice.SalesOrder.Agent.Id == filter.Agent)
                           && (filter.Brand == null || ili.Item.Brand.Id == filter.Brand)
                           && (filter.Item == null || ili.Item.Id == filter.Item)
                           && (ili.Invoice.Status.Code.Trim().ToLower() == "invoiced" || ili.Invoice.Status.Code.Trim().ToLower() == "draft")
                           select new AgentSalesPerItemRVM
                           {
                               Agent = ili.Invoice.SalesOrder.Agent.FirstName + " " + ili.Invoice.SalesOrder.Agent.LastName,
                               AgentId = ili.Invoice.SalesOrder.Agent.Id,
                               Brand = ili.Item.Brand.Name,
                               ItemCode = ili.Item.Code,
                               ItemName = ili.Item.Name,
                               ItemId = ili.Item.Id,
                               ItemType = ili.Item.ItemType.Name,
                               PriceD = ili.UnitPrice * discount1 * discount2,
                               QuantityD = ili.Quantity,
                               DateInvoiceD = ili.Invoice.InvoiceDate,
                               ControlNumber = ili.Invoice.ControlNumber,
                               InvoiceId = ili.Invoice.Id
                           };
            return await invoices.ToListAsync();
        }

        public async Task<ForDataTables<AgentSalesPerItemRVM>> GetAgentSalesPerItemDT(DataTablesRequestForm form, ListFilterSalesModel filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            int totalQuantity = 0;
            decimal totalAmount = 0;

            var data = (await GetAgentSalesPerItem(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace((form.SearchValue + "").Trim()))
            {
                data = data.Where(s => new[]
                {
                    s.Agent,
                    s.AmountD.ToString(),
                    s.Brand,
                    s.ControlNumber,
                    s.DateInvoice,
                    s.ItemCode,
                    s.ItemName,
                    s.ItemType,
                    s.PriceD.ToString(),
                    s.QuantityD.ToString()
                }.Any(a => a != null && a.Trim().ToLower().Contains((form.SearchValue + "").Trim().ToLower())));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                bool isAsc = form.SortColumnDirection == "asc";
                data = form.SortColumn switch
                {
                    "Amount" => isAsc ? data.OrderBy(s => s.AmountD) : data.OrderByDescending(s => s.AmountD),
                    "Price" => isAsc ? data.OrderBy(s => s.PriceD) : data.OrderByDescending(s => s.PriceD),
                    "Quantity" => isAsc ? data.OrderBy(s => s.QuantityD) : data.OrderByDescending(s => s.QuantityD),
                    _ => data.OrderBy(form.SortColumn + " " + form.SortColumnDirection)
                };
            }
            //get total quantity of data in table
            totalQuantity = data.Sum(s => s.QuantityD);
            //get total amount of data in table
            totalAmount = data.Sum(s => s.AmountD);
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<AgentSalesPerItemRVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList,
                TotalQuantity = totalQuantity,
                TotalAmount = totalAmount
            };
        }

        public async Task<AgentSalesPerItemPVM> PrintAgentSalesPerItem(ListFilterSalesModel filter)
        {
            var items = await GetAgentSalesPerItem(filter);
            var agent = await context.Agents.Where(s => s.Id == filter.Agent).FirstOrDefaultAsync();
            var brand = await context.Brands.Where(s => s.Id == filter.Brand).FirstOrDefaultAsync();
            var item = await context.Items.Where(s => s.Id == filter.Item).FirstOrDefaultAsync();
            return new AgentSalesPerItemPVM
            {
                Contents = items,
                DateFromD = filter.DateFrom.GetValueOrDefault(),
                DateToD = filter.DateTo.GetValueOrDefault(),
                Agent = filter.Agent == null || agent == null ? "All" : agent.FirstName + (agent.MiddleName == null ? "" : " " + agent.MiddleName) + " " + agent.LastName,
                Brand = filter.Brand == null || brand == null ? "All" : brand.Name,
                Item = filter.Item == null || item == null ? "All" : item.Name,
                TotalQuantityD = items.Sum(s => s.QuantityD),
                TotalAmountD = items.Sum(s => s.AmountD)
            };
        }

        #endregion Agent Sales per Item

        #region Brand Sales Report

        public async Task<IList<BrandSalesRVM>> GetBrandSales(ListFilterSalesModel filter)
        {
            var invoices = from ili in context.InvoiceLineItems
                           join soli in context.SalesOrderLineItems on new { ili.Invoice.SalesOrderId, ili.ItemId }
                                                                equals new { soli.SalesOrderId, soli.ItemId }
                           let discount1 = (100 - soli.Discount1) / 100
                           let discount2 = (100 - soli.Discount2) / 100
                           where (filter.DateFrom == null || filter.DateFrom <= ili.Invoice.InvoiceDate.Date)
                           && (filter.DateTo == null || ili.Invoice.InvoiceDate.Date <= filter.DateTo)
                           && (filter.Agent == null || ili.Invoice.SalesOrder.Agent.Id == filter.Agent)
                           && (filter.Brand == null || ili.Item.Brand.Id == filter.Brand)
                           && (ili.Invoice.Status.Code.Trim().ToLower() == "invoiced" || ili.Invoice.Status.Code.Trim().ToLower() == "draft")
                           select new BrandSalesRVM
                           {
                               AgentCode = ili.Invoice.SalesOrder.Agent.Code,
                               AgentName = ili.Invoice.SalesOrder.Agent.FirstName + " " + ili.Invoice.SalesOrder.Agent.LastName,
                               AgentId = ili.Invoice.SalesOrder.Agent.Id,
                               AmountD = ili.Quantity * ili.UnitPrice * discount1 * discount2,
                               Brand = ili.Item.Brand.Name
                           };
            var grpinvoice = from invoice in await invoices.ToListAsync()
                             orderby invoice.Brand ascending, invoice.AgentCode ascending
                             group invoice by new
                             {
                                 invoice.AgentCode,
                                 invoice.AgentName,
                                 invoice.AgentId,
                                 invoice.Brand
                             } into i
                             select new BrandSalesRVM
                             {
                                 AgentCode = i.Key.AgentCode,
                                 AgentName = i.Key.AgentName,
                                 AgentId = i.Key.AgentId,
                                 Brand = i.Key.Brand,
                                 AmountD = i.Sum(s => s.AmountD)
                             };
            return grpinvoice.ToList();
        }

        public async Task<ForDataTables<BrandSalesRVM>> GetBrandSalesDT(DataTablesRequestForm form, ListFilterSalesModel filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            decimal totalAmount = 0;

            var data = (await GetBrandSales(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace((form.SearchValue + "").Trim()))
            {
                data = data.Where(s => new[]
                {
                    s.AgentCode,
                    s.AgentName,
                    s.Amount,
                    s.AmountD.ToString(),
                    s.Brand
                }.Any(a => a != null && a.Trim().ToLower().Contains((form.SearchValue + "").Trim().ToLower())));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                bool isAsc = form.SortColumnDirection == "asc";
                data = form.SortColumn switch
                {
                    "Amount" => isAsc ? data.OrderBy(s => s.AmountD) : data.OrderByDescending(s => s.AmountD),
                    _ => data.OrderBy(form.SortColumn + " " + form.SortColumnDirection)
                };
            }
            //get total amount of data in table
            totalAmount = data.Sum(s => s.AmountD);
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<BrandSalesRVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList,
                TotalAmount = totalAmount
            };
        }

        public async Task<BrandSalesPVM> PrintBrandSales(ListFilterSalesModel filter)
        {
            var items = await GetBrandSales(filter);
            var agent = await context.Agents.Where(s => s.Id == filter.Agent).FirstOrDefaultAsync();
            var brand = await context.Brands.Where(s => s.Id == filter.Brand).FirstOrDefaultAsync();
            return new BrandSalesPVM
            {
                Contents = items,
                DateFromD = filter.DateFrom.GetValueOrDefault(),
                DateToD = filter.DateTo.GetValueOrDefault(),
                Agent = filter.Agent == null || agent == null ? "All" : agent.FirstName + (agent.MiddleName == null ? "" : " " + agent.MiddleName) + " " + agent.LastName,
                Brand = filter.Brand == null || brand == null ? "All" : brand.Name,
                TotalAmountD = items.Sum(s => s.AmountD)
            };
        }

        #endregion Brand Sales Report

        #region Best Selling Item (Quantity / Amount)

        public async Task<IList<BestSellingItemRVM>> GetBestSellingItem(ListFilterSalesModel filter)
        {
            var invoices = from ili in context.InvoiceLineItems
                           join soli in context.SalesOrderLineItems on new { ili.Invoice.SalesOrderId, ili.ItemId }
                                                                equals new { soli.SalesOrderId, soli.ItemId }
                           let ii = context.ItemInventories
                                           .Where(s => s.ItemId == ili.Item.Id
                                                    && s.Warehouse.IsMain == true)
                                           .Sum(s => (int?)s.Quantity) ?? 0
                           let discount1 = (100 - soli.Discount1) / 100
                           let discount2 = (100 - soli.Discount2) / 100
                           where (filter.DateFrom == null || filter.DateFrom <= ili.Invoice.InvoiceDate.Date)
                           && (filter.DateTo == null || ili.Invoice.InvoiceDate.Date <= filter.DateTo)
                           && (filter.Agent == null || ili.Invoice.SalesOrder.Agent.Id == filter.Agent)
                           && (filter.Brand == null || ili.Item.Brand.Id == filter.Brand)
                           && (ili.Invoice.Status.Code.Trim().ToLower() == "invoiced" || ili.Invoice.Status.Code.Trim().ToLower() == "draft")
                           select new BestSellingItemRVM
                           {
                               AmountD = ili.Quantity * ili.UnitPrice * discount1 * discount2,
                               Brand = ili.Item.Brand.Name,
                               CurrentQuantityD = ii,
                               ItemId = ili.Item.Id,
                               ItemCode = ili.Item.Code,
                               ItemName = ili.Item.Name + (soli.ItemPromo.Name == null ? "" : " (" + soli.ItemPromo.Name + ")"),
                               PartNumber = ili.Item.PartNumber + "",
                               QuantityD = ili.Quantity,
                               Unit = ili.Item.Unit.Name,
                               UnitPriceD = ili.UnitPrice
                           };
            var grpinvoice = from invoice in await invoices.ToListAsync()
                             orderby invoice.Brand ascending, invoice.ItemCode ascending
                             group invoice by new
                             {
                                 invoice.Brand,
                                 invoice.ItemCode,
                                 invoice.ItemName,
                                 invoice.ItemId,
                                 invoice.PartNumber,
                                 invoice.Unit,
                                 invoice.UnitPriceD
                             } into i
                             select new BestSellingItemRVM
                             {
                                 AmountD = filter.Basis == "Amount" ? i.Sum(s => s.AmountD) : 0,
                                 Brand = i.Key.Brand,
                                 CurrentQuantityD = filter.Basis == "Quantity" ? i.Sum(s => s.CurrentQuantityD) : 0,
                                 ItemCode = i.Key.ItemCode,
                                 ItemName = i.Key.ItemName,
                                 ItemId = i.Key.ItemId,
                                 PartNumber = i.Key.PartNumber,
                                 QuantityD = filter.Basis == "Quantity" ? i.Sum(s => s.QuantityD) : 0,
                                 Unit = i.Key.Unit,
                                 UnitPriceD = filter.Basis == "Amount" ? i.Key.UnitPriceD : 0
                             };
            return grpinvoice.ToList();
        }

        public async Task<ForDataTables<BestSellingItemRVM>> GetBestSellingItemDT(DataTablesRequestForm form, ListFilterSalesModel filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            int totalQuantity = 0;
            int totalCurrentQuantity = 0;
            decimal totalPrice = 0;
            decimal totalAmount = 0;

            var data = (await GetBestSellingItem(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace((form.SearchValue + "").Trim()))
            {
                data = data.Where(s => new[]
                {
                    s.Amount,
                    s.AmountD.ToString(),
                    s.Brand,
                    s.CurrentQuantity,
                    s.CurrentQuantityD.ToString(),
                    s.ItemCode,
                    s.ItemName,
                    s.PartNumber,
                    s.Quantity,
                    s.QuantityD.ToString(),
                    s.Unit,
                    s.UnitPrice,
                    s.UnitPriceD.ToString()
                }.Any(a => a != null && a.Trim().ToLower().Contains((form.SearchValue + "").Trim().ToLower())));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                bool isAsc = form.SortColumnDirection == "asc";
                data = form.SortColumn switch
                {
                    "Amount" => isAsc ? data.OrderBy(s => s.AmountD) : data.OrderByDescending(s => s.AmountD),
                    "CurrentQuantity" => isAsc ? data.OrderBy(s => s.CurrentQuantityD) : data.OrderByDescending(s => s.CurrentQuantityD),
                    "Quantity" => isAsc ? data.OrderBy(s => s.QuantityD) : data.OrderByDescending(s => s.QuantityD),
                    "UnitPrice" => isAsc ? data.OrderBy(s => s.UnitPriceD) : data.OrderByDescending(s => s.UnitPriceD),
                    _ => data.OrderBy(form.SortColumn + " " + form.SortColumnDirection)
                };
            }
            //get total quantity of date in table
            totalQuantity = data.Sum(s => s.QuantityD);
            //get total current quantity of data in table
            totalCurrentQuantity = data.Sum(s => s.CurrentQuantityD);
            //get total price of data in table
            totalPrice = data.Sum(s => s.UnitPriceD);
            //get total amount of data in table
            totalAmount = data.Sum(s => s.AmountD);
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<BestSellingItemRVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList,
                TotalQuantity = totalQuantity,
                TotalQuantity2 = totalCurrentQuantity,
                TotalAmount = totalPrice,
                TotalAmount2 = totalAmount
            };
        }

        public async Task<BestSellingItemPVM> PrintBestSellingItem(ListFilterSalesModel filter)
        {
            var items = await GetBestSellingItem(filter);
            var brand = await context.Brands.Where(s => s.Id == filter.Brand).FirstOrDefaultAsync();
            return new BestSellingItemPVM
            {
                Contents = items,
                DateFromD = filter.DateFrom.GetValueOrDefault(),
                DateToD = filter.DateTo.GetValueOrDefault(),
                Basis = filter.Basis + "",
                Brand = filter.Brand == null || brand == null ? "All" : brand.Name,
                TotalQuantityD = items.Sum(s => s.QuantityD),
                TotalCurrentQuantityD = items.Sum(s => s.CurrentQuantityD),
                TotalUnitPriceD = items.Sum(s => s.UnitPriceD),
                TotalAmountD = items.Sum(s => s.AmountD)
            };
        }

        #endregion Best Selling Item (Quantity / Amount)

        #region Commission

        public async Task<IList<CommissionRVM>> GetCommission(ListFilterSalesModel filter)
        {
            var invoices = from ili in context.InvoiceLineItems
                           join soli in context.SalesOrderLineItems on new { ili.Invoice.SalesOrderId, ili.ItemId }
                                                                equals new { soli.SalesOrderId, soli.ItemId }
                           let discount1 = (100 - soli.Discount1) / 100
                           let discount2 = (100 - soli.Discount2) / 100
                           where (filter.DateFrom == null || filter.DateFrom <= ili.Invoice.InvoiceDate.Date)
                           && (filter.DateTo == null || ili.Invoice.InvoiceDate.Date <= filter.DateTo)
                           && (filter.Agent == null || ili.Invoice.SalesOrder.Agent.Id == filter.Agent)
                           && (filter.Customer == null || ili.Invoice.SalesOrder.Customer.Id == filter.Customer)
                           && (filter.Area == null || ili.Invoice.SalesOrder.Customer.Area.Id == filter.Area)
                           && (ili.Invoice.Status.Code.Trim().ToLower() == "invoiced" || ili.Invoice.Status.Code.Trim().ToLower() == "draft")
                           select new CommissionRVM
                           {
                               AgentId = ili.Invoice.SalesOrder.Agent.Id,
                               AgentCode = ili.Invoice.SalesOrder.Agent.Code,
                               Agent = ili.Invoice.SalesOrder.Agent.FirstName + " " + ili.Invoice.SalesOrder.Agent.LastName,
                               Area = ili.Invoice.SalesOrder.Agent.Area.Name,
                               InvoiceDateD = ili.Invoice.InvoiceDate,
                               InvoiceNumber = ili.Invoice.InvoiceNumber,
                               InvoiceId = ili.Invoice.Id,
                               InvoiceAmountD = ili.Quantity * ili.UnitPrice * discount1 * discount2,
                               CommissionRateD = ili.Invoice.SalesOrder.Customer.CommissionRate != null
                                               ? ili.Invoice.SalesOrder.Customer.CommissionRate!.Value
                                               : ili.Invoice.SalesOrder.Agent.Area.CommissionRate,
                               CommissionAmount2D = (ili.Quantity * ili.UnitPrice * discount1 * discount2)
                                                  * (ili.Invoice.SalesOrder.Customer.CommissionRate != null
                                                  ? ili.Invoice.SalesOrder.Customer.CommissionRate!.Value
                                                  : ili.Invoice.SalesOrder.Agent.Area.CommissionRate) / 100,
                               Customer = ili.Invoice.SalesOrder.Customer.Name,
                               CustomerId = ili.Invoice.SalesOrder.Customer.Id
                           };
            var invoicelist = await invoices.ToListAsync();
            var grpinvoice = from invoice in invoicelist
                             orderby invoice.Agent ascending, invoice.InvoiceDateD ascending
                             group invoice by new
                             {
                                 invoice.AgentId,
                                 invoice.AgentCode,
                                 invoice.Agent,
                                 invoice.CommissionRateD,
                                 invoice.Area,
                                 invoice.Customer,
                                 invoice.CustomerId,
                                 invoice.InvoiceDateD,
                                 invoice.InvoiceNumber,
                                 invoice.InvoiceId
                             } into i
                             let total = invoicelist.Where(s => s.AgentCode == i.Key.AgentCode)
                             select new CommissionRVM
                             {
                                 AgentId = i.Key.AgentId,
                                 AgentCode = i.Key.AgentCode,
                                 CommissionRateD = i.Key.CommissionRateD,
                                 Agent = i.Key.Agent,
                                 Area = i.Key.Area,
                                 Customer = i.Key.Customer,
                                 CustomerId = i.Key.CustomerId,
                                 InvoiceDateD = i.Key.InvoiceDateD,
                                 InvoiceNumber = i.Key.InvoiceNumber,
                                 InvoiceId = i.Key.InvoiceId,
                                 InvoiceAmountD = i.Sum(s => s.InvoiceAmountD),
                                 CommissionAmount2D = i.Sum(s => s.CommissionAmount2D),
                                 AgentTotalInvoiceD = total.Sum(s => s.InvoiceAmountD),
                                 AgentCommissionRateD = total.Sum(s => s.CommissionAmount2D)
                             };
            return grpinvoice.ToList();
        }

        public async Task<ForDataTables<CommissionRVM>> GetCommissionDT(DataTablesRequestForm form, ListFilterSalesModel filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            decimal totalInvoice = 0;
            decimal totalCommission = 0;

            var data = (await GetCommission(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace((form.SearchValue + "").Trim()))
            {
                data = data.Where(s => new[]
                {
                    s.Agent,
                    s.Customer,
                    s.InvoiceDate,
                    s.InvoiceDateD.ToString(),
                    s.InvoiceNumber,
                    s.CommissionRate,
                    s.CommissionRateD.ToString(),
                    s.CommissionAmount2,
                    s.CommissionAmount2D.ToString(),
                    s.InvoiceAmount,
                    s.InvoiceAmountD.ToString()
                }.Any(a => a != null && a.Trim().ToLower().Contains((form.SearchValue + "").Trim().ToLower())));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                bool isAsc = form.SortColumnDirection == "asc";
                data = form.SortColumn switch
                {
                    "InvoiceDate" => isAsc ? data.OrderBy(s => s.InvoiceDateD) : data.OrderByDescending(s => s.InvoiceDateD),
                    "CommissionRate" => isAsc ? data.OrderBy(s => s.CommissionRateD) : data.OrderByDescending(s => s.CommissionRateD),
                    "CommissionAmount" => isAsc ? data.OrderBy(s => s.CommissionAmount2D) : data.OrderByDescending(s => s.CommissionAmount2D),
                    "InvoiceAmount" => isAsc ? data.OrderBy(s => s.InvoiceAmountD) : data.OrderByDescending(s => s.InvoiceAmountD),
                    _ => data.OrderBy(form.SortColumn + " " + form.SortColumnDirection)
                };
            }
            //get total amount of data in table
            totalInvoice = data.Sum(s => s.InvoiceAmountD);
            totalCommission = data.Sum(s => s.CommissionAmount2D);
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<CommissionRVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList,
                TotalAmount = totalInvoice,
                TotalAmount2 = totalCommission
            };
        }

        public async Task<CommissionPVM> PrintCommission(ListFilterSalesModel filter)
        {
            var items = await GetCommission(filter);
            var agent = await context.Agents.Where(s => s.Id == filter.Agent).FirstOrDefaultAsync();
            var area = await context.Areas.Where(s => s.Id == filter.Area).FirstOrDefaultAsync();
            var customer = await context.Customers.Where(s => s.Id == filter.Customer).FirstOrDefaultAsync();
            return new CommissionPVM
            {
                Contents = items,
                DateFromD = filter.DateFrom.GetValueOrDefault(),
                DateToD = filter.DateTo.GetValueOrDefault(),
                Agent = filter.Agent == null || agent == null
                      ? "All"
                      : (agent.FirstName + (agent.MiddleName == null ? "" : " " + agent.MiddleName) + " " + agent.LastName),
                Area = filter.Area == null || area == null ? "All" : area.Name,
                Customer = filter.Customer == null || customer == null ? "All" : customer.Name,
                AgentTotalInvoiceD = items.Sum(s => s.InvoiceAmountD),
                AgentCommissionRateD = items.Sum(s => s.CommissionAmount2D)
            };
        }


        #endregion Commission

        #region Customer Sales Ranking

        public async Task<IList<CustomerSalesRankingRVM>> GetCustomerSalesRanking(ListFilterSalesModel filter)
        {
            var invoices = from ili in context.InvoiceLineItems
                           join soli in context.SalesOrderLineItems on new { ili.Invoice.SalesOrderId, ili.ItemId }
                                                                equals new { soli.SalesOrderId, soli.ItemId }
                           let discount1 = (100 - soli.Discount1) / 100
                           let discount2 = (100 - soli.Discount2) / 100
                           where (filter.DateFrom == null || filter.DateFrom <= ili.Invoice.InvoiceDate.Date)
                           && (filter.DateTo == null || ili.Invoice.InvoiceDate.Date <= filter.DateTo)
                           && (filter.Region == null || (ili.Invoice.SalesOrder.Customer.Area != null && ili.Invoice.SalesOrder.Region.Id == filter.Region))
                           && (ili.Invoice.Status.Code.Trim().ToLower() == "invoiced" || ili.Invoice.Status.Code.Trim().ToLower() == "draft")
                           select new CustomerSalesRankingRVM
                           {
                               AmountD = ili.Quantity * ili.UnitPrice * discount1 * discount2,
                               CustomerCode = ili.Invoice.SalesOrder.Customer.Code,
                               CustomerName = ili.Invoice.SalesOrder.Customer.Name,
                               CustomerId = ili.Invoice.SalesOrder.Customer.Id,
                               RankD = 0
                           };
            var grpinvoice = (await invoices.ToListAsync())
                            .GroupBy(s => new
                            {
                                s.CustomerCode,
                                s.CustomerName,
                                s.CustomerId
                            })
                            .OrderByDescending(s => s.Sum(a => a.AmountD))
                            .Select((s, i) => new CustomerSalesRankingRVM
                            {
                                AmountD = s.Sum(s => s.AmountD),
                                CustomerCode = s.Key.CustomerCode,
                                CustomerName = s.Key.CustomerName,
                                CustomerId = s.Key.CustomerId,
                                RankD = i + 1
                            });
            return grpinvoice.ToList();
        }

        public async Task<ForDataTables<CustomerSalesRankingRVM>> GetCustomerSalesRankingDT(DataTablesRequestForm form, ListFilterSalesModel filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            decimal totalAmount = 0;

            var data = (await GetCustomerSalesRanking(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace((form.SearchValue + "").Trim()))
            {
                data = data.Where(s => new[]
                {
                    s.Amount,
                    s.AmountD.ToString(),
                    s.CustomerCode,
                    s.CustomerName,
                    s.Rank,
                    s.RankD.ToString()
                }.Any(a => a != null && a.Trim().ToLower().Contains((form.SearchValue + "").Trim().ToLower())));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                bool isAsc = form.SortColumnDirection == "asc";
                data = form.SortColumn switch
                {
                    "Amount" => isAsc ? data.OrderBy(s => s.AmountD) : data.OrderByDescending(s => s.AmountD),
                    "Rank" => isAsc ? data.OrderBy(s => s.RankD) : data.OrderByDescending(s => s.RankD),
                    _ => data.OrderBy(form.SortColumn + " " + form.SortColumnDirection)
                };
            }
            //get total amount of data in table
            totalAmount = data.Sum(s => s.AmountD);
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<CustomerSalesRankingRVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList,
                TotalAmount = totalAmount
            };
        }

        public async Task<CustomerSalesRankingPVM> PrintCustomerSalesRanking(ListFilterSalesModel filter)
        {
            var items = await GetCustomerSalesRanking(filter);
            var region = await context.Regions.Where(s => s.Id == filter.Region).FirstOrDefaultAsync();
            return new CustomerSalesRankingPVM
            {
                Contents = items,
                DateFromD = filter.DateFrom.GetValueOrDefault(),
                DateToD = filter.DateTo.GetValueOrDefault(),
                Region = filter.Region == null || region == null ? "All" : region.Name,
                TotalAmountD = items.Sum(s => s.AmountD)
            };
        }

        #endregion Customer Sales Ranking

        #region Customer Sales

        public async Task<IList<CustomerSalesRVM>> GetCustomerSales(ListFilterSalesModel filter)
        {
            var invoices = from ili in context.InvoiceLineItems
                           join soli in context.SalesOrderLineItems on new { ili.Invoice.SalesOrderId, ili.ItemId }
                                                                equals new { soli.SalesOrderId, soli.ItemId }
                           let discount1 = (100 - soli.Discount1) / 100
                           let discount2 = (100 - soli.Discount2) / 100
                           where (filter.DateFrom == null || filter.DateFrom <= ili.Invoice.InvoiceDate.Date)
                           && (filter.DateTo == null || ili.Invoice.InvoiceDate.Date <= filter.DateTo)
                           && (filter.Brand == null || ili.Item.Brand.Id == filter.Brand)
                           && (filter.Customer == null || ili.Invoice.SalesOrder.Customer.Id == filter.Customer)
                           && (ili.Invoice.Status.Code.Trim().ToLower() == "invoiced" || ili.Invoice.Status.Code.Trim().ToLower() == "draft")
                           select new CustomerSalesRVM
                           {
                               AmountD = ili.Quantity * ili.UnitPrice * discount1 * discount2,
                               Brand = ili.Item.Brand.Name,
                               CustomerCode = ili.Invoice.SalesOrder.Customer.Code,
                               CustomerName = ili.Invoice.SalesOrder.Customer.Name,
                               CustomerId = ili.Invoice.SalesOrder.Customer.Id
                           };
            var grpinvoice = from invoice in await invoices.ToListAsync()
                             orderby invoice.CustomerCode ascending, invoice.Brand ascending
                             group invoice by new
                             {
                                 invoice.CustomerCode,
                                 invoice.CustomerName,
                                 invoice.CustomerId,
                                 invoice.Brand
                             } into i
                             select new CustomerSalesRVM
                             {
                                 AmountD = i.Sum(s => s.AmountD),
                                 Brand = i.Key.Brand,
                                 CustomerCode = i.Key.CustomerCode,
                                 CustomerName = i.Key.CustomerName,
                                 CustomerId = i.Key.CustomerId
                             };
            return grpinvoice.ToList();
        }

        public async Task<ForDataTables<CustomerSalesRVM>> GetCustomerSalesDT(DataTablesRequestForm form, ListFilterSalesModel filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            decimal totalAmount = 0;

            var data = (await GetCustomerSales(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace((form.SearchValue + "").Trim()))
            {
                data = data.Where(s => new[]
                {
                    s.Amount,
                    s.AmountD.ToString(),
                    s.Brand,
                    s.CustomerCode,
                    s.CustomerName
                }.Any(a => a != null && a.Trim().ToLower().Contains((form.SearchValue + "").Trim().ToLower())));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                bool isAsc = form.SortColumnDirection == "asc";
                data = form.SortColumn switch
                {
                    "Amount" => isAsc ? data.OrderBy(s => s.AmountD) : data.OrderByDescending(s => s.AmountD),
                    _ => data.OrderBy(form.SortColumn + " " + form.SortColumnDirection)
                };
            }
            //get total amount of data in table
            totalAmount = data.Sum(s => s.AmountD);
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<CustomerSalesRVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList,
                TotalAmount = totalAmount
            };
        }

        public async Task<CustomerSalesPVM> PrintCustomerSales(ListFilterSalesModel filter)
        {
            var items = await GetCustomerSales(filter);
            var brand = await context.Brands.Where(s => s.Id == filter.Brand).FirstOrDefaultAsync();
            var customer = await context.Customers.Where(s => s.Id == filter.Customer).FirstOrDefaultAsync();
            return new CustomerSalesPVM
            {
                Contents = items,
                DateFromD = filter.DateFrom.GetValueOrDefault(),
                DateToD = filter.DateTo.GetValueOrDefault(),
                Customer = filter.Customer == null || customer == null ? "All" : customer.Name,
                Brand = filter.Brand == null || brand == null ? "All" : brand.Name,
                TotalAmountD = items.Sum(s => s.AmountD)
            };
        }

        #endregion Customer Sales

        #region Daily Sales

        public async Task<IList<DailySalesRVM>> GetDailySales(ListFilterSalesModel filter)
        {

            var invoices = from invoice in context.Invoices
                           where (filter.DateFrom == null || filter.DateFrom <= invoice.InvoiceDate.Date)
                           && (filter.DateTo == null || invoice.InvoiceDate.Date <= filter.DateTo)
                           && (filter.InvoiceNumberFrom == null || invoice.ControlNumber.Trim().ToLower().CompareTo(filter.InvoiceNumberFrom.Trim().ToLower()) >= 0)
                           && (filter.InvoiceNumberTo == null || invoice.ControlNumber.Trim().ToLower().CompareTo(filter.InvoiceNumberTo.Trim().ToLower()) <= 0)
                           && (filter.StatusCode == null || invoice.Status.Code.Trim().ToLower() == filter.StatusCode.Trim().ToLower())
                           select new DailySalesRVM
                           {
                               Agent = invoice.SalesOrder.Agent.FirstName + " " + invoice.SalesOrder.Agent.LastName,
                               AgentId = invoice.SalesOrder.Agent.Id,
                               AmountD = (from ili in invoice.InvoiceLineItems
                                          join soli in invoice.SalesOrder.SalesOrderLineItems on ili.ItemId equals soli.ItemId
                                          let discount1 = (100 - soli.Discount1) / 100
                                          let discount2 = (100 - soli.Discount2) / 100
                                          select (decimal?)(ili.Quantity * ili.UnitPrice * discount1 * discount2)).Sum() ?? 0,
                               CustomerCode = invoice.SalesOrder.Customer.Code,
                               CustomerName = invoice.SalesOrder.Customer.Name,
                               CustomerId = invoice.SalesOrder.Customer.Id,
                               InvoiceDateD = invoice.InvoiceDate,
                               InvoiceNumber = invoice.InvoiceNumber,
                               BIRInvoiceNumber = invoice.ControlNumber,
                               InvoiceId = invoice.Id,
                               Term = invoice.SalesOrder.Customer.CreditTerm == null ? "" : invoice.SalesOrder.Customer.CreditTerm.Name,
                               PercentD = invoice.SalesOrder.Customer.CommissionRate != null
                                        ? invoice.SalesOrder.Customer.CommissionRate.Value
                                        : invoice.SalesOrder.Agent.Area.CommissionRate
                           };
            return await invoices.ToListAsync();
        }

        public async Task<ForDataTables<DailySalesRVM>> GetDailySalesDT(DataTablesRequestForm form, ListFilterSalesModel filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            decimal totalAmount = 0;

            var data = (await GetDailySales(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace((form.SearchValue + "").Trim()))
            {
                data = data.Where(s => new[]
                {
                    s.Agent,
                    s.Amount,
                    s.AmountD.ToString(),
                    s.BIRInvoiceNumber,
                    s.CustomerCode,
                    s.CustomerName,
                    s.InvoiceDate,
                    s.InvoiceNumber,
                    s.Term
                }.Any(a => a != null && a.Trim().ToLower().Contains((form.SearchValue + "").Trim().ToLower())));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                bool isAsc = form.SortColumnDirection == "asc";
                data = form.SortColumn switch
                {
                    "Amount" => isAsc ? data.OrderBy(s => s.AmountD) : data.OrderByDescending(s => s.AmountD),
                    "InvoiceDate" => isAsc ? data.OrderBy(s => s.InvoiceDateD) : data.OrderByDescending(s => s.InvoiceDateD),
                    _ => data.OrderBy(form.SortColumn + " " + form.SortColumnDirection)
                };
            }
            //get total amount of data in table
            totalAmount = data.Sum(s => s.AmountD);
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<DailySalesRVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList,
                TotalAmount = totalAmount
            };
        }

        public async Task<IList<DailySalesRVM>> FilteredDailySales(ListFilterSalesModel filter)
        {
            var data = (await GetDailySales(filter)).AsQueryable();
            var brand = await context.Brands.Where(s => s.Id == filter.Brand).FirstOrDefaultAsync();
            if (!string.IsNullOrWhiteSpace(filter.SearchValue))
            {
                data = data.Where(s => new[]
                {
                    s.Agent,
                    s.Amount,
                    s.AmountD.ToString(),
                    s.BIRInvoiceNumber,
                    s.CustomerCode,
                    s.CustomerName,
                    s.InvoiceDate,
                    s.InvoiceNumber,
                    s.Term
                }.Any(a => a != null && a.Trim().ToLower().Contains(filter.SearchValue.Trim().ToLower())));
            }
            if (!string.IsNullOrEmpty(filter.SortColumn) && !string.IsNullOrEmpty(filter.SortColumnDirection))
            {
                bool isAsc = filter.SortColumnDirection == "asc";
                data = filter.SortColumn switch
                {
                    "Amount" => isAsc ? data.OrderBy(s => s.AmountD) : data.OrderByDescending(s => s.AmountD),
                    "InvoiceDate" => isAsc ? data.OrderBy(s => s.InvoiceDateD) : data.OrderByDescending(s => s.InvoiceDateD),
                    _ => data.OrderBy(filter.SortColumn + " " + filter.SortColumnDirection)
                };
            }
            return data.ToList();
        }

        public async Task<DailySalesPVM> PrintDailySales(ListFilterSalesModel filter)
        {
            var dataList = await FilteredDailySales(filter);
            return new DailySalesPVM
            {
                Contents = dataList,
                DateFromD = filter.DateFrom.GetValueOrDefault(),
                DateToD = filter.DateTo.GetValueOrDefault(),
                InvoiceNumberFrom = filter.InvoiceNumberFrom == null ? "All" : filter.InvoiceNumberFrom,
                InvoiceNumberTo = filter.InvoiceNumberTo == null ? "All" : filter.InvoiceNumberTo,
                Status = filter.StatusCode == null ? "All" : filter.StatusCode,
                TotalAmountD = dataList.Sum(s => s.AmountD)
            };
        }

        #endregion Daily Sales

        #region Sales Invoice

        public async Task<IList<SalesInvoiceRVM>> GetSalesInvoice(ListFilterSalesModel filter, bool IsPaged = false)
        {
            var regions = filter.Region2 == "ML" ? new[] { "MM", "LUZ" } 
                        : filter.Region2 == "VM" ? new[] { "VIS", "MIN" }
                        : null;

            //var baseQuery = from invoice in context.Invoices
            //                where (filter.DateFrom == null || filter.DateFrom <= invoice.InvoiceDate.Date)
            //                && (filter.DateTo == null || invoice.InvoiceDate.Date <= filter.DateTo)
            //                && (filter.Agent == null || invoice.SalesOrder.Agent.Id == filter.Agent)
            //                && (filter.Customer == null || invoice.SalesOrder.Customer.Id == filter.Customer)
            //                && (filter.StatusCode == null || invoice.Status.Code.Trim().ToLower() == filter.StatusCode.Trim().ToLower())
            //                && (filter.Region2 == null || regions == null || regions.Contains(invoice.SalesOrder.Customer.Area.Region.Code))
            //                select invoice;

            //if (IsPaged)
            //{
            //    var invoices = from invoice in baseQuery
            //                   select new SalesInvoiceRVM
            //                   {
            //                       Id = invoice.Id,
            //                       Agent = invoice.SalesOrder.Agent.FirstName + " " + invoice.SalesOrder.Agent.LastName,
            //                       AgentCode = invoice.SalesOrder.Agent.Code,
            //                       ControlNumber = invoice.ControlNumber,
            //                       CustomerCode = invoice.SalesOrder.Customer.Code,
            //                       CustomerName = invoice.SalesOrder.Customer.Name,
            //                       InvoiceDateD = invoice.InvoiceDate.Date,
            //                       InvoiceNumber = invoice.InvoiceNumber,
            //                       Status = invoice.Status.Code
            //                   };
            //    return await invoices.ToListAsync();
            //}
            //else
            //{

                var invoiceAmounts = from ili in context.InvoiceLineItems
                                     join soli in context.SalesOrderLineItems on new { ili.Invoice.SalesOrderId, ili.ItemId } equals new { soli.SalesOrderId, soli.ItemId }
                                     group new { ili, soli } by ili.InvoiceId into g
                                     select new
                                     {
                                         InvoiceId = g.Key,
                                         TotalAmount = g.Sum(s => (decimal?)s.ili.Quantity *
                                                                  s.ili.UnitPrice *
                                                                  (100 - s.soli.Discount1) / 100m *
                                                                  (100 - s.soli.Discount2) / 100m *
                                                                  (100 - (s.soli.Discount3 == null ? 0 : s.soli.Discount3)) / 100m *
                                                                  (100 - (s.soli.Discount4 == null ? 0 : s.soli.Discount4)) / 100m *
                                                                  (100 - (s.soli.Discount5 == null ? 0 : s.soli.Discount5)) / 100m) ?? 0
                                     };
                var invoices = from invoice in context.Invoices
                               where (filter.DateFrom == null || filter.DateFrom <= invoice.InvoiceDate.Date)
                               && (filter.DateTo == null || invoice.InvoiceDate.Date <= filter.DateTo)
                               && (filter.Agent == null || invoice.SalesOrder.Agent.Id == filter.Agent)
                               && (filter.Customer == null || invoice.SalesOrder.Customer.Id == filter.Customer)
                               && (filter.StatusCode == null || invoice.Status.Code.Trim().ToLower() == filter.StatusCode.Trim().ToLower())
                               && (filter.Region2 == null || regions == null || regions.Contains(invoice.SalesOrder.Region.Code))
                                let amount = invoiceAmounts.Where(s => s.InvoiceId == invoice.Id)
                                                           .Sum(s => (decimal?)s.TotalAmount) ?? 0
                               select new SalesInvoiceRVM
                               {
                                   Id = invoice.Id,
                                   Agent = invoice.SalesOrder.Agent.FirstName + " " + invoice.SalesOrder.Agent.LastName,
                                   AgentCode = invoice.SalesOrder.Agent.Code,
                                   AgentId = invoice.SalesOrder.Agent.Id,
                                   AmountD = amount,
                                   ControlNumber = invoice.ControlNumber,
                                   CustomerCode = invoice.SalesOrder.Customer.Code,
                                   CustomerName = invoice.SalesOrder.Customer.Name,
                                   CustomerId = invoice.SalesOrder.Customer.Id,
                                   InvoiceDateD = invoice.InvoiceDate.Date,
                                   InvoiceNumber = invoice.InvoiceNumber,
                                   Status = invoice.Status.Code
                               };
                return await invoices.ToListAsync();
            //}
        }

        public async Task<ForDataTables<SalesInvoiceRVM>> GetSalesInvoiceDT(DataTablesRequestForm form, ListFilterSalesModel filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            decimal totalAmount = 0;

            var data = (await GetSalesInvoice(filter, true)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace((form.SearchValue + "").Trim()))
            {
                data = data.Where(s => new[]
                {
                    s.Agent,
                    s.ControlNumber,
                    s.CustomerCode,
                    s.CustomerName,
                    s.InvoiceDate,
                    s.InvoiceNumber,
                    s.Status
                }.Any(a => a != null && a.Trim().ToLower().Contains((form.SearchValue + "").Trim().ToLower())));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                bool isAsc = form.SortColumnDirection == "asc";
                data = form.SortColumn switch
                {
                    "InvoiceDate" => isAsc ? data.OrderBy(s => s.InvoiceDateD) : data.OrderByDescending(s => s.InvoiceDateD),
                    _ => data.OrderBy(form.SortColumn + " " + form.SortColumnDirection)
                };
            }
            //get total amount of data in table
            totalAmount = data.Sum(s => s.AmountD);
            /* DAMI PROBLEMA NETO KAYA BALIK NALANG SA LOAD LAHAT
             * 
                totalAmount = (from ili in context.InvoiceLineItems.ToList()
                               join soli in context.SalesOrderLineItems.ToList() on new { ili.Invoice.SalesOrderId, ili.ItemId } equals new { soli.SalesOrderId, soli.ItemId }
                               where chunk.Contains(ili.InvoiceId)
                               select (decimal?)ili.Quantity * ili.UnitPrice
                                    * (100 - soli.Discount1) / 100
                                    * (100 - soli.Discount2) / 100
                                    * (100 - (soli.Discount3 == null ? 0 : soli.Discount3)) / 100
                                    * (100 - (soli.Discount4 == null ? 0 : soli.Discount4)) / 100
                                    * (100 - (soli.Discount5 == null ? 0 : soli.Discount5)) / 100).Sum() ?? 0;*/
            //pagination
            var pagedList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            // loads data for selected page
            //var InvoicesOnPagedList = pagedList.Select(s => s.Id).ToList();

            //var invoiceAmounts = from ili in context.InvoiceLineItems
            //                     join soli in context.SalesOrderLineItems on new { ili.Invoice.SalesOrderId, ili.ItemId } equals new { soli.SalesOrderId, soli.ItemId }
            //                     where InvoicesOnPagedList.Contains(ili.InvoiceId)
            //                     group new { ili, soli } by ili.InvoiceId into g
            //                     select new
            //                     {
            //                         InvoiceId = g.Key,
            //                         TotalAmount = g.Sum(s => (decimal?)s.ili.Quantity *
            //                                                  s.ili.UnitPrice *
            //                                                  (100 - s.soli.Discount1) / 100m *
            //                                                  (100 - s.soli.Discount2) / 100m *
            //                                                  (100 - (s.soli.Discount3 == null ? 0 : s.soli.Discount3)) / 100m *
            //                                                  (100 - (s.soli.Discount4 == null ? 0 : s.soli.Discount4)) / 100m *
            //                                                  (100 - (s.soli.Discount5 == null ? 0 : s.soli.Discount5)) / 100m) ?? 0
            //                     };
            //var invoiceList = invoiceAmounts.ToList();
            //var invoiceDict = invoiceList.ToDictionary(s => s.InvoiceId, s => s.TotalAmount);

            //foreach (var pl in pagedList)
            //{
            //    pl.AmountD = invoiceDict.GetValueOrDefault(pl.Id);
            //}

            return new ForDataTables<SalesInvoiceRVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pagedList,
                TotalAmount = totalAmount
            };
        }

        public async Task<SalesInvoicePVM> PrintSalesInvoice(ListFilterSalesModel filter)
        {
            var items = await GetSalesInvoice(filter);
            var agent = await context.Agents.Where(s => s.Id == filter.Agent).FirstOrDefaultAsync();
            var customer = await context.Customers.Where(s => s.Id == filter.Customer).FirstOrDefaultAsync();
            return new SalesInvoicePVM
            {
                Contents = items,
                DateFromD = filter.DateFrom.GetValueOrDefault(),
                DateToD = filter.DateTo.GetValueOrDefault(),
                Agent = filter.Agent == null || agent == null
                      ? "All"
                      : agent.FirstName + " " + (agent.MiddleName == null ? "" : agent.MiddleName + " ") + agent.LastName,
                Customer = filter.Customer == null || customer == null ? "All" : customer.Name,
                Status = filter.StatusCode == null ? "All" : filter.StatusCode,
                Region = filter.Region2 == "ML" ? "Metro Manila & Luzon" : filter.Region2 == "VM" ? "Visayas & Mindanao" : "All",
                TotalAmountD = items.Sum(s => s.AmountD)
            };
        }

        #endregion Sales Invoice

        #region Sales by Agent / Area

        public async Task<IList<SalesAgentAreaRVM>> GetSalesAgentArea(ListFilterSalesModel filter)
        {
            var invoices = from ili in context.InvoiceLineItems
                           join soli in context.SalesOrderLineItems on new { ili.Invoice.SalesOrderId, ili.ItemId }
                                                                equals new { soli.SalesOrderId, soli.ItemId }
                           let discount1 = (100 - soli.Discount1) / 100
                           let discount2 = (100 - soli.Discount2) / 100
                           where (filter.DateFrom == null || filter.DateFrom <= ili.Invoice.InvoiceDate.Date)
                           && (filter.DateTo == null || ili.Invoice.InvoiceDate.Date <= filter.DateTo)
                           && (filter.Agent == null || ili.Invoice.SalesOrder.Agent.Id == filter.Agent)
                           && (filter.Area == null || ili.Invoice.SalesOrder.Customer.Area.Id == filter.Area)
                           && (ili.Invoice.Status.Code.Trim().ToLower() == "invoiced" || ili.Invoice.Status.Code.Trim().ToLower() == "draft")
                           select new SalesAgentAreaRVM
                           {
                               AgentId = ili.Invoice.SalesOrder.Agent.Id,
                               AgentCode = ili.Invoice.SalesOrder.Agent.Code,
                               AgentName = ili.Invoice.SalesOrder.Agent.FirstName + " " + ili.Invoice.SalesOrder.Agent.LastName,
                               Area = ili.Invoice.SalesOrder.Customer.Area.Name,
                               SalesD = ili.Quantity * ili.UnitPrice * discount1 * discount2
                           };

            var invoicelist = await invoices.ToListAsync();

            var grpinvoice = from invoice in invoicelist
                             orderby invoice.AgentName ascending, invoice.Area ascending
                             group invoice by new
                             {
                                 invoice.AgentId,
                                 invoice.AgentCode,
                                 invoice.AgentName,
                                 invoice.Area
                             } into i
                             let total = invoicelist.Where(s => s.AgentCode == i.Key.AgentCode).Sum(s => s.SalesD)
                             select new SalesAgentAreaRVM
                             {
                                 AgentId = i.Key.AgentId,
                                 AgentCode = i.Key.AgentCode,
                                 AgentName = i.Key.AgentName,
                                 Area = i.Key.Area,
                                 SalesD = i.Sum(s => s.SalesD),
                                 //PercentSalesD = (i.Sum(s => s.SalesD) / total) * 100, // why 0 total
                                 //PercentSalesD = (i.Sum(s=>s.SalesD) / (total < 1 ? 1 : total)) * 100,
                                 PercentSalesD = i.Sum(s => s.SalesD) > 0 && total > 0 ? ((i.Sum(s => s.SalesD) / total) * 100) : 0,
                                 AgentTotalSalesD = total
                             };
            return grpinvoice.ToList();
        }

        public async Task<ForDataTables<SalesAgentAreaRVM>> GetSalesAgentAreaDT(DataTablesRequestForm form, ListFilterSalesModel filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            decimal totalAmount = 0;

            var data = (await GetSalesAgentArea(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace((form.SearchValue + "").Trim()))
            {
                data = data.Where(s => new[]
                {
                    s.AgentCode,
                    s.AgentName,
                    s.Area,
                    s.Sales,
                    s.SalesD.ToString(),
                    s.PercentSales,
                    s.PercentSalesD.ToString()
                }.Any(a => a != null && a.Trim().ToLower().Contains((form.SearchValue + "").Trim().ToLower())));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                bool isAsc = form.SortColumnDirection == "asc";
                data = form.SortColumn switch
                {
                    "Sales" => isAsc ? data.OrderBy(s => s.SalesD) : data.OrderByDescending(s => s.SalesD),
                    "PercentSales" => isAsc ? data.OrderBy(s => s.PercentSalesD) : data.OrderByDescending(s => s.PercentSalesD),
                    _ => data.OrderBy(form.SortColumn + " " + form.SortColumnDirection)
                };
            }
            //get total amount of data in table
            totalAmount = data.Sum(s => s.SalesD);
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<SalesAgentAreaRVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList,
                TotalAmount = totalAmount
            };
        }

        public async Task<SalesAgentAreaPVM> PrintSalesAgentArea(ListFilterSalesModel filter)
        {
            var items = await GetSalesAgentArea(filter);
            var agent = await context.Agents.Where(s => s.Id == filter.Agent).FirstOrDefaultAsync();
            var area = await context.Areas.Where(s => s.Id == filter.Area).FirstOrDefaultAsync();
            return new SalesAgentAreaPVM
            {
                Contents = items,
                DateFromD = filter.DateFrom.GetValueOrDefault(),
                DateToD = filter.DateTo.GetValueOrDefault(),
                Agent = filter.Agent == null || agent == null
                      ? "All"
                      : (agent.FirstName + (agent.MiddleName == null ? "" : " " + agent.MiddleName) + " " + agent.LastName),
                Area = filter.Area == null || area == null ? "All" : area.Name,
                TotalSalesD = items.Sum(s => s.SalesD)
            };
        }

        #endregion Sales by Agent / Area

        #region Sales Report Basis

        public async Task<IList<SalesReportBasisRVM>> GetSalesReportBasis(ListFilterSalesModel filter)
        {
            var invoices = from ili in context.InvoiceLineItems
                           join soli in context.SalesOrderLineItems on new { ili.Invoice.SalesOrderId, ili.ItemId }
                                                                equals new { soli.SalesOrderId, soli.ItemId }
                           let discount1 = (100 - soli.Discount1) / 100
                           let discount2 = (100 - soli.Discount2) / 100
                           where (filter.DateFrom == null || filter.DateFrom <= ili.Invoice.InvoiceDate.Date)
                           && (filter.DateTo == null || ili.Invoice.InvoiceDate.Date <= filter.DateTo)
                           && (ili.Invoice.Status.Code.Trim().ToLower() == "invoiced" || ili.Invoice.Status.Code.Trim().ToLower() == "draft")
                           select new SalesReportBasisRVM
                           {
                               Code = filter.Basis == "Customer" ? ili.Invoice.SalesOrder.Customer.Code
                                    : filter.Basis == "Region" ? ili.Invoice.SalesOrder.Region.Code
                                    : /*filter.Basis == "Brand" ?*/ ili.Item.Brand.Code,
                               Name = filter.Basis == "Customer" ? ili.Invoice.SalesOrder.Customer.Name
                                    : filter.Basis == "Region" ? ili.Invoice.SalesOrder.Region.Name
                                    : /*filter.Basis == "Brand" ?*/ ili.Item.Brand.Name,
                               Id = filter.Basis == "Customer" ? ili.Invoice.SalesOrder.Customer.Id
                                  : filter.Basis == "Region" ? ili.Invoice.SalesOrder.Region.Id
                                  : ili.Item.Brand.Id,
                               AmountD = ili.Quantity * ili.UnitPrice * discount1 * discount2
                           };
            var grpinvoice = from invoice in await invoices.ToListAsync()
                             orderby invoice.AmountD descending
                             group invoice by new
                             {
                                 invoice.Code,
                                 invoice.Name,
                                 invoice.Id
                             } into i
                             select new SalesReportBasisRVM
                             {
                                 Code = i.Key.Code,
                                 Name = i.Key.Name,
                                 Id = i.Key.Id,
                                 AmountD = i.Sum(s => s.AmountD)
                             };
            return grpinvoice.ToList();
        }

        public async Task<ForDataTables<SalesReportBasisRVM>> GetSalesReportBasisDT(DataTablesRequestForm form, ListFilterSalesModel filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            decimal totalAmount = 0;

            var data = (await GetSalesReportBasis(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace((form.SearchValue + "").Trim()))
            {
                data = data.Where(s => new[]
                {
                    s.Code,
                    s.Name,
                    s.Amount,
                    s.AmountD.ToString(),
                }.Any(a => a != null && a.Trim().ToLower().Contains((form.SearchValue + "").Trim().ToLower())));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                bool isAsc = form.SortColumnDirection == "asc";
                data = form.SortColumn switch
                {
                    "Amount" => isAsc ? data.OrderBy(s => s.AmountD) : data.OrderByDescending(s => s.AmountD),
                    _ => data.OrderBy(form.SortColumn + " " + form.SortColumnDirection)
                };
            }
            //get total amount of data in table
            totalAmount = data.Sum(s => s.AmountD);
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<SalesReportBasisRVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList,
                TotalAmount = totalAmount
            };
        }

        public async Task<SalesReportBasisPVM> PrintSalesReportBasis(ListFilterSalesModel filter)
        {
            var items = await GetSalesReportBasis(filter);
            return new SalesReportBasisPVM
            {
                Contents = items,
                DateFromD = filter.DateFrom.GetValueOrDefault(),
                DateToD = filter.DateTo.GetValueOrDefault(),
                Basis = filter.Basis + "",
                TotalAmountD = items.Sum(s => s.AmountD)
            };
        }

        #endregion Sales Report Basis

        #region Sales Return Listing

        public async Task<IList<SalesReturnRVM>> GetSalesReturn(ListFilterSalesModel filter)
        {
            var sreturns = from sreturn in context.SalesReturnLineItems
                           where (filter.DateFrom == null || filter.DateFrom <= sreturn.SalesReturn.ReturnDate.Date)
                           && (filter.DateTo == null || sreturn.SalesReturn.ReturnDate.Date <= filter.DateTo)
                           && (filter.Customer == null || sreturn.SalesReturn.SalesOrder!.Customer.Id == filter.Customer)
                           && (filter.Item == null || sreturn.Item.Id == filter.Item || (filter.Item == 0 && sreturn.Item.Code.Trim().ToLower().Contains((filter.ItemKeyword + "").Trim().ToLower())))
                           && (sreturn.InvoiceLineItem!.Invoice.Status.Code.Trim().ToLower() == "invoiced" || sreturn.InvoiceLineItem!.Invoice.Status.Code.Trim().ToLower() == "draft")
                           let delivery = sreturn.SalesReturn.SalesOrder.SalesOrderDeliveryMonitorings.FirstOrDefault()
                           select new SalesReturnRVM
                           {
                               Agent = sreturn.InvoiceLineItem == null ? "" : sreturn.InvoiceLineItem.Invoice.SalesOrder.Agent.Code,
                               AgentId = sreturn.InvoiceLineItem == null ? 0 : sreturn.InvoiceLineItem.Invoice.SalesOrder.Agent.Id,
                               Brand = sreturn.Item.Brand.Name,
                               ControlNumber = sreturn.InvoiceLineItem == null ? "TBR" : sreturn.InvoiceLineItem.Invoice.ControlNumber,
                               Customer = sreturn.SalesReturn.SalesOrder!.Customer.Name,
                               CustomerId = sreturn.SalesReturn.SalesOrder!.Customer.Id,
                               DeliveryDateD = delivery == null ? null : delivery.DateDeliver,
                               ItemCode = sreturn.Item.Code,
                               ItemName = sreturn.Item.Name,
                               ItemId = sreturn.Item.Id,
                               ItemType = sreturn.Item.ItemType.Code,
                               OrderDateD = sreturn.InvoiceLineItem == null || sreturn.InvoiceLineItem.Invoice == null ? null : sreturn.InvoiceLineItem.Invoice.SalesOrder.SalesOrderDate,
                               PickupDateD = delivery == null ? null : delivery.DatePickup,
                               PriceReturnedD = sreturn.SellingPrice,
                               QuantityReturnedD = sreturn.Quantity,
                               ReturnReason = sreturn.ReasonOfReturn.Name,
                               ReturnDateD = sreturn.SalesReturn.ReturnDate,
                               SINumber = sreturn.InvoiceLineItem == null || sreturn.InvoiceLineItem.Invoice == null ? null : sreturn.InvoiceLineItem.Invoice.InvoiceNumber,
                               SIId = sreturn.InvoiceLineItem == null || sreturn.InvoiceLineItem.Invoice == null ? 0 : sreturn.InvoiceLineItem.Invoice.Id,
                               SONumber = sreturn.InvoiceLineItem == null || sreturn.InvoiceLineItem.Invoice == null ? null : sreturn.InvoiceLineItem.Invoice.SalesOrder.SalesOrderNumber,
                               SOId = sreturn.InvoiceLineItem == null || sreturn.InvoiceLineItem.Invoice == null ? 0 : sreturn.InvoiceLineItem.Invoice.SalesOrder.Id,
                               SRNumber = sreturn.SalesReturn.SalesReturnNumber,
                               SRId = sreturn.SalesReturn.Id,
                               Unit = sreturn.Item.Unit.Code
                           };
            return await sreturns.ToListAsync();
        }

        public async Task<ForDataTables<SalesReturnRVM>> GetSalesReturnDT(DataTablesRequestForm form, ListFilterSalesModel filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            int totalQuantity = 0;
            decimal totalReturn = 0;
            decimal totalAmount = 0;

            var data = (await GetSalesReturn(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace((form.SearchValue + "").Trim()))
            {
                data = data.Where(s => new[]
                {
                    s.Agent,
                    s.Amount,
                    s.AmountD.ToString(),
                    s.Brand,
                    s.ControlNumber,
                    s.Customer,
                    (s.DeliveryDate == null ? "" : s.DeliveryDate.ToString()),
                    s.ItemCode,
                    s.ItemName,
                    s.ItemType,
                    (s.OrderDate == null ? "" : s.OrderDate.ToString()),
                    (s.PickupDate == null ? "" : s.PickupDate.ToString()),
                    s.PriceReturned,
                    s.PriceReturnedD.ToString(),
                    s.QuantityReturned,
                    s.QuantityReturnedD.ToString(),
                    s.ReturnReason,
                    s.ReturnDate.ToString(),
                    s.SINumber,
                    s.SONumber,
                    s.SRNumber,
                    s.Unit,
                }.Any(a => a != null && a.Trim().ToLower().Contains((form.SearchValue + "").Trim().ToLower())));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                bool isAsc = form.SortColumnDirection == "asc";
                data = form.SortColumn switch
                {
                    "DeliveryDate" => isAsc ? data.OrderBy(s => s.DeliveryDateD) : data.OrderByDescending(s => s.DeliveryDateD),
                    "OrderDate" => isAsc ? data.OrderBy(s => s.OrderDateD) : data.OrderByDescending(s => s.OrderDateD),
                    "PickupDate" => isAsc ? data.OrderBy(s => s.PickupDateD) : data.OrderByDescending(s => s.PickupDateD),
                    "PriceReturned" => isAsc ? data.OrderBy(s => s.PriceReturnedD) : data.OrderByDescending(s => s.PriceReturnedD),
                    "QuantityReturned" => isAsc ? data.OrderBy(s => s.QuantityReturnedD) : data.OrderByDescending(s => s.QuantityReturnedD),
                    "ReturnDate" => isAsc ? data.OrderBy(s => s.ReturnDateD) : data.OrderByDescending(s => s.ReturnDateD),
                    _ => data.OrderBy(form.SortColumn + " " + form.SortColumnDirection)
                };
            }
            //get total quantity return in table
            totalQuantity = data.Sum(s => s.QuantityReturnedD);
            //get total price return in table
            totalReturn = data.Sum(s => s.PriceReturnedD);
            //get total amount of data in table
            totalAmount = data.Sum(s => s.AmountD);
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<SalesReturnRVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList,
                TotalQuantity = totalQuantity,
                TotalAmount = totalReturn,
                TotalAmount2 = totalAmount
            };
        }

        public async Task<SalesReturnPVM> PrintSalesReturn(ListFilterSalesModel filter)
        {
            var items = await GetSalesReturn(filter);
            var customer = await context.Customers.Where(s => s.Id == filter.Customer).FirstOrDefaultAsync();
            var item = await context.Items.Where(s => s.Id == filter.Item).FirstOrDefaultAsync();
            return new SalesReturnPVM
            {
                Contents = items,
                DateFromD = filter.DateFrom.GetValueOrDefault(),
                DateToD = filter.DateTo.GetValueOrDefault(),
                Customer = filter.Customer == null || customer == null ? "All" : customer.Name,
                Item = filter.Item == null || item == null ? "All" : item.Name,
                TotalQuantityReturnedD = items.Sum(s => s.QuantityReturnedD),
                TotalPriceReturnedD = items.Sum(s => s.PriceReturnedD),
                TotalAmountD = items.Sum(s => s.AmountD)
            };
        }

        #endregion Sales Return Listing

        #region Unserved Item

        public async Task<IList<UnservedItemRVM>> GetUnservedItem(ListFilterSalesModel filter)
        {

            var solines = from soline in context.SalesOrderLineItems
                          let packedQuantity = context.PackLineItems
                                                      .Where(s => s.Pack.SalesOrderId == soline.SalesOrderId
                                                               && s.ItemId == soline.ItemId)
                                                      .Sum(s => (int?)s.Quantity) ?? 0

                          //join packed in context.PackLineItems on new { soline.SalesOrderId, soline.ItemId } equals new { packed.Pack.SalesOrderId, packed.ItemId }
                          let returnedQuantity = context.SalesReturnLineItems
                                                        .Where(s => s.SalesReturn.SalesOrderId == soline.SalesOrderId
                                                                 && s.ItemId == soline.ItemId)
                                                        .Sum(s => (int?)s.Quantity) ?? 0
                          //join returned in context.SalesReturnLineItems on new { SalesOrderId = (int?)soline.SalesOrderId, soline.ItemId }
                          //                                          equals new { returned.SalesReturn.SalesOrderId, returned.ItemId } into greturned
                          //from returned in greturned.DefaultIfEmpty()
                          //let inventory = context.ItemInventories
                          //                       .Where(s => s.ItemId == soline.ItemId)
                          //                       .Sum(s => (int?)s.Quantity) ?? 0
                          where (soline.SalesOrder.Status.Name.Trim().ToLower() == "packing draft"
                             || soline.SalesOrder.Status.Name.Trim().ToLower() == "packing submitted"
                             || soline.SalesOrder.Status.Name.Trim().ToLower() == "fully packed")
                          && (filter.DateFrom == null || filter.DateFrom.Value.Date <= soline.SalesOrder.SalesOrderDate.Date)
                          && (filter.DateTo == null || soline.SalesOrder.SalesOrderDate.Date <= filter.DateTo.Value.Date)
                          && (filter.Brand == null || soline.Item.Brand.Id == filter.Brand)
                          && soline.Quantity - (packedQuantity - returnedQuantity) > 0
                          select new UnservedItemRVM
                          {
                              Brand = soline.Item.Brand.Name,
                              CurrentD = packedQuantity - returnedQuantity,
                              ItemCode = soline.Item.Code,
                              OldCode = soline.Item.OldCode + "",
                              ItemName = soline.Item.Name,
                              ItemId = soline.Item.Id,
                              //PendingD = soline.Quantity - (packedQuantity - returnedQuantity),
                              //PendingD = soline.Quantity - inventory > 0 ? soline.Quantity - inventory : 0,
                              //PendingD = Math.Max(0, soline.Quantity - inventory),
                              PendingD = soline.Quantity - (packedQuantity - returnedQuantity),
                              ReplenishDateD = soline.DateCreated
                          };
            var solineslist = await solines.ToListAsync();
            var grpsolines = from soal in solineslist
                             orderby soal.Brand ascending, soal.ItemCode ascending, soal.ItemName ascending
                             group soal by new
                             {
                                 soal.Brand,
                                 soal.ItemCode,
                                 soal.OldCode,
                                 soal.ItemName,
                                 soal.ItemId
                             } into i
                             let current = solineslist.Where(s => s.ItemCode == i.Key.ItemCode).Take(1).Max(s => s.ReplenishDateD)
                             select new UnservedItemRVM
                             {
                                 Brand = i.Key.Brand,
                                 ItemCode = i.Key.ItemCode,
                                 OldCode = i.Key.OldCode,
                                 ItemName = i.Key.ItemName,
                                 ItemId = i.Key.ItemId,
                                 ReplenishDateD = current,
                                 CurrentD = i.Sum(s => s.CurrentD),
                                 PendingD = i.Sum(s => s.PendingD)
                             };
            return grpsolines.ToList();
        }

        public async Task<ForDataTables<UnservedItemRVM>> GetUnservedItemDT(DataTablesRequestForm form, ListFilterSalesModel filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            int totalPending = 0;
            int totalCurrent = 0;

            var data = (await GetUnservedItem(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace((form.SearchValue + "").Trim()))
            {
                data = data.Where(s => new[]
                {
                    s.Brand,
                    s.Current,
                    s.CurrentD.ToString(),
                    s.ItemCode,
                    s.OldCode,
                    s.ItemName,
                    s.Pending,
                    s.PendingD.ToString(),
                    s.ReplenishDate
                }.Any(a => a != null && a.Trim().ToLower().Contains((form.SearchValue + "").Trim().ToLower())));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                bool isAsc = form.SortColumnDirection == "asc";
                data = form.SortColumn switch
                {
                    "Current" => isAsc ? data.OrderBy(s => s.CurrentD) : data.OrderByDescending(s => s.CurrentD),
                    "Pending" => isAsc ? data.OrderBy(s => s.PendingD) : data.OrderByDescending(s => s.PendingD),
                    "ReplenishDate" => isAsc ? data.OrderBy(s => s.ReplenishDateD) : data.OrderByDescending(s => s.ReplenishDateD),
                    _ => data.OrderBy(form.SortColumn + " " + form.SortColumnDirection)
                };
            }
            //get total pending of data in table
            totalPending = data.Sum(s => s.PendingD);
            //get total current of data in table
            totalCurrent = data.Sum(s => s.CurrentD);
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<UnservedItemRVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList,
                TotalQuantity = totalPending,
                TotalQuantity2 = totalCurrent
            };
        }

        public async Task<UnservedItemPVM> PrintUnservedItem(ListFilterSalesModel filter)
        {
            var items = await GetUnservedItem(filter);
            var brand = await context.Brands.Where(s => s.Id == filter.Brand).FirstOrDefaultAsync();
            return new UnservedItemPVM
            {
                Contents = items,
                DateFromD = filter.DateFrom.GetValueOrDefault(),
                DateToD = filter.DateTo.GetValueOrDefault(),
                Brand = filter.Brand == null || brand == null ? "All" : brand.Name,
                TotalCurrentD = items.Sum(s => s.CurrentD),
                TotalPendingD = items.Sum(s => s.PendingD)
            };
        }

        #endregion Unserved Item

        #region Outbound

        public async Task<IList<OutboundRVM>> GetOutbound(ListFilterSalesModel filter)
        {
            var outbound = from ili in context.InvoiceLineItems
                           where (filter.DateFrom == null || filter.DateFrom <= ili.Invoice.InvoiceDate.Date)
                           && (filter.DateTo == null || ili.Invoice.InvoiceDate.Date <= filter.DateTo)
                           && (filter.Area == null || ili.Invoice.SalesOrder.Customer.Area.Id == filter.Area)
                           && (filter.Brand == null || ili.Item.Brand.Id == filter.Brand)
                           && (filter.Region == null || ili.Invoice.SalesOrder.Region.Id == filter.Region)
                           && (ili.Invoice.Status.Code.Trim().ToLower() == "invoiced" || ili.Invoice.Status.Code.Trim().ToLower() == "draft")
                           select new OutboundRVM
                           {
                               Area = ili.Invoice.SalesOrder.Customer.Area.Name,
                               Brand = ili.Item.Brand.Name,
                               ControlNumber = ili.Invoice.ControlNumber,
                               InvoiceId = ili.Invoice.Id,
                               InvoiceDateD = ili.Invoice.InvoiceDate,
                               ItemCode = ili.Item.Code,
                               ItemName = ili.Item.Name,
                               ItemId = ili.Item.Id,
                               OldSkuCode = ili.Item.OldCode,
                               QuantityD = ili.Quantity,
                               Region = ili.Invoice.SalesOrder.Region.Name,
                               SalesOrderNumber = ili.Invoice.SalesOrder.SalesOrderNumber,
                               SalesOrderId = ili.Invoice.SalesOrder.Id,
                               GrossPrinceD = ili.UnitPrice
                           };
            return await outbound.ToListAsync();
        }

        public async Task<ForDataTables<OutboundRVM>> GetOutboundDT(DataTablesRequestForm form, ListFilterSalesModel filter)
        {
            int totalRecord = 0;
            int totalQuantity = 0;
            int filterRecord = 0;
            var data = (await GetOutbound(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace((form.SearchValue + "").Trim()))
            {
                data = data.Where(s => new[]
                {
                    s.InvoiceDate,
                    s.ControlNumber,
                    s.ItemCode,
                    s.ItemName,
                    s.QuantityD.ToString(),
                    s.OldSkuCode,
                    s.SalesOrderNumber,
                    s.Region,
                    s.Area,
                    s.Brand
                }.Any(a => a != null && a.Trim().ToLower().Contains((form.SearchValue + "").Trim().ToLower())));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                bool isAsc = form.SortColumnDirection == "asc";
                data = form.SortColumn switch
                {
                    "InvoiceDate" => isAsc ? data.OrderBy(s => s.InvoiceDateD) : data.OrderByDescending(s => s.InvoiceDateD),
                    "Quantity" => isAsc ? data.OrderBy(s => s.QuantityD) : data.OrderByDescending(s => s.QuantityD),
                    _ => data.OrderBy(form.SortColumn + " " + form.SortColumnDirection)
                };
            }
            totalQuantity = data.Sum(s => s.QuantityD);
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<OutboundRVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList,
                TotalQuantity = totalQuantity
            };
        }
        public async Task<OutboundPVM> PrintOutbound(ListFilterSalesModel filter)
        {
            var items = await GetOutbound(filter);
            var brand = await context.Brands.Where(s => s.Id == filter.Brand).FirstOrDefaultAsync();
            var region = await context.Regions.Where(s => s.Id == filter.Region).FirstOrDefaultAsync();
            var area = await context.Areas.Where(s => s.Id == filter.Area).FirstOrDefaultAsync();
            return new OutboundPVM
            {
                Outbounds = items,
                DateFromD = filter.DateFrom.GetValueOrDefault(),
                DateToD = filter.DateTo.GetValueOrDefault(),
                Brand = filter.Brand == null || brand == null ? "All" : brand.Name,
                Region = filter.Region == null || region == null ? "All" : region.Name,
                Area = filter.Area == null || area == null ? "All" : area.Name,
                TotalQuantityD = items.Sum(s => s.QuantityD)
            };
        }

        #endregion Outbound

    }
}