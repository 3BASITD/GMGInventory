using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;

namespace GMGInventory.Application.Contracts
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<IList<JsonOptionsModel>> GetOptions(int? IncludeId);
        Task<int?> GetIdAsync(string? Name);
        Task<IList<GenericAuditVM>> GetAllGenericVMAsync();
        Task<GenericAuditVM?> GetGenericVMAsync(int? id);
        Task<CategoryVM?> GetCategoryVMAsync(int? id);
        Task<IList<Category>> GetAllActiveCategoryAsync(int includeId);
        Task<bool> IsCategoryCodeExist(string code);
        Task<bool> IsCategoryInUse(int id);
        Task AddAsync(CategoryVM categoryVM);
        Task UpdateAsync(CategoryVM categoryVM);
        Task<bool> UploadCombinationExistAsync(CategoryVM categoryVM);
        Task<bool> IsCodeExist(string code);
        Task<IList<CategoryVM>> GetAllCategoryAsync();
        Task<ForDataTables<CategoryVM>> GetAllForDataTablesAsync(DataTablesRequestForm form);
        Task<IList<APIGeneric>> GetListAPI();
        Task<ResponseModelData<FileVM>> UploadFile(IFormFile file);
        Task<string> SaveItemFromFile(CategoryFileVM categoryVM);
    }
}