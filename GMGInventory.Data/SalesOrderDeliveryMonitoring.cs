using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class SalesOrderDeliveryMonitoring : BaseEntity
    {
        [ForeignKey("SalesOrderId")]
        public int SalesOrderId { get; set; }
        public SalesOrder SalesOrder { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Forwarder { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Waybill { get; set; }
    }
}
