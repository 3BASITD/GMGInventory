using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class PackLineItem : BaseEntity
    {
        [ForeignKey("PackId")]
        public int PackId { get; set; }
        public Pack Pack { get; set; }

        [ForeignKey("ItemId")]
        public int ItemId { get; set; }
        public Item Item { get; set; }

        public int Quantity { get; set; }
        public int BoxNumber { get; set; }

        [ForeignKey("PackReturnId")]
        public int? PackReturnId { get; set; }
        public PackReturn PackReturn { get; set; }
    }
}
