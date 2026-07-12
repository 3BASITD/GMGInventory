using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class User : IdentityUser
    {
        public User()
        {
            this.InvoiceHashMicroMoveBy = new HashSet<Invoice>();
            this.KYCSubmitter = new HashSet<KYC>();
            this.KYCApprover = new HashSet<KYC>();
            this.KYCDisapprover = new HashSet<KYC>();
            this.PurchaseOrderApprover = new HashSet<PurchaseOrderNew>();
            this.PurchaseOrderDisapprover = new HashSet<PurchaseOrderNew>();
            this.PurchaseReceivingApprover = new HashSet<PurchaseReceiving>();
            this.PurchaseReceivingDisapprover = new HashSet<PurchaseReceiving>();
            this.PurchaseReceivingHashMicroMoveBy = new HashSet<PurchaseReceiving>();
            this.PurchaseRequestApprover1 = new HashSet<PurchaseRequest>();
            this.PurchaseRequestApprover2 = new HashSet<PurchaseRequest>();
            this.PurchaseRequestDisapprover = new HashSet<PurchaseRequest>();
            this.PurchaseReturnApprover = new HashSet<PurchaseReturnNew>();
            this.PurchaseReturnDisapprover = new HashSet<PurchaseReturnNew>();
            this.TrackerTaskAssignee = new HashSet<Tracker_Task>();
            this.TrackerTaskApprover = new HashSet<Tracker_Task>();
            this.TrackerTaskDispprover = new HashSet<Tracker_Task>();
        }

        [Column(TypeName = "nvarchar(50)")]
        public string? FirstName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? LastName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Nickname { get; set; }

        public bool IsDarkMode { get; set; }
        public bool? IsNavIconPlain { get; set; }
        public bool IsActive { get; set; }

        [Column(TypeName = "nvarchar(450)")]
        public string UserCreated { get; set; }

        [Column(TypeName = "nvarchar(450)")]
        public string? UserModified { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }


        [InverseProperty(nameof(Invoice.HashMicroMoveBy))]
        public ICollection<Invoice> InvoiceHashMicroMoveBy { get; set; }

        [InverseProperty(nameof(KYC.Submitter))]
        public ICollection<KYC> KYCSubmitter { get; set; }

        [InverseProperty(nameof(KYC.Approver))]
        public ICollection<KYC> KYCApprover { get; set; }

        [InverseProperty(nameof(KYC.Disapprover))]
        public ICollection<KYC> KYCDisapprover { get; set; }

        [InverseProperty(nameof(PurchaseOrderNew.Approver))]
        public ICollection<PurchaseOrderNew> PurchaseOrderApprover { get; set; }

        [InverseProperty(nameof(PurchaseOrderNew.Disapprover))]
        public ICollection<PurchaseOrderNew> PurchaseOrderDisapprover { get; set; }

        [InverseProperty(nameof(PurchaseReceiving.Approver))]
        public ICollection<PurchaseReceiving> PurchaseReceivingApprover { get; set; }

        [InverseProperty(nameof(PurchaseReceiving.Disapprover))]
        public ICollection<PurchaseReceiving> PurchaseReceivingDisapprover { get; set; }

        [InverseProperty(nameof(PurchaseReceiving.HashMicroMoveBy))]
        public ICollection<PurchaseReceiving> PurchaseReceivingHashMicroMoveBy { get; set; }

        [InverseProperty(nameof(PurchaseRequest.Approver1))]
        public ICollection<PurchaseRequest> PurchaseRequestApprover1 { get; set; }

        [InverseProperty(nameof(PurchaseRequest.Approver2))]
        public ICollection<PurchaseRequest> PurchaseRequestApprover2 { get; set; }

        [InverseProperty(nameof(PurchaseRequest.Disapprover))]
        public ICollection<PurchaseRequest> PurchaseRequestDisapprover { get; set; }

        [InverseProperty(nameof(PurchaseReturnNew.Approver))]
        public ICollection<PurchaseReturnNew> PurchaseReturnApprover { get; set; }

        [InverseProperty(nameof(PurchaseReturnNew.Disapprover))]
        public ICollection<PurchaseReturnNew> PurchaseReturnDisapprover { get; set; }

        [InverseProperty(nameof(Tracker_Task.Assignee))]
        public ICollection<Tracker_Task> TrackerTaskAssignee { get; set; }

        [InverseProperty(nameof(Tracker_Task.ApprovedBy))]
        public ICollection<Tracker_Task> TrackerTaskApprover { get; set; }

        [InverseProperty(nameof(Tracker_Task.DisapprovedBy))]
        public ICollection<Tracker_Task> TrackerTaskDispprover { get; set; }
    }
}
