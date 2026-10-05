using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class ServiceLevelAgreement : BaseEntity
    {
        public ServiceLevelAgreement()
        {
            this.SalesOrders = new HashSet<SalesOrder>();
        }

        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }
        public TimeSpan? PriorAvailability { get; set; }
        public int RegionId { get; set; }
        public int HourAvailability { get; set; }
        public bool IsActive { get; set; }
        public ICollection<SalesOrder> SalesOrders { get; set; }
    }
}
