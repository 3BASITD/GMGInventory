using GMGInventory.Application.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class UnitController(IAccessRepository accessRepository
                             , IUnitRepository unitRepository) : BaseController(accessRepository)
    {
        public async Task<IActionResult> GetOptions(int? IncludeId)
        {
            return Json(await unitRepository.GetOptions(IncludeId));
        }

        public async Task<IActionResult> GetOptions2(string search, int page)
        {
            return Json(await unitRepository.GetOptions2(search, page));
        }
    }
}
