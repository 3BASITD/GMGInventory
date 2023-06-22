using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<Select2Model<JsonAddressOptionsModel>> GetOptions(string search, int page);
        Task<Select2Model<JsonAddressOptionsModel>> GetByAgentOptions(string search, int page, int? agent);
        Task<Select2Model<JsonAddressOptionsModel>> GetOptionsWithOrder(string search, int page);

        Task<IList<JsonOptionsModel>> GetOptions2(int? IncludeId);

        //Task<IList<CustomerViewVM>> GetAllCustomerVMAsync();
        //Task<CustomerViewVM?> GetCustomerViewVMAsync(int? id);
        //Task<CustomerVM?> GetCustomerVMAsync(int? id);
        Task<IList<Customer>> GetAllActiveCustomerAsync(int includeId);
        Task<bool> IsCustomerCodeExist(string code);
        Task<bool> IsCustomerInUse(int id);
        Task<bool> IsCustomerIdExistAsync(int id);
        Task<bool> IsCodeExistAsync(string code);
        Task DeactivateCustomerAsync(int id);
        Task ReactivateCustomerAsync(int id);
        Task AddAsync(CustomerVM custVM);

        Task AddShippingAsync(CustomerVM shippingVM);
        //Task AddForwarderAsync(CustomerVM forwarderVM);
        Task UpdateAsync(CustomerVM custVM);

        //Task UpdateAsync(int id, CustomerVM customerVM);
        Task<ForDataTables<CustomerVM>> GetListDataTables(DataTablesRequestForm form);
        Task<IList<CustomerVM>> GetAllCustomerAsync();
        Task<bool> UploadCombinationExistAsync(CustomerVM customerVM);
        CustomerVM? GetCustomerDtlVM(int? id);
        Task<int?> GetIdAsync(string? Name);
        Task<ForDataTables<CustomerVM>> GetCustomerShippingDTAsync(DataTablesRequestForm form, int id);
        //Task<ForDataTables<CustomerVM>> GetCustomerForwarderDTAsync(DataTablesRequestForm form, int id);
        Task<ForDataTables<CustomerVM>> GetCustomerPaymentDTAsync(DataTablesRequestForm form, int id);
        Task<ForDataTables<CustomerVM>> GetCustomerReceivableDTAsync(DataTablesRequestForm form, int id);
        Task<ForDataTables<CustomerVM>> GetCustomerDetailsDTAsync(DataTablesRequestForm form, int id);
    }
}