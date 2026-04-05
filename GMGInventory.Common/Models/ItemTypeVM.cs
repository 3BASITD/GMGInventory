using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class ItemTypeVM
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

    public class ItemTypeViewVM : GenericAuditVM
    {
    }

    public class ItemTypeFileVM : ItemTypeVM
    {
        public string? Status { get; set; }
    }
}
