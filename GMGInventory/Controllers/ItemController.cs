using ClosedXML.Excel;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        public IItemRepository itemRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IItemTypeRepository itemTypeRepository;
        private readonly IBrandRepository brandRepository;
        private readonly IWarehouseRepository warehouseRepository;

        public ItemController(IItemRepository itemRepository
            , ICategoryRepository categoryRepository
            , IItemTypeRepository itemTypeRepository
            , IBrandRepository brandRepository
            , IWarehouseRepository warehouseRepository)
        {
            this.itemRepository = itemRepository;
            this.categoryRepository = categoryRepository;
            this.itemTypeRepository = itemTypeRepository;
            this.brandRepository = brandRepository;
            this.warehouseRepository = warehouseRepository;
        }

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

        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.InvoicingClerk + ","
                         + Roles.SalesDirector + ","
                         + Roles.SystemAdmin + ","
                         + Roles.Telemarketer)]
        public async Task<ActionResult> Index()
        {
            ItemFilterVM itemFilterVM = new ItemFilterVM();
            itemFilterVM.Categories = new SelectList(await categoryRepository.GetAllActiveCategoryAsync(-1), "Id", "Name");
            itemFilterVM.ItemTypes = new SelectList(await itemTypeRepository.GetAllActiveItemTypeAsync(-1), "Id", "Name");
            itemFilterVM.Brands = new SelectList(await brandRepository.GetAllActiveBrandAsync(-1), "Id", "Name");
            itemFilterVM.Warehouses = new SelectList(await warehouseRepository.GetAllActiveWarehouseAsync(-1), "Id", "Name");
            ViewData["ItemFilter"] = itemFilterVM;
            return View();
        }

        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.InvoicingClerk + ","
                         + Roles.SalesDirector + ","
                         + Roles.SystemAdmin + ","
                         + Roles.Telemarketer)]
        [HttpPost]
        public async Task<JsonResult> GetListJson(ItemFilterVM filter)
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await itemRepository.GetAllForDataTablesAsync(form, filter));
        }

        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.SalesDirector + ","
                         + Roles.SystemAdmin + ","
                         + Roles.Telemarketer)]
        [HttpPost]
        public async Task<JsonResult> CreateJson(ItemCommonVM itemVM)
        {
            ResponseModel responseModel = new ResponseModel();
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

        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.SalesDirector + ","
                         + Roles.SystemAdmin + ","
                         + Roles.Telemarketer)]
        [HttpPost]
        public async Task<JsonResult> EditJson(ItemCommonVM itemVM)
        {
            ResponseModel responseModel = new ResponseModel();
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


        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.InvoicingClerk + ","
                         + Roles.SalesDirector + ","
                         + Roles.SystemAdmin + ","
                         + Roles.Telemarketer)]
        public ActionResult Details(int id)
        {
            var item = itemRepository.GetItemViewVM(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }


        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.InvoicingClerk + ","
                         + Roles.SalesDirector + ","
                         + Roles.SystemAdmin + ","
                         + Roles.Telemarketer)]
        [HttpPost]
        public ActionResult DetailsJson(int id)
        {
            return Json(itemRepository.GetItemViewVM(id));
        }


        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.SalesDirector + ","
                         + Roles.SystemAdmin + ","
                         + Roles.Telemarketer)]
        [HttpPost]
        public async Task<JsonResult> EditDetailQuantity(int id, int quantity, string remarks)
        {
            ResponseModel responseModel = new ResponseModel();
            await itemRepository.UpdateQuantityAsync(id, quantity, remarks);
            responseModel.Status = "Success";
            responseModel.Message = "Item quantity successfully updated";
            return Json(responseModel);
        }


        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.SalesDirector + ","
                         + Roles.SystemAdmin + ","
                         + Roles.Telemarketer)]
        [HttpPost]
        public async Task<JsonResult> DeleteWarehouse(int id)
        {
            ResponseModel responseModel = new ResponseModel();
            await itemRepository.DeleteWarehouse(id);
            responseModel.Status = "Success";
            responseModel.Message = "Item warehouse successfully deleted";
            return Json(responseModel);
        }


        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.SalesDirector + ","
                         + Roles.SystemAdmin + ","
                         + Roles.Telemarketer)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
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


        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.SalesDirector + ","
                         + Roles.SystemAdmin + ","
                         + Roles.Telemarketer)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reactivate(int id)
        {
            if (await itemRepository.IsIdExistAsync(id) == false)
            {
                return NotFound();
            }
            await itemRepository.ReactivateAsync(id);
            TempData["SuccessMessage"] = "Item reactivated successfully";
            return RedirectToAction(nameof(Index));
        }


        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.SalesDirector + ","
                         + Roles.SystemAdmin + ","
                         + Roles.Telemarketer)]
        [HttpPost]
        public async Task<JsonResult> UploadJson(ItemFileVM itemVM)
        {
            ResponseModel responseModel = new ResponseModel();
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

        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.InvoicingClerk + ","
                         + Roles.SalesDirector + ","
                         + Roles.SystemAdmin + ","
                         + Roles.Telemarketer)]
        public async Task<IActionResult> DownloadFile(ItemFilterVM filter)
        {
            DataTable dt = new DataTable("Items");
            dt.Columns.AddRange(new DataColumn[12] {
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
            });
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

            using (XLWorkbook wb = new())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        DateTime.Now.ToString("yyyyMMdd") + " Items.xlsx");
                }
            }
        }

        [HttpPost]
        public async Task<JsonResult> GetWarehousesJson(int id)
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await itemRepository.GetItemWarehouseDTAsync(form, id));
        }

        [HttpPost]
        public async Task<JsonResult> GetPurchasesJson(int id)
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await itemRepository.GetItemPurchaseDTAsync(form, id));
        }

        [HttpPost]
        public async Task<JsonResult> GetSalesJson(int id)
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await itemRepository.GetItemSalesDTAsync(form, id));
        }

        [HttpPost]
        public async Task<JsonResult> GetCountsJson(int id)
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await itemRepository.GetItemCountDTAsync(form, id));
        }

        [HttpPost]
        public async Task<JsonResult> GetPricesJson(int id)
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await itemRepository.GetItemPriceDTAsync(form, id));
        }

        [HttpPost]
        public async Task<JsonResult> GetTransfersJson(int id)
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await itemRepository.GetItemTransferDTAsync(form, id));
        }

        [HttpPost]
        public async Task<JsonResult> GetReturnsJson(int id)
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await itemRepository.GetItemReturnDTAsync(form, id));
        }
    }
}