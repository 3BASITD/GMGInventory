using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class PurchaseOrderType : BaseTypeEntity
    {
        public PurchaseOrderType()
        {
            this.PurchaseOrders = new HashSet<PurchaseOrderNew>();
        }

        [ForeignKey("RequestTypeId")]
        public int RequestTypeId { get; set; }
        public virtual PurchaseRequestType RequestType { get; set; }

        public ICollection<PurchaseOrderNew> PurchaseOrders { get; set; }
    }
}
