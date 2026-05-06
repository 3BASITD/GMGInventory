using AutoMapper;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using GMGInventory.Data.Migrations;
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

        public async Task<IList<JsonOptionsModel>> GetOptions(string? IncludeId)
        {
            return await userManager.Users
                .Where(s => s.IsActive || s.Id == IncludeId)
                .Select(s => new JsonOptionsModel
                {
                    Id = s.Id.ToString(),
                    Text = s.UserName,
                    Selected = IncludeId != null && IncludeId == s.Id
                })
                .OrderBy(s => s.Text)
                .ToListAsync();
        }

        public async Task<Select2Model<JsonNameOptionsModel>> GetOptions2(string search, int page)
        {
            var users = from user in userManager.Users
                        where string.IsNullOrWhiteSpace(search)
                           || user.UserName.ToLower().Contains(search.ToLower())
                           || (user.FirstName + "").ToLower().Contains(search.ToLower())
                           || (user.LastName + "").ToLower().Contains(search.ToLower())
                        orderby user.UserName ascending
                        select new JsonNameOptionsModel
                        {
                            Id = user.Id.ToString(),
                            Text = user.UserName,
                            Name = user.FirstName + " " + user.LastName
                        };
            var usersFiltered = await users.Skip((page - 1) * 50).Take(50).ToListAsync();
            return new Select2Model<JsonNameOptionsModel>
            {
                //incomplete_results = false,
                items = usersFiltered,
                total_count = users.Count()
            };
        }

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

        public async Task<IList<string>> IsUserInUseAsync(string id)
        {
            List<string> tables = new List<string>();

            if (await context.Accesses.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Accesses"); }
            if (await context.Agents.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Agents"); }
            if (await context.Areas.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Areas"); }
            if (await context.AuditLogs.AnyAsync(s => s.UserId == id)) { tables.Add("Audit Logs"); }
            if (await context.Brands.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Brands"); }
            if (await context.BusinessStyles.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Business Styles"); }
            if (await context.Categories.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Categories"); }
            if (await context.Cheques.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Cheques"); }
            if (await context.CreditTerms.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Credit Terms"); }
            if (await context.CustomerBrandDiscount.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Customer Brand Discounts"); }
            if (await context.CustomerPaymentDetails.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Customer Payment Details"); }
            if (await context.CustomerPayments.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Customer Payments"); }
            if (await context.Customers.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Customers"); }
            if (await context.Drivers.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Drivers"); }
            if (await context.Forwarders.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Forwarders"); }
            if (await context.HomeImages.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Home Images"); }
            if (await context.InvoiceLineItems.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Invoice Line Items"); }
            if (await context.Invoices.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Invoices"); }
            if (await context.ItemConversions.AnyAsync(s => s.UserCreated == id)) { tables.Add("Item Conversions"); }
            if (await context.ItemInventories.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Item Inventories"); }
            if (await context.ItemLocations.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Item Locations"); }
            if (await context.ItemPrices.AnyAsync(s => s.UserCreated == id)) { tables.Add("Item Prices"); }
            if (await context.Items.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Items"); }
            if (await context.ItemTypes.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Item Types"); }
            if (await context.PackLineItems.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Pack Line Items"); }
            if (await context.PackReturnLineItems.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Pack Return Line Items"); }
            if (await context.PackReturns.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Pack Returns"); }
            if (await context.Packs.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Packs"); }
            if (await context.PhysicalCountHistories.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Physical Count Histories"); }
            if (await context.PurchaseOrderLineItems.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Purchase Order Line Items"); }
            if (await context.PurchaseOrderNewLineItemNewCosts.AnyAsync(s => s.UserCreated == id)) { tables.Add("Purchase Order Line Items Cost"); }
            if (await context.PurchaseOrderNewLineItemNewQuantities.AnyAsync(s => s.UserCreated == id)) { tables.Add("Purchase Order Line Items Quantity"); }
            if (await context.PurchaseOrderNewLineItems.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Purchase Order Line Items"); }
            if (await context.PurchaseOrders.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Purchase Orders"); }
            if (await context.PurchaseReceivingLineItems.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Purchase Receiving Line Items"); }
            if (await context.PurchaseReceivings.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Purchase Receivings"); }
            if (await context.PurchaseRequestLineItems.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Purchase Request Line Items"); }
            if (await context.PurchaseRequests.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Purchase Requests"); }
            if (await context.PurchaseReturnLineItems.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Purchase Return Line Items"); }
            if (await context.PurchaseReturnNewLineItems.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Purchase Return Line Items"); }
            if (await context.PurchaseReturnNews.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Purchase Returns"); }
            if (await context.PurchaseReturns.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Purchase Returns"); }
            if (await context.ReasonOfReturns.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Reason of Returns"); }
            if (await context.ReceptaclePrintHistories.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Receptacle Print Histories"); }
            if (await context.Regions.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Regions"); }
            if (await context.SalesOrderDeliveryMonitorings.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Sales Order Delivery Monitorings"); }
            if (await context.SalesOrderLineItems.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Sales Order Line Items"); }
            if (await context.SalesOrders.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Sales Orders"); }
            if (await context.SalesOrderSources.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Sales Order Sources"); }
            if (await context.SalesReturnLineItems.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Sales Return Line Items"); }
            if (await context.SalesReturns.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Sales Returns"); }
            if (await context.ServiceLevelAgreements.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Service Level Agreements"); }
            if (await context.ShippingAddresses.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Shipping Addresses"); }
            if (await context.SOACRChecks.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("SOA CR Checks"); }
            if (await context.SOACRLineItems.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("SOA CR Line Items"); }
            if (await context.SOACRLineItemTBRs.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("SOA CR Line Item TBRs"); }
            if (await context.SOACRs.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("SOACRs"); }
            if (await context.Suppliers.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Suppliers"); }
            if (await context.TransferItemDetails.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Transfer Item Details"); }
            if (await context.TransferItems.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Transfer Items"); }
            if (await context.Vehicles.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Vehicles"); }
            if (await context.Warehouses.AnyAsync(s => s.UserCreated == id || s.UserModified == id)) { tables.Add("Warehouses"); }

            return tables;
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

        public async Task UpdateNavIconAsync(string id, bool isNavIconPlain)
        {
            var user = await userManager.FindByIdAsync(id);
            user.IsNavIconPlain = isNavIconPlain;

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