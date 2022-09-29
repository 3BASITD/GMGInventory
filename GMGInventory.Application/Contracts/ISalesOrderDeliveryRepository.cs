using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface ISalesOrderDeliveryRepository
    {
        Task<ForDataTables<SalesOrderDeliveryVM>> GetListDataTablesAsync(DataTablesRequestForm form, SalesOrderDeliveryFilterVM filter);
        Task<string> EditAsync(SalesOrderDeliveryFormVM form);
    }
}