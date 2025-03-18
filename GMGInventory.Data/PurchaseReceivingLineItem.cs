using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class PurchaseReceivingLineItem : BaseEntity
    {
        public PurchaseReceivingLineItem()
        {
            PurchaseReturnLineItem = new HashSet<PurchaseReturnNewLineItem>();
        }

        [ForeignKey("PurchaseReceivingId")]
        public int PurchaseReceivingId { get; set; }
        public virtual PurchaseReceiving PurchaseReceiving { get; set; }

        [ForeignKey("OrderItemId")]
        public int OrderItemId { get; set; }
        public virtual PurchaseOrderNewLineItem OrderItem { get; set; }

        public int Quantity { get; set; }

        public ICollection<PurchaseReturnNewLineItem> PurchaseReturnLineItem { get; set; }
    }
}
