using GMGInventory.Application.Contracts;
using GMGInventory.Application.Repositories;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class AccessController(IAccessRepository accessRepository) : BaseController(accessRepository)
    {
        public async Task<IActionResult> GetRolesOptions(string? IncludeId)
        {
            return Json(await accessRepository.GetRolesOptions(IncludeId));
        }

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

        public async Task<IActionResult> RolesPages(string RoleId)
        {
            var redirectResultAdd = await CheckAccessAndRedirectAsync(PageNames.Accesses, AccessTypes.Add);
            var redirectResultEdit = await CheckAccessAndRedirectAsync(PageNames.Accesses, AccessTypes.Edit);
            if (redirectResultAdd != null && redirectResultEdit != null) return redirectResultAdd;

            var accesses = await accessRepository.GetRolesPagesAsync(RoleId);
            return PartialView(accesses);
        }

        public async Task<IActionResult> RoleDetails(string RoleId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Accesses, AccessTypes.View);
            if (redirectResult != null) return redirectResult;
            var accesses = await accessRepository.GetRolesPagesAsync(RoleId);
            ViewData["RoleUsers"] = await accessRepository.GetRolesUsersAsync(RoleId);
            return PartialView(accesses);
        }

        public async Task<IActionResult> SaveRolesPages(string RoleId, string RoleName, IList<AccessVM> accesses)
        {
            var redirectResultAdd = await CheckAccessAndRedirectAsync(PageNames.Accesses, AccessTypes.Add);
            var redirectResultEdit = await CheckAccessAndRedirectAsync(PageNames.Accesses, AccessTypes.Edit);
            if (redirectResultAdd != null && redirectResultEdit != null) return redirectResultAdd;

            var responseModel = await accessRepository.SaveRolesPagesAsync(RoleId, RoleName, accesses);
            return Json(responseModel);
        }

        public async Task<IActionResult> DeleteDeactivateRole(string RoleId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Accesses, AccessTypes.Delete);
            if (redirectResult != null) return redirectResult;
            var responseModel = await accessRepository.DeleteDeactivateRoleAsync(RoleId);
            return Json(responseModel);
        } 

        public async Task<IActionResult> ReactivateRole(string RoleId)
        {
            var redirectResultAdd = await CheckAccessAndRedirectAsync(PageNames.Accesses, AccessTypes.Add);
            var redirectResultEdit = await CheckAccessAndRedirectAsync(PageNames.Accesses, AccessTypes.Edit);
            if (redirectResultAdd != null && redirectResultEdit != null) return redirectResultAdd;

            var responseModel = await accessRepository.ReactivateRoleAsync(RoleId);

            return Json(responseModel);
        }
    }
}