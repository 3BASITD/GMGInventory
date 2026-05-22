using ClosedXML.Excel;
using GMGInventory.Application.Contracts;
using GMGInventory.Application.Services;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Data;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class ItemController(IAccessRepository accessRepository
                              , IItemRepository itemRepository
                              , IItemConversionRepository itemConversionRepository
                              , IAPIBCT_Service apiBCTService
                              , IOptions<APIBCTSettings> apiBCTOptions) : BaseController(accessRepository)
    {
        public async Task<IActionResult> GetOptions(string search, int page)
        {
            return Json(await itemRepository.GetOptions(search, page));
        }

        public async Task<IActionResult> GetOptionsPromo(string search, int page, DateTime orderDate)
        {
            return Json(await itemRepository.GetOptionsPromo(search, page, orderDate));
        }

        public async Task<IActionResult> GetUnservedOptions(string search, int page, int SalesOrderId)
        {
            return Json(await itemRepository.GetUnservedOptions(search, page, SalesOrderId));
        }

        public async Task<IActionResult> GetModelNumberOptions(string search, int page)
        {
            return Json(await itemRepository.GetModelNumberOptions(search, page));
        }

        public async Task<IActionResult> GetSizeOptions(string search, int page)
        {
            return Json(await itemRepository.GetSizeOptions(search, page));
        }

        public async Task<IActionResult> GetInWarehouseOptions(string search, int warehouse, int page)
        {
            return Json(await itemRepository.GetInWarehouseOptions(search, warehouse, page));
        }

        public async Task<IActionResult> GetUnitOptions(int? IncludeId)
        {
            return Json(await itemRepository.GetUnitOptions(IncludeId));
        }

        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Item, AccessTypes.View);
            var redirectResultBasic = await CheckAccessAndRedirectAsync(PageNames.ItemBasic, AccessTypes.View);
            if (redirectResult != null && redirectResultBasic != null) return redirectResultBasic;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.Item)).Convert();
            ViewData["BarcotechUserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.BarcotechStock)).Convert();

            //ItemFilterVM itemFilterVM = new();
            //itemFilterVM.Categories = new(await categoryRepository.GetAllActiveCategoryAsync(-1), "Id", "Name");
            //itemFilterVM.ItemTypes = new(await itemTypeRepository.GetAllActiveItemTypeAsync(-1), "Id", "Name");
            //itemFilterVM.Brands = new(await brandRepository.GetAllActiveBrandAsync(-1), "Id", "Name");
            //itemFilterVM.Warehouses = new(await warehouseRepository.GetAllActiveWarehouseAsync(-1), "Id", "Name");
            //itemFilterVM.ActiveStatus = new(new[]
            //{
            //    new { Id = 1, Name = "Active" },
            //    new { Id = 0, Name = "Inactive" }
            //}, "Id", "Name");
            //ViewData["ItemFilter"] = itemFilterVM;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetListJson(ItemFilterVM filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Item, AccessTypes.View);
            var redirectResultBasic = await CheckAccessAndRedirectAsync(PageNames.ItemBasic, AccessTypes.View);
            if (redirectResult != null && redirectResultBasic != null) return redirectResultBasic;

            DataTablesRequestForm form = new(Request.Form);
            return Json(await itemRepository.GetAllForDataTablesAsync(form, filter));
        }

        [HttpPost]
        public async Task<IActionResult> CreateJson(ItemCommonVM itemVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Item, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            ResponseModel responseModel = new();
            if (await itemRepository.IsCodeExistAsync(itemVM.Code))
            {
                responseModel.Status = "Error";
                responseModel.Message = "Item code already in use";
            }
            else
            {
                await itemRepository.AddAsync(itemVM);
                responseModel.Status = "Success";
                responseModel.Message = "Item successfully added";
            }
            return Json(responseModel);
        }


        [HttpPost]
        public async Task<IActionResult> EditJson(ItemCommonVM itemVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Item, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            ResponseModel responseModel = new();
            try
            {
                await itemRepository.UpdateAsync(itemVM);
                responseModel.Status = "Success";
                responseModel.Message = "Item successfully updated";
            }
            catch (Exception ex)
            {
                if (!await itemRepository.IsIdExistAsync(itemVM.Id))
                {
                    responseModel.Status = "Error";
                    responseModel.Message = "Item cannot be found";
                }
                else
                {
                    responseModel.Status = "Error";
                    responseModel.Message = SystemFormat.GetInnermostExceptionMessage(ex);
                }
            }
            return Json(responseModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Item, AccessTypes.View);
            var redirectResultBasic = await CheckAccessAndRedirectAsync(PageNames.ItemBasic, AccessTypes.View);
            if (redirectResult != null && redirectResultBasic != null) return redirectResultBasic;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.Item)).Convert();
            ViewData["BarcotechUserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.BarcotechStock)).Convert();

            var item = itemRepository.GetItemViewVM(id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> DetailsJson(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Item, AccessTypes.View);
            var redirectResultBasic = await CheckAccessAndRedirectAsync(PageNames.ItemBasic, AccessTypes.View);
            if (redirectResult != null && redirectResultBasic != null) return redirectResultBasic;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.Item)).Convert();

            return Json(itemRepository.GetItemViewVM(id));
        }

        [HttpPost]
        public async Task<IActionResult> EditDetailQuantity(int id, int quantity, int? locationCode, string remarks)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Item, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            ResponseModel responseModel = new();
            await itemRepository.UpdateQuantityAsync(id, quantity, locationCode, remarks);
            responseModel.Status = "Success";
            responseModel.Message = "Warehouse details successfully updated";
            return Json(responseModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteWarehouse(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Item, AccessTypes.Delete);
            if (redirectResult != null) return redirectResult;

            ResponseModel responseModel = new();
            await itemRepository.DeleteWarehouse(id);
            responseModel.Status = "Success";
            responseModel.Message = "Item warehouse successfully deleted";
            return Json(responseModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Item, AccessTypes.Delete);
            if (redirectResult != null) return redirectResult;

            if (await itemRepository.IsIdExistAsync(id) == false)
            {
                return NotFound();
            }
            if (await itemRepository.IsInUseAsync(id))
            {
                await itemRepository.DeactivateAsync(id);
                TempData["WarningMessage"] = "Item has been deactivated";
            }
            else
            {
                await itemRepository.DeleteAsync(id);
                TempData["SuccessMessage"] = "Item successfully deleted";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reactivate(int id)
        {
            var redirectResultAdd = await CheckAccessAndRedirectAsync(PageNames.Item, AccessTypes.Add);
            var redirectResultEdit = await CheckAccessAndRedirectAsync(PageNames.Item, AccessTypes.Edit);
            if (redirectResultAdd != null && redirectResultEdit != null) return redirectResultAdd;

            if (await itemRepository.IsIdExistAsync(id) == false)
            {
                return NotFound();
            }
            await itemRepository.ReactivateAsync(id);
            TempData["SuccessMessage"] = "Item reactivated successfully";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> UploadJson(ItemFileVM itemVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Item, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            return Json(new ResponseModel()
            {
                Status = "Success",
                Message = await itemRepository.SaveItemFromFile(itemVM)
            });
        }

        [HttpPost]
        public async Task<IActionResult> UploadFileJson(IFormFile file)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Item, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            var response = await itemRepository.UploadFile(file);

            Response.Headers["X-Status"] = response.Status;
            Response.Headers["X-Message"] = response.Message;
            return File(response.Data.Content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        response.Data.FileName);
        }

        [HttpPost]
        public async Task<IActionResult> SaveItemPromoJson(ItemPromoVM form)
        {
            var response = new ResponseModel();
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Item, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            if (form.Id == 0)
            {
                var dateTime = DateTime.Now;

                if (dateTime > form.ExpirationDateD)
                {
                    response.Message = "Cannot set Expiration Date as past date.";
                    response.Status = "Error";

                    return Json(response);
                }

            }

            if (form.EffectiveDateD > form.ExpirationDateD)
            {
                response.Message = "Effective Date cannot be past the Expiration Date.";
                response.Status = "Error";

            }

            else
            {
                response = await itemRepository.ItemPromoAddAsync(form);
            }

            return Json(response);

        }

        [HttpPost]
        public async Task<IActionResult> ReactivateItemPromoJson(int id, int itemId, DateTime start, DateTime end)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Item, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            var response = await itemRepository.ReactivateItemPromoAsync(id, itemId, start, end);

            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteOrDeactivateItemPromoJson(int Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Item, AccessTypes.Delete);
            if (redirectResult != null) return redirectResult;

            var response = await itemRepository.DeleteOrDeactivateItemPromoAsync(Id);

            return Json(response);
        }

        public async Task<IActionResult> DownloadFile(ItemFilterVM filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Item, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            DataTable dt = new("Items");
            dt.Columns.AddRange([
                new DataColumn("Code"),
                new DataColumn("Name"),
                new DataColumn("Old SKU"),
                new DataColumn("Part Number"),
                new DataColumn("Interchange Number"),
                new DataColumn("Warehouse"),
                new DataColumn("Model Number"),
                new DataColumn("Item Class"),
                new DataColumn("Category"),
                new DataColumn("Item Type"),
                new DataColumn("Brand"),
                new DataColumn("Size"),
                new DataColumn("Unit"),
                new DataColumn("Quantity on Hand"),
                new DataColumn("Beginning Cost"),
                new DataColumn("Gross Price"),
                new DataColumn("Net Price"),
                new DataColumn("Is Active")
            ]);
            //var items = await itemRepository.GetAllFileAsync();
            var items = await itemRepository.GetAllAsync(filter);

            foreach (var item in items)
            {
                dt.Rows.Add(item.Code,
                            item.Name,
                            item.OldCode,
                            item.PartNumber,
                            item.InterchangeNumber,
                            item.Warehouse,
                            item.ModelNumber,
                            item.ItemClass,
                            item.Category,
                            item.ItemType,
                            item.Brand,
                            item.Size,
                            item.Unit,
                            item.QuantityD,
                            item.BeginningCostD,
                            item.RetailPriceD,
                            item.NetPriceD,
                            item.IsActive ? "Yes" : "No");
            }

            using XLWorkbook wb = new();
            wb.Worksheets.Add(dt);
            using MemoryStream stream = new();
            wb.SaveAs(stream);
            return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        DateTime.Now.ToString("yyyyMMdd") + " Items.xlsx");
        }

        [HttpPost]
        public async Task<IActionResult> GetWarehousesJson(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Item, AccessTypes.View);
            var redirectResultBasic = await CheckAccessAndRedirectAsync(PageNames.ItemBasic, AccessTypes.View);
            if (redirectResult != null && redirectResultBasic != null) return redirectResultBasic;

            DataTablesRequestForm form = new(Request.Form);
            return Json(await itemRepository.GetItemWarehouseDTAsync(form, id));
        }

        [HttpPost]
        public async Task<IActionResult> GetPurchasesJson(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Item, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm form = new(Request.Form);
            return Json(await itemRepository.GetItemPurchaseDTAsync(form, id));
        }

        [HttpPost]
        public async Task<IActionResult> GetSalesJson(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Item, AccessTypes.View);
            var redirectResultBasic = await CheckAccessAndRedirectAsync(PageNames.ItemBasic, AccessTypes.View);
            if (redirectResult != null && redirectResultBasic != null) return redirectResultBasic;

            DataTablesRequestForm form = new(Request.Form);
            return Json(await itemRepository.GetItemSalesDTAsync(form, id));
        }

        [HttpPost]
        public async Task<IActionResult> GetCountsJson(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Item, AccessTypes.View);
            var redirectResultBasic = await CheckAccessAndRedirectAsync(PageNames.ItemBasic, AccessTypes.View);
            if (redirectResult != null && redirectResultBasic != null) return redirectResultBasic;

            DataTablesRequestForm form = new(Request.Form);
            return Json(await itemRepository.GetItemCountDTAsync(form, id));
        }


        public async Task<IActionResult> DownloadCounts(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Item, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            var details = itemRepository.GetItemViewVM(id);

            DataTable dt = new("Physical Count Adjustment");
            dt.Columns.AddRange([
                new DataColumn("Warehouse"),
                new DataColumn("Quantity From"),
                new DataColumn("Quantity To"),
                new DataColumn("Quantity Adjusted"),
                new DataColumn("Adjustment Date"),
                new DataColumn("Adjustment By"),
                new DataColumn("Remarks")
            ]);
            var pcas = await itemRepository.GetItemCountAsync(id);
            foreach (var pca in pcas)
            {
                dt.Rows.Add(pca.Warehouse,
                            pca.QuantityFromD,
                            pca.QuantityToD,
                            pca.QuantityAdjustedD,
                            pca.AdjustmentDate,
                            pca.AdjustmentBy,
                            pca.Remarks
                            );
            }

            using XLWorkbook wb = new();
            wb.Worksheets.Add(dt);
            using MemoryStream stream = new();
            wb.SaveAs(stream);
            return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        SystemFormat.ToValidFileName(DateTime.Now.ToString("yyyyMMdd") + " " + details!.Name + " Physical Count Adjustment.xlsx"));
        }

        [HttpPost]
        public async Task<IActionResult> GetPricesJson(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Item, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm form = new(Request.Form);
            return Json(await itemRepository.GetItemPriceDTAsync(form, id));
        }

        [HttpPost]
        public async Task<IActionResult> GetPromoJson(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Item, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm form = new(Request.Form);
            return Json(await itemRepository.GetItemPromoDTAsync(form, id));
        }

        [HttpPost]
        public async Task<IActionResult> GetTransfersJson(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Item, AccessTypes.View);
            var redirectResultBasic = await CheckAccessAndRedirectAsync(PageNames.ItemBasic, AccessTypes.View);
            if (redirectResult != null && redirectResultBasic != null) return redirectResultBasic;

            DataTablesRequestForm form = new(Request.Form);
            return Json(await itemRepository.GetItemTransferDTAsync(form, id));
        }

        [HttpPost]
        public async Task<IActionResult> GetReturnsJson(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Item, AccessTypes.View);
            var redirectResultBasic = await CheckAccessAndRedirectAsync(PageNames.ItemBasic, AccessTypes.View);
            if (redirectResult != null && redirectResultBasic != null) return redirectResultBasic;

            DataTablesRequestForm form = new(Request.Form);
            return Json(await itemRepository.GetItemReturnDTAsync(form, id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateItemConversionJson(ItemDetailConversionVM itemConversionVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Item, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            ResponseModel responseModel = new();
            if (await itemConversionRepository.IsConversionExistAsync(itemConversionVM.ItemId, itemConversionVM.SubItemId, itemConversionVM.UnitId, itemConversionVM.EquivalentUnitId))
            {
                responseModel.Status = "Error";
                responseModel.Message = "Item conversion already exist";
            }
            else
            {
                await itemConversionRepository.AddAsync(itemConversionVM);
                responseModel.Status = "Success";
                responseModel.Message = "Item conversion successfully added";
            }
            return Json(responseModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetConversionListJson(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Item, AccessTypes.View);
            var redirectResultBasic = await CheckAccessAndRedirectAsync(PageNames.ItemBasic, AccessTypes.View);
            if (redirectResult != null && redirectResultBasic != null) return redirectResultBasic;

            DataTablesRequestForm form = new(Request.Form);
            return Json(await itemConversionRepository.GetAllForDataTablesAsync(form, id));
        }

        [HttpPost]
        public async Task<IActionResult> EditConversionJson(ItemDetailConversionVM itemDetailConversionVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Item, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            ResponseModel responseModel = new();
            try
            {
                await itemConversionRepository.UpdateAsync(itemDetailConversionVM);
                responseModel.Status = "Success";
                responseModel.Message = "Item successfully updated";
            }
            catch (Exception ex)
            {
                if (!await itemConversionRepository.IsIdExistAsync(itemDetailConversionVM.Id))
                {
                    responseModel.Status = "Error";
                    responseModel.Message = "Item conversion cannot be found";
                }
                else
                {
                    responseModel.Status = "Error";
                    responseModel.Message = SystemFormat.GetInnermostExceptionMessage(ex);
                }
            }
            return Json(responseModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetItemConversionVM(int itemId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Item, AccessTypes.View);
            var redirectResultBasic = await CheckAccessAndRedirectAsync(PageNames.ItemBasic, AccessTypes.View);
            if (redirectResult != null && redirectResultBasic != null) return redirectResultBasic;

            return Json(itemConversionRepository.GetItemConversionVM(itemId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConversion(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.LocationCodes, AccessTypes.Delete);
            if (redirectResult != null) return redirectResult;

            var conversion = await itemConversionRepository.GetAsync(id);
            if (conversion == null)
            {
                return NotFound();
            }

            if (conversion.SubItemId != null || conversion.EquivalentUnitId == null)
            {
                await itemConversionRepository.DeleteAsync(id);
            }
            else
            {
                await itemConversionRepository.DeleteConversionAsync(conversion.ItemId, conversion.OrderNo);
            }

            TempData["SuccessMessage"] = "Item conversion successfully deleted";
            return RedirectToAction("Details", new { Id = conversion.ItemId });
        }

        [HttpPost]
        public async Task<IActionResult> SaveImageAsync(ItemImageFormVM form)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Item, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            var response = await itemRepository.SaveImageAsync(form);
            return Json(response);
        }

        public async Task<IActionResult> SyncStock(int? ItemId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.BarcotechStock, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            var response = await apiBCTService.SyncStock(apiBCTOptions.Value.URLStock, ItemId);
            return Json(response);
        }
    }
}