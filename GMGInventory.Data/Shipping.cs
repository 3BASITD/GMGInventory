using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class Shipping : BaseEntity
    {
        public Shipping()
        {
            ShippingLineItem = new HashSet<ShippingLineItem>();
        }

        [ForeignKey("InvoiceId")]
        public int InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; }

        public ICollection<ShippingLineItem> ShippingLineItem { get; set; }
    }
}
