using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class TransferItemDetail : BaseEntity
    {
        [ForeignKey("TransferItemId")]
        public int TransferItemId { get; set; }
        public TransferItem TransferItem { get; set; }

        [ForeignKey("ItemId")]
        public int ItemId { get; set; }
        public Item Item { get; set; }

        [ForeignKey("ToWarehouseId")]
        public int ToWarehouseId { get; set; }
        public Warehouse ToWarehouse { get; set; }

        public int Quantity { get; set; }
    }
}
