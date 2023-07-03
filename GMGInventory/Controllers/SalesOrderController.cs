using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.Excel;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    [Authorize]
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

        public async Task<IActionResult> GetSalesOrderOptions(string search, int page)
        {
            return Json(await salesOrderRepository.GetSalesOrderOptions(search, page));
        }

        public async Task<IActionResult> GetPriceTypeOptions(int? IncludeId)
        {
            return Json(await salesOrderRepository.GetPriceTypeOptions(IncludeId));
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetListDTJson(SalesOrderFilterVM filter)
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await salesOrderRepository.GetListDTAsync(form, filter));
        }

        public async Task<ActionResult> Details(int? Id)
        {
            var order = await salesOrderRepository.GetDetailsAsync(Id);
            if (order == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var dtNow = DateTime.Now;
            var newChecker = dtNow.AddMinutes(-1) <= order.OrderDateD && order.OrderDateD <= dtNow;
            if (newChecker)
            {
                TempData["SuccessMessage"] = "Order successfully submitted";
            }

            return View(order);
        }

        [HttpPost]
        public async Task<ActionResult> DetailsJson(int? Id)
        {
            return Json(await salesOrderRepository.GetDetailsAsync(Id));
        }

        public async Task<ActionResult> Print(int? Id)
        {
            var order = await salesOrderRepository.PrintDetailsAsync(Id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpPost]
        public async Task<JsonResult> GetItemsJson(int salesOrderId)
        {
            return Json(await salesOrderRepository.GetItemsAsync(salesOrderId));
        }

        [HttpPost]
        public async Task<JsonResult> GetItemsForPackJson(int salesOrderId)
        {
            return Json(await salesOrderRepository.GetItemsAsync(salesOrderId, true));
        }

        [HttpPost]
        public async Task<JsonResult> GetItemsDTJson(int salesOrderId)
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await salesOrderRepository.GetItemsDTAsync(form, salesOrderId));
        }

        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.SalesDirector + ","
                         + Roles.SystemAdmin + ","
                         + Roles.Telemarketer)]
        public async Task<ActionResult> Create(int? Id)
        {
            var order = await salesOrderRepository.GetDetailsAsync(Id);
            if (Id != null && (order == null || order.DisapprovedBy == null || order.DisapprovedDateD == null))
            {
                return RedirectToAction("Details", new { Id = Id });
            }
            TempData["SalesOrderId"] = Id;
            return View();
        }

        [Authorize(Roles = Roles.Accounting + ","
                         + Roles.InventoryManager + ","
                         + Roles.SalesDirector + ","
                         + Roles.SystemAdmin + ","
                         + Roles.Telemarketer)]
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

        [Authorize(Roles = Roles.Accounting + ","
                         + Roles.InventoryManager + ","
                         + Roles.SalesDirector + ","
                         + Roles.SystemAdmin + ","
                         + Roles.Telemarketer)]
        [HttpPost]
        public async Task<JsonResult> SaveItemsJson(SalesOrderFormVM form)
        {
            ResponseModel<int> responseModel = new ResponseModel<int>();
            if (form.Id != null)
            {
                await salesOrderRepository.SaveItemsAsync(form.Items, (int)form.Id);
                responseModel.Status = "Success";
                responseModel.Message = "Order item(s) successfully saved";
            }
            else
            {
                responseModel.Status = "Error";
                responseModel.Message = "Invalid Sales Order";
            }
            return Json(responseModel);
        }

        [Authorize(Roles = Roles.SystemAdmin + ","
                         + Roles.SalesDirector)]
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

        [Authorize(Roles = Roles.SystemAdmin + ","
                         + Roles.SalesDirector)]
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

        [Authorize(Roles = Roles.SystemAdmin + ","
                         + Roles.SalesDirector)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disapprove(int Id, string? Reason)
        {
            if (ModelState.IsValid)
            {
                if (await salesOrderRepository.DisapproveAsync(Id, Reason + ""))
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

        [HttpPost]
        public async Task<ActionResult> UpdateServedSOItem(int salesOrderLineItemId, int quantity)
        {
            ResponseModel responseModel = new ResponseModel();
            if (await salesOrderRepository.UpdateServedSOItem(salesOrderLineItemId, quantity))
            {
                responseModel.Status = "Success";
                responseModel.Message = "Served Quantity successfully updated";
            }
            else
            {
                responseModel.Status = "Error";
                responseModel.Message = "Cannot update served quantity. Please try again later";
            }
            return Json(responseModel);
        }
    }
}