using GMGInventory.Application.Contracts;
using GMGInventory.Application.Repositories;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    [Authorize(Roles = Roles.Accounting + ","
                     + Roles.InventoryManager + ","
                     + Roles.InvoicingClerk + ","
                     + Roles.SalesDirector + ","
                     + Roles.SystemAdmin + "," 
                     + Roles.Telemarketer)]
    public class SalesOrderDeliveryController : Controller
    {
        private readonly ISalesOrderDeliveryRepository salesOrderDeliveryRepository;

        public SalesOrderDeliveryController(ISalesOrderDeliveryRepository salesOrderDeliveryRepository)
        {
            this.salesOrderDeliveryRepository = salesOrderDeliveryRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetListJson(SalesOrderDeliveryFilterVM filter)
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await salesOrderDeliveryRepository.GetListDataTablesAsync(form, filter));
        }

        [HttpPost]
        public async Task<JsonResult> EditJson(SalesOrderDeliveryFormVM form)
        {
            ResponseModel responseModel = new ResponseModel();
                var msg = await salesOrderDeliveryRepository.EditAsync(form);
            if (msg == "Invalid")
            {
                responseModel.Status = "Error";
                responseModel.Message = "Invalid sales order";
            }
            else
            {
                responseModel.Status = "Success";
                responseModel.Message = msg;
            }
            return Json(responseModel);
        }
    }
}