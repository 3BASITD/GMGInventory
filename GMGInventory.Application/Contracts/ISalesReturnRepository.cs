using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface ISalesReturnRepository
    {
        Task<SalesReturnVM?> GetDetailsAsync(int? Id);
        Task<ForDataTables<SalesReturnVM>> GetListDTAsync(DataTablesRequestForm form);
        Task<IList<SalesReturnItemVM>> GetItemsAsync(int salesReturnId);
    }
}