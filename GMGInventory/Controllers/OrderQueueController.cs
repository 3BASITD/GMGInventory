using DocumentFormat.OpenXml.Bibliography;
using GMGInventory.Application.Contracts;
using GMGInventory.Application.Repositories;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class OrderQueueController(IAccessRepository accessRepository
                                    , IOrderQueueRepository queueRepository) : BaseController(accessRepository)
    {
        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.OrderQueue, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.OrderQueue)).Convert();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetOrdersDT(bool IncludeAdded, bool IncludeIgnored)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.OrderQueue, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            var form = new DataTablesRequestForm(Request.Form);
            return Json(await queueRepository.GetOrdersDT(form, IncludeAdded, IncludeIgnored));
        }

        public async Task<IActionResult> Details(string Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.OrderQueue, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.OrderQueue)).Convert();

            return View(await queueRepository.GetDetails(Id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(string OrderNumber, List<int> ItemIds)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.OrderQueue, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            return Json(await queueRepository.AddOrder(OrderNumber, ItemIds));
        }

        [HttpPost]
        public async Task<IActionResult> MarkIgnoreOrder(string OrderNumber)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.OrderQueue, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            return Json(await queueRepository.MarkIgnoreOrder(OrderNumber));
        }
    }
}