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

        public DateTime DateHired { get; set; }

        [Column(TypeName = "decimal(18, 5)")]
        public decimal CommissionRate { get; set; }

        [ForeignKey("AreaId")]
        public int AreaId { get; set; }
        public Area Area { get; set; }

        //public string FullName
        //{
        //    get
        //    {
        //        return FirstName + (MiddleName == null ? "" : " " + MiddleName) + " " + LastName;
        //    }
        //}

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<Customer> Customers { get; set; }
        public ICollection<SalesOrder> SalesOrders { get; set; }
    }
}
