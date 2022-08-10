using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class SalesOrderLineItem : BaseEntity
    {
        [ForeignKey("SalesOrderId")]
        public int SalesOrderId { get; set; }
        public SalesOrder SalesOrder { get; set; }

        [ForeignKey("ItemId")]
        public int ItemId { get; set; }
        public Item Item { get; set; }

        public int Quantity { get; set; }
        public int PackedQuantity { get; set; }
        public int InvoicedQuantity { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal UnitPrice { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Discount1 { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Discount2 { get; set; }

        public bool IsServed { get; set; }
    }
}
