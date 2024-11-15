using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class Area : BaseEntity
    {
        public Area()
        {
            this.Agents = new HashSet<Agent>();
            this.Customers = new HashSet<Customer>();
            //  this.Suppliers = new HashSet<Supplier>();
        }

        [ForeignKey("RegionId")]
        public int RegionId { get; set; }
        public Region Region { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public string Code { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [Column(TypeName = "decimal(18, 5)")]
        public decimal CommissionRate { get; set; }

        public bool IsActive { get; set; }

        public ICollection<Agent> Agents { get; set; }
        public ICollection<Customer> Customers { get; set; }
        //  public ICollection<Supplier> Suppliers { get; set; }
    }
}
