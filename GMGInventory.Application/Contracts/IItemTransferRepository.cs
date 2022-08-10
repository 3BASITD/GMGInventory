using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IItemTransferRepository
    {
        Task<IList<WarehouseTransferVM>> GetWarehouseTransfersAsync();
        Task<TransferVM?> GetTransferVMAsync(int? Id);
        Task<ForDataTables<WarehouseTransferVM>> GetWarehouseTransfersDataTablesAsync(DataTablesRequestForm form);
        Task<IList<ItemTransferVM>> GetItemTransfersAsync();
        Task<ForDataTables<ItemTransferVM>> GetItemTransfersDataTablesAsync(DataTablesRequestForm form, int? TransferId);
    }
}