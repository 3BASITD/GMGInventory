using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;

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
            var invoices = from invoice in context.InvoiceLineItems
                           where filter.DateFrom <= (invoice.Invoice.DateModified ?? invoice.Invoice.DateCreated)
                           && (invoice.Invoice.DateModified ?? invoice.Invoice.DateCreated) <= filter.DateTo
                           && (filter.Agent == null || invoice.Invoice.SalesOrder.Agent.Id == filter.Agent)
                           && (filter.Brand == null || invoice.Item.Brand.Id == filter.Brand)
                           && (filter.Item == null || invoice.Item.Id == filter.Item)
                           && invoice.Invoice.Status.Code == "Invoiced"
                           select new AgentSalesPerItemRVM
                           {
                               Agent = invoice.Invoice.SalesOrder.Agent.FirstName
                                     + (invoice.Invoice.SalesOrder.Agent.MiddleName == null ? "" : " " + invoice.Invoice.SalesOrder.Agent.MiddleName) + " "
                                     + invoice.Invoice.SalesOrder.Agent.LastName,
                               Brand = invoice.Item.Brand.Name,
                               ItemCode = invoice.Item.Code,
                               ItemName = invoice.Item.Name,
                               ItemType = invoice.Item.ItemType.Name,
                               PriceD = invoice.UnitPrice,
                               QuantityD = invoice.Quantity
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
            //get total quantity of data in table
            totalQuantity = data.Sum(s => s.QuantityD);
            //get total amount of data in table
            totalAmount = data.Sum(s => s.AmountD);
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Agent.ToLower().Contains(form.SearchValue)
                                    || x.AmountD.ToString().Contains(form.SearchValue)
                                    || x.Brand.ToLower().Contains(form.SearchValue)
                                    || x.ItemCode.ToLower().Contains(form.SearchValue)
                                    || x.ItemName.ToLower().Contains(form.SearchValue)
                                    || x.ItemType.ToLower().Contains(form.SearchValue)
                                    || x.PriceD.ToString().Contains(form.SearchValue)
                                    || x.QuantityD.ToString().Contains(form.SearchValue));
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
                        case "Amount": data = data.OrderBy(s => s.AmountD); break;
                        case "Brand": data = data.OrderBy(s => s.Brand); break;
                        case "ItemCode": data = data.OrderBy(s => s.ItemCode); break;
                        case "ItemName": data = data.OrderBy(s => s.ItemName); break;
                        case "ItemType": data = data.OrderBy(s => s.ItemType); break;
                        case "Price": data = data.OrderBy(s => s.PriceD); break;
                        case "Quantity": data = data.OrderBy(s => s.QuantityD); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Agent": data = data.OrderByDescending(s => s.Agent); break;
                        case "Amount": data = data.OrderByDescending(s => s.AmountD); break;
                        case "Brand": data = data.OrderByDescending(s => s.Brand); break;
                        case "ItemCode": data = data.OrderByDescending(s => s.ItemCode); break;
                        case "ItemName": data = data.OrderByDescending(s => s.ItemName); break;
                        case "ItemType": data = data.OrderByDescending(s => s.ItemType); break;
                        case "Price": data = data.OrderByDescending(s => s.PriceD); break;
                        case "Quantity": data = data.OrderByDescending(s => s.QuantityD); break;
                        default: break;
                    }
                }
            }
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
            var invoices = from invoice in context.InvoiceLineItems
                           where filter.DateFrom <= (invoice.Invoice.DateModified ?? invoice.Invoice.DateCreated)
                           && (invoice.Invoice.DateModified ?? invoice.Invoice.DateCreated) <= filter.DateTo
                           && (filter.Agent == null || invoice.Invoice.SalesOrder.Agent.Id == filter.Agent)
                           && (filter.Brand == null || invoice.Item.Brand.Id == filter.Brand)
                           && invoice.Invoice.Status.Code == "Invoiced"
                           select new BrandSalesRVM
                           {
                               AgentCode = invoice.Invoice.SalesOrder.Agent.Code,
                               AgentName = invoice.Invoice.SalesOrder.Agent.FirstName
                                         + (invoice.Invoice.SalesOrder.Agent.MiddleName == null ? "" : " " + invoice.Invoice.SalesOrder.Agent.MiddleName)
                                         + " " + invoice.Invoice.SalesOrder.Agent.LastName,
                               AmountD = invoice.UnitPrice * invoice.Quantity,
                               Brand = invoice.Item.Brand.Name
                           };
            var grpinvoice = from invoice in await invoices.ToListAsync()
                             orderby invoice.Brand ascending, invoice.AgentCode ascending
                             group invoice by new
                             {
                                 invoice.AgentCode,
                                 invoice.AgentName,
                                 invoice.Brand
                             } into i
                             select new BrandSalesRVM
                             {
                                 AgentCode = i.Key.AgentCode,
                                 AgentName = i.Key.AgentName,
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
            //get total amount of data in table
            totalAmount = data.Sum(s => s.AmountD);
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.AgentCode.ToLower().Contains(form.SearchValue)
                                    || x.AgentName.ToLower().Contains(form.SearchValue)
                                    || x.AmountD.ToString().Contains(form.SearchValue)
                                    || x.Brand.ToLower().Contains(form.SearchValue));
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
                        case "AgentCode": data = data.OrderBy(s => s.AgentCode); break;
                        case "AgentName": data = data.OrderBy(s => s.AgentName); break;
                        case "Amount": data = data.OrderBy(s => s.AmountD); break;
                        case "Brand": data = data.OrderBy(s => s.Brand); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "AgentCode": data = data.OrderByDescending(s => s.AgentCode); break;
                        case "AgentName": data = data.OrderByDescending(s => s.AgentName); break;
                        case "Amount": data = data.OrderByDescending(s => s.AmountD); break;
                        case "Brand": data = data.OrderByDescending(s => s.Brand); break;
                        default: break;
                    }
                }
            }
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
            var invoices = from invoice in context.InvoiceLineItems
                           let inventory = context.ItemInventories.Where(s => s.ItemId == invoice.Item.Id).Sum(s => s.Quantity)
                           where filter.DateFrom <= (invoice.Invoice.DateModified ?? invoice.Invoice.DateCreated)
                           && (invoice.Invoice.DateModified ?? invoice.Invoice.DateCreated) <= filter.DateTo
                           && (filter.Agent == null || invoice.Invoice.SalesOrder.Agent.Id == filter.Agent)
                           && (filter.Brand == null || invoice.Item.Brand.Id == filter.Brand)
                           && invoice.Invoice.Status.Code == "Invoiced"
                           select new BestSellingItemRVM
                           {
                               AmountD = invoice.Quantity * invoice.UnitPrice,
                               Brand = invoice.Item.Brand.Name,
                               CurrentQuantityD = inventory,
                               ItemCode = invoice.Item.Code,
                               ItemName = invoice.Item.Name,
                               PartNumber = invoice.Item.PartNumber + "",
                               QuantityD = invoice.Quantity,
                               Unit = invoice.Item.Unit.Name,
                               UnitPriceD = invoice.UnitPrice
                           };
            var grpinvoice = from invoice in await invoices.ToListAsync()
                             orderby invoice.Brand ascending, invoice.ItemCode ascending
                             group invoice by new
                             {
                                 invoice.Brand,
                                 invoice.ItemCode,
                                 invoice.ItemName,
                                 invoice.PartNumber,
                                 invoice.Unit
                             } into i
                             select new BestSellingItemRVM
                             {
                                 AmountD = filter.Basis == "Amount" ? i.Sum(s => s.AmountD) : 0,
                                 Brand = i.Key.Brand,
                                 CurrentQuantityD = filter.Basis == "Quantity" ? i.Sum(s => s.CurrentQuantityD) : 0,
                                 ItemCode = i.Key.ItemCode,
                                 ItemName = i.Key.ItemName,
                                 PartNumber = i.Key.PartNumber,
                                 QuantityD = filter.Basis == "Quantity" ? i.Sum(s => s.QuantityD) : 0,
                                 Unit = i.Key.Unit,
                                 UnitPriceD = filter.Basis == "Amount" ? i.Sum(s => s.UnitPriceD) : 0
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
            //get total quantity of date in table
            totalQuantity = data.Sum(s => s.QuantityD);
            //get total current quantity of data in table
            totalCurrentQuantity = data.Sum(s => s.CurrentQuantityD);
            //get total price of data in table
            totalPrice = data.Sum(s => s.UnitPriceD);
            //get total amount of data in table
            totalAmount = data.Sum(s => s.AmountD);
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.AmountD.ToString().Contains(form.SearchValue)
                                    || x.Brand.ToLower().Contains(form.SearchValue)
                                    || x.CurrentQuantityD.ToString().Contains(form.SearchValue)
                                    || x.ItemCode.ToLower().Contains(form.SearchValue)
                                    || x.ItemName.ToLower().Contains(form.SearchValue)
                                    || x.PartNumber.ToLower().Contains(form.SearchValue)
                                    || x.QuantityD.ToString().Contains(form.SearchValue)
                                    || x.Unit.ToLower().Contains(form.SearchValue)
                                    || x.UnitPriceD.ToString().Contains(form.SearchValue));
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
                        case "Brand": data = data.OrderBy(s => s.Brand); break;
                        case "CurrentQuantity": data = data.OrderBy(s => s.CurrentQuantityD); break;
                        case "ItemCode": data = data.OrderBy(s => s.ItemCode); break;
                        case "ItemName": data = data.OrderBy(s => s.ItemName); break;
                        case "PartNumber": data = data.OrderBy(s => s.PartNumber); break;
                        case "Quantity": data = data.OrderBy(s => s.QuantityD); break;
                        case "Unit": data = data.OrderBy(s => s.Unit); break;
                        case "UnitPrice": data = data.OrderBy(s => s.UnitPriceD); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Amount": data = data.OrderByDescending(s => s.AmountD); break;
                        case "Brand": data = data.OrderByDescending(s => s.Brand); break;
                        case "CurrentQuantity": data = data.OrderByDescending(s => s.CurrentQuantityD); break;
                        case "ItemCode": data = data.OrderByDescending(s => s.ItemCode); break;
                        case "ItemName": data = data.OrderByDescending(s => s.ItemName); break;
                        case "PartNumber": data = data.OrderByDescending(s => s.PartNumber); break;
                        case "Quantity": data = data.OrderByDescending(s => s.QuantityD); break;
                        case "Unit": data = data.OrderByDescending(s => s.Unit); break;
                        case "UnitPrice": data = data.OrderByDescending(s => s.UnitPriceD); break;
                        default: break;
                    }
                }
            }
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
            var invoices = from invoice in context.InvoiceLineItems
                           where filter.DateFrom <= (invoice.Invoice.DateModified ?? invoice.Invoice.DateCreated)
                           && (invoice.Invoice.DateModified ?? invoice.Invoice.DateCreated) <= filter.DateTo
                           && (filter.Agent == null || invoice.Invoice.SalesOrder.Agent.Id == filter.Agent)
                           && (filter.Area == null || invoice.Invoice.SalesOrder.Agent.Area.Id == filter.Area)
                           && (filter.Customer == null || invoice.Invoice.SalesOrder.Customer.Id == filter.Customer)
                           && invoice.Invoice.Status.Code == "Invoiced"
                           select new CommissionRVM
                           {
                               Agent = invoice.Invoice.SalesOrder.Agent.FirstName
                                     + (invoice.Invoice.SalesOrder.Agent.MiddleName == null ? "" : " " + invoice.Invoice.SalesOrder.Agent.MiddleName)
                                     + " " + invoice.Invoice.SalesOrder.Agent.LastName,
                               CommissionRateD = invoice.Invoice.SalesOrder.Agent.CommissionRate > 0
                                               ? invoice.Invoice.SalesOrder.Agent.CommissionRate
                                               : invoice.Invoice.SalesOrder.Agent.Area.CommissionRate,
                               Customer = invoice.Invoice.SalesOrder.Customer.Name,
                               InvoiceAmountD = invoice.Quantity * invoice.UnitPrice,
                               InvoiceDateD = invoice.Invoice.DateModified ?? invoice.Invoice.DateCreated,
                               InvoiceNumber = invoice.Invoice.InvoiceNumber
                           };
            var grpinvoice = from invoice in await invoices.ToListAsync()
                             orderby invoice.Agent ascending, invoice.InvoiceDateD ascending, invoice.Customer ascending
                             group invoice by new
                             {
                                 invoice.Agent,
                                 invoice.CommissionRateD,
                                 invoice.Customer,
                                 invoice.InvoiceDateD,
                                 invoice.InvoiceNumber
                             } into i
                             select new CommissionRVM
                             {
                                 Agent = i.Key.Agent,
                                 CommissionRateD = i.Key.CommissionRateD,
                                 Customer = i.Key.Customer,
                                 InvoiceAmountD = i.Sum(s => s.InvoiceAmountD),
                                 InvoiceDateD = i.Key.InvoiceDateD,
                                 InvoiceNumber = i.Key.InvoiceNumber,
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
            //get total invoice amount of data in table
            totalInvoice = data.Sum(s => s.InvoiceAmountD);
            //get total commission amount of data in table
            totalCommission = data.Sum(s => s.CommissionRateD);
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Agent.ToLower().Contains(form.SearchValue)
                                    || x.CommissionAmountD.ToString().Contains(form.SearchValue)
                                    || x.CommissionRateD.ToString().Contains(form.SearchValue)
                                    || x.Customer.ToLower().Contains(form.SearchValue)
                                    || x.InvoiceAmountD.ToString().Contains(form.SearchValue)
                                    || x.InvoiceDate.ToLower().Contains(form.SearchValue)
                                    || x.InvoiceNumber.ToLower().Contains(form.SearchValue));
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
                        case "CommissionRate": data = data.OrderBy(s => s.CommissionRateD); break;
                        case "CommissionAmount": data = data.OrderBy(s => s.CommissionAmountD); break;
                        case "Customer": data = data.OrderBy(s => s.Customer); break;
                        case "InvoiceAmount": data = data.OrderBy(s => s.InvoiceAmountD); break;
                        case "InvoiceDate": data = data.OrderBy(s => s.InvoiceDateD); break;
                        case "InvoiceNumber": data = data.OrderBy(s => s.InvoiceNumber); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Agent": data = data.OrderByDescending(s => s.Agent); break;
                        case "CommissionRate": data = data.OrderByDescending(s => s.CommissionRateD); break;
                        case "CommissionAmount": data = data.OrderByDescending(s => s.CommissionAmountD); break;
                        case "Customer": data = data.OrderByDescending(s => s.Customer); break;
                        case "InvoiceAmount": data = data.OrderByDescending(s => s.InvoiceAmountD); break;
                        case "InvoiceDate": data = data.OrderByDescending(s => s.InvoiceDateD); break;
                        case "InvoiceNumber": data = data.OrderByDescending(s => s.InvoiceNumber); break;
                        default: break;
                    }
                }
            }
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
                Agent = filter.Agent == null || agent == null ? "All" : agent.FirstName + (agent.MiddleName == null ? "" : " " + agent.MiddleName) + " " + agent.LastName,
                Area = filter.Area == null || area == null ? "All" : area.Name,
                Customer = filter.Customer == null || customer == null ? "All" : customer.Name,
                TotalInvoiceAmountD = items.Sum(s => s.InvoiceAmountD),
                TotalCommissionAmountD = items.Sum(s => s.CommissionAmountD)
            };
        }

        #endregion Commission

        #region Customer Sales Ranking

        public async Task<IList<CustomerSalesRankingRVM>> GetCustomerSalesRanking(ListFilterSalesModel filter)
        {
            var invoices = from invoice in context.InvoiceLineItems
                           where filter.DateFrom <= (invoice.Invoice.DateModified ?? invoice.Invoice.DateCreated)
                           && (invoice.Invoice.DateModified ?? invoice.Invoice.DateCreated) <= filter.DateTo
                           && (filter.Region == null || (invoice.Invoice.SalesOrder.Customer.Area != null && invoice.Invoice.SalesOrder.Customer.Area.Region.Id == filter.Region))
                           && invoice.Invoice.Status.Code == "Invoiced"
                           select new CustomerSalesRankingRVM
                           {
                               AmountD = invoice.Quantity * invoice.UnitPrice,
                               CustomerCode = invoice.Invoice.SalesOrder.Customer.Code,
                               CustomerName = invoice.Invoice.SalesOrder.Customer.Name,
                               RankD = 0
                           };
            var grpinvoice = (await invoices.ToListAsync())
                            .GroupBy(s => new
                            {
                                s.CustomerCode,
                                s.CustomerName
                            })
                            .OrderByDescending(s => s.Sum(a => a.AmountD))
                            .Select((s, i) => new CustomerSalesRankingRVM
                            {
                                AmountD = s.Sum(s => s.AmountD),
                                CustomerCode = s.Key.CustomerCode,
                                CustomerName = s.Key.CustomerName,
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
            //get total amount of data in table
            totalAmount = data.Sum(s => s.AmountD);
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.AmountD.ToString().Contains(form.SearchValue)
                                    || x.CustomerCode.ToLower().Contains(form.SearchValue)
                                    || x.CustomerName.ToLower().Contains(form.SearchValue)
                                    || x.RankD.ToString().Contains(form.SearchValue));
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
                        case "CustomerCode": data = data.OrderBy(s => s.CustomerCode); break;
                        case "CustomerName": data = data.OrderBy(s => s.CustomerName); break;
                        case "Rank": data = data.OrderBy(s => s.RankD); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Amount": data = data.OrderByDescending(s => s.AmountD); break;
                        case "CustomerCode": data = data.OrderByDescending(s => s.CustomerCode); break;
                        case "CustomerName": data = data.OrderByDescending(s => s.CustomerName); break;
                        case "Rank": data = data.OrderByDescending(s => s.RankD); break;
                        default: break;
                    }
                }
            }
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
            var invoices = from invoice in context.InvoiceLineItems
                           where filter.DateFrom <= (invoice.Invoice.DateModified ?? invoice.Invoice.DateCreated)
                           && (invoice.Invoice.DateModified ?? invoice.Invoice.DateCreated) <= filter.DateTo
                           && (filter.Brand == null || invoice.Item.Brand.Id == filter.Brand)
                           && (filter.Customer == null || invoice.Invoice.SalesOrder.Customer.Id == filter.Customer)
                           && invoice.Invoice.Status.Code == "Invoiced"
                           select new CustomerSalesRVM
                           {
                               AmountD = invoice.UnitPrice * invoice.Quantity,
                               Brand = invoice.Item.Brand.Name,
                               CustomerCode = invoice.Invoice.SalesOrder.Customer.Code,
                               CustomerName = invoice.Invoice.SalesOrder.Customer.Name
                           };
            var grpinvoice = from invoice in await invoices.ToListAsync()
                             orderby invoice.CustomerCode ascending, invoice.Brand ascending
                             group invoice by new
                             {
                                 invoice.CustomerCode,
                                 invoice.CustomerName,
                                 invoice.Brand
                             } into i
                             select new CustomerSalesRVM
                             {
                                 AmountD = i.Sum(s => s.AmountD),
                                 Brand = i.Key.Brand,
                                 CustomerCode = i.Key.CustomerCode,
                                 CustomerName = i.Key.CustomerName,
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
            //get total amount of data in table
            totalAmount = data.Sum(s => s.AmountD);
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.AmountD.ToString().Contains(form.SearchValue)
                                    || x.Brand.ToLower().Contains(form.SearchValue)
                                    || x.CustomerCode.ToLower().Contains(form.SearchValue)
                                    || x.CustomerName.ToLower().Contains(form.SearchValue));
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
                        case "Brand": data = data.OrderBy(s => s.Brand); break;
                        case "CustomerCode": data = data.OrderBy(s => s.CustomerCode); break;
                        case "CustomerName": data = data.OrderBy(s => s.CustomerName); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Amount": data = data.OrderByDescending(s => s.AmountD); break;
                        case "Brand": data = data.OrderByDescending(s => s.Brand); break;
                        case "CustomerCode": data = data.OrderByDescending(s => s.CustomerCode); break;
                        case "CustomerName": data = data.OrderByDescending(s => s.CustomerName); break;
                        default: break;
                    }
                }
            }
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
            var invoices = from invoice in context.InvoiceLineItems
                           where filter.DateFrom <= (invoice.Invoice.DateModified ?? invoice.Invoice.DateCreated)
                           && (invoice.Invoice.DateModified ?? invoice.Invoice.DateCreated) <= filter.DateTo
                           && invoice.Invoice.InvoiceNumber.CompareTo(filter.InvoiceNumberFrom) >= 0
                           && invoice.Invoice.InvoiceNumber.CompareTo(filter.InvoiceNumberTo) <= 0
                           && (filter.StatusCode == null || invoice.Invoice.Status.Code == filter.StatusCode)
                           select new DailySalesRVM
                           {
                               Agent = invoice.Invoice.SalesOrder.Agent.FirstName
                                     + (invoice.Invoice.SalesOrder.Agent.MiddleName == null ? "" : " " + invoice.Invoice.SalesOrder.Agent.MiddleName) + " "
                                     + invoice.Invoice.SalesOrder.Agent.LastName,
                               AmountD = invoice.UnitPrice * invoice.Quantity,
                               CommissionRateD = invoice.Invoice.SalesOrder.Agent.CommissionRate > 0
                                               ? invoice.Invoice.SalesOrder.Agent.CommissionRate
                                               : invoice.Invoice.SalesOrder.Agent.Area.CommissionRate,
                               CustomerCode = invoice.Invoice.SalesOrder.Customer.Code,
                               CustomerName = invoice.Invoice.SalesOrder.Customer.Name,
                               InvoiceDateD = (invoice.Invoice.DateModified ?? invoice.Invoice.DateCreated).Date,
                               InvoiceNumber = invoice.Invoice.InvoiceNumber,
                               Term = invoice.Invoice.SalesOrder.Customer.CreditTerm == null ? "" : invoice.Invoice.SalesOrder.Customer.CreditTerm.Name
                           };
            var grpinvoice = from invoice in await invoices.ToListAsync()
                             orderby invoice.InvoiceDateD ascending, invoice.InvoiceNumber ascending
                             group invoice by new
                             {
                                 invoice.Agent,
                                 invoice.CommissionRateD,
                                 invoice.CustomerCode,
                                 invoice.CustomerName,
                                 invoice.InvoiceDateD,
                                 invoice.InvoiceNumber,
                                 invoice.Term
                             } into i
                             select new DailySalesRVM
                             {
                                 Agent = i.Key.Agent,
                                 AmountD = i.Sum(s => s.AmountD),
                                 CommissionRateD = i.Key.CommissionRateD,
                                 CustomerCode = i.Key.CustomerCode,
                                 CustomerName = i.Key.CustomerName,
                                 InvoiceDateD = i.Key.InvoiceDateD,
                                 InvoiceNumber = i.Key.InvoiceNumber,
                                 Term = i.Key.Term
                             };
            return grpinvoice.ToList();
        }

        public async Task<ForDataTables<DailySalesRVM>> GetDailySalesDT(DataTablesRequestForm form, ListFilterSalesModel filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            decimal totalAmount = 0;

            var data = (await GetDailySales(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            //get total amount of data in table
            totalAmount = data.Sum(s => s.AmountD);
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Agent.ToLower().Contains(form.SearchValue)
                                    || x.AmountD.ToString().Contains(form.SearchValue)
                                    || x.CommissionRateD.ToString().Contains(form.SearchValue)
                                    || x.CustomerCode.ToLower().Contains(form.SearchValue)
                                    || x.CustomerName.ToLower().Contains(form.SearchValue)
                                    || x.InvoiceDate.ToLower().Contains(form.SearchValue)
                                    || x.InvoiceNumber.ToLower().Contains(form.SearchValue)
                                    || x.Term.ToLower().Contains(form.SearchValue));
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
                        case "Amount": data = data.OrderBy(s => s.AmountD); break;
                        case "CommissionRate": data = data.OrderBy(s => s.CommissionRateD); break;
                        case "CustomerCode": data = data.OrderBy(s => s.CustomerCode); break;
                        case "CustomerName": data = data.OrderBy(s => s.CustomerName); break;
                        case "InvoiceDate": data = data.OrderBy(s => s.InvoiceDateD); break;
                        case "InvoiceNumber": data = data.OrderBy(s => s.InvoiceNumber); break;
                        case "Term": data = data.OrderBy(s => s.Term); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Agent": data = data.OrderByDescending(s => s.Agent); break;
                        case "Amount": data = data.OrderByDescending(s => s.AmountD); break;
                        case "CommissionRate": data = data.OrderByDescending(s => s.CommissionRateD); break;
                        case "CustomerCode": data = data.OrderByDescending(s => s.CustomerCode); break;
                        case "CustomerName": data = data.OrderByDescending(s => s.CustomerName); break;
                        case "InvoiceDate": data = data.OrderByDescending(s => s.InvoiceDateD); break;
                        case "InvoiceNumber": data = data.OrderByDescending(s => s.InvoiceNumber); break;
                        case "Term": data = data.OrderByDescending(s => s.Term); break;
                        default: break;
                    }
                }
            }
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

        public async Task<DailySalesPVM> PrintDailySales(ListFilterSalesModel filter)
        {
            var items = await GetDailySales(filter);
            var brand = await context.Brands.Where(s => s.Id == filter.Brand).FirstOrDefaultAsync();
            return new DailySalesPVM
            {
                Contents = items,
                DateFromD = filter.DateFrom.GetValueOrDefault(),
                DateToD = filter.DateTo.GetValueOrDefault(),
                InvoiceNumberFrom = filter.InvoiceNumberFrom == null ? "All" : filter.InvoiceNumberFrom,
                InvoiceNumberTo = filter.InvoiceNumberTo == null ? "All" : filter.InvoiceNumberTo,
                Status = filter.StatusCode == null ? "All" : filter.StatusCode,
                TotalAmountD = items.Sum(s => s.AmountD)
            };
        }

        #endregion Daily Sales

        #region Sales Invoice

        public async Task<IList<SalesInvoiceRVM>> GetSalesInvoice(ListFilterSalesModel filter)
        {
            var invoices = from invoice in context.InvoiceLineItems
                           where filter.DateFrom <= (invoice.Invoice.DateModified ?? invoice.Invoice.DateCreated)
                           && (invoice.Invoice.DateModified ?? invoice.Invoice.DateCreated) <= filter.DateTo
                           && (filter.Agent == null || invoice.Invoice.SalesOrder.Agent.Id == filter.Agent)
                           && (filter.Customer == null || invoice.Invoice.SalesOrder.Customer.Id == filter.Customer)
                           && (filter.StatusCode == null || invoice.Invoice.Status.Code == filter.StatusCode)
                           select new SalesInvoiceRVM
                           {
                               Agent = invoice.Invoice.SalesOrder.Agent.FirstName
                                     + (invoice.Invoice.SalesOrder.Agent.MiddleName == null ? "" : " " + invoice.Invoice.SalesOrder.Agent.MiddleName) + " "
                                     + invoice.Invoice.SalesOrder.Agent.LastName,
                               AmountD = invoice.UnitPrice * invoice.Quantity,
                               ControlNumber = invoice.Invoice.ControlNumber,
                               CustomerCode = invoice.Invoice.SalesOrder.Customer.Code,
                               CustomerName = invoice.Invoice.SalesOrder.Customer.Name,
                               InvoiceDateD = (invoice.Invoice.DateModified ?? invoice.Invoice.DateCreated).Date,
                               InvoiceNumber = invoice.Invoice.InvoiceNumber,
                               Status = invoice.Invoice.Status.Code
                           };
            var grpinvoice = from invoice in await invoices.ToListAsync()
                             orderby invoice.ControlNumber ascending, invoice.InvoiceDateD ascending, invoice.CustomerCode ascending
                             group invoice by new
                             {
                                 invoice.Agent,
                                 invoice.ControlNumber,
                                 invoice.CustomerCode,
                                 invoice.CustomerName,
                                 invoice.InvoiceDateD,
                                 invoice.InvoiceNumber,
                                 invoice.Status
                             } into i
                             select new SalesInvoiceRVM
                             {
                                 Agent = i.Key.Agent,
                                 AmountD = i.Sum(s => s.AmountD),
                                 ControlNumber = i.Key.ControlNumber,
                                 CustomerCode = i.Key.CustomerCode,
                                 CustomerName = i.Key.CustomerName,
                                 InvoiceDateD = i.Key.InvoiceDateD,
                                 InvoiceNumber = i.Key.InvoiceNumber,
                                 Status = i.Key.Status
                             };
            return grpinvoice.ToList();
        }

        public async Task<ForDataTables<SalesInvoiceRVM>> GetSalesInvoiceDT(DataTablesRequestForm form, ListFilterSalesModel filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            decimal totalAmount = 0;

            var data = (await GetSalesInvoice(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            //get total amount of data in table
            totalAmount = data.Sum(s => s.AmountD);
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Agent.ToLower().Contains(form.SearchValue)
                                    || x.AmountD.ToString().Contains(form.SearchValue)
                                    || x.ControlNumber.ToLower().Contains(form.SearchValue)
                                    || x.CustomerCode.ToLower().Contains(form.SearchValue)
                                    || x.CustomerName.ToLower().Contains(form.SearchValue)
                                    || x.InvoiceDate.ToLower().Contains(form.SearchValue)
                                    || x.InvoiceNumber.ToLower().Contains(form.SearchValue)
                                    || x.Status.ToLower().Contains(form.SearchValue));
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
                        case "Amount": data = data.OrderBy(s => s.AmountD); break;
                        case "ControlNumber": data = data.OrderBy(s => s.ControlNumber); break;
                        case "CustomerCode": data = data.OrderBy(s => s.CustomerCode); break;
                        case "CustomerName": data = data.OrderBy(s => s.CustomerName); break;
                        case "InvoiceDate": data = data.OrderBy(s => s.InvoiceDateD); break;
                        case "InvoiceNumber": data = data.OrderBy(s => s.InvoiceNumber); break;
                        case "Status": data = data.OrderBy(s => s.Status); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Agent": data = data.OrderByDescending(s => s.Agent); break;
                        case "Amount": data = data.OrderByDescending(s => s.AmountD); break;
                        case "ControlNumber": data = data.OrderByDescending(s => s.ControlNumber); break;
                        case "CustomerCode": data = data.OrderByDescending(s => s.CustomerCode); break;
                        case "CustomerName": data = data.OrderByDescending(s => s.CustomerName); break;
                        case "InvoiceDate": data = data.OrderByDescending(s => s.InvoiceDateD); break;
                        case "InvoiceNumber": data = data.OrderByDescending(s => s.InvoiceNumber); break;
                        case "Status": data = data.OrderByDescending(s => s.Status); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<SalesInvoiceRVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList,
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
                TotalAmountD = items.Sum(s => s.AmountD)
            };
        }

        #endregion Sales Invoice

        #region Sales by Agent / Area

        public async Task<IList<SalesAgentAreaRVM>> GetSalesAgentArea(ListFilterSalesModel filter)
        {
            var invoices = from invoice in context.InvoiceLineItems
                           where filter.DateFrom <= (invoice.Invoice.DateModified ?? invoice.Invoice.DateCreated)
                           && (invoice.Invoice.DateModified ?? invoice.Invoice.DateCreated) <= filter.DateTo
                           && (filter.Agent == null || invoice.Invoice.SalesOrder.Agent.Id == filter.Agent)
                           && (filter.Area == null || invoice.Invoice.SalesOrder.Customer.Area.Id == filter.Area)
                           && invoice.Invoice.Status.Code == "Invoiced"
                           select new SalesAgentAreaRVM
                           {
                               AgentCode = invoice.Invoice.SalesOrder.Agent.Code,
                               AgentName = invoice.Invoice.SalesOrder.Agent.FirstName
                                         + (invoice.Invoice.SalesOrder.Agent.MiddleName == null ? "" : " " + invoice.Invoice.SalesOrder.Agent.MiddleName) + " "
                                         + invoice.Invoice.SalesOrder.Agent.LastName,
                               Area = invoice.Invoice.SalesOrder.Customer.Area.Name,
                               SalesD = invoice.UnitPrice * invoice.Quantity,
                               PercentSalesD = 0
                           };
            var grpinvoice = from invoice in await invoices.ToListAsync()
                             orderby invoice.AgentName ascending, invoice.Area ascending
                             group invoice by new
                             {
                                 invoice.AgentCode,
                                 invoice.AgentName,
                                 invoice.Area
                             } into i
                             let total = invoices.Where(s => s.AgentCode == i.Key.AgentCode).Sum(s => s.SalesD)
                             select new SalesAgentAreaRVM
                             {
                                 AgentCode = i.Key.AgentCode,
                                 AgentName = i.Key.AgentName,
                                 Area = i.Key.Area,
                                 SalesD = i.Sum(s => s.SalesD),
                                 PercentSalesD = (i.Sum(s => s.SalesD) / total) * 100
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
            //get total amount of data in table
            totalAmount = data.Sum(s => s.SalesD);
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.AgentCode.ToLower().Contains(form.SearchValue)
                                    || x.AgentName.ToLower().Contains(form.SearchValue)
                                    || x.Area.ToLower().Contains(form.SearchValue)
                                    || x.SalesD.ToString().Contains(form.SearchValue)
                                    || x.PercentSalesD.ToString().Contains(form.SearchValue));
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
                        case "AgentCode": data = data.OrderBy(s => s.AgentCode); break;
                        case "AgentName": data = data.OrderBy(s => s.AgentName); break;
                        case "Area": data = data.OrderBy(s => s.Area); break;
                        case "Sales": data = data.OrderBy(s => s.SalesD); break;
                        case "PercentSales": data = data.OrderBy(s => s.PercentSalesD); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "AgentCode": data = data.OrderByDescending(s => s.AgentCode); break;
                        case "AgentName": data = data.OrderByDescending(s => s.AgentName); break;
                        case "Area": data = data.OrderByDescending(s => s.Area); break;
                        case "Sales": data = data.OrderByDescending(s => s.SalesD); break;
                        case "PercentSales": data = data.OrderByDescending(s => s.PercentSalesD); break;
                        default: break;
                    }
                }
            }
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
            var invoices = from invoice in context.InvoiceLineItems
                           where filter.DateFrom <= (invoice.Invoice.DateModified ?? invoice.Invoice.DateCreated)
                           && (invoice.Invoice.DateModified ?? invoice.Invoice.DateCreated) <= filter.DateTo
                           && invoice.Invoice.Status.Code == "Invoiced"
                           select new SalesReportBasisRVM
                           {
                               Code = filter.Basis == "Customer" ? invoice.Invoice.SalesOrder.Customer.Code
                                    : filter.Basis == "Region" ? invoice.Invoice.SalesOrder.Customer.Area.Region.Code
                                    : /*filter.Basis == "Brand" ?*/ invoice.Item.Brand.Code,
                               Name = filter.Basis == "Customer" ? invoice.Invoice.SalesOrder.Customer.Name
                                    : filter.Basis == "Region" ? invoice.Invoice.SalesOrder.Customer.Area.Region.Name
                                    : /*filter.Basis == "Brand" ?*/ invoice.Item.Brand.Name,
                               AmountD = invoice.UnitPrice * invoice.Quantity
                           };
            var grpinvoice = from invoice in await invoices.ToListAsync()
                             orderby invoice.AmountD descending
                             group invoice by new
                             {
                                 invoice.Code,
                                 invoice.Name
                             } into i
                             select new SalesReportBasisRVM
                             {
                                 Code = i.Key.Code,
                                 Name = i.Key.Name,
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
            //get total amount of data in table
            totalAmount = data.Sum(s => s.AmountD);
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Code.ToLower().Contains(form.SearchValue)
                                    || x.Name.ToLower().Contains(form.SearchValue)
                                    || x.AmountD.ToString().Contains(form.SearchValue));
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
                        case "Amount": data = data.OrderBy(s => s.AmountD); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Code": data = data.OrderByDescending(s => s.Code); break;
                        case "Name": data = data.OrderByDescending(s => s.Name); break;
                        case "Amount": data = data.OrderByDescending(s => s.AmountD); break;
                        default: break;
                    }
                }
            }
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
                           where filter.DateFrom <= (sreturn.SalesReturn.DateModified ?? sreturn.SalesReturn.DateCreated)
                           && (sreturn.SalesReturn.DateModified ?? sreturn.SalesReturn.DateCreated) <= filter.DateTo
                           && (filter.Customer == null || sreturn.SalesReturn.Customer.Id == filter.Customer)
                           && (filter.Item == null || sreturn.Item.Id == filter.Item)
                           select new SalesReturnRVM
                           {
                               Agent = sreturn.Invoice.SalesOrder.Agent.Code,
                               Brand = sreturn.Item.Brand.Name,
                               ControlNumber = sreturn.Invoice.ControlNumber,
                               Customer = sreturn.SalesReturn.Customer.Name,
                               ItemCode = sreturn.Item.Code,
                               ItemName = sreturn.Item.Name,
                               ItemType = sreturn.Item.ItemType.Code,
                               PriceReturnedD = sreturn.SellingPrice,
                               QuantityReturnedD = sreturn.Quantity,
                               ReturnDateD = sreturn.SalesReturn.DateModified ?? sreturn.SalesReturn.DateCreated,
                               SINumber = sreturn.Invoice.InvoiceNumber,
                               SRNumber = sreturn.SalesReturn.Number,
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
            //get total quantity return in table
            totalQuantity = data.Sum(s => s.QuantityReturnedD);
            //get total price return in table
            totalReturn = data.Sum(s => s.PriceReturnedD);
            //get total amount of data in table
            totalAmount = data.Sum(s => s.AmountD);
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Agent.ToLower().Contains(form.SearchValue)
                                    || x.AmountD.ToString().Contains(form.SearchValue)
                                    || x.Brand.ToLower().Contains(form.SearchValue)
                                    || x.ControlNumber.ToLower().Contains(form.SearchValue)
                                    || x.Customer.ToLower().Contains(form.SearchValue)
                                    || x.ItemCode.ToLower().Contains(form.SearchValue)
                                    || x.ItemName.ToLower().Contains(form.SearchValue)
                                    || x.ItemType.ToLower().Contains(form.SearchValue)
                                    || x.PriceReturnedD.ToString().Contains(form.SearchValue)
                                    || x.QuantityReturnedD.ToString().Contains(form.SearchValue)
                                    || x.ReturnDate.ToLower().Contains(form.SearchValue)
                                    || x.SINumber.ToLower().Contains(form.SearchValue)
                                    || x.SRNumber.ToLower().Contains(form.SearchValue)
                                    || x.Unit.ToLower().Contains(form.SearchValue));
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
                        case "Amount": data = data.OrderBy(s => s.AmountD); break;
                        case "Brand": data = data.OrderBy(s => s.Brand); break;
                        case "ControlNumber": data = data.OrderBy(s => s.ControlNumber); break;
                        case "Customer": data = data.OrderBy(s => s.Customer); break;
                        case "ItemCode": data = data.OrderBy(s => s.ItemCode); break;
                        case "ItemName": data = data.OrderBy(s => s.ItemName); break;
                        case "ItemType": data = data.OrderBy(s => s.ItemType); break;
                        case "PriceReturned": data = data.OrderBy(s => s.PriceReturnedD); break;
                        case "QuantityReturned": data = data.OrderBy(s => s.QuantityReturnedD); break;
                        case "ReturnDate": data = data.OrderBy(s => s.ReturnDateD); break;
                        case "SINumber": data = data.OrderBy(s => s.SINumber); break;
                        case "SRNumber": data = data.OrderBy(s => s.SRNumber); break;
                        case "Unit": data = data.OrderBy(s => s.Unit); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Agent": data = data.OrderByDescending(s => s.Agent); break;
                        case "Amount": data = data.OrderByDescending(s => s.AmountD); break;
                        case "Brand": data = data.OrderByDescending(s => s.Brand); break;
                        case "ControlNumber": data = data.OrderByDescending(s => s.ControlNumber); break;
                        case "Customer": data = data.OrderByDescending(s => s.Customer); break;
                        case "ItemCode": data = data.OrderByDescending(s => s.ItemCode); break;
                        case "ItemName": data = data.OrderByDescending(s => s.ItemName); break;
                        case "ItemType": data = data.OrderByDescending(s => s.ItemType); break;
                        case "PriceReturned": data = data.OrderByDescending(s => s.PriceReturnedD); break;
                        case "QuantityReturned": data = data.OrderByDescending(s => s.QuantityReturnedD); break;
                        case "ReturnDate": data = data.OrderByDescending(s => s.ReturnDateD); break;
                        case "SINumber": data = data.OrderByDescending(s => s.SINumber); break;
                        case "SRNumber": data = data.OrderByDescending(s => s.SRNumber); break;
                        case "Unit": data = data.OrderByDescending(s => s.Unit); break;
                        default: break;
                    }
                }
            }
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
                          where soline.Quantity - soline.PackedQuantity > 0
                          && (filter.Brand == null || soline.Item.Brand.Id == filter.Brand)
                          select new UnservedItemRVM
                          {
                              Brand = soline.Item.Brand.Name,
                              CurrentD = 0,
                              ItemCode = soline.Item.Code,
                              ItemName = soline.Item.Name,
                              PendingD = 0,
                              ReplenishDateD = soline.DateModified ?? soline.DateCreated
                          };
            return await solines.ToListAsync();
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
            //get total pending of data in table
            totalPending = data.Sum(s => s.PendingD);
            //get total current of data in table
            totalCurrent = data.Sum(s => s.CurrentD);
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Brand.ToLower().Contains(form.SearchValue)
                                    || x.CurrentD.ToString().Contains(form.SearchValue)
                                    || x.ItemCode.ToLower().Contains(form.SearchValue)
                                    || x.ItemName.ToLower().Contains(form.SearchValue)
                                    || x.PendingD.ToString().Contains(form.SearchValue)
                                    || x.ReplenishDate.ToLower().Contains(form.SearchValue));
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
                        case "Brand": data = data.OrderBy(s => s.Brand); break;
                        case "Current": data = data.OrderBy(s => s.CurrentD); break;
                        case "ItemCode": data = data.OrderBy(s => s.ItemCode); break;
                        case "ItemName": data = data.OrderBy(s => s.ItemName); break;
                        case "Pending": data = data.OrderBy(s => s.PendingD); break;
                        case "ReplenishDate": data = data.OrderBy(s => s.ReplenishDateD); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Brand": data = data.OrderByDescending(s => s.Brand); break;
                        case "Current": data = data.OrderByDescending(s => s.CurrentD); break;
                        case "ItemCode": data = data.OrderByDescending(s => s.ItemCode); break;
                        case "ItemName": data = data.OrderByDescending(s => s.ItemName); break;
                        case "Pending": data = data.OrderByDescending(s => s.PendingD); break;
                        case "ReplenishDate": data = data.OrderByDescending(s => s.ReplenishDateD); break;
                        default: break;
                    }
                }
            }
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
    }
}