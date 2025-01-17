using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class PurchaseOrderNew : BaseEntity
    {
        public PurchaseOrderNew()
        {
            PurchaseOrderResubmits = new HashSet<PurchaseOrderNew>();
            PurchaseOrderLineItems = new HashSet<PurchaseOrderNewLineItem>();
            PurchaseReceivings = new HashSet<PurchaseReceiving>();
            SalesOrders = new HashSet<SalesOrder>();
        }

        public string DocumentNumber { get; set; }

        [ForeignKey("TypeId")]
        public int TypeId { get; set; }
        public virtual PurchaseOrderType Type { get; set; }

        [ForeignKey("SupplierId")]
        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }

        [ForeignKey("TermId")]
        public int TermId { get; set; }
        public virtual CreditTerm Term { get; set; }

        [ForeignKey("DeliveryModeId")]
        public int DeliveryModeId { get; set; }
        public virtual PurchaseOrderDeliveryMode DeliveryMode { get; set; }

        public DateTime OrderDate { get; set; }

        [ForeignKey("DeliveryAddressId")]
        public int DeliveryAddressId { get; set; }
        public virtual Warehouse DeliveryAddress { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string CanvasSummaryReference { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string Remarks { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal OtherCharges { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Discount1 { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Discount2 { get; set; }

        [ForeignKey("StatusId")]
        public int? StatusId { get; set; }
        public virtual PurchaseStatus Status { get; set; }

        [ForeignKey("ApproverId")]
        public string? ApproverId { get; set; }
        public virtual User Approver { get; set; }

        [ForeignKey("DisapproverId")]
        public string? DisapproverId { get; set; }
        public virtual User Disapprover { get; set; }

        public string? DisapprovedReason { get; set; }

        [ForeignKey("ResubmitId")]
        public int? ResubmitId { get; set; }
        public virtual PurchaseOrderNew Resubmit { get; set; }

        public ICollection<PurchaseOrderNew> PurchaseOrderResubmits { get; set; }
        public ICollection<PurchaseOrderNewLineItem> PurchaseOrderLineItems { get; set; }
        public ICollection<PurchaseReceiving> PurchaseReceivings { get; set; }
        public ICollection<SalesOrder> SalesOrders { get; set; }
    }
}
