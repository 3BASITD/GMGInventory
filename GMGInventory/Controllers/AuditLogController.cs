using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    public class AuditLogController(IAccessRepository accessRepository
                             , IAuditLogRepository auditLogRepository
                             , UserManager<User> userManager) : BaseController(accessRepository, userManager)
    {
        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.AuditLogs, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetListJson()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.AuditLogs, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm dtForm = new(Request.Form);
            return Json(await auditLogRepository.GetListDataTablesAsync(dtForm));
        }
    }
}