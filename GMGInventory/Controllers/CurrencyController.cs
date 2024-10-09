using GMGInventory.Application.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class CurrencyController(IAccessRepository accessRepository
                                  , ICurrencyRepository currencyRepository) : BaseController(accessRepository)
    {
        public async Task<IActionResult> GetOptions(int? IncludeId)
        {
            return Json(await currencyRepository.GetOptions(IncludeId));
        }
    }
}