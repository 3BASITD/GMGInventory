using GMGInventory.Application.Contracts;
using GMGInventory.Application.Repositories;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class UserController(IAccessRepository accessRepository
                              , IUserRepository userRepository) : BaseController(accessRepository)
    {
        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Users, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.Users)).Convert();
            var users = await userRepository.GetUsersInformationVMAsync();
            return View(users);
        }

        public async Task<IActionResult> GetOptions(string? IncludeId)
        {
            return Json(await userRepository.GetOptions(IncludeId));
        }

        public async Task<IActionResult> GetOptions2(string search, int page)
        {
            return Json(await userRepository.GetOptions2(search, page));
        }

        public async Task<IActionResult> Details(string id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Users, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.Users)).Convert();
            var user = await userRepository.GetUserInformationVMByIdAsync(id);
            if (user == null) return NotFound();

            user.UserId = id;
            return View(user);
        }

        public async Task<IActionResult> Create()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Users, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserInformationVM userInformationVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Users, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;
            userInformationVM.RoleId = userInformationVM.Role;

            if (ModelState.IsValid)
            {
                if (await userRepository.IsUsernameExistAsync(userInformationVM.Username) == true)
                    TempData["ErrorMessage"] = "Username already in use";
                else
                {
                    if (await userRepository.AddAsync(userInformationVM) > 0)
                    {
                        TempData["SuccessMessage"] = "User successfully created";
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View(userInformationVM);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Users, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;
            var user = await userRepository.GetUserInformationVMToEditAsync(id);
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserInformationForUpdateVM userInformationVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Users, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            if (ModelState.IsValid)
            {
                if (await userRepository.UpdateUserAsync(userInformationVM) > 0)
                {
                    TempData["SuccessMessage"] = "User details of " + userInformationVM.Username + " was successfully updated";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(userInformationVM);
        }

        public async Task<IActionResult> Password(string id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Users, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            var user = await userRepository.GetUserInformationVMToEditAsync(id);
            if (user == null) return NotFound();

            var userPasswordVM = new UserResetPasswordVM()
            {
                Id = id,
                Username = user.Username
            };
            return View(userPasswordVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Password(UserResetPasswordVM userPasswordVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Users, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            if (ModelState.IsValid)
            {
                await userRepository.ResetPassword(userPasswordVM);
                TempData["SuccessMessage"] = "Password successfully reset";
                return RedirectToAction(nameof(Index));
            }
            return View(userPasswordVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Users, AccessTypes.Delete);
            if (redirectResult != null) return redirectResult;

            if (await userRepository.IsUserIdExistAsync(id) != true) return NotFound();

            var tablesUsingUser = await userRepository.IsUserInUseAsync(id);
            if (tablesUsingUser.Count() > 0)
            {
                await userRepository.DeactivateUserAsync(id);
                TempData["WarningMessage"] = "Users is in use by other records. User has been deactivated instead. <br />Records: " + string.Join(", ", tablesUsingUser);
            }
            else
            {
                await userRepository.DeleteAsync(id);
                TempData["SuccessMessage"] = "User successfully deleted";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reactivate(string id)
        {
            var redirectResultAdd = await CheckAccessAndRedirectAsync(PageNames.Users, AccessTypes.Add);
            var redirectResultEdit = await CheckAccessAndRedirectAsync(PageNames.Users, AccessTypes.Edit);
            if (redirectResultAdd != null && redirectResultEdit != null) return redirectResultAdd;

            if (await userRepository.IsUserIdExistAsync(id) != true) return NotFound();

            await userRepository.ReactivateUserAsync(id);
            TempData["SuccessMessage"] = "User successfully reactivated";
            return RedirectToAction(nameof(Index));
        }
    }
}