using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class PriceType : BaseTypeEntity
    {
        public PriceType()
        {
            this.SalesOrders = new HashSet<SalesOrder>();
        }

        public ICollection<SalesOrder> SalesOrders { get; set; }
    }
}
