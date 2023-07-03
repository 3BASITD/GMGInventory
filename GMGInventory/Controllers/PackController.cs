using GMGInventory.Application.Contracts;
using GMGInventory.Application.Repositories;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class PackController : Controller
    {
        private readonly IPackRepository packRepository;

        public PackController(IPackRepository packRepository)
        {
            this.packRepository = packRepository;
        }

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
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = Roles.InvoicingClerk + ","
                         + Roles.SystemAdmin + ","
                         + Roles.Telemarketer)]
        [HttpPost]
        public async Task<JsonResult> GetListJson(ListFilterCommonModel filter)
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await packRepository.GetListDTAsync(form, filter));
        }

        [Authorize(Roles = Roles.InvoicingClerk + ","
                         + Roles.SystemAdmin + ","
                         + Roles.Telemarketer)]
        public async Task<ActionResult> Details(int? Id)
        {
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
            return View(await packRepository.PrintDetailsAsync(Id));
        }

        [HttpPost]
        public async Task<JsonResult> GetItemsJson(int packId)
        {
            return Json(await packRepository.GetItemsAsync(packId));
        }

        [HttpPost]
        public async Task<JsonResult> GetItemsWithPriceJson(int packId)
        {
            return Json(await packRepository.GetItemsWithPriceAsync(packId));
        }

        [HttpPost]
        public async Task<JsonResult> GetItemsDTJson(int packId)
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await packRepository.GetItemsDTAsync(form, packId));
        }

        [Authorize(Roles = Roles.InvoicingClerk + ","
                         + Roles.SystemAdmin + ","
                         + Roles.Telemarketer)]
        public async Task<ActionResult> Create(int? Id)
        {
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
        public async Task<JsonResult> SaveJson(PackFormVM form)
        {
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
        public async Task<JsonResult> UpdateJson(PackFormVM form)
        {
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