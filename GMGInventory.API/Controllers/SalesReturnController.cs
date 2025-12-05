using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalesReturnController(ISalesReturnRepository salesReturnRepository) : ControllerBase
    {
        [HttpGet("List")]
        public async Task<IList<APISalesReturn>> GetList(DateTime? ReturnDate)
        {
            return await salesReturnRepository.GetListAPI(ReturnDate);
        }
    }
}
