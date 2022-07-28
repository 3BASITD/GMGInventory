using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<List<CustomerViewVM>> GetAllCustomerVMAsync();
        Task<CustomerViewVM?> GetCustomerViewVMAsync(int? id);
        Task<CustomerVM?> GetCustomerVMAsync(int? id);
        Task<List<Customer>> GetAllActiveCustomerAsync(int includeId);
        Task<bool> IsCustomerCodeExist(string code);
        Task<bool> IsCustomerInUse(int id);
        Task<bool> IsCustomerIdExistAsync(int id);
        Task DeactivateCustomerAsync(int id);
        Task ReactivateCustomerAsync(int id);
        Task AddAsync(CustomerVM customerVM);
        Task UpdateAsync(int id, CustomerVM customerVM);
    }
}