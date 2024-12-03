using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class PurchaseReceiving : BaseEntity
    {
        public PurchaseReceiving()
        {
            PurchaseReceivingResubmits = new HashSet<PurchaseReceiving>();
            PurchaseReceivingLineItems = new HashSet<PurchaseReceivingLineItem>();
            PurchaseReturns = new HashSet<PurchaseReturnNew>();
        }

        public string DocumentNumber { get; set; }

        [ForeignKey("PurchaseOrderId")]
        public int PurchaseOrderId { get; set; }
        public virtual PurchaseOrderNew PurchaseOrder { get; set; }

        public DateTime ReceiveDate { get; set; }

        [ForeignKey("WarehouseId")]
        public int WarehouseId { get; set; }
        public virtual Warehouse Warehouse { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string DeliveryReceiptNumber { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string SalesInvoiceNumber { get; set; }

        public string Remarks { get; set; }

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
        public virtual PurchaseReceiving Resubmit { get; set; }

        public ICollection<PurchaseReceiving> PurchaseReceivingResubmits { get; set; }
        public ICollection<PurchaseReceivingLineItem> PurchaseReceivingLineItems { get; set; }
        public ICollection<PurchaseReturnNew> PurchaseReturns { get; set; }
    }
}
