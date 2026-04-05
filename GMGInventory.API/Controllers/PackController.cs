using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PackController(IPackRepository packRepository) : ControllerBase
    {
        [HttpGet("List")]
        public async Task<IList<APIPack>> GetList(DateTime? date)
        {
            return await packRepository.GetListAPI(date);
        }
    }
}
