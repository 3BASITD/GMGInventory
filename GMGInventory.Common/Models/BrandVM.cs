using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class BrandVM
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [StringLength(100)]
        [MinLength(5)]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }

    public class BrandViewVM : GenericAuditVM
    {
    }

    public class BrandFileVM : BrandVM
    {
        public string? Status { get; set; }
    }
}