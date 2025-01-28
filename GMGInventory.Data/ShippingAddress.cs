using GMGInventory.Common.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class ShippingAddress : BaseEntity
    {
        public ShippingAddress()
        {
            ReceptaclePrintHistories = new HashSet<ReceptaclePrintHistory>();
        }

        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string Address { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? PhoneNumber { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? FaxNumber { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string ContactPerson { get; set; }

        public bool IsDefault { get; set; }

        public bool IsActive { get; set; }

        public ICollection<ReceptaclePrintHistory> ReceptaclePrintHistories { get; set; }
    }
}
