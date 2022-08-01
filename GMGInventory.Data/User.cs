using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class User : IdentityUser
    {
        [Column(TypeName = "nvarchar(50)")]
        public string? FirstName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? LastName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Nickname { get; set; }

        public bool IsDarkMode { get; set; }
        public bool IsActive { get; set; }

        [Column(TypeName = "nvarchar(450)")]
        public string UserCreated { get; set; }

        [Column(TypeName = "nvarchar(450)")]
        public string? UserModified { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

    }
}
