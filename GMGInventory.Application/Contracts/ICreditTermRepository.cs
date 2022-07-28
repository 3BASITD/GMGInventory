using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface ICreditTermRepository : IGenericRepository<CreditTerm>
    {
        Task<List<CreditTermVM>> GetAllCreditTermVMAsync();
        Task<CreditTermVM?> GetCreditTermVMAsync(int? id);
        Task<List<CreditTerm>> GetAllActiveCreditTermAsync(int includeId);
        Task<bool> IsCreditTermCodeExist(int code);
        Task<bool> IsCreditTermInUse(int id);
    }
}