using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class InvoiceLineItem : BaseEntity
    {
        public InvoiceLineItem()
        {
            this.SalesReturnLineItems = new HashSet<SalesReturnLineItem>();
        }

        [ForeignKey("InvoiceId")]
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

        [ForeignKey("ItemId")]
        public int ItemId { get; set; }
        public Item Item { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public ICollection<SalesReturnLineItem> SalesReturnLineItems { get; set; }
    }
}
