using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class KYCDocumentType
    {
        public KYCDocumentType()
        {
            this.KYCDocuments = new HashSet<KYCDocument>();
        }

        public int Id { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        public ICollection<KYCDocument> KYCDocuments { get; set; }
    }
}
