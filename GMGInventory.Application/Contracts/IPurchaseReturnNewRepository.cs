using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IPurchaseReturnNewRepository
    {
        Task<IList<JsonOptionsModel>> GetPurchaseReceivingOptions(int? SelectedId);
        Task<IList<JsonPurchaseItemOptionsModel>> GetPurchaseReceiveItemOptions(int ReceiveId, int? SelectedId);
        Task<IList<PurchaseReturnNewLineItemVM>> GetPurchaseReceiveItemList(int ReceiveId);
        Task<string> GetNewDocumentNumber();
        Task<IList<PurchaseReturnNewVM>> GetListAsync(PurchaseReturnFilter filter);
        Task<ForDataTables<PurchaseReturnNewVM>> GetListDTAsync(DataTablesRequestForm form, PurchaseReturnFilter filter);
        Task<IList<APIPurchaseReturn>> GetListAPI();
        Task<PurchaseReturnNewDetailedVM?> GetDetails(int returnId);
        Task<IList<PurchaseReturnNewLineItemVM>> GetItemsFromRRListAsync(int purchaseReceivingId);
        Task<IList<PurchaseReturnNewLineItemVM>> GetWithRRItemsListAsync(int purchaseReturnId);
        Task<IList<PurchaseReturnNewLineItemVM>> GetItemListAsync(int purchaseReturnId);

        Task<ForDataTables<PurchaseReturnNewLineItemVM>> GetItemListDTAsync(DataTablesRequestForm form, int purchaseReturnId);
        Task<ResponseModel<int>> AddAsync(PurchaseReturnNewSaveVM form, List<PurchaseReturnNewLineItemVM> Items);
        Task<ResponseModel> SaveItemsAsync(IList<PurchaseReturnNewLineItemVM> Items, int returnId, string? status = null);
        Task<ResponseModel> ApproveAsync(int returnId);
        Task<ResponseModel> DisapproveAsync(int returnId, string? reason);
        Task<ResponseModel> DeleteAsync(int returnId);
    }
}