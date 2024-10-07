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
    public class PackController(IAccessRepository accessRepository
                           , IPackRepository packRepository
                           , UserManager<User> userManager) : BaseController(accessRepository, userManager)
    {

        public async Task<IActionResult> GetPackTypeOptions(int? IncludeId)
        {
            return Json(await packRepository.GetPackTypeOptions(IncludeId));
        }

        public async Task<IActionResult> GetOptions(string search, int page)
        {
            return Json(await packRepository.GetOptions(search, page));
        }

        [Authorize(Roles = Roles.InvoicingClerk + ","
                         + Roles.SystemAdmin + ","
                         + Roles.Telemarketer)]
        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PackingListing, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.PackingListing)).Convert();
            ViewData["UserAccess_SalesInvoice"] = new ViewsAccessData(await GetAccessAsync(PageNames.SalesInvoice)).Convert();

            return View();
        }

        [Authorize(Roles = Roles.InvoicingClerk + ","
                         + Roles.SystemAdmin + ","
                         + Roles.Telemarketer)]
        [HttpPost]
        public async Task<IActionResult> GetListJson(ListFilterCommonModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PackingListing, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await packRepository.GetListDTAsync(form, filter));
        }

        [Authorize(Roles = Roles.InvoicingClerk + ","
                         + Roles.SystemAdmin + ","
                         + Roles.Telemarketer)]
        public async Task<IActionResult> Details(int? Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PackingListing, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.PackingListing)).Convert();

            var order = await packRepository.GetDetailsAsync(Id);
            if (order == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        [Authorize(Roles = Roles.InvoicingClerk + ","
                         + Roles.SystemAdmin + ","
                         + Roles.Telemarketer)]
        public async Task<IActionResult> PrintDetails(int Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PackingListing, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;

            return View(await packRepository.PrintDetailsAsync(Id));
        }

        [HttpPost]
        public async Task<IActionResult> GetItemsJson(int packId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PackingListing, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return Json(await packRepository.GetItemsAsync(packId));
        }

        [HttpPost]
        public async Task<IActionResult> GetItemsWithPriceJson(int packId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PackingListing, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return Json(await packRepository.GetItemsWithPriceAsync(packId));
        }

        [HttpPost]
        public async Task<IActionResult> GetItemsDTJson(int packId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PackingListing, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await packRepository.GetItemsDTAsync(form, packId));
        }

        [Authorize(Roles = Roles.InvoicingClerk + ","
                         + Roles.SystemAdmin + ","
                         + Roles.Telemarketer)]
        public async Task<IActionResult> Create(int? Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PackingListing, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            var order = await packRepository.GetDetailsBySOAsync(Id);
            if (order == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(order);
        }

        [Authorize(Roles = Roles.InvoicingClerk + ","
                         + Roles.SystemAdmin + ","
                         + Roles.Telemarketer)]
        [HttpPost]
        public async Task<IActionResult> SaveJson(PackFormVM form)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PackingListing, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            ResponseModel<int> responseModel = new ResponseModel<int>();
            var orderId = await packRepository.SaveAsync(form);
            if (orderId == -1)
            {
                responseModel.Status = "Error";
                responseModel.Message = "Sales Order is missing. Failed to submit.";
            }
            else
            {
                responseModel.Status = "Success";
                responseModel.Message = "Pack successfully " + (form.SavingAs == "post" ? "posted" : "saved as draft");
            }
            responseModel.NewRow = orderId;
            return Json(responseModel);
        }

        [Authorize(Roles = Roles.InvoicingClerk + ","
                         + Roles.SystemAdmin + ","
                         + Roles.Telemarketer)]
        [HttpPost]
        public async Task<IActionResult> UpdateJson(PackFormVM form)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PackingListing, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            ResponseModel<int> responseModel = new ResponseModel<int>();
            var packId = await packRepository.UpdateAsync(form);
            if (packId == -1)
            {
                responseModel.Status = "Error";
                responseModel.Message = "Pack is missing. Failed to submit.";
            }
            else
            {
                responseModel.Status = "Success";
                var msg = "Pack successfully " + (form.SavingAs == "post" ? "posted" : form.SavingAs == "save" ? "saved as draft" : "deleted");
                responseModel.Message = msg;
                TempData["SuccessMessage"] = msg;
            }
            responseModel.NewRow = packId;
            return Json(responseModel);
        }
    }
}