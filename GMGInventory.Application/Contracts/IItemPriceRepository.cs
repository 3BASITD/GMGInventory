using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IItemPriceRepository
    {
        Task<ItemPriceVM?> GetAsync(int? id);
        IQueryable<ItemPriceVM> GetAllItemPrice(ItemFilterVM filter);
        Task<bool?> UpdateAsync(int? id, decimal retailPrice, decimal netPrice);
    }
}