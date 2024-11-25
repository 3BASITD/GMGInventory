using GMGInventory.Common.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class PurchaseReturnNew : BaseEntity
    {
        public PurchaseReturnNew()
        {
            this.PurchaseReturnResubmits = new HashSet<PurchaseReturnNew>();
            this.PurchasReturnLineItem = new HashSet<PurchaseReturnNewLineItem>();
        }

        public string DocumentNumber { get; set; }

        [ForeignKey("SupplierId")]
        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }

        [ForeignKey("PurchaseOrderId")]
        public int PurchaseOrderId { get; set; }
        public virtual PurchaseOrderNew PurchaseOrder { get; set; }

        [ForeignKey("PurchaseReceiveId")]
        public int PurchaseReceiveId { get; set; }
        public virtual PurchaseReceiving PurchaseReceive { get; set; }
        public DateTime ReturnDate { get; set; }

        [ForeignKey("WarehouseId")]
        public int WarehouseId { get; set; }
        public virtual Warehouse Warehouse { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string DeliveryReceiptNumber { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string SalesInvoiceNumber { get; set; }

        [ForeignKey("CurrencyId")]
        public int CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal CurrencyRate { get; set; }

        public string Remarks { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Discount1 { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Discount2 { get; set; }

        [ForeignKey("StatusId")]
        public int? StatusId { get; set; }
        public virtual PurchaseStatus Status { get; set; }

        //public string? PreparedBy { get; set; }
        //public string? CheckedBy { get; set; }

        [ForeignKey("ApproverId")]
        public string? ApproverId { get; set; }
        public virtual User Approver { get; set; }

        [ForeignKey("DisapproverId")]
        public string? DisapproverId { get; set; }
        public virtual User Disapprover { get; set; }

        public string? DisapprovedReason { get; set; }

        [ForeignKey("ResubmitId")]
        public int? ResubmitId { get; set; }
        public PurchaseReturnNew Resubmit { get; set; }

        public ICollection<PurchaseReturnNew> PurchaseReturnResubmits { get; set; }

        public ICollection<PurchaseReturnNewLineItem> PurchasReturnLineItem { get; set; }
    }
}
