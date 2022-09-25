using AutoMapper;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace GMGInventory.Controllers
{
    public class UserController : Controller
    {
        public IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<ActionResult> Index()
        {
            var users = await userRepository.GetUsersInformationVMAsync();
            return View(users);
        }

        public async Task<ActionResult> Details(string id)
        {
            var user = await userRepository.GetUserInformationVMByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            user.UserId = id;
            return View(user);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserInformationVM userInformationVM)
        {
            if (ModelState.IsValid)
            {
                if (await userRepository.IsUsernameExistAsync(userInformationVM.Username) == true)
                {
                    TempData["ErrorMessage"] = "Username already in use";
                }
                else
                {
                    await userRepository.AddAsync(userInformationVM);
                    TempData["SuccessMessage"] = "User successfully created";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(userInformationVM);
        }

        public async Task<ActionResult> Edit(string id)
        {
            var user = await userRepository.GetUserInformationVMToEditAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UserInformationForUpdateVM userInformationVM)
        {
            if (ModelState.IsValid)
            {
                await userRepository.UpdateUserAsync(userInformationVM);
                TempData["SuccessMessage"] = "User details of " +userInformationVM.Username + " was successfully updated";
                return RedirectToAction(nameof(Index));
            }
            return View(userInformationVM);
        }

        public async Task<ActionResult> Password(string id)
        {
            var user = await userRepository.GetUserInformationVMToEditAsync(id);
            if (user == null)
            {
                return NotFound();
            }
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
            if(ModelState.IsValid)
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
            if (await userRepository.IsUserIdExistAsync(id) != true)
            {
                return NotFound();
            }
            if (await userRepository.IsUserInUseAsync(id))
            {
                await userRepository.DeactivateUserAsync(id);
                TempData["WarningMessage"] = "User has been deactivated";
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
            if (await userRepository.IsUserIdExistAsync(id) != true)
            {
                return NotFound();
            }
            await userRepository.ReactivateUserAsync(id);
            TempData["SuccessMessage"] = "User successfully reactivated";
            return RedirectToAction(nameof(Index));
        }
    }
}
