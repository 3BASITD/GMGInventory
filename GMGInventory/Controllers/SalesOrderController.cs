using DocumentFormat.OpenXml.Office2010.Excel;
using GMGInventory.Application.Contracts;
using GMGInventory.Application.Repositories;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GMGInventory.Controllers
{
    [Authorize(Roles = Roles.Accounting + ","
                     + Roles.InventoryManager + ","
                     + Roles.InvoicingClerk + ","
                     + Roles.SalesDirector + ","
                     + Roles.SystemAdmin + ","
                     + Roles.Telemarketer)]
    public class SalesOrderController : Controller
    {
        private readonly ISalesOrderRepository salesOrderRepository;

        public SalesOrderController(ISalesOrderRepository salesOrderRepository)
        {
            this.salesOrderRepository = salesOrderRepository;
        }

        public async Task<IActionResult> GetOptions(string search, int page)
        {
            return Json(await salesOrderRepository.GetOptions(search, page));
        }

        public async Task<IActionResult> GetPriceTypeOptions(int? IncludeId)
        {
            return Json(await salesOrderRepository.GetPriceTypeOptions(IncludeId));
        }

        public async Task<IActionResult> GetDeliveryTypeOptions(int? IncludeId)
        {
            return Json(await salesOrderRepository.GetDeliveryTypeOptions(IncludeId));
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetListDTJson()
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await salesOrderRepository.GetListDTAsync(form));
        }

        public async Task<ActionResult> Details(int? Id)
        {
            var order = await salesOrderRepository.GetDetailsAsync(Id);
            if (order == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        [HttpPost]
        public async Task<JsonResult> GetItemsJson(int salesOrderId)
        {
            return Json(await salesOrderRepository.GetItemsAsync(salesOrderId));
        }

        [HttpPost]
        public async Task<JsonResult> GetItemsDTJson(int salesOrderId)
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await salesOrderRepository.GetItemsDTAsync(form, salesOrderId));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> SaveJson(SalesOrderFormVM form)
        {
            ResponseModel<int> responseModel = new ResponseModel<int>();
            var orderId = await salesOrderRepository.SaveAsync(form);
            responseModel.Status = "Success";
            responseModel.Message = "Order successfully submitted";
            responseModel.NewRow = orderId;
            return Json(responseModel);
        }

        [HttpPost]
        public async Task<JsonResult> SaveItemsJson(SalesOrderFormVM form)
        {
            ResponseModel<int> responseModel = new ResponseModel<int>();
            if (form.Id != null)
            {
                await salesOrderRepository.SaveItemsAsync(form.Items, (int)form.Id);
                responseModel.Status = "Success";
                responseModel.Message = "Order item(s) successfully saved";
            } else
            {
                responseModel.Status = "Error";
                responseModel.Message = "Invalid Sales Order";
            }
            return Json(responseModel);
        }

        [HttpPost]
        public async Task<JsonResult> ApproveJson(int Id)
        {
            ResponseModel responseModel = new ResponseModel();
            if (await salesOrderRepository.ApproveAsync(Id))
            {
                responseModel.Status = "Success";
                responseModel.Message = "Order successfully approved";
            }
            else
            {
                responseModel.Status = "Error";
                responseModel.Message = "Cannot approve order. Please try again later";
            }
            return Json(responseModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Approve(int Id)
        {
            if (ModelState.IsValid)
            {
                if (await salesOrderRepository.ApproveAsync(Id))
                {
                    TempData["SuccessMessage"] = "Sales order approved successfully";
                }
                else
                {
                    TempData["ErrorMessage"] = "Approving sales order failed";
                }
            }
            return RedirectToAction(nameof(Details), new { id = Id });
        }

        [HttpPost]
        public async Task<JsonResult> DisapproveJson(int Id)
        {
            ResponseModel responseModel = new ResponseModel();
            if (await salesOrderRepository.DisapproveAsync(Id))
            {
                responseModel.Status = "Success";
                responseModel.Message = "Order successfully disapproved";
            }
            else
            {
                responseModel.Status = "Error";
                responseModel.Message = "Cannot disapprove order. Please try again later";
            }
            return Json(responseModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disapprove(int Id)
        {
            if (ModelState.IsValid)
            {
                if (await salesOrderRepository.DisapproveAsync(Id))
                {
                    TempData["SuccessMessage"] = "Sales order disapproved successfully";
                }
                else
                {
                    TempData["ErrorMessage"] = "Disapproving sales order failed";
                }
            }
            return RedirectToAction(nameof(Details), new { id = Id });
        }
    }
}