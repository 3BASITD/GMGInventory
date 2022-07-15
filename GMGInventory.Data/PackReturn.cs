using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class PackReturn : BaseEntity
    {
        [ForeignKey("PackId")]
        public int PackId { get; set; }
        public Pack Pack { get; set; }
    }
}
