using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class PurchaseReceiving : BaseEntity
    {
        public PurchaseReceiving()
        {
            this.PurchaseReceivingLineItems = new HashSet<PurchaseReceivingLineItem>();
        }

        public string DocumentNumber { get; set; }

        [ForeignKey("SupplierId")]
        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }

        [ForeignKey("PurchasePaymentTermId")]
        public int PurchasePaymentTermId { get; set; }
        public virtual PurchasePaymentTerm PurchasePaymentTerm { get; set; }

        [ForeignKey("PurchaseOrderId")]
        public int PurchaseOrderId { get; set; }
        public virtual PurchaseOrderNew PurchaseOrder { get; set; }
        public DateTime ReceiveDate { get; set; }

        [ForeignKey("DeliveryAddressId")]
        public int DeliveryAddressId { get; set; }
        public virtual Warehouse DeliveryAddress { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string DeliveryReceiptNumber { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string SalesInvoiceNumber { get; set; }

        public string Remarks { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Discount1 { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Discount2 { get; set; }

        [ForeignKey("StatusId")]
        public int? StatusId { get; set; }
        public virtual PurchaseStatus Status { get; set; }

        public string? PreparedBy { get; set; }
        public string? CheckedBy { get; set; }

        [ForeignKey("ApproverId")]
        public string? ApproverId { get; set; }
        public virtual User Approver { get; set; }

        public ICollection<PurchaseReceivingLineItem> PurchaseReceivingLineItems { get; set; }
    }
}
