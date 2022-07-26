using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class Pack : BaseEntity
    {
        public Pack()
        {
            this.Invoices = new HashSet<Invoice>();
            this.PackLineItems = new HashSet<PackLineItem>();
            this.PackReturns = new HashSet<PackReturn>();
        }

        [ForeignKey("SalesOrderId")]
        public int SalesOrderId { get; set; }
        public SalesOrder SalesOrder { get; set; }

        [ForeignKey("StatusId")]
        public int StatusId { get; set; }
        public Status Status { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TotalAmount { get; set; }

        public int TotalQuantity { get; set; }
        public int QuantityInvoiced { get; set; }

        public ICollection<Invoice> Invoices { get; set; }
        public ICollection<PackLineItem> PackLineItems { get; set; }
        public ICollection<PackReturn> PackReturns { get; set; }

    }
}
