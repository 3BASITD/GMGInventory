using GMGInventory.Common.Models;
using GMGInventory.Data;
using GMGInventory.Data.OrderAppDB;

namespace GMGInventory.Application.Contracts
{
    public interface IOrderRecommendationRepository
    {
        Task<ForDataTables<OrderRecommendationVM>> GetOrderRecommendationsDT(DataTablesRequestForm form, OrderRecommendationFilterVM filter);
        Task<IList<OrderRecommendationHistoryVM>> GetPurchaseHistories();
        Task<IList<OrderRecommendationHistoryVM>> GetIssuanceHistories();
        Task<OrderRecommendationNewVM> AddRequestAsync(IList<OrderRecommendationNewItemsVM> recos);
    }
}