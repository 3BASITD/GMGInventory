using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class PurchaseReturn : BaseEntity
    {
        public PurchaseReturn()
        {
            this.PurchaseReturnLineItems = new HashSet<PurchaseReturnLineItem>();
        }

        [ForeignKey("PurchaseOrder")]
        public int PurchaseOrderId { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }

        [Column(TypeName = "nvarchar(501)")]
        public string? Remarks { get; set; }

        public ICollection<PurchaseReturnLineItem> PurchaseReturnLineItems { get; set; }
    }
}
