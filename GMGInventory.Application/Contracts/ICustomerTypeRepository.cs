using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface ICustomerTypeRepository : IGenericRepository<CustomerType>
    {
        Task<IList<CustomerTypeVM>> GetAllCustomerTypeVMAsync();
        Task<CustomerTypeVM?> GetCustomerTypeVMAsync(int? id);
        Task<IList<CustomerType>> GetAllActiveCustomerTypeAsync(int includeId);
        Task<bool> IsCustomerTypeCodeExist(string code);
        Task<bool> IsCustomerTypeInUse(int id);
        Task<IList<JsonOptionsModel>> GetOptions(int? IncludeId);
    }
}