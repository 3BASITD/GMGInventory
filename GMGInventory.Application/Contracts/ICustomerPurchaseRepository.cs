using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface ICustomerPurchaseRepository
    {
        Task<ForDataTables<CustomerPurchaseVM>> GetListDataTablesAsync(DataTablesRequestForm form, CustomerPurchaseFilterVM filter);
    }
}