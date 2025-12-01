using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalesOrderController(ISalesOrderRepository salesOrderRepository) : ControllerBase
    {
        [HttpGet("List")]
        public async Task<IList<APISalesOrder>> GetList(DateTime? OrderDate)
        {
            return await salesOrderRepository.GetListAPI(OrderDate);
        }
    }
}
