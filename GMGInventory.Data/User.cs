using Microsoft.AspNetCore.Identity;

namespace GMGInventory.Data
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Nickname { get; set; }
        public bool IsDarkMode { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
