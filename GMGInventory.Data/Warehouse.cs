using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class Warehouse : BaseEntity
    {
        public Warehouse()
        {
            this.ItemInventories = new HashSet<ItemInventory>();
            this.Packs = new HashSet<Pack>();
            this.PhysicalCountHistories = new HashSet<PhysicalCountHistory>();
            this.PurchaseOrders = new HashSet<PurchaseOrder>();
            this.PurchaseOrderNews = new HashSet<PurchaseOrderNew>();
            this.PurchaseReceivings = new HashSet<PurchaseReceiving>();
            this.PurchaseReturns = new HashSet<PurchaseReturnNew>();
            this.SourceWarehouse = new HashSet<TransferItem>();
            this.DestinationWarehouse = new HashSet<TransferItem>();
        }

        [Column(TypeName = "nvarchar(10)")]
        public string Code { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string? Address { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? ContactPerson { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? ContactNumber { get; set; }

        public bool? IsMain { get; set; }
        public bool IsActive { get; set; }

        public ICollection<ItemInventory> ItemInventories { get; set; }
        public ICollection<PhysicalCountHistory> PhysicalCountHistories { get; set; }
        public ICollection<Pack> Packs { get; set; }
        public ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public ICollection<PurchaseOrderNew> PurchaseOrderNews { get; set; }
        public ICollection<PurchaseReceiving> PurchaseReceivings { get; set; }
        public ICollection<PurchaseReturnNew> PurchaseReturns { get; set; }

        [InverseProperty(nameof(TransferItem.Source))]
        public ICollection<TransferItem> SourceWarehouse { get; set; }

        [InverseProperty(nameof(TransferItem.Destination))]
        public ICollection<TransferItem> DestinationWarehouse { get; set; }
    }
}
