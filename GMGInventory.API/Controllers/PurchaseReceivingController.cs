using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PurchaseReceivingController(IPurchaseReceiveRepository receiveRepository) : ControllerBase
    {

        [Authorize]
        [HttpPost("Add")]
        public async Task<ResponseModelData<int>> AddPack([FromBody] PurchaseReceiveFormAPIVM form)
        {
            return await receiveRepository.AddAPI(form);
        }
    }
}
