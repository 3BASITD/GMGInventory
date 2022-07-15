using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class PackReturnLineItem : BaseEntity
    {
        [ForeignKey("PackReturnId")]
        public int PackReturnId { get; set; }
        public PackReturn PackReturn { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("ItemId")]
        public int ItemId { get; set; }
        public Item Item { get; set; }

        public int BoxNumber { get; set; }

    }
}
