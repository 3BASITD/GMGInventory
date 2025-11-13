using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class SalesOrderSourced : BaseEntity
    {
        public SalesOrderSourced()
        {
        }

        [Column(TypeName = "nvarchar(50)")]
        public string SourceOrderNumber { get; set; }

        [ForeignKey("SalesOrderId")]
        public int SalesOrderId { get; set; }
        public SalesOrder SalesOrder { get; set; }
    }
}
