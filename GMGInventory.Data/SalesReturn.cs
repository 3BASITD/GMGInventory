using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class SalesReturn : BaseEntity
    {
        public SalesReturn()
        {
            this.SalesReturnLineItems = new HashSet<SalesReturnLineItem>();
        }

        [Column(TypeName = "nvarchar(50)")]
        public string SalesReturnNumber { get; set; }

        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string Remarks { get; set; }

        public ICollection<SalesReturnLineItem> SalesReturnLineItems { get; set; }
    }
}
