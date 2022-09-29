using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class PurchaseOrder : BaseEntity
    {
        public PurchaseOrder()
        {
            this.PurchaseOrderLineItems = new HashSet<PurchaseOrderLineItem>();
            this.PurchaseReturns = new HashSet<PurchaseReturn>();
            this.SalesOrders = new HashSet<SalesOrder>();
        }

        [ForeignKey("SupplierId")]
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string PONumber { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? ApprovedBy { get; set; }

        [ForeignKey("DestinationId")]
        public int DestinationId { get; set; }
        public Warehouse Destination { get; set; }

        [ForeignKey("CurrencyId")]
        public int? CurrencyId { get; set; }
        public Currency? Currency { get; set; }

        [ForeignKey("StatusId")]
        public int StatusId { get; set; }
        public Status Status { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TotalPurchaseAmount { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? ReferenceNumber { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal CurrencyRate { get; set; }

        public DateTime ReplenishDate { get; set; }

        public ICollection<PurchaseOrderLineItem> PurchaseOrderLineItems { get; set; }
        public ICollection<PurchaseReturn> PurchaseReturns { get; set; }
        public ICollection<SalesOrder> SalesOrders { get; set; }
    }
}
