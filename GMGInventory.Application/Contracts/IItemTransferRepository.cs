using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IItemTransferRepository
    {
        Task<TransferVM?> GetTransferVMAsync(int? Id);
        Task<ForDataTables<WarehouseTransferVM>> GetWarehouseTransfersDTAsync(DataTablesRequestForm form);
        Task<ForDataTables<ItemTransferVM>> GetItemTransfersDTAsync(DataTablesRequestForm form, int? TransferId);
        Task AddAsync(TransferFormVM formVM);
    }
}