using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class PurchaseOrderNewLineItem : BaseEntity
    {
        [ForeignKey("PurchaseOrderId")]
        public int PurchaseOrderId { get; set; }
        public virtual PurchaseOrderNew PurchaseOrder { get; set; }
        public int LineNumber { get; set; }

        [ForeignKey("PurchaseRequestId")]
        public int? PurchaseRequestId { get; set; }
        public virtual PurchaseRequest PurchaseRequest { get; set; }

        [ForeignKey("ItemId")]
        public int? ItemId { get; set; }
        public virtual Item Item { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string ItemDescription { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("UnitId")]
        public int UnitId { get; set; }
        public virtual Unit Unit { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal UnitCost { get; set; }
    }
}
