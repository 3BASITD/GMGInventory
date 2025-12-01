using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemTypeController(IItemTypeRepository itemTypeRepository) : ControllerBase
    {
        [HttpGet("List")]
        public async Task<IList<APIItemType>> GetList(int? CategoryId)
        {
            return await itemTypeRepository.GetListAPI(CategoryId);
        }
    }
}
