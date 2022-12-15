using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface IRegionRepository : IGenericRepository<Region>
    {
        Task<IList<RegionVM>> GetAllRegionVMAsync();
        Task<RegionVM?> GetRegionVMAsync(int? id);
        Task<IList<Region>> GetAllActiveRegionAsync(int includeId);
        Task<bool> IsRegionCodeExist(string code);
        Task<bool> IsRegionInUse(int id);
        Task<IList<JsonOptionsModel>> GetOptions(int? IncludeId);
        Task<int?> GetIdAsync(string? Name);
    }
}