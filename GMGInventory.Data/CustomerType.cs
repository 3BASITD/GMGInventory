using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class CustomerType : BaseTypeEntity
    {
        public CustomerType()
        {
            this.Customers = new HashSet<Customer>();
        }

        public ICollection<Customer> Customers { get; set; }
    }
}
