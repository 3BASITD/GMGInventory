using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    [Authorize(Roles = Roles.InventoryManager + "," + Roles.SalesDirector + "," + Roles.SystemAdmin + "," + Roles.Telemarketer)]
    public class PriceUpdateController : Controller
    {
        private readonly IItemPriceRepository itemPriceRepository;

        public PriceUpdateController(IItemPriceRepository itemPriceRepository)
        {
            this.itemPriceRepository = itemPriceRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetListJson(ItemFilterVM filter)
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await itemPriceRepository.GetAllItemPriceUpdateDataTablesAsync(form, filter));
        }

        [HttpPost]
        public async Task<JsonResult> UpdateJson(int? Id, decimal RetailPrice, decimal NetPrice)
        {
            ResponseModel responseModel = new ResponseModel();
            var update = await itemPriceRepository.UpdateAsync(Id, RetailPrice, NetPrice);
            if (update == null)
            {
                responseModel.Status = "Error";
                responseModel.Message = "Item cannot be found";
            }
            else
            {
                responseModel.Status = "Success";
                responseModel.Message = "Item Price successfully updated";
            }
            return Json(responseModel);
        }
    }
}