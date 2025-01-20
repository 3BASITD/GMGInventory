using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class Currency
    {
        public Currency()
        {
            this.PurchaseOrders = new HashSet<PurchaseOrder>();
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
        public ICollection<Supplier> Suppliers { get; set; }
    }
}
