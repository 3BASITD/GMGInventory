using GMGInventory.Common.Models;
using GMGInventory.Common.Constants;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface IAccessRepository : IGenericRepository<Access>
    {
        Task<IList<JsonOptionsModel>> GetRolesOptions(string? IncludeId);
        Task<RolesPagesVM> GetRolesPagesAsync(string RoleId);
        Task<IList<string>> GetRolesUsersAsync(string RoleId);
        Task<ResponseModel> SaveRolesPagesAsync(string RoleId, string RoleName, IList<AccessVM> accesses);
        Task<ResponseModel> DeleteDeactivateRoleAsync(string RoleId);
        Task<ResponseModel> ReactivateRoleAsync(string RoleId);
        Task<IList<string>> GetUserAllowedPagesAsync(string UserId);
        Task<bool> HasAccessAsync(string UserId, PageNames PageName, AccessTypes AccessType);
        Task<Access> GetUserAccessToPageAsync(string UserId, PageNames PageName);
        Task<IList<AccessVM>> GetUserAccessesAsync(string UserId);
        Task<ForDataTables<AccessVM>> GetUserAccessesDTAsync(DataTablesRequestForm form, string UserId);
        Task<IList<RoleVM>> GetRolesAsync();
        Task<ForDataTables<RoleVM>> GetRolesDTAsync(DataTablesRequestForm form);
        //Task<IList<AccessVM>> GetListAsync();
        //Task<ForDataTables<AccessVM>> GetListDTAsync(DataTablesRequestForm form);
    }
}