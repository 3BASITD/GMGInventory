using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class Customer : BaseEntity
    {
        [Column(TypeName = "nvarchar(50)")]
        public string Code { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }
        public bool IsActive { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string Address { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Email { get; set; }

        [ForeignKey("CustomerTypeId")]
        public int CustomerTypeId { get; set; }
        public CustomerType CustomerType { get; set; }

        [ForeignKey("CreditTermId")]
        public int? CreditTermId { get; set; }
        public CreditTerm CreditTerm { get; set; }

        [ForeignKey("AgentId")]
        public int? AgentId { get; set; }
        public Agent Agent { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? ContactPerson { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? ContactNumber { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? FaxNumber { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? TIN { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal CreditLimit { get; set; }

        [ForeignKey("AreaId")]
        public int? AreaId { get; set; }
        public Area Area { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string? Remarks { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal OutstandingBalance { get; set; }
    }
}
