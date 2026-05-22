using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class LocationCode : BaseEntity
    {
        public LocationCode()
        {
            ItemInventories = new HashSet<ItemInventory>();
        }

        [Column(TypeName = "nvarchar(50)")]
        public string Code { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public ICollection<ItemInventory> ItemInventories { get; set; }
    }
}
