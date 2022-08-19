using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface IRegionRepository : IGenericRepository<Region>
    {
        Task<List<RegionVM>> GetAllRegionVMAsync();
        Task<RegionVM?> GetRegionVMAsync(int? id);
        Task<List<Region>> GetAllActiveRegionAsync(int includeId);
        Task<bool> IsRegionCodeExist(string code);
        Task<bool> IsRegionInUse(int id);
        Task<List<JsonOptionsModel>> GetOptions(int? IncludeId);
    }
}