using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GMGInventory.Controllers
{
    [Authorize(Roles = Roles.InventoryManager + ","
                     + Roles.SalesDirector + ","
                     + Roles.SystemAdmin + ","
                     + Roles.Telemarketer)]
    public class PriceListController(IAccessRepository accessRepository
                             , IItemPriceRepository itemPriceRepository
                             , ICategoryRepository categoryRepository
                             , IItemTypeRepository itemTypeRepository
                             , IBrandRepository brandRepository
                             , UserManager<User> userManager) : BaseController(accessRepository, userManager)
    {

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
    }
}