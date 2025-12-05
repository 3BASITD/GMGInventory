using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface IReasonOfReturnRepository : IGenericRepository<ReasonOfReturn>
    {
        Task<IList<JsonOptionsModel>> GetOptions(int? IncludeId);
        Task<IList<ReasonOfReturnVM>> GetAllReasonOfReturnAsync();
        Task<ForDataTables<ReasonOfReturnVM>> GetAllForDataTablesAsync(DataTablesRequestForm form);
        Task<IList<APIGeneric>> GetListAPI();
        Task<bool> IsReasonOfReturnInUse(int id);

        Task<bool> IsReasonOfReturnCodeExist(string code);
        Task AddAsync(ReasonOfReturnVM reasonOfReturnVM);
        Task UpdateAsync(ReasonOfReturnVM reasonOfReturnVM);
    }
}