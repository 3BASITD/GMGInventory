using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;

namespace GMGInventory.Application.Contracts
{
    public interface IBrandRepository : IGenericRepository<Brand>
    {
        Task<IList<JsonNameOptionsModel>> GetOptions(int? IncludeId);
        Task<Select2Model<JsonNameOptionsModel>> GetOptions2(string search, int page);
        Task<IList<BrandViewVM>> GetListAsync();
        Task<ForDataTables<BrandViewVM>> GetListDTAsync(DataTablesRequestForm form);
        Task<IList<APIBrand>> GetListAPI(); 
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
        Task<int?> GetIdAsync(string? Name);
        Task<ResponseModelData<FileVM>> UploadFile(IFormFile file);
        Task<string> SaveItemFromFile(BrandFileVM brandVM);
    }
}
