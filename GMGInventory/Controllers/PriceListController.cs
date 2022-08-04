using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GMGInventory.Controllers
{
    [Authorize(Roles = Roles.InventoryManager + "," + Roles.SalesDirector + "," + Roles.SystemAdmin + "," + Roles.Telemarketer)]
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
            return View();
        }

        [HttpPost]
        public JsonResult GetListJson(ItemFilterVM filter)
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            int totalRecord = 0;
            int filterRecord = 0;
            var data = itemPriceRepository.GetAllItemPrice(filter);
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Brand.ToLower().Contains(form.SearchValue)
                                    || x.Category.ToLower().Contains(form.SearchValue)
                                    || x.Code.ToLower().Contains(form.SearchValue)
                                    || x.ItemType.ToLower().Contains(form.SearchValue)
                                    || x.ModelNumber.ToLower().Contains(form.SearchValue)
                                    || x.Name.ToLower().Contains(form.SearchValue)
                                    || x.NetPrice.ToString().ToLower().Contains(form.SearchValue)
                                    || x.PartNumber.ToLower().Contains(form.SearchValue)
                                    || x.Quantity.ToString().ToLower().Contains(form.SearchValue)
                                    || x.RetailPrice.ToString().ToLower().Contains(form.SearchValue)
                                    || x.Size.ToLower().Contains(form.SearchValue)
                                    || x.Unit.ToLower().Contains(form.SearchValue)
                                    || x.UserCreated.ToLower().Contains(form.SearchValue)
                                    || x.DateCreated.ToLower().Contains(form.SearchValue));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                if (form.SortColumnDirection == "asc")
                {
                    switch (form.SortColumn)
                    {
                        case "Brand": data = data.OrderBy(s => s.Brand); break;
                        case "Category": data = data.OrderBy(s => s.Category); break;
                        case "Code": data = data.OrderBy(s => s.Code); break;
                        case "ItemType": data = data.OrderBy(s => s.ItemType); break;
                        case "ModelNumber": data = data.OrderBy(s => s.ModelNumber); break;
                        case "Name": data = data.OrderBy(s => s.Name); break;
                        case "NetPrice": data = data.OrderBy(s => s.NetPrice); break;
                        case "PartNumber": data = data.OrderBy(s => s.PartNumber); break;
                        case "Quantity": data = data.OrderBy(s => s.Quantity); break;
                        case "RetailPrice": data = data.OrderBy(s => s.RetailPrice); break;
                        case "Size": data = data.OrderBy(s => s.Size); break;
                        case "Unit": data = data.OrderBy(s => s.Unit); break;
                        case "UserCreated": data = data.OrderBy(s => s.UserCreated); break;
                        case "DateCreated": data = data.OrderBy(s => s.DateCreatedD); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Brand": data = data.OrderByDescending(s => s.Brand); break;
                        case "Category": data = data.OrderByDescending(s => s.Category); break;
                        case "Code": data = data.OrderByDescending(s => s.Code); break;
                        case "ItemType": data = data.OrderByDescending(s => s.ItemType); break;
                        case "ModelNumber": data = data.OrderByDescending(s => s.ModelNumber); break;
                        case "Name": data = data.OrderByDescending(s => s.Name); break;
                        case "NetPrice": data = data.OrderByDescending(s => s.NetPrice); break;
                        case "PartNumber": data = data.OrderByDescending(s => s.PartNumber); break;
                        case "Quantity": data = data.OrderByDescending(s => s.Quantity); break;
                        case "RetailPrice": data = data.OrderByDescending(s => s.RetailPrice); break;
                        case "Size": data = data.OrderByDescending(s => s.Size); break;
                        case "Unit": data = data.OrderByDescending(s => s.Unit); break;
                        case "UserCreated": data = data.OrderByDescending(s => s.UserCreated); break;
                        case "DateCreated": data = data.OrderByDescending(s => s.DateCreatedD); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return Json(new
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            });
        }
    }
}