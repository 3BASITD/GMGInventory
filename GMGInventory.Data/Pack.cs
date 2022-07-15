using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class Pack : BaseEntity
    {
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

    }
}
