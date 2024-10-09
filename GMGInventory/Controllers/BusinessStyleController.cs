using GMGInventory.Application.Contracts;
using GMGInventory.Application.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class BusinessStyleController(IBusinessStyleRepository businessStyleRepository
                                       , IAccessRepository accessRepository) : BaseController(accessRepository)
    {
        public async Task<IActionResult> GetOptions(int? IncludeId)
        {
            return Json(await businessStyleRepository.GetOptions(IncludeId));
        }
    }
}
