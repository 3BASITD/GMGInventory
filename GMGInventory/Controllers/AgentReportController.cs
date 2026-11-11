using ClosedXML.Excel;
using GMGInventory.Application.Contracts;
using GMGInventory.Application.Repositories;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class AgentReportController(IAccessRepository accessRepository, IAgentReport agentRepository) : BaseController(accessRepository)
    {

        private readonly ControllerHelper chelper = new();
        public async Task<IActionResult> AgentItirenary()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.AgentItirenary, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.AgentItirenary)).Convert();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetAgentItineraryDT(ListFilterCustomerModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.AgentItirenary, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm dtForm = new(Request.Form);
            return Json(await agentRepository.GetAgentDT(dtForm, filter));
        }

        public async Task<IActionResult> PrintAgentWeeklyReport(ListFilterCustomerModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.AgentItirenary, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;

            return View(await agentRepository.PrintAgentWeeklyReport(filter));
        }

        public async Task<IActionResult> PrintAgentMonthlyReport(ListFilterCustomerModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.AgentItirenary, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;

            return View(await agentRepository.PrintAgentMonthlyReport(filter));
        }
    }
}
