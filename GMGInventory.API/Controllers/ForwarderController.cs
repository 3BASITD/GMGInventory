using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ForwarderController(IForwarderRepository forwarderRepository) : ControllerBase
    {
        [HttpGet("List")]
        public async Task<IList<APIGeneric>> GetList()
        {
            return await forwarderRepository.GetListAPI();
        }
    }
}
