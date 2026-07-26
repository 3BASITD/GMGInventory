using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface ICustomerPurchaseRepository
    {
        Task<IList<CustomerPurchaseVM>> GetListAsync(CustomerPurchaseFilterVM filter);
        Task<ForDataTables<CustomerPurchaseVM>> GetListDataTablesAsync(DataTablesRequestForm form, CustomerPurchaseFilterVM filter);
    }
}