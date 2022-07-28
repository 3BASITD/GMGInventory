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
            return View();
        }

        [HttpPost]
        public JsonResult GetListJson(ItemFilterVM itemFilterVM)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var draw = Request.Form["draw"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = Convert.ToInt32(Request.Form["length"].FirstOrDefault() ?? "0");
            int skip = Convert.ToInt32(Request.Form["start"].FirstOrDefault() ?? "0");
            var data = itemPriceRepository.GetAll(itemFilterVM);
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrEmpty(searchValue))
            {
                data = data.Where(x => x.Brand.ToLower().Contains(searchValue.ToLower())
                                    || x.Code.ToLower().Contains(searchValue.ToLower())
                                    || x.ModelNumber.ToLower().Contains(searchValue.ToLower())
                                    || x.Name.ToLower().Contains(searchValue.ToLower())
                                    || x.NetPrice.ToString().ToLower().Contains(searchValue.ToLower())
                                    || x.PartNumber.ToLower().Contains(searchValue.ToLower())
                                    || x.Quantity.ToString().ToLower().Contains(searchValue.ToLower())
                                    || x.RetailPrice.ToString().ToLower().Contains(searchValue.ToLower())
                                    || x.Size.ToLower().Contains(searchValue.ToLower())
                                    || x.Unit.ToLower().Contains(searchValue.ToLower()));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection))
            {
                //data = data.OrderBy(sortColumn + " " + sortColumnDirection);
                if (sortColumnDirection == "asc")
                {
                    switch (sortColumn)
                    {
                        case "Brand": data = data.OrderBy(s => s.Brand); break;
                        case "Code": data = data.OrderBy(s => s.Code); break;
                        case "ModelNumber": data = data.OrderBy(s => s.ModelNumber); break;
                        case "Name": data = data.OrderBy(s => s.Name); break;
                        case "NetPrice": data = data.OrderBy(s => s.NetPrice); break;
                        case "PartNumber": data = data.OrderBy(s => s.PartNumber); break;
                        case "Quantity": data = data.OrderBy(s => s.Quantity); break;
                        case "RetailPrice": data = data.OrderBy(s => s.RetailPrice); break;
                        case "Size": data = data.OrderBy(s => s.Size); break;
                        case "Unit": data = data.OrderBy(s => s.Unit); break;
                        default: break;
                    }
                }
                else if (sortColumnDirection == "desc")
                {
                    switch (sortColumn)
                    {
                        case "Brand": data = data.OrderByDescending(s => s.Brand); break;
                        case "Code": data = data.OrderByDescending(s => s.Code); break;
                        case "ModelNumber": data = data.OrderByDescending(s => s.ModelNumber); break;
                        case "Name": data = data.OrderByDescending(s => s.Name); break;
                        case "NetPrice": data = data.OrderByDescending(s => s.NetPrice); break;
                        case "PartNumber": data = data.OrderByDescending(s => s.PartNumber); break;
                        case "Quantity": data = data.OrderByDescending(s => s.Quantity); break;
                        case "RetailPrice": data = data.OrderByDescending(s => s.RetailPrice); break;
                        case "Size": data = data.OrderByDescending(s => s.Size); break;
                        case "Unit": data = data.OrderByDescending(s => s.Unit); break;
                        default: break;
                    }
                }
            }
            //pagination
            var empList = data.Skip(skip).Take(pageSize).ToList();
            var returnObj = new
            {
                draw = draw,
                recordsTotal = totalRecord,
                recordsFiltered = filterRecord,
                data = empList
            };
            return Json(returnObj);
        }
    }
}