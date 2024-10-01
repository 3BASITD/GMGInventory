using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class RegionVM : GenericAuditVM
    {
        [Required]
        [StringLength(10)]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [StringLength(100)]
        [MinLength(5)]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}
