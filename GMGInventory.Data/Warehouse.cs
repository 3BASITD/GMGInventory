using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class Warehouse : BaseEntity
    {
        public Warehouse()
        {
            this.ItemInventories = new HashSet<ItemInventory>();
            this.PhysicalCountHistories = new HashSet<PhysicalCountHistory>();
            this.PurchaseOrders = new HashSet<PurchaseOrder>();
            this.TransferItems = new HashSet<TransferItem>();
            this.TransferItemDetails = new HashSet<TransferItemDetail>();
            this.SourceWarehouse = new HashSet<TransferItem>();
            this.DestinationWarehouse = new HashSet<TransferItem>();
        }

        [Column(TypeName = "nvarchar(10)")]
        public string Code { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string AddressLine1 { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string? AddressLine2 { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string ContactPerson { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string ContactNumber { get; set; }

        public bool IsActive { get; set; }

        public ICollection<ItemInventory> ItemInventories { get; set; }
        public ICollection<PhysicalCountHistory> PhysicalCountHistories { get; set; }
        public ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public ICollection<TransferItem> TransferItems { get; set; }
        public ICollection<TransferItemDetail> TransferItemDetails { get; set; }

        [InverseProperty(nameof(TransferItem.Source))]
        public ICollection<TransferItem> SourceWarehouse { get; set; }

        [InverseProperty(nameof(TransferItem.Destination))]
        public ICollection<TransferItem> DestinationWarehouse { get; set; }
    }
}
