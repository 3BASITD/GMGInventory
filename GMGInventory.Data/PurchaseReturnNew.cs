using GMGInventory.Common.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    [Index(nameof(DocumentNumber), IsUnique = true)]
    public class PurchaseReturnNew : BaseEntity
    {
        public PurchaseReturnNew()
        {
            PurchaseReturnResubmits = new HashSet<PurchaseReturnNew>();
            PurchasReturnLineItem = new HashSet<PurchaseReturnNewLineItem>();
        }

        public string DocumentNumber { get; set; }

        [ForeignKey("PurchaseReceiveId")]
        public int PurchaseReceiveId { get; set; }
        public virtual PurchaseReceiving PurchaseReceive { get; set; }

        public DateTime ReturnDate { get; set; }

        [ForeignKey("WarehouseId")]
        public int WarehouseId { get; set; }
        public virtual Warehouse Warehouse { get; set; }

        [Column(TypeName = "nvarchar(501)")]
        public string? Remarks { get; set; }

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
        public virtual PurchaseReturnNew Resubmit { get; set; }

        public DateTime? BarcotechRequestDate { get; set; }

        public ICollection<PurchaseReturnNew> PurchaseReturnResubmits { get; set; }
        public ICollection<PurchaseReturnNewLineItem> PurchasReturnLineItem { get; set; }
    }
}
