using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class PurchaseRequest : BaseEntity
    {
        public PurchaseRequest()
        {
            this.PurchaseOrderLineItems = new HashSet<PurchaseOrderNewLineItem>();
            this.PurchaseRequestLineItems = new HashSet<PurchaseRequestLineItem>();
        }

        public string DocumentNumber { get; set; }

        [ForeignKey("TypeId")]
        public int TypeId { get; set; }
        public virtual PurchaseRequestType Type { get; set; }

        [ForeignKey("PurposeId")]
        public int? PurposeId { get; set; }
        public virtual PurchaseRequestPurpose Purpose { get; set; }

        [ForeignKey("AreaId")]
        public int? AreaId { get; set; }
        public virtual Area Area { get; set; }

        public DateTime RequestDate { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string? PurposeDefinition { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Remarks { get; set; }

        [ForeignKey("StatusId")]
        public int? StatusId { get; set; }
        public virtual PurchaseStatus Status { get; set; }


        [Column(TypeName = "nvarchar(100)")]
        public string? PreparedBy { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? CheckedBy { get; set; }

        [ForeignKey("Approver1Id")]
        public string? Approver1Id { get; set; }
        public virtual User Approver1 { get; set; }

        [ForeignKey("Approver2Id")]
        public string? Approver2Id { get; set; }
        public virtual User Approver2 { get; set; }

        public ICollection<PurchaseOrderNewLineItem> PurchaseOrderLineItems { get; set; }
        public ICollection<PurchaseRequestLineItem> PurchaseRequestLineItems { get; set; }
    }
}
