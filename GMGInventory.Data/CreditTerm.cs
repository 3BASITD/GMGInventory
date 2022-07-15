using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class CreditTerm : BaseEntity
    {
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        public int DaysToPay { get; set; }
    }
}
