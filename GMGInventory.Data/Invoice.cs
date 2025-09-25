using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class Invoice : BaseEntity
    {
        public Invoice()
        {
            this.CustomerPaymentDetails = new HashSet<CustomerPaymentDetail>();
            this.InvoiceLineItems = new HashSet<InvoiceLineItem>();
            this.SOACRLineItems = new HashSet<SOACRLineItem>();
        }

        [ForeignKey("SalesOrderId")]
        public int SalesOrderId { get; set; }
        public SalesOrder SalesOrder { get; set; }

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

        [Column(TypeName = "nvarchar(501)")]
        public string? Remarks { get; set; }

        public DateTime InvoiceDate { get; set; }
        public DateTime? DueDate { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? DisapprovedBy { get; set; }
        public DateTime? DisapprovedDate { get; set; }

        public int? HashMicroMoveId { get; set; }
        public string? HashMicroMoveName { get; set; }
        public string? HashMicroMoveStatus { get; set; }

        [ForeignKey("HashMicroMoveById")]
        public string? HashMicroMoveById { get; set; }
        public User HashMicroMoveBy { get; set; }
        public DateTime? HashMicroMoveDate { get; set; }

        public ICollection<CustomerPaymentDetail> CustomerPaymentDetails { get; set; }
        public ICollection<InvoiceLineItem> InvoiceLineItems { get; set; }
        public ICollection<SOACRLineItem> SOACRLineItems { get; set; }

    }
}
