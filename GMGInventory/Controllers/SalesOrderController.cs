using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.Excel;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    //[Authorize]
    //public class SalesOrderController : Controller
    //{
    //    private readonly ISalesOrderRepository salesOrderRepository;

    //    public SalesOrderController(ISalesOrderRepository salesOrderRepository)
    //    {
    //        this.salesOrderRepository = salesOrderRepository;
    //    }
    public class SalesOrderController(IAccessRepository accessRepository
                            , ISalesOrderRepository salesOrderRepository
                            , UserManager<User> userManager) : BaseController(accessRepository, userManager)
    {

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

        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesOrder, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetListDTJson(SalesOrderFilterVM filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesOrder, AccessTypes.View);
            if (redirectResult != null) return redirectResult;


            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await salesOrderRepository.GetListDTAsync(form, filter));
        }

        public async Task<IActionResult> Details(int? Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesOrder, AccessTypes.View);
            if (redirectResult != null) return redirectResult;


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
        public async Task<IActionResult> DetailsJson(int? Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesOrder, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return Json(await salesOrderRepository.GetDetailsAsync(Id));
        }

        public async Task<IActionResult> Print(int? Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesOrder, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;


            var order = await salesOrderRepository.PrintDetailsAsync(Id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> GetItemsJson(int salesOrderId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesOrder, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return Json(await salesOrderRepository.GetItemsAsync(salesOrderId));
        }

        [HttpPost]
        public async Task<IActionResult> GetItemsForPackJson(int salesOrderId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesOrder, AccessTypes.View);
            if (redirectResult != null) return redirectResult;


            return Json(await salesOrderRepository.GetItemsAsync(salesOrderId, true));
        }

        [HttpPost]
        public async Task<IActionResult> GetItemsDTJson(int salesOrderId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesOrder, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await salesOrderRepository.GetItemsDTAsync(form, salesOrderId));
        }

        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.SalesDirector + ","
                         + Roles.SystemAdmin + ","
                         + Roles.Telemarketer)]
        public async Task<IActionResult> Create(int? Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesOrder, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;


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
        public async Task<IActionResult> SaveJson(SalesOrderFormVM form)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesOrder, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

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
        public async Task<IActionResult> SaveItemsJson(SalesOrderFormVM form)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesOrder, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;


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
        public async Task<IActionResult> ApproveJson(int Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesOrder, AccessTypes.ApproveDisapprove);
            if (redirectResult != null) return redirectResult;


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
        public async Task<IActionResult> Approve(int Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesOrder, AccessTypes.ApproveDisapprove);
            if (redirectResult != null) return redirectResult;

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
        public async Task<IActionResult> Disapprove(int Id, string? Reason)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesOrder, AccessTypes.ApproveDisapprove);
            if (redirectResult != null) return redirectResult;

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
        public async Task<IActionResult> UpdateServedSOItem(int salesOrderLineItemId, int quantity)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesOrder, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

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