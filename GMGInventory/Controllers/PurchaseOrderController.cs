using GMGInventory.Application.Contracts;
using GMGInventory.Application.Repositories;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        //[HttpPost]
        //public async Task<JsonResult> CreateJson(TransferFormVM formVM)
        //{
        //    ResponseModel responseModel = new ResponseModel();
        //    await itemTransferRepository.AddAsync(formVM);
        //    responseModel.Status = "Success";
        //    responseModel.Message = "Transfer successfully added";
        //    return Json(responseModel);
        //}
    }
}