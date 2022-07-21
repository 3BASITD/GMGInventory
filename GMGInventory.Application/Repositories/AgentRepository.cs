using AutoMapper;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Application.Repositories
{
    public class AgentRepository : GenericRepository<Agent>, IAgentRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public AgentRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
            , UserManager<User> userManager
            , IMapper mapper) : base(context, httpContextAccessor, userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<List<AgentVM>> GetAllAgentVMAsync()
        {
            var agents = await context.Agents.ToListAsync();
            var agentsVM = mapper.Map<List<AgentVM>>(agents);
            foreach (AgentVM avm in agentsVM)
            {
                avm.UserCreated = await GetUserName(avm.UserCreated);
                avm.UserModified = await GetUserName(avm.UserModified);
            }
            return agentsVM;
        }

        public async Task<AgentVM?> GetAgentVMAsync(int? id)
        {
            var agents = await context.Agents.Where(s => s.Id == id).FirstOrDefaultAsync();
            if (agents == null)
            {
                return null;
            }
            var agentsVM = mapper.Map<AgentVM>(agents);
            agentsVM.UserCreated = await GetUserName(agentsVM.UserCreated);
            agentsVM.UserModified = await GetUserName(agentsVM.UserModified);
            return agentsVM;
        }

        public async Task<List<Agent>> GetAllActiveAgentAsync(int includeId)
        {
            return await context.Agents.Where(s => s.IsActive || s.Id == includeId).ToListAsync();
        }

        public async Task<bool> IsAgentCodeExist(string code)
        {
            return await context.Agents.AnyAsync(c => c.Code == code);
        }

        public async Task<bool> IsAgentInUse(int id)
        {
            return await context.Agents.AnyAsync(s => s.Id == id);
        }
    }
}