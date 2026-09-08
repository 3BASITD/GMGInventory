using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class ItemInventory : BaseEntity
    {
        [ForeignKey("WarehouseId")]
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }

        [ForeignKey("ItemId")]
        public int ItemId { get; set; }
        public Item Item { get; set; }

        public int Quantity { get; set; }
        public int AllocatedQuantity { get; set; }

        public string? BinLocation { get; set; }

        [ForeignKey("LocationCodeId")]
        public int? LocationCodeId { get; set; }
        public LocationCode LocationCode { get; set; }
    }
}
