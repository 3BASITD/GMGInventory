using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface IReportSalesRepository
    {
        Task<IList<SalesCustomerRVM>> GetCustomerSales(ListFilterSalesModel filter);
        Task<ForDataTables<SalesCustomerRVM>> GetCustomerSalesDT(DataTablesRequestForm form, ListFilterSalesModel filter);
        Task<SalesCustomerPVM> PrintCustomerSales(ListFilterSalesModel filter);

        IList<int> GetAgentSalesPerItems();
        IList<int> GetBrandSales();
        IList<int> GetBestSellingItem();
        IList<int> GetBestSellingItemInvoice();
        IList<int> GetCommission();
        IList<int> GetCustomerSalesAnalytics();
        IList<int> GetDailySalesReport();
        IList<int> GetDailySalesMargin();
        IList<int> GetSalesInvoiceListing();
        IList<int> GetSalesByAgentArea();
        IList<int> GetSalesMarginItem();
        IList<int> GetSalesReportBasis();
        IList<int> GetSalesReturnListing();
        Task<IList<UnservedItemRVM>> GetUnservedItem(ListFilterSalesModel filter);
        Task<ForDataTables<UnservedItemRVM>> GetUnservedItemDT(DataTablesRequestForm form, ListFilterSalesModel filter);
        Task<UnservedItemPVM> PrintUnservedItem(ListFilterSalesModel filter);
    }
}