using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class InvoiceSequence
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string SequenceNumber { get; set; }
    }
}
