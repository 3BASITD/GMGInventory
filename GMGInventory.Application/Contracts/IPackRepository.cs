using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IPackRepository
    {
        Task<ForDataTables<PackVM>> GetListDataTablesAsync(DataTablesRequestForm form, int? statusId);
    }
}