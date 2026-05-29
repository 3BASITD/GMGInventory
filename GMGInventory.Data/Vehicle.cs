using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class Vehicle : BaseEntity
    {
        public Vehicle()
        {
            this.SalesOrderDeliveries = new HashSet<SalesOrderDelivery>();
        }

        [Column(TypeName = "nvarchar(100)")]
        public string FleetPlateNumber { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string VehicleType { get; set; }
        public DateTime? DateRegistered { get; set; }
        public DateTime? DateExpiration { get; set; }
        public bool IsActive { get; set; }

        public ICollection<SalesOrderDelivery> SalesOrderDeliveries { get; set; }
    }
}
