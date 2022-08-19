using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface IAreaRepository : IGenericRepository<Area>
    {

        Task<List<AreaViewVM>> GetAllAreaVMAsync();
        Task<AreaViewVM?> GetAreaViewVMAsync(int? id);
        Task<AreaVM?> GetAreaVMAsync(int? id);
        Task<List<Area>> GetAllActiveAreaAsync(int includeId);
        Task<bool> IsAreaCodeExist(string code);
        Task<bool> IsAreaInUse(int id);
        Task<bool> IsAreaIdExistAsync(int id);
        Task DeactivateAreaAsync(int id);
        Task ReactivateAreaAsync(int id);
        Task AddAsync(AreaVM areaVM);
        Task UpdateAsync(int id, AreaVM areaVM);
        Task<List<JsonOptionsModel>> GetOptions(int? IncludeId);
    }
}