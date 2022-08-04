using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface IItemTypeRepository : IGenericRepository<ItemType>
    {
        Task<List<JsonOptionsModel>> GetOptions(int CategoryId, int? IncludeId);
        IQueryable<ItemTypeViewVM> GetAllItemTypeVM();
        ForDataTables<ItemTypeViewVM> GetListDataTables(DataTablesRequestForm form);
        ItemTypeViewVM? GetItemTypeViewVM(int? id);
        Task<bool> UploadCombinationExistAsync(ItemTypeVM itemTypeVM);
        Task<ItemTypeVM?> GetItemTypeVMAsync(int? id);
        Task<List<ItemType>> GetAllActiveItemTypeAsync(int includeId);
        Task<bool> IsItemTypeCodeExist(string code);
        Task<bool> IsItemTypeInUse(int id);
        Task<bool> IsItemTypeIdExistAsync(int id);
        Task DeactivateItemTypeAsync(int id);
        Task ReactivateItemTypeAsync(int id);
        Task AddAsync(ItemTypeVM itemTypeVM);
        Task UpdateAsync(ItemTypeVM itemTypeVM);
        Task<int?> GetIdAsync(string? Name);
        Task<List<JsonOptionsModel>> GetOptionsCreate(int? IncludeId);
        Task<List<JsonOptionsModel>> GetOptionsEdit(int? IncludeId);

    }
}