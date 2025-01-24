using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class Shipping : BaseEntity
    {
        public Shipping()
        {
            ShippingLineItem = new HashSet<ShippingLineItem>();
        }

        [ForeignKey("SalesOrderId")]
        public int SalesOrderId { get; set; }
        public virtual SalesOrder SalesOrder { get; set; }

        public ICollection<ShippingLineItem> ShippingLineItem { get; set; }
    }
}
