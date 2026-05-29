using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class Pick : BaseEntity
    {
        public Pick()
        {
        }

        [ForeignKey("SalesOrderId")]
        public int SalesOrderId { get; set; }
        public SalesOrder SalesOrder { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Status { get; set; }

        public DateTime PickDate { get; set; }
    }
}
