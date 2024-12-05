using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface ICustomerAccountStatusRepository
    {
        Task<IList<JsonOptionsModel>> GetOptions(int? IncludeId);
    }
}