using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class InvoicePayment : BaseEntity
    {
        [ForeignKey("InvoiceId")]
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string BankName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string BankBranch { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string CheckNumber { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal CheckAmount { get; set; }

        public DateTime CheckDate { get; set; }
        public bool IsCheckClear { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string CheckReferenceNumber { get; set; }
    }
}
