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

        public async Task<IActionResult> GetOptions(ItemFilterVM filter, int? IncludeId)
        {
            return Json(await itemRepository.GetOptions(filter, IncludeId));
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
        public JsonResult GetListJson(ItemFilterVM filter)
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            int totalRecord = 0;
            int filterRecord = 0;
            var data = itemRepository.GetAllItemVM(filter);
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Code.ToLower().Contains(form.SearchValue)
                                    || x.Name.ToLower().Contains(form.SearchValue)
                                    || x.Category.ToLower().Contains(form.SearchValue)
                                    || x.UserCreated.ToLower().Contains(form.SearchValue)
                                    || x.DateCreated.ToString().ToLower().Contains(form.SearchValue)
                                    || x.UserModified.ToString().ToLower().Contains(form.SearchValue)
                                    || x.DateModified.ToLower().Contains(form.SearchValue));
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
                        case "InterchangeNumber": data = data.OrderBy(s => s.InterchangeNumber); break;
                        case "ItemType": data = data.OrderBy(s => s.ItemType); break;
                        case "IsActive": data = data.OrderBy(s => s.IsActive); break;
                        case "ModelNumber": data = data.OrderBy(s => s.ModelNumber); break;
                        case "Name": data = data.OrderBy(s => s.Name); break;
                        case "NetPrice": data = data.OrderBy(s => s.NetPrice); break;
                        case "PartNumber": data = data.OrderBy(s => s.PartNumber); break;
                        case "Quantity": data = data.OrderBy(s => s.Quantity); break;
                        case "RetailPrice": data = data.OrderBy(s => s.RetailPrice); break;
                        case "Size": data = data.OrderBy(s => s.Size); break;
                        case "UnitCode": data = data.OrderBy(s => s.UnitCode); break;
                        case "Warehouse": data = data.OrderBy(s => s.Warehouse); break;
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
                        case "InterchangeNumber": data = data.OrderByDescending(s => s.InterchangeNumber); break;
                        case "ItemType": data = data.OrderByDescending(s => s.ItemType); break;
                        case "IsActive": data = data.OrderByDescending(s => s.IsActive); break;
                        case "ModelNumber": data = data.OrderByDescending(s => s.ModelNumber); break;
                        case "Name": data = data.OrderByDescending(s => s.Name); break;
                        case "NetPrice": data = data.OrderByDescending(s => s.NetPrice); break;
                        case "PartNumber": data = data.OrderByDescending(s => s.PartNumber); break;
                        case "Quantity": data = data.OrderByDescending(s => s.Quantity); break;
                        case "RetailPrice": data = data.OrderByDescending(s => s.RetailPrice); break;
                        case "Size": data = data.OrderByDescending(s => s.Size); break;
                        case "UnitCode": data = data.OrderByDescending(s => s.UnitCode); break;
                        case "Warehouse": data = data.OrderByDescending(s => s.Warehouse); break;
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

        public ActionResult Details(int id)
        {
            var item = itemRepository.GetItemViewVM(id);
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
