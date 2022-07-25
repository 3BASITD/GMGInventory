using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class Warehouse : BaseEntity
    {
        [Column(TypeName = "nvarchar(10)")]
        public string Code { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string AddressLine1 { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string? AddressLine2 { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string ContactPerson { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string ContactNumber { get; set; }

        public bool IsActive { get; set; }
    }
}
