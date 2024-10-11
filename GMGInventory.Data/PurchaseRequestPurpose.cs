using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class PurchaseRequestPurpose : BaseTypeEntity
    {
        public PurchaseRequestPurpose()
        {
            this.PurchaseRequests = new HashSet<PurchaseRequest>();
        }

        [ForeignKey("TypeId")]
        public int TypeId { get; set; }
        public virtual PurchaseRequestType Type { get; set; }

        public ICollection<PurchaseRequest> PurchaseRequests { get; set; }
    }
}
