using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class KYCClient : BaseEntity
    {
        public KYCClient()
        {
        }

        [ForeignKey(nameof(KYCId))]
        public int KYCId { get; set; }
        public virtual KYC KYC { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? ContactPerson { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? ContactNumber { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? SalesVolume { get; set; }
    }
}
