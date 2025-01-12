using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface ISalesOrderRepository
    {
        Task<Select2Model<JsonPurchaseOrderOptionsModel>> GetOptions(string search, int page);
        Task<Select2Model<JsonSalesOrderOptionsModel>> GetSalesOrderOptions(string search, int page);
        Task<IList<JsonOptionsModel>> GetPriceTypeOptions(int? IncludeId);
        Task<SalesOrderDetailsVM?> GetDetailsAsync(int? Id);
        Task<SalesOrderPrintVM?> PrintDetailsAsync(int? Id);
        Task<ForDataTables<SalesOrderVM>> GetListDTAsync(DataTablesRequestForm form, SalesOrderFilterVM filter);
        Task<IList<SalesOrderVM>> GetListAsync(SalesOrderFilterVM filter);
        Task<IList<SalesOrderItemVM>> GetItemsAsync(int salesOrderId, bool forPack = false);
        Task<ForDataTables<SalesOrderItemVM>> GetItemsDTAsync(DataTablesRequestForm form, int salesOrderId);
        Task<ResponseModel<int>> SaveAsync(SalesOrderFormVM form);
        Task SaveItemsAsync(IList<SalesOrderFormItemVM> Items, int salesOrderId);
        Task<bool> ApproveAsync(int salesOrderId);
        Task<bool> DisapproveAsync(int salesOrderId, string reason);
        Task<bool> UpdateServedSOItem(int salesOrderLineItemId, int quantity);
    }
}