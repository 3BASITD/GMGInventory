using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class PurchaseReceivingLineItem : BaseEntity
    {

        [ForeignKey("PurchaseReceivingId")]
        public int PurchaseReceivingId { get; set; }
        public virtual PurchaseReceiving PurchaseReceiving { get; set; }
        public int LineNumber { get; set; }

        [ForeignKey("ItemId")]
        public int? ItemId { get; set; }
        public virtual Item Item { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string ItemDescription { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("UnitId")]
        public int UnitId { get; set; }
        public virtual Unit Unit { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal UnitCost { get; set; }
    }
}
