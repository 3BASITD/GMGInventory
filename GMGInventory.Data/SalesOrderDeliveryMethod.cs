namespace GMGInventory.Data
{
    public class SalesOrderDeliveryMethod : BaseTypeEntity
    {
        public SalesOrderDeliveryMethod()
        {
            this.SalesOrderDeliveries = new HashSet<SalesOrderDelivery>();
        }
        public ICollection<SalesOrderDelivery> SalesOrderDeliveries { get; set; }
    }

}
