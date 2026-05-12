using GMGInventory.Application.Contracts;
using GMGInventory.Application.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
   [Authorize]
    public class KYCController(IAccessRepository accessRepository) : BaseController(accessRepository)
    {

        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}