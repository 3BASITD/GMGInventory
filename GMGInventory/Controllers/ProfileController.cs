using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IUserRepository userRepository;

        public ProfileController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<ActionResult> Index()
        {
            var userId = await userRepository.GetUserIdByNameAsync(User?.Identity?.Name);
            return View(await userRepository.GetAuditLogAsync(userId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeTheme(string id, string isDarkMode)
        {
            await userRepository.UpdateThemeAsync(id, isDarkMode == "on");
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Update()
        {
            var currentUserName = User?.Identity?.Name;
            return View(await userRepository.GetUserInformationVMToEditAsync(currentUserName));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(UserInformationForUpdateVM userInformationVM)
        {
            if (await userRepository.IsUserExistAsync(userInformationVM.Id) == false)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await userRepository.UpdateUserAsync(userInformationVM);
                TempData["SuccessMessage"] = "Account successfully updated";
                return RedirectToAction(nameof(Index));
            }
            return View(userInformationVM);
        }

        public async Task<ActionResult> Password()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Password(UserPasswordVM userPasswordVM)
        {
            if (ModelState.IsValid)
            {
                if (await userRepository.IsPasswordMatchAsync(userPasswordVM) == false)
                {
                    //ModelState.AddModelError(string.Empty, "Invalid current password.");
                    TempData["ErrorMessage"] = "Invalid current password";
                }
                else
                {
                    await userRepository.UpdatePassword(userPasswordVM);
                    TempData["SuccessMessage"] = "Password successfully updated";
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(userPasswordVM);
        }
    }
}
