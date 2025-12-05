using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BrandController(IBrandRepository brandRepository) : ControllerBase
    {
        [HttpGet("List")]
        public async Task<IList<APIBrand>> GetList(int? ItemTypeId)
        {
            return await brandRepository.GetListAPI(ItemTypeId);
        }
    }
}
