using GMGInventory.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class PurchaseOrderNewVM
    {
        public int Id { get; set; }

        [Display(Name = "Doc Number")]
        public string DocumentNumber { get; set; }

        [Display(Name = "Type")]
        public string Type { get; set; }

        [Display(Name = "Supplier")]
        public string Supplier { get; set; }

        [Display(Name = "Term")]
        public string Term { get; set; }

        [Display(Name = "Delivery Mode")]
        public string DeliveryMode { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Date Ordered")]
        public string OrderDate => OrderDateD.ToString(SystemFormat.StringDateTime);
        public DateTime OrderDateD { get; set; }
    }

    public class PurchaseOrderNewDetailedVM :PurchaseOrderNewVM
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
