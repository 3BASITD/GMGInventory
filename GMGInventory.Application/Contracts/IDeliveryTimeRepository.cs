using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IDeliveryTimeRepository
    {
        Task<IList<JsonOptionsModel>> GetOptionAsync(int? SelectedId);
    }
}