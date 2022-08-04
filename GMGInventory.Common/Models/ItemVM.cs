using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class ItemFilterVM
    {
        [Display(Name = "Category")]
        public int? CategoryId { get; set; }
        public SelectList? Categories { get; set; }

        [Display(Name = "Item Type")]
        public int? ItemTypeId { get; set; }
        public SelectList? ItemTypes { get; set; }

        [Display(Name = "Brand")]
        public int? BrandId { get; set; }
        public SelectList? Brands { get; set; }

        [Display(Name = "Warehouse")]
        public int? WarehouseId { get; set; }
        public SelectList? Warehouses { get; set; }

        public bool ActiveOnly { get; set; } = false;
    }

    public class ItemVM
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
        [StringLength(50)]
        [Display(Name = "Part Number")]
        public string PartNumber { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Interchange Number")]
        public string InterchangeNumber { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Size")]
        public string Size { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Model Number")]
        public string ModelNumber { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Unit Code")]
        public string UnitCode { get; set; }

        [Display(Name = "Non Inventory")]
        public bool IsNonInventory { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [Display(Name = "User Created")]
        public string UserCreated { get; set; }

        [Display(Name = "Date Created")]
        public string DateCreated { get; set; }

        [Display(Name = "User Modified")]
        public string UserModified { get; set; }

        [Display(Name = "Date Modified")]
        public string DateModified { get; set; }
    }

    public class ItemFormVM : ItemVM
    {
        [Required]
        [Display(Name = "Brand")]
        public int BrandId { get; set; }
        public SelectList? Brands { get; set; }

        [Required]
        [Display(Name = "Item Type")]
        public int ItemTypeId { get; set; }
        public SelectList? ItemTypes { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public SelectList? Categories { get; set; }
    }

    public class ItemViewVM : ItemVM
    {
        [Display(Name = "Brand")]
        public string Brand { get; set; }

        [Display(Name = "Item Type")]
        public string ItemType { get; set; }

        [Display(Name = "Category")]
        public string Category { get; set; }

        [Display(Name = "Retail Price")]
        public string RetailPrice { get; set; }

        [Display(Name = "Net Price")]
        public string NetPrice { get; set; }

        [Display(Name = "Quantity on Hand")]
        public int Quantity { get; set; }

        [Display(Name = "Warehouse")]
        public string Warehouse { get; set; }
    }
}
