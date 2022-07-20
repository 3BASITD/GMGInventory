using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IItemRepository
    {
        Task<ItemInformationVM?> GetInformationAsync(int? id);
        Task<List<ItemInformationVM>> GetAllInformationAsync();
        Task<ItemVM> AddAsync(ItemVM itemVM);
        Task<bool> Exists(int id);
        Task DeleteAsync(int id);
        Task UpdateAsync(ItemVM itemVM);
    }
}