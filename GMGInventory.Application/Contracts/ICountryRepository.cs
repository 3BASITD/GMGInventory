using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface ICountryRepository
    {
        Task<int?> GetIdAsync(string? Name);
        Task<Select2Model<JsonCountryOptionsModel>> GetOptions2(string search, int page);
        Task<IList<JsonCountryOptionsModel>> GetOptions(int? IncludeId);
    }
}