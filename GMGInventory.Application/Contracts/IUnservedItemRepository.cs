using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IUnservedItemRepository
    {
        Task<IList<UnservedItemVM>> GetListAsync();
        Task<ForDataTables<UnservedItemVM>> GetListDataTablesAsync(DataTablesRequestForm form);
    }
}