using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IItemPriceRepository
    {
        Task<ItemPriceVM?> GetAsync(int? id);
        Task<List<ItemPriceVM>> GetAllAsync(ItemFilterVM itemFilterVM);
    }
}