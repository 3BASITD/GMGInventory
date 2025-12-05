using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReasonOfReturnController(IReasonOfReturnRepository reasonRepository) : ControllerBase
    {
        [HttpGet("List")]
        public async Task<IList<APIGeneric>> GetList()
        {
            return await reasonRepository.GetListAPI();
        }
    }
}
