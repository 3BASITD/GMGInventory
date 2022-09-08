using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IPurchaseOrderRepository
    {
        Task<Select2Model<JsonPurchaseOrderOptionsModel>> GetOptions(string search, int page);
        Task<IList<JsonCurrencyOptionModel>> GetCurrencyOptions(int? IncludeId);
        Task<PurchaseOrderDetailsVM?> GetDetailsAsync(int? Id);
        Task<ForDataTables<PurchaseOrderVM>> GetListDTAsync(DataTablesRequestForm form);
        Task<IList<PurchaseOrderItemVM>> GetItemsAsync(int purchaseOrderId);
        Task<ForDataTables<PurchaseOrderItemVM>> GetItemsDTAsync(DataTablesRequestForm form, int purchaseOrderId);
        Task<int> SaveDraftAsync(PurchaseOrderFormVM form);
        Task<int> SaveReplenishAsync(PurchaseOrderFormVM form);
        Task SaveReplenishItemsAsync(IList<PurchaseOrderFormItemVM> Items, int PurchaseOrderId);
        Task<bool> DeleteDraftAsync(int purchaseOrderId);
        //Task AddAsync(TransferFormVM formVM);
    }
}