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
        //Task AddAsync(TransferFormVM formVM);
    }
}