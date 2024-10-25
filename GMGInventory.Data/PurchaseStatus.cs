namespace GMGInventory.Data
{
    public class PurchaseStatus : BaseTypeEntity
    {
        public PurchaseStatus()
        {
            this.PurchaseOrders = new HashSet<PurchaseOrderNew>();
            this.PurchaseReceivings = new HashSet<PurchaseReceiving>();
            this.PurchaseRequests = new HashSet<PurchaseRequest>();
            this.PurchaseReturns = new HashSet<PurchaseReturnNew>();
        }

        public ICollection<PurchaseOrderNew> PurchaseOrders { get; set; }
        public ICollection<PurchaseReceiving> PurchaseReceivings { get; set; }
        public ICollection<PurchaseRequest> PurchaseRequests { get; set; }
        public ICollection<PurchaseReturnNew> PurchaseReturns { get; set; }
    }
}
