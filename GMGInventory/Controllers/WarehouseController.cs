using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Mvc;


namespace GMGInventory.Controllers
{
    public class WarehouseController : Controller
    {
        private readonly IWarehouseRepository warehouseRepository;
        private readonly IItemRepository itemRepository;

        public WarehouseController(IWarehouseRepository warehouseRepository,
            IItemRepository itemRepository)
        {
            this.warehouseRepository = warehouseRepository;
            this.itemRepository = itemRepository;
        }

        public async Task<ActionResult> Index()
        {
            var warehouses = await warehouseRepository.GetAllAsync();
            return View(warehouses);
        }

        public async Task<ActionResult> Details(int id)
        {
            var warehouse = await warehouseRepository.GetAsync(id);
            if (warehouse == null)
            {
                return NotFound();
            }
            var itemFilter = new ItemFilterVM() { WarehouseId = warehouse.Id };
            return View(warehouse);
        }

        [HttpPost]
        public JsonResult GetItemListJson(ItemFilterVM itemFilterVM)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var draw = Request.Form["draw"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = Convert.ToInt32(Request.Form["length"].FirstOrDefault() ?? "0");
            int skip = Convert.ToInt32(Request.Form["start"].FirstOrDefault() ?? "0");
            var data = itemRepository.GetAll(itemFilterVM);
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrEmpty(searchValue))
            {
                data = data.Where(x => x.Code.ToLower().Contains(searchValue.ToLower())
                                || x.Name.ToLower().Contains(searchValue.ToLower())
                                || x.NetPrice.ToString().ToLower().Contains(searchValue.ToLower())
                                || x.RetailPrice.ToString().ToLower().Contains(searchValue.ToLower()));
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
                        case "Code": data = data.OrderBy(s => s.Code); break;
                        case "Name": data = data.OrderBy(s => s.Name); break;
                        case "NetPrice": data = data.OrderBy(s => s.NetPrice); break;
                        case "RetailPrice": data = data.OrderBy(s => s.RetailPrice); break;
                        default: break;
                    }
                }
                else if (sortColumnDirection == "desc")
                {
                    switch (sortColumn)
                    {
                        case "Code": data = data.OrderByDescending(s => s.Code); break;
                        case "Name": data = data.OrderByDescending(s => s.Name); break;
                        case "NetPrice": data = data.OrderByDescending(s => s.NetPrice); break;
                        case "RetailPrice": data = data.OrderByDescending(s => s.RetailPrice); break;
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

        public async Task<ActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(WarehouseFormVM warehouseVM)
        {
            if (ModelState.IsValid)
            {
                if (await warehouseRepository.IsCodeExists(warehouseVM.Code))
                {
                    TempData["ErrorMessage"] = "Code already in use";
                }
                else
                {
                    await warehouseRepository.AddAsync(warehouseVM);
                    TempData["SuccessMessage"] = "Warehouse successfully created";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(warehouseVM);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var warehouse = await warehouseRepository.GetToEditAsync(id);
            if (warehouse == null)
            {
                return NotFound();
            }

            return View(warehouse);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(WarehouseFormVM warehouseVM)
        {
            if (ModelState.IsValid)
            {
                await warehouseRepository.UpdateAsync(warehouseVM);
                TempData["SuccessMessage"] = "Warehouse successfully updated";
                return RedirectToAction(nameof(Index));
            }
            return View(warehouseVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (await warehouseRepository.IsIdExists(id) != true)
            {
                return NotFound();
            }
            if (await warehouseRepository.IsInUse(id))
            {
                await warehouseRepository.DeactivateAsync(id);
                TempData["WarningMessage"] = "Warehouse has been deactivated";
            }
            else
            {
                await warehouseRepository.DeleteAsync(id);
                TempData["SuccessMessage"] = "Warehouse successfully deleted";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reactivate(int id)
        {
            if (await warehouseRepository.IsIdExists(id) != true)
            {
                return NotFound();
            }
            await warehouseRepository.ReactivateAsync(id);
            TempData["SuccessMessage"] = "Warehouse successfully reactivated";
            return RedirectToAction(nameof(Index));
        }
    }
}
