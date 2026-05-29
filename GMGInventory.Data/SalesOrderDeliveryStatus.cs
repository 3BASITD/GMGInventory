namespace GMGInventory.Data
{
    public class SalesOrderDeliveryStatus : BaseTypeEntity
    {
        public SalesOrderDeliveryStatus()
        {
            this.SalesOrderDeliveries = new HashSet<SalesOrderDelivery>();
        }
        public ICollection<SalesOrderDelivery> SalesOrderDeliveries { get; set; }
    }

}
