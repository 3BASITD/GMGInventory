using GMGInventory.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.ViewComponents
{
    public class LayoutDataViewComponent : ViewComponent
    {
        private readonly IConfigurationRepository config;

        public LayoutDataViewComponent(IConfigurationRepository config)
        {
            this.config = config;
        }

        public async Task<IViewComponentResult> InvokeAsync(string viewType = "CompanyTitle")
        {
            var allowedViews = new HashSet<string>
            {
                "CompanyLogin",
                "CompanyNavbar",
                "CompanyTitle"
            };

            if (!allowedViews.Contains(viewType))
                viewType = "Default";

            var content = await config.GetValue(viewType);
            if (string.IsNullOrEmpty(content))
                content = "3BAS Tigers Inc...";

            return View(viewType, new ConfigurationModel
            {
                Name = viewType,
                Value = content
            });
        }

        public class ConfigurationModel
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }
    }
}
