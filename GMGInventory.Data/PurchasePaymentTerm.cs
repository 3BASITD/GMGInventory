using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class PurchasePaymentTerm : BaseTypeEntity
    {
        public PurchasePaymentTerm()
        {
            this.PurchaseOrderNews = new HashSet<PurchaseOrderNew>();
            this.PurchaseReceivings = new HashSet<PurchaseReceiving>();
        }

        public int DaysToPay { get; set; }

        public ICollection<PurchaseOrderNew> PurchaseOrderNews { get; set; }
        public ICollection<PurchaseReceiving> PurchaseReceivings { get; set; }
    }
}
