using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class KYCBank : BaseEntity
    {
        public KYCBank()
        {
        }

        [ForeignKey(nameof(KYCId))]
        public int KYCId { get; set; }
        public virtual KYC KYC { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Branch { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string AccountNumber { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string ContactNumber { get; set; }
    }
}
