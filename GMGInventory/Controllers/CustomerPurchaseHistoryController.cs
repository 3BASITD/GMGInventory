using ClosedXML.Excel;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class CustomerPurchaseHistoryController(IAccessRepository accessRepository
                                                 , ICustomerPurchaseRepository customerPurchaseRepository) : BaseController(accessRepository)
    {
        private readonly ControllerHelper chelper = new();

        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.CustomerPurchaseHistory, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.CustomerPurchaseHistory)).Convert();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetListJson(CustomerPurchaseFilterVM filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.CustomerPurchaseHistory, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            var form = new DataTablesRequestForm(Request.Form);
            return Json(await customerPurchaseRepository.GetListDataTablesAsync(form, filter));
        }

        public async Task<IActionResult> DownloadFile(CustomerPurchaseFilterVM filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.CustomerPurchaseHistory, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            DataTable dt = new("Customer Purchase History");
            dt.Columns.AddRange([
                 new DataColumn(chelper.DisplayName<CustomerPurchaseVM>("SalesOrderDate")),
                 new DataColumn(chelper.DisplayName<CustomerPurchaseVM>("SONumber")),
                 new DataColumn(chelper.DisplayName<CustomerPurchaseVM>("InvoiceNumber")),
                 new DataColumn(chelper.DisplayName<CustomerPurchaseVM>("ControlNumber")),
                 new DataColumn(chelper.DisplayName<CustomerPurchaseVM>("Brand")),
                 new DataColumn(chelper.DisplayName<CustomerPurchaseVM>("ItemCode")),
                 new DataColumn(chelper.DisplayName<CustomerPurchaseVM>("ItemName")),
                 new DataColumn(chelper.DisplayName<CustomerPurchaseVM>("PartNumber")),
                 new DataColumn(chelper.DisplayName<CustomerPurchaseVM>("Unit")),
                 new DataColumn(chelper.DisplayName<CustomerPurchaseVM>("Quantity")),
                 new DataColumn(chelper.DisplayName<CustomerPurchaseVM>("SellingPrice")),
                 new DataColumn(chelper.DisplayName<CustomerPurchaseVM>("ExtendedPriceD")),
             ]);
            var customerPurchase = await customerPurchaseRepository.GetListAsync(filter);
            foreach (var customer in customerPurchase)
            {
                dt.Rows.Add(customer.SalesOrderDate,
                            customer.SONumber,
                            customer.InvoiceNumber,
                            customer.ControlNumber,
                            customer.Brand,
                            customer.ItemCode,
                            customer.ItemName,
                            customer.PartNumber,
                            customer.Unit,
                            customer.Quantity,
                            customer.SellingPrice,
                            customer.ExtendedPrice);

            }

            using XLWorkbook wb = new();
            wb.Worksheets.Add(dt);
            using MemoryStream stream = new();
            wb.SaveAs(stream);
            return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        DateTime.Now.ToString("yyyyMMdd") + " Customer Purchase History.xlsx");
        }
    }
}