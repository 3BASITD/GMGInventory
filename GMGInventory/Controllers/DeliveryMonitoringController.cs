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
    public class DeliveryMonitoringController(IAccessRepository accessRepository
                                            , IDeliveryMonitoringRepository deliveryMonitoringRepository) : BaseController(accessRepository)
    {
        private readonly ControllerHelper chelper = new();

        public async Task<IActionResult> GetStatusOptions(int? IncludeId)
        {
            return Json(await deliveryMonitoringRepository.GetStatusOptions(IncludeId));
        }

        public async Task<IActionResult> GetStatusOptions2(string search, int page)
        {
            return Json(await deliveryMonitoringRepository.GetStatusOptions2(search, page));
        }

        public async Task<IActionResult> GetMethodOptions(int? IncludeId)
        {
            return Json(await deliveryMonitoringRepository.GetMethodOptions(IncludeId));
        }

        public async Task<IActionResult> GetMethodOptions2(string search, int page)
        {
            return Json(await deliveryMonitoringRepository.GetMethodOptions2(search, page));
        }

        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.DeliveryMonitoring, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.DeliveryMonitoring)).Convert();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetListDT(DeliveryMonitoringFilterVM filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.DeliveryMonitoring, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            var form = new DataTablesRequestForm(Request.Form);
            return Json(await deliveryMonitoringRepository.GetListDTAsync(form, filter));
        }

        [HttpPost]
        public async Task<IActionResult> SaveDeliveryAsync(DeliveryMonitoringFormVM form)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.DeliveryMonitoring, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            return Json(await deliveryMonitoringRepository.SaveDeliveryAsync(form));
        }

        public async Task<IActionResult> DownloadFile(DeliveryMonitoringFilterVM filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.DeliveryMonitoring, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            DataTable dt = new("Delivery Monitoring");
            dt.Columns.AddRange([
                new DataColumn(chelper.DisplayName<DeliveryMonitoringVM>("DateOrder")),
                new DataColumn(chelper.DisplayName<DeliveryMonitoringVM>("SalesOrderNumber")),
                new DataColumn(chelper.DisplayName<DeliveryMonitoringVM>("OrderQueueNumber")),
                new DataColumn(chelper.DisplayName<DeliveryMonitoringVM>("ServiceLevelAgreement")),
                new DataColumn(chelper.DisplayName<DeliveryMonitoringVM>("Customer")),
                new DataColumn(chelper.DisplayName<DeliveryMonitoringVM>("SOPreparedBy")),
                new DataColumn(chelper.DisplayName<DeliveryMonitoringVM>("SOApprovedBy")),
                new DataColumn(chelper.DisplayName<DeliveryMonitoringVM>("SpecialInstruction")),
                new DataColumn(chelper.DisplayName<DeliveryMonitoringVM>("Forwarder")),
                new DataColumn(chelper.DisplayName<DeliveryMonitoringVM>("Status1")),
                new DataColumn(chelper.DisplayName<DeliveryMonitoringVM>("Status2")),
                new DataColumn(chelper.DisplayName<DeliveryMonitoringVM>("Status3")),
                new DataColumn(chelper.DisplayName<DeliveryMonitoringVM>("Status4")),
                new DataColumn(chelper.DisplayName<DeliveryMonitoringVM>("Status5"))


            ]);

            var sos = await deliveryMonitoringRepository.GetListAsync(filter);
            foreach (var so in sos)
            {
                dt.Rows.Add(so.DateOrder,
                            so.SalesOrderNumber,
                            so.OrderQueueNumber,
                            so.ServiceLevelAgreement,
                            so.Customer,
                            so.SOPreparedBy,
                            so.SOApprovedBy,
                            so.SpecialInstruction,
                            so.Forwarder,
                            so.Status1,
                            so.Status2,
                            so.Status3,
                            so.Status4,
                            so.Status5);
            }

            using XLWorkbook wb = new();
            wb.Worksheets.Add(dt);
            using MemoryStream stream = new();
            wb.SaveAs(stream);
            return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        DateTime.Now.ToString("yyyyMMdd") + " Sales Order Delivery.xlsx");
        }

        [HttpPost]
        public async Task<IActionResult> GetLatestCountAsync()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.DeliveryMonitoring, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return Json(await deliveryMonitoringRepository.GetLatestCounts());
        }
    }
}