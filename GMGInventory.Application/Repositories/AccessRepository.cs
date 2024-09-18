using AutoMapper;
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
        private readonly RoleManager<IdentityRole> roleManager;

        public AccessRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
            , UserManager<User> userManager
            , RoleManager<IdentityRole> roleManager) : base(context, httpContextAccessor, userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.roleManager = roleManager;
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
            var user = await userManager.FindByIdAsync(UserId);
            if (user == null) return new Access();

            var roles = await userManager.GetRolesAsync(user);
            if (roles.Count != 1) return new Access();

            var role = await roleManager.FindByNameAsync(roles.First());
            var roleId = await roleManager.GetRoleIdAsync(role);
            var pageId = await context.Pages.Where(s => s.Name == PageName.ToEnumString()).Select(s => s.Id).FirstOrDefaultAsync();
            var access = await context.Accesses.FirstOrDefaultAsync(s => s.RoleId == roleId && s.PageId == pageId);
            return access ?? new Access();
        }

        public async Task<IList<AccessVM>> GetUserAccessesAsync(string UserId)
        {
            var user = await userManager.FindByIdAsync(UserId);
            if (user == null) return [];

            var roles = await userManager.GetRolesAsync(user);
            if (roles.Count != 1) return [];

            var role = await roleManager.FindByNameAsync(roles.First());
            var roleId = await roleManager.GetRoleIdAsync(role);
            var accesses = from p in context.Pages
                           from a in context.Accesses.Where(s => s.PageId == p.Id && s.RoleId == roleId).DefaultIfEmpty()
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
    }
}