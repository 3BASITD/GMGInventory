using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class KYCSignatory : BaseEntity
    {
        public KYCSignatory()
        {
            this.KYCs = new HashSet<KYC>();
        }

        public string Name { get; set; }
        public string Designation { get; set; }
        public string? Email { get; set; }
        public string? ContactNumber { get; set; }

        public ICollection<KYC> KYCs { get; set; }
    }
}
