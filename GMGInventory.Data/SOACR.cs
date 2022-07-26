using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class SOACR : BaseEntity
    {
        public SOACR()
        {
            this.CustomerPayments = new HashSet<CustomerPayment>();
            this.SOACRLineItems = new HashSet<SOACRLineItem>();
        }

        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public DateTime? SOAFrom { get; set; }
        public DateTime? SOATo { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal AmountToPay { get; set; }

        [ForeignKey("SOACRTypeId")]
        public int SOACRTypeId { get; set; }
        public SOACRType SOACRType { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TotalCMAAmount { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TotalDMAAmount { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TotalDMRAmount { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TotalBalance { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TotalFreightCost { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TotalAmount { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal OverPayment { get; set; }

        public ICollection<CustomerPayment> CustomerPayments { get; set; }
        public ICollection<SOACRLineItem> SOACRLineItems { get; set; }
    }
}
