using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class BarcotechSyncController(IAccessRepository accessRepository
                                       , IBarcotechSyncRepository syncRepository) : BaseController(accessRepository)
    {
        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.BarcotechSync, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetListJson(BarcotechSyncFilter filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.BarcotechSync, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm dtForm = new(Request.Form);
            return Json(await syncRepository.GetBarcotechStatusListDTAsync(dtForm, filter));
        }
    }
}