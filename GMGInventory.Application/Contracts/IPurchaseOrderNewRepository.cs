using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IPurchaseOrderNewRepository
    {
        Task<IList<JsonCodeOptionsModel>> GetTypeOptions(int? SelectedId);
        Task<IList<JsonOptionsModel>> GetDeliveryModeOptions(int? SelectedId);
        Task<IList<JsonOptionsModel>> GetPurchaseRequestOptions(int OrderTypeId, int? SelectedId);
        Task<IList<JsonPurchaseItemOptionsModel>> GetPurchaseRequestItemOptions(int RequestId, int? SelectedId);
        Task<string> GetNewDocumentNumber(int? TypeId);
        Task<ForDataTables<PurchaseOrderNewVM>> GetListDTAsync(DataTablesRequestForm form);
        Task<PurchaseOrderNewDetailedVM?> GetDetails(int orderId);
        Task<IList<PurchaseOrderLineItemVM>> GetItemListAsync(int purchaseOrderId);
        Task<ForDataTables<PurchaseOrderLineItemVM>> GetItemListDTAsync(DataTablesRequestForm form, int purchaseRequestId);
        Task<ResponseModel<int>> AddAsync(PurchaseOrderSaveVM form, List<PurchaseOrderLineItemVM> Items);
        Task<ResponseModel> SaveItemsAsync(IList<PurchaseOrderLineItemVM> Items, int orderId, string? status = null);
        Task<ResponseModel> ApproveAsync(int orderId);
        Task<ResponseModel> DisapproveAsync(int orderId, string? reason);
        Task<ResponseModel> DeleteAsync(int requestId);
    }
}