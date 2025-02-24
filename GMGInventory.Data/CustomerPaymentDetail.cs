using GMGInventory.Common.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class CustomerPaymentDetail : BaseEntity
    {
        public CustomerPaymentDetail()
        {
        }

        [ForeignKey("CustomerPaymentId")]
        public int CustomerPaymentId { get; set; }
        public CustomerPayment CustomerPayment { get; set; }

        [ForeignKey("InvoiceId")]
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal PaidAmount { get; set; }
    }
}
