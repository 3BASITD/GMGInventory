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

        [ForeignKey("LocationCodeId")]
        public int? LocationCodeId { get; set; }
        public LocationCode LocationCode { get; set; }
        public int Quantity { get; set; }

        public string? BarcotechTransferCode { get; set; }
    }
}
