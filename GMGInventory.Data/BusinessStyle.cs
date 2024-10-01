using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class BusinessStyle : BaseEntity
    {
        public BusinessStyle()
        {
        }

        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        public bool IsActive { get; set; }

    }
}
