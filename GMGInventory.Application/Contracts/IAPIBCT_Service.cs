using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IAPIBCT_Service
    {
        Task<IList<BarcotechStockSyncVM>> SyncStock(int? ItemId);
        Task<IList<BarcotechPickSyncVM>> SyncPick();
    }
}