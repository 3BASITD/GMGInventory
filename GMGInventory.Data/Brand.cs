using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class Brand : BaseEntity
    {
        [Column(TypeName = "nvarchar(10)")]
        public string Code { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [ForeignKey("ItemTypeId")]
        public int ItemTypeId { get; set; }
        public ItemType ItemType { get; set; }

        public bool IsActive { get; set; }
    }
}
