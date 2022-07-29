using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<List<JsonOptionsModel>> GetOptions(int? IncludeId);
        Task<int?> GetIdAsync(string? Name);
        Task<List<GenericAuditVM>> GetAllGenericVMAsync();
        Task<GenericAuditVM?> GetGenericVMAsync(int? id);
        Task<CategoryVM?> GetCategoryVMAsync(int? id);
        Task<List<Category>> GetAllActiveCategoryAsync(int includeId);
        Task<bool> IsCategoryCodeExist(string code);
        Task<bool> IsCategoryInUse(int id);
        Task AddAsync(CategoryVM categoryVM);
        Task UpdateAsync(int id, CategoryVM categoryVM);
    }
}