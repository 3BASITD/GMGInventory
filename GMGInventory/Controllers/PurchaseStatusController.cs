using GMGInventory.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    public class PurchaseStatusController(IAccessRepository accessRepository
                                      , IPurchaseStatusRepository statusRepository) : BaseController(accessRepository)
    {
        public async Task<IActionResult> GetOptions()
        {
            return Json(await statusRepository.GetOptionAsync());
        }

        public async Task<IActionResult> GetOptions2(string search, int page)
        {
            return Json(await statusRepository.GetOptions2(search, page));
        }
    }
}