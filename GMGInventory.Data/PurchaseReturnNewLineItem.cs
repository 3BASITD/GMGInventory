using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class PurchaseReturnNewLineItem : BaseEntity
    {

        [ForeignKey("PurchaseReturnId")]
        public int PurchaseReturnId { get; set; }
        public virtual PurchaseReturnNew PurchaseReturn { get; set; }

        [ForeignKey("ReceivingItemId")]
        public int ReceivingItemId { get; set; }
        public virtual PurchaseReceivingLineItem ReceivingItem { get; set; }

        public int Quantity { get; set; }
    }
}
