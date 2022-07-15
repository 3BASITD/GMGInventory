using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class TransferItem : BaseEntity
    {
        [ForeignKey("FromWarehouseId")]
        public int FromWarehouseId { get; set; }
        public Warehouse FromWarehouse { get; set; }

        [ForeignKey("ToWarehouseId")]
        public int ToWarehouseId { get; set; }
        public Warehouse ToWarehouse { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string Remarks { get; set; }
    }
}
