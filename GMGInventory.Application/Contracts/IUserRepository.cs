using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface IUserRepository
    {
        Task<UserInformationVM> GetUserInformationVMAsync(string name);
        Task<bool> IsUserExistAsync(string id);
        Task UpdateThemeAsync(string id, bool isDarkMode);
        Task UpdateUserAsync(UserInformationVM userInformationVM);
        Task<bool> IsPasswordMatchAsync(UserPasswordVM userPasswordVM);
        Task UpdatePassword(UserPasswordVM userPasswordVM);
        Task<List<AuditLog>> GetAuditLogAsync(string Id);
        Task<string> GetUserIdByNameAsync(string name);
    }
}