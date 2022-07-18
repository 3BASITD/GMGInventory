using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<List<GenericAuditVM>> GetAllCategoryVMAsync();
        Task<GenericAuditVM?> GetCategoryVMAsync(int? id);
        Task<List<Category>> GetAllActiveCategoryAsync(int includeId);
        Task<bool> IsCategoryCodeExist(string code);
        Task<bool> IsCategoryInUse(int id);
    }
}