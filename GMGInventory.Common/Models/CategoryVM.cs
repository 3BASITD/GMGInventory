using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class CategoryVM: GenericAuditVM
    {
        [Required]
        [StringLength(10)]
        public new string Code { get; set; }

        [Required]
        [StringLength(100)]
        [MinLength(5)]
        public new string Name { get; set; }
    }

    public class CategoryFileVM : CategoryVM
    {
        public string? Status { get; set; }
    }
}
