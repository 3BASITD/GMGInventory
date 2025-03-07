using ClosedXML.Excel;
using GMGInventory.Application.Contracts;
using GMGInventory.Application.Repositories;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class SalesOrderDeliveryController(IAccessRepository accessRepository
                             , ISalesOrderDeliveryRepository salesOrderDeliveryRepository) : BaseController(accessRepository)
    {
        private readonly ControllerHelper chelper = new();

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

        public async Task<IActionResult> DownloadFile(SalesOrderDeliveryFilterVM filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesOrderDelivery, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            DataTable dt = new("Sales Order");
            dt.Columns.AddRange([
                new DataColumn(chelper.DisplayName<SalesOrderDeliveryVM>("SalesOrderDate")),
                new DataColumn(chelper.DisplayName<SalesOrderDeliveryVM>("SONumber")),
                new DataColumn(chelper.DisplayName<SalesOrderDeliveryVM>("CustomerCode")),
                new DataColumn(chelper.DisplayName<SalesOrderDeliveryVM>("CustomerName")),
                new DataColumn(chelper.DisplayName<SalesOrderDeliveryVM>("CustomerAddress")),
                new DataColumn(chelper.DisplayName<SalesOrderDeliveryVM>("DatePickup")),
                new DataColumn(chelper.DisplayName<SalesOrderDeliveryVM>("DateDeliver")),
                new DataColumn(chelper.DisplayName<SalesOrderDeliveryVM>("Forwarder")),
                new DataColumn(chelper.DisplayName<SalesOrderDeliveryVM>("WaybillNumber")),
                new DataColumn(chelper.DisplayName<SalesOrderDeliveryVM>("Remarks")),
                new DataColumn(chelper.DisplayName<SalesOrderDeliveryVM>("PONumber")),
                new DataColumn(chelper.DisplayName<SalesOrderDeliveryVM>("ServiceLevelAgreement")),
                new DataColumn(chelper.DisplayName<SalesOrderDeliveryVM>("SalesOrderStatus")),
                new DataColumn(chelper.DisplayName<SalesOrderDeliveryVM>("RequiredDeliveryDate")),
                new DataColumn(chelper.DisplayName<SalesOrderDeliveryVM>("Representative")),
                new DataColumn(chelper.DisplayName<SalesOrderDeliveryVM>("DateReceive"))


            ]);
            var sos = await salesOrderDeliveryRepository.GetListAsync(filter);
            foreach (var so in sos)
            {
                dt.Rows.Add(so.SalesOrderDate,
                            so.SONumber,
                            so.CustomerCode,
                            so.CustomerName,
                            so.CustomerAddress,
                            so.DatePickup,
                            so.DateDeliver,
                            so.Forwarder,
                            so.WaybillNumber,
                            so.Remarks,
                            so.PONumber,
                            so.ServiceLevelAgreement,
                            so.SalesOrderStatus,
                            so.RequiredDeliveryDate,
                            so.Representative,
                            so.DateReceive);
            }

            using XLWorkbook wb = new();
            wb.Worksheets.Add(dt);
            using MemoryStream stream = new();
            wb.SaveAs(stream);
            return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        DateTime.Now.ToString("yyyyMMdd") + " Sales Order Delivery.xlsx");
        }

    }
}