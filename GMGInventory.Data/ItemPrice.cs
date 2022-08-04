using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class ItemPrice
    {
        public int Id { get; set; }

        [ForeignKey("ItemId")]
        public int ItemId { get; set; }
        public Item Item { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal NetSellingPrice { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal RetailSellingPrice { get; set; }

        [Column(TypeName = "nvarchar(450)")]
        public string UserCreated { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
