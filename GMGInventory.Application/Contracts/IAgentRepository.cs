using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface IAgentRepository : IGenericRepository<Agent>
    {
        Task<List<AgentViewVM>> GetAllAgentVMAsync();
        Task<AgentViewVM?> GetAgentViewVMAsync(int? id);
        Task<AgentVM?> GetAgentVMAsync(int? id);
        Task<List<Agent>> GetAllActiveAgentAsync(int includeId);
        Task<bool> IsAgentCodeExist(string code);
        Task<bool> IsAgentInUse(int id);
        Task<bool> IsAgentIdExistAsync(int id);
        Task DeactivateAgentAsync(int id);
        Task ReactivateAgentAsync(int id);
        Task AddAsync(AgentVM agentVM);
        Task UpdateAsync(int id, AgentVM agentVM);
    }
}