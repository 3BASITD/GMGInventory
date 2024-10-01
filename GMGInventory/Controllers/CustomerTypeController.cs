using GMGInventory.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    public class CustomerTypeController(ICustomerTypeRepository customertypeRepository) : Controller
    {
        public async Task<IActionResult> GetOptions(int? IncludeId)
        {
            return Json(await customertypeRepository.GetOptions(IncludeId));
        }
    }
}