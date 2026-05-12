using GMGInventory.Application.Contracts;
using GMGInventory.Application.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
   [Authorize]
    public class ItemClassController(IAccessRepository accessRepository
                                   , IItemClassRepository itemTypeRepository) : BaseController(accessRepository)
    {
        public async Task<IActionResult> GetOptions(int? IncludeId)
        {
            return Json(await itemTypeRepository.GetOptions(IncludeId));
        }

        public async Task<IActionResult> GetOptions2(string search, int page)
        {
            return Json(await itemTypeRepository.GetOptions2(search, page));
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}