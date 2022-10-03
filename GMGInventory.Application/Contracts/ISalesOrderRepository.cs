using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface ISalesOrderRepository
    {
        Task<Select2Model<JsonPurchaseOrderOptionsModel>> GetOptions(string search, int page);
        Task<IList<JsonOptionsModel>> GetPriceTypeOptions(int? IncludeId);
        Task<IList<JsonOptionsModel>> GetDeliveryTypeOptions(int? IncludeId);
        Task<SalesOrderDetailsVM?> GetDetailsAsync(int? Id);
        Task<ForDataTables<SalesOrderVM>> GetListDTAsync(DataTablesRequestForm form);
        Task<IList<SalesOrderItemVM>> GetItemsAsync(int salesOrderId);
        Task<ForDataTables<SalesOrderItemVM>> GetItemsDTAsync(DataTablesRequestForm form, int salesOrderId);
        Task<int> SaveAsync(SalesOrderFormVM form);
        Task SaveItemsAsync(IList<SalesOrderFormItemVM> Items, int salesOrderId);
        Task<bool> ApproveAsync(int salesOrderId);
        Task<bool> DisapproveAsync(int salesOrderId);
    }
}