using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface ICurrencyRepository : IGenericRepository<Currency>

    {
        Task<IList<CurrencyVM>> GetAllCurrencyVMAsync();
        Task<CurrencyVM?> GetCurrencyVMAsync(int? id);
        Task<IList<Currency>> GetAllActiveCurrencyAsync(int includeId);
        Task<bool> IsCurrencyCodeExist(string code);
        Task<bool> IsCurrencyInUse(int id);
        Task<IList<JsonCurrencyOptionModel>> GetOptions(int? IncludeId);
        Task<int?> GetIdAsync(string? Name);

    }
}