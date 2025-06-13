using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class Pack : BaseEntity
    {
        public Pack()
        {
            Invoices = new HashSet<Invoice>();
            PackLineItems = new HashSet<PackLineItem>();
            PackReturns = new HashSet<PackReturn>();
            ReceptaclePrintHistories = new HashSet<ReceptaclePrintHistory>();
        }

        [ForeignKey("SalesOrderId")]
        public int SalesOrderId { get; set; }
        public SalesOrder SalesOrder { get; set; }

        [ForeignKey("StatusId")]
        public int StatusId { get; set; }
        public Status Status { get; set; }

        public DateTime PackDate { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? DisapprovedBy { get; set; }
        public DateTime? DisapprovedDate { get; set; }

        [Column(TypeName = "nvarchar(501)")]
        public string? Remarks { get; set; }

        public ICollection<Invoice> Invoices { get; set; }
        public ICollection<PackLineItem> PackLineItems { get; set; }
        public ICollection<PackReturn> PackReturns { get; set; }
        public ICollection<ReceptaclePrintHistory> ReceptaclePrintHistories { get; set; }

    }
}
