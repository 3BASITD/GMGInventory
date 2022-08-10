using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.Data.SqlClient;
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

        public List<int> GetAgentSalesPerItems()
        {
            return context.Users.Select(s => s.AccessFailedCount).ToList(); // This is temporary
        }

        public List<int> GetBrandSales()
        {
            return context.Users.Select(s => s.AccessFailedCount).ToList(); // This is temporary
        }

        public List<int> GetBestSellingItem()
        {
            return context.Users.Select(s => s.AccessFailedCount).ToList(); // This is temporary
        }

        public List<int> GetBestSellingItemInvoice()
        {
            return context.Users.Select(s => s.AccessFailedCount).ToList(); // This is temporary
        }

        public List<int> GetCommission()
        {
            return context.Users.Select(s => s.AccessFailedCount).ToList(); // This is temporary
        }

        public List<int> GetCustomerSalesAnalytics()
        {
            return context.Users.Select(s => s.AccessFailedCount).ToList(); // This is temporary
        }

        public List<int> GetCustomerSales()
        {
            return context.Users.Select(s => s.AccessFailedCount).ToList(); // This is temporary
        }

        public List<int> GetDailySalesReport()
        {
            return context.Users.Select(s => s.AccessFailedCount).ToList(); // This is temporary
        }

        public List<int> GetDailySalesMargin()
        {
            return context.Users.Select(s => s.AccessFailedCount).ToList(); // This is temporary
        }

        public List<int> GetSalesInvoiceListing()
        {
            return context.Users.Select(s => s.AccessFailedCount).ToList(); // This is temporary
        }

        public List<int> GetSalesByAgentArea()
        {
            return context.Users.Select(s => s.AccessFailedCount).ToList(); // This is temporary
        }

        public List<int> GetSalesMarginItem()
        {
            return context.Users.Select(s => s.AccessFailedCount).ToList(); // This is temporary
        }

        public List<int> GetSalesReportBasis()
        {
            return context.Users.Select(s => s.AccessFailedCount).ToList(); // This is temporary
        }

        public List<int> GetSalesReturnListing()
        {
            return context.Users.Select(s => s.AccessFailedCount).ToList(); // This is temporary
        }

        public List<UnservedItemVM> GetUnservedItem(UnservedItemFilter filter)
        {
            List<UnservedItemVM> unservedItemVMs = new List<UnservedItemVM>();

            using (SqlConnection conn = new SqlConnection(SystemFormat.SqlConnectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetUnservedItemReport", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StartDate", filter.StartDate);
                cmd.Parameters.AddWithValue("@EndDate", filter.EndDate);
                cmd.Parameters.AddWithValue("@CategoryId", filter.CategoryId);
                cmd.Parameters.AddWithValue("@ItemTypeId", filter.ItemTypeId);
                cmd.Parameters.AddWithValue("@BrandId", filter.BrandId);

                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    UnservedItemVM unservedItemVM = new UnservedItemVM
                    {
                        Agent = rdr["Agent"].ToString() ?? string.Empty,
                        CreatedBy = rdr["CreatedBy"].ToString() ?? string.Empty,
                        Customer = rdr["Customer"].ToString() ?? string.Empty,
                        Discount1D = Convert.ToDecimal(rdr["Discount1"]),
                        Discount2D = Convert.ToDecimal(rdr["Discount2"]),
                        Id = Convert.ToInt32(rdr["Id"]),
                        Item = rdr["Item"].ToString() ?? string.Empty,
                        OrderDateD = Convert.ToDateTime(rdr["OrderDate"]),
                        PackedQuantity = Convert.ToInt32(rdr["PackedQuantity"]),
                        Quantity = Convert.ToInt32(rdr["Quantity"]),
                        SalesOrderNumber = rdr["SalesOrderNumber"].ToString() ?? string.Empty,
                        UnitPriceD = Convert.ToDecimal(rdr["UnitPrice"])
                    };

                    unservedItemVMs.Add(unservedItemVM);
                }
                conn.Close();
            }
            return unservedItemVMs;
        }

        public ForDataTables<UnservedItemVM> GetUnservedItemDT(DataTablesRequestForm form, UnservedItemFilter filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;

            var data = GetUnservedItem(filter).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Agent.ToLower().Contains(form.SearchValue)
                                    || x.CreatedBy.ToLower().Contains(form.SearchValue)
                                    || x.Customer.ToLower().Contains(form.SearchValue)
                                    || x.Discount1.ToLower().Contains(form.SearchValue)
                                    || x.Discount2.ToLower().Contains(form.SearchValue)
                                    || x.Item.ToLower().Contains(form.SearchValue)
                                    || x.OrderDate.ToLower().Contains(form.SearchValue)
                                    || x.PackedQuantity.ToString().Contains(form.SearchValue)
                                    || x.Quantity.ToString().Contains(form.SearchValue)
                                    || x.SalesOrderNumber.ToLower().Contains(form.SearchValue)
                                    || x.UnitPrice.ToLower().Contains(form.SearchValue)
                                    || x.UnservedQuantity.ToString().Contains(form.SearchValue));
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
                        case "CreatedBy": data = data.OrderBy(s => s.CreatedBy); break;
                        case "Customer": data = data.OrderBy(s => s.Customer); break;
                        case "Discount1": data = data.OrderBy(s => s.Discount1D); break;
                        case "Discount2": data = data.OrderBy(s => s.Discount2D); break;
                        case "Item": data = data.OrderBy(s => s.Item); break;
                        case "OrderDate": data = data.OrderBy(s => s.OrderDateD); break;
                        case "PackedQuantity": data = data.OrderBy(s => s.PackedQuantity); break;
                        case "Quantity": data = data.OrderBy(s => s.Quantity); break;
                        case "SalesOrderNumber": data = data.OrderBy(s => s.SalesOrderNumber); break;
                        case "UnitPrice": data = data.OrderBy(s => s.UnitPriceD); break;
                        case "UnservedQuantity": data = data.OrderBy(s => s.UnservedQuantity); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Agent": data = data.OrderByDescending(s => s.Agent); break;
                        case "CreatedBy": data = data.OrderByDescending(s => s.CreatedBy); break;
                        case "Customer": data = data.OrderByDescending(s => s.Customer); break;
                        case "Discount1": data = data.OrderByDescending(s => s.Discount1D); break;
                        case "Discount2": data = data.OrderByDescending(s => s.Discount2D); break;
                        case "Item": data = data.OrderByDescending(s => s.Item); break;
                        case "OrderDate": data = data.OrderByDescending(s => s.OrderDateD); break;
                        case "PackedQuantity": data = data.OrderByDescending(s => s.PackedQuantity); break;
                        case "Quantity": data = data.OrderByDescending(s => s.Quantity); break;
                        case "SalesOrderNumber": data = data.OrderByDescending(s => s.SalesOrderNumber); break;
                        case "UnitPrice": data = data.OrderByDescending(s => s.UnitPriceD); break;
                        case "UnservedQuantity": data = data.OrderByDescending(s => s.UnservedQuantity); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<UnservedItemVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList
            };
        }
    }
}