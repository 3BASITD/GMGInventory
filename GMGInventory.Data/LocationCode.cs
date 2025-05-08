using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class LocationCode : BaseEntity
    {
        public LocationCode()
        {
            ItemLocations = new HashSet<ItemLocation>();
        }

        [Column(TypeName = "nvarchar(10)")]
        public string Code { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public ICollection<ItemLocation> ItemLocations { get; set; }
    }
}
