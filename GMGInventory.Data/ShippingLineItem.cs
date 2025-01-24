using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class ShippingLineItem : BaseEntity
    {

        [ForeignKey("ShippingId")]
        public int ShippingId { get; set; }
        public virtual Shipping Shipping { get; set; }

        [ForeignKey("SalesOrderLineItemId")]
        public int SalesOrderLineItemId { get; set; }
        public virtual SalesOrderLineItem SalesOrderLineItem { get; set; }
    }
}
