using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IPurchaseStatusRepository
    {
        Task<IList<JsonOptionsModel>> GetOptionAsync();
        Task<Select2Model<JsonOptionsModel>> GetOptions2(string search, int page);
    }
}