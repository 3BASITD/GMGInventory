using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface IStockRepository
    {
        Task<ForDataTables<StockVM>> GetStocksDT(DataTablesRequestForm form);

    }
}