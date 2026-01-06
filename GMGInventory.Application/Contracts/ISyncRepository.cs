using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface ISyncRepository
    {
        Task<ResponseModel> SaveBarcotechStatus(BarcotechSync sync);
        Task<ForDataTables<BarcotechSyncVM>> GetBarcotechStatusListDTAsync(DataTablesRequestForm form);
    }
}