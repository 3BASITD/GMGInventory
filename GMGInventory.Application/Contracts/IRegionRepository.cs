using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface IRegionRepository : IGenericRepository<Region>
    {
        Task<IList<RegionVM>> GetAllRegionAsync();
        Task<RegionVM?> GetRegionVMAsync(int? id);
        Task<IList<Region>> GetAllActiveRegionAsync(int includeId);
        Task<bool> IsRegionCodeExist(string code);
        Task<bool> IsRegionInUse(int id);
        Task<IList<JsonOptionsModel>> GetOptions(int? IncludeId, bool IncludeLocal, bool IncludeInternational);
        Task<int?> GetIdAsync(string? Name);
        Task<ForDataTables<RegionVM>> GetAllForDataTablesAsync(DataTablesRequestForm form);
        Task AddAsync(RegionVM regionVM);
        Task UpdateAsync(RegionVM regionVM);
    }
}