using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class Region : BaseTypeEntity
    {
        public Region()
        {
            this.Areas = new HashSet<Area>();
        }

        public ICollection<Area> Areas { get; set; }
    }
}
