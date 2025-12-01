using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PurchaseOrderController(IPurchaseOrderNewRepository purchaseOrderRepository) : ControllerBase
    {
        [HttpGet("List")]
        public async Task<IList<APIPurchaseOrder>> GetList(DateTime? OrderDate)
        {
            return await purchaseOrderRepository.GetListAPI(OrderDate);
        }
    }
}
