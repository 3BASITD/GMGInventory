using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class DeliveryType : BaseTypeEntity
    {
        public DeliveryType()
        {
            this.SalesOrders = new HashSet<SalesOrder>();
        }

        public ICollection<SalesOrder> SalesOrders { get; set; }
    }
}
