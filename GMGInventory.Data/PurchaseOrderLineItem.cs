using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class PurchaseOrderLineItem : BaseEntity
    {
        public PurchaseOrderLineItem()
        {
            this.PurchaseReturnLineItems = new HashSet<PurchaseReturnLineItem>();
        }

        [ForeignKey("PurchaseOrderId")]
        public int PurchaseOrderId { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }

        [ForeignKey("ItemId")]
        public int ItemId { get; set; }
        public Item Item { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal UnitPrice { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Discount1 { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Discount2 { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Discount3 { get; set; }

        public ICollection<PurchaseReturnLineItem> PurchaseReturnLineItems { get; set; }
    }
}
