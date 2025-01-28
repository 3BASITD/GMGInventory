using GMGInventory.Application.Contracts;
using GMGInventory.Application.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class ChequeStatusController(IAccessRepository accessRepository
                                      , IChequeStatusRepository statusRepository) : BaseController(accessRepository)
    {
        public async Task<IActionResult> GetOptions(int? IncludeId)
        {
            return Json(await statusRepository.GetOptions(IncludeId));
        }
    }
}