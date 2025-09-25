using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class Currency
    {
        public Currency()
        {
            this.PurchaseOrders = new HashSet<PurchaseOrder>();
            this.PurchaseOrderNews = new HashSet<PurchaseOrderNew>();
            this.Suppliers = new HashSet<Supplier>();
        }

        public int Id { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        public bool IsActive { get; set; }

        public ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public ICollection<PurchaseOrderNew> PurchaseOrderNews { get; set; }
        public ICollection<Supplier> Suppliers { get; set; }
    }
}
