using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface IAreaRepository : IGenericRepository<Area>
    {
        Task<IList<AreaViewVM>> GetAllAsync();
        Task<ForDataTables<AreaViewVM>> GetAllForDataTablesAsync(DataTablesRequestForm form);
        Task<IList<AreaViewVM>> GetAllAreaVMAsync();
        Task<AreaViewVM?> GetAreaViewVMAsync(int? id);
        Task<AreaVM?> GetAreaVMAsync(int? id);
        Task<int?> GetIdAsync(string? Name);
        Task<IList<Area>> GetAllActiveAreaAsync(int includeId);
        Task<bool> IsAreaCodeExist(string code);
        Task<bool> IsAreaInUse(int id);
        Task<bool> IsAreaIdExistAsync(int id);
        Task DeactivateAreaAsync(int id);
        Task ReactivateAreaAsync(int id);
        Task AddAsync(AreaVM areaVM);
        Task UpdateAsync(AreaVM areaVM);
        Task<IList<JsonOptionsModel>> GetOptions(int? IncludeId);
        Task<bool> UploadCombinationExistAsync(AreaVM areaVM);
        Task<bool> IsCodeExist(string code);
    }
}