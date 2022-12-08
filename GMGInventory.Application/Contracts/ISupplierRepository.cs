using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface ISupplierRepository : IGenericRepository<Supplier>
    {
        Task<IList<JsonSupplierOptionsModel>> GetOptions(int? IncludeId);
        Task<IList<SupplierViewVM>> GetAllSupplierVMAsync();
        Task<SupplierViewVM?> GetSupplierViewVMAsync(int? id);
        Task<SupplierVM?> GetSupplierVMAsync(int? id);
        Task<IList<Supplier>> GetAllActiveSupplierAsync(int includeId);
        Task<bool> IsSupplierCodeExist(string code);
        Task<bool> IsSupplierInUse(int id);
        Task<bool> IsSupplierIdExistAsync(int id);
        Task DeactivateSupplierAsync(int id);
        Task ReactivateSupplierAsync(int id);
        Task AddAsync(SupplierVM supplierVM);
        Task UpdateAsync(SupplierVM supplierVM);
        Task<bool> IsCodeExist(string code);
        Task<bool> UploadCombinationExistAsync(SupplierVM supplierVM);
        Task<IList<SupplierViewVM>> GetAllAsync();
        Task<ForDataTables<SupplierViewVM>> GetAllForDataTablesAsync(DataTablesRequestForm form);

    }
}