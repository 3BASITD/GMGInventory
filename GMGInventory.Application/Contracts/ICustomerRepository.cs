using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        //Task<List<CustomerViewVM>> GetAllCustomerVMAsync();
        //Task<CustomerViewVM?> GetCustomerViewVMAsync(int? id);
        //Task<CustomerVM?> GetCustomerVMAsync(int? id);
        Task<List<Customer>> GetAllActiveCustomerAsync(int includeId);
        Task<bool> IsCustomerCodeExist(string code);
        Task<bool> IsCustomerInUse(int id);
        Task<bool> IsCustomerIdExistAsync(int id);
        Task<bool> IsCodeExistAsync(string code);
        Task DeactivateCustomerAsync(int id);
        Task ReactivateCustomerAsync(int id);
        Task AddAsync(CustomerVM custVM);

        Task AddShippingAsync(CustomerVM shippingVM);
        Task AddForwarderAsync(CustomerVM forwarderVM);
        Task UpdateAsync(CustomerVM custVM);

        //Task UpdateAsync(int id, CustomerVM customerVM);
        ForDataTables<CustomerVM> GetListDataTables(DataTablesRequestForm form);
        CustomerVM? GetCustomerDtlVM(int? id);
        Task<ForDataTables<CustomerVM>> GetCustomerShippingDTAsync(DataTablesRequestForm form, int id);
        Task<ForDataTables<CustomerVM>> GetCustomerForwarderDTAsync(DataTablesRequestForm form, int id);
        Task<ForDataTables<CustomerVM>> GetCustomerPaymentDTAsync(DataTablesRequestForm form, int id);
        Task<ForDataTables<CustomerVM>> GetCustomerReceivableDTAsync(DataTablesRequestForm form, int id);
        Task<ForDataTables<CustomerVM>> GetCustomerDetailsDTAsync(DataTablesRequestForm form, int id);
    }
}