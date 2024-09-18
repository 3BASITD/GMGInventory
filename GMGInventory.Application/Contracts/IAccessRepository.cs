using GMGInventory.Common.Models;
using GMGInventory.Common.Constants;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface IAccessRepository : IGenericRepository<Access>
    {
        Task<bool> HasAccessAsync(string UserId, PageNames PageName, AccessTypes AccessType);
        Task<Access> GetUserAccessToPageAsync(string UserId, PageNames PageName);
        Task<IList<AccessVM>> GetUserAccessesAsync(string UserId);
        Task<ForDataTables<AccessVM>> GetUserAccessesDTAsync(DataTablesRequestForm form, string UserId);
        //Task<IList<AccessVM>> GetListAsync();
        //Task<ForDataTables<AccessVM>> GetListDTAsync(DataTablesRequestForm form);
    }
}