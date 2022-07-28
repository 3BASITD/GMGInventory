using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IItemTransferRepository
    {
        IQueryable<ItemTransferHistoryVM> GetHistory(int WarehouseId);
    }
}