using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class Payment : BaseEntity
    {
        public Payment()
        {
            this.CustomerPaymentDetails = new HashSet<CustomerPaymentDetail>();
        }

        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public DateTime ReceivedDate { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal ReturnedAmount { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal AdjustedAmount { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? CRNumber { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? PaymentMode { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string? Remarks { get; set; }

        public ICollection<CustomerPaymentDetail> CustomerPaymentDetails { get; set; }
        public ICollection<PaymentDetail> PaymentDetails { get; set; }
    }
}
