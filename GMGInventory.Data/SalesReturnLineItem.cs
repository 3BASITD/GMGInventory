using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class SalesReturnLineItem : BaseEntity
    {
        [ForeignKey("SalesReturn")]
        public int SalesReturnId { get; set; }
        public SalesReturn SalesReturn { get; set; }

        [ForeignKey("InvoiceLineItem")]
        public int? InvoiceLineItemId { get; set; }
        public InvoiceLineItem? InvoiceLineItem { get; set; }

        [ForeignKey("ItemId")]
        public int ItemId { get; set; }
        public Item Item { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal SellingPrice { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Discount1 { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Discount2 { get; set; }

        [ForeignKey("ReasonOfReturnId")]
        public int ReasonOfReturnId { get; set; }
        public ReasonOfReturn ReasonOfReturn { get; set; }

        public int? BarcotechReturnId { get; set; }
    }
}
