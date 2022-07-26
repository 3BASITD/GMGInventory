using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class Unit : BaseTypeEntity
    {
        public Unit()
        {
            this.Items = new HashSet<Item>();
        }

        public ICollection<Item> Items { get; set; }
    }
}
