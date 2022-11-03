using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class SalesReturnLineItem : BaseEntity
    {
        [ForeignKey("SalesReturn")]
        public int SalesReturnId { get; set; }
        public SalesReturn SalesReturn { get; set; }

        [ForeignKey("Invoice")]
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

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

        public bool IsDefective { get; set; }
    }
}
