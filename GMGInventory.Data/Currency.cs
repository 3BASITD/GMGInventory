using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class Currency
    {
        public Currency()
        {
            this.PurchaseOrders = new HashSet<PurchaseOrder>();
            this.PurchaseOrderNews = new HashSet<PurchaseOrderNew>();
            this.PurchaseReceivings = new HashSet<PurchaseReceiving>();
            this.PurchaseReturnNews = new HashSet<PurchaseReturnNew>();
            this.Suppliers = new HashSet<Supplier>();
        }

        public int Id { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public string Code { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [Column(TypeName = "decimal(18, 5)")]
        public decimal Rate { get; set; }

        public DateTime EffectiveDate { get; set; }
        public ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public ICollection<PurchaseOrderNew> PurchaseOrderNews { get; set; }
        public ICollection<PurchaseReceiving> PurchaseReceivings { get; set; }
        public ICollection<PurchaseReturnNew> PurchaseReturnNews { get; set; }
        public ICollection<Supplier> Suppliers { get; set; }
    }
}
