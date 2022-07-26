using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class StockCard : BaseEntity
    {
        [ForeignKey("ItemId")]
        public int ItemId { get; set; }
        public Item Item { get; set; }

        public int? In { get; set; }
        public int? Out { get; set; }
        public int? Balance { get; set; }
        public DateTime? InOutDate { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string? Remarks { get; set; }
    }
}
