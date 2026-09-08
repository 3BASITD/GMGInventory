using System.ComponentModel.DataAnnotations;
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

        [ForeignKey("PackTypeId")]
        public int PackTypeId { get; set; }
        public PackType PackType { get; set; }

        public string? BarcotechOutboundId { get; set; }
        public decimal GrossPriceD { get; set; }

        [StringLength(50)]
        public string? BinLocation { get; set; }

    }
}
