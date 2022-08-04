using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IItemRepository
    {
        Task<List<JsonOptionsModel>> GetOptions(ItemFilterVM filter, int? IncludeId);
        IQueryable<ItemViewVM> GetAllItemVM(ItemFilterVM filter);
        ItemViewVM? GetItemViewVM(int? id);
        Task<ItemVM> AddAsync(ItemVM itemVM);
        Task<bool> Exists(int id);
        Task DeleteAsync(int id);
        Task UpdateAsync(ItemVM itemVM);
    }
}