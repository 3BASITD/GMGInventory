using GMGInventory.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.ViewComponents
{
    public class LayoutDataViewComponent : ViewComponent
    {
        private readonly ISystemSettingRepository setting;

        public LayoutDataViewComponent(ISystemSettingRepository setting)
        {
            this.setting = setting;
        }

        public async Task<IViewComponentResult> InvokeAsync(string viewType = "CompanyTitle")
        {
            //var allowedViews = new HashSet<string>
            //{
            //    "CompanyLogin",
            //    "CompanyNavbar",
            //    "CompanyTitle",
            //    "LoginImage",
            //    "NavigationImage"
            //};

            //if (!allowedViews.Contains(viewType))
            //    viewType = "Default";

            var contents = await setting.GetSystemSettings();
            //if (string.IsNullOrEmpty(content))
            //    content = "3BAS Tigers Inc.";

            //return View(viewType, new ConfigurationModel
            //{
            //    Name = viewType,
            //    Value = content
            //});

            return View(viewType, contents);
        }

        public class ConfigurationModel
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }
    }
}
