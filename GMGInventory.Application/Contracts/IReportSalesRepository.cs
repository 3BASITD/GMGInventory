using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface IReportSalesRepository
    {
        List<int> GetAgentSalesPerItems();
        List<int> GetBrandSales();
        List<int> GetBestSellingItem();
        List<int> GetBestSellingItemInvoice();
        List<int> GetCommission();
        List<int> GetCustomerSalesAnalytics();
        List<int> GetCustomerSales();
        List<int> GetDailySalesReport();
        List<int> GetDailySalesMargin();
        List<int> GetSalesInvoiceListing();
        List<int> GetSalesByAgentArea();
        List<int> GetSalesMarginItem();
        List<int> GetSalesReportBasis();
        List<int> GetSalesReturnListing();
        List<UnservedItemVM> GetUnservedItem(UnservedItemFilter filter);
        ForDataTables<UnservedItemVM> GetUnservedItemDT(DataTablesRequestForm form, UnservedItemFilter filter);
    }
}