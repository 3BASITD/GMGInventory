using GMGInventory.Application.Contracts;
using GMGInventory.Application.Services;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class SalesReturnController(IAccessRepository accessRepository
                                     , ISalesReturnRepository salesReturnRepository) : BaseController(accessRepository)
    {
        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesReturn, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.SalesReturn)).Convert();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetListJson()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesReturn, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await salesReturnRepository.GetListDTAsync(form));
        }

        public async Task<IActionResult> Details(int? Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesReturn, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.SalesReturn)).Convert();

            var rturn = await salesReturnRepository.GetDetailsAsync(Id);
            if (rturn == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(rturn);
        }

        [HttpPost]
        public async Task<IActionResult> GetItemsJson(int salesReturnId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesReturn, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return Json(await salesReturnRepository.GetItemsAsync(salesReturnId));
        }

        public async Task<IActionResult> Create()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesReturn, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.SalesReturn)).Convert();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetNextFormattedNumberJson()
        {
            return Json(await salesReturnRepository.GetNextFormattedNumber());
        }

        [HttpPost]
        public async Task<IActionResult> SaveJson(SalesReturnFormVM form)
        {
            var redirectResultAdd = await CheckAccessAndRedirectAsync(PageNames.SalesReturn, AccessTypes.Add);
            var redirectResultEdit = await CheckAccessAndRedirectAsync(PageNames.SalesReturn, AccessTypes.Edit);
            if (redirectResultAdd != null && redirectResultEdit != null) return redirectResultAdd;

            ResponseModel<int> responseModel = await salesReturnRepository.SaveAsync(form);
            return Json(responseModel);
        }

        public async Task<IActionResult> PrintDetails(int Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesReturn, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;

            return View(await salesReturnRepository.PrintDetails(Id));
        }
    }
}