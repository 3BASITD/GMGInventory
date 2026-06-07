using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class KYCClient : BaseEntity
    {
        public KYCClient()
        {
            this.KYCs = new HashSet<KYC>();
        }

        public string Name { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNumber { get; set; }
        public decimal SalesVolume { get; set; }

        public ICollection<KYC> KYCs { get; set; }
    }
}
