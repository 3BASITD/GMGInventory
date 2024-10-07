using DocumentFormat.OpenXml.Office2010.Excel;
using GMGInventory.Application.Contracts;
using GMGInventory.Application.Repositories;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static ClosedXML.Excel.XLPredefinedFormat;

namespace GMGInventory.Controllers
{
    public class SalesReturnController(IAccessRepository accessRepository
                            , ISalesReturnRepository salesReturnRepository
                            , UserManager<User> userManager) : BaseController(accessRepository, userManager)
    {
        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesReturn, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

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

        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.InvoicingClerk + ","
                         + Roles.SystemAdmin + ","
                         + Roles.SalesDirector + ","
                         + Roles.Telemarketer)]
        public async Task<IActionResult> Create()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesReturn, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            return View();
        }

        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.InvoicingClerk + ","
                         + Roles.SystemAdmin + ","
                         + Roles.SalesDirector + ","
                         + Roles.Telemarketer)]
        [HttpPost]
        public async Task<IActionResult> SaveJson(SalesReturnFormVM form)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesReturn, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            ResponseModel<int> responseModel = new ();
            var orderId = await salesReturnRepository.SaveAsync(form);
            responseModel.Status = "Success";
            responseModel.Message = "Pack successfully submitted";
            responseModel.NewRow = orderId;

            return Json(responseModel);
        }

        public async Task<IActionResult> PrintDetails(int Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesReturn, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;

            return View(await salesReturnRepository.PrintDetails(Id));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateJson(SalesReturnUpdateVM form)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesReturn, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            ResponseModel<int> responseModel = new();
            if(await salesReturnRepository.IsNumberInUse(form.Id, form.ReturnNumber))
            {
                responseModel.Status = "Error";
                responseModel.Message = "Sales Return Number already in use";
            }
            var orderId = await salesReturnRepository.UpdateAsync(form);
            if(orderId == -1)
            {
                responseModel.Status = "Error";
                responseModel.Message = "Sales Return cannot be found. Try reloading the page";
            }
            responseModel.Status = "Success";
            responseModel.Message = "Pack successfully updated";
            responseModel.NewRow = orderId;

            return Json(responseModel);
        }
    }
}