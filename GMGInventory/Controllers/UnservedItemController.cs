using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    [Authorize(Roles = Roles.InventoryManager + ","
                     + Roles.SalesDirector + ","
                     + Roles.SystemAdmin + ","
                     + Roles.Telemarketer)]
    public class UnservedItemController(IAccessRepository accessRepository
                             , IUnservedItemRepository unservedItemRepository
                             , UserManager<User> userManager) : BaseController(accessRepository, userManager)
    {
        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.UnservedItem, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetListJson(UnservedItemFilter filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.UnservedItem, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await unservedItemRepository.GetListDataTablesAsync(form, filter));
        }
    }
}