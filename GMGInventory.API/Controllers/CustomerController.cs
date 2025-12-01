using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController(ICustomerRepository customerRepository) : ControllerBase
    {
        [HttpGet("List")]
        public async Task<IList<APIGeneric>> GetList()
        {
            return await customerRepository.GetListAPI();
        }
    }
}
