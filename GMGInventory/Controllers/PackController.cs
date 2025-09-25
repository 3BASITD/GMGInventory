using GMGInventory.Application.Contracts;
using GMGInventory.Application.Repositories;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class PackController(IAccessRepository accessRepository
                           , IPackRepository packRepository) : BaseController(accessRepository)
    {

        public async Task<IActionResult> GetPackTypeOptions(int? IncludeId)
        {
            return Json(await packRepository.GetPackTypeOptions(IncludeId));
        }

        public async Task<IActionResult> GetOptions(string search, int page)
        {
            return Json(await packRepository.GetOptions(search, page));
        }

        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PackingListing, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.PackingListing)).Convert();
            ViewData["UserAccess_SalesInvoice"] = new ViewsAccessData(await GetAccessAsync(PageNames.SalesInvoice)).Convert();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetListJson(ListFilterCommonModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PackingListing, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await packRepository.GetListDTAsync(form, filter));
        }

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

        public async Task<IActionResult> Create(int? Id, string Page)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PackingListing, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.PackingListing)).Convert();

            var order = await packRepository.GetDetailsBySOAsync(Id);

            ViewData["SOId"] = Id;
            ViewData["PageName"] = Page;

            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> SaveJson(PackFormVM form)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PackingListing, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            ResponseModel<int> responseModel = new ResponseModel<int>();

            //if (form.SavingAs == "post")
            //{
                var checkItems = packRepository.CheckItem(form);
                if (checkItems != null)
                {
                    responseModel.Status = "Error2";
                    responseModel.Message = checkItems;
                    return Json(responseModel);
                }
            //}

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

        [HttpPost]
        public async Task<IActionResult> UpdateJson(PackFormVM form)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PackingListing, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            ResponseModel<int> responseModel = new ResponseModel<int>();

            if (form.SavingAs == "post")
            {
                var checkItems = packRepository.CheckItem(form);
                if (checkItems != null)
                {
                    responseModel.Status = "Error2";
                    responseModel.Message = checkItems;
                    return Json(responseModel);
                }
            }
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