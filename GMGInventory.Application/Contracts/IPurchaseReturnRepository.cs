using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IPurchaseReturnRepository
    {
        Task<IList<PurchaseReturnVM>> GetListAsync(int purchaseOrderId);
        Task AddAsync(PurchaseReturnFormVM formVM);
    }
}