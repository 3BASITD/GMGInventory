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
            return Json(await itemTransferRepository.GetWarehouseTransfersDTAsync(form));
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
            return Json(await itemTransferRepository.GetItemTransfersDTAsync(form, TransferId));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> CreateJson(TransferFormVM formVM)
        {
            ResponseModel responseModel = new ResponseModel();
            await itemTransferRepository.AddAsync(formVM);
            responseModel.Status = "Success";
            responseModel.Message = "Transfer successfully added";
            return Json(responseModel);
        }
    }
}