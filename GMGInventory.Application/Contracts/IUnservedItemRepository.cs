using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IUnservedItemRepository
    {
        Task<IList<UnservedItemVM>> GetListAsync(UnservedItemFilter filter);
        Task<ForDataTables<UnservedItemVM>> GetListDataTablesAsync(DataTablesRequestForm form, UnservedItemFilter filter);
    }
}