using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class Region : BaseEntity
    {
        public Region()
        {
            this.Areas = new HashSet<Area>();
            this.Suppliers = new HashSet<Supplier>();
            this.PurchaseRequests = new HashSet<PurchaseRequest>();
        }

        [Column(TypeName = "nvarchar(10)")]
        public string Code { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        public bool IsActive { get; set; }

        public ICollection<Area> Areas { get; set; }
        public ICollection<Supplier> Suppliers { get; set; }
        public ICollection<PurchaseRequest> PurchaseRequests { get; set; }
    }
}
