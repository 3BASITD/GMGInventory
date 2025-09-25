using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class Driver : BaseEntity
    {
        public Driver()
        {
        }

        [Column(TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Nickname { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? LicenseNumber { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? Restriction { get; set; }
        public DateTime? LicenseDateExpiration { get; set; }
        public bool IsActive { get; set; }
    }
}
