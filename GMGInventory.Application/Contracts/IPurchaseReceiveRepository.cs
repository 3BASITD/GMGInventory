using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IPurchaseReceiveRepository
    {
        Task<ForDataTables<PurchaseReceiveVM>> GetListDTAsync(DataTablesRequestForm form);
    }
}