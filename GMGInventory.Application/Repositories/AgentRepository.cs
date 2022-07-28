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

        public async Task<List<AgentViewVM>> GetAllAgentVMAsync()
        {
            var agents = await context.Agents.Include(s => s.Area).ToListAsync();
            var agentsViewVM = mapper.Map<List<AgentViewVM>>(agents);
            foreach (AgentViewVM ivm in agentsViewVM)
            {
                ivm.UserCreated = await GetUserName(ivm.UserCreated);
                ivm.UserModified = await GetUserName(ivm.UserModified);
            }
            return agentsViewVM;
        }

        public async Task<AgentViewVM?> GetAgentViewVMAsync(int? id)
        {
            var agents = await context.Agents
                .Where(s => s.Id == id)
                .Include(s => s.Area)
                .FirstOrDefaultAsync();
            if (agents == null)
            {
                return null;
            }
            var agentsViewVM = mapper.Map<AgentViewVM>(agents);
            agentsViewVM.UserCreated = await GetUserName(agentsViewVM.UserCreated);
            agentsViewVM.UserModified = await GetUserName(agentsViewVM.UserModified);
            return agentsViewVM;
        }

        public async Task<AgentVM?> GetAgentVMAsync(int? id)
        {
            var agents = await context.Agents.Include(s => s.Area).Where(s => s.Id == id).FirstOrDefaultAsync();
            if (agents == null)
            {
                return null;
            }
            var agentViewVM = mapper.Map<AgentVM>(agents);
            return agentViewVM;
        }

        public async Task<List<AgentVM>> GetAllActiveAgentAsync(int includeId)

        { 
            var agents =  await context.Agents.Where(s => s.IsActive || s.Id == includeId).ToListAsync();
            var agentViewVM = mapper.Map <List<AgentVM>>(agents);
            return agentViewVM;
        }

        public async Task<bool> IsAgentCodeExist(string code)
        {
            return await context.Agents.AnyAsync(c => c.Code == code);
        }

        public async Task<bool> IsAgentInUse(int id)
        {
            return await context.Areas.AnyAsync(s => s.Id == id);
        }

        public async Task<bool> IsAgentIdExistAsync(int id)
        {
            return await context.Agents.AnyAsync(s => s.Id == id);
        }

        public async Task DeactivateAgentAsync(int id)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var agents = await context.Agents.FirstAsync(s => s.Id == id);
            agents.IsActive = false;
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task ReactivateAgentAsync(int id)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var agents = await context.Agents.FirstAsync(s => s.Id == id);
            agents.IsActive = true;
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task AddAsync(AgentVM agentVM)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var agents = mapper.Map<Agent>(agentVM);
            agents.IsActive = true;
            await context.Agents.AddAsync(agents);
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task UpdateAsync(int id, AgentVM agentVM)
        {
            var agents = context.Agents.Where(s => s.Id == id).FirstOrDefault();
            if (agents == null)
            {
                throw new DbUpdateConcurrencyException();
            }
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            mapper.Map(agentVM, agents);
            context.Agents.Update(agents);
            await context.SaveChangesAsync(currentUserId);
        }
    }
}