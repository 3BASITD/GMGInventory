using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.Excel;
using GMGInventory.Application.Contracts;
using GMGInventory.Application.Repositories;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GMGInventory.Controllers
{

    [Authorize]
    public class TrackerController(IAccessRepository accessRepository
                                 , ITrackerRepository trackerRepository) : BaseController(accessRepository)
    {
        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.TrackerTask, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.TrackerTask)).Convert();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetTasksDT(TaskFilter filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.TrackerTask, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            var form = new DataTablesRequestForm(Request.Form);
            return Json(await trackerRepository.GetTasksDT(form, filter));
        }
    }
}