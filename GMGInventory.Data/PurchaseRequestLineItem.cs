using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class PurchaseRequestLineItem : BaseEntity
    {
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

        [Column(TypeName = "nvarchar(100)")]
        public string ItemDescription { get; set; }

        public int StockOnHand { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("UnitId")]
        public int UnitId { get; set; }
        public virtual Unit Unit { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Purpose { get; set; }
    }
}
