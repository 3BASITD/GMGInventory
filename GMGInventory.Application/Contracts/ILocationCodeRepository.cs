using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface ILocationCodeRepository : IGenericRepository<LocationCode>
    {
        Task<IList<JsonNameOptionsModel>> GetOptions(int? IncludeId);
        Task<IList<JsonOptionsModel>> GetOptions2(int? IncludeId, int itemId);
        Task<IList<LocationCodeVM>> GetAllLocationCodeAsync();
        Task<ForDataTables<LocationCodeVM>> GetAllForDataTablesAsync(DataTablesRequestForm form);
        Task<IList<APIGeneric>> GetListAPI();
        Task<bool> IsLocationCodeInUse(int id);

        Task<bool> IsLocationCodeExist(string code);
        Task<int> AddAsync(LocationCodeVM locationCodeVM);
        Task UpdateAsync(LocationCodeVM locationCodeVM);
        Task<int?> GetIdAsync(string? Name);
    }
}