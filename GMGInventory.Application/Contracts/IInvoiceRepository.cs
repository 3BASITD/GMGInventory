using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IInvoiceRepository
    {
        Task<ForDataTables<InvoiceVM>> GetListDataTablesAsync(DataTablesRequestForm form, ListFilterCommonModel filter);
    }
}