using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface IItemTypeRepository : IGenericRepository<ItemType>
    {
        Task<List<ItemTypeViewVM>> GetAllItemTypeVMAsync();
        Task<ItemTypeViewVM?> GetItemTypeVMAsync(int? id);
        Task<List<ItemType>> GetAllActiveItemTypeAsync(int includeId);
        Task<bool> IsItemTypeCodeExist(string code);
        Task<bool> IsItemTypeInUse(int id);
    }
}