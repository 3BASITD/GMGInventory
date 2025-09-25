using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GMGInventory.Controllers
{
    public class APIHMController(IAPIHM_Service apiHMService
                               , IOptions<APIHMSettings> apiHMOptions) : Controller()
    {
        public async Task<IActionResult> GetTaxes()
        {
            return Json(await apiHMService.GetTaxes(apiHMOptions.Value.Host, apiHMOptions.Value.User, apiHMOptions.Value.Password));
        }

        public async Task<IActionResult> GetCurrencies()
        {
            return Json(await apiHMService.GetCurrencies(apiHMOptions.Value.Host, apiHMOptions.Value.User, apiHMOptions.Value.Password));
        }
    }
}