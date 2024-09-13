using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface ISalesReturnRepository
    {
        Task<SalesReturnVM?> GetDetailsAsync(int? Id);
        Task<ForDataTables<SalesReturnVM>> GetListDTAsync(DataTablesRequestForm form);
        Task<IList<SalesReturnItemVM>> GetItemsAsync(int salesReturnId);
        Task<int> SaveAsync(SalesReturnFormVM form);
        Task<bool> IsNumberInUse(int Id, string Number);
        Task<int> UpdateAsync(SalesReturnUpdateVM form);
        Task<SalesReturnDetailPVM> PrintDetails(int returnId);
    }
}