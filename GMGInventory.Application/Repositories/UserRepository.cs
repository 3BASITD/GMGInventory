using AutoMapper;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Application.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public UserRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
            , UserManager<User> userManager
            , IMapper mapper)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<IList<UserInformationVM>> GetUsersInformationVMAsync()
        {
            var users = userManager.Users.ToList();
            var usersVM = mapper.Map<List<UserInformationVM>>(users);
            foreach (var userVM in usersVM)
            {
                userVM.UserId = await GetUserIdByNameAsync(userVM.Username);
            }
            return usersVM;
        }

        public async Task<UserInformationVM> GetUserInformationVMAsync(string name)
        {
            //var user = await context.Users.FindAsync(id);
            var user = await userManager.FindByNameAsync(name);
            return mapper.Map<UserInformationVM>(user);
        }

        public async Task<UserInformationForUpdateVM?> GetUserInformationVMToEditAsync(string id)
        {
            if (id == null)
            {
                return null;
            }
            var userData = userManager.Users.Where(s => s.Id == id).First();
            var user = await userManager.FindByNameAsync(userData.UserName);
            var userVM = mapper.Map<UserInformationForUpdateVM>(user);
            var userRole = userManager.GetRolesAsync(user).Result;
            userVM.Role = userRole.ToArray();
            return userVM;
        }

        public async Task<UserInformationVM?> GetUserInformationVMByIdAsync(string id)
        {
            if (id == null)
            {
                return null;
            }
            var userData = userManager.Users.Where(s => s.Id == id).First();
            var user = await userManager.FindByNameAsync(userData.UserName);
            var userVM = mapper.Map<UserInformationVM>(user);
            var userRole = userManager.GetRolesAsync(user).Result;
            userVM.Role = userRole.ToArray();
            return userVM;
        }

        public async Task<bool> IsUserInUseAsync(string id)
        {
            return await context.Agents.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.Areas.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.AuditLogs.AnyAsync(s => s.UserId == id)
                || await context.Brands.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.Categories.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.CreditTerms.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.CustomerPayments.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.Customers.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.Forwarders.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.InvoiceLineItems.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.InvoicePayments.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.Invoices.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.ItemInventories.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.ItemPrices.AnyAsync(s => s.UserCreated == id)
                || await context.Items.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.ItemTypes.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.PackLineItems.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.PackReturnLineItems.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.PackReturns.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.Packs.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.PaymentDetails.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.Payments.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.PhysicalCountHistories.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.PurchaseOrderLineItems.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.PurchaseOrders.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.PurchaseReturnLineItems.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.PurchaseReturns.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.SalesOrderDeliveryMonitorings.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.SalesOrderLineItems.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.SalesOrders.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.SalesReturnAppliedAmounts.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.SalesReturnLineItems.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.SalesReturns.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.SalesReturnTotalReturnAmounts.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.ShippingAddresses.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.SOACRChecks.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.SOACRLineItems.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.SOACRs.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.StockCards.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.Suppliers.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.TransferItemDetails.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.TransferItems.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.Warehouses.AnyAsync(s => s.UserCreated == id || s.UserModified == id);
        }

        public async Task<bool?> IsUserIdExistAsync(string? id)
        {
            if (id == null) return null;
            var user = await userManager.FindByIdAsync(id);
            return user != null;
        }

        public async Task<bool?> IsUsernameExistAsync(string? username)
        {
            if (username == null) return null;
            return await userManager.Users.AnyAsync(s => s.UserName == username);
        }

        private async Task ChangeRolesAsync(User user, IEnumerable<string> roles)
        {
            var userRoles = await userManager.GetRolesAsync(user);
            await userManager.RemoveFromRolesAsync(user, userRoles.ToArray());
            await userManager.AddToRolesAsync(user, roles);
            //await userManager.UpdateAsync(user);
        }

        public async Task AddAsync(UserInformationVM userInformationVM)
        {
            var hasher = new PasswordHasher<User>();
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var user = mapper.Map<User>(userInformationVM);
            user.Id = user.Id.ToUpper();
            user.IsActive = true;
            user.NormalizedUserName = user.UserName.ToUpper();
            user.PasswordHash = hasher.HashPassword(null, userInformationVM.ConfirmPassword);
            await context.AddAsync(user);
            await context.SaveChangesAsync(currentUserId);

            await ChangeRolesAsync(user, userInformationVM.Role);
        }

        public async Task UpdateThemeAsync(string id, bool isDarkMode)
        {
            var user = await userManager.FindByIdAsync(id);
            user.IsDarkMode = isDarkMode;

            await userManager.UpdateAsync(user);
        }

        public async Task UpdateUserAsync(UserInformationForUpdateVM userInformationVM)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var userId = await GetUserIdByNameAsync(userInformationVM.Username);
            var user = await userManager.FindByIdAsync(userId);
            user.FirstName = userInformationVM.FirstName;
            user.LastName = userInformationVM.LastName;
            user.Nickname = userInformationVM.Nickname;

            await ChangeRolesAsync(user, userInformationVM.Role);

            context.Update(user);
            await context.SaveChangesAsync(currentUserId);
            //await userManager.UpdateAsync(user);
        }

        public async Task DeactivateUserAsync(string id)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var user = await userManager.FindByIdAsync(id);
            user.IsActive = false;
            context.Update(user);
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task ReactivateUserAsync(string id)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var user = await userManager.FindByIdAsync(id);
            user.IsActive = true;
            context.Update(user);
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task DeleteAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var userRoles = await userManager.GetRolesAsync(user);
            await userManager.RemoveFromRolesAsync(user, userRoles.ToArray());
            await userManager.DeleteAsync(user);
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

        public async Task ResetPassword(UserResetPasswordVM userPasswordVM)
        {
            var hasher = new PasswordHasher<User>();
            var user = await userManager.FindByIdAsync(userPasswordVM.Id);
            user.PasswordHash = hasher.HashPassword(user, userPasswordVM.ConfirmPassword);

            await userManager.UpdateAsync(user);
        }

        public async Task<IList<AuditLog>> GetAuditLogAsync(string Id)
        {
            return await context.AuditLogs
                .Where(s => s.UserId == Id && s.Type != "None")
                .OrderByDescending(s => s.DateTime)
                .Take(100)
                .ToListAsync();
        }

        public async Task<string> GetUserIdByNameAsync(string name)
        {
            var user = await userManager.FindByNameAsync(name);
            return user.Id;
        }
    }
}