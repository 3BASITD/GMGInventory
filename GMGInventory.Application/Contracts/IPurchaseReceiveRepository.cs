using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IPurchaseReceiveRepository
    {
        Task<ForDataTables<PurchaseReceiveVM>> GetListDTAsync(DataTablesRequestForm form);
        Task<PurchaseReceiveDetailsVM?> GetDetails(int receivingId);
        Task<IList<PurchaseReceiveLineItemVM>> GetItemListAsync(int purchaseRequestId);
        Task<ResponseModel> SaveItemsAsync(IList<PurchaseReceiveLineItemVM> Items, int receiveId, string? status = null);
        Task<ResponseModel<int>> AddAsync(PurchaseReceiveSaveVM form, List<PurchaseReceiveLineItemVM> Items);
        Task<IList<JsonOptionsModel>> GetPurchaseOrderOptions(int? SelectedId);
        Task<ResponseModel> ApproveAsync(int requestId);
        Task<ResponseModel> DisapproveAsync(int requestId, string? reason);
        Task<string> GetNewDocumentNumber();
        Task<IList<JsonPurchaseItemOptionsModel>> GetPurchaseOrderItemOptions(int PurchaseOrderId);
    }
}