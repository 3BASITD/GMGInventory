using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IPurchaseOrderNewRepository
    {
        Task<ForDataTables<PurchaseOrderNewVM>> GetListDTAsync(DataTablesRequestForm form);
    }
}