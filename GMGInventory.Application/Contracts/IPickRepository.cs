using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IPickRepository
    {
        Task<ForDataTables<PickVM>> GetListDTAsync(DataTablesRequestForm form);
    }
}