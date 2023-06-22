using GMGInventory.Application.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class CustomerTypeController : Controller
    {
        private readonly ICustomerTypeRepository customertypeRepository;

        public CustomerTypeController(ICustomerTypeRepository customertypeRepository)
        {
            this.customertypeRepository = customertypeRepository;
        }

        public async Task<IActionResult> GetOptions(int? IncludeId)
        {
            return Json(await customertypeRepository.GetOptions(IncludeId));
        }
    }
}