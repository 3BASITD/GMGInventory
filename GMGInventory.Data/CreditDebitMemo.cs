using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class CreditDebitMemo : BaseEntity
    {
        [Column(TypeName = "nvarchar(50)")]
        public string MemoNumber { get; set; }

        [ForeignKey("InvoiceId")]
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }

        [ForeignKey("CreditDebitMemoTypeId")]
        public int CreditDebitMemoTypeId { get; set; }
        public CreditDebitMemoType CreditDebitMemoType { get; set; }
    }
}