using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace GMGInventory.Application.Repositories
{
    public class AccessRepository : GenericRepository<Access>, IAccessRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;

        public AccessRepository(ApplicationDbContext context
                              , IHttpContextAccessor httpContextAccessor
                              , UserManager<User> userManager
                              , RoleManager<Role> roleManager) : base(context, httpContextAccessor, userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<IList<JsonOptionsModel>> GetRolesOptions(string? IncludeId)
        {
            return await roleManager.Roles
                .Where(s => s.IsActive || s.Id == IncludeId)
                .Select(s => new JsonOptionsModel
                {
                    Id = s.Id.ToString(),
                    Text = s.Name,
                    Selected = IncludeId != null && IncludeId == s.Id
                })
                .OrderBy(s => s.Text)
                .ToListAsync();
        }

        public async Task<RolesPagesVM> GetRolesPagesAsync(string RoleId)
        {
            var role = context.Roles.Where(s => s.Id == RoleId).FirstOrDefault();
            var accesses = from p in context.Pages
                           from a in context.Accesses.Where(s => s.PageId == p.Id && s.RoleId == RoleId).DefaultIfEmpty()
                           select new AccessVM
                           {
                               AccessId = a == null ? -1 : a.Id,
                               PageId = p.Id,
                               Name = p.Name,
                               HasView = p.HasView,
                               CanView = a != null && a.CanView,
                               HasAdd = p.HasAdd,
                               CanAdd = a != null && a.CanAdd,
                               HasEdit = p.HasEdit,
                               CanEdit = a != null && a.CanEdit,
                               HasDelete = p.HasDelete,
                               CanDelete = a != null && a.CanDelete,
                               HasApproveDisapprove = p.HasApproveDisapprove,
                               CanApproveDisapprove = a != null && a.CanApproveDisapprove != null && a.CanApproveDisapprove == true,
                               HasUploadDownload = p.HasUploadDownload,
                               CanUploadDownload = a != null && a.CanUploadDownload != null && a.CanUploadDownload == true,
                               HasPrint = p.HasPrint,
                               CanPrint = a != null && a.CanPrint != null && a.CanPrint == true,
                               UserCreated = a == null ? "" : a.UserCreated,
                               UserModified = a == null ? null : a.UserModified,
                               DateCreatedD = a == null ? DateTime.Now : a.DateCreated,
                               DateModifiedD = a == null ? null : a.DateModified
                           };
            return new RolesPagesVM
            {
                RoleName = role?.Name ?? "",
                RoleId = RoleId,
                Accesses = await accesses.ToListAsync()
            };
        }

        public async Task<IList<string>> GetRolesUsersAsync(string RoleId)
        {
            var users = new List<string>();
            var role = await roleManager.FindByIdAsync(RoleId);
            if (role != null)
            {
                var userInRole = await userManager.GetUsersInRoleAsync(role.Name);
                users = userInRole.Select(s => s.FirstName + " " + s.LastName).ToList();
            }
            return users;
        }

        public async Task<ResponseModel> SaveRolesPagesAsync(string RoleId, string RoleName, IList<AccessVM> accesses)
        {
            var response = new ResponseModel();
            if (string.IsNullOrWhiteSpace(RoleName))
            {
                response.Status = "Error";
                response.Message = "Role name is required.";
            }
            else if (context.Roles.Any(s => s.Name == RoleName && s.Id != RoleId))
            {
                response.Status = "Error";
                response.Message = "Role name already exists.";
            }
            else
            {
                string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
                var role = await roleManager.FindByIdAsync(RoleId);
                if (role == null)
                {
                    // Add
                    var newRole = new Role
                    {
                        Name = RoleName,
                        NormalizedName = RoleName.ToUpper(),
                        IsActive = true,
                        UserCreated = currentUserId,
                        DateCreated = DateTime.Now
                    };
                    var result = await roleManager.CreateAsync(newRole);
                    if (result.Succeeded)
                    {
                        var newRoleId = newRole.Id;

                        foreach (var access in accesses)
                        {
                            var newAccess = new Access
                            {
                                RoleId = newRoleId,
                                PageId = access.PageId,
                                CanView = access.CanView,
                                CanAdd = access.CanAdd,
                                CanEdit = access.CanEdit,
                                CanDelete = access.CanDelete,
                                CanApproveDisapprove = access.CanApproveDisapprove,
                                CanUploadDownload = access.CanUploadDownload,
                                CanPrint = access.CanPrint,
                                UserCreated = currentUserId,
                                DateCreated = DateTime.Now
                            };
                            context.Accesses.Add(newAccess);
                        }
                        await context.SaveChangesAsync(currentUserId);

                        response.Status = "Success";
                        response.Message = "The role has been successfully added. You can now assign it to users.";
                    }
                    else
                    {
                        response.Status = "Error";
                        response.Message = string.Join(", ", result.Errors.Select(e => e.Description));
                    }
                }
                else
                {
                    // Edit
                    role.Name = RoleName;
                    role.NormalizedName = RoleName.ToUpper();
                    role.UserCreated = currentUserId;
                    role.DateCreated = DateTime.Now;
                    context.Roles.Update(role);
                    await context.SaveChangesAsync(currentUserId);

                    foreach (var access in accesses)
                    {
                        var acc = await context.Accesses.Where(s => s.PageId == access.PageId && s.RoleId == RoleId).FirstOrDefaultAsync();
                        if (acc == null)
                        {
                            var newAccess = new Access
                            {
                                RoleId = RoleId,
                                PageId = access.PageId,
                                CanView = access.CanView,
                                CanAdd = access.CanAdd,
                                CanEdit = access.CanEdit,
                                CanDelete = access.CanDelete,
                                CanApproveDisapprove = access.CanApproveDisapprove,
                                CanUploadDownload = access.CanUploadDownload,
                                CanPrint = access.CanPrint,
                                UserCreated = currentUserId,
                                DateCreated = DateTime.Now
                            };
                            context.Accesses.Add(newAccess);
                        }
                        else
                        {
                            acc.CanView = access.CanView;
                            acc.CanAdd = access.CanAdd;
                            acc.CanEdit = access.CanEdit;
                            acc.CanDelete = access.CanDelete;
                            acc.CanApproveDisapprove = access.CanApproveDisapprove;
                            acc.CanUploadDownload = access.CanUploadDownload;
                            acc.CanPrint = access.CanPrint;
                            acc.UserModified = currentUserId;
                            acc.DateModified = DateTime.Now;
                            context.Accesses.Update(acc);
                        }
                    }
                    await context.SaveChangesAsync(currentUserId);

                    response.Status = "Success";
                    response.Message = "The role has been successfully update.";
                }
            }
            return response;
        }

        public async Task<ResponseModel> DeleteDeactivateRoleAsync(string RoleId)
        {
            var response = new ResponseModel();
            var role = await roleManager.FindByIdAsync(RoleId);
            if (role == null)
            {
                response.Status = "Error";
                response.Message = "Invalid role";
            }
            else
            {
                var userInRole = await userManager.GetUsersInRoleAsync(role.Name);
                if (userInRole.Count > 0)
                {
                    // Deactivate
                    role.IsActive = false;

                    var result = await roleManager.UpdateAsync(role);
                    if (result.Succeeded)
                    {
                        response.Status = "Success";
                        response.Message = "Role successully deactivated";
                    }
                    else
                    {
                        response.Status = "Error";
                        response.Message = string.Join(", ", result.Errors.Select(e => e.Description));
                    }
                }
                else
                {
                    // Delete
                    var accesses = await context.Accesses.Where(s => s.RoleId == RoleId).ToListAsync();
                    context.Accesses.RemoveRange(accesses);
                    await context.SaveChangesAsync();

                    var result = await roleManager.DeleteAsync(role);
                    if (result.Succeeded)
                    {
                        response.Status = "Success";
                        response.Message = "Role successully deleted";
                    }
                    else
                    {
                        response.Status = "Error";
                        response.Message = string.Join(", ", result.Errors.Select(e => e.Description));
                    }

                }
            }
            return response;
        }

        public async Task<ResponseModel> ReactivateRoleAsync(string RoleId)
        {
            var response = new ResponseModel();
            var role = await roleManager.FindByIdAsync(RoleId);
            if (role == null)
            {
                response.Status = "Error";
                response.Message = "Invalid role";
            }
            else
            {
                if (role.IsActive)
                {
                    response.Status = "Warning";
                    response.Message = "The role is already active.";
                }
                else
                {
                    role.IsActive = true;

                    var result = await roleManager.UpdateAsync(role);
                    if (result.Succeeded)
                    {
                        response.Status = "Success";
                        response.Message = "Role successully activated";
                    }
                    else
                    {
                        response.Status = "Error";
                        response.Message = string.Join(", ", result.Errors.Select(e => e.Description));
                    }
                }
            }
            return response;
        }

        public async Task<IList<string>> GetUserAllowedPagesAsync(string UserId)
        {
            var accesses = from p in context.Pages
                           join a in context.Accesses on p.Id equals a.PageId
                           join ur in context.UserRoles on a.RoleId equals ur.RoleId
                           join u in context.Users on ur.UserId equals u.Id
                           where u.Id == UserId
                           && (
                               a.CanView
                            || a.CanAdd
                            || a.CanEdit
                            || a.CanDelete
                            || a.CanApproveDisapprove == true
                            || a.CanUploadDownload == true
                            || a.CanPrint == true
                           )
                           select p.Name;
            return await accesses.ToListAsync();

        }

        public async Task<bool> HasAccessAsync(string UserId, PageNames PageName, AccessTypes AccessType)
        {
            var access = await GetUserAccessToPageAsync(UserId, PageName);
            if (access == new Access()) return false;

            return AccessType switch
            {
                AccessTypes.View => access.CanView,
                AccessTypes.Add => access.CanAdd,
                AccessTypes.Edit => access.CanEdit,
                AccessTypes.Delete => access.CanDelete,
                AccessTypes.ApproveDisapprove => access.CanApproveDisapprove == true,
                AccessTypes.UploadDownload => access.CanUploadDownload == true,
                AccessTypes.Print => access.CanPrint == true,
                _ => false
            };
        }

        public async Task<Access> GetUserAccessToPageAsync(string UserId, PageNames PageName)
        {

            var access = from p in context.Pages
                         join a in context.Accesses on p.Id equals a.PageId
                         join ur in context.UserRoles on a.RoleId equals ur.RoleId
                         join u in context.Users on ur.UserId equals u.Id
                         where p.Name == PageName.ToEnumString()
                         && u.Id == UserId
                         select a;
            return await access.FirstOrDefaultAsync() ?? new Access();
        }

        public async Task<IList<AccessVM>> GetUserAccessesAsync(string UserId)
        {
            var accesses = from p in context.Pages
                           join a in context.Accesses on p.Id equals a.PageId
                           join ur in context.UserRoles on a.RoleId equals ur.RoleId
                           join u in context.Users on ur.UserId equals u.Id
                           where u.Id == UserId
                           select new AccessVM
                           {
                               AccessId = a == null ? -1 : a.Id,
                               PageId = p.Id,
                               Name = p.Name,
                               HasView = p.HasView,
                               CanView = a != null && a.CanView,
                               HasAdd = p.HasAdd,
                               CanAdd = a != null && a.CanAdd,
                               HasEdit = p.HasEdit,
                               CanEdit = a != null && a.CanEdit,
                               HasDelete = p.HasDelete,
                               CanDelete = a != null && a.CanDelete,
                               HasApproveDisapprove = p.HasApproveDisapprove,
                               CanApproveDisapprove = a != null && a.CanApproveDisapprove == true,
                               HasUploadDownload = p.HasUploadDownload,
                               CanUploadDownload = a != null && a.CanUploadDownload == true,
                               HasPrint = p.HasPrint,
                               CanPrint = a != null && a.CanPrint == true,
                               UserCreated = a == null ? "" : a.UserCreated,
                               UserModified = a == null ? null : a.UserModified,
                               DateCreatedD = a == null ? DateTime.Now : a.DateCreated,
                               DateModifiedD = a == null ? null : a.DateModified
                           };
            return await accesses.ToListAsync();
        }

        public async Task<ForDataTables<AccessVM>> GetUserAccessesDTAsync(DataTablesRequestForm form, string UserId)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetUserAccessesAsync(UserId)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(s => s.Name.ToLower().Contains(form.SearchValue));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                data = data.OrderBy(form.SortColumn + " " + form.SortColumnDirection);
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<AccessVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public async Task<IList<RoleVM>> GetRolesAsync()
        {
            var roles = from r in roleManager.Roles
                        join userc in userManager.Users on r.UserCreated equals userc.Id
                        join userm in userManager.Users on r.UserModified equals userm.Id into m
                        from um in m.DefaultIfEmpty()
                        select new RoleVM
                        {
                            Id = r.Id,
                            Name = r.Name,
                            IsActive = r.IsActive,
                            UserCreated = userc.Nickname,
                            DateCreatedD = r.DateCreated,
                            UserModified = um == null ? "-" : um.Nickname,
                            DateModifiedD = r.DateModified
                        };
            return await roles.ToListAsync();
        }

        public async Task<ForDataTables<RoleVM>> GetRolesDTAsync(DataTablesRequestForm form)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetRolesAsync()).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(s => s.Name.ToLower().Contains(form.SearchValue));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                data = data.OrderBy(form.SortColumn + " " + form.SortColumnDirection);
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<RoleVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }
    }
}