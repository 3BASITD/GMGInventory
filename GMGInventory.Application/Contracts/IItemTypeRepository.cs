using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface IItemTypeRepository : IGenericRepository<ItemType>
    {
        Task<List<ItemTypeViewVM>> GetAllItemTypeVMAsync();
        Task<ItemTypeViewVM?> GetItemTypeViewVMAsync(int? id);
        Task<ItemTypeVM?> GetItemTypeVMAsync(int? id);
        Task<List<ItemType>> GetAllActiveItemTypeAsync(int includeId);
        Task<bool> IsItemTypeCodeExist(string code);
        Task<bool> IsItemTypeInUse(int id);
        Task<bool> IsItemTypeIdExistAsync(int id);
        Task DeactivateItemTypeAsync(int id);
        Task ReactivateItemTypeAsync(int id);
        Task AddAsync(ItemTypeVM itemTypeVM);
        Task UpdateAsync(int id, ItemTypeVM itemTypeVM);
    }
}