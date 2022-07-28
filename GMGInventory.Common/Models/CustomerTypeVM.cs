using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class CustomerTypeVM : GenericAuditVM
    {
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [StringLength(100)]
        [MinLength(5)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        public bool IsActive { get; set; }
    }
}
