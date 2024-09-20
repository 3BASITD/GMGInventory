using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface ILocationCodeRepository : IGenericRepository<LocationCode>
    {
        Task<IList<LocationCodeVM>> GetAllLocationCodeAsync();
        Task<ForDataTables<LocationCodeVM>> GetAllForDataTablesAsync(DataTablesRequestForm form);
        Task<bool> IsLocationCodeInUse(int id);

        Task<bool> IsLocationCodeExist(string code);
        Task AddAsync(LocationCodeVM locationCodeVM);
        Task UpdateAsync(LocationCodeVM locationCodeVM);
    }
}