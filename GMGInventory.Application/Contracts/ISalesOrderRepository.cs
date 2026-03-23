using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface ISalesOrderRepository
    {
        Task<Select2Model<JsonPurchaseOrderOptionsModel>> GetOptions(string search, int page);
        Task<Select2Model<JsonSalesOrderOptionsModel>> GetSalesOrderOptions(string search, int page);
        Task<Select2Model<JsonSalesOrderOptionsModel>> GetSalesOrderUnservedOnlyOptions(string search, int page, int? regionId);
        Task<Select2Model<JsonSalesOrderOptionsModel>> GetSalesOrderInvoicedOptions(string search, int page);
        Task<IList<JsonOptionsModel>> GetStatusOptions();
        Task<Select2Model<JsonOptionsModel>> GetStatusOptions2(string search, int page);
        Task<SalesOrderDetailsVM?> GetDetailsAsync(int? Id);
        Task<SalesOrderPrintVM?> PrintDetailsAsync(int? Id);
        Task<ForDataTables<SalesOrderVM>> GetListDTAsync(DataTablesRequestForm form, SalesOrderFilterVM filter);
        Task<IList<SalesOrderVM>> GetListAsync(SalesOrderFilterVM filter);
        Task<IList<APISalesOrder>> GetListAPI();
        Task<IList<SalesOrderItemVM>> GetItemsAsync(int salesOrderId);
        Task<IList<CustomerBrandDiscountVM>> GetBrandDiscountAsync(int customerId,int brandId);
        Task<ForDataTables<SalesOrderItemVM>> GetItemsDTAsync(DataTablesRequestForm form, int salesOrderId);
        Task<ResponseModel<int>> SaveAsync(SalesOrderFormVM form);
        Task SaveItemsAsync(IList<SalesOrderFormItemVM> Items, int salesOrderId);
        Task<ResponseModel> DeleteDraft(int Id);
        Task<bool> ApproveAsync(int salesOrderId);
        Task<bool> DisapproveAsync(int salesOrderId, string reason);
        Task<bool> UpdateServedSOItem(int salesOrderLineItemId, int quantity);
    }
}