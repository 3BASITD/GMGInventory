using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface IBrandRepository : IGenericRepository<Brand>
    {
        Task<List<JsonOptionsModel>> GetOptions(int? IncludeId);
        Task<IList<BrandViewVM>> GetAllBrandVMAsync();
        Task<BrandViewVM?> GetBrandViewVMAsync(int? id);
        Task<ForDataTables<BrandViewVM>> GetListDataTables(DataTablesRequestForm form);
        //<BrandViewVM?> GetBrandViewVMAsync(int? id);
        Task<bool> UploadCombinationExistAsync(BrandVM brandVM);
        Task<BrandVM?> GetBrandVMAsync(int? id);
        Task<List<Brand>> GetAllActiveBrandAsync(int includeId);
        Task<bool> IsBrandCodeExist(string code);
        Task<bool> IsBrandInUse(int id);
        Task<bool> IsBrandIdExistAsync(int id);
        Task DeactivateBrandAsync(int id);
        Task ReactivateBrandAsync(int id);
        Task AddAsync(BrandVM brandVM);
        Task UpdateAsync(BrandVM brandVM);
    }
}