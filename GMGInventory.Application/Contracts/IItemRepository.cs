using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IItemRepository
    {
        Task<Select2Model<JsonItemOptionsModel>> GetOptions(string search, int page);
        Task<Select2Model<JsonItemOptionsModel>> GetPOItemOptions(int id, string search);
        Task<Select2Model<JsonItemOptionsModel>> GetUnservedOptions(string search, int page, int SalesOrderId);
        Task<Select2Model<JsonOptionsModel>> GetModelNumberOptions(string search, int page);
        Task<Select2Model<JsonOptionsModel>> GetSizeOptions(string search, int page);
        Task<Select2Model<JsonItemOptionsModel>> GetInWarehouseOptions(string search, int warehouse, int page);
        Task<int?> GetIdAsync(string? Code);
        Task<IList<JsonOptionsModel>> GetUnitOptions(int? IncludeId);
        Task<int?> GetUnitIdAsync(string? Code);
        Task<int?> GetUnitIdByNameAsync(string? Name);
        Task<IList<ItemCommonVM>> GetAllAsync(ItemFilterVM filter);
        Task<ForDataTables<ItemCommonVM>> GetAllForDataTablesAsync(DataTablesRequestForm form, ItemFilterVM filter);
        ItemViewVM? GetItemViewVM(int? id);
        Task<IList<ItemFileVM>> GetAllFileAsync();
        Task<bool> IsCodeExistAsync(string code);
        Task<bool> IsInUseAsync(int id);
        Task<bool> IsIdExistAsync(int id);
        Task DeactivateAsync(int id);
        Task ReactivateAsync(int id);
        Task AddAsync(ItemCommonVM itemVM);
        Task UpdateAsync(ItemCommonVM itemVM);
        Task DeleteAsync(int id);
        Task UpdateQuantityAsync(int id, int quantity, string remarks);
        Task DeleteWarehouse(int id);
        Task<bool> UpdateOrAddItemWarehouse(int itemId, int warehouseId, int quantity, string remarks);
        Task UpdateOrAddItemPrice(int itemId, decimal retailPrice, decimal netPrice);
        Task<ForDataTables<ItemDetailWarehouseVM>> GetItemWarehouseDTAsync(DataTablesRequestForm form, int id);
        Task<ForDataTables<ItemDetailPurchaseVM>> GetItemPurchaseDTAsync(DataTablesRequestForm form, int id);
        Task<ForDataTables<ItemDetailSalesVM>> GetItemSalesDTAsync(DataTablesRequestForm form, int id);
        Task<ForDataTables<ItemDetailCountVM>> GetItemCountDTAsync(DataTablesRequestForm form, int id);
        Task<ForDataTables<ItemDetailPriceVM>> GetItemPriceDTAsync(DataTablesRequestForm form, int id);
        Task<ForDataTables<ItemDetailTransferVM>> GetItemTransferDTAsync(DataTablesRequestForm form, int id);
        Task<ForDataTables<ItemDetailReturnVM>> GetItemReturnDTAsync(DataTablesRequestForm form, int id);
    }
}