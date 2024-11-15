using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IPurchaseOrderNewRepository
    {
        Task<IList<JsonOrderTypeOptionsModel>> GetTypeOptions(int? SelectedId);
        Task<IList<JsonOptionsModel>> GetDeliveryModeOptions(int? SelectedId);
        Task<IList<JsonOptionsModel>> GetPurchaseRequestOptions(int RequestTypeId, int? SelectedId);
        Task<IList<JsonRequestItemOptionsModel>> GetPurchaseRequestItemOptions(int RequestId, int? SelectedId);
        Task<string> GetNewDocumentNumber(int? TypeId);
        Task<ForDataTables<PurchaseOrderNewVM>> GetListDTAsync(DataTablesRequestForm form);
    }
}