using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
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