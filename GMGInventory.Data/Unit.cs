namespace GMGInventory.Data
{
    public class Unit : BaseTypeEntity
    {
        public Unit()
        {
            this.Items = new HashSet<Item>();
            this.PurchaseOrderNewLineItems = new HashSet<PurchaseOrderNewLineItem>();
            this.PurchaseReceivingLineItems = new HashSet<PurchaseReceivingLineItem>();
            this.PurchaseReturnLineItems = new HashSet<PurchaseReturnNewLineItem>();
            this.PurchaseRequestLineItems = new HashSet<PurchaseRequestLineItem>();
        }

        public ICollection<Item> Items { get; set; }
        public ICollection<PurchaseOrderNewLineItem> PurchaseOrderNewLineItems { get; set; }
        public ICollection<PurchaseReceivingLineItem> PurchaseReceivingLineItems { get; set; }
        public ICollection<PurchaseReturnNewLineItem> PurchaseReturnLineItems { get; set; }
        public ICollection<PurchaseRequestLineItem> PurchaseRequestLineItems { get; set; }
    }
}
