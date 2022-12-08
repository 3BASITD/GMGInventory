using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface IForwarderRepository : IGenericRepository<Forwarder>
    {
        Task<IList<JsonOptionsModel>> GetOptions(int? IncludeId);
        Task<IList<ForwarderViewVM>> GetAllForwarderVMAsync();
        Task<ForwarderViewVM?> GetForwarderViewVMAsync(int? id);
        Task<ForwarderVM?> GetForwarderVMAsync(int? id);
        Task<IList<Forwarder>> GetAllActiveForwarderAsync(int includeId);
        Task<bool> IsForwarderCodeExist(string code);
        Task<bool> IsForwarderInUse(int id);
        Task<bool> IsForwarderIdExistAsync(int id);
        Task DeactivateForwarderAsync(int id);
        Task ReactivateForwarderAsync(int id);
        Task AddAsync(ForwarderVM forwarderVM);
        Task UpdateAsync(ForwarderVM forwarderVM);

        Task<IList<ForwarderViewVM>> GetAllAsync();
        Task<bool> UploadCombinationExistAsync(ForwarderVM forwarderVM);
        Task<ForDataTables<ForwarderViewVM>> GetAllForDataTablesAsync(DataTablesRequestForm form);
    }
}