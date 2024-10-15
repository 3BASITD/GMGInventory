using GMGInventory.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class PurchaseReceiveVM
    {
        public int Id { get; set; }

        [Display(Name = "Doc Number")]
        public string DocumentNumber { get; set; }

        [Display(Name = "PO Number")]
        public string PONumber { get; set; }

        [Display(Name = "Supplier")]
        public string Supplier { get; set; }

        [Display(Name = "Term")]
        public string Term { get; set; }

        [Display(Name = "Delivery Address")]
        public string DeliveryAddress { get; set; }

        [Display(Name = "DR Number")]
        public string DRNumber { get; set; }

        [Display(Name = "SI Number")]
        public string SINumber { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Date Received")]
        public string ReceiveDate => ReceiveDateD.ToString(SystemFormat.StringDateTime);
        public DateTime ReceiveDateD { get; set; }
    }

    public class PurchaseReceiveDetailsVM :PurchaseReceiveVM
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
