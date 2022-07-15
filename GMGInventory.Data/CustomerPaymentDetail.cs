using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class CustomerPaymentDetail : BaseEntity
    {
        [ForeignKey("PaymentId")]
        public int PaymentId { get; set; }
        public Payment Payment { get; set; }

        [ForeignKey("InvoiceId")]
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Balance { get; set; }
    }
}
