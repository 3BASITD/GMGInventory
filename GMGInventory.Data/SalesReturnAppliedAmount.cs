using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class SalesReturnAppliedAmount : BaseEntity
    {
        [ForeignKey("SalesReturnTotalReturnAmountId")]
        public int SalesReturnTotalReturnAmountId { get; set; }
        public SalesReturnTotalReturnAmount SalesReturnTotalReturnAmount { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string AppliedTo { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }
    }
}
