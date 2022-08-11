using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GMGInventory.Controllers
{
    [Authorize(Roles = Roles.InventoryManager + "," + Roles.SalesDirector + "," + Roles.SystemAdmin + "," + Roles.Telemarketer)]
    public class TransferController : Controller
    {
        private readonly IItemTransferRepository itemTransferRepository;

        public TransferController(IItemTransferRepository itemTransferRepository)
        {
            this.itemTransferRepository = itemTransferRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetWarehouseTransferJson()
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await itemTransferRepository.GetWarehouseTransfersDataTablesAsync(form));
        }

        public async Task<ActionResult> Details(int? Id)
        {
            var transfer = await itemTransferRepository.GetTransferVMAsync(Id);
            if (transfer == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(transfer);
        }

        [HttpPost]
        public async Task<JsonResult> GetItemTransferJson(int? TransferId)
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await itemTransferRepository.GetItemTransfersDataTablesAsync(form, TransferId));
        }

        //[HttpPost]
        //public async Task<JsonResult> UpdateJson(int? Id, decimal RetailPrice, decimal NetPrice)
        //{
        //    ResponseModel responseModel = new ResponseModel();
        //    var update = await itemPriceRepository.UpdateAsync(Id, RetailPrice, NetPrice);
        //    if (update == null)
        //    {
        //        responseModel.Status = "Error";
        //        responseModel.Message = "Item cannot be found";
        //    }
        //    else
        //    {
        //        responseModel.Status = "Success";
        //        responseModel.Message = "Item Price successfully updated";
        //    }
        //    return Json(responseModel);
        //}
    }
}