using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class Invoice : BaseEntity
    {
        [ForeignKey("SalesOrderId")]
        public int SalesOrderId { get; set; }
        public SalesOrder SalesOrder { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string InvoiceNumber { get; set; }

        [ForeignKey("InvoiceTypeId")]
        public int? InvoiceTypeId { get; set; }
        public InvoiceType InvoiceType { get; set; }

        [ForeignKey("Status")]
        public int? StatusId { get; set; }
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

    }
}
