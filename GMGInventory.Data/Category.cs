using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class Category : BaseEntity
    {
        public Category()
        {
            this.Items = new HashSet<Item>();
            this.ItemTypes = new HashSet<ItemType>();
        }

        [Column(TypeName = "nvarchar(10)")]
        public string Code { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        public bool IsActive { get; set; }

        public ICollection<Item> Items { get; set; }
        public ICollection<ItemType> ItemTypes { get; set; }
    }
}
