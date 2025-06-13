using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class PurchaseOrderNewLineItemNewCost
    {
        public int Id { get; set; }

        [ForeignKey("ItemId")]
        public int OrderItemId { get; set; }
        public virtual PurchaseOrderNewLineItem OrderItem { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal PreviousCost { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal NewCost { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string Remarks { get; set; }

        [Column(TypeName = "nvarchar(450)")]
        public string UserCreated { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
