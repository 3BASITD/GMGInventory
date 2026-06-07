namespace GMGInventory.Data
{
    public class KYCStatus : BaseTypeEntity
    {
        public KYCStatus()
        {
            this.KYCs = new HashSet<KYC>();
        }
        public ICollection<KYC> KYCs { get; set; }
    }
}
