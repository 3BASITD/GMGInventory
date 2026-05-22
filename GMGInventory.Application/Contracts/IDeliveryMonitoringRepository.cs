using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IDeliveryMonitoringRepository
    {
        Task<ForDataTables<DeliveryMonitoringVM>> GetListDTAsync(DataTablesRequestForm form, DeliveryMonitoringFilterVM filter);
    }
}