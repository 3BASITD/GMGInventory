using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IPriceRepository
    {
        Task<PriceVM?> GetAsync(int? id);
        Task<List<PriceVM>> GetAllAsync(ItemFilterVM itemFilterVM);
    }
}