using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IWarehouseDashboardRepository
    {
        Task<OrderFulfillmentVM> GetOrderFulfillments(DateTime dt, bool IncludeDone);
        Task<IncomingOrderVM> GetIncomingOrders();
    }
}