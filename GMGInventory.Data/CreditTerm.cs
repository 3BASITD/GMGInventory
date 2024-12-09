using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class CreditTerm : BaseEntity
    {
        public CreditTerm()
        {
            this.Customers = new HashSet<Customer>();
            this.PurchaseOrderNews = new HashSet<PurchaseOrderNew>();
        }

        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        public int DaysToPay { get; set; }

        public ICollection<Customer> Customers { get; set; }
        public ICollection<PurchaseOrderNew> PurchaseOrderNews { get; set; }
    }
}
