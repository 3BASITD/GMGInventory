using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Mvc;


namespace GMGInventory.Controllers
{
    public class WarehouseController : Controller
    {
        public IWarehouseRepository warehouseRepository;

        public WarehouseController(IWarehouseRepository warehouseRepository)
        {
            this.warehouseRepository = warehouseRepository;
        }

        public async Task<ActionResult> Index()
        {
            var items = await warehouseRepository.GetAllAsync();
            return View(items);
        }

        public async Task<ActionResult> Details(int id)
        {
            var item = await warehouseRepository.GetAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
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
