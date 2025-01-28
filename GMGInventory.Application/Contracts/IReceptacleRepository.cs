using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IReceptacleRepository
    {
        Task<ForDataTables<ReceptacleVM>> GetListDTAsync(DataTablesRequestForm form, ReceptacleFilterVM filter);
        Task<ReceptacleDetailVM?> GetDetailsAsync(int Id);
        Task<ReceptacleDetailVM?> GetPrintDetailsAsync(int Id, int BoxNumber);
        Task SaveReceptacleHistory(int PackId, int BoxNumber, int? ShippingAddressId, string CompanyAs);
        Task<ForDataTables<ReceptaclePrintHistoryVM>> GetReceptaclePrintHistoriesDTAsync(DataTablesRequestForm form, int packId);
    }
}