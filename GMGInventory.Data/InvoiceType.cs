using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class InvoiceType : BaseTypeEntity
    {
        public InvoiceType()
        {
            this.Invoices = new HashSet<Invoice>();
        }

        public ICollection<Invoice> Invoices { get; set; }
    }
}
