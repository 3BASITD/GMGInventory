using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class ItemPromo : BaseEntity
    {
        public ItemPromo() { 
        }
        [ForeignKey("ItemId")]
        public int ItemId { get; set; }
        public Item Item { get; set; }

        [Column(TypeName = "nvarchar(300)")]
        public string Name { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal PromoPrice { get; set; }
        
        public DateTime EffectiveDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }

    }
}
