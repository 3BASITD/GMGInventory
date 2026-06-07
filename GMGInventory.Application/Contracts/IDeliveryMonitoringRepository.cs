using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IDeliveryMonitoringRepository
    {
        Task<IList<JsonNameOptionsModel>> GetStatusOptions(int? IncludeId);
        Task<Select2Model<JsonNameOptionsModel>> GetStatusOptions2(string search, int page);
        Task<IList<JsonNameOptionsModel>> GetMethodOptions(int? IncludeId);
        Task<Select2Model<JsonNameOptionsModel>> GetMethodOptions2(string search, int page);
        Task<IList<DeliveryMonitoringVM>> GetListAsync(DeliveryMonitoringFilterVM filter);
        Task<ForDataTables<DeliveryMonitoringVM>> GetListDTAsync(DataTablesRequestForm form, DeliveryMonitoringFilterVM filter);
        Task<ResponseModel> SaveDeliveryAsync(DeliveryMonitoringFormVM form);
        Task<IList<DeliveryMonitoringChartVM>> GetLatestCounts();
    }
}