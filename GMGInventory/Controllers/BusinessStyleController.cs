using GMGInventory.Application.Contracts;
using GMGInventory.Application.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    public class BusinessStyleController : Controller
    {
        private readonly IBusinessStyleRepository businessStyleRepository;

        public BusinessStyleController(IBusinessStyleRepository businessStyleRepository)
        {
            this.businessStyleRepository = businessStyleRepository;
        }

        public async Task<IActionResult> GetOptions(int? IncludeId)
        {
            return Json(await businessStyleRepository.GetOptions(IncludeId));
        }
    }
}
