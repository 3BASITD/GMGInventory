using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface IPurchaseRequestRepository
    {
        Task<IList<JsonCodeOptionsModel>> GetTypeOptions(int? SelectedId);
        Task<IList<JsonCodeOptionsModel>> GetPurposeOptions(int TypeId, int? SelectedId);
        Task<IList<PurchaseRequestVM>> GetListAsync(PurchaseRequestFilter filter);
        Task<ForDataTables<PurchaseRequestVM>> GetListDTAsync(DataTablesRequestForm form, PurchaseRequestFilter filter);
        Task<PurchaseRequestDetailedVM?> GetDetails(int requestId);
        Task<IList<PurchaseRequestLineItemVM>> GetItemListAsync(int purchaseRequestId);
        Task<ForDataTables<PurchaseRequestLineItemVM>> GetItemListDTAsync(DataTablesRequestForm form, int purchaseRequestId);
        Task<ResponseModel<int>> AddAsync(PurchaseRequestSaveVM form, List<PurchaseRequestLineItemVM> Items);
        Task<ResponseModel> SaveItemsAsync(IList<PurchaseRequestLineItemVM> Items, int requestId, string? status = null);
        Task<ResponseModel> ApproveAsync(int requestId);
        Task<ResponseModel> DisapproveAsync(int requestId, string? reason);
        Task<ResponseModel> DeleteAsync(int requestId);
        Task<string?> GetPurchaseRequestType(int? Id);
    }
}