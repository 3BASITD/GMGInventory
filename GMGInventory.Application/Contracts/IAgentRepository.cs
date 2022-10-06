using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface IAgentRepository : IGenericRepository<Agent>
    {
        Task<Select2Model<JsonNameOptionsModel>> GetOptions2(string search, int page);
        Task<IList<JsonOptionsModel>> GetOptions(int? IncludeId);
        Task<int?> GetIdAsync(string? Name);
        Task<IList<AgentViewVM>> GetListAsync();
        Task<ForDataTables <AgentViewVM>> GetListDTAsync(DataTablesRequestForm form);

        Task<IList<AgentViewVM>> GetAllAsync(AgentFilterVM filter);
        Task<ForDataTables<AgentViewVM>> GetAllForDataTablesAsync(DataTablesRequestForm form, AgentFilterVM filter);
        Task<AgentViewVM?> GetAgentViewVM(int? id);
        Task<bool> UploadCombinationExistAsync(AgentVM agentVM);
        Task<AgentVM?> GetAgentVMAsync(int? id);
        Task<IList<Agent>> GetAllActiveAgentAsync(int includeId);
        Task<bool> IsCodeExist(string code);
        Task<bool> IsAgentInUse(int id);
        Task<bool> IsAgentIdExistAsync(int id);
        Task DeactivateAgentAsync(int id);
        Task ReactivateAgentAsync(int id);
        Task AddAsync(AgentVM agentVM);
        Task UpdateAsync(AgentVM agentVM);

    }
}