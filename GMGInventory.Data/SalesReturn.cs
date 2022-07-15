using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class SalesReturn : BaseEntity
    {
        [Column(TypeName = "nvarchar(50)")]
        public string Number { get; set; }

        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string Remarks { get; set; }
    }
}
