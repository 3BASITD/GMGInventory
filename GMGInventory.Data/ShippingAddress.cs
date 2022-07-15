using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class ShippingAddress : BaseEntity
    {
        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string Address { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string PhoneNumber { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string FaxNumber { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string ContactPerson { get; set; }
    }
}
