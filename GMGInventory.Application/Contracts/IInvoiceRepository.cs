using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IInvoiceRepository
    {
        Task<Select2Model<JsonInvoiceOptionsModel>> GetOptions(string search, int page);
        Task<Select2Model<JsonOptionsModel>> GetOptionsInvoiceNumber(string search, int page);
        Task<Select2Model<JsonOptionsModel>> GetOptionsBIRInvoiceNumber(string search, int page);
        Task<Select2Model<JsonNameOptionsModel>> GetInvoiceBySalesOrder(string search, int page, int? salesOrderId);
        Task<IList<JsonOptionsModel>> GetInvoiceTypeOptions(int? IncludeId);
        Task<InvoiceDetailsVM?> GetDetailsAsync(int? Id);
        Task<InvoiceDetailsWithItemsVM> GetDetailsWithItemsAsync(int Id);
        Task<InvoiceDetailsVM?> GetDetailsByPackAsync(int? Id);
        Task<ForDataTables<InvoiceVM>> GetListDTAsync(DataTablesRequestForm form, ListFilterCommonModel filter);
        Task<IList<InvoiceItemVM>> GetItemsAsync(int invoiceId);
        Task<ForDataTables<InvoiceItemVM>> GetItemsDTAsync(DataTablesRequestForm form, int invoiceId);
        Task<bool> IsBIRInvoiceNumberInUser(int? invoiceId, string BIRInvoiceNumber);
        Task<bool> IsInvoiceInUse(int? invoiceId);
        Task<int> SaveAsync(InvoiceFormVM form);
        Task<int> UpdateAsync(InvoiceFormVM form);
    }
}