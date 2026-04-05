using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class ReasonOfReturnVM : GenericAuditVM
    {
        [Required]
        [StringLength(50)]
        [Display(Name = "Code")]
        public string ReasonOfReturnCode { get; set; }

        [Required]
        [StringLength(100)]
        [MinLength(5)]
        [Display(Name = "Name")]
        public string ReasonOfReturnName { get; set; }
    }
}
