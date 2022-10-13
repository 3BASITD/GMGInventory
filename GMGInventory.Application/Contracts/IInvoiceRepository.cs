using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IInvoiceRepository
    {
        Task<Select2Model<JsonInvoiceOptionsModel>> GetOptions(string search, int page);
        Task<InvoiceDetailsVM?> GetDetailsAsync(int? Id);
        Task<ForDataTables<InvoiceVM>> GetListDTAsync(DataTablesRequestForm form, ListFilterCommonModel filter);
        Task<IList<InvoiceItemVM>> GetItemsAsync(int invoiceId);
        Task<ForDataTables<InvoiceItemVM>> GetItemsDTAsync(DataTablesRequestForm form, int invoiceId);
    }
}