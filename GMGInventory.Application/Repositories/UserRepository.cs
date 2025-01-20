using AutoMapper;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Application.Repositories
{
    public class UserRepository(ApplicationDbContext context
                              , IHttpContextAccessor httpContextAccessor
                              , UserManager<User> userManager
                              , IMapper mapper) : IUserRepository
    {
        public async Task<IList<UserInformationVM>> GetUsersInformationVMAsync()
        {
            var users = await userManager.Users.ToListAsync();
            var usersVM = mapper.Map<List<UserInformationVM>>(users);
            foreach (var userVM in usersVM)
            {
                userVM.UserId = await GetUserIdByNameAsync(userVM.Username);
                var role = await (from r in context.Roles
                                  join ur in context.UserRoles on r.Id equals ur.RoleId
                                  where ur.UserId == userVM.UserId
                                  select r).FirstOrDefaultAsync();
                userVM.RoleId = role == null ? "" : role.Id;
                userVM.Role = role == null ? "-" : role.Name;
                userVM.UserCreated = await userManager.Users
                                                      .Where(s => s.Id == userVM.UserCreated)
                                                      .Select(s => s.FirstName + " " + s.LastName)
                                                      .FirstOrDefaultAsync() ?? "-";
                userVM.DateCreatedS = userVM.DateCreated?.ToString("yyyy/MM/dd HH:ss") ?? "-";
                userVM.UserModified = await userManager.Users
                                                       .Where(s => s.Id == userVM.UserModified)
                                                       .Select(s => s.FirstName + " " + s.LastName)
                                                       .FirstOrDefaultAsync() ?? "-";
                userVM.DateModifiedS = userVM.DateModified?.ToString("yyyy/MM/dd HH:ss") ?? "-";

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
            //var userData = userManager.Users.Where(s => s.Id == id).First();
            //var user = await userManager.FindByNameAsync(userData.UserName);
            //var userVM = mapper.Map<UserInformationForUpdateVM>(user);
            //var userRole = await userManager.GetRolesAsync(user);
            //userVM.UserId = user.Id;
            //userVM.RoleId = userRole.FirstOrDefault() ?? "";
            //userVM.Role = await context.Roles.Where(s => userRole.Contains(s.Id)).Select(s => s.Name).FirstOrDefaultAsync() ?? "";

            var userVM = from user in userManager.Users
                         join userRole in context.UserRoles on user.Id equals userRole.UserId into ur
                         from userRole in ur.DefaultIfEmpty()
                         join role in context.Roles on userRole.RoleId equals role.Id into r
                         from role in r.DefaultIfEmpty()
                         where user.Id == id
                         select new UserInformationForUpdateVM
                         {
                             UserId = user.Id,
                             Username = user.UserName,
                             RoleId = role == null ? "" : role.Id,
                             Role = role == null ? "" : role.Name,
                             FirstName = user.FirstName,
                             LastName = user.LastName,
                             Nickname = user.Nickname
                         };
            return await userVM.FirstOrDefaultAsync();
        }

        public async Task<UserInformationVM?> GetUserInformationVMByIdAsync(string id)
        {
            if (id == null)
            {
                return null;
            }
            //var userData = userManager.Users.Where(s => s.Id == id).First();
            //var user = await userManager.FindByNameAsync(userData.UserName);
            //var userVM = mapper.Map<UserInformationVM>(user);
            //var userRole = await userManager.GetRolesAsync(user);
            //userVM.RoleId = userRole.FirstOrDefault() ?? "";
            //userVM.Role = await context.Roles.Where(s => userRole.Contains(s.Id)).Select(s => s.Name).FirstOrDefaultAsync() ?? "";

            var userVM = from user in userManager.Users
                         join userRole in context.UserRoles on user.Id equals userRole.UserId into ur
                         from userRole in ur.DefaultIfEmpty()
                         join role in context.Roles on userRole.RoleId equals role.Id into r
                         from role in r.DefaultIfEmpty()
                         join userc in userManager.Users on user.UserCreated equals userc.Id
                         join userm in userManager.Users on user.UserModified equals userm.Id into m
                         from userm in m.DefaultIfEmpty()
                         where user.Id == id
                         select new UserInformationVM
                         {
                             UserId = user.Id,
                             Username = user.UserName,
                             RoleId = role == null ? "" : role.Id,
                             Role = role == null ? "-" : role.Name,
                             FirstName = user.FirstName,
                             LastName = user.LastName,
                             Nickname = user.Nickname,
                             IsActive = user.IsActive,
                             UserCreated = SystemFormat.Name(userc.FirstName, userc.LastName, userc.Nickname),
                             DateCreatedS = user.DateCreated.ToString("MMMM d, yyyy hh:mm:ss tt"),
                             UserModified = userm == null ? "-" : SystemFormat.Name(userm.FirstName, userm.LastName, userm.Nickname),
                             DateModifiedS = user.DateModified == null ? "-" : user.DateModified!.Value.ToString("MMMM d, yyyy hh:mm:ss tt")
                         };
            return await userVM.FirstOrDefaultAsync();
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
                || await context.CustomerPaymentDetails.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.Customers.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.Forwarders.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.InvoiceLineItems.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                //|| await context.InvoicePayments.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.Invoices.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.ItemInventories.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.ItemPrices.AnyAsync(s => s.UserCreated == id)
                || await context.Items.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.ItemTypes.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.PackLineItems.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.PackReturnLineItems.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.PackReturns.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.Packs.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                //|| await context.PaymentDetails.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                //|| await context.Payments.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.PhysicalCountHistories.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.PurchaseOrderLineItems.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.PurchaseOrders.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.PurchaseReturnLineItems.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.PurchaseReturns.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.SalesOrderDeliveryMonitorings.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.SalesOrderLineItems.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.SalesOrders.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                //|| await context.SalesReturnAppliedAmounts.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.SalesReturnLineItems.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.SalesReturns.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                //|| await context.SalesReturnTotalReturnAmounts.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.ShippingAddresses.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.SOACRChecks.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.SOACRLineItems.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.SOACRs.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
                || await context.SOACRLineItemTBRs.AnyAsync(s => s.UserCreated == id || s.UserModified == id)
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

        public async Task<int> AddAsync(UserInformationVM userInformationVM)
        {
            var hasher = new PasswordHasher<User>();
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var user = mapper.Map<User>(userInformationVM);
            user.Id = user.Id.ToUpper();
            user.IsActive = true;
            user.NormalizedUserName = user.UserName.ToUpper();
            user.PasswordHash = hasher.HashPassword(null, userInformationVM.ConfirmPassword);
            await context.AddAsync(user);
            var saved = await context.SaveChangesAsync(currentUserId);

            var role = await context.Roles.Where(s => s.Id == userInformationVM.Role).FirstOrDefaultAsync();
            if (role != null)
                await userManager.AddToRoleAsync(user, role.Name);

            return saved;
        }

        public async Task UpdateThemeAsync(string id, bool isDarkMode)
        {
            var user = await userManager.FindByIdAsync(id);
            user.IsDarkMode = isDarkMode;

            await userManager.UpdateAsync(user);
        }

        public async Task<int> UpdateUserAsync(UserInformationForUpdateVM userInformationVM)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var userId = await GetUserIdByNameAsync(userInformationVM.Username);
            var user = await userManager.FindByIdAsync(userId);
            user.FirstName = userInformationVM.FirstName;
            user.LastName = userInformationVM.LastName;
            user.Nickname = userInformationVM.Nickname;

            var role = await context.Roles.Where(s => s.Id == userInformationVM.Role).FirstOrDefaultAsync();
            if (role != null)
            {
                var roles = await userManager.GetRolesAsync(user);
                await userManager.RemoveFromRolesAsync(user, roles);
                await userManager.AddToRoleAsync(user, role.Name);
            }

            context.Update(user);
            return await context.SaveChangesAsync(currentUserId);
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
            await userManager.RemoveFromRolesAsync(user, [.. userRoles]);
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
            //var user = await userManager.FindByNameAsync(name);
            var user = await userManager.Users.Where(s => s.UserName == name).FirstOrDefaultAsync();
            return user?.Id ?? "";
        }
    }
}