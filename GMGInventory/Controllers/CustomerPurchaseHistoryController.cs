using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    public class CustomerPurchaseHistoryController(IAccessRepository accessRepository
                                                 , ICustomerPurchaseRepository customerPurchaseRepository) : BaseController(accessRepository)
    {
        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.CustomerPurchaseHistory, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetListJson(CustomerPurchaseFilterVM filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.CustomerPurchaseHistory, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            var form = new DataTablesRequestForm(Request.Form);
            return Json(await customerPurchaseRepository.GetListDataTablesAsync(form, filter));
        }
    }
}