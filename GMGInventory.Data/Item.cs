using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class Item : BaseEntity
    {
        [ForeignKey("BrandId")]
        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Code { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string PartNumber { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string InterchangeNumber { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Size { get; set; }
         
        [Column(TypeName = "nvarchar(50)")]
        public string ModelNumber { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string UnitCode { get; set; }

        [ForeignKey("ItemTypeId")]
        public int? ItemTypeId { get; set; }
        public ItemType ItemType { get; set; }

        [ForeignKey("CategoryId")]
        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        //public int? MinimumQuantity { get; set; }
        //public int? MaximumQuantity { get; set; }
        public bool IsNonInventory { get; set; }
        public bool IsActive { get; set; }
        //public bool IsDeleted { get; set; }
    }
}
