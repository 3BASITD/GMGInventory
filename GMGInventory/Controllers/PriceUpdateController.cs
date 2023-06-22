using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.Excel;
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
    [Authorize(Roles = Roles.InventoryManager + ","
                     + Roles.SalesDirector + ","
                     + Roles.SystemAdmin + ","
                     + Roles.Telemarketer)]
    public class PriceUpdateController : Controller
    {
        private readonly IItemPriceRepository itemPriceRepository;
        private readonly IItemRepository itemRepository;
        private readonly ControllerHelper chelper;

        public PriceUpdateController(IItemPriceRepository itemPriceRepository
                                    , IItemRepository itemRepository)
        {
            this.itemPriceRepository = itemPriceRepository;
            this.itemRepository = itemRepository;
            chelper = new();
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

        [HttpPost]
        public async Task<JsonResult> UploadJson(string Code, string RetailPrice, string NetPrice)
        {
            ResponseModel responseModel = new ResponseModel();

            if ((await itemRepository.IsCodeExistAsync(Code)) == false)
            {
                responseModel.Status = "Error";
                responseModel.Message = "Unknown Item Code";
            }
            else if (decimal.TryParse(RetailPrice.Replace(",", ""), out decimal retailPrice) == false)
            {
                responseModel.Status = "Error";
                responseModel.Message = "Gross is an invalid price/number";
            }
            else if (decimal.TryParse(NetPrice.Replace(",", ""), out decimal netPrice) == false)
            {
                responseModel.Status = "Error";
                responseModel.Message = "Net is an invalid price/number";
            }
            else
            {
                var itemId = await itemRepository.GetIdAsync(Code);
                var update = await itemPriceRepository.UpdateAsync(itemId, retailPrice, netPrice);
                if (update == null)
                {
                    responseModel.Status = "Error";
                    responseModel.Message = "Item Price failed to update";
                }
                else
                {
                    responseModel.Status = "Success";
                    responseModel.Message = "Item Price successfully updated";
                }
            }
            return Json(responseModel);
        }

        public async Task<IActionResult> DownloadFile(ItemFilterVM filter)
        {
            DataTable dt = new DataTable("Price Update");
            dt.Columns.AddRange(new DataColumn[4] {
                new DataColumn(chelper.DisplayName<ItemPriceVM>("Code")),
                new DataColumn(chelper.DisplayName<ItemPriceVM>("Name")),
                new DataColumn(chelper.DisplayName<ItemPriceVM>("RetailPrice")),
                new DataColumn(chelper.DisplayName<ItemPriceVM>("NetPrice"))
            });

            filter.ActiveOnly = true;
            var prices = await itemPriceRepository.GetAllItemPriceAsync(filter);
            foreach (var price in prices)
            {
                dt.Rows.Add(price.Code,
                            price.Name,
                            price.RetailPrice,
                            price.NetPrice);
            }

            using (XLWorkbook wb = new())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        DateTime.Now.ToString("yyyyMMdd") + " Price List.xlsx");
                }
            }
        }
    }
}