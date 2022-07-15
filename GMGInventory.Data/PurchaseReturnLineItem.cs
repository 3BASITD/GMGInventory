using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class PurchaseReturnLineItem : BaseEntity
    {
        [ForeignKey("PurchaseOrderLineItemId")]
        public int PurchaseOrderLineItemId { get; set; }
        public PurchaseOrderLineItem PurchaseOrderLineItem { get; set; }

        [ForeignKey("PurchaseReturnId")]
        public int PurchaseReturnId { get; set; }
        public PurchaseReturn PurchaseReturn { get; set; }

        public int Quantity { get; set; }
    }
}
