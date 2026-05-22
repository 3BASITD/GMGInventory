using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IWarehouseDashboardRepository
    {
        Task<IList<OrderFulfillmentVM>> GetOrderFulfillments(DateTime dt, bool IncludeDone);
        Task<IncomingOrderVM> GetIncomingOrders();
    }
}