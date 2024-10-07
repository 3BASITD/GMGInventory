using DocumentFormat.OpenXml.Spreadsheet;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Data;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;

namespace GMGInventory.Controllers
{
    public class BaseController : Controller
    {
        private readonly IAccessRepository _accessRepository;
        protected PageNames PageName { get; set; }

        public BaseController(IAccessRepository accessRepository)
        {
            _accessRepository = accessRepository ?? throw new ArgumentNullException(nameof(accessRepository));
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var userId = HttpContext.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            ViewData["NavigationPages"] = _accessRepository.GetUserAllowedPagesAsync(userId ?? "").Result;
            return base.OnActionExecutionAsync(context, next);
        }

        protected async Task<IActionResult> CheckAccessAndRedirectAsync(PageNames pageName, AccessTypes accessType)
        {
            if (HttpContext.User?.Identity?.IsAuthenticated != true)
            {
                if (IsAjaxRequest())
                    return new JsonResult(new { error = "Not authenticated" }) { StatusCode = StatusCodes.Status401Unauthorized };
                else
                    return Redirect("/Identity/Account/Login");
            }

            if (!await CheckAccessAsync(pageName, accessType))
            {
                if (IsAjaxRequest())
                    return new JsonResult(new { error = "Access Denied" }) { StatusCode = StatusCodes.Status403Forbidden };
                else
                    return View("~/Views/Shared/E403.cshtml");
            }

            return null;
        }

        private bool IsAjaxRequest()
        {
            return HttpContext.Request.Headers.XRequestedWith == "XMLHttpRequest"
                || HttpContext.Request.Headers.Accept.ToString().Contains("application/json");
        }

        protected async Task<bool> CheckAccessAsync(PageNames pageName, AccessTypes accessType)
        {
            if (HttpContext.User?.Identity?.IsAuthenticated != true)
                return false;
            var userId = HttpContext.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return false;

            return await _accessRepository.HasAccessAsync(userId, pageName, accessType);
        }

        protected async Task<Access> GetAccessAsync(PageNames pageName)
        {
            if (HttpContext.User?.Identity?.IsAuthenticated != true)
                return new Access();
            var userId = HttpContext.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return new Access();

            return await _accessRepository.GetUserAccessToPageAsync(userId, pageName);
        }
    }
}