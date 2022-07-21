using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface ISupplierRepository : IGenericRepository<Supplier>
    {
        Task<List<SupplierVM>> GetAllSupplierVMAsync();
        Task<SupplierVM?> GetSupplierVMAsync(int? id);
        Task<List<Supplier>> GetAllActiveSupplierAsync(int includeId);
        Task<bool> IsSupplierCodeExist(string code);
        Task<bool> IsSupplierInUse(int id);
    }
}