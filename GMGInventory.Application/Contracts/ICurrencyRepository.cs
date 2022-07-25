using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface ICurrencyRepository : IGenericRepository<Currency>

    {
        Task<List<CurrencyVM>> GetAllCurrencyVMAsync();
        Task<CurrencyVM?> GetCurrencyVMAsync(int? id);
        Task<List<Currency>> GetAllActiveCurrencyAsync(int includeId);
        Task<bool> IsCurrencyCodeExist(string code);
        Task<bool> IsCurrencyInUse(int id);
    }
}