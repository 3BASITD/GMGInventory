using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class PurchaseOrderNewLineItemNewQuantity
    {
        public int Id { get; set; }

        [ForeignKey("ItemId")]
        public int OrderItemId { get; set; }
        public virtual PurchaseOrderNewLineItem OrderItem { get; set; }
        public int PreviousQuantity { get; set; }
        public int NewQuantity { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string Remarks { get; set; }

        [Column(TypeName = "nvarchar(450)")]
        public string UserCreated { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
