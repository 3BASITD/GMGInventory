using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class SOACRLineItemTBR : BaseEntity
    {
        [ForeignKey("SalesReturnLineItem")]
        public int SalesReturnLineItemId { get; set; }
        public SalesReturnLineItem SalesReturnLineItem { get; set; }

        [ForeignKey("SOACRLineItem")]
        public int SOACRLineItemId { get; set; }
        public SOACRLineItem SOACRLineItem { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }
    }
}
