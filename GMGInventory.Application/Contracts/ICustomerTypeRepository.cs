using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface ICustomerTypeRepository : IGenericRepository<CustomerType>
    {
        Task<List<CustomerTypeVM>> GetAllCustomerTypeVMAsync();
        Task<CustomerTypeVM?> GetCustomerTypeVMAsync(int? id);
        Task<List<CustomerType>> GetAllActiveCustomerTypeAsync(int includeId);
        Task<bool> IsCustomerTypeCodeExist(string code);
        Task<bool> IsCustomerTypeInUse(int id);
        Task<List<JsonOptionsModel>> GetOptions(int? IncludeId);
    }
}