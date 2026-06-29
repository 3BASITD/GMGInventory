using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class Region : BaseEntity
    {
        public Region()
        {
            this.Areas = new HashSet<Area>();
            this.PurchaseOrderNews = new HashSet<PurchaseOrderNew>();
            this.Suppliers = new HashSet<Supplier>();
            this.SalesOrders = new HashSet<SalesOrder>();
        }

        [Column(TypeName = "nvarchar(50)")]
        public string Code { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        public bool IsActive { get; set; }

        public ICollection<Area> Areas { get; set; }
        public ICollection<PurchaseOrderNew> PurchaseOrderNews { get; set; }
        public ICollection<Supplier> Suppliers { get; set; }
        public ICollection<SalesOrder> SalesOrders { get; set; }
    }
}
