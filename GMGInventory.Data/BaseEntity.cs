using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class BaseEntity
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(450)")]
        public string UserCreated { get; set; }

        [Column(TypeName = "nvarchar(450)")]
        public string? UserModified { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
