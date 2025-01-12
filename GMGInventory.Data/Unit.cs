namespace GMGInventory.Data
{
    public class Unit : BaseTypeEntity
    {
        public Unit()
        {
            Items = new HashSet<Item>();
            PurchaseRequestLineItems = new HashSet<PurchaseRequestLineItem>();
        }

        public ICollection<Item> Items { get; set; }
        public ICollection<PurchaseRequestLineItem> PurchaseRequestLineItems { get; set; }
    }
}
