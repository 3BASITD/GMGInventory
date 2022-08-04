using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GMGInventory.Data;
using GMGInventory.Common.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using GMGInventory.Application.Contracts;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using ClosedXML.Excel;
using GMGInventory.Helpers;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class SalesReportController : Controller
    {
        private readonly IReportSalesRepository reportSalesRepository;
        private readonly ControllerHelper chelper;

        public SalesReportController(IReportSalesRepository reportSalesRepository)
        {
            this.reportSalesRepository = reportSalesRepository;
            chelper = new();
        }

        public async Task<IActionResult> UnservedItem()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetUnservedItemDT(UnservedItemFilter filter)
        {
            DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
            return Json(reportSalesRepository.GetUnservedItemDT(dtForm, filter));
        }

        public IActionResult DownloadUnservedItem(UnservedItemFilter filter)
        {
            DataTable dt = new DataTable("Unserved Items");
            UnservedItemVM unservedItemVM = new UnservedItemVM();
            dt.Columns.AddRange(new DataColumn[12] {
                new DataColumn(chelper.DisplayName<UnservedItemVM>("Item")),
                new DataColumn(chelper.DisplayName<UnservedItemVM>("OrderDate")),
                new DataColumn(chelper.DisplayName<UnservedItemVM>("SalesOrderNumber")),
                new DataColumn(chelper.DisplayName<UnservedItemVM>("Customer")),
                new DataColumn(chelper.DisplayName<UnservedItemVM>("UnitPrice")),
                new DataColumn(chelper.DisplayName<UnservedItemVM>("Quantity")),
                new DataColumn(chelper.DisplayName<UnservedItemVM>("PackedQuantity")),
                new DataColumn(chelper.DisplayName<UnservedItemVM>("UnservedQuantity")),
                new DataColumn(chelper.DisplayName<UnservedItemVM>("Discount1")),
                new DataColumn(chelper.DisplayName<UnservedItemVM>("Discount2")),
                new DataColumn(chelper.DisplayName<UnservedItemVM>("Agent")),
                new DataColumn(chelper.DisplayName<UnservedItemVM>("CreatedBy"))
            });
            var unservedItems = reportSalesRepository.GetUnservedItem(filter);
            foreach (var unservedItem in unservedItems)
            {
                dt.Rows.Add(unservedItem.Item,
                            unservedItem.OrderDate,
                            unservedItem.SalesOrderNumber,
                            unservedItem.Customer,
                            unservedItem.UnitPrice,
                            unservedItem.Quantity,
                            unservedItem.PackedQuantity,
                            unservedItem.UnservedQuantity,
                            unservedItem.Discount1,
                            unservedItem.Discount2,
                            unservedItem.Agent,
                            unservedItem.CreatedBy
                            );
            }

            using (XLWorkbook wb = new())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        DateTime.Now.ToString("yyyyMMdd") + " Item Types.xlsx");
                }
            }
        }
    }
}
