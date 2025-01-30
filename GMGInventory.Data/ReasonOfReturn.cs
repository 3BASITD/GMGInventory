using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class ReasonOfReturn : BaseEntity
    {
        public ReasonOfReturn()
        {
            SalesReturnLineItems = new HashSet<SalesReturnLineItem>();
        }

        [Column(TypeName = "nvarchar(10)")]
        public string Code { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        public bool IsActive { get; set; }
        public ICollection<SalesReturnLineItem> SalesReturnLineItems { get; set; }
    }
}
