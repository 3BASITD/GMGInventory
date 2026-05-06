using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController(IItemRepository itemRepository) : ControllerBase
    {
        [HttpGet("List")]
        public async Task<IList<APIItem>> GetList(int? ItemClassId, int? CategoryId, int? ItemTypeId, int? BrandId)
        {
            return await itemRepository.GetListAPI(ItemClassId, CategoryId, ItemTypeId, BrandId);
        }
    }
}
