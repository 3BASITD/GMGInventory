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
        [Authorize]
        [HttpGet("List")]
        public async Task<IList<APIGenericActive>> GetList()
        {
            return await categoryRepository.GetListAPI();
        }
    }
}
