using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class PackReturn : BaseEntity
    {
        public PackReturn()
        {
            this.PackLineItems = new HashSet<PackLineItem>();
            this.PackReturnLineItems = new HashSet<PackReturnLineItem>();
        }

        [ForeignKey("PackId")]
        public int PackId { get; set; }
        public Pack Pack { get; set; }

        public ICollection<PackLineItem> PackLineItems { get; set; }
        public ICollection<PackReturnLineItem> PackReturnLineItems { get; set; }
    }
}
