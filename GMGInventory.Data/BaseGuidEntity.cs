using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class BaseGuidEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Column(TypeName = "nvarchar(450)")]
        public string UserCreated { get; set; }

        [Column(TypeName = "nvarchar(450)")]
        public string? UserModified { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
