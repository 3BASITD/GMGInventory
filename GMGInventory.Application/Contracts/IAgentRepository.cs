using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface IAgentRepository : IGenericRepository<Agent>
    {
        Task<List<AgentVM>> GetAllAgentVMAsync();
        Task<AgentVM?> GetAgentVMAsync(int? id);
        Task<List<Agent>> GetAllActiveAgentAsync(int includeId);
        Task<bool> IsAgentCodeExist(string code);
        Task<bool> IsAgentInUse(int id);
    }
}