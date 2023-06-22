using GMGInventory.Application.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class RegionController : Controller
    {
        private readonly IRegionRepository regionRepository;

        public RegionController(IRegionRepository regionRepository)
        {
            this.regionRepository = regionRepository;
        }

        public async Task<IActionResult> GetOptions(int? IncludeId)
        {
            return Json(await regionRepository.GetOptions(IncludeId));
        }
    }
}