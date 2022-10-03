using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface IItemTypeRepository : IGenericRepository<ItemType>
    {
        Task<IList<JsonOptionsModel>> GetOptions(int? IncludeId);
        Task<int?> GetIdAsync(string? Name);
        Task<IList<ItemTypeViewVM>> GetListAsync();
        Task<ForDataTables<ItemTypeViewVM>> GetListDTAsync(DataTablesRequestForm form);
        Task<ItemTypeViewVM?> GetItemTypeViewVM(int? id);
        Task<bool> UploadCombinationExistAsync(ItemTypeVM itemTypeVM);
        Task<ItemTypeVM?> GetItemTypeVMAsync(int? id);
        Task<IList<ItemType>> GetAllActiveItemTypeAsync(int includeId);
        Task<bool> IsCodeExist(string code);
        Task<bool> IsItemTypeInUse(int id);
        Task<bool> IsItemTypeIdExistAsync(int id);
        Task DeactivateItemTypeAsync(int id);
        Task ReactivateItemTypeAsync(int id);
        Task AddAsync(ItemTypeVM itemTypeVM);
        Task UpdateAsync(ItemTypeVM itemTypeVM);

    }
}