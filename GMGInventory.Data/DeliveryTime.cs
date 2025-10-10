using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class DeliveryTime : BaseTypeEntity
    {
        public DeliveryTime()
        {
            this.SalesOrders = new HashSet<SalesOrder>();
        }

        public ICollection<SalesOrder> SalesOrders { get; set; }
    }
}
