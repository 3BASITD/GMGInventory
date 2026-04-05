using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class ItemType : BaseEntity
    {
        public ItemType()
        {
            this.Brands = new HashSet<Brand>();
            this.Items = new HashSet<Item>();
        }

        [Column(TypeName = "nvarchar(50)")]
        public string Code { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }
        
        public bool IsActive { get; set; }

        public ICollection<Brand> Brands { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}
