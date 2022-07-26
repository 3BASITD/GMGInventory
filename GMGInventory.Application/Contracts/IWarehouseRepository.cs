using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IWarehouseRepository
    {
        Task<WarehouseViewVM?> GetAsync(int? id);
        Task<WarehouseFormVM?> GetToEditAsync(int? id);
        Task<List<WarehouseViewVM>> GetAllAsync();
        Task<WarehouseFormVM> AddAsync(WarehouseFormVM itemVM);
        Task<bool> IsIdExists(int id);
        Task<bool> IsCodeExists(string code);
        Task DeleteAsync(int id);
        Task UpdateAsync(WarehouseFormVM itemVM);
        Task ReactivateAsync(int id);
        Task DeactivateAsync(int id);
        Task<bool> IsInUse(int id);
    }
}