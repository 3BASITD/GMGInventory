using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController(ICategoryRepository categoryRepository) : ControllerBase
    {
        [HttpGet("List")]
        public async Task<IList<APIGeneric>> GetList()
        {
            return await categoryRepository.GetListAPI();
        }
    }
}
