using GMGInventory.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    public class CountryController(ICountryRepository countryRepository) : Controller
    {
        public async Task<IActionResult> GetOptions(int? IncludeId)
        {
            return Json(await countryRepository.GetOptions(IncludeId));
        }
        public async Task<IActionResult> GetOptions2(string search, int page)
        {
            return Json(await countryRepository.GetOptions2(search, page));
        }
    }
}