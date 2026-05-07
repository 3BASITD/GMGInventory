using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IItemClassRepository
    {
        Task<IList<JsonOptionsModel>> GetOptions(int? IncludeId);
        Task<Select2Model<JsonNameOptionsModel>> GetOptions2(string search, int page);
    }
}