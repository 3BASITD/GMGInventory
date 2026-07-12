using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class KYCSignatory : BaseEntity
    {
        public KYCSignatory()
        {
        }

        [ForeignKey(nameof(KYCId))]
        public int KYCId { get; set; }
        public virtual KYC KYC { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Designation { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? Email { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? ContactNumber { get; set; }
    }
}
