using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface IReportSalesRepository
    {
        Task<IList<AgentSalesPerItemRVM>> GetAgentSalesPerItem(ListFilterSalesModel filter);
        Task<ForDataTables<AgentSalesPerItemRVM>> GetAgentSalesPerItemDT(DataTablesRequestForm form, ListFilterSalesModel filter);
        Task<AgentSalesPerItemPVM> PrintAgentSalesPerItem(ListFilterSalesModel filter);

        Task<IList<BrandSalesRVM>> GetBrandSales(ListFilterSalesModel filter);
        Task<ForDataTables<BrandSalesRVM>> GetBrandSalesDT(DataTablesRequestForm form, ListFilterSalesModel filter);
        Task<BrandSalesPVM> PrintBrandSales(ListFilterSalesModel filter);

        Task<IList<BestSellingItemRVM>> GetBestSellingItem(ListFilterSalesModel filter);
        Task<ForDataTables<BestSellingItemRVM>> GetBestSellingItemDT(DataTablesRequestForm form, ListFilterSalesModel filter);
        Task<BestSellingItemPVM> PrintBestSellingItem(ListFilterSalesModel filter);

        Task<IList<CommissionRVM>> GetCommission(ListFilterSalesModel filter);
        Task<ForDataTables<CommissionRVM>> GetCommissionDT(DataTablesRequestForm form, ListFilterSalesModel filter);
        Task<CommissionPVM> PrintCommission(ListFilterSalesModel filter);

        Task<IList<CustomerSalesRankingRVM>> GetCustomerSalesRanking (ListFilterSalesModel filter);
        Task<ForDataTables<CustomerSalesRankingRVM>> GetCustomerSalesRankingDT(DataTablesRequestForm form, ListFilterSalesModel filter);
        Task<CustomerSalesRankingPVM> PrintCustomerSalesRanking(ListFilterSalesModel filter);

        Task<IList<CustomerSalesRVM>> GetCustomerSales(ListFilterSalesModel filter);
        Task<ForDataTables<CustomerSalesRVM>> GetCustomerSalesDT(DataTablesRequestForm form, ListFilterSalesModel filter);
        Task<CustomerSalesPVM> PrintCustomerSales(ListFilterSalesModel filter);

        Task<IList<DailySalesRVM>> GetDailySales(ListFilterSalesModel filter);
        Task<ForDataTables<DailySalesRVM>> GetDailySalesDT(DataTablesRequestForm form, ListFilterSalesModel filter);
        Task<DailySalesPVM> PrintDailySales(ListFilterSalesModel filter);

        Task<IList<SalesAgentAreaRVM>> GetSalesAgentArea(ListFilterSalesModel filter);
        Task<ForDataTables<SalesAgentAreaRVM>> GetSalesAgentAreaDT(DataTablesRequestForm form, ListFilterSalesModel filter);
        Task<SalesAgentAreaPVM> PrintSalesAgentArea(ListFilterSalesModel filter);

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