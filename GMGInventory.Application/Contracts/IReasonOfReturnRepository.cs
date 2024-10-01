using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface IReasonOfReturnRepository : IGenericRepository<ReasonOfReturn>
    {
        Task<IList<ReasonOfReturnVM>> GetAllReasonOfReturnAsync();
        Task<ForDataTables<ReasonOfReturnVM>> GetAllForDataTablesAsync(DataTablesRequestForm form);

        Task<bool> IsReasonOfReturnInUse(int id);

        Task<bool> IsReasonOfReturnCodeExist(string code);
        Task AddAsync(ReasonOfReturnVM reasonOfReturnVM);
        Task UpdateAsync(ReasonOfReturnVM reasonOfReturnVM);
    }
}