using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class ShippingLineItem : BaseEntity
    {

        [ForeignKey("ShippingId")]
        public int ShippingId { get; set; }
        public virtual Shipping Shipping { get; set; }

        [ForeignKey("InvoiceLineItemId")]
        public int InvoiceLineItemId { get; set; }
        public virtual InvoiceLineItem InvoiceLineItem { get; set; }
    }
}
