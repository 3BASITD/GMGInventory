using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface IBrandRepository : IGenericRepository<Brand>
    {
        Task<IList<BrandViewVM>> GetListAsync();
        Task<ForDataTables<BrandViewVM>> GetListDTAsync(DataTablesRequestForm form);
        Task<IList<APIBrand>> GetListAPI(int? ItemTypeId); 
        Task<BrandViewVM?> GetBrandViewVM(int? id);
        Task<bool> UploadCombinationExistAsync(BrandVM brandVM);
        Task<BrandVM?> GetBrandVMAsync(int? id);
        Task<IList<Brand>> GetAllActiveBrandAsync(int includeId);
        Task<bool> IsBrandCodeExist(string code);
        Task<bool> IsBrandInUse(int id);
        Task<bool> IsBrandIdExistAsync(int id);
        Task DeactivateBrandAsync(int id);
        Task ReactivateBrandAsync(int id);
        Task AddAsync(BrandVM brandVM);
        Task UpdateAsync(BrandVM brandVM);
        Task<IList<JsonNameOptionsModel>> GetOptions(int? IncludeId);
        Task<int?> GetIdAsync(string? Name);
    }
}
