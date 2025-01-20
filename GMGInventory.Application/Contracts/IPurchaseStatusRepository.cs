using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IPurchaseStatusRepository
    {
        Task<IList<JsonOptionsModel>> GetOptionAsync();
    }
}