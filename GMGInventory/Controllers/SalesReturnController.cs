using DocumentFormat.OpenXml.Office2010.Excel;
using GMGInventory.Application.Contracts;
using GMGInventory.Application.Repositories;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static ClosedXML.Excel.XLPredefinedFormat;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class SalesReturnController : Controller
    {
        private readonly ISalesReturnRepository salesReturnRepository;

        public SalesReturnController(ISalesReturnRepository salesReturnRepository)
        {
            this.salesReturnRepository = salesReturnRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetListJson()
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await salesReturnRepository.GetListDTAsync(form));
        }

        public async Task<ActionResult> Details(int? Id)
        {
            var rturn = await salesReturnRepository.GetDetailsAsync(Id);
            if (rturn == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(rturn);
        }

        [HttpPost]
        public async Task<JsonResult> GetItemsJson(int salesReturnId)
        {
            return Json(await salesReturnRepository.GetItemsAsync(salesReturnId));
        }

        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.InvoicingClerk + ","
                         + Roles.SystemAdmin + ","
                         + Roles.SalesDirector + ","
                         + Roles.Telemarketer)]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.InvoicingClerk + ","
                         + Roles.SystemAdmin + ","
                         + Roles.SalesDirector + ","
                         + Roles.Telemarketer)]
        [HttpPost]
        public async Task<JsonResult> SaveJson(SalesReturnFormVM form)
        {
            ResponseModel<int> responseModel = new ();
            var orderId = await salesReturnRepository.SaveAsync(form);
            responseModel.Status = "Success";
            responseModel.Message = "Pack successfully submitted";
            responseModel.NewRow = orderId;

            return Json(responseModel);
        }

        public async Task<IActionResult> PrintDetails(int Id)
        {
            return View(await salesReturnRepository.PrintDetails(Id));
        }

        [HttpPost]
        public async Task<JsonResult> UpdateJson(SalesReturnUpdateVM form)
        {
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