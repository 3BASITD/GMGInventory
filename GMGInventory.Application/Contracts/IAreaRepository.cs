using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface IAreaRepository : IGenericRepository<Area>
    {
        Task<List<AreaVM>> GetAllAreaVMAsync();
        Task<AreaVM?> GetAreaVMAsync(int? id);
        Task<List<Area>> GetAllActiveAreaAsync(int includeId);
        Task<bool> IsAreaCodeExist(string code);
        Task<bool> IsAreaInUse(int id);
    }
}