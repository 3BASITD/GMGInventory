using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IItemPriceRepository
    {
        Task<ItemPriceVM?> GetAsync(int? id);
        Task<IList<ItemPriceVM>> GetAllItemPriceAsync(ItemFilterVM filter);
        Task<ForDataTables<ItemPriceVM>> GetAllItemPriceDataTablesAsync(DataTablesRequestForm form, ItemFilterVM filter);
        Task<ItemPricePVM> GetAllItemPricePrintAsync(ItemFilterVM filter);
        Task<IList<ItemPriceUpdateVM>> GetAllItemPriceUpdateAsync(ItemFilterVM filter);
        Task<ForDataTables<ItemPriceUpdateVM>> GetAllItemPriceUpdateDataTablesAsync(DataTablesRequestForm form, ItemFilterVM filter);
        Task<bool?> UpdateAsync(int? id, decimal retailPrice, decimal netPrice);
    }
}