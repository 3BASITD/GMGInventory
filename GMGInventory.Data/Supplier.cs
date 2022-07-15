using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class Supplier : BaseEntity
    {
        [Column(TypeName = "nvarchar(50)")]
        public string Code { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [ForeignKey("AreaId")]
        public int AreaId { get; set; }
        public Area Area { get; set; }

        [ForeignKey("CurrencyId")]
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal CreditLimit { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string AddressLine1 { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string AddressLine2 { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Barangay { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string City { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string ZipCode { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string ContactPerson { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string ContactNumber { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string EmailAddress { get; set; }

        public bool IsActive { get; set; }
        public bool IsForeign { get; set; }
        public bool IsDeleted { get; set; }
    }
}
