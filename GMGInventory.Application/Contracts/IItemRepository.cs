using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IItemRepository
    {
        Task<ItemViewVM?> GetAsync(int? id);
        Task<List<ItemViewVM>> GetAllAsync(ItemFilterVM itemFilterVM);
        Task<ItemVM> AddAsync(ItemVM itemVM);
        Task<bool> Exists(int id);
        Task DeleteAsync(int id);
        Task UpdateAsync(ItemVM itemVM);
    }
}