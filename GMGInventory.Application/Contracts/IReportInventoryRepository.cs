using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface IReportInventoryRepository
    {
        Task<IList<BelowMaintainingRVM>> GetBelowMaintaining(ListFilterInventoryModel filter);
        Task<ForDataTables<BelowMaintainingRVM>> GetBelowMaintainingDT(DataTablesRequestForm form, ListFilterInventoryModel filter);
        Task<BelowMaintainingPVM> PrintBelowMaintaining(ListFilterInventoryModel filter);

        Task<IList<ReceivingRVM>> GetReceiving(ListFilterInventoryModel filter);
        Task<ForDataTables<ReceivingRVM>> GetReceivingDT(DataTablesRequestForm form, ListFilterInventoryModel filter);
        Task<ReceivingPVM> PrintReceiving(ListFilterInventoryModel filter);

        Task<IList<ReceivingSupplierRVM>> GetReceivingSupplier(ListFilterInventoryModel filter);
        Task<ForDataTables<ReceivingSupplierRVM>> GetReceivingSupplierDT(DataTablesRequestForm form, ListFilterInventoryModel filter);
        Task<ReceivingSupplierPVM> PrintReceivingSupplier(ListFilterInventoryModel filter);

        Task<IList<ReceivingSummaryRVM>> GetReceivingSummary(ListFilterInventoryModel filter);
        Task<ForDataTables<ReceivingSummaryRVM>> GetReceivingSummaryDT(DataTablesRequestForm form, ListFilterInventoryModel filter);
        Task<ReceivingSummaryPVM> PrintReceivingSummary(ListFilterInventoryModel filter);

        Task<IList<StockCardRVM>> GetStockCard(ListFilterInventoryModel filter);
        Task<ForDataTables<StockCardRVM>> GetStockCardDT(DataTablesRequestForm form, ListFilterInventoryModel filter);
        Task<StockCardPVM> PrintStockCard(ListFilterInventoryModel filter);

        Task<IList<StockStatusRVM>> GetStockStatus(ListFilterInventoryModel filter);
        Task<ForDataTables<StockStatusRVM>> GetStockStatusDT(DataTablesRequestForm form, ListFilterInventoryModel filter);
        Task<StockStatusPVM> PrintStockStatus(ListFilterInventoryModel filter);

        Task<IList<SupplierPurchaseRVM>> GetSupplierPurchase(ListFilterInventoryModel filter);
        Task<ForDataTables<SupplierPurchaseRVM>> GetSupplierPurchaseDT(DataTablesRequestForm form, ListFilterInventoryModel filter);
        Task<SupplierPurchasePVM> PrintSupplierPurchase(ListFilterInventoryModel filter);
    }
}