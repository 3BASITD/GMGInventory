using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GMGInventory.Controllers
{
    [Authorize(Roles = Roles.InventoryManager + "," + Roles.SalesDirector + "," + Roles.Telemarketer)]
    public class PriceListController : Controller
    {
        private readonly IItemPriceRepository itemPriceRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IItemTypeRepository itemTypeRepository;
        private readonly IBrandRepository brandRepository;

        public PriceListController(IItemPriceRepository itemPriceRepository
            , ICategoryRepository categoryRepository
            , IItemTypeRepository itemTypeRepository
            , IBrandRepository brandRepository)
        {
            this.itemPriceRepository = itemPriceRepository;
            this.categoryRepository = categoryRepository;
            this.itemTypeRepository = itemTypeRepository;
            this.brandRepository = brandRepository;
        }

        public async Task<ActionResult> Index()
        {
            ItemFilterVM itemFilterVM = new ItemFilterVM();
            itemFilterVM.Categories = new SelectList(await categoryRepository.GetAllActiveCategoryAsync(-1), "Id", "Name");
            itemFilterVM.ItemTypes = new SelectList(await itemTypeRepository.GetAllActiveItemTypeAsync(-1), "Id", "Name");
            itemFilterVM.Brands = new SelectList(await brandRepository.GetAllActiveBrandAsync(-1), "Id", "Name");
            ViewData["ItemFilter"] = itemFilterVM;
            var itemPricesVM = await itemPriceRepository.GetAllAsync(itemFilterVM);
            return View(itemPricesVM);
        }

        [HttpPost]
        public async Task<ActionResult> Index(ItemFilterVM itemFilterVM)
        {
            itemFilterVM.Categories = new SelectList(await categoryRepository.GetAllActiveCategoryAsync(itemFilterVM.CategoryId ?? -1), "Id", "Name", itemFilterVM.CategoryId);
            itemFilterVM.ItemTypes = new SelectList(await itemTypeRepository.GetAllActiveItemTypeAsync(itemFilterVM.ItemTypeId ?? -1), "Id", "Name", itemFilterVM.ItemTypeId);
            itemFilterVM.Brands = new SelectList(await brandRepository.GetAllActiveBrandAsync(itemFilterVM.BrandId ?? -1), "Id", "Name", itemFilterVM.BrandId);
            ViewData["ItemFilter"] = itemFilterVM;
            var itemPricesVM = await itemPriceRepository.GetAllAsync(itemFilterVM);
            return View(itemPricesVM);
        }
    }
}
