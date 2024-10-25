namespace GMGInventory.Data
{
    public class PurchaseOrderType : BaseTypeEntity
    {
        public PurchaseOrderType()
        {
            this.PurchaseOrders = new HashSet<PurchaseOrderNew>();
        }
        public ICollection<PurchaseOrderNew> PurchaseOrders { get; set; }
    }
}
