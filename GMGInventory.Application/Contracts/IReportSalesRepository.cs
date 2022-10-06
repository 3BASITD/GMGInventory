using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface IReportSalesRepository
    {
        IList<int> GetAgentSalesPerItems();
        IList<int> GetBrandSales();
        IList<int> GetBestSellingItem();
        IList<int> GetBestSellingItemInvoice();
        IList<int> GetCommission();
        IList<int> GetCustomerSalesAnalytics();
        IList<int> GetCustomerSales();
        IList<int> GetDailySalesReport();
        IList<int> GetDailySalesMargin();
        IList<int> GetSalesInvoiceListing();
        IList<int> GetSalesByAgentArea();
        IList<int> GetSalesMarginItem();
        IList<int> GetSalesReportBasis();
        IList<int> GetSalesReturnListing();
        IList<UnservedItemVM> GetUnservedItem(UnservedItemFilter filter);
        ForDataTables<UnservedItemVM> GetUnservedItemDT(DataTablesRequestForm form, UnservedItemFilter filter);
    }
}