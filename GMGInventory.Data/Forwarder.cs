using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class Forwarder : BaseEntity
    {
        public Forwarder()
        {
            this.SalesOrderDeliveryMonitorings = new HashSet<SalesOrderDeliveryMonitoring>();
        }

        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string Address { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string PhoneNumber { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string FaxNumber { get; set; }

        //[ForeignKey("CustomerId")]
        //public int CustomerId { get; set; }
        //public Customer Customer { get; set; }

        public bool IsActive { get; set; }

        public ICollection<SalesOrderDeliveryMonitoring> SalesOrderDeliveryMonitorings { get; set; }
    }
}
