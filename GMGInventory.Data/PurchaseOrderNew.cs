using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class PurchaseOrderNew : BaseEntity
    {
        public PurchaseOrderNew()
        {
            this.PurchaseOrderLineItems = new HashSet<PurchaseOrderNewLineItem>();
            this.PurchaseReceivings = new HashSet<PurchaseReceiving>();
            this.PurchaseReturns = new HashSet<PurchaseReturnNew>();
        }

        public string DocumentNumber { get; set; }

        [ForeignKey("TypeId")]
        public int TypeId { get; set; }
        public virtual PurchaseOrderType Type { get; set; }

        [ForeignKey("SupplierId")]
        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }


        [ForeignKey("PurchaseOrderTermId")]
        public int PurchaseOrderTermId { get; set; }
        public virtual PurchasePaymentTerm PurchaseOrderTerm { get; set; }


        [ForeignKey("PurchaseOrderDeliveryModeId")]
        public int PurchaseOrderDeliveryModeId { get; set; }
        public virtual PurchaseOrderDeliveryMode PurchaseOrderDeliveryMode { get; set; }

        public DateTime OrderDate { get; set; }

        [ForeignKey("DeliveryAddressId")]
        public int DeliveryAddressId { get; set; }
        public virtual Warehouse DeliveryAddress { get; set; }

        [ForeignKey("CurrencyId")]
        public int CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal CurrencyRate { get; set; }

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


        [Column(TypeName = "nvarchar(100)")]
        public string? PreparedBy { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? CheckedBy { get; set; }

        [ForeignKey("ApproverId")]
        public string? ApproverId { get; set; }
        public virtual User Approver { get; set; }

        public ICollection<PurchaseOrderNewLineItem> PurchaseOrderLineItems { get; set; }
        public ICollection<PurchaseReceiving> PurchaseReceivings { get; set; }
        public ICollection<PurchaseReturnNew> PurchaseReturns { get; set; }
    }
}
