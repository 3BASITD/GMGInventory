using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class Invoice : BaseEntity
    {
        public Invoice()
        {
            this.CustomerPaymentDetails = new HashSet<CustomerPaymentDetail>();
            this.InvoiceLineItems = new HashSet<InvoiceLineItem>();
            this.InvoicePayments = new HashSet<InvoicePayment>();
            this.PaymentDetails = new HashSet<PaymentDetail>();
            this.SOACRLineItems = new HashSet<SOACRLineItem>();
        }

        [ForeignKey("SalesOrderId")]
        public int SalesOrderId { get; set; }
        public SalesOrder SalesOrder { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string InvoiceNumber { get; set; }

        [ForeignKey("InvoiceTypeId")]
        public int InvoiceTypeId { get; set; }
        public InvoiceType InvoiceType { get; set; }

        [ForeignKey("Status")]
        public int StatusId { get; set; }
        public Status Status { get; set; }

        [ForeignKey("PackId")]
        public int PackId { get; set; }
        public Pack Pack { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string ControlNumber { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal PaidAmount { get; set; }

        public bool IsPaid { get; set; }
        public bool IsGenerated { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string Remarks { get; set; }

        public ICollection<CustomerPaymentDetail> CustomerPaymentDetails { get; set; }
        public ICollection<InvoiceLineItem> InvoiceLineItems { get; set; }
        public ICollection<InvoicePayment> InvoicePayments { get; set; }
        public ICollection<PaymentDetail> PaymentDetails { get; set; }
        public ICollection<SOACRLineItem> SOACRLineItems { get; set; }

    }
}
