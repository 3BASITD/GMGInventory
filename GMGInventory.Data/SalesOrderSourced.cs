using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class SalesOrderSourced : BaseEntity
    {
        public SalesOrderSourced()
        {
            //this.SalesOrderLineItems = new HashSet<SalesOrderLineItem>();
        }

        [Column(TypeName = "nvarchar(50)")]
        public string SourceOrderNumber { get; set; }

        [ForeignKey("SalesOrderId")]
        public int? SalesOrderId { get; set; }
        public SalesOrder? SalesOrder { get; set; }

        public string? Status { get; set; }

        //public ICollection<SalesOrderLineItem> SalesOrderLineItems { get; set; }
    }
}
