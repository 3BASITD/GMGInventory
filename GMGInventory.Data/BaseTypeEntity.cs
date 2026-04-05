using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class BaseTypeEntity
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Code { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }
    }
}
