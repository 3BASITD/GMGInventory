using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class KYCBank : BaseEntity
    {
        public KYCBank()
        {
            this.KYCs = new HashSet<KYC>();
        }

        public string Name { get; set; }
        public string Branch { get; set; }
        public string AccountNumber { get; set; }
        public string ContactNumber { get; set; }

        public ICollection<KYC> KYCs { get; set; }
    }
}
