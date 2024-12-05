using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class PurchaseReceivingLineItem : BaseEntity
    {
        public PurchaseReceivingLineItem()
        {
            PurchaseReturnLineItems = new HashSet<PurchaseReturnLineItem>();
        }

        [ForeignKey("PurchaseReceivingId")]
        public int PurchaseReceivingId { get; set; }
        public virtual PurchaseReceiving PurchaseReceiving { get; set; }

        [ForeignKey("OrderItemId")]
        public int? OrderItemId { get; set; }
        public virtual PurchaseOrderNewLineItem OrderItem { get; set; }

        public int Quantity { get; set; }

        public ICollection<PurchaseReturnLineItem> PurchaseReturnLineItems { get; set; }
    }
}
