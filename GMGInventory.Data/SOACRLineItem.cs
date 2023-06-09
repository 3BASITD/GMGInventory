using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class SOACRLineItem : BaseEntity
    {
        [ForeignKey("SOACRId")]
        public int SOACRId { get; set; }
        public SOACR SOACR { get; set; }

        [ForeignKey("InvoiceId")]
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal InvoiceAmount { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal PaidAmount { get; set; }

        //[Column(TypeName = "decimal(18, 2)")]
        //public decimal TBRAmount { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal CreditAmount { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal DebitAmount { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal FreightCost { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? FreightWaybill { get; set; }

        //[Column(TypeName = "decimal(18, 2)")]
        //public decimal? Balance { get; set; }

        public bool? IsPaid { get; set; }
    }
}
