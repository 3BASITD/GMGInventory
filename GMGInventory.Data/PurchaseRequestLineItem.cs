using GMGInventory.Common.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class PurchaseRequestLineItem : BaseEntity
    {
        public PurchaseRequestLineItem()
        {
            this.PurchaseOrderLineItems = new HashSet<PurchaseOrderLineItem>();
        }

        [ForeignKey("PurchaseRequestId")]
        public int PurchaseRequestId { get; set; }

        public virtual PurchaseRequest PurchaseRequest { get; set; }

        public int LineNumber { get; set; }

        [ForeignKey("SalesOrderId")]
        public int? SalesOrderId { get; set; }
        public virtual SalesOrder SalesOrder { get; set; }

        [ForeignKey("ItemId")]
        public int? ItemId { get; set; }
        public virtual Item Item { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string ItemDescription { get; set; }

        [ForeignKey("UnitId")]
        public int? UnitId { get; set; }
        public virtual Unit Unit { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Purpose { get; set; }

        public bool? HaveStock { get; set; }

        public ICollection<PurchaseOrderLineItem> PurchaseOrderLineItems { get; set; }
    }
}
