using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IItemTransferRepository
    {
        Task<IList<WarehouseTransferVM>> GetAllTransferAsync(ListFilterCommonModel filter);
        Task<TransferVM?> GetTransferVMAsync(int? Id);
        Task<ForDataTables<WarehouseTransferVM>> GetWarehouseTransfersDTAsync(DataTablesRequestForm form, ListFilterCommonModel filter);
        Task<ForDataTables<WarehouseTransferVM>> GetWarehouseTransfersDTAsync(DataTablesRequestForm form, int WarehouseId, ListFilterCommonModel filter);
        Task<ForDataTables<ItemTransferVM>> GetItemTransfersDTAsync(DataTablesRequestForm form, int? TransferId);
        Task<int> AddAsync(TransferFormVM formVM);
    }
}