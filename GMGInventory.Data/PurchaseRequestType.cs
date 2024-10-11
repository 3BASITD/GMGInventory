namespace GMGInventory.Data
{
    public class PurchaseRequestType : BaseTypeEntity
    {
        public PurchaseRequestType()
        {
            this.PurchaseRequests = new HashSet<PurchaseRequest>();
            this.PurchaseRequestPurposes = new HashSet<PurchaseRequestPurpose>();
        }
        public ICollection<PurchaseRequest> PurchaseRequests { get; set; }
        public ICollection<PurchaseRequestPurpose> PurchaseRequestPurposes { get; set; }
    }
}
