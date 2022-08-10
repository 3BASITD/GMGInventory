using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class BrandVM
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

        [Required]
        [Display(Name = "Item Types")]
        public int ItemTypeId { get; set; }
        public string ItemType { get; set; }
        public List<SelectListItem>? ItemTypes { get; set; }
    }

    public class BrandViewVM : GenericAuditVM
    {
        [Display(Name = "Item Type")]
        public string ItemType { get; set; }
        public int ItemTypeId { get; set; }
    }
}
