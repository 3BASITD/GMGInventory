using GMGInventory.Common.Models;
using GMGInventory.Data;
using GMGInventory.Data.OrderAppDB;

namespace GMGInventory.Application.Contracts
{
    public interface IOrderQueueRepository
    {
        Task<ForDataTables<OrderQueueVM>> GetOrdersDT(DataTablesRequestForm form);
        Task<OrderQueueDetailsVM?> GetDetails(string OrderNumber);
        Task<ResponseModel> AddOrder(string OrderNumber);
    }
}