using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class CustomerPayment : BaseEntity
    {
        public CustomerPayment()
        {
            this.CustomerPaymentDetails = new HashSet<CustomerPaymentDetail>();
            this.SOACRChecks = new HashSet<SOACRCheck>();
        }

        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string CRNumber { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal OverPayment { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Tax { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Discount { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TKTSolicitation { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal OffSet { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal BackLoad { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Cash { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal CashAdvance { get; set; }

        [Column(TypeName = "nvarchar(501)")]
        public string Remarks { get; set; }

        [ForeignKey("SOACRId")]
        public int SOACRId { get; set; }
        public SOACR SOACR { get; set; }

        //[Column(TypeName = "decimal(18, 2)")]
        //public decimal TotalAmountPaid { get; set; }

        //[Column(TypeName = "decimal(18, 2)")]
        //public decimal OverPay { get; set; }

        //[Column(TypeName = "decimal(18, 2)")]
        //public decimal TotalInvoiceAmount { get; set; }

        public ICollection<CustomerPaymentDetail> CustomerPaymentDetails { get; set; }
        public ICollection<SOACRCheck> SOACRChecks { get; set; }
    }
}
