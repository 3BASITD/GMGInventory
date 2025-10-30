using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BrandController(IBrandRepository brandRepository) : ControllerBase
    {
        [Authorize]
        [HttpGet("List")]
        public async Task<IList<APIBrand>> GetList()
        {
            return await brandRepository.GetListAPI();
        }
    }
}
