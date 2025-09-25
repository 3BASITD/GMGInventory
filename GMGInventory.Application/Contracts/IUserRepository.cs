using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface IUserRepository
    {
        Task<IList<JsonOptionsModel>> GetOptions(string? IncludeId);
        Task<Select2Model<JsonNameOptionsModel>> GetOptions2(string search, int page);
        Task<IList<UserInformationVM>> GetUsersInformationVMAsync();
        Task<UserInformationVM> GetUserInformationVMAsync(string name);
        Task<UserInformationVM?> GetUserInformationVMByIdAsync(string id);
        Task<UserInformationForUpdateVM?> GetUserInformationVMToEditAsync(string id);
        Task<IList<string>> IsUserInUseAsync(string id);
        Task<bool?> IsUserIdExistAsync(string? id);
        Task<bool?> IsUsernameExistAsync(string? username);
        Task<int> AddAsync(UserInformationVM userInformationVM);
        Task UpdateThemeAsync(string id, bool isDarkMode);
        Task UpdateNavIconAsync(string id, bool isNavIconPlain);
        Task<int> UpdateUserAsync(UserInformationForUpdateVM userInformationVM);
        Task DeactivateUserAsync(string id);
        Task ReactivateUserAsync(string id);
        Task DeleteAsync(string id);
        Task<bool> IsPasswordMatchAsync(UserPasswordVM userPasswordVM);
        Task UpdatePassword(UserPasswordVM userPasswordVM);
        Task ResetPassword(UserResetPasswordVM userPasswordVM);
        Task<IList<AuditLog>> GetAuditLogAsync(string Id);
        Task<string> GetUserIdByNameAsync(string name);
    }
}