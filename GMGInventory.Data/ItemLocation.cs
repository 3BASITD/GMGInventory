using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class ItemLocation : BaseEntity
    {
        [ForeignKey("ItemId")]
        public int ItemId { get; set; }
        public Item Item { get; set; }


        [ForeignKey("LocationCodeId")]
        public int LocationCodeId { get; set; }
        public LocationCode LocationCode { get; set; }
    }
}
