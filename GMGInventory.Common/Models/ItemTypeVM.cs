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

        public int? CategoryId { get; set; }
        public string Category { get; set; }
    }

    public class ItemTypeViewVM : GenericAuditVM
    {
        [Display(Name = "Category")]
        public string? Category { get; set; }
        [Display(Name = "Category Code")]
        public string? CategoryCode { get; set; }
        public int? CategoryId { get; set; }
    }

    public class ItemTypeFileVM : ItemTypeVM
    {
        public string? Status { get; set; }
    }
}
