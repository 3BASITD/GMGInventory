using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface IPurchaseRequestRepository
    {
        Task<IList<JsonCodeOptionsModel>> GetTypeOptions(int? SelectedId);
        Task<Select2Model<JsonCodeOptionsModel>> GetTypeOptions2(string search, int page);
        Task<IList<JsonCodeOptionsModel>> GetPurposeOptions(int TypeId, int? SelectedId);
        Task<Select2Model<JsonCodeOptionsModel>> GetPurposeOptions2(string search, int page, int? TypeId);
        Task<IList<PurchaseRequestVM>> GetListAsync(PurchaseRequestFilter filter);
        Task<ForDataTables<PurchaseRequestVM>> GetListDTAsync(DataTablesRequestForm form, PurchaseRequestFilter filter);
        Task<PurchaseRequestDetailedVM?> GetDetails(int requestId);
        Task<IList<PurchaseRequestLineItemVM>> GetItemListAsync(int purchaseRequestId);
        Task<ForDataTables<PurchaseRequestLineItemVM>> GetItemListDTAsync(DataTablesRequestForm form, int purchaseRequestId);
        Task<IList<ItemFromSalesOrderVM>> GetSalesOrderItems(int SalesOrderId);
        Task<ResponseModel<int>> AddAsync(PurchaseRequestSaveVM form, List<PurchaseRequestLineItemVM> Items);
        Task<ResponseModel> SaveItemsAsync(IList<PurchaseRequestLineItemVM> Items, int requestId, string? status = null);
        Task<ResponseModel> ApproveAsync(int requestId, int? approverNumber = null);
        Task<ResponseModel> DisapproveAsync(int requestId, string? reason);
        Task<ResponseModel> CancelAsync(int requestId);
        Task<ResponseModel> DeleteAsync(int requestId);
        Task<string?> GetPurchaseRequestType(int? Id);
    }
}