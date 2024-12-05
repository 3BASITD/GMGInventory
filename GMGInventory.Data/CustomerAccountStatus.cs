namespace GMGInventory.Data
{
    public class CustomerAccountStatus : BaseTypeEntity
    {
        public CustomerAccountStatus()
        {
            Customers = new HashSet<Customer>();
        }

        public ICollection<Customer> Customers { get; set; }
    }

}
