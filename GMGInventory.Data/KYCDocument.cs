using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class KYCDocument : BaseEntity
    {
        public KYCDocument()
        {
        }

        [ForeignKey(nameof(KYCId))]
        public int KYCId { get; set; }
        public virtual KYC KYC { get; set; }

        [ForeignKey("KYCDocumentTypeId")]
        public int KYCDocumentTypeId { get; set; }
        public KYCDocumentType KYCDocumentType { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string FileName { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime ExpirationDate { get; set; }
    }
}
