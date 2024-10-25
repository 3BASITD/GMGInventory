using System.ComponentModel.DataAnnotations.Schema;


namespace GMGInventory.Data
{
    public class ItemConversion 
    {
        public int Id { get; set; }

        [ForeignKey("ItemId")]
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public int Quantity { get; set; }
        public int? SubItemId { get; set; }
        public Item SubItem { get; set; }
        public int? UnitId { get; set; }
        public Unit Unit { get; set; }
        public int? EquivalentQuantity { get; set; }
        public int? EquivalentUnitId { get; set; }
        public Unit EquivalentUnit { get; set; }
        public int OrderNo { get; set; }

        [Column(TypeName = "nvarchar(450)")]
        public string UserCreated { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
