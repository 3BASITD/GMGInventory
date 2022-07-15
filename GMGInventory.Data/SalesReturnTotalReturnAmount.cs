using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class SalesReturnTotalReturnAmount : BaseEntity
    {
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? ReferenceNumber { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalReturnAmount { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal UsedAmount { get; set; }
    }
}
