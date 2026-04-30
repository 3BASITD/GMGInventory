using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.API.Controllers
{
    [Route("[controller]")]
    public class WarehouseController(IWarehouseRepository warehouseRepository) : ControllerBase
    {
        [HttpGet("List")]
        public async Task<IList<APIWarehouse>> GetList()
        {
            return await warehouseRepository.GetListAPI();
        }
    }
}
