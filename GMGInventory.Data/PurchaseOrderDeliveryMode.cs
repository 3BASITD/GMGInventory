using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class PurchaseOrderDeliveryMode : BaseTypeEntity
    {
        public PurchaseOrderDeliveryMode()
        {
            this.PurchaseOrderNews = new HashSet<PurchaseOrderNew>();
        }

        public ICollection<PurchaseOrderNew> PurchaseOrderNews { get; set; }
    }
}
