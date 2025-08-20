using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class PurchaseOrderNewLineItem : BaseEntity
    {
        public PurchaseOrderNewLineItem()
        {
            PurchaseReceivingLineItems = new HashSet<PurchaseReceivingLineItem>();
            PurchaseOrderNewLineItemNewCosts = new HashSet<PurchaseOrderNewLineItemNewCost>();
            PurchaseOrderNewLineItemNewQuantities = new HashSet<PurchaseOrderNewLineItemNewQuantity>();
        }

        [ForeignKey("PurchaseOrderId")]
        public int PurchaseOrderId { get; set; }
        public virtual PurchaseOrderNew PurchaseOrder { get; set; }

        public int LineNumber { get; set; }

        [ForeignKey("ItemId")]
        public int RequestItemId { get; set; }
        public virtual PurchaseRequestLineItem RequestItem { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal UnitCost { get; set; }

        public ICollection<PurchaseReceivingLineItem> PurchaseReceivingLineItems { get; set; }
        public ICollection<PurchaseOrderNewLineItemNewCost> PurchaseOrderNewLineItemNewCosts { get; set; }
        public ICollection<PurchaseOrderNewLineItemNewQuantity> PurchaseOrderNewLineItemNewQuantities { get; set; }
    }
}
