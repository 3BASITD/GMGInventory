using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface ISalesOrderDeliveryRepository
    {
        Task<IList<SalesOrderDeliveryVM>> GetListAsync(SalesOrderDeliveryFilterVM filter);
        Task<ForDataTables<SalesOrderDeliveryVM>> GetListDataTablesAsync(DataTablesRequestForm form, SalesOrderDeliveryFilterVM filter);
        Task<string> EditAsync(SalesOrderDeliveryFormVM form);
    }
}