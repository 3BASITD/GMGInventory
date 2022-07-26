using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class Status : BaseTypeEntity
    {
        public Status()
        {
            this.Invoices = new HashSet<Invoice>();
            this.Packs = new HashSet<Pack>();
            this.PurchaseOrders = new HashSet<PurchaseOrder>();
            this.SalesOrders = new HashSet<SalesOrder>();
        }

        public ICollection<Invoice> Invoices { get; set; }
        public ICollection<Pack> Packs { get; set; }
        public ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public ICollection<SalesOrder> SalesOrders { get; set; }
    }
}
