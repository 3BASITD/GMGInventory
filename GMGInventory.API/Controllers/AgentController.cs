using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AgentController(IAgentRepository agentRepository) : ControllerBase
    {
        [Authorize]
        [HttpGet("List")]
        public async Task<IList<APIAgent>> GetList()
        {
            return await agentRepository.GetListAPI();
        }
    }
}
