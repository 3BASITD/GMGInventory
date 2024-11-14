using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class HomeImage : BaseEntity
    {
        [Column(TypeName = "nvarchar(MAX)")]
        public string File { get; set; }

        [Column(TypeName = "nvarchar(25)")]
        public string? Title { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string? Description { get; set; }

        public int DisplayOrder { get; set; }

        public int DisplayTime { get; set; }
    }
}
