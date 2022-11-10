using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace GMGInventory.Helpers
{
    public class ControllerHelper
    {
        public string DisplayName<T>(string propertyName)
        {
            MemberInfo? property = typeof(T).GetProperty(propertyName);
            return property?.GetCustomAttribute<DisplayAttribute>()?.Name ?? propertyName;
        }
    }
}