using ClosedXML.Excel;
using GMGInventory.Application.Contracts;
using GMGInventory.Application.Repositories;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class PriceListController(IAccessRepository accessRepository
                             , IItemPriceRepository itemPriceRepository
                             , ICategoryRepository categoryRepository
                             , IItemTypeRepository itemTypeRepository
                             , IBrandRepository brandRepository) : BaseController(accessRepository)
    {
        private readonly ControllerHelper chelper = new();

        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PriceList, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.PriceList)).Convert();

            ItemFilterVM itemFilterVM = new ItemFilterVM();
            itemFilterVM.Categories = new SelectList(await categoryRepository.GetAllActiveCategoryAsync(-1), "Id", "Name");
            itemFilterVM.ItemTypes = new SelectList(await itemTypeRepository.GetAllActiveItemTypeAsync(-1), "Id", "Name");
            itemFilterVM.Brands = new SelectList(await brandRepository.GetAllActiveBrandAsync(-1), "Id", "Name");
            ViewData["ItemFilter"] = itemFilterVM;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetListJson(ItemFilterVM filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PriceList, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await itemPriceRepository.GetAllItemPriceDataTablesAsync(form, filter));
        }

        public async Task<IActionResult> Print(ItemFilterVM filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PriceList, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;

            return View(await itemPriceRepository.GetAllItemPricePrintAsync(filter));
        }

        public async Task<IActionResult> DownloadFile(ItemFilterVM filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PriceList, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            DataTable dt = new("Price List");
            dt.Columns.AddRange([
                new DataColumn(chelper.DisplayName<SalesOrderVM>("Brand")),
                new DataColumn(chelper.DisplayName<SalesOrderVM>("ModelNumber")),
                new DataColumn(chelper.DisplayName<SalesOrderVM>("Size")),
                new DataColumn(chelper.DisplayName<SalesOrderVM>("Code")),
                new DataColumn(chelper.DisplayName<SalesOrderVM>("Name")),
                new DataColumn(chelper.DisplayName<SalesOrderVM>("PartNumber")),
                new DataColumn(chelper.DisplayName<SalesOrderVM>("Unit")),
                new DataColumn(chelper.DisplayName<SalesOrderVM>("RetailPrice")),
                new DataColumn(chelper.DisplayName<SalesOrderVM>("NetPrice")),
                new DataColumn(chelper.DisplayName<SalesOrderVM>("Quantity"))
            ]);
            var sos = await itemPriceRepository.GetAllItemPriceAsync(filter);
            foreach (var so in sos)
            {
                dt.Rows.Add(so.Brand,
                            so.ModelNumber,
                            so.Size,
                            so.Code,
                            so.Name,
                            so.PartNumber,
                            so.Unit,
                            so.RetailPrice,
                            so.NetPrice,
                            so.Quantity);
            }

            using XLWorkbook wb = new();
            wb.Worksheets.Add(dt);
            using MemoryStream stream = new();
            wb.SaveAs(stream);
            return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        DateTime.Now.ToString("yyyyMMdd") + " Price List.xlsx");
        }
    }
}