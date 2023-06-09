using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class CategoryVM: GenericAuditVM
    {
        [Required]
        [StringLength(10)]
        [Display(Name = "Code")]
        public string CategoryCode { get; set; }

        [Required]
        [StringLength(100)]
        [MinLength(5)]
        [Display(Name = "Name")]
        public string CategoryName { get; set; }
    }
}
