using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DriverController(IDriverRepository driverRepository) : ControllerBase
    {
        [Authorize]
        [HttpGet("List")]
        public async Task<IList<APIDriver>> GetList()
        {
            return await driverRepository.GetListAPI();
        }
    }
}
