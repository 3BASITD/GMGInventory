using ClosedXML.Excel;
using System.Data;
using GMGInventory.Application.Contracts;
using GMGInventory.Application.Repositories;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GMGInventory.Helpers;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class UnservedItemController(IAccessRepository accessRepository
                             , IUnservedItemRepository unservedItemRepository) : BaseController(accessRepository)
    {
        private readonly ControllerHelper chelper = new();

        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.UnservedItem, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.UnservedItem)).Convert();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetListJson(UnservedItemFilter filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.UnservedItem, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await unservedItemRepository.GetListDataTablesAsync(form, filter));
        }


        public async Task<IActionResult> DownloadFile(UnservedItemFilter filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.UnservedItem, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            DataTable dt = new("Unserved Item");
            dt.Columns.AddRange([
                new DataColumn(chelper.DisplayName<UnservedItemVM>("Item")),
                new DataColumn(chelper.DisplayName<UnservedItemVM>("OrderDate")),
                new DataColumn(chelper.DisplayName<UnservedItemVM>("SalesOrderNumber")),
                new DataColumn(chelper.DisplayName<UnservedItemVM>("Customer")),
                new DataColumn(chelper.DisplayName<UnservedItemVM>("UnitPrice")),
                new DataColumn(chelper.DisplayName<UnservedItemVM>("Quantity")),
                new DataColumn(chelper.DisplayName<UnservedItemVM>("PackedQuantity")),
                new DataColumn(chelper.DisplayName<UnservedItemVM>("UnservedQuantity")),
                new DataColumn(chelper.DisplayName<UnservedItemVM>("QuantityOnHand")),
                new DataColumn(chelper.DisplayName<UnservedItemVM>("Discount1")),
                new DataColumn(chelper.DisplayName<UnservedItemVM>("Discount2")),
                new DataColumn(chelper.DisplayName<UnservedItemVM>("Agent")),
                new DataColumn(chelper.DisplayName<UnservedItemVM>("CreatedBy"))
            ]);
            var uis = await unservedItemRepository.GetListAsync(filter);
            foreach (var ui in uis)
            {
                dt.Rows.Add(ui.Item,
                            ui.OrderDateD,
                            ui.SalesOrderNumber,
                            ui.Customer,
                            ui.UnitPriceD,
                            ui.QuantityD,
                            ui.PackedQuantityD,
                            ui.UnservedQuantityD,
                            ui.QuantityOnHandD,
                            ui.Discount1D,
                            ui.Discount2D,
                            ui.Agent,
                            ui.CreatedBy
                            );
            }

            using XLWorkbook wb = new();
            wb.Worksheets.Add(dt);
            using MemoryStream stream = new();
            wb.SaveAs(stream);
            return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        DateTime.Now.ToString("yyyyMMdd") + " Unserved Item.xlsx");
        }
    }
}