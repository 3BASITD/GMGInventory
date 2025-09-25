using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface ICurrencyRepository : IGenericRepository<Currency>
    {
        Task<int?> GetIdAsync(string? Name);
        Task<IList<JsonCurrencyOptionModel>> GetOptions(int? IncludeId);
        Task<ForDataTables<Currency>> GetCurrenciesDT(DataTablesRequestForm form);
        Task<ResponseModel> UpdateList(IList<Currency> currencies);
        Task<IList<Currency>> GetAllActiveCurrencyAsync(int includeId);

    }
}