
using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface IPurchasePaymentTermRepository : IGenericRepository<PurchasePaymentTerm>
    { 
        Task<IList<JsonOptionsModel>> GetOptions(int? IncludeId);
    }
}
