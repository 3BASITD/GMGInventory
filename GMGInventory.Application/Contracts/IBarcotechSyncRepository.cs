using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IBarcotechSyncRepository
    {
        Task<ForDataTables<BarcotechSyncVM>> GetBarcotechStatusListDTAsync(DataTablesRequestForm form, BarcotechSyncFilter filter);
    }
}