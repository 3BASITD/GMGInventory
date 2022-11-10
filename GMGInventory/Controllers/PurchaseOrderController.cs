using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    [Authorize(Roles = Roles.InventoryManager + "," + Roles.SalesDirector + "," + Roles.SystemAdmin + "," + Roles.Telemarketer)]
    public class PurchaseOrderController : Controller
    {
        private readonly IPurchaseOrderRepository purchaseOrderRepository;

        public PurchaseOrderController(IPurchaseOrderRepository purchaseOrderRepository)
        {
            this.purchaseOrderRepository = purchaseOrderRepository;
        }

        public async Task<IActionResult> GetOptions(string search, int page)
        {
            return Json(await purchaseOrderRepository.GetOptions(search, page));
        }

        public async Task<IActionResult> GetCurrencyOptions(int? IncludeId)
        {
            return Json(await purchaseOrderRepository.GetCurrencyOptions(IncludeId));
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetListDTJson()
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await purchaseOrderRepository.GetListDTAsync(form));
        }

        public async Task<ActionResult> Details(int? Id)
        {
            var order = await purchaseOrderRepository.GetDetailsAsync(Id);
            if (order == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var dtNow = DateTime.Now;
            var newChecker = dtNow.AddMinutes(-1) <= order.OrderDateD && order.OrderDateD <= dtNow;
            if (newChecker)
            {
                TempData["SuccessMessage"] = order.Status + " successfully saved";
            }

            return View(order);
        }

        [HttpPost]
        public async Task<JsonResult> GetItemsJson(int purchaseOrderId)
        {
            return Json(await purchaseOrderRepository.GetItemsAsync(purchaseOrderId));
        }

        [HttpPost]
        public async Task<JsonResult> GetItemsDTJson(int purchaseOrderId)
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await purchaseOrderRepository.GetItemsDTAsync(form, purchaseOrderId));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> SaveJson(PurchaseOrderFormVM form, string type)
        {
            ResponseModel<int> responseModel = new ResponseModel<int>();
            if (type == "Draft")
            {
                var draft = await purchaseOrderRepository.SaveDraftAsync(form);
                responseModel.Status = "Success";
                responseModel.Message = "Draft successfully saved";
                responseModel.NewRow = draft;
            }
            else
            {
                var replenish = await purchaseOrderRepository.SaveReplenishAsync(form);
                responseModel.Status = "Success";
                responseModel.Message = "Replenish successfully saved";
                responseModel.NewRow = replenish;
            }
            return Json(responseModel);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteDraftJson(int Id)
        {
            ResponseModel responseModel = new ResponseModel();
            if (await purchaseOrderRepository.DeleteDraftAsync(Id))
            {
                responseModel.Status = "Success";
                responseModel.Message = "Draft successfully removed";
            }
            else
            {
                responseModel.Status = "Error";
                responseModel.Message = "Cannot remove draft. Please try again later";
            }
            return Json(responseModel);
        }

        //[HttpPost]
        //public async Task<JsonResult> CreateJson(PurchaseOrderFormVM formVM)
        //{
        //    ResponseModel responseModel = new ResponseModel();
        //    await purchaseOrderRepository.AddAsync(formVM);
        //    responseModel.Status = "Success";
        //    responseModel.Message = "Transfer successfully added";
        //    return Json(responseModel);
        //}
    }
}