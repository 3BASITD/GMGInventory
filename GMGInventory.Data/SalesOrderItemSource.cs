using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class SalesOrderItemSource : BaseEntity
    {
        public SalesOrderItemSource()
        {
        }

        [Column(TypeName = "nvarchar(50)")]
        public string SourceOrderNumber { get; set; }

        [ForeignKey("SalesOrderId")]
        public int? SalesOrderId { get; set; }
        public SalesOrder SalesOrder { get; set; }

        [ForeignKey("ItemId")]
        public int? ItemId { get; set; }
        public Item Item { get; set; }

        public string Status { get; set; }
    }
}
