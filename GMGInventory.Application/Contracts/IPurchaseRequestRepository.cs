using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IPurchaseRequestRepository
    {
        Task<ForDataTables<PurchaseRequestVM>> GetListDTAsync(DataTablesRequestForm form);
    }
}