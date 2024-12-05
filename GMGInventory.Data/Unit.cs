namespace GMGInventory.Data
{
    public class Unit : BaseTypeEntity
    {
        public Unit()
        {
            this.Items = new HashSet<Item>();
            this.PurchaseRequestLineItems = new HashSet<PurchaseRequestLineItem>();
        }

        public ICollection<Item> Items { get; set; }
        public ICollection<PurchaseRequestLineItem> PurchaseRequestLineItems { get; set; }
    }
}
