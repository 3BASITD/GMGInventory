using GMGInventory.Application.Contracts;
using GMGInventory.Application.Repositories;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    public class AccessController(IAccessRepository accessRepository) : BaseController(accessRepository)
    {
        [HttpPost]
        public async Task<JsonResult> GetUserAccessesDT(string userId)
        {
            var form = new DataTablesRequestForm(Request.Form);
            return Json(await accessRepository.GetUserAccessesDTAsync(form, userId));
        }

        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Accesses, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.Accesses)).Convert();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetRolesListJson()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Accesses, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            var form = new DataTablesRequestForm(Request.Form);
            return Json(await accessRepository.GetRolesDTAsync(form));
        }
    }
}