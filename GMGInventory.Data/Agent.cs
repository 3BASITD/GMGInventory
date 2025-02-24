using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class Agent : BaseEntity
    {
        public Agent()
        {
            this.Customers = new HashSet<Customer>();
            this.SalesOrders = new HashSet<SalesOrder>();
        }

        [Column(TypeName = "nvarchar(50)")]
        public string Code { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? MiddleName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; }

        [ForeignKey("AreaId")]
        public int AreaId { get; set; }
        public Area Area { get; set; }

        public bool IsActive { get; set; }

        public ICollection<Customer> Customers { get; set; }
        public ICollection<SalesOrder> SalesOrders { get; set; }
    }
}
