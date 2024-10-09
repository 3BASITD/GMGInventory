using GMGInventory.Application.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class PurchaseOrderController(IAccessRepository accessRepository) : BaseController(accessRepository)
    {
        public IActionResult Index()
        {
            return View("~/Views/Shared/Temp.cshtml");

        }
    }
}