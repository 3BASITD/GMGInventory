using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class PhysicalCountHistory : BaseEntity
    {
        [ForeignKey("ItemId")]
        public int ItemId { get; set; }
        public Item Item { get; set; }

        [ForeignKey("WarehouseId")]
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }

        public int QuantityFrom { get; set; }
        public int QuantityTo { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string? Remarks { get; set; }
    }
}
