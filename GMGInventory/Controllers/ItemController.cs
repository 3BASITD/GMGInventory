using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GMGInventory.Controllers
{
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

        public async Task<IActionResult> GetInWarehouseOptions(string search, int warehouse, int page)
        {
            return Json(await itemRepository.GetInWarehouseOptions(search, warehouse, page));
        }

        public async Task<IActionResult> GetUnitOptions(int? IncludeId)
        {
            return Json(await itemRepository.GetUnitOptions(IncludeId));
        }

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

        [HttpPost]
        public async Task<JsonResult> GetListJson(ItemFilterVM filter)
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await itemRepository.GetAllForDataTablesAsync(form, filter));
        }

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

        public ActionResult Details(int id)
        {
            var item = itemRepository.GetItemViewVM(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [HttpPost]
        public async Task<JsonResult> EditDetailQuantity(int id, int quantity, string remarks)
        {
            ResponseModel responseModel = new ResponseModel();
            await itemRepository.UpdateQuantityAsync(id, quantity, remarks);
            responseModel.Status = "Success";
            responseModel.Message = "Item quantity successfully updated";
            return Json(responseModel);
        }

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
