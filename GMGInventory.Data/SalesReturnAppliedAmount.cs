using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class SalesReturnAppliedAmount : BaseEntity
    {
        [ForeignKey("TotalReturnId")]
        public int TotalReturnId { get; set; }
        public SalesReturnTotalReturnAmount TotalReturn { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string AppliedTo { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }
    }
}
