using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface IWarehouseRepository
    {
        Task<List<JsonOptionsModel>> GetOptions(int? IncludeId);
        Task<WarehouseViewVM?> GetAsync(int? id);
        Task<WarehouseFormVM?> GetToEditAsync(int? id);
        Task<List<WarehouseViewVM>> GetAllAsync();
        Task<WarehouseFormVM> AddAsync(WarehouseFormVM itemVM);
        Task<List<Warehouse>> GetAllActiveWarehouseAsync(int includeId);
        Task<bool> IsIdExists(int id);
        Task<bool> IsCodeExists(string code);
        Task DeleteAsync(int id);
        Task UpdateAsync(WarehouseFormVM itemVM);
        Task ReactivateAsync(int id);
        Task DeactivateAsync(int id);
        Task<bool> IsInUse(int id);
    }
}