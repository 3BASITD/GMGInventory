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
            var data = itemRepository.GetAll(itemFilterVM);
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrEmpty(searchValue))
            {
                data = data.Where(x => x.Code.ToLower().Contains(searchValue.ToLower())
                                || x.InterchangeNumber.ToLower().Contains(searchValue.ToLower())
                                || x.Name.ToLower().Contains(searchValue.ToLower())
                                || x.NetPrice.ToString().ToLower().Contains(searchValue.ToLower())
                                || x.PartNumber.ToLower().Contains(searchValue.ToLower())
                                || x.Quantity.ToString().ToLower().Contains(searchValue.ToLower())
                                || x.RetailPrice.ToString().ToLower().Contains(searchValue.ToLower())
                                || x.Warehouse.ToLower().Contains(searchValue.ToLower()));
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
                        case "InterchangeNumber": data = data.OrderBy(s => s.InterchangeNumber); break;
                        case "Name": data = data.OrderBy(s => s.Name); break;
                        case "NetPrice": data = data.OrderBy(s => s.NetPrice); break;
                        case "PartNumber": data = data.OrderBy(s => s.PartNumber); break;
                        case "Quantity": data = data.OrderBy(s => s.Quantity); break;
                        case "RetailPrice": data = data.OrderBy(s => s.RetailPrice); break;
                        case "Warehouse": data = data.OrderBy(s => s.Warehouse); break;
                        default: break;
                    }
                }
                else if (sortColumnDirection == "desc")
                {
                    switch (sortColumn)
                    {
                        case "Code": data = data.OrderByDescending(s => s.Code); break;
                        case "InterchangeNumber": data = data.OrderByDescending(s => s.InterchangeNumber); break;
                        case "Name": data = data.OrderByDescending(s => s.Name); break;
                        case "NetPrice": data = data.OrderByDescending(s => s.NetPrice); break;
                        case "PartNumber": data = data.OrderByDescending(s => s.PartNumber); break;
                        case "Quantity": data = data.OrderByDescending(s => s.Quantity); break;
                        case "RetailPrice": data = data.OrderByDescending(s => s.RetailPrice); break;
                        case "Warehouse": data = data.OrderByDescending(s => s.Warehouse); break;
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

        public async Task<ActionResult> Details(int id)
        {
            var item = await itemRepository.GetAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        //public async Task<ActionResult> Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create(UserInformationVM userInformationVM)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (await userRepository.IsUsernameExistAsync(userInformationVM.Username) == true)
        //        {
        //            TempData["ErrorMessage"] = "Username already in use";
        //        }
        //        else
        //        {
        //            await userRepository.AddAsync(userInformationVM);
        //            TempData["SuccessMessage"] = "User successfully created";
        //            return RedirectToAction(nameof(Index));
        //        }
        //    }
        //    return View(userInformationVM);
        //}

        //public async Task<ActionResult> Edit(string id)
        //{
        //    var user = await userRepository.GetUserInformationVMToEditAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(user);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit(UserInformationForUpdateVM userInformationVM)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await userRepository.UpdateUserAsync(userInformationVM);
        //        TempData["SuccessMessage"] = "User details of " +userInformationVM.Username + " was successfully updated";
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(userInformationVM);
        //}

        //public async Task<ActionResult> Password(string id)
        //{
        //    var user = await userRepository.GetUserInformationVMToEditAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    var userPasswordVM = new UserResetPasswordVM()
        //    {
        //        Id = id,
        //        Username = user.Username
        //    };
        //    return View(userPasswordVM);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Password(UserResetPasswordVM userPasswordVM)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        await userRepository.ResetPassword(userPasswordVM);
        //        TempData["SuccessMessage"] = "Password successfully reset";
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(userPasswordVM);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Delete(string id)
        //{
        //    if (await userRepository.IsUserIdExistAsync(id) != true)
        //    {
        //        return NotFound();
        //    }
        //    if (await userRepository.IsUserInUseAsync(id))
        //    {
        //        await userRepository.DeactivateUserAsync(id);
        //        TempData["WarningMessage"] = "User has been deactivated";
        //    }
        //    else
        //    {
        //        await userRepository.DeleteAsync(id);
        //        TempData["SuccessMessage"] = "User successfully deleted";
        //    }
        //    return RedirectToAction(nameof(Index));
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Reactivate(string id)
        //{
        //    if (await userRepository.IsUserIdExistAsync(id) != true)
        //    {
        //        return NotFound();
        //    }
        //    await userRepository.ReactivateUserAsync(id);
        //    TempData["SuccessMessage"] = "User successfully reactivated";
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
