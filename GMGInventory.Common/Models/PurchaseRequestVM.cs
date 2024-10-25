using GMGInventory.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class PurchaseRequestVM
    {
        public int Id { get; set; }

        [Display(Name = "Doc Number")]
        public string DocumentNumber { get; set; }

        [Display(Name = "Type")]
        public string Type { get; set; }

        [Display(Name = "Purpose")]
        public string Purpose { get; set; }

        [Display(Name = "Area")]
        public string Area { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Date Requested")]
        public string RequestDate => RequestDateD.ToString(SystemFormat.StringDateTime);
        public DateTime RequestDateD { get; set; }
    }

    public class PurchaseRequestDetailedVM
    {

        [Display(Name = "Created By")]
        public string UserCreated { get; set; }

        [Display(Name = "Date Created")]
        public string DateCreated => DateCreatedD.ToString(SystemFormat.StringDateTime);
        public DateTime DateCreatedD { get; set; }

        [Display(Name = "Modified By")]
        public string? UserModified { get; set; }

        [Display(Name = "Date Modified")]
        public string DateModified => DateModifiedD == null ? "-" : DateModifiedD.Value.ToString(SystemFormat.StringDateTime);
        public DateTime? DateModifiedD { get; set; }
    }
}
