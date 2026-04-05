using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IPurchaseOrderNewRepository
    {
        Task<Select2Model<JsonPurchaseOrderOptionsModel>> GetOptions(string search, int page);
        Task<IList<JsonOrderTypeOptionsModel>> GetTypeOptions(int? SelectedId);
        Task<Select2Model<JsonOrderTypeOptionsModel>> GetTypeOptions2(string search, int page);
        Task<IList<JsonOptionsModel>> GetDeliveryModeOptions(int? SelectedId);
        Task<Select2Model<JsonOptionsModel>> GetDeliveryModeOptions2(string search, int page);
        Task<Select2Model<JsonPurchaseOptionModel>> GetPurchaseRequestOptions(string search, int page, int RequestTypeId, int? RequestPurposeId, int? RegionId);
        Task<IList<JsonPurchaseItemOptionsModel>> GetPurchaseRequestItemOptions(int RequestId, int? SelectedId);
        Task<string> GetNewDocumentNumber(int? TypeId);
        Task<IList<PurchaseOrderNewVM>> GetListAsync(PurchaseOrderFilter filter);
        Task<ForDataTables<PurchaseOrderNewVM>> GetListDTAsync(DataTablesRequestForm form, PurchaseOrderFilter filter);
        Task<IList<ItemFromPurchaseRequestVM>> GetPurchaseRequestItems(int PurchaseRequestId);
        Task<IList<APIPurchaseOrder>> GetListAPI(DateTime? dt);
        Task<PurchaseOrderNewDetailedVM?> GetDetails(int orderId);
        Task<IList<PurchaseOrderLineItemVM>> GetItemListAsync(int purchaseOrderId);
        Task<ForDataTables<PurchaseOrderLineItemVM>> GetItemListDTAsync(DataTablesRequestForm form, int purchaseRequestId);
        Task<ResponseModel<int>> AddAsync(PurchaseOrderSaveVM form, IList<PurchaseOrderLineItemVM> Items);
        Task<ResponseModel> SaveItemsAsync(IList<PurchaseOrderLineItemVM> Items, int orderId, string? status = null);
        Task<ResponseModel> UpdateCostAsync(POItemCostVM cost);
        Task<ResponseModel> UpdateQuantityAsync(POItemQuantityVM qty);
        Task<IList<POItemCostVM>> GetCostUpdateListAsync(int orderItemId);
        Task<IList<POItemQuantityVM>> GetQuantityUpdateListAsync(int orderItemId);
        Task<ResponseModel> ApproveAsync(int orderId);
        Task<ResponseModel> DisapproveAsync(int orderId, string? reason);
        Task<ResponseModel> CancelAsync(int orderId);
        Task<ResponseModel> DeleteAsync(int orderId);
    }
}