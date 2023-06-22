using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class CustomerPurchaseHistoryController : Controller
    {
        private readonly ICustomerPurchaseRepository customerPurchaseRepository;

        public CustomerPurchaseHistoryController(ICustomerPurchaseRepository customerPurchaseRepository)
        {
            this.customerPurchaseRepository = customerPurchaseRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetListJson(CustomerPurchaseFilterVM filter)
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await customerPurchaseRepository.GetListDataTablesAsync(form, filter));
        }
    }
}