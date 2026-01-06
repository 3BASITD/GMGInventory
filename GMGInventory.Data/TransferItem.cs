using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class TransferItem : BaseEntity
    {
        public TransferItem()
        {
            this.TransferItemDetails = new HashSet<TransferItemDetail>();
        }

        [ForeignKey(nameof(TransferItem.Source))]
        public int SourceId { get; set; }
        public Warehouse Source { get; set; }

        [ForeignKey(nameof(TransferItem.Destination))]
        public int DestinationId { get; set; }
        public Warehouse Destination { get; set; }

        [Column(TypeName = "nvarchar(501)")]
        public string Remarks { get; set; }

        public DateTime TransferDate { get; set; }
        public string? BarcotechTransferCode { get; set; }

        public ICollection<TransferItemDetail> TransferItemDetails { get; set; }
    }
}
