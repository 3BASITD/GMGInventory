using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class ItemPrice : BaseEntity
    {
        [ForeignKey("ItemId")]
        public int ItemId { get; set; }
        public Item Item { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal NetSellingPrice { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal RetailSellingPrice { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal WholesaleSellingPrice { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal SpecialPrice { get; set; }

        public DateTime EffectiveDate { get; set; }
    }
}
