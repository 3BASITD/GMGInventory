using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface ISupplierRepository : IGenericRepository<Supplier>
    {
        Task<List<JsonSupplierOptionsModel>> GetOptions(int? IncludeId);
        Task<List<SupplierViewVM>> GetAllSupplierVMAsync();
        Task<SupplierViewVM?> GetSupplierViewVMAsync(int? id);
        Task<SupplierVM?> GetSupplierVMAsync(int? id);
        Task<List<Supplier>> GetAllActiveSupplierAsync(int includeId);
        Task<bool> IsSupplierCodeExist(string code);
        Task<bool> IsSupplierInUse(int id);
        Task<bool> IsSupplierIdExistAsync(int id);
        Task DeactivateSupplierAsync(int id);
        Task ReactivateSupplierAsync(int id);
        Task AddAsync(SupplierVM supplierVM);
        Task UpdateAsync(int id, SupplierVM supplierVM);
    }
}