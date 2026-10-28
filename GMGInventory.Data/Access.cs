using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class Access : BaseEntity
    {
        public Access()
        {
        }

        [ForeignKey("Role")]
        public string RoleId { get; set; }

        public virtual IdentityRole Role { get; set; }

        [ForeignKey("PageId")]
        public int PageId { get; set; }
        public Page Page { get; set; }

        public bool CanView { get; set; }

        public bool CanAdd { get; set; }

        public bool CanEdit { get; set; }

        public bool CanDelete { get; set; }

        public bool? CanApproveDisapprove { get; set; }

        public bool? CanUploadDownload { get; set; }

        public bool? CanPrint { get; set; }

        public bool? CanForceClose { get; set; }
    }
}
