using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class SalesOrderDeliveryController(IAccessRepository accessRepository
                             , ISalesOrderDeliveryRepository salesOrderDeliveryRepository) : BaseController(accessRepository)
    {

        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesOrderDelivery, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.SalesOrderDelivery)).Convert();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetListJson(SalesOrderDeliveryFilterVM filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesOrderDelivery, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await salesOrderDeliveryRepository.GetListDataTablesAsync(form, filter));
        }

        [HttpPost]
        public async Task<IActionResult> EditJson(SalesOrderDeliveryFormVM form)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesOrderDelivery, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

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