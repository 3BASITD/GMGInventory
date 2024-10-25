namespace GMGInventory.Data
{
    public class PurchaseOrderType : BaseTypeEntity
    {
        public PurchaseOrderType()
        {
            this.PurchaseRequests = new HashSet<PurchaseRequest>();
            this.PurchaseRequestPurposes = new HashSet<PurchaseRequestPurpose>();
        }
        public ICollection<PurchaseRequest> PurchaseRequests { get; set; }
        public ICollection<PurchaseRequestPurpose> PurchaseRequestPurposes { get; set; }
    }
}
