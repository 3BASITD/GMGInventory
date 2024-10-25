using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IPurchaseReturnNewRepository
    {
        Task<ForDataTables<PurchaseReturnNewVM>> GetListDTAsync(DataTablesRequestForm form);
    }
}