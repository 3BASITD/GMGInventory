using AutoMapper;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace GMGInventory.Application.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public UserRepository(ApplicationDbContext context
            , UserManager<User> userManager
            , IMapper mapper)
        {
            this.context = context;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<UserInformationVM> GetUserInformationVMAsync(string name)
        {
            //var user = await context.Users.FindAsync(id);
            var user = await userManager.FindByNameAsync(name);
            return mapper.Map<UserInformationVM>(user);
        }

        public async Task<bool> IsUserExistAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            return user != null;
        }

        public async Task UpdateThemeAsync(string id, bool isDarkMode)
        {
            var user = await userManager.FindByIdAsync(id);
            user.IsDarkMode = isDarkMode;

            await userManager.UpdateAsync(user);
        }

        public async Task UpdateUserAsync(UserInformationVM userInformationVM)
        {
            var user = await userManager.FindByIdAsync(userInformationVM.Id);
            user.FirstName = userInformationVM.FirstName;
            user.LastName = userInformationVM.LastName;
            user.Nickname = userInformationVM.Nickname;

            await userManager.UpdateAsync(user);
        }

        public async Task<bool> IsPasswordMatchAsync(UserPasswordVM userPasswordVM)
        {
            var hasher = new PasswordHasher<User>();
            var user = await userManager.FindByIdAsync(userPasswordVM.Id);
            var passwordVerify = hasher.VerifyHashedPassword(user, user.PasswordHash, userPasswordVM.CurrentPassword);
            return passwordVerify == PasswordVerificationResult.Success;
        }

        public async Task UpdatePassword(UserPasswordVM userPasswordVM)
        {
            var hasher = new PasswordHasher<User>();
            var user = await userManager.FindByIdAsync(userPasswordVM.Id);
            user.PasswordHash = hasher.HashPassword(user, userPasswordVM.ConfirmPassword);

            await userManager.UpdateAsync(user);
        }

        public async Task<List<AuditLog>> GetAuditLogAsync(string Id)
        {
            return context.AuditLogs.Where(s => s.UserId == Id).ToList();
        }

        public async Task<string> GetUserIdByNameAsync(string name)
        {
            var user = await userManager.FindByNameAsync(name);
            return user.Id;
        }
    }
}