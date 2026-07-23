using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;

namespace GMGInventory.Application.Contracts
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<Select2Model<JsonCustomerSimpleOptionsModel>> GetOptions2Simple(string search, int page, bool ActiveOnly);
        Task<Select2Model<JsonCustomerSimpleOptionsModel>> GetOptions(string search, int page);
        Task<Select2Model<JsonCustomerOptionsModel>> GetByAgentOptions(string search, int page, int? agent);
        Task<decimal> GetOutstandingBalance(int CustomerId);
        Task<Select2Model<JsonCustomerOptionsModel>> GetOptionsWithOrder(string search, int page);

        Task<IList<JsonCustomerSimpleOptionsModel>> GetOptions2(int? IncludeId);

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

        //Task AddForwarderAsync(CustomerVM forwarderVM);
        Task UpdateAsync(CustomerVM custVM);

        //Task UpdateAsync(int id, CustomerVM customerVM);
        Task<ForDataTables<CustomerVM>> GetListDataTables(DataTablesRequestForm form);
        Task<IList<CustomerVM>> GetAllCustomerAsync();
        Task<IList<APICustomer>> GetListAPI();
        Task<bool> UploadCombinationExistAsync(CustomerVM customerVM);
        CustomerVM? GetCustomerDtlVM(int? id);
        Task<int?> GetIdAsync(string? Name);
        Task<int?> GetIdByCodeAsync(string? Code);

        Task<ForDataTables<CustomerShippingVM>> GetCustomerShippingDTAsync(DataTablesRequestForm form, int id);
        Task<ForDataTables<CustomerBrandDiscountVM>> GetCustomerBrandDiscountDTAsync(DataTablesRequestForm form, int id);
        Task<IList<CustomerBrandDiscountVM>> GetCustomerBrandDiscountAsync(int id);
        Task<IList<CustomerBrandDiscountDowloadVM>> GetCustomerBrandDiscountDownload(int id);
        Task<ResponseModel> SaveShippingAsync(CustomerShippingVM shippingVM);
        Task<ResponseModel> SaveDiscountAsync(CustomerBrandDiscountVM brandDiscountVM);
        Task<ResponseModel> SetDefaultShippingAsync(int Id);
        Task<ResponseModel> DeleteOrDeactivateShippingAsync(int Id);
        Task<ResponseModel> DeleteOrDeactivateDiscountAsync(int Id);
        Task<ResponseModel> ReactivateShippingAsync(int Id);
        Task<ResponseModel> ReactivateDiscountAsync(int Id);

        //Task<ForDataTables<CustomerVM>> GetCustomerForwarderDTAsync(DataTablesRequestForm form, int id);
        Task<ForDataTables<CustomerVM>> GetCustomerPaymentDTAsync(DataTablesRequestForm form, int id);
        Task<ForDataTables<CustomerVM>> GetCustomerReceivableDTAsync(DataTablesRequestForm form, int id);
        Task<ForDataTables<CustomerVM>> GetCustomerDetailsDTAsync(DataTablesRequestForm form, int id);
        Task<ResponseModelData<FileVM>> UploadFile(IFormFile file);
    }
}