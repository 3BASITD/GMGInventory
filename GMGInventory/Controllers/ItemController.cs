using ClosedXML.Excel;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class ItemController(IAccessRepository accessRepository
                              , IItemRepository itemRepository
                              , ICategoryRepository categoryRepository
                              , IItemTypeRepository itemTypeRepository
                              , IBrandRepository brandRepository
                              , IWarehouseRepository warehouseRepository) : BaseController(accessRepository)
    {
        public async Task<IActionResult> GetOptions(string search, int page)
        {
            return Json(await itemRepository.GetOptions(search, page));
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
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.Item)).Convert();

            ItemFilterVM itemFilterVM = new();
            itemFilterVM.Categories = new(await categoryRepository.GetAllActiveCategoryAsync(-1), "Id", "Name");
            itemFilterVM.ItemTypes = new(await itemTypeRepository.GetAllActiveItemTypeAsync(-1), "Id", "Name");
            itemFilterVM.Brands = new(await brandRepository.GetAllActiveBrandAsync(-1), "Id", "Name");
            itemFilterVM.Warehouses = new(await warehouseRepository.GetAllActiveWarehouseAsync(-1), "Id", "Name");
            ViewData["ItemFilter"] = itemFilterVM;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetListJson(ItemFilterVM filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Item, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

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
                    responseModel.Message = ex.Message;
                }
            }
            return Json(responseModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Item, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.Item)).Convert();

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
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.Item)).Convert();

            return Json(itemRepository.GetItemViewVM(id));
        }

        [HttpPost]
        public async Task<IActionResult> EditDetailQuantity(int id, int quantity, string remarks)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Item, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            ResponseModel responseModel = new();
            await itemRepository.UpdateQuantityAsync(id, quantity, remarks);
            responseModel.Status = "Success";
            responseModel.Message = "Item quantity successfully updated";
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

            ResponseModel responseModel = new();
            if (await itemRepository.UploadCombinationExistAsync(itemVM))
            {
                responseModel.Status = "Warning";
                responseModel.Message = "Nothing to change";
            }
            else
            {
                int? CategoryId = await categoryRepository.GetIdAsync(itemVM.Category);
                if (CategoryId == null)
                {
                    responseModel.Status = "Error";
                    responseModel.Message = "Unknown Category";
                }
                else
                {
                    int? ItemTypeId = await itemTypeRepository.GetIdAsync(itemVM.ItemType);
                    if (ItemTypeId == null)
                    {
                        responseModel.Status = "Error";
                        responseModel.Message = "Unknown Item Type";
                    }
                    else
                    {
                        int? BrandId = await brandRepository.GetIdAsync(itemVM.Brand);
                        if (BrandId == null)
                        {
                            responseModel.Status = "Error";
                            responseModel.Message = "Unknown Brand";
                        }
                        else
                        {
                            int? UnitId = await itemRepository.GetUnitIdByNameAsync(itemVM.Unit);
                            if (UnitId == null)
                            {
                                responseModel.Status = "Error";
                                responseModel.Message = "Unknown Unit";
                            }
                            else
                            {
                                int? warehouseId = await warehouseRepository.GetIdAsync(itemVM.Warehouse);
                                if (warehouseId == null)
                                {
                                    responseModel.Status = "Error";
                                    responseModel.Message = "Unknown Warehouse";
                                }
                                else
                                {
                                    if (await itemRepository.IsCodeExistAsync(itemVM.Code))
                                    {
                                        int itemId = (int)await itemRepository.GetIdAsync(itemVM.Code);

                                        await itemRepository.UpdateAsync(new ItemCommonVM
                                        {
                                            BrandId = BrandId.Value,
                                            CategoryId = CategoryId.Value,
                                            Id = itemId,
                                            InterchangeNumber = itemVM.InterchangeNumber,
                                            ItemTypeId = ItemTypeId.Value,
                                            ModelNumber = itemVM.ModelNumber,
                                            Name = itemVM.Name,
                                            PartNumber = itemVM.PartNumber,
                                            Size = itemVM.Size,
                                            UnitId = UnitId.Value,
                                            WarehouseId = warehouseId.Value,
                                            QuantityD = itemVM.Quantity
                                        });

                                        await itemRepository.UpdateOrAddItemWarehouse(itemId, warehouseId.Value, itemVM.Quantity, "Updated through file upload");

                                        responseModel.Status = "Success";
                                        responseModel.Message = "Item updated";
                                    }
                                    else
                                    {
                                        await itemRepository.AddAsync(new ItemCommonVM
                                        {
                                            BrandId = BrandId.Value,
                                            CategoryId = CategoryId.Value,
                                            Code = itemVM.Code,
                                            InterchangeNumber = itemVM.InterchangeNumber,
                                            ItemTypeId = ItemTypeId.Value,
                                            ModelNumber = itemVM.ModelNumber,
                                            Name = itemVM.Name,
                                            NetPriceD = 0,
                                            PartNumber = itemVM.PartNumber,
                                            RetailPriceD = 0,
                                            Size = itemVM.Size,
                                            UnitId = UnitId.Value,
                                            QuantityD = itemVM.Quantity
                                        });
                                        responseModel.Status = "Success";
                                        responseModel.Message = "Item added";
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return Json(responseModel);
        }

        public async Task<IActionResult> DownloadFile(ItemFilterVM filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Item, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            DataTable dt = new("Items");
            dt.Columns.AddRange([
                new DataColumn("Code"),
                new DataColumn("Name"),
                new DataColumn("Part Number"),
                new DataColumn("Interchange Number"),
                new DataColumn("Warehouse"),
                new DataColumn("Model Number"),
                new DataColumn("Category"),
                new DataColumn("Item Type"),
                new DataColumn("Brand"),
                new DataColumn("Size"),
                new DataColumn("Unit"),
                new DataColumn("Quantity on Hand")
            ]);
            //var items = await itemRepository.GetAllFileAsync();
            var items = (await itemRepository.GetAllAsync(filter)).Where(s=>s.IsActive == true);

            foreach (var item in items)
            {
                dt.Rows.Add(item.Code,
                            item.Name,
                            item.PartNumber,
                            item.InterchangeNumber,
                            item.Warehouse,
                            item.ModelNumber,
                            item.Category,
                            item.ItemType,
                            item.Brand,
                            item.Size,
                            item.Unit,
                            item.Quantity);
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
            if (redirectResult != null) return redirectResult;

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
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm form = new(Request.Form);
            return Json(await itemRepository.GetItemSalesDTAsync(form, id));
        }

        [HttpPost]
        public async Task<IActionResult> GetCountsJson(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Item, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm form = new(Request.Form);
            return Json(await itemRepository.GetItemCountDTAsync(form, id));
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
        public async Task<IActionResult> GetTransfersJson(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Item, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm form = new(Request.Form);
            return Json(await itemRepository.GetItemTransferDTAsync(form, id));
        }

        [HttpPost]
        public async Task<IActionResult> GetReturnsJson(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Item, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm form = new(Request.Form);
            return Json(await itemRepository.GetItemReturnDTAsync(form, id));
        }
    }
}