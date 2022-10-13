using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Globalization;

namespace GMGInventory.Application.Repositories
{
    public class ReportSalesRepository : IReportSalesRepository
    {
        private readonly ApplicationDbContext context;

        public ReportSalesRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        #region Sales by Customer
        public async Task<IList<SalesCustomerRVM>> GetCustomerSales(ListFilterSalesModel filter)
        {
            var customers = from customer in context.Customers
                            select new SalesCustomerRVM
                            {
                                AmountD = 0,
                                Brand = "",
                                CustomerCode = customer.Code,
                                CustomerName = customer.Name
                            };
            return await customers.ToListAsync();
        }

        public async Task<ForDataTables<SalesCustomerRVM>> GetCustomerSalesDT(DataTablesRequestForm form, ListFilterSalesModel filter)
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
            return new ForDataTables<SalesCustomerRVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList,
                TotalAmount = totalAmount
            };
        }

        public async Task<SalesCustomerPVM> PrintCustomerSales(ListFilterSalesModel filter)
        {
            var items = await GetCustomerSales(filter);
            var brand = await context.Brands.Where(s => s.Id == filter.Brand).FirstOrDefaultAsync();
            var customer = await context.Customers.Where(s => s.Id == filter.Customer).FirstOrDefaultAsync();
            return new SalesCustomerPVM
            {
                Contents = items,
                DateFromD = filter.DateFrom.GetValueOrDefault(),
                DateToD = filter.DateTo.GetValueOrDefault(),
                Customer = filter.Customer == null || customer == null ? "All" : customer.Name,
                Brand = filter.Brand == null || brand == null ? "All" : brand.Name,
                TotalAmountD = items.Sum(s => s.AmountD)
            };
        }
        #endregion

        public IList<int> GetAgentSalesPerItems()
        {
            return context.Users.Select(s => s.AccessFailedCount).ToList(); // This is temporary
        }

        public IList<int> GetBrandSales()
        {
            return context.Users.Select(s => s.AccessFailedCount).ToList(); // This is temporary
        }

        public IList<int> GetBestSellingItem()
        {
            return context.Users.Select(s => s.AccessFailedCount).ToList(); // This is temporary
        }

        public IList<int> GetBestSellingItemInvoice()
        {
            return context.Users.Select(s => s.AccessFailedCount).ToList(); // This is temporary
        }

        public IList<int> GetCommission()
        {
            return context.Users.Select(s => s.AccessFailedCount).ToList(); // This is temporary
        }

        public IList<int> GetCustomerSalesAnalytics()
        {
            return context.Users.Select(s => s.AccessFailedCount).ToList(); // This is temporary
        }

        public IList<int> GetDailySalesReport()
        {
            return context.Users.Select(s => s.AccessFailedCount).ToList(); // This is temporary
        }

        public IList<int> GetDailySalesMargin()
        {
            return context.Users.Select(s => s.AccessFailedCount).ToList(); // This is temporary
        }

        public IList<int> GetSalesInvoiceListing()
        {
            return context.Users.Select(s => s.AccessFailedCount).ToList(); // This is temporary
        }

        public IList<int> GetSalesByAgentArea()
        {
            return context.Users.Select(s => s.AccessFailedCount).ToList(); // This is temporary
        }

        public IList<int> GetSalesMarginItem()
        {
            return context.Users.Select(s => s.AccessFailedCount).ToList(); // This is temporary
        }

        public IList<int> GetSalesReportBasis()
        {
            return context.Users.Select(s => s.AccessFailedCount).ToList(); // This is temporary
        }

        public IList<int> GetSalesReturnListing()
        {
            return context.Users.Select(s => s.AccessFailedCount).ToList(); // This is temporary
        }


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
        #endregion
    }
}