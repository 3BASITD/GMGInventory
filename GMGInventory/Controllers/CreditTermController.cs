using GMGInventory.Application.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class CreditTermController : Controller
    {
        private readonly ICreditTermRepository creditTermRepository;

        public CreditTermController(ICreditTermRepository creditTermRepository)
        {
            this.creditTermRepository = creditTermRepository;
        }

        public async Task<IActionResult> GetOptions(int? IncludeId)
        {
            return Json(await creditTermRepository.GetOptions(IncludeId));
        }
    }
}