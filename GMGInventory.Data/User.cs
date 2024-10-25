using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class User : IdentityUser
    {
        public User()
        {
            this.PurchaseOrderApprover = new HashSet<PurchaseOrderNew>();
            this.PurchaseReceivingApprover = new HashSet<PurchaseReceiving>();
            this.PurchaseRequestApprover1 = new HashSet<PurchaseRequest>();
            this.PurchaseRequestApprover2 = new HashSet<PurchaseRequest>();
            this.PurchaseRequestDisapprover = new HashSet<PurchaseRequest>();
            this.PurchaseReturnApprover = new HashSet<PurchaseReturnNew>();
        }

        [Column(TypeName = "nvarchar(50)")]
        public string? FirstName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? LastName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Nickname { get; set; }

        public bool IsDarkMode { get; set; }
        public bool IsActive { get; set; }

        [Column(TypeName = "nvarchar(450)")]
        public string UserCreated { get; set; }

        [Column(TypeName = "nvarchar(450)")]
        public string? UserModified { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }


        public ICollection<PurchaseOrderNew> PurchaseOrderApprover { get; set; }
        public ICollection<PurchaseReceiving> PurchaseReceivingApprover { get; set; }

        [InverseProperty(nameof(PurchaseRequest.Approver1))]
        public ICollection<PurchaseRequest> PurchaseRequestApprover1 { get; set; }

        [InverseProperty(nameof(PurchaseRequest.Approver2))]
        public ICollection<PurchaseRequest> PurchaseRequestApprover2 { get; set; }
        public ICollection<PurchaseRequest> PurchaseRequestDisapprover { get; set; }
        public ICollection<PurchaseReturnNew> PurchaseReturnApprover { get; set; }
    }
}
