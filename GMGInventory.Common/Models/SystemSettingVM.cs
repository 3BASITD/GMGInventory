using GMGInventory.Common.Constants;
using Microsoft.AspNetCore.Http;

namespace GMGInventory.Common.Models
{
    public class SystemSettingVM
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class SystemSettingsVM
    {
        public string Name { get; set; }
        public List<SystemSettingVM> Settings { get; set; }
    }

    public class PageImageFormVM
    {

        public string Name { get; set; }
        public IFormFile File { get; set; }
    }
}
