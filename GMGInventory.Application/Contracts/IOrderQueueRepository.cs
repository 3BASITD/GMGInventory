using GMGInventory.Common.Models;
using GMGInventory.Data;
using GMGInventory.Data.OrderAppDB;

namespace GMGInventory.Application.Contracts
{
    public interface IOrderQueueRepository
    {
        Task<ForDataTables<OrderQueueVM>> GetOrdersDT(DataTablesRequestForm form, string? Status, DateTime? DateFrom, DateTime? DateTo);
        Task<OrderQueueDetailsVM?> GetDetails(string OrderNumber);
        Task<ResponseModel> AddOrder(string OrderNumber, List<int> ItemIds);
        Task<ResponseModel> IgnoreOrder(string OrderNumber, List<int> ItemIds);
        Task<ResponseModel> MarkIgnoreOrder(string OrderNumber);
    }
}