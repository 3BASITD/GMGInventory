using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class SalesOrderDeliveryMonitoring : BaseEntity
    {
        public SalesOrderDeliveryMonitoring()
        {
            this.SalesReturnAppliedAmounts = new HashSet<SalesReturnAppliedAmount>();
        }

        [ForeignKey("SalesOrderId")]
        public int SalesOrderId { get; set; }
        public SalesOrder SalesOrder { get; set; }

        [ForeignKey("ForwarderId")]
        public int ForwarderId { get; set; }
        public Forwarder Forwarder { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Waybill { get; set; }

        public ICollection<SalesReturnAppliedAmount> SalesReturnAppliedAmounts { get; set; }
    }
}
