using GMGInventory.Application.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class PurchaseStatusController(IAccessRepository accessRepository
                                      , IPurchaseStatusRepository statusRepository) : BaseController(accessRepository)
    {
        public async Task<IActionResult> GetOptions()
        {
            return Json(await statusRepository.GetOptionAsync());
        }
    }
}