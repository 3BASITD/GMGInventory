using GMGInventory.Application.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class CreditTermController(IAccessRepository accessRepository
                                    , ICreditTermRepository creditTermRepository) : BaseController(accessRepository)
    {

        public async Task<IActionResult> GetOptions(int? IncludeId)
        {
            return Json(await creditTermRepository.GetOptions(IncludeId));
        }
    }
}