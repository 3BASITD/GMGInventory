using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface ISystemSettingRepository
    {
        Task<IList<SystemSettingVM>> GetSystemSettings();
        Task<string?> GetValue(string? name);
        Task <ResponseModel> UpdateSettings(SystemSettingsVM ssettings);
        Task<ResponseModel<string>> SaveImageAsync(PageImageFormVM form);
        Task<ResponseModel> RemoveImageAsync(string Name);
    }
}