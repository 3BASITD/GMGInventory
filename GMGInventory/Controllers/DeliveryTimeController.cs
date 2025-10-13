using GMGInventory.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    public class DeliveryTimeController(IDeliveryTimeRepository timeRepository) : Controller
    {
        public async Task<IActionResult> GetOptions(int? SelectedId)
        {
            return Json(await timeRepository.GetOptionAsync(SelectedId));
        }
    }
}