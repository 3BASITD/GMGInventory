using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IUnservedItemRepository
    {
        ForDataTables<UnservedItemVM> GetListDataTables(DataTablesRequestForm form);
    }
}