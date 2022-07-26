using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class TransferItem : BaseEntity
    {
        public TransferItem()
        {
            this.TransferItemDetails = new HashSet<TransferItemDetail>();
        }

        [ForeignKey("FromWarehouseId")]
        public int FromWarehouseId { get; set; }
        public Warehouse FromWarehouse { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string Remarks { get; set; }

        public ICollection<TransferItemDetail> TransferItemDetails { get; set; }
    }
}
