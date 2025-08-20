using GMGInventory.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class WarehouseVM
    {
        public int? Id { get; set; }

        [Display(Name = "Code")]
        public string Code { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; } 

        [Display(Name = "Address")]
        public string? Address { get; set; }

        [Display(Name = "Contact Person")]
        public string? ContactPerson { get; set; }

        [Display(Name = "Contact Number")]
        public string? ContactNumber { get; set; }

        [Display(Name = "Is Main")]
        public bool IsMain { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [Display(Name = "User Created")]
        public string UserCreated { get; set; }

        [Display(Name = "Date Created")]
        public string DateCreated => DateCreatedD.ToString(SystemFormat.StringDate);
        public DateTime DateCreatedD { get; set; }

        [Display(Name = "User Modified")]
        public string? UserModified { get; set; }

        [Display(Name = "Date Modified")]
        public string? DateModified => DateModifiedD == null ? null : DateModifiedD.Value.ToString(SystemFormat.StringDate);
        public DateTime? DateModifiedD { get; set; }
    }
}
