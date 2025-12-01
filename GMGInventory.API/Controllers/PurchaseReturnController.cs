using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PurchaseReturnController(IPurchaseReturnNewRepository purchaseReturnRepository) : ControllerBase
    {
        [HttpGet("List")]
        public async Task<IList<APIPurchaseReturn>> GetList(DateTime? ReturnDate)
        {
            return await purchaseReturnRepository.GetListAPI(ReturnDate);
        }
    }
}
